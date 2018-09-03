Imports System.Data

Partial Class ACC_Edit
    Inherits BasePage

    Dim V As New clsValidatie
    Dim P As New clsPage
    Dim PI As New clsPageItems

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        onlineCheck()

        If Page.IsPostBack = False Then

            iPartijID = sPartijID()
            sLanguage = RouteData.Values("language").ToString()
            sQs = RouteData.Values("page").ToString()

            Dim P As New clsPage
            P.dt = P.sPagesByActief(sLanguage, "Account", 0, True, iPartijIDBeheerder)
            repMenu.DataSource = P.dt
            repMenu.DataBind()

            PI.sPI(Page, sLanguage, sQs, False, False, True)

            aClose.NavigateUrl = P.sPageUrlByGuid(sLanguage, "d83e9d6e-b4b9-4faa-a254-a755a10717c7")

            Dim LAND As New clsLanden
            LAND.dt = LAND.sLanden(sLanguage, iPartijIDBeheerder)
            For Each LAND.dr In LAND.dt.Rows
                Dim opt As New ListItem
                opt.Value = LAND.dr.Item("iLandID")
                opt.Text = LAND.dr.Item("sLandNaam")
                opt.Attributes.Add("data-id", LAND.dr.Item("sData"))
                opt.Attributes.Add("data-code", LAND.dr.Item("sLandCode").ToString.ToUpper)
                ddlLand.Items.Add(opt)
                ddlLand2.Items.Add(opt)
            Next

            Dim LI As New clsLijstItems
            Dim iTaalID As Long = sTaalID(sLanguage)

            VeldenVullen(iPartijID)

            Dim sSessieID As String = ""
            If Request.Cookies("SessieID") IsNot Nothing Then
                'Cookie bestaat return SessieID
                sSessieID = Request.Cookies("SessieID").Value
            Else
                'Cookie bestaat niet, maak aan
                Dim guid As Guid = Guid.NewGuid
                sSessieID = guid.ToString()
                Response.Cookies("SessieID").Value = sSessieID
                Response.Cookies("SessieID").Expires = Date.Now.AddDays(1)
                Response.Cookies("SessieID").Path = "/"
            End If
        End If
    End Sub

    Private Sub VeldenVullen(iPartijID As Long)

        hidPartijID.Value = iPartijID
        'PI.sPI(Page, "EN", sQs, False, False, True)

        Dim ACC As New clsAccounts
        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
        End If

        Dim PAR As New clsPartijen
        PAR.dt = PAR.sPartijByPartijID(iPartijID)
        If PAR.dt.Rows.Count > 0 Then
            PAR.dr = PAR.dt.Rows(0)
            'txtCompany.Text = PAR.dr.Item("sNaam")
            'txtVAT.Text = PAR.dr.Item("sBtwNummer")
        End If

        Dim PERS As New clsPersonen
        PERS.dt = PERS.sPersoonByPersID(iPersID)
        If PERS.dt.Rows.Count > 0 Then
            PERS.dr = PERS.dt.Rows(0)
            'rblAanhef.SelectedValue = PERS.drPers.sAanhef
            txtFirstName.Text = PERS.dr.Item("sVoorletters")
            txtSurname.Text = PERS.dr.Item("sNaam")
            txtEmail.Text = PERS.dr.Item("sEmail")
            txtPhone.Text = PERS.dr.Item("sTelefoon")
        End If
        ltlTitle.Text = "Welcome " & txtFirstName.Text & " " & txtSurname.Text
        'ltlUsername.Text = PERS.dr.Item("sVoorletters") & " " & PERS.dr.Item("sNaam")


        Dim ADR As New clsAdressen
        ADR.dt = ADR.sAdresByPartijIDAndType(iPartijID, "factuur")
        If ADR.dt.Rows.Count > 0 Then
            ADR.dr = ADR.dt.Rows(0)
            'txtPostcode.Value = ADR.drADR.sPostCode 'ADR.drADR.sWijkCode & " " & ADR.drADR.sStraatCode.ToUpper()
            txtZipCode.Text = ADR.dr.Item("sPostCode")
            txtHouseNr.Text = ADR.dr.Item("sHuisNr")
            txtAdd.Text = ADR.dr.Item("sToev")
            txtAddress.Text = ADR.dr.Item("sStraat")
            txtResidence.Text = ADR.dr.Item("sPlaats")
            hdfProvincie.Value = ADR.dr.Item("sProvincie")
            ddlLand.SelectedValue = ADR.dr.Item("iLandID")
            hdfLatitudeX.Value = ADR.dr.Item("rLatitudeX")
            hdfLongitudeY.Value = ADR.dr.Item("rlongitudeY")
        End If

        ADR.dt = ADR.sAdresByPersID(iPartijID, iPersID, "lever")
        If ADR.dt.Rows.Count > 0 Then
            ADR.dr = ADR.dt.Rows(0)
            txtZipCode2.Text = ADR.dr.Item("sPostCode")
            txtHouseNr2.Text = ADR.dr.Item("sHuisNr")
            txtAdd2.Text = ADR.dr.Item("sToev")
            txtAddress2.Text = ADR.dr.Item("sStraat")
            txtResidence2.Text = ADR.dr.Item("sPlaats")
            hdfProvincie2.Value = ADR.dr.Item("sProvincie")
            ddlLand2.SelectedValue = ADR.dr.Item("iLandID")
            hdfLatitudeX2.Value = ADR.dr.Item("rLatitudeX")
            hdfLongitudeY2.Value = ADR.dr.Item("rlongitudeY")
        End If

        'Dim PARTIJ As New clsPartijen
        'PARTIJ.drPartij = PARTIJ.daPartij.sPartij(iPartijID).Rows(0)
        'txtKVK.Text = PARTIJ.drPartij.sKvkNummer
        'txtBTW.Text = PARTIJ.drPartij.sBtwNummer

        'Dim ART As New clsArtikelen
        'ART.dtART = ART.daART.sArtikelenByPartijID(iPartijID)
        'If ART.dtART.Rows.Count > 0 Then
        '    repArtikelen.DataSource = ART.daART.sArtikelenByPartijID(iPartijID)
        '    repArtikelen.DataBind()
        '    divArtikelen.Visible = True

        'Else
        '    divArtikelen.Visible = False
        'End If
    End Sub


    Dim STF As New clsArtikelStaffel
    Dim CT As New clsCategorie

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim bOk As Boolean = True

        If V.validateAddress(ddlLand, txtZipCode, txtHouseNr, txtAdd, txtAddress, txtResidence) = False Then
            ltlError.Text = V.msg
            Exit Sub
        End If

        If V.ValidateDetails(txtFirstName, txtSurname, txtEmail, txtPhone) = False Then
            Exit Sub
        End If

        If V.validateAddress2(ddlLand2, txtZipCode2, txtHouseNr2, txtAdd2, txtAddress2, txtResidence2) = False Then
            ltlError.Text = V.msg
            Exit Sub
        End If

        iPartijID = hidPartijID.Value
        Dim iPersID As Long
        Dim ACC As New clsAccounts
        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")

            Dim PERS As New clsPersonen
            PERS.uPersoon("", txtFirstName.Text & " " & txtSurname.Text, txtFirstName.Text, "", txtSurname.Text, "", "", "", "", txtEmail.Text, txtPhone.Text, txtPhone.Text, "", True, Date.Now, 0, 0, Date.Now, True, "actief", "", Decimal.Parse("0,00"), iPartijIDBeheerder, iPersID)

            Dim ADR As New clsAdressen

            ADR.uAdresByPartijIDAndTypeAndPartijIDBeheerder(iPartijID, "factuur", "", "", txtZipCode.Text.ToString().ToUpper().Replace(" ", ""), txtHouseNr.Text, txtAdd.Text, txtAddress.Text, "", txtResidence.Text, "", hdfProvincie.Value, ddlLand.SelectedItem.Text, ddlLand.SelectedValue, 0, "", 0, "", "", "", "", iPartijIDBeheerder)

            ADR.uAdresByPersIDAndTypeAndPartijIDBeheerder(iPersID, "lever", "", "", txtZipCode2.Text.ToString().ToUpper().Replace(" ", ""), txtHouseNr2.Text, txtAdd2.Text, txtAddress2.Text, "", txtResidence2.Text, "", hdfProvincie2.Value, ddlLand2.SelectedItem.Text, ddlLand2.SelectedValue, 0, "", 0, "", "", "", "", iPartijIDBeheerder)

            Dim PT As New clsPartijen
            'PT.uPartij(iPartijIDBeheerder, txtCompany.text, "", "", "", "", txtVAT.text, "", "", Date.Now, "", "", "", "", "", "bedrijf", "actief", "", "", "", "", "", "", "", "", "", "", "", "", hidPartijID.Value)

            ltlError.Text = Resources.Resource.GegevensGewijzigd
        Else
            ltlError.Text = Resources.Resource.defaultError
        End If
    End Sub

    'Protected Sub btnUpdatePass_Click(sender As Object, e As EventArgs) Handles btnUpdatePass.Click
    '    If IsOnline() = False Then
    '        Response.Redirect("/login")
    '    End If


    '    Dim mu As MembershipUser = Membership.GetUser(User.Identity.Name)
    '    mu.ChangePassword(mu.GetPassword(), txtWW.Text)
    '    ltlInfoPass.Text = Resources.Resource.GegevensGewijzigd

    'End Sub
End Class
