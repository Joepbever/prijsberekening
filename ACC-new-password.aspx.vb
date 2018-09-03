Imports System.Data

Partial Class newPassowrd
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
    End Sub

    Dim SSO As New clsSSO
    Private Sub ControleerSSO()
        Dim U As New clsUtility
        Dim guid = U.Decrypt(Request.QueryString("guid"))

        Dim guidList As New List(Of String)(guid.Split(","))

        Dim qId As String = guidList(2)
        Dim qI As String = guidList(1)

        Dim dId = U.Decrypt(qId)
        Dim ticksComapre As Boolean = String.Compare(qI, dId, False)

        If ticksComapre = True Then
            Response.Redirect("/")
        End If

        If (guidList(3) = "QuestionValidationSucceeded") = False Then
            Response.Redirect("/")
        End If

        Dim dtNow As Date = Now
        dtNow.AddHours(1)

        Dim sGuid As String = guidList(0)
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

    Protected Sub btnRetrievePassword_Click(sender As Object, e As EventArgs) Handles btnUpdatePassword.Click

        Dim V As New clsValidatie
        If V.ValidatePassword(txtNewPassword, txtNewPasswordConfirm) = False Then
            ltlError.Text = V.msg
            Exit Sub
        End If

        Dim newGuid As Guid = Guid.Parse(hidUserID.Value)
        Dim mu As MembershipUser = Membership.Providers("AspNetSqlMembershipProviderAdmin").GetUser(newGuid, False)

        If mu.ChangePassword(mu.ResetPassword(), txtNewPassword.Text.Trim()) = False Then
            ltlError.Text = "Resetting password failed. Try again."
            Exit Sub
        Else
            SSO.uActive(False, hidSSOID.Value, iPartijIDBeheerder)
            Response.Redirect(P.sPageUrlByGuid(sLanguage, "6c6b98de-1e42-4b7c-8d17-970defc66065"))
        End If
    End Sub
End Class