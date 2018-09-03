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
    End Sub

    Dim SSO As New clsSSO
    Private Sub ControleerSSO()
        If Request.QueryString("id") <> "s123UlKaj123wse3800Vq12!-" Then
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

    Protected Sub btnRetrievePassword_Click(sender As Object, e As EventArgs) Handles btnUpdatePassword.Click
       
        Dim sNewPassword As String = txtNewPassword.Text
        Dim sNewPasswordConfirm As String = txtNewPasswordConfirm.Text

        Dim bOk As Boolean = True

        If Trim(sNewPassword) = "" Then bOk = False
        If Trim(sNewPasswordConfirm) = "" Then bOk = False

        If bOk = False Then
            ltlError.Text = Resources.Resource.ValidateVeldenVerplicht
            Exit Sub
        End If

        Dim bPassword As Boolean = String.Compare(txtNewPassword.Text, txtNewPasswordConfirm.Text, False)
        If bPassword = True Then
            ltlError.Text = Resources.Resource.WachtwoordKomtNietOvereen
            txtNewPassword.Text = ""
            txtNewPasswordConfirm.Text = ""
            Exit Sub

        End If
        Dim newGuid As Guid = Guid.Parse(hidUserID.Value)
        Dim mu As MembershipUser = Membership.GetUser(newGuid)
        If mu.ChangePassword(mu.ResetPassword(), txtNewPassword.Text) = False Then
            ltlError.Text = Resources.Resource.WachtwoordKomtNietOvereen
            Exit Sub
        Else

            SSO.uActive(False, hidSSOID.Value, iPartijIDBeheerder)
            Response.Redirect(P.sPageUrlByGuid(sLanguage, "6c6b98de-1e42-4b7c-8d17-970defc66065"))
        End If



    End Sub
End Class