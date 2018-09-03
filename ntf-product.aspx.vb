Imports System.Data
Imports System.Web.Services

Partial Class Product
    Inherits BasePage

    'Dim UTIL As New clsUtility
    'Dim P As New clsPage

    'Dim iTaalID As Long
    'Public sQs, sLanguage, sType As String

    'Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    '    sQs = RouteData.Values("page")
    '    Dim responseBack As String = "/" & sQs
    '    If RouteData.Values("language") Is Nothing Then
    '        sLanguage = sDefaultLanguage
    '    Else
    '        sLanguage = RouteData.Values("language")
    '        responseBack = "/" & sLanguage & responseBack
    '    End If

    '    iTaalID = sTaalID(sLanguage)

    '    If Page.IsPostBack = False Then


    '        Dim PT As New clsPartijen
    '        Dim PartijGegevens As String = PT.sPartij(iPartijIDBeheerder)
    '        'aTel.HRef = "tel:" & PT.sTelefoon


    '        Dim sArt As String = RouteData.Values("artikel")
    '        Dim iArtikelID As Long = RouteData.DataTokens.Item("artikelid")
    '        hidArtikelID.Value = iArtikelID.ToString()

    '        Dim PI As New clsPageItems
    '        PI.sPI(Page, sLanguage, "product", False, True, True)

    '        Dim ART As New clsArtikelen

    '        Dim sArtikel As String = ""
    '        Dim sInformatie As String = ""
    '        Dim sDescription As String = ""
    '        Dim sProductDescription As String = ""
    '        Dim sDocumentation As String = ""
    '        Dim sAdditionInformation As String = ""

    '        Dim bQuotation As Boolean = False

    '        Dim ARTITEMS As New clsArtikelItems
    '        ARTITEMS.dt = ARTITEMS.sByArtikelIDAndTaalID(iArtikelID, sTaalID(sLanguage))
    '        For Each ARTITEMS.dr In ARTITEMS.dt.Rows
    '            Select Case ARTITEMS.dr.Item("sType").ToLower()
    '                Case "naam"
    '                    sArtikel = ARTITEMS.dr.Item("sWaarde")
    '                Case "korte omschrijving"
    '                    sDescription = ARTITEMS.dr.Item("sWaarde").ToString().Replace(Environment.NewLine, "<br />")
    '                Case "lange omschrijving"
    '                    sProductDescription = ARTITEMS.dr.Item("sWaarde").ToString().Replace(Environment.NewLine, "<br />")
    '                    liProductDescription.Visible = True
    '                    liProductDescriptionTab.Visible = True
    '                Case "documentatie"
    '                    sDocumentation = ARTITEMS.dr.Item("sWaarde").ToString().Replace(Environment.NewLine, "<br />")
    '                    liDocumentation.Visible = True
    '                    liDocumentationTab.Visible = True
    '                Case "overige informatie"
    '                    sAdditionInformation = ARTITEMS.dr.Item("sWaarde").ToString().Replace(Environment.NewLine, "<br />")
    '                    liAdditionalInformation.Visible = True
    '                    liAdditionalInformationTab.Visible = True
    '            End Select
    '        Next

    '        ART.dr = ART.sArtikelByID(iArtikelID)
    '        If ART.dr IsNot Nothing Then
    '            Title = sArtikel
    '            MetaDescription = sDescription
    '            ltlPrijs.Text = ART.dr.Item("iPrijs")
    '            ltlOldPrijs.Text = ART.dr.Item("iPrijs")
    '            ' ltlDeliveryTime.Text = ART.dr.Item("iLevertijd")
    '            'ltlArtikelCode.Text = ART.dr.Item("sArtikelCode")

    '            ltlArtikel.Text = sArtikel.Replace(Environment.NewLine, "<br/>")
    '            ltlDescription.Text = sDescription.Replace(Environment.NewLine, "<br/>")
    '            ltlProductDescription.Text = sProductDescription.Replace(Environment.NewLine, "<br/>")
    '            ltlDocumentation.Text = sDocumentation.Replace(Environment.NewLine, "<br/>")
    '            ltlAdditionalInformation.Text = sAdditionInformation.Replace(Environment.NewLine, "<br/>")
    '            'Voorraad controle




    '            Dim bWinkelwagen As Boolean = True
    '            Dim AV As New clsArtikelVarianten
    '            AV.dt = AV.sArtikelVariantenBeschikbaar(iArtikelID)

    '            repArtikelVarianten.DataSource = AV.dt
    '            repArtikelVarianten.DataBind()

    '            If AV.dt.Rows.Count > 0 Then
    '                AV.dr = AV.dt.Rows(0)
    '                Dim iBeschikbaar As Integer = AV.scBeschikbaar(AV.dr.Item("iArtikelVariantID"))
    '                If iBeschikbaar > 0 Then
    '                Else
    '                    'ltlDeliveryTime.Text = "<span style='color:#f00;'>Niet op voorraad</span>"
    '                End If
    '                hidArtikelVariantID.Value = AV.dr.Item("iArtikelVariantID")
    '            Else
    '                ' ltlDeliveryTime.Text = "<span style='color:#f00;'>Niet op voorraad</span>"
    '                hidArtikelVariantID.Value = "0"
    '            End If


    '            'Korting tonen ja / nee
    '            Dim dKorting As Decimal = 0
    '            Dim dPrijs As Decimal = CDec(ART.dr.Item("iPrijs")) ' .ToString().Replace(".", ",")
    '            Try
    '                If IsNumeric(ART.dr.Item("sKorting")) Then
    '                    dKorting = CDec(ART.dr.Item("sKorting")) ' .ToString().Replace(".", ",")

    '                    If dKorting <> 0 Then
    '                        divDiscount.Visible = True
    '                        dPrijs = dPrijs - dKorting
    '                        ltlNewPrice.Text = dPrijs
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                Dim LOG As New clsLog
    '                LOG.iLog(Page.ToString(), "page_load", "Berekenen van korting")
    '            End Try



    '            Dim bPrijsTonenIngelogd As Boolean = ART.dr.Item("bIngelogd")
    '            Dim bPrijsTonenUitgelogd As Boolean = ART.dr.Item("bUitgelogd")

    '            Dim bIsOnline As Boolean = IsOnline()

    '            Dim bPrijsTonen As Boolean = True
    '            If bPrijsTonenIngelogd = False And bIsOnline = True Then
    '                bPrijsTonen = False
    '            End If
    '            If bPrijsTonenUitgelogd = False And bIsOnline = False Then
    '                bPrijsTonen = False
    '            End If

    '            If bPrijsTonen Then
    '                If dKorting = 0 Then
    '                    spanPrice.Visible = True
    '                End If
    '                divRequest.Visible = False
    '            Else
    '                divRequest.Visible = True
    '            End If

    '            If bWinkelwagen = False Or bPrijsTonen = False Then
    '                'Offerte aanvragen
    '                Dim sOffertePage As String = P.sPageUrlByGuid(sLanguage, "2181c6e0-0bd4-4ad0-9bea-1114d69ed16c")
    '                aProductLink.HRef = sOffertePage & "?product=" & sArtikel
    '                aProductLink.Target = "_blank"
    '                aProductLink.Attributes.Add("data-buy", "quotation")
    '                ltlLink.Text = Resources.Resource.quotation
    '            Else  'Toevoegen aan winkelwagen
    '                ltlLink.Text = Resources.Resource.BtnAddToCart
    '                aProductLink.Attributes.Add("data-buy", "cart")
    '            End If






    '            Dim IMG As New clsImages
    '            IMG.dt = IMG.sImageByIDAndSoort(iArtikelID, "Foto", "Artikel")
    '            If IMG.dt.Rows.Count > 0 Then
    '                IMG.dr = IMG.dt.Rows(0)
    '                repPictures.DataSource = IMG.dt
    '                repPictures.DataBind()
    '                If IMG.dt.Rows.Count > 1 Then
    '                    repThumbs.DataSource = IMG.dt
    '                    repThumbs.DataBind()
    '                Else
    '                    repThumbs.Visible = False
    '                End If
    '            End If
    '            IMG.dt = IMG.sImageByIDAndSoort(iArtikelID, "Foto groot", "Artikel")
    '            'If IMG.dt.Rows.Count > 0 Then
    '            '    IMG.dr = IMG.dt.Rows(0)
    '            '    repPicturesBig.DataSource = IMG.dt
    '            '    repPicturesBig.DataBind()
    '            'End If

    '            IMG.dt = IMG.sImageByIDAndSoort(iArtikelID, "Facebook", "Artikel")
    '            If IMG.dt.Rows.Count > 0 Then
    '                IMG.dr = IMG.dt.Rows(0)
    '                Dim og_Image As New HtmlMeta
    '                og_Image.Attributes.Add("property", "og:image")
    '                og_Image.Content = IMG.dr.Item("sOriginal").Replace("~/", sURL())
    '                Header.Controls.Add(og_Image)
    '            End If

    '            'repImagesBig.DataSource = lastImages
    '            'repImagesBig.DataBind()


    '            Dim og_Description As New HtmlMeta
    '            og_Description.Attributes.Add("property", "og:description")
    '            og_Description.Content = sDescription
    '            Header.Controls.Add(og_Description)

    '            Dim og_Title As New HtmlMeta
    '            og_Title.Attributes.Add("property", "og:title")
    '            og_Title.Content = sArtikel
    '            Header.Controls.Add(og_Title)

    '            'Dim TA As New clsTussArtikelen
    '            'TA.dt = TA.sTussArtikelenJoinArtikelenWithRoutes(iArtikelID, iTaalID, "artikel")
    '            'If TA.dt.Rows.Count > 0 Then
    '            '    repRelated.DataSource = TA.dt
    '            '    repRelated.DataBind()
    '            'End If
    '        Else
    '            Response.Redirect(responseBack)
    '        End If

    '        aBack.HRef = P.sPageUrlByGuid(sLanguage, "fbe94eda-faa1-45c5-a89a-0371ff4f8d68")
    '    End If
    'End Sub

    'Dim iCount2 As Long = 0
    'Protected Sub repBetaalmethodes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelVarianten.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim rb As HtmlInputRadioButton = e.Item.FindControl("rb")
    '        rb.ID = "rb" & DataBinder.Eval(e.Item.DataItem, "sWaarde")
    '        If iCount2 = 0 Then
    '            hidArtikelVariantID.Value = DataBinder.Eval(e.Item.DataItem, "iArtikelVariantID")
    '            rb.Checked = True
    '        End If
    '        iCount2 = iCount2 + 1
    '    End If
    'End Sub

    '<WebMethod>
    'Public Shared Function iFacRgl(iArtikelID As Long, iArtikelVariantID As Long, iAantal As Long) As Object
    '    Dim R As New oResponse
    '    R.type = "winkelwagen"

    '    Dim sSession As String = HttpContext.Current.Request.Cookies("SessieID").Value
    '    If iAantal < 1 Then
    '        R.code = 0
    '        R.msg = "Required field quantity."
    '        Return R
    '    End If

    '    If IsNumeric(iAantal) = False Then
    '        R.code = 0
    '        R.msg = "Required field quantity."
    '        Return R
    '    End If

    '    'If CInt(iArtikelVariantID) < 1 Then
    '    '    R.code = 0
    '    '    R.msg = "Required field size."
    '    '    Return R
    '    'End If

    '    Dim AV As New clsArtikelVarianten
    '    Dim FR As New clsFacRgl
    '    Dim BP As New BasePage
    '    Dim dt As New DataTable
    '    dt = AV.sArtikelVariantByID(iArtikelVariantID)
    '    If dt.Rows.Count > 0 Then
    '        Dim dr As DataRow = dt.Rows(0)
    '        FR.iArtikelInFacRgl(iArtikelID, iArtikelVariantID, sSession, iAantal, BP.iPartijIDBeheerder, dr.Item("sWaarde"))

    '        R.maat = dr.Item("sWaarde")
    '        R.code = 1
    '        R.msg = "Product added to shopping cart."
    '    Else
    '        R.code = 0
    '        R.msg = "Product not found."
    '    End If

    '    Dim iAantalArtikelen As String = CInt(FR.scAantalArtikelen_Session(sSession, BP.iPartijIDBeheerder, "artikel"))
    '    If CInt(iAantal) > 0 Then
    '        R.aantal = iAantalArtikelen
    '    Else
    '        R.aantal = "0"
    '    End If

    '    Return R
    'End Function

    'Dim lastImages As New ArrayList()
    'Private Sub repPictures_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repPictures.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then


    '        'Dim i As String = e.Item.ItemIndex

    '        'If i >= 3 Then
    '        '    e.Item.Visible = False
    '        '    'Dim div As HtmlGenericControl = e.Item.FindControl("divItem")
    '        '    'div.Visible = False

    '        '    Dim Original As String = DataBinder.Eval(e.Item.DataItem, "sOriginal").Replace("~/", sURL()).Replace("Original", "Minified")
    '        '    lastImages.Add(New PositionData(Original))
    '        'End If
    '    End If
    'End Sub

    'Private Sub repRelated_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repRelated.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
    '        Dim aFollow As HtmlAnchor = e.Item.FindControl("aFollow")
    '        aFollow.HRef = "/" & DataBinder.Eval(e.Item.DataItem, "sURL").ToString.Replace("~/", "")
    '        Dim aNoFollow As HtmlAnchor = e.Item.FindControl("aNoFollow")
    '        aNoFollow.HRef = "/" & DataBinder.Eval(e.Item.DataItem, "sURL").ToString.Replace("~/", "")
    '    End If
    'End Sub

    'Public Class PositionData
    '    Private sOriginal As String

    '    Public Sub New(Original As String)
    '        sOriginal = Original
    '    End Sub

    '    Public ReadOnly Property Original() As String
    '        Get
    '            Return sOriginal
    '        End Get
    '    End Property
    'End Class
End Class
