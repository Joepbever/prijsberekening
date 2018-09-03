
Imports System.Web.Services

Partial Class _categorie
    Inherits BasePage

    Dim P As New clsPage
    Dim CAT As New clsCategorie
    Dim CI As New clsCategorieItems
    Dim ART As New clsArtikelen
    Dim IMG As New clsImages
    Dim ROUTE As New clsRoutes
    Dim ST As New clsSettings
    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Dim bIsOnline As Boolean = False
    Dim iCurrentCatID As Long = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim responseBack As String = "/"
        If RouteData.Values("language") Is Nothing Then
            sLanguage = sDefaultLanguage
        Else
            sLanguage = RouteData.Values("language")
            responseBack = "/" '& sLanguage & responseBack
        End If
        sLanguage = sLanguage.ToLower()

        iCurrentCatID = RouteData.DataTokens.Item("categorieid")
        Dim sPageGuid As String = RouteData.DataTokens.Item("sGuid")
        iTaalID = sTaalID(sLanguage)

        ST.InitWebshopSettings(iPartijIDBeheerder)

        bIsOnline = IsOnline()
        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

            If Tekst.Text = "" Then
                divTekst.Visible = False
            End If
            If iCurrentCatID = Nothing Then
                'Alle hoofdcategorieën tonen
                If sPageGuid = "bf4b1252-bade-40f5-99a5-6f6b6635d0f5" Then
                    'Kortingpagina
                    ART.dt = ART.sArtikelenByKorting(iPartijIDBeheerder)
                    If ART.dt.Rows.Count > 0 Then
                        ART.dr = ART.dt.Rows(0)
                        repArtikelen.DataSource = ART.dt
                        repArtikelen.DataBind()
                    End If
                Else
                    Dim iParentID As Long = 0
                    CI.dt = CI.sCategorieItemsAndRoutesByParentID(iPartijIDBeheerder, iTaalID, iParentID, True, "Thumb")
                    If CI.dt.Rows.Count > 0 Then
                        repCategorien.DataSource = CI.dt
                        repCategorien.DataBind()
                    End If
                End If

            Else
                Dim iParentID As Long = 0
                'Alle HOOFDcategorieën tonen in een menu
                InitCategorieInfo(iCurrentCatID, iTaalID)


                CI.dt = CI.sCategorieItemsAndRoutesByParentID(iPartijIDBeheerder, iTaalID, iParentID, True, "Thumb")
                If CI.dt.Rows.Count > 0 Then
                    repCategories.DataSource = CI.dt
                    repCategories.DataBind()
                End If

                CAT.dt = CAT.sCategorieAndRoutesByParentID(iPartijIDBeheerder, iTaalID, iCurrentCatID)
                If CAT.dt.Rows.Count > 0 Then
                    'Alle SUBcategorieën tonen van huidige categorie
                    repSubCategorieLabels.DataSource = CAT.dt
                    repSubCategorieLabels.DataBind()
                Else

                    CAT.dt = CAT.sCategorieByCatID(iPartijIDBeheerder, iTaalID, iCurrentCatID)
                    If CAT.dt.Rows.Count > 0 And Not CAT.dt.Rows(0).Item("iParentID") = 0 Then
                        CAT.dt = CAT.sCategorieAndRoutesByParentID(iPartijIDBeheerder, iTaalID, CAT.dt.Rows(0).Item("iParentID"))
                        repSubCategorieLabels.DataSource = CAT.dt
                        repSubCategorieLabels.DataBind()
                    End If
                End If

                ART.dt = ART.sArtikelenByCategorieIDAndImages(iTaalID, iCurrentCatID)
                If ART.dt.Rows.Count > 0 Then
                    ART.dr = ART.dt.Rows(0)
                    Title = ART.dr.Item("sTitle")
                    MetaDescription = ART.dr.Item("sOmschrijving")
                    repArtikelen.DataSource = ART.dt
                    repArtikelen.DataBind()

                    'CAT.dt = CAT.sCategorieByParentIDTOP(iPartijIDBeheerder, iTaalID, iCatID, 3)
                    'If CAT.dt.Rows.Count > 0 Then
                    '    repSubCategoriesContent.DataSource = CAT.dt
                    '    repSubCategoriesContent.DataBind()
                    'End If
                    'Else
                    '    CAT.dt = CAT.sCategorieByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)
                    '    If CAT.dt.Rows.Count > 0 Then
                    '        repSubCategoriesContent.DataSource = CAT.dt
                    '        repSubCategoriesContent.DataBind()
                    '    End If
                End If
            End If
        End If
    End Sub

    Private Sub InitCategorieInfo(iCurrentCatID As Long, iTaalID As Long)
        CI.dt = CI.sCategorieItemsByCatID(iPartijIDBeheerder, iTaalID, iCurrentCatID)
        If CI.dt.Rows.Count > 0 Then
            CI.dr = CI.dt.Rows(0)
            'Titel.Text = CI.dr.Item("sTitle")
            Tekst.Text = CI.dr.Item("sPageItem1")

            divPageTitle.Visible = False
        End If
    End Sub


    'Dim bImageArtikel As Boolean = False

    Dim sCurrentURL As String = sURL()
    Protected Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")

            Dim AI As New clsArtikelItems
            AI.dt = AI.sArtikelItemsByArtikelID(iArtikelID, iTaalID)
            Dim sName As String = ""
            If AI.dt.Rows.Count > 0 Then
                For Each AI.dr In AI.dt.Rows
                    Select Case AI.dr.Item("sType").toLower
                        Case "naam"
                            Dim ltlName As Literal = e.Item.FindControl("ltlName")
                            ltlName.Text = AI.dr.Item("sWaarde")
                            sName = AI.dr.Item("sWaarde")
                        Case "korte omschrijving"
                            Dim ltlOmschrijving As Literal = e.Item.FindControl("ltlOmschrijving")
                            ltlOmschrijving.Text = AI.dr.Item("sWaarde")
                    End Select
                Next
            End If

            'Voorraad controle
            If ST.bVoorraad Then
                Dim AV As New clsArtikelVarianten
                AV.dt = AV.sArtikelVariantenBeschikbaar(iArtikelID)
                Dim spVoorraadInfo As HtmlGenericControl = e.Item.FindControl("spVoorraadInfo")

                If AV.dt.Rows.Count > 0 Then
                    AV.dr = AV.dt.Rows(0)
                    Dim iBeschikbaar As Integer = AV.scBeschikbaar(AV.dr.Item("iArtikelVariantID"))
                    If iBeschikbaar > 0 Then
                        spVoorraadInfo.Visible = False
                    Else
                        spVoorraadInfo.Visible = True
                        spVoorraadInfo.InnerHtml = "<span style='color:#f00;'>SOLD OUT</span>"
                    End If
                    Dim divGridItem As HtmlGenericControl = e.Item.FindControl("divGridItem")
                    divGridItem.Attributes.Add("data-variant-id", AV.dr.Item("iArtikelVariantID"))
                Else
                    Dim divGridItem As HtmlGenericControl = e.Item.FindControl("divGridItem")
                    divGridItem.Attributes.Add("data-variant-id", 0)
                    spVoorraadInfo.Visible = True
                    spVoorraadInfo.InnerHtml = "<span style='color:#e12c2c;'>SOLD OUT</span>"
                End If
            End If

            Dim dKorting As Decimal = CDec(DataBinder.Eval(e.Item.DataItem, "sKorting").ToString().Replace(".", ",")) ' .ToString().Replace(".", ",")
            Dim dPrijs As Decimal = DataBinder.Eval(e.Item.DataItem, "iPrijs")

            'Korting tonen ja / nee
            If dKorting <> 0 Then
                Dim ltlNewPrice As Literal = e.Item.FindControl("ltlNewPrice")
                ltlNewPrice.Text = "&euro;" & dPrijs - dKorting
                Dim spPrice As HtmlGenericControl = e.Item.FindControl("spPrice")
                spPrice.Attributes.Add("class", "price old-price")
            End If

            Dim bPrijsTonenIngelogd As Boolean = DataBinder.Eval(e.Item.DataItem, "bIngelogd")
            Dim bPrijsTonenUitgelogd As Boolean = DataBinder.Eval(e.Item.DataItem, "bUitgelogd")



            Dim bPrijsTonen As Boolean = True
            If bPrijsTonenIngelogd = False And bIsOnline = True Then
                bPrijsTonen = False
            End If
            If bPrijsTonenUitgelogd = False And bIsOnline = False Then
                bPrijsTonen = False
            End If

            Dim divRequest As HtmlGenericControl = e.Item.FindControl("divRequest")
            If bPrijsTonen Then
                If dKorting = 0 Then
                    e.Item.FindControl("spanPrice").Visible = True
                End If
                divRequest.Visible = False
            Else
                divRequest.Visible = True
            End If

            Dim ltlLink As Literal = e.Item.FindControl("ltlLink")
            ' Dim aLink As HtmlAnchor = e.Item.bPrijsTonenFindControl("aLink")
            If bPrijsTonen = False Then
                'Offerte aanvragen
                'aLink.HRef = "?product=" & sName
                'aLink.Attributes.Add("class", "btn btn-default")
                ltlLink.Text = Resources.Resource.quotation
            Else  'Toevoegen aan winkelwagen
                ltlLink.Text = Resources.Resource.BtnAddToCart
            End If

        End If
    End Sub


    Public sCategorie As String = ""
    Private Sub repCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCategories.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)
            sCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")
            If iCurrentCatID = iCatID Then
                hidIndex.Value = e.Item.ItemIndex
            End If

            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            If iCatID = iCurrentCatID Then
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
            End If

        End If
    End Sub

    Public sSubCategorie As String = ""
    Protected Sub repSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)

            sSubCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")

            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            ROUTE.dt = ROUTE.sRoutes("categorie", iCatID, iTaalID)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
                aLink.HRef = ROUTE.dr.Item("sURL")
            End If

            If iCatID = iCurrentCatID Then
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
            End If

            If CI.dt.Rows.Count > 0 Then
                Dim repSubSubCategories As Repeater = e.Item.FindControl("repSubSubCategories")
                repSubSubCategories.DataSource = CI.dt
                repSubSubCategories.DataBind()
            End If
        End If
    End Sub

    Public sSubSubCategorie As String = ""
    Protected Sub repSubSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            sSubSubCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            ROUTE.dt = ROUTE.sRoutes("categorie", iCatID, iTaalID)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
                aLink.HRef = ROUTE.dr.Item("sURL")
            End If

            If iCatID = iCurrentCatID Then
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
            End If

            CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)
            If CI.dt.Rows.Count > 0 Then
                Dim repSubSubCategories As Repeater = e.Item.FindControl("repSubSubSubCategories")
                repSubSubCategories.DataSource = CI.dt
                repSubSubCategories.DataBind()
            End If
        End If
    End Sub

    Protected Sub repSubSubSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            ROUTE.dt = ROUTE.sRoutes("categorie", iCatID, iTaalID)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
                aLink.HRef = ROUTE.dr.Item("sURL")
            End If
            If iCatID = iCurrentCatID Then
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
            End If
        End If
    End Sub



    Dim bImage As Boolean = False
    'Protected Sub repSubCategoriesContent_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")

    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        ROUTE.dt = ROUTE.sRoutes("categorie", iCatID, iTaalID)
    '        If ROUTE.dt.Rows.Count > 0 Then
    '            ROUTE.dr = ROUTE.dt.Rows(0)
    '            Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
    '            aLink.HRef = ROUTE.dr.Item("sURL")
    '        End If

    '        IMG.dt = IMG.sImageByTussTypeAndID("Categorie", iCatID)
    '        If IMG.dt.Rows.Count > 0 Then
    '            For Each IMG.dr In IMG.dt.Rows
    '                Select Case IMG.dr.Item("sSoort").ToString().ToLower()
    '                    Case "foto"
    '                        Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
    '                        imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
    '                        imgThumb.Alt = IMG.dr.Item("sData")
    '                        bImage = True
    '                End Select
    '            Next
    '        End If

    '        If bImage = False Then
    '            Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
    '            imgThumb.Src = "/Resources/img/thumb.jpeg"
    '            imgThumb.Alt = DataBinder.Eval(e.Item.DataItem, "sTitle")
    '        End If
    '    End If
    'End Sub


    Protected Sub repSubCategorieLabels_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            Dim liSubCatLabel As HtmlGenericControl = e.Item.FindControl("liSubCatLabel")
            If iCatID = iCurrentCatID Then
                liSubCatLabel.Attributes.Add("class", "active")
            End If
        End If
    End Sub
End Class