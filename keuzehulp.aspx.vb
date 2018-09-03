
Imports System.Web.Services

Partial Class _categorie
    Inherits BasePage

    'Dim P As New clsPage
    'Dim CI As New clsCategorieItems
    'Dim ART As New clsArtikelen
    'Dim IMG As New clsImages

    'Public iLastCatID As Long = 0
    Public sLanguage, sQs, sParentPage, sOffertePage As String
    'Dim iTaalID As Long

    'Public sFullLink As String = ""
    'Public sFullLinkCurrentCategorie As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim responseBack As String = "/"
        If RouteData.Values("language") Is Nothing Then
            sLanguage = sDefaultLanguage
        Else
            sLanguage = RouteData.Values("language")
            responseBack = "/" '& sLanguage & responseBack
        End If
        sLanguage = sLanguage.ToLower()


        'Me.Master.FindControl("divFooter").Visible = False
        'Me.Master.FindControl("liShop").Visible = False
        'sQueryString = RouteData.Values("page")
        'Dim sNiveau As String = "1"
        'Dim sNiveau2, sNiveau3, sNiveau4 As String
        'If Not IsNothing(RouteData.Values("niveau2")) Then
        '    sNiveau2 = RouteData.Values("niveau2")
        '    sNiveau = "2"
        'End If

        'If Not IsNothing(RouteData.Values("niveau3")) Then
        '    sNiveau3 = RouteData.Values("niveau3")
        '    sNiveau = "3"
        'End If

        'If Not IsNothing(RouteData.Values("niveau4")) Then
        '    sNiveau4 = RouteData.Values("niveau4")
        '    sNiveau = "4"
        'End If


        'Dim CAT As New clsCategorie
        'Dim iCatID As Long
        'Select Case sNiveau
        '    Case "1"
        '        iCatID = RouteData.DataTokens.Item("niveau1ID")
        '    Case "2"
        '        iCatID = RouteData.DataTokens.Item("niveau2ID")
        '    Case "3"
        '        iCatID = RouteData.DataTokens.Item("niveau3ID")
        '    Case "4"
        '        iCatID = RouteData.DataTokens.Item("niveau4ID")
        'End Select

        'iLastCatID = iCatID
        'iTaalID = sTaalID(sLanguage)

        'sOffertePage = P.sPageUrlByGuid(sLanguage, "2181c6e0-0bd4-4ad0-9bea-1114d69ed16c")


        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)


            ''Menu links
            'Dim iParentID As Long = 0
            'CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iParentID)
            'If CI.dt.Rows.Count > 0 Then
            '    repCategories.DataSource = CI.dt
            '    repCategories.DataBind()
            'End If


            'Dim ART As New clsArtikelen
            'ART.dt = ART.sArtikelenByCategorie(iTaalID, iCatID)
            'If ART.dt.Rows.Count > 0 Then
            '    ART.dr = ART.dt.Rows(0)
            '    'Title = ART.dr.Item("sTitle")
            '    'MetaDescription = ART.dr.Item("sOmschrijving")
            '    repArtikelen.DataSource = ART.dt
            '    repArtikelen.DataBind()

            '    CAT.dt = CAT.sCategorieByParentIDTOP(iPartijIDBeheerder, iTaalID, iCatID, 3)
            '    If CAT.dt.Rows.Count > 0 Then
            '        repSubCategoriesContent.DataSource = CAT.dt
            '        repSubCategoriesContent.DataBind()
            '    End If
            'Else
            '    CAT.dt = CAT.sCategorieByParentID(iPartijIDBeheerder, iTaalID, iCatID)
            '    If CAT.dt.Rows.Count > 0 Then
            '        repSubCategoriesContent.DataSource = CAT.dt
            '        repSubCategoriesContent.DataBind()
            '    End If
            'End If
            If IsOnline() Then

            Else
                divPriceRange.Attributes.Add("class", "hidden")
                SortOptions.Attributes.Add("class", "hidden")
                lblSort.Attributes.Add("class", "hidden")

            End If


            'divPriceRange
        End If
    End Sub



    ''Dim bImageArtikel As Boolean = False
    'Private Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
    '        Dim iPartijID = sPartijID()
    '        'Afbeelding tonen
    '        IMG.dt = IMG.sImageByTussTypeAndID("Artikel", iArtikelID)
    '        If IMG.dt.Rows.Count > 0 Then
    '            For Each IMG.dr In IMG.dt.Rows
    '                Select Case IMG.dr.Item("sSoort").ToString().ToLower()
    '                    Case "foto"
    '                        Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
    '                        imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
    '                        imgThumb.Alt = IMG.dr.Item("sData")
    '                End Select
    '            Next
    '        End If

    '        'Links
    '        Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
    '        aFollow.HRef = sFullLinkCurrentCategorie & "/" & FriendlyUrl(DataBinder.Eval(e.Item.DataItem, "sArtikel"))

    '        Dim aNoFollow As HtmlAnchor = e.Item.FindControl("aNoFollow")
    '        aNoFollow.HRef = sFullLinkCurrentCategorie & "/" & FriendlyUrl(DataBinder.Eval(e.Item.DataItem, "sArtikel"))


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

    '                    Case "korte omschrijving"
    '                        Dim ltlOmschrijving As Literal = e.Item.FindControl("ltlOmschrijving")
    '                        ltlOmschrijving.Text = AI.dr.Item("sWaarde")
    '                End Select
    '            Next
    '        End If

    '        'Voorraad controle
    '        Dim AV As New clsArtikelVarianten
    '        AV.dt = AV.sArtikelVariantenByArtikelID(iArtikelID)
    '        Dim spanDirect As HtmlGenericControl = e.Item.FindControl("spanDirect")
    '        Dim bWinkelwagen As Boolean = True
    '        If AV.dt.Rows.Count > 0 Then
    '            AV.dr = AV.dt.Rows(0)
    '            Dim iBeschikbaar As Integer = AV.scBeschikbaar(AV.dr.Item("iArtikelVariantID"))
    '            If iBeschikbaar > 0 Then
    '                spanDirect.Visible = True
    '            Else
    '                spanDirect.Visible = True
    '                spanDirect.InnerHtml = "<span style='color:#f00;'>Niet op voorraad</span>"
    '            End If
    '            Dim divGridItem As HtmlGenericControl = e.Item.FindControl("divGridItem")
    '            divGridItem.Attributes.Add("data-variant-id", AV.dr.Item("iArtikelVariantID"))
    '        Else
    '            Dim divGridItem As HtmlGenericControl = e.Item.FindControl("divGridItem")
    '            divGridItem.Attributes.Add("data-variant-id", 0)
    '            spanDirect.Visible = True
    '            spanDirect.InnerHtml = "<span style='color:#f00;'>Niet op voorraad</span>"
    '        End If

    '        Dim dKorting As Decimal = DataBinder.Eval(e.Item.DataItem, "sKorting").ToString().Replace(".", ",")
    '        Dim dPrijs As Decimal = DataBinder.Eval(e.Item.DataItem, "iPrijs")

    '        'Korting tonen ja / nee
    '        If dKorting <> 0 Then
    '            Dim divDiscount As HtmlGenericControl = e.Item.FindControl("divDiscount")
    '            divDiscount.Visible = True
    '            Dim ltlNewPrice As Literal = e.Item.FindControl("ltlNewPrice")
    '            ltlNewPrice.Text = dPrijs - dKorting
    '            e.Item.FindControl("spanPrice").Visible = False
    '        End If


    '        Dim bPrijsTonenIngelogd As Boolean = DataBinder.Eval(e.Item.DataItem, "bIngelogd")
    '        Dim bPrijsTonenUitgelogd As Boolean = DataBinder.Eval(e.Item.DataItem, "bUitgelogd")

    '        Dim bIsOnline As Boolean = IsOnline()

    '        Dim bPrijsTonen As Boolean = True
    '        If bPrijsTonenIngelogd = False And bIsOnline = True Then
    '            bPrijsTonen = False
    '        End If
    '        If bPrijsTonenUitgelogd = False And bIsOnline = False Then
    '            bPrijsTonen = False
    '        End If

    '        Dim divRequest As HtmlGenericControl = e.Item.FindControl("divRequest")
    '        If bPrijsTonen Then
    '            If dKorting = 0 Then
    '                e.Item.FindControl("spanPrice").Visible = True
    '            End If
    '            'Op basis van prijsafspraak de prijs voor het ingelogde bedrijf tonen
    '            Dim ASTF As New clsArtikelStaffel
    '            ASTF._iArtikelID = iArtikelID
    '            ASTF._iPartijID = iPartijID
    '            ASTF.sArtikelStaffelByArtikelIDAndPartijID()
    '            If ASTF.dt.Rows.Count > 0 Then
    '                Dim ltlPrice As Literal = e.Item.FindControl("ltlPrice")
    '                ltlPrice.Text = ASTF._iPrijs.ToString()
    '            End If
    '            divRequest.Visible = False
    '            Else
    '                divRequest.Visible = True
    '        End If

    '        Dim ltlLink As Literal = e.Item.FindControl("ltlLink")
    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        If bWinkelwagen = False Or bPrijsTonen = False Then
    '            'Offerte aanvragen

    '            aLink.HRef = sOffertePage & "?product=" & sName
    '            aLink.Attributes.Add("class", "btn btn-default")
    '            ltlLink.Text = Resources.Resource.quotation
    '        Else  'Toevoegen aan winkelwagen
    '            ltlLink.Text = Resources.Resource.BtnAddToCart
    '        End If
    '    End If
    'End Sub


    'Public sCategorie As String = ""
    'Private Sub repCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCategories.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
    '        CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID)
    '        sFullLink = "/" & sLanguage.ToLower() 'start 

    '        sCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        aLink.HRef = "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '        If iCatID = iLastCatID Then
    '            aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
    '            sFullLinkCurrentCategorie = "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
    '            If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

    '            Else
    '                ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
    '            End If
    '        End If

    '        If CI.dt.Rows.Count > 0 Then
    '            Dim repSubCategories As Repeater = e.Item.FindControl("repSubCategories")
    '            sFullLink &= "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            repSubCategories.DataSource = CI.dt
    '            repSubCategories.DataBind()
    '        End If
    '    End If
    'End Sub

    'Public sSubCategorie As String = ""
    'Protected Sub repSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
    '        CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID)

    '        sSubCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")

    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        aLink.HRef = "/" & sCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")

    '        If iCatID = iLastCatID Then
    '            sFullLinkCurrentCategorie = "/" & sCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
    '            ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
    '            If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

    '            Else
    '                ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
    '            End If
    '        End If

    '        If CI.dt.Rows.Count > 0 Then
    '            Dim repSubSubCategories As Repeater = e.Item.FindControl("repSubSubCategories")
    '            sFullLink &= "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            repSubSubCategories.DataSource = CI.dt
    '            repSubSubCategories.DataBind()
    '        End If
    '    End If
    'End Sub

    'Public sSubSubCategorie As String = ""
    'Protected Sub repSubSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
    '        sSubSubCategorie = DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        aLink.HRef = "/" & sCategorie & "/" & sSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")

    '        If iCatID = iLastCatID Then
    '            sFullLinkCurrentCategorie = "/" & sCategorie & "/" & sSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
    '            ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
    '            If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

    '            Else
    '                ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
    '            End If
    '        End If



    '        CI.dt = CI.sCategorieItemsByParentID(iPartijIDBeheerder, iTaalID, iCatID)
    '        If CI.dt.Rows.Count > 0 Then
    '            Dim repSubSubCategories As Repeater = e.Item.FindControl("repSubSubSubCategories")
    '            sFullLink &= "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            repSubSubCategories.DataSource = CI.dt
    '            repSubSubCategories.DataBind()
    '        End If
    '    End If
    'End Sub

    'Protected Sub repSubSubSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        aLink.HRef = sFullLink & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '        aLink.HRef = "/" & sCategorie & "/" & sSubCategorie & "/" & sSubSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '        If iCatID = iLastCatID Then
    '            sFullLinkCurrentCategorie = "/" & sCategorie & "/" & sSubCategorie & "/" & sSubSubCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
    '            aLink.Attributes.Add("class", aLink.Attributes.Item("class") & " active")
    '            ltlActiveCategorie.Text = DataBinder.Eval(e.Item.DataItem, "sTitle")
    '            If IsDBNull(DataBinder.Eval(e.Item.DataItem, "sPageItem1")) Then

    '            Else
    '                ltlCategoriePageItem.Text = DataBinder.Eval(e.Item.DataItem, "sPageItem1")
    '            End If
    '        End If
    '    End If
    'End Sub



    'Dim bImage As Boolean = False
    'Protected Sub repSubCategoriesContent_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")

    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        aLink.HRef = sFullLinkCurrentCategorie & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")

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
    '            imgThumb.Src = sURL() & "Resources/img/thum.png"
    '            imgThumb.Alt = DataBinder.Eval(e.Item.DataItem, "sTitle")
    '        End If
    '    End If
    'End Sub
End Class