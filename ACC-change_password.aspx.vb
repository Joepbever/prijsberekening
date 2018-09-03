
Partial Class ACC_ChangePassword
    Inherits BasePage

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        onlineCheck()


        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage) 'literals vullen

            Dim P As New clsPage
            P.dt = P.sPagesByActief(sLanguage, "Account", 0, True, iPartijIDBeheerder)
            repMenu.DataSource = P.dt
            repMenu.DataBind()
        End If
    End Sub

    Protected Sub btnSavePassword_Click(sender As Object, e As EventArgs) Handles btnSavePassword.Click
        Dim PER As New clsPersonen
        Dim user As MembershipUser = Membership.GetUser(True)
        PER.sPersoonByPartijIDAndUsername(322, user.UserName)
        Dim newPassword As String = txtNewPassword.Text
        Dim newPasswordDouble As String = txtNewPasswordRepeat.Text
        Dim oldPassword As String = txtOldPassword.Text
        'Dim dateOfBirth As Date
        'If PER.dt.Rows.Count > 0 Then
        '    dateOfBirth = PER.dt.Rows(0)("dtGeboorteDatum")
        'End If

        'If dateOfBirth = Date.Parse("#1/1/0001 12:00:00 AM#") Then
        '    ltlError.Text = "Date of birth not found, please contact customer support."
        '    Exit Sub
        'End If

        'dateOfBirth = New Date(1999, 7, 13)

        'ltlError.Text = ""
        'Dim passwordTodate As Date
        'Date.TryParse(newPassword, passwordTodate)
        Dim validate As New clsValidatie
        If validate.ValidatePassword(txtNewPassword, txtNewPasswordRepeat) = False Then
            ltlError.Text = validate.msg
            Exit Sub
        End If

        If user.ChangePassword(oldPassword, newPassword) = False Then
            ltlError.Text = Resources.Resource.ErrorWachtwordInvalid
            Exit Sub
        End If

        'Change the password
        Dim HIS As New clsHistorie
        Dim P As New clsPage
        HIS.iHistorie(iPartijID, iPersID, "ChangePassword", 0, "ChangePassword", "ChangePassword", "ChangePassword", "ChangePassword", "ChangePassword")
        Response.Redirect(P.sPageUrlByGuid(sLanguage, "f2221bbe-7eb0-4015-be2e-6e78963a6175"))


    End Sub
End Class
