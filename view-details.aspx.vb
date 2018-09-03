
Imports System.Data

Partial Class ACC_view_details
    Inherits BasePage


    'Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    '    If Page.IsPostBack = False Then

    '        InitPage()

    '        Dim PERS As New clsPersonen
    '        Dim dt As DataTable = PERS.sPersoonByPersID(iPersID)

    '        For Each row As DataRow In dt.Rows
    '            lblName.Text = row("sNaam").ToString()
    '            lblDateOfBirth.Text = row("dtGeboorteDatum").ToString()
    '            lblMobile.Text = row("sTelefoon").ToString()
    '            lblEmail.Text = row("sEmail").ToString()
    '        Next

    '        Dim ADR As New clsAdressen
    '        dt = ADR.sAdresByPartijAndPersoonID(iPartijID, iPersID)

    '        For Each row As DataRow In dt.Rows
    '            lblAdress1.Text = row("sStraat") + " " + row("sHuisNr")
    '            lblAdress2.Text = row("Adressline2")
    '            lblStateCountry.Text = row("sProvincie") + " " + row("sLand")
    '            lblZipcodeCity.Text = row("sPostcode") + " " + row("sPlaats")
    '        Next

    '    End If

    'End Sub

    Protected Sub btnEditDetails_Click(sender As Object, e As EventArgs)
        Response.Redirect("/ACC-EditDetails.aspx")
    End Sub
    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs)
        Response.Redirect("/ACC-ChangePassword.aspx")
    End Sub
    Protected Sub btnSecurity_Click(sender As Object, e As EventArgs)
        Response.Redirect("/ACC-securityquestion.aspx")
    End Sub
    Protected Sub btnCloseAccount_Click(sender As Object, e As EventArgs)
        Response.Redirect("/ACC-CloseAccount.aspx")
    End Sub
End Class
