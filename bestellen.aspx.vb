
Partial Class _Default
    Inherits BasePage
    Public sLanguage As String
    Dim IMG As New clsImages
    Dim MOLLIE As New clsMollie
    Dim ART As New clsArtikelen
    Dim iTaalID As Long
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            Dim sQs As String = RouteData.Values("page").ToString()

            Dim PI As New clsPageItems
            PI.sPI(Me.Page, sLanguage, sQs, False, False, True)


            ART.dt = ART.sArtikelenByArtikelgroep(iPartijIDBeheerder, "Cadeaubonnen")
            repArtikelen.DataSource = ART.dt
            repArtikelen.DataBind()

            'Dim awe As HtmlAnchor = Me.Master.FindControl("awelkom")
            'awe.HRef = "/#intro"
            'Dim asp As HtmlAnchor = Me.Master.FindControl("aspeel")
            'asp.HRef = "/#speel"

            repBetaalmethodes.DataSource = MOLLIE.ListMethods()
            repBetaalmethodes.DataBind()
            'repBetaalmethodes.Items(0).Selected = True


            MOLLIE.ListIssuers(ddlBank)
            ddlBank.Items(0).Selected = True
        End If
    End Sub

    Protected Sub btnAfronden_Click(sender As Object, e As EventArgs) Handles btnAfronden.Click

        Dim V As New clsValidatie
        Dim bOk As Boolean = True

        'If V.(txtVoornaam.Value) = False Then bOk = False
        'If V.sVerplicht(txtAchternaam.Value) = False Then bOk = False
        'If V.sVerplicht(txtPostcode.Value) = False Then bOk = False
        'If V.sVerplicht(txtNr.Value) = False Then bOk = False
        'If V.sVerplicht(txtStraatnaam.Value) = False Then bOk = False
        'If V.sVerplicht(txtPlaatsnaam.Value) = False Then bOk = False
        'If V.sVerplicht(txtEmail.Value) = False Then bOk = False

        'If bOk = False Then
        '    V.MessagePage(Me, "Let op: ", "Wij missen de verplichte velden.")
        '    Exit Sub
        'End If



        Dim iArtikelID As Long = hidArtikelID.Value.Replace("rb", "")
        Dim sBetaalmethode As String = hidBetaalmethode.Value.Replace("rb", "")
        ART.dt = ART.sArtikelByIDdt(iArtikelID)
        If ART.dt.Rows.Count > 0 Then
            ART.dr = ART.dt.Rows(0)
        End If

        Dim sPrijs As String = ART.dr.Item("iPrijs").ToString()
        Dim M As New clsMail

        Dim P As New clsPage
        Dim sResponsePage As String = P.sPageUrlByGuid(sLanguage, "6bc20e6c-b0dc-4fd5-b542-9f72de9deb44")
        'sResponsePage = sResponsePage.Substring(1, sResponsePage.Length - 1)
        Dim U As New clsUtility
        Dim LI As New clsLijstItems
        LI.dt = LI.sLijstItemByTitleAndType("Cadeaubon", iTaalID, "Mailing")
        Dim sName As String = U.Name()
        Dim sMsg As String = ""
        Dim sSubject As String = ""
        Dim sNaam As String = txtVoornaam.Value & " " & txtAchternaam.Value
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            sMsg = LI.dr.Item("sDescription")
            sSubject = LI.dr.Item("sSubTitle") & " " & sNaam
            Dim sVerzendGegevens As String = ""
            sVerzendGegevens &= txtStraatnaam.Value & " " & txtNr.Value & " " & txtToev.Value & Environment.NewLine
            sVerzendGegevens &= txtPostcode.Value & " " & txtPlaatsnaam.Value & Environment.NewLine
            sMsg = sMsg.Replace("[naam]", sNaam)
            sMsg = sMsg.Replace("[adres]", sVerzendGegevens)
            sMsg = sMsg.Replace("[bedrag]", sPrijs & "(Tekst: " & ART.dr.Item("sArtikel") & ")")
            sMsg = sMsg.Replace("[email]", txtEmail.Value)

            If sBetaalmethode.ToLower() = "ideal" Then
                sMsg = sMsg.Replace("[bank]", ddlBank.SelectedItem.Text)
            Else
                sMsg = sMsg.Replace("[bank]", "")
            End If
            sMsg = sMsg.Replace("[betaalmethode]", sBetaalmethode)
        End If
        Dim iMailID As Long = M.iscMail(0, iPartijID, "versturen", LI.dr.Item("sTitle"), "", LI.dr.Item("sItem1"), LI.dr.Item("sColor"), "", "", sSubject, sMsg, "", "", sName, Now)

        iTaalID = sTaalID(sLanguage)
        'Mail naar bezoeker
        LI.dt = LI.sLijstItemByTitleAndType("Cadeaubon_naar_bezoeker", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            sMsg = LI.dr.Item("sDescription")
            sMsg = sMsg.Replace("[naam]", sNaam)
            sSubject = LI.dr.Item("sSubTitle")
            Dim sMailTemplate As String = M.sHtml("~/EmailTemplates/Mail.aspx")
            sMailTemplate = sMailTemplate.Replace("[subject]", sSubject)
            sMailTemplate = sMailTemplate.Replace("[body]", sMsg)
            iMailID = M.iscMail(0, iPartijID, "pending", LI.dr.Item("sTitle"), "", LI.dr.Item("sItem1"), txtEmail.Value, "", "", LI.dr.Item("sSubTitle"), sMailTemplate, "", "", LI.dr.Item("sItem5"), Now)
        End If
        Dim sPaymentURL As String = MOLLIE.CreatePayment(sPrijs.Replace(",", "."), sLanguage, sResponsePage, "Cadeaubon", 0, iMailID, 0, sBetaalmethode, ddlBank.SelectedValue, "", 0, "MollieResponsePaymentBestellen.aspx")

        Response.Redirect(sPaymentURL)
    End Sub

    Dim iCount As Long = 0
    Protected Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rb As HtmlInputRadioButton = e.Item.FindControl("rb")
            rb.ID = "rb" & DataBinder.Eval(e.Item.DataItem, "iArtikelID")
            If iCount = 0 Then
                hidArtikelID.Value = DataBinder.Eval(e.Item.DataItem, "iArtikelID") 'standaard waarde 
                rb.Checked = True
            End If
            iCount = iCount + 1
        End If
    End Sub

    Dim iCount2 As Long = 0
    Protected Sub repBetaalmethodes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repBetaalmethodes.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rb As HtmlInputRadioButton = e.Item.FindControl("rb")
            rb.ID = "rb" & DataBinder.Eval(e.Item.DataItem, "sID")
            If iCount2 = 0 Then
                hidBetaalmethode.Value = DataBinder.Eval(e.Item.DataItem, "sID")
                rb.Checked = True
            End If
            iCount2 = iCount2 + 1
        End If
    End Sub
    Protected Sub btnBestellen_Click(sender As Object, e As EventArgs)

    End Sub
End Class