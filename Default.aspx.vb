
Partial Class _Default
    Inherits BasePage
    Dim LI As New clsLijstItems
    Dim IMG As New clsImages
    Dim ART As New clsArtikelen
    Dim C As New clsCategorie
    Dim CI As New clsCategorieItems
    Dim ROUTE As New clsRoutes
    Dim ST As New clsSettings
    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Dim bIsOnline As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)


            LI.dt = LI.sLIByTypeAndTaal("Slider", iTaalID, iPartijIDBeheerder, False)
            If LI.dt.Rows.Count > 0 Then
                repSlider.DataSource = LI.dt
                repSlider.DataBind()
            End If


            bIsOnline = IsOnline()
            ST.InitWebshopSettings(iPartijIDBeheerder)

            C.dt = C.sCategoriesByParentName(iTaalID, "Home")

            repCategories.DataSource = C.dt
            repCategories.DataBind()



            'ART.dt = ART.sArtikelenByCategorieNaam(iTaalID, "Home")
            'If ART.dt.Rows.Count > 0 Then
            '    ART.dr = ART.dt.Rows(0)
            '    repArtikelenFeatured.DataSource = ART.dt
            '    repArtikelenFeatured.DataBind()
            'End If


            Dim P As New clsPage
            Dim sAbout As String = P.sPageUrlByGuid(sLanguage, "b3918d98-7c75-43f5-a586-05dfd05e945e")
            'aUsp1.HRef = sAbout
            'aUsp2.HRef = sAbout
            'aUsp3.HRef = sAbout
            'aUsp4.HRef = sAbout

            ' Dim iParentID As Long = 0
            'CI.dt = CI.sCategorieItemsByParentIDTop(iPartijIDBeheerder, iTaalID, iParentID, 4)
            'If CI.dt.Rows.Count > 0 Then
            '    repCategories.DataSource = CI.dt
            '    repCategories.DataBind()
            'End If

        End If


        'Dim CAT As New clsCategorie
        'CAT.dt = CAT.sCategorieByParentIDTOP(iPartijIDBeheerder, iTaalID, 0, 9999999)
        'If CAT.dt.Rows.Count > 0 Then
        '    repSliderCategories.DataSource = CAT.dt
        '    repSliderCategories.DataBind()
        'End If


        'repReviews
        'LI.dt = LI.sLIByTypeAndTaal("Reviews", iTaalID, iPartijIDBeheerder, False)
        'If LI.dt.Rows.Count > 0 Then
        '    repReviews.DataSource = LI.dt
        '    repReviews.DataBind()
        'End If
    End Sub

    Private Sub repSlider_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repSlider.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iLijstItemID As Long = DataBinder.Eval(e.Item.DataItem, "iLijstItemID")
            IMG.dt = IMG.sImageByTussTypeAndID("Slider", iLijstItemID)

            If IMG.dt.Rows.Count > 0 Then
                For Each IMG.dr In IMG.dt.Rows
                    Select Case IMG.dr.Item("sSoort").ToString().ToLower()
                        Case "desktop"
                            Dim Desktop As HtmlImage = e.Item.FindControl("sliderImg")
                            Desktop.Attributes.Add("src", IMG.dr.Item("sSmall").Replace("~/", sURL()))
                            Desktop.Alt = IMG.dr.Item("sData")
                    End Select
                Next
            Else
                e.Item.FindControl("sliderItem").Visible = False
            End If

        End If
    End Sub

    Public Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubscribeHome.Click
        If Page.RouteData.Values("language") IsNot Nothing Then
            sLanguage = Page.RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)
        Dim LI As New clsLijstItems
        LI.dt = LI.sLijstItemByTitleAndType("Nieuwsbrief bevestiging", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            Dim email As String = LI.dr.Item("sItem1")
            'sBericht = sBericht.Replace("[name]", txtName.Text)
            'sBericht = sBericht.Replace("[phone]", txtPhone.Text)
            'sBericht = sBericht.Replace("[message]", txtMessage.Text)

            Dim iBcorePartijID = 0
            Dim iPatijBeheerder As Long = iPartijIDBeheerder
            Dim sStatus As String = "versturen"
            Dim sType As String = LI.dr.Item("sTitle")
            Dim sItem As String = ""
            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", email)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", email)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", email)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", email)
            Dim sOnderwerp As String = LI.dr.Item("sSubTitle").replace("[email]", email)
            Dim sBericht As String = LI.dr.Item("sDescription")
            sBericht = sBericht.Replace("[email]", txtEmailHome.Text)
            Dim sBijlagen As String = ""
            Dim sData As String = ""
            Dim sInfo As String = ""

            Dim M As New clsMail
            Dim iMailID As Long = M.iscMail(iBcorePartijID, iPatijBeheerder, sStatus, sType, sItem, sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, sData, sInfo, Now)

            LI.dt = LI.sLijstItemByTitleAndType("Nieuwsbrief bevestiging", iTaalID, "Mailing")
            If LI.dt.Rows.Count > 0 Then
                LI.dr = LI.dt.Rows(0)
                sBericht = LI.dr.Item("sDescription")
                email = LI.dr.Item("sItem1")
                sBericht = sBericht.Replace("[name]", txtNameHome.Text)
                sBericht = sBericht.Replace("[email]", txtEmailHome.Text)
                sBericht = sBericht.Replace("[bedrijfsnaam]", "Basis")
                'sBericht = sBericht.Replace("[phone]", txtPhone.Text)
                'sBericht = sBericht.Replace("[message]", txtMessage.Text.Replace(Environment.NewLine, "<br />"))

                sType = LI.dr.Item("sTitle")
                sVan = LI.dr.Item("sItem1").replace("[email]", email)
                sNaar = txtEmailHome.Text
                sCC = LI.dr.Item("sItem4").replace("[email]", email)
                sBCC = LI.dr.Item("sItem5").replace("[email]", email)
                sOnderwerp = LI.dr.Item("sSubTitle").replace("[email]", email)
                sBijlagen = ""
                sInfo = "website.nl"

                iMailID = M.iscMail(0, iPartijIDBeheerder, "versturen", sType, "", sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, "", sInfo, Now)

                Dim cCookie As HttpCookie = Request.Cookies("Follow")

                Dim sSessie As Sessie = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Sessies)(cCookie.Value).seSessies(0)

                sSessie.save(iPartijIDBeheerder, txtEmailHome.Text)
                Dim P As New clsPage
                Response.Redirect(P.sPageUrlByGuid(sLanguage, "1d6fb722-2352-44f4-a91e-9c9c176912aa"))
            Else

            End If
        End If
    End Sub

    Private Sub repSlider_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles repSlider.ItemCommand
        Dim t = e.CommandArgument
    End Sub

    Private Sub repCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCategories.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            Dim repArtikelen As Repeater = e.Item.FindControl("repArtikelen")


            ART.dt = ART.sArtikelenAndRoutesByCategorie(iTaalID, iCatID, 3)
            repArtikelen.DataSource = ART.dt
            repArtikelen.DataBind()

        End If
    End Sub

    'Protected Sub repArtikelenFeatured_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelenFeatured.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

    '        Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
    '        'Afbeelding tonen
    '        IMG.dt = IMG.sImageByTussTypeAndID("Artikel", iArtikelID)
    '        Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
    '        If IMG.dt.Rows.Count > 0 Then
    '            For Each IMG.dr In IMG.dt.Rows
    '                Select Case IMG.dr.Item("sSoort").ToString().ToLower()
    '                    Case "thumb"
    '                        imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
    '                        imgThumb.Alt = IMG.dr.Item("sData")
    '                End Select
    '            Next
    '        Else
    '            imgThumb.Src = sURL() & "UI/images/placehold-small.jpg"
    '        End If

    '        Dim AI As New clsArtikelItems
    '        AI.dt = AI.sArtikelItemsByArtikelID(iArtikelID, iTaalID)
    '        Dim sName As String = ""
    '        If AI.dt.Rows.Count > 0 Then
    '            For Each AI.dr In AI.dt.Rows
    '                Select Case AI.dr.Item("sType").toLower
    '                    Case "naam"
    '                        Dim ltlName As Literal = e.Item.FindControl("ltlName")
    '                        ltlName.Text = AI.dr.Item("sWaarde")
    '                        sName = AI.dr.Item("sWaarde")

    '                End Select
    '            Next
    '        End If


    '        ROUTE.dt = ROUTE.sRoutes("artikel", iArtikelID, iTaalID)
    '        If ROUTE.dt.Rows.Count > 0 Then
    '            ROUTE.dr = ROUTE.dt.Rows(0)
    '            Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
    '            aFollow.HRef = ROUTE.dr.Item("sURL")

    '        End If
    '        'Voorraad controle
    '        If ST.bVoorraad Then
    '            Dim AV As New clsArtikelVarianten
    '            AV.dt = AV.sArtikelVariantenBeschikbaar(iArtikelID)
    '            Dim spVoorraadInfo As HtmlGenericControl = e.Item.FindControl("spVoorraadInfo")
    '            Dim bWinkelwagen As Boolean = True
    '            If AV.dt.Rows.Count > 0 Then
    '                AV.dr = AV.dt.Rows(0)
    '                Dim iBeschikbaar As Integer = AV.scBeschikbaar(AV.dr.Item("iArtikelVariantID"))
    '                If iBeschikbaar > 0 Then
    '                    spVoorraadInfo.Visible = False
    '                Else
    '                    spVoorraadInfo.Visible = True
    '                    spVoorraadInfo.InnerHtml = "<span style='color:#f00;'>SOLD OUT</span>"
    '                End If
    '            Else
    '                spVoorraadInfo.Visible = True
    '                spVoorraadInfo.InnerHtml = "<span style='color:#e12c2c;'>SOLD OUT</span>"
    '            End If
    '        End If


    '        Dim dKorting As Decimal = CDec(DataBinder.Eval(e.Item.DataItem, "sKorting").ToString().Replace(".", ","))
    '        Dim dPrijs As Decimal = DataBinder.Eval(e.Item.DataItem, "iPrijs")

    '        'Korting tonen ja / nee
    '        If dKorting <> 0 Then
    '            Dim ltlNewPrice As Literal = e.Item.FindControl("ltlNewPrice")
    '            ltlNewPrice.Text = "&euro;" & dPrijs - dKorting
    '            'e.Item.FindControl("divDiscount").Visible = True
    '            e.Item.FindControl("spanPrice").Visible = True
    '            Dim spPrice As HtmlGenericControl = e.Item.FindControl("spPrice")
    '            spPrice.Attributes.Add("class", "price old-price")
    '        Else

    '            'e.Item.FindControl("divDiscount").Visible = False
    '            e.Item.FindControl("spanPrice").Visible = False
    '        End If
    '    End If
    'End Sub

    'Private Sub repCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCategories.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
    '        ROUTE.dt = ROUTE.sRoutes("Categorie", iCatID, iTaalID)
    '        If ROUTE.dt.Rows.Count > 0 Then
    '            ROUTE.dr = ROUTE.dt.Rows(0)
    '            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '            aLink.HRef = ROUTE.dr.Item("sURL")

    '        End If
    '    End If
    'End Sub
    'Private Sub repSlider_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repSlider.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim iLijstItemID As Long = DataBinder.Eval(e.Item.DataItem, "iLijstItemID")
    '        IMG.dt = IMG.sImageByTussTypeAndID("Slider", iLijstItemID)
    '        Dim bMobile As Boolean = False

    '        If IMG.dt.Rows.Count > 0 Then
    '            IMG.dr = IMG.dt.Rows(0)
    '            Dim imgDesktop As HtmlImage = e.Item.FindControl("imgDesktop")
    '            imgDesktop.Src = IMG.dr.Item("sSmall").Replace("~/", sURL())
    '            imgDesktop.Alt = IMG.dr.Item("sData")
    '        Else
    '            e.Item.Visible = False
    '        End If
    '    End If
    'End Sub



    'Dim sCurrentURL As String = sURL()
    'Protected Sub repSliderCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim iCategorieID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
    '        ROUTE.dt = ROUTE.sRoutes("categorie", iCategorieID, iTaalID)
    '        If ROUTE.dt.Rows.Count > 0 Then
    '            ROUTE.dr = ROUTE.dt.Rows(0)
    '            Dim aArtikelLink As HtmlAnchor = e.Item.FindControl("aCategoryLink")
    '            aArtikelLink.HRef = ROUTE.dr.Item("sURL")
    '        End If

    '        'Afbeelding tonen
    '        IMG.dt = IMG.sImageByTussTypeAndID("Categorie", iCategorieID)
    '        Dim iIndex As Long = 0
    '        Dim imgCategorieThumb As HtmlImage = e.Item.FindControl("imgCategorieThumb")
    '        If imgCategorieThumb IsNot Nothing Then
    '            If IMG.dt.Rows.Count > 0 Then
    '                For Each IMG.dr In IMG.dt.Rows
    '                    Select Case IMG.dr.Item("sSoort").ToString().ToLower()
    '                        Case "thumb"
    '                            If iIndex = 0 Then
    '                                imgCategorieThumb.Src = IMG.dr.Item("sSmall").replace("~/", sCurrentURL)
    '                                imgCategorieThumb.Alt = IMG.dr.Item("sData")
    '                                iIndex = iIndex + 1
    '                            End If
    '                    End Select
    '                Next
    '            Else
    '                imgCategorieThumb.Src = sCurrentURL & "UI/images/placehold-small.jpg"
    '            End If
    '        End If
    '    End If
    'End Sub
    'Protected Sub repReviews_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim dtDatum As Date = DataBinder.Eval(e.Item.DataItem, "dtDatum")
    '        Dim ltlDatum As Literal = e.Item.FindControl("ltlDatum")

    '        Dim sDate As String() = dtDatum.ToString("dd-MM-yyyy").Split("-")
    '        sDate(1) = MonthName(sDate(1))

    '        ltlDatum.Text = sDate(0) + " " + sDate(1) + " " + sDate(2)
    '    End If
    'End Sub

End Class