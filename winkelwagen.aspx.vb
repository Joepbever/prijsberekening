Imports System.Data
Imports System.Web.Services

Partial Class Winkelwagen
    Inherits BasePage

    Dim STF As New clsArtikelStaffel
    Dim FR As New clsFacRgl
    Dim UTIL As New clsUtility
    Dim V As New clsValidatie
    Dim IMG As New clsImages
    Dim P As New clsPage
    Dim U As New clsUtility
    Dim TA As New clsTussArtikelen

    Public sLanguage, sQs, sParentPage, sProductPage, sOffertePage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sLanguage = RouteData.Values("language").ToString()
        sQs = RouteData.Values("page").ToString()

        sProductPage = P.sPageUrlByGuid(sLanguage, "c64a4429-cceb-4f70-9451-ad2c8bcf21b8")

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
        InitWw(sSessieID)
        Dim ST As New clsSettings
        ST.InitWebshopSettings(iPartijIDBeheerder)
        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then

            If ST.bKortingsCode = False Then
                'divKorting.Visible = False
            End If

            If ST.bOfferteAanvragen Then
                initOffer()
            Else
                divOffer.Visible = False
            End If

            'FR.sInfo(sSessieID, iPartijIDBeheerder)
            'If FR.iAantalRegels < 1 Then
            '    'btnCheckout.Visible = False
            '    spInfo.Style.Add("display", "block")
            'Else
            '    'aStepTwo.HRef = P.sPageUrlByGuid(sLanguage, "f723fde6-19ae-4571-9a71-904f146eb430")
            '    'aStepThree.HRef = P.sPageUrlByGuid(sLanguage, "f723fde6-19ae-4571-9a71-904f146eb430")
            '    'aStepFour.HRef = P.sPageUrlByGuid(sLanguage, "f723fde6-19ae-4571-9a71-904f146eb430")
            'End If

            ''aWinkelwagen.HRef = P.sPageUrlByGuid(sLanguage, "d5394f19-746a-4c1e-b257-21a7767a7acb")

            'If RouteData.Values("parentpage") IsNot Nothing Then
            '    sParentPage = RouteData.Values("parentpage")
            'End If
            'aVerderWinkelen.HRef = P.sPageUrlByGuid(sLanguage, "fbe94eda-faa1-45c5-a89a-0371ff4f8d68")
            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

            If ST.bGerelateerdeArtikelenWinkelwagen Then
                FR.dt = FR.sFacRgls_Session_Related_Routes_Items(sSessieID, iPartijIDBeheerder, "artikel", iTaalID)
                If FR.dt.Rows.Count > 0 Then
                    For Each FR.dr In FR.dt.Rows
                        If wwItems.Contains(FR.dr.Item("iArtikelID")) Then
                            FR.dr.Delete()
                        End If
                    Next
                    'repArtikelen.DataSource = FR.dt
                    'repArtikelen.DataBind()
                End If
            Else
                'divRelated.Visible = False
            End If
        End If
    End Sub

    Dim wwItems As New List(Of String)()
    Private Sub InitWw(ByVal sSession As String)
        FR.dt = FR.sFacRgls_SessionAndImg(sSession, iPartijIDBeheerder, "artikel")
        If FR.dt.Rows.Count > 0 Then
            repWinkelwagen.DataSource = FR.dt
            repWinkelwagen.DataBind()
            For Each FR.dr In FR.dt.Rows
                wwItems.Add(FR.dr.Item("iArtikelID"))
            Next

        End If
        U.updateCart(Page, sSession)

        FR.sInfo(sSession, iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            divLeeg.Visible = False
            ltlBedragEx.Text = FR.sBedrag
            ltlTotaalIn.Text = FR.sBedragMinusKorting
            ltlDiscount.Text = FR.sDiscount

            If FR.bDiscount Then
                'divKorting.Visible = False
            End If
        Else
            divLeeg.Visible = True
            ltlBedragEx.Text = "0.00"
            ltlTotaalIn.Text = "0.00"
            ltlDiscount.Text = "0.00"
        End If
    End Sub

    <WebMethod>
    Public Shared Function RefreshWinkelwagen() As Object
        Dim R As New oResponse
        R.type = "refresh"

        Dim FR As New clsFacRgl
        Dim sSession As String = HttpContext.Current.Request.Cookies("SessieID").Value
        Dim BP As New BasePage

        FR.sInfo(sSession, BP.iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            R.aantalregels = FR.iAantalRegels
            R.totaalExcl = FR.sBedrag
            R.totaalIncl = FR.sBedragMinusKorting
            R.aantal = FR.sAantalArtikelen
            R.korting = FR.sDiscount
        Else
            R.aantalregels = FR.iAantalRegels
            R.totaalExcl = "0,00"
            R.totaalIncl = "0,00"
            R.aantal = "0"
            R.korting = FR.sDiscount
        End If

        R.code = 1
        R.msg = "Product removed from shopping cart."

        Return R
    End Function
    <WebMethod>
    Public Shared Function dFacRgl(iFacRglID As Long) As Object
        Dim R As New oResponse
        R.type = "dfacrgl"

        Dim FR As New clsFacRgl
        FR.dFacRglByFacRglID(iFacRglID)

        Dim sSession As String = HttpContext.Current.Request.Cookies("SessieID").Value
        Dim BP As New BasePage

        Dim dt As New DataTable
        Dim iBtwTarief, iBtwTotaal As Decimal
        dt = FR.sFacRglsByTypeAndSessionAndData("korting", sSession, BP.iPartijIDBeheerder, "percentage")
        If dt.Rows.Count > 0 Then
            Dim KT As New clsKortingCodes
            For Each dr As DataRow In dt.Rows
                FR.dFacRglByFacRglID(dr.Item("iFacRglID"))
                Dim iKortingsbedrag As Decimal = KT.KortingsBedragViaSession(BP.iPartijIDBeheerder, dr.Item("sArtikel"), sSession)
                If KT.bOk = True Then 'controle of de korting nog geldig is.
                    iBtwTotaal = iKortingsbedrag / (iBtwTarief + 100) * iBtwTarief
                    FR.iscFacRgl(0, "", 0, KT.sOmschrijving, Math.Round(-iKortingsbedrag, 2), Math.Round(-iBtwTotaal, 2), Math.Round(-iKortingsbedrag, 2), iBtwTarief, KT.sKortingsCode, "", KT.sSoort, 0, 0, sSession, BP.iPartijIDBeheerder, 0, "korting")
                End If
            Next
        End If


        FR.sInfo(sSession, BP.iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            R.aantalregels = FR.iAantalRegels
            R.totaalExcl = FR.sBedrag
            R.totaalIncl = FR.sBedragMinusKorting
            R.aantal = FR.sAantalArtikelen
            R.korting = FR.sDiscount
        Else
            R.aantalregels = FR.iAantalRegels
            R.totaalExcl = "0.00"
            R.totaalIncl = "0.00"
            R.aantal = "0"
            R.korting = FR.sDiscount
        End If

        R.code = 1
        R.msg = "Product removed from shopping cart."

        Return R
    End Function

    <WebMethod>
    Public Shared Function uFacRgl(iFacRglID As Long, iAantal As Long) As Object
        Dim R As New oResponse
        R.type = "ufacrgl"
        Dim sSession As String = HttpContext.Current.Request.Cookies("SessieID").Value
        Dim BP As New BasePage

        Dim FR As New clsFacRgl
        FR.uFacRglAantal(iAantal, iFacRglID, sSession)

        FR.sInfo(sSession, BP.iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            R.totaalExcl = "€ " & FR.sBedrag
            R.totaalIncl = "€ " & FR.sBedragMinusKorting
            R.aantal = FR.sAantalArtikelen
            R.korting = "€ " & FR.sDiscount
            R.bedragregel = "€ " & FR.iBedragRegel
        Else
            R.totaalExcl = "€ 0.00"
            R.totaalIncl = "€ 0.00"
            R.aantal = "0"
            R.korting = FR.sDiscount
        End If

        R.code = 1
        R.msg = "Cart updated"

        Return R
    End Function

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
    End Sub

    Protected Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        Dim sSessieID As String = Request.Cookies("SessieID").Value
        sLanguage = RouteData.Values("language").ToString()
        FR.sInfo(sSessieID, iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            Response.Redirect(P.sPageUrlByGuid(sLanguage, "deee4d8c-ff21-4bb9-b11d-4d1c0b43c57c"))
        End If
    End Sub

    Dim ROUTE As New clsRoutes

    'Private Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")

    '        'Afbeelding tonen
    '        IMG.dt = IMG.sImageByIDAndSoort(iArtikelID, "thumb", "Artikel")
    '        If IMG.dt.Rows.Count > 0 Then
    '            IMG.dr = IMG.dt.Rows(0)
    '            Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
    '            imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
    '            imgThumb.Alt = IMG.dr.Item("sData")
    '        End If
    '    End If
    'End Sub

    Private Sub initOffer()
        Dim sSessieID As String = Request.Cookies("SessieID").Value
        FR.dt = FR.sFacRgls_Session(sSessieID, iPartijIDBeheerder, "artikel")
        If FR.dt.Rows.Count > 0 Then
            For Each row As DataRow In FR.dt.Rows
                Try
                    If row.Item("iaantal") > row.Item("iVrdBeschikbaar") Or row.Item("iVrdBeschikbaar") = 0 Then
                        divOffer.Visible = True
                    End If
                Catch ex As Exception
                    If row.Item("iaantal") > row.Item("iVrdBeschikbaarArtikel") Or row.Item("iVrdBeschikbaarArtikel") = 0 Then
                        divOffer.Visible = True
                    End If
                End Try
            Next
        End If
    End Sub

    Protected Sub btnOfferte_Click(sender As Object, e As EventArgs) Handles btnOfferte.Click
        Dim FR As New clsFacRgl
        Dim M As New clsMail
        Dim LI As New clsLijstItems
        Dim sSessieID As String = Request.Cookies("SessieID").Value
        Dim sName As String = U.Config("Name")
        Dim sMsg, sSubject, sMailTemplate, sMailVan, sMailNaar As String
        Dim sTemplate As String = M.sHtml("~/EmailTemplates/mail-offer.aspx?sessie=" & sSessieID & "&iTaalID=" & iTaalID)

        Dim sEmail As String = txtMailOfferte.Text
        LI.dt = LI.sLijstItemByTitleAndType("Offerte", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            sMsg = LI.dr.Item("sDescription")

            sMsg = sMsg.Replace("[email]", txtMailOfferte.Text)
            sMsg = sMsg.Replace("[name]", txtName.Text)
            sMsg = sMsg.Replace("[tel]", txtTel.Text)

            sMailVan = LI.dr.Item("sColor")
            sSubject = LI.dr.Item("sSubTitle")
            sMailNaar = txtMailOfferte.Text
            sMailTemplate = sTemplate.Replace("[subject]", sSubject)
            sMailTemplate = sMailTemplate.Replace("[body]", sMsg)

            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", sEmail)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", sEmail)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", sEmail)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", sEmail)

            M.iscMail(0, iPartijIDBeheerder, "versturen", LI.dr.Item("sTitle"), "", sVan, sNaar, sCC, sBCC, sSubject, sMailTemplate, "", "", sName, Now)
        End If

        Response.Redirect(P.sPageUrlByGuid(sLanguage, "a5ce3147-92a3-4802-852c-92a68f559b6b"))
    End Sub

End Class
