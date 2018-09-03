
Imports System.Web.Services

Partial Class _categorie
    Inherits BasePage

    Dim P As New clsPage
    Dim CI As New clsCategorieItems
    Dim ART As New clsArtikelen
    Dim IMG As New clsImages

    Dim ST As New clsSettings
    Public iLastCatID As Long = 0
    Public sLanguage, sQs, sParentPage, sOffertePage As String
    Dim iTaalID As Long

    Public sFullLink As String = ""
    Public sFullLinkCurrentCategorie As String = ""
    Dim bIsOnline As Boolean = False
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim responseBack As String = "/"
        If RouteData.Values("language") Is Nothing Then
            sLanguage = sDefaultLanguage
        Else
            sLanguage = RouteData.Values("language")
            responseBack = "/" '& sLanguage & responseBack
        End If
        sLanguage = sLanguage.ToLower()

        Dim CAT As New clsCategorie
        Dim iCatID As Long = RouteData.DataTokens.Item("categorieid")
        iLastCatID = iCatID
        iTaalID = sTaalID(sLanguage)

        sOffertePage = P.sPageUrlByGuid(sLanguage, "2181c6e0-0bd4-4ad0-9bea-1114d69ed16c")

        bIsOnline = IsOnline()
        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)


            'Menu links
            Dim iParentID As Long = 0
            CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iParentID, True)
            If CI.dt.Rows.Count > 0 Then
                repCategories.DataSource = CI.dt
                repCategories.DataBind()
            End If


            Dim ART As New clsArtikelen
            ART.dt = ART.sArtikelen(322)
            If ART.dt.Rows.Count > 0 Then
                ART.dr = ART.dt.Rows(0)
                'Title = ART.dr.Item("sTitle")
                'MetaDescription = ART.dr.Item("sOmschrijving")

                ST.InitWebshopSettings(iPartijIDBeheerder)

                repArtikelen.DataSource = ART.dt
                repArtikelen.DataBind()

                CAT.dt = CAT.sCategorieByParentIDTOP(iPartijIDBeheerder, iTaalID, iCatID, 3)
                If CAT.dt.Rows.Count > 0 Then
                    'repSubCategoriesContent.DataSource = CAT.dt
                    'repSubCategoriesContent.DataBind()
                End If
            Else
                CAT.dt = CAT.sCategorieByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)
                If CAT.dt.Rows.Count > 0 Then
                    'repSubCategoriesContent.DataSource = CAT.dt
                    'repSubCategoriesContent.DataBind()
                End If
            End If
        End If
    End Sub


    Dim ROUTE As New clsRoutes
    'Dim bImageArtikel As Boolean = False

    Dim sCurrentURL As String = sURL()
    Protected Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
            'Afbeelding tonen
            IMG.dt = IMG.sImageByTussTypeAndID("Artikel", iArtikelID)
            Dim iIndex As Long = 0
            Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
            If IMG.dt.Rows.Count > 0 Then
                For Each IMG.dr In IMG.dt.Rows
                    Select Case IMG.dr.Item("sSoort").ToString().ToLower()
                        Case "thumb"
                            If iIndex = 1 Then
                                imgThumb = e.Item.FindControl("imgThumb2")
                            End If
                            imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sCurrentURL)
                            imgThumb.Alt = IMG.dr.Item("sData")
                            iIndex = iIndex + 1
                    End Select
                Next
            Else
                imgThumb.Src = sCurrentURL & "Resources/img/placehold-small.jpg"
            End If


            ROUTE.dt = ROUTE.sRoutes("artikel", iArtikelID, iTaalID)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
                aFollow.HRef = ROUTE.dr.Item("sURL")

                Dim aNoFollow As HtmlAnchor = e.Item.FindControl("aNoFollow")
                aNoFollow.HRef = ROUTE.dr.Item("sURL")
            End If

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



            Dim dKorting As Decimal = CDec(DataBinder.Eval(e.Item.DataItem, "sKorting")) ' .ToString().Replace(".", ",")
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
            sFullLink = "/" & "shop" & "/" & sLanguage.ToLower()  'start 

            sCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            aLink.HRef = "/" & "shop" & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
            If iCatID = iLastCatID Then
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
                sFullLinkCurrentCategorie = "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                'ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
                'If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

                'Else
                '    ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
                'End If
            End If

            'If CI.dt.Rows.Count > 0 Then
            '    Dim repSubCategories As Repeater = e.Item.FindControl("repSubCategories")
            '    sFullLink &= "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
            '    repSubCategories.DataSource = CI.dt
            '    repSubCategories.DataBind()
            'End If
        End If
    End Sub

    Public sSubCategorie As String = ""
    Protected Sub repSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)

            sSubCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")

            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            aLink.HRef = "/" & "shop" & "/" & sCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")

            If iCatID = iLastCatID Then
                sFullLinkCurrentCategorie = "/" & sCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
                'ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
                'If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

                'Else
                '    ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
                'End If
            End If

            If CI.dt.Rows.Count > 0 Then
                Dim repSubSubCategories As Repeater = e.Item.FindControl("repSubSubCategories")
                sFullLink &= "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
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
            aLink.HRef = "/" & sCategorie & "/" & sSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")

            If iCatID = iLastCatID Then
                sFullLinkCurrentCategorie = "/" & sCategorie & "/" & sSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
                'ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
                'If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

                'Else
                '    ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
                'End If
            End If



            CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID, True)
            If CI.dt.Rows.Count > 0 Then
                Dim repSubSubCategories As Repeater = e.Item.FindControl("repSubSubSubCategories")
                sFullLink &= "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                repSubSubCategories.DataSource = CI.dt
                repSubSubCategories.DataBind()
            End If
        End If
    End Sub

    Protected Sub repSubSubSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            aLink.HRef = sFullLink & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
            aLink.HRef = "/" & sCategorie & "/" & sSubCategorie & "/" & sSubSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
            If iCatID = iLastCatID Then
                sFullLinkCurrentCategorie = "/" & sCategorie & "/" & sSubCategorie & "/" & sSubSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
                'ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
                'If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

                'Else
                '    ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
                'End If
            End If
        End If
    End Sub



    Dim bImage As Boolean = False
    Protected Sub repSubCategoriesContent_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")

            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            aLink.HRef = sFullLinkCurrentCategorie & "/" & "shop" & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")

            IMG.dt = IMG.sImageByTussTypeAndID("Categorie", iCatID)
            If IMG.dt.Rows.Count > 0 Then
                For Each IMG.dr In IMG.dt.Rows
                    Select Case IMG.dr.Item("sSoort").ToString().ToLower()
                        Case "foto"
                            Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
                            imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
                            imgThumb.Alt = IMG.dr.Item("sData")
                            bImage = True
                    End Select
                Next
            End If

            If bImage = False Then
                Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
                imgThumb.Src = "/Resources/img/thumb.jpeg"
                imgThumb.Alt = DataBinder.Eval(e.Item.DataItem, "sTitle")
            End If
        End If
    End Sub
End Class