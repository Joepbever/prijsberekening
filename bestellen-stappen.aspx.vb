Imports System.Globalization
Imports System.Web.Services
Imports System.Data
Imports System.Web.Routing

Partial Class Bestellen_stappen
    Inherits BasePage

    Dim V As New clsValidatie
    Dim PT As New clsPartijen
    Dim TPP As New clsTussPersonenPartijen
    Dim PERS As New clsPersonen
    Dim ACC As New clsAccounts
    Dim ADR As New clsAdressen
    Dim iAdresID, iPersID2, iAdresID2 As Long
    Dim M As New clsMail
    Dim MOLLIE As New clsMollie
    Dim FACRGL As New clsFacRgl
    Dim ST As New clsSettings
    Dim L As New clsLanden
    Dim sLanguage As String
    Protected P As New clsPage


    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If
        iTaalID = sTaalID(sLanguage)

        ST.InitWebshopSettings(iPartijIDBeheerder)
        If Page.IsPostBack = False Then
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
            Dim sQs As String = RouteData.Values("page").ToString()

            Dim FACRGL As New clsFacRgl


            If ST.bKortingsCode = False Then
                divKorting.Visible = False
            End If
            If ST.bCompany = False Then
                divCompany.Visible = False
            End If
            'FACRGL.dtFacRgl = FACRGL.daFacRgl.sFacRgls_Session(sSessieID)
            'If FACRGL.dtFacRgl.Rows.Count < 1 Then
            '    Response.Redirect("~" & P.sPageUrlByGuid(sLanguage, "173814a4-142b-4592-b25c-6c552b9e2d3a"))
            'End If

            MOLLIE.ListMethods(rblBetaalmethode)
            rblBetaalmethode.Items(0).Selected = True

            MOLLIE.ListIssuers(ddlBank)
            ddlBank.Items(0).Selected = True

            aWinkelwagen.NavigateUrl = P.sPageUrlByGuid(sLanguage, "3659abe8-3b74-41a4-9889-c2d8638ba811")

            If IsOnline() Then
                'logged in
                iPartijID = sPartijID()
                hidPartijID.Value = iPartijID
                hidAccount.Value = "false" 'zodat dit niet gevalideerd wordt ivm formvalidation
                VeldenVullen(iPartijID)


                divAccount.Visible = False
                divCbAccount.Visible = False

                'divLogin.Visible = False
                'alaccount.Visible = False
                'PT.sPartijItems(iPartijID)
                'If PT.sRekening.ToLower() = "true" Then
                '    Dim opt2 As New ListItem
                '    opt2.Value = "rekening"
                '    opt2.Text = Resources.Resource.OpRekening
                '    rblBetaalmethode.Items.Add(opt2)
                'End If
            Else
                divAccount.Visible = True
                divCbAccount.Visible = True
            End If

            Dim PI As New clsPageItems
            PI.sPI(Me.Page, sLanguage, sQs, False, False, True)


            L.dt = L.sLanden(sLanguage, iPartijIDBeheerder)
            For Each L.dr In L.dt.Rows
                Dim opt As New ListItem
                opt.Value = L.dr.Item("iLandID")
                opt.Text = L.dr.Item("sLandNaam")
                opt.Attributes.Add("data-id", L.dr.Item("sData"))
                opt.Attributes.Add("data-code", L.dr.Item("sLandCode"))
                ddlLand.Items.Add(opt)
                ddlLand2.Items.Add(opt)
            Next
            hidArtikelVerzendID.Value = 0
            If ST.bVerzendmethode Then
                Dim A As New clsArtikelen
                A.dt = A.sArtikelenByArtikelgroep(iPartijIDBeheerder, "verzendkosten")
                If A.dt.Rows.Count > 0 Then
                    rblVerzendmethode.DataSource = A.dt
                    rblVerzendmethode.DataBind()
                    rblVerzendmethode.Items(0).Selected = True
                    hidArtikelVerzendID.Value = rblVerzendmethode.SelectedValue
                End If
            Else
                divVerzendMethode.Visible = False
            End If



            ' aAV.HRef = P.sPageUrlByGuid(sLanguage, "52479d6b-48ae-405c-985c-afafece044e7")

            BepaalVerzendkosten()
            InitWw(sSessieID)
        End If
    End Sub

    Private Sub InitWw(ByVal sSession As String)

        Dim UTIL As New clsUtility
        FACRGL.dt = FACRGL.sFacRgls_SessionAndImg(sSession, iPartijIDBeheerder, "artikel")
        repWinkelwagen.DataSource = FACRGL.dt
        repWinkelwagen.DataBind()

        UTIL.updateCart(Page, sSession)

        FACRGL.sInfo(sSession, iPartijIDBeheerder)
        If FACRGL.iAantalRegels > 0 Then
            ltlBedragEx.Text = FACRGL.sBedrag
            ltlVerzendkosten.Text = FACRGL.sVerzendkosten
            ' ltlTotaalIn.Text = FACRGL.sBedragMinusKortingZonderVerzendkosten
            ltlTotaalInclVerzenden.Text = "&euro; " & FACRGL.sBedragMinusKorting
            ltlDiscount.Text = FACRGL.sDiscount
            If FACRGL.sDiscount = "0" Then
                divKortingTonen.Visible = False
            Else
                divKortingTonen.Visible = True
            End If
            Dim iAantal As String = FACRGL.scAantalFacRgls_Session(sSession, iPartijIDBeheerder)
            If CInt(iAantal) > 0 Then
                btnOrder.Enabled = True
            Else
                ltlBedragEx.Text = "0,00"
                ' ltlTotaalIn.Text = "0,00"
                btnOrder.Enabled = False
            End If

            If FACRGL.bDiscount Then
                divKorting.Visible = False
            End If

        Else
            divKortingTonen.Visible = False
            ltlTotaalInclVerzenden.Text = "&euro; " & "0.00"
            ltlVerzendkosten.Text = "&euro; " & "0.00"
            ltlBedragEx.Text = "&euro; " & "0.00"
            ' ltlTotaalIn.Text = "0.00"
            ltlDiscount.Text = "0.00"
        End If

        '  UTIL.updateCart(Me.Page, sSession)

    End Sub


    Protected Sub btnKortingscode_Click(sender As Object, e As EventArgs) Handles btnKortingscode.Click
        Dim FR As New clsFacRgl
        Dim sSessieID As String = Request.Cookies("SessieID").Value
        Dim KT As New clsKortingCodes
        Dim sKortingsCode As String = txtKortingscode.Text

        Dim BTW As New clsBtwTarieven
        Dim iBtwTarief As Decimal = BTW.scBtwTarief("H")
        Dim iKortingsbedrag As Decimal = KT.KortingsBedragViaSession(iPartijIDBeheerder, sKortingsCode, sSessieID)
        If KT.bOk = True Then
            Dim iBtwTotaal As Decimal = iKortingsbedrag / (iBtwTarief + 100) * iBtwTarief
            FR.iscFacRgl(0, "", 0, KT.sOmschrijving, -iKortingsbedrag, Math.Round(-iBtwTotaal, 2), -iKortingsbedrag, iBtwTarief, sKortingsCode, "", KT.sSoort, 0, 0, sSessieID, iPartijIDBeheerder, 0, "korting")
            InitWw(sSessieID)
            txtKortingscode.Text = ""
            'V.Message(Me.Master, "", KT.sMelding, "success")
        Else
            'V.Message(Me.Master, "", KT.sMelding, "warning")
        End If
        ltlDiscountMsg.Text = KT.sMelding
        divDiscountMsg.Style.Add("display", "block")
        InitWw(sSessieID)
    End Sub



    'Protected Sub repBetaalmethode_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repBetaalmethode.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim ltl As Literal = e.Item.FindControl("ltlPaymentMeanBrand")
    '        Dim cb As RadioButton = e.Item.FindControl("cb")
    '        Dim lblFor As HtmlGenericControl = e.Item.FindControl("lblFor")
    '        lblFor.Attributes.Add("for", cb.ClientID)

    '        If ltl.Text = "REKENING" Then
    '            'If User.Identity.IsAuthenticated Then
    '            '    PT.drPartij = PT.daPartij.sPartijByPersID(Profile.iPersID).Rows(0)
    '            'Else
    '            '    PT.drPartij = PT.daPartij.sPartijByPersID(hidPersID.Value).Rows(0)
    '            'End If

    '            'If PT.drPartij.sBarcode <> "True" Then
    '            '    Dim li As HtmlGenericControl = e.Item.FindControl("liBetaalmethode")
    '            '    li.Visible = False
    '            '    Exit Sub
    '            'End If
    '        ElseIf ltl.Text = "IDEAL" Then
    '            cb.Checked = True
    '        End If
    '        'Later direct vanuit page_load
    '    End If
    'End Sub

    Private Sub VeldenVullen(ByVal iPartijID As Long)
        Dim iPersID As Long
        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then

            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
        End If


        If ST.bCompany Then
            Dim PAR As New clsPartijen
            PAR.dt = PAR.sPartijByPartijID(iPartijID)
            If PAR.dt.Rows.Count > 0 Then
                PAR.dr = PAR.dt.Rows(0)
                txtVat.Text = PAR.dr.Item("sBtwNummer")
                txtCompany.Text = PAR.dr.Item("sNaam")
            End If
        Else
            divCompany.Visible = False
        End If

        PERS.dt = PERS.sPersoonByPersID(iPersID)
        If PERS.dt.Rows.Count > 0 Then
            PERS.dr = PERS.dt.Rows(0)
            txtFirstName.Text = PERS.dr.Item("sVoorletters")
            txtSurname.Text = PERS.dr.Item("sNaam")
            txtEmail.Text = PERS.dr.Item("sEmail")
            txtPhone.Text = PERS.dr.Item("sTelefoon")
        End If


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


        ' PT.drPartij = PT.daPartij.sPartij(iPartijID).Rows(0)
        'txtBedrijfsnaam.Text = PT.drPartij.sNaam
    End Sub

    Private Function BepaalVerzendkosten() As String
        If ST.bVerzendmethode Then
            Dim nfi As NumberFormatInfo = New CultureInfo("nl-NL", False).NumberFormat
            nfi.NumberDecimalDigits = 2

            Dim FACRGL As New clsFacRgl
            Dim rVerzendkosten As Decimal

            Dim iLandID As Long
            If cbAnderAdres.Checked Then
                iLandID = ddlLand2.SelectedValue
            Else
                iLandID = ddlLand.SelectedValue
            End If
            Dim iArtikelVerzendID As Long = rblVerzendmethode.SelectedValue

            Dim sSessieID As String = Request.Cookies("SessieID").Value
            rVerzendkosten = FACRGL.RefreshVerzendkosten(0, sSessieID, iPartijIDBeheerder, iLandID, iArtikelVerzendID, rblBetaalmethode.SelectedValue)

            ltlVerzendkosten.Text = rVerzendkosten.ToString("N", nfi)
            Return rVerzendkosten.ToString()
        End If
        Return ""
    End Function

    <WebMethod>
    Public Shared Function BepaalVerzendkosten(iLandID As Long, iArtikelVerzendID As Long, sBetaalmethode As String) As Object
        Dim R As New oResponse
        R.type = "verzendkosten"
        Dim nfi As NumberFormatInfo = New CultureInfo("nl-NL", False).NumberFormat
        nfi.NumberDecimalDigits = 2
        Dim rVerzendkosten As Decimal
        Dim sSessieID As String = HttpContext.Current.Request.Cookies("SessieID").Value

        Dim BP As New BasePage
        Dim FACRGL As New clsFacRgl
        rVerzendkosten = FACRGL.RefreshVerzendkosten(0, sSessieID, BP.iPartijIDBeheerder, iLandID, iArtikelVerzendID, sBetaalmethode)

        FACRGL.sInfo(sSessieID, BP.iPartijIDBeheerder)
        R.totaalExcl = FACRGL.sBedrag
        R.totaalIncl = FACRGL.sBedragMinusKorting
        R.korting = FACRGL.sDiscount
        R.totaalInclZonderVerzendkosten = FACRGL.sBedragMinusKortingZonderVerzendkosten

        R.code = "1"
        R.msg = rVerzendkosten.ToString("N", nfi)
        Return R
    End Function

    Protected Sub btnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click

        hidValidatePostback.Value = "true"
        Dim sSessieID As String = Request.Cookies("SessieID").Value

        ST.InitWebshopSettings(iPartijIDBeheerder)

        Dim AV As New clsArtikelVarianten
        'hidVoorraad.Value is om de postback ('terug' bij betaalpagina)  te controleren
        If ST.bVoorraad Then
            If AV.CheckVoorraad(sSessieID, iPartijIDBeheerder) = False Then
                InitWw(sSessieID)
                ltlInfo.Text = "Tijdens het bestellen zijn de volgende artikelen niet meer op voorraad: <br />" & AV.sMelding
                Exit Sub
            End If
            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, True)

        End If

        Dim V As New clsValidatie

        Dim bOk As Boolean = True
        Dim voornaam As String = txtFirstName.Text
        Dim achternaam As String = txtSurname.Text
        Dim bedrijfsnaam As String = txtCompany.Text
        Dim email As String = txtEmail.Text
        Dim username As String = txtEmail.Text

        If Trim(voornaam) = "" Then bOk = False
        If Trim(achternaam) = "" Then bOk = False
        If Trim(email) = "" Then bOk = False
        If Trim(username) = "" Then bOk = False
        If Trim(username) = "" Then bOk = False


        'If ControleerPostcode() = False Then
        '    ltlInfo.Text = "Postcode is geen geldig Nederlandse postcode."
        '    Exit Sub
        'End If

        If bOk = False Then
            ltlInfo.Text = Resources.Resource.ValidateVeldenVerplicht
            'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.VeldenVerplicht)
            If ST.bVoorraad Then
                AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
            End If
            Exit Sub
        End If

        If V.sVerplichtEmail(email) = False Then bOk = False
        If bOk = False Then
            ltlInfo.Text = Resources.Resource.EmailadresOnjuist
            'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.EmailadresOnjuist)
            If ST.bVoorraad Then
                AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
            End If
            Exit Sub
        End If

        If terms.Checked = False Then
            ltlInfo.Text = "U moet akkoord gaan met de algemene voorwaarden"
            If ST.bVoorraad Then
                AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
            End If
            Exit Sub
        End If

        Dim sBetaalmethode As String = rblBetaalmethode.SelectedValue
        If sBetaalmethode = "" Then
            ltlInfo.Text = Resources.Resource.SelecteerBetaalmethode
            If ST.bVoorraad Then
                AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
            End If
            Exit Sub
        End If


        Dim FR As New clsFacRgl
        Dim iTotaalExcl, iTotaalIncl, iBtwBedrag As Decimal

        FACRGL.sInfo(sSessieID, iPartijIDBeheerder)
        Dim iVerzendKosten As Decimal
        If FACRGL.iAantalRegels > 0 Then
            iVerzendKosten = FACRGL.sVerzendkosten
            iTotaalIncl = FACRGL.sBedragMinusKorting
            iBtwBedrag = Math.Round(FACRGL.iBtw, 2)
            iTotaalExcl = Math.Round(iTotaalIncl - iBtwBedrag, 2)
        Else
            ltlInfo.Text = "No Items in your shopping cart."
            ' WEET NIET ZEKER ?! VOORRAAD ?!
            If ST.bVoorraad Then
                AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
            End If
            Exit Sub
        End If


        Dim dLat As Double = 0
        If IsNumeric(hdfLatitudeX.Value) Then
            dLat = hdfLatitudeX.Value
        End If


        Dim dLon As Double = 0
        If IsNumeric(hdfLongitudeY.Value) Then
            dLon = hdfLongitudeY.Value
        End If


        Dim dLat2 As Double = 0
        If IsNumeric(hdfLatitudeX2.Value) Then
            dLat2 = hdfLatitudeX2.Value
        End If


        Dim dLon2 As Double = 0
        If IsNumeric(hdfLongitudeY2.Value) Then
            dLon2 = hdfLongitudeY2.Value
        End If

        Dim iPersID, iPartijID As Long
        If IsOnline() Then
            'update data
            iPartijID = sPartijID()
            ACC.dt = ACC.sAccountByPartijID(iPartijID)
            If ACC.dt.Rows.Count > 0 Then
                ACC.dr = ACC.dt.Rows(0)
                iPersID = ACC.dr.Item("iPersID")
            End If

            PERS.uPersoon("", txtFirstName.Text & " " & txtSurname.Text, txtFirstName.Text, "", txtSurname.Text, "", "", "", "", txtEmail.Text, txtPhone.Text, txtPhone.Text, "", True, Date.Now, 0, 0, Date.Now, True, "actief", "", 0, iPartijIDBeheerder, iPersID)

            ADR.uAdresByPersIDAndType(iPersID, iPartijID, "factuur", "", "", txtZipCode.Text, txtHouseNr.Text, txtAdd.Text, txtAddress.Text, "", txtResidence.Text, "", hdfProvincie.Value, ddlLand.SelectedItem.Text, ddlLand.SelectedValue, dLat, "", dLon, "", "", "", txtAddressline2.Text)
            If cbAnderAdres.Checked Then

                Dim iVerzendPersID As Long = PERS.iscPersoon("", txtFirstName.Text & " " & txtSurname.Text, txtFirstName.Text, "", txtSurname.Text, "", "", "", "", "", "", "", "", False, Date.Now, "", 0, 0, Date.Now, True, "actief", "", 0, iPartijIDBeheerder)

                ADR.dt = ADR.sAdresByPartijIDAndType(iPartijID, "lever")
                Dim bInsert As Boolean = True
                For Each ADR.dr In ADR.dt.Rows
                    If Trim(ADR.dr.Item("sPostCode")) = Trim(txtZipCode.Text) And txtHouseNr.Text = ADR.dr.Item("sHuisNr") Then
                        bInsert = False
                    End If
                Next
                If bInsert Then
                    ADR.uAdresByPersIDAndType(iPersID, iPartijID, "lever", "", "", txtZipCode2.Text, txtHouseNr2.Text, txtAdd2.Text, txtAddress2.Text, "", txtResidence2.Text, "", hdfProvincie2.Value, ddlLand2.SelectedItem.Text, ddlLand2.SelectedValue, dLat2, "", dLon2, "", "", "", txtAddressline22.Text)
                End If
            End If
        Else

            Dim bAccount As Boolean = False
            Dim newUser As MembershipUser = Nothing

            If txtNewPasswordOrder.Text = "" Then
                'Geen account

                iPersID = PERS.iscPersoon("", txtFirstName.Text & " " & txtSurname.Text, txtFirstName.Text, "", txtSurname.Text, "", "", "", "", txtEmail.Text, txtPhone.Text, txtPhone.Text, "", False, Date.Now, "", 0, 0, Date.Now, True, "actief", "", Decimal.Parse("0,00"), iPartijIDBeheerder)


            Else
                'account aanmaken of guest
                Dim bPassword As Boolean = String.Compare(txtNewPasswordOrder.Text, txtNewPasswordConfirmOrder.Text, False)
                If bPassword = True Then
                    ltlInfo.Text = Resources.Resource.WachtwoordKomtNietOvereen
                    If ST.bVoorraad Then
                        AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)

                    End If
                    Exit Sub
                End If

                Dim createStatus As MembershipCreateStatus
                newUser = Membership.CreateUser(email, txtNewPasswordOrder.Text, email, "What is it good for?", "Absolutely nothing", True, createStatus)
                Select Case createStatus
                    Case MembershipCreateStatus.Success
                        iPersID = PERS.iscPersoon("", txtFirstName.Text & " " & txtSurname.Text, txtFirstName.Text, "", txtSurname.Text, "", "", "", "", txtEmail.Text, txtPhone.Text, txtPhone.Text, "", False, Date.Now, newUser.ProviderUserKey.ToString(), 0, 0, Date.Now, True, "actief", "", Decimal.Parse("0,00"), iPartijIDBeheerder)
                        bAccount = True
                        M.RegistratieMailVersturen(iPartijIDBeheerder, iTaalID, txtEmail.Text, txtFirstName.Text, txtSurname.Text)
                    Case MembershipCreateStatus.DuplicateUserName
                        ltlInfo.Text = Resources.Resource.GebruikersnaamBestaat & email
                        If ST.bVoorraad Then
                            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
                        End If
                        Exit Sub
                    Case MembershipCreateStatus.DuplicateEmail
                        ltlInfo.Text = Resources.Resource.EmailadresBestaat & email
                        If ST.bVoorraad Then
                            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
                        End If
                        Exit Sub
                    Case MembershipCreateStatus.InvalidEmail
                        ltlInfo.Text = Resources.Resource.EmailadresNietJuist & email
                        If ST.bVoorraad Then
                            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
                        End If
                        Exit Sub
                    Case MembershipCreateStatus.InvalidAnswer
                        ltlInfo.Text = Resources.Resource.BeveiligingsantwoordNietJuist
                        If ST.bVoorraad Then
                            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
                        End If
                        Exit Sub
                    Case MembershipCreateStatus.InvalidPassword
                        ltlInfo.Text = Resources.Resource.WachtwoordEisen
                        If ST.bVoorraad Then
                            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
                        End If
                        Exit Sub
                    Case Else
                        ltlInfo.Text = Resources.Resource.OnbekendeFout2
                        If ST.bVoorraad Then
                            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, False)
                        End If
                        Exit Sub
                End Select



            End If

            BepaalVerzendkosten()

            iPartijID = PT.iscPartij(iPartijIDBeheerder, bedrijfsnaam, "", "", "", "", txtVat.Text, "", "", Date.Now, "", "", "", "", "", "bedrijf", "actief", "", "", "", "", "", "", "", "", "", "", "", "")

            TPP.iTussPersonenPartijen(iPersID, iPartijID)

            ADR.iscAdres(iPersID, iPartijID, "factuur", "", "", txtZipCode.Text, txtHouseNr.Text, txtAdd.Text, txtAddress.Text, "", txtResidence.Text, "", hdfProvincie.Value, ddlLand.SelectedItem.Text, ddlLand.SelectedValue, dLat, "", dLon, "", "", "", "", iPartijIDBeheerder)


            If bAccount Then
                FormsAuthentication.SetAuthCookie(email, True)
                ACC.iscAccount(iPartijID, iPersID, "bedrijf", "actief", Now, Now.Date, newUser.ProviderUserKey.ToString(), iPartijIDBeheerder)
            End If

            If cbAnderAdres.Checked Then

                Dim iVerzendPersID As Long = PERS.iscPersoon("", txtFirstName2.Text & " " & txtSurname2.Text, txtFirstName2.Text, "", txtSurname2.Text, "", "", "", "", "", "", "", "", False, Date.Now, "", 0, 0, Date.Now, True, "actief", "", 0, iPartijIDBeheerder)
                ADR.iscAdres(iVerzendPersID, iPartijID, "lever", "", "", txtZipCode2.Text.ToString().ToUpper().Replace(" ", ""), txtHouseNr2.Text, txtAdd2.Text, txtAddress2.Text, "", txtResidence2.Text, "", hdfProvincie2.Value, ddlLand2.SelectedItem.Text, ddlLand2.SelectedValue, dLat2, "", dLon2, "", "", txtAddressline2.Text, "", iPartijIDBeheerder)
            Else
                ADR.iscAdres(iPersID, iPartijID, "lever", "", "", txtZipCode.Text, txtHouseNr.Text, txtAdd.Text, txtAddress.Text, "", txtResidence.Text, "", hdfProvincie.Value, ddlLand.SelectedItem.Text, ddlLand.SelectedValue, dLat, "", dLon, "", "", "", "", iPartijIDBeheerder)

            End If
        End If

        'Follow Cookie
        Dim cCookie As HttpCookie = Request.Cookies("Follow")
        If cCookie IsNot Nothing Then
            Dim sSessie As Sessie = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Sessies)(cCookie.Value).seSessies(0)
            sSessie.saveByIPersID(iPersID, iPartijIDBeheerder, txtEmail.Text, "Bestellen stappen")
        End If
        'Follow Cookie

        Dim sPartijGegevens As String = ""
        Dim sVerzendGegevens As String = ""

        sPartijGegevens &= txtFirstName.Text & " " & txtSurname.Text & Environment.NewLine
        sPartijGegevens &= txtAddress.Text & " " & txtHouseNr.Text & " " & txtAdd.Text & Environment.NewLine
        sPartijGegevens &= txtZipCode.Text & " " & txtResidence.Text & Environment.NewLine
        If Trim(txtAddressline2.Text) <> "" Then
            sPartijGegevens &= txtAddressline2.Text & Environment.NewLine
        End If
        sPartijGegevens &= hdfProvincie.Value & Environment.NewLine & ddlLand.SelectedItem.Text & Environment.NewLine


        If cbAnderAdres.Checked Then
            sVerzendGegevens = txtFirstName2.Text & " " & txtSurname2.Text & Environment.NewLine
            sVerzendGegevens &= txtAddress2.Text & " " & txtHouseNr2.Text & " " & txtAdd2.Text & Environment.NewLine
            sVerzendGegevens &= txtZipCode2.Text & " " & txtResidence2.Text & Environment.NewLine
            If Trim(txtAddressline22.Text) <> "" Then
                sVerzendGegevens &= txtAddressline22.Text & Environment.NewLine
            End If
            sVerzendGegevens &= hdfProvincie2.Value & Environment.NewLine & ddlLand2.SelectedItem.Text
        Else
            sVerzendGegevens = sPartijGegevens
        End If

        sPartijGegevens &= txtEmail.Text


        'Factuur aanmaken
        Dim iTotaalInclAfgerond As Decimal = Math.Round(iTotaalIncl, 2)
        Dim sTimestamp As String = Date.Now.Ticks.ToString()
        Dim FACKOP As New clsFacKop


        iBtwBedrag = Math.Round(iTotaalInclAfgerond / 121 * 21, 2)
        iTotaalExcl = Math.Round(iTotaalInclAfgerond - iBtwBedrag, 2)

        If iTotaalInclAfgerond < 0 Then
            iTotaalInclAfgerond = 0.00
        End If

        If iTotaalExcl < 0 Then
            iTotaalExcl = 0.00
        End If

        If iBtwBedrag < 0 Then
            iBtwBedrag = 0.00
        End If

        'sData wordt in de webhook gevuld met mollie ID
        Dim iFacKopID As Long = FACKOP.iFacKop(sPartijGegevens, sVerzendGegevens, iTotaalInclAfgerond, Math.Round(iTotaalExcl, 2), Math.Round(iBtwBedrag, 2), txtOpmerking.Text, "", sTimestamp, iPersID, 0, 0, 0, iPartijID, "order", iPartijIDBeheerder, "open")

        If ST.bMyParcel Then
            Dim Z As New clsZendingen
            If cbAnderAdres.Checked Then
                Z.iZending(iFacKopID, "", L.scLandCodeByID(ddlLand2.SelectedValue), txtResidence2.Text, txtAddress2.Text, txtHouseNr2.Text, txtAdd2.Text, txtZipCode2.Text, txtFirstName2.Text & " " & txtSurname2.Text, txtPhone.Text, txtEmail.Text)
            Else
                Z.iZending(iFacKopID, "", L.scLandCodeByID(ddlLand.SelectedValue), txtResidence.Text, txtAddress.Text, txtHouseNr.Text, txtAdd.Text, txtZipCode.Text, txtFirstName.Text & " " & txtSurname.Text, txtPhone.Text, txtEmail.Text)
            End If
        End If

        Dim Url As String = sURL()

        Dim FACITEMS As New clsFacItems
        ' FACITEMS.iFacItems(iFacKopID, "verzendkosten", Math.Round(iVerzendKosten, 2), "")
        FACITEMS.iFacItems(iFacKopID, "timestamp", sTimestamp, "")
        FACITEMS.iFacItems(iFacKopID, "session", sSessieID, "")

        Dim P As New clsPage
        Dim sResponsePage As String = P.sPageUrlByGuid(sLanguage, "58a8219f-0c5a-4b44-a89f-02231ecf769b") ' "/order-information"
        sResponsePage = sResponsePage.Substring(1, sResponsePage.Length - 1)
        sResponsePage = sResponsePage & "/" & iFacKopID & "/" & sTimestamp & "/" & iPartijID & "/" & iPersID

        Dim ROUTE As New clsRoutes
        ROUTE.iRoute(iPartijIDBeheerder, iTaalID, iFacKopID, "idealbevestiging", "", "~" & sResponsePage)

        Dim routes = RouteTable.Routes
        Using routes.GetWriteLock()
            routes.Clear()
            RouteConfig.RegisterRoutes(routes)
        End Using

        If iTotaalInclAfgerond < 1 Then
            Try
                If FACKOP.dr.Item("sStatus").ToLower() = "betaald" Then
                    FACITEMS.iFacItems(iFacKopID, "Al betaald", "", "")
                    Exit Sub
                End If
            Catch ex As Exception

            End Try
            Dim sType As String = "factuur"
            Dim sFactuurnummer As String = ST.scSettingByTypeAndGroup("webshop", sType & "teller", iPartijIDBeheerder)
            ST.uSettingByTypeAndGroup(CInt(sFactuurnummer) + 1, sType & "teller", "webshop", iPartijIDBeheerder)
            Dim iBetaalTermijn As Integer = ST.scSettingByTypeAndGroup("webshop", sType & "termijn", iPartijIDBeheerder)


            FACKOP.uStatusType("factuur", "betaald", 0, iFacKopID)
            FACKOP.uFactuurByID(iFacKopID, sFactuurnummer, "~/facturen/" & iFacKopID.ToString() & "_" & sFactuurnummer & ".pdf")

            FACRGL.uFacRglBySession(iFacKopID, sFactuurnummer, 0, sSessieID) 'sessie verwijderen

            If ST.bFactuurPDF Then
                Dim PDF As New clsPDF
                Dim U As New clsUtility
                PDF.sFooterUrl = U.Config("URL") & "/Rapporten/factuur_footer.aspx?id=" & iFacKopID
                PDF.sPDF(Page, "/Rapporten/Factuur.aspx?id=" & iFacKopID, iFacKopID & "_" & sFactuurnummer, "", False, True, "opslaan")
            End If

            Dim M As New clsMail
            M.MailOrder(iFacKopID, sLanguage, ST.bFactuurPDF)

            Response.Redirect("/" & sResponsePage)
        Else
            Dim sPaymentURL As String = MOLLIE.CreatePayment(iTotaalInclAfgerond.ToString().Replace(",", "."), sLanguage, sResponsePage, FACKOP.iOrdernummer, iFacKopID, 0, 0, rblBetaalmethode.SelectedValue, ddlBank.SelectedValue, sSessieID)
            FACITEMS.iFacItems(iFacKopID, "sPaymentID", MOLLIE.sPaymentID, "")
            Response.Redirect(sPaymentURL)
        End If
    End Sub

    Private Function ControleerPostcode() As Boolean

        Dim iVerzendLandID As Long = 0
        Dim sPostcode, sWijkCode, sNummer As String
        Dim sStraatCode As String = ""
        If cbAnderAdres.Checked Then
            iVerzendLandID = ddlLand2.SelectedValue
            sPostcode = txtZipCode2.Text
            sNummer = txtHouseNr2.Text
        Else
            iVerzendLandID = ddlLand.SelectedValue
            sPostcode = txtZipCode.Text
            sNummer = txtHouseNr.Text
        End If

        sWijkCode = sPostcode.Substring(0, 4)
        If sPostcode.Length = 6 Then
            sStraatCode = sPostcode.Substring(4, 2)
        End If
        If sPostcode.Length = 7 Then
            sStraatCode = sPostcode.Substring(5, 2)
        End If
        Dim sLandCode As String = L.scLandCodeByID(iVerzendLandID)
        If sLandCode.ToLower = "nl" Then
            Dim PC As New clsPostcodeNL
            PC.AdresVanPostcode(sWijkCode & sStraatCode, sNummer.Trim)
            If PC.bOK = True Then
                Return True
            Else
                Dim ADR As New clsAdressenCompleet
                Dim sAdresCompleet As String = ADR.sAdres(sWijkCode, sStraatCode, sNummer)
                If Trim(sAdresCompleet) = "" Then
                    Return False
                Else
                    Return True
                End If
            End If
        Else
            Return True
        End If
    End Function
End Class
