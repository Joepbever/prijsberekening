Imports System.Data

Partial Class ShopTheLook
    Inherits BasePage

    Dim P As New clsPage
    Dim PI As New clsPageItems
    Dim U As New clsUtility

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Dim IMG As New clsImages
    Dim iCatIDActive As Long = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If
        iTaalID = sTaalID(sLanguage)
        hidTaalID.Value = iTaalID
        hidCatID.Value = ""

        If Request.QueryString("id") IsNot Nothing Then
            iCatIDActive = Request.QueryString("id")
            hidCatID.Value = iCatIDActive
        End If
        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()
            PI.sPI(Page, sLanguage, sQs, False, False, True)
            'repeater vullen 
            Dim CAT As New clsCategorie
            CAT.dt = CAT.sCategorieByParentID(iPartijIDBeheerder, iTaalID, 783) ' 696 staat voor shopthelook
            If CAT.dt.Rows.Count > 0 Then
                repCategorie.DataSource = CAT.dt
                repCategorie.DataBind()

                If ART.dt.Rows.Count > 0 Then
                    ART.dr = ART.dt.Rows(0)
                    repArtikelen.DataSource = ART.dt
                    repArtikelen.DataBind()
                End If
            End If
        End If
    End Sub



    Dim ART As New clsArtikelen

    Protected Sub repCategorie_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCategorie.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")

            ART.dt.Merge(ART.sArtikelenAndRoutesByCategorie(iTaalID, iCatID))

            Dim li As HtmlGenericControl = e.Item.FindControl("liCategorie")
            If iCatIDActive = 0 Then
                If e.Item.ItemIndex = 0 Then
                    li.Attributes.Add("class", "item button is-checked")
                End If
            Else
                If iCatIDActive = iCatID Then
                    li.Attributes.Add("class", "item button is-checked")
                End If
            End If


            'VOOR EEN NESTED REPEATER
            'If ART.dt.Rows.Count > 0 Then
            '    ART.dr = ART.dt.Rows(0)
            '    Dim repArtikelen As Repeater = e.Item.FindControl("repArtikelen")
            '    repArtikelen.DataSource = ART.dt
            '    repArtikelen.DataBind()
            'Else
            '    e.Item.Visible = False
            'End If

            'IMG.dt = IMG.sImageByTussTypeAndID("Categorie", iCatID)
            'If IMG.dt.Rows.Count > 0 Then
            '    For Each IMG.dr In IMG.dt.Rows
            '        Select Case IMG.dr.Item("sSoort").ToString().ToLower()
            '            Case "thumb"
            '                Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
            '                imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
            '                imgThumb.Alt = IMG.dr.Item("sData")
            '        End Select
            '    Next
            'End If



            IMG.dt = IMG.sImageByTussTypeAndID("Categorie", iCatID)
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
                imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
                imgThumb.Alt = IMG.dr.Item("sData")

            End If
        End If
    End Sub

    Dim ROUTE As New clsRoutes
    Dim AV As New clsArtikelVarianten
    Dim iIndex2 As Long = 0
    Protected Sub repArtikelen_ItemDataBound1(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
            IMG.dt = IMG.sImageByTussTypeAndID("Artikel", iArtikelID)
            Dim iIndex As Long = 0
            iIndex2 = iIndex2 + 1
            Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
                imgThumb.Alt = IMG.dr.Item("sData")

                'For Each IMG.dr In IMG.dt.Rows
                '    Select Case IMG.dr.Item("sSoort").ToString().ToLower()
                '        Case "thumb"
                '            'VOOR EEN HOVER FOTO
                '            'If iIndex = 1 Then
                '            '    imgThumb = e.Item.FindControl("imgThumb2")
                '            'End If

                '            iIndex = iIndex + 1
                '    End Select
                'Next
            Else
                imgThumb.Src = sURL() & "UI/images/placehold-small.jpg"
            End If




            'Dim AI As New clsArtikelItems
            'AI.dt = AI.sArtikelItemsByArtikelID(iArtikelID, iTaalID)
            'Dim sName As String = ""
            'If AI.dt.Rows.Count > 0 Then
            '    For Each AI.dr In AI.dt.Rows
            '        Select Case AI.dr.Item("sType").toLower
            '            Case "naam"
            '                Dim ltlName As Literal = e.Item.FindControl("ltlName")
            '                ltlName.Text = AI.dr.Item("sWaarde")
            '                sName = AI.dr.Item("sWaarde")

            '                'Case "korte omschrijving"
            '                '    Dim ltlOmschrijving As Literal = e.Item.FindControl("ltlOmschrijving")
            '                '    ltlOmschrijving.Text = AI.dr.Item("sWaarde")
            '        End Select
            '    Next
            'End If


            iCount = 0


            Dim divContent As HtmlGenericControl = e.Item.FindControl("divContent")
            divContent.ID = "divContent_" & iIndex2


            AV.dt = AV.sArtikelVariantenBeschikbaar(iArtikelID)
            Dim repArtikelVarianten As Repeater = e.Item.FindControl("repArtikelVarianten")
            repArtikelVarianten.DataSource = AV.dt
            repArtikelVarianten.DataBind()

            If AV.dt.Rows.Count > 0 Then
                AV.dr = AV.dt.Rows(0)
                Dim iBeschikbaar As Integer = AV.scBeschikbaar(AV.dr.Item("iArtikelVariantID"))
                If iBeschikbaar > 0 Then
                Else
                    'ltlDeliveryTime.Text = "<span style='color:#f00;'>Niet op voorraad</span>"
                End If
                hidArtikelVariantID.Value = AV.dr.Item("iArtikelVariantID")
            Else
                ' ltlDeliveryTime.Text = "<span style='color:#f00;'>Niet op voorraad</span>"
                hidArtikelVariantID.Value = "0"
            End If

            Dim dKorting As Decimal = CDec(DataBinder.Eval(e.Item.DataItem, "sKorting")) ' .ToString().Replace(".", ",")
            Dim dPrijs As Decimal = DataBinder.Eval(e.Item.DataItem, "iPrijs")

            'Korting tonen ja / nee
            If dKorting <> 0 Then
                Dim ltlNewPrice As Literal = e.Item.FindControl("ltlNewPrice")
                ltlNewPrice.Text = "&euro;" & dPrijs - dKorting
                'e.Item.FindControl("divDiscount").Visible = True
                'e.Item.FindControl("spanPrice").Visible = True
                Dim spPrice As HtmlGenericControl = e.Item.FindControl("spPrice")
                spPrice.Attributes.Add("class", "price old-price")
            Else

                'e.Item.FindControl("divDiscount").Visible = False
                '   e.Item.FindControl("spanPrice").Visible = False
            End If
        End If
    End Sub


    Public iCount2 As Long = 0
    Public iCount As Long = 0

    Protected Sub repArtikelVarianten_ItemDataBound1(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rb As HtmlInputRadioButton = e.Item.FindControl("rb")
            rb.ID = "rb_" & DataBinder.Eval(e.Item.DataItem, "sWaarde") & "_" & iCount2.ToString()
            If iCount = 0 Then
                hidArtikelVariantID.Value = DataBinder.Eval(e.Item.DataItem, "iArtikelVariantID")
                rb.Checked = True
            End If
            iCount2 = iCount2 + 1
            iCount = iCount + 1
        End If
    End Sub
End Class