Imports System.Data

Partial Class Mijn_account
    Inherits BasePage

    Dim P As New clsPage
    Dim PI As New clsPageItems
    Dim U As New clsUtility

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ControleerSSO()

        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)


        If Page.IsPostBack = False Then
            If IsOnline() Then
                Response.Redirect(P.sPageUrlByGuid(sLanguage, "6fa52603-ebeb-4f21-be02-d20e9b24ef80"))
            End If

            iPartijID = sPartijID()
            sQs = RouteData.Values("page").ToString()

            PI.sPI(Page, sLanguage, sQs, False, False, True)
        End If


        Dim newGuid As Guid = Guid.Parse(hidUserID.Value)
        Dim mu As MembershipUser = Membership.GetUser(newGuid)
        ltlQuestion.Text = mu.PasswordQuestion()

    End Sub

    Dim SSO As New clsSSO
    Private Sub ControleerSSO()
        Dim qId As String = Request.QueryString("id")
        Dim qI As String = Request.QueryString("i")

        Dim dId = U.Decrypt(qId)
        Dim ticksComapre As Boolean = String.Compare(qI, dId, False)

        If ticksComapre = True Then
            Response.Redirect("/")
            Exit Sub
        End If

        Dim dtNow As Date = Now
        dtNow.AddHours(1)

        Dim sGuid As String = Request.QueryString("guid")
        SSO.dt = SSO.sSSO(sGuid, iPartijIDBeheerder)
        If SSO.dt.Rows.Count > 0 Then
            SSO.dr = SSO.dt.Rows(0)
            If CBool(SSO.dr.Item("bActive")) Then
                Dim dtSSO As Date = SSO.dr.Item("dtDatum")
                If dtSSO < dtNow Then
                    hidUserID.Value = SSO.dr.Item("sUserID").ToString()
                    hidSSOID.Value = SSO.dr.Item("iSSOID").ToString()
                Else
                    Response.Redirect("/")
                End If
            Else
                Response.Redirect("/")
            End If
        Else
            Response.Redirect("/")
        End If
    End Sub

    Private Sub btnCheckAnswer_Click(sender As Object, e As EventArgs) Handles btnCheckAnswer.Click
        Dim newGuid As Guid = Guid.Parse(hidUserID.Value)
        Dim mu As MembershipUser = Membership.GetUser(newGuid)
        Try
            mu.ResetPassword(txtAnswer.Text.Trim())
            Dim sGuid As String = Request.QueryString("guid") & "," & Request.QueryString("i") & "," & Request.QueryString("id") & "," & "QuestionValidationSucceeded"
            sGuid = U.Encrypt(sGuid)
            Response.Redirect(P.sPageUrlByGuid(sLanguage, "fe874df5-677e-499f-b34a-8a1feb943542") & "/?guid=" & sGuid)
        Catch ex As Exception
            ltlError.Text = ex.Message
            divError.Style.Add("display", "block")
        End Try

    End Sub
End Class