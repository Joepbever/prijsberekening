
Partial Class MailTemplates_Mail
    Inherits System.Web.UI.Page


    Dim BP As New BasePage
    Dim iTaalID As Long = 0
    Dim sLanguage As String = BP.sDefaultLanguage.ToUpper

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim iFacKopID As Long = 0

        If Request.QueryString("lang") IsNot Nothing Then
            sLanguage = Request.QueryString("lang").ToString().ToUpper
        End If
        If Request.QueryString("iFacKopID") IsNot Nothing Then
            iFacKopID = Request.QueryString("iFacKopID")
        End If
        If Request.QueryString("iTaalID") IsNot Nothing Then
            iTaalID = Request.QueryString("iTaalID")
        End If
        Dim PI As New clsPageItems
        PI.sPI(Page, sLanguage, "Algemeen", True, False, False)

        Dim PT As New clsPartijen
        'Partij gegevens ophalen
        PT.sPartij(BP.iPartijIDBeheerder)
        '    Dim sUrl As String = BP.sURL()
        '  Dim P As New clsPage
        ltlInfo.Text = PT.sPartijGegevens
        Dim U As New clsUtility
        'imgLogo.Src = PT.sLogo.Replace("~/", U.Config("UrlCms"))
        'img1.Src = PT.sLogo.Replace("~/", U.Config("UrlCms"))
        '  imgLogo2.Src = imgLogo.Src
        aWebsite.HRef = PT.sWebsite
        'aWebsite.InnerHtml = PT.sBedrijfsnaam

        'If iFacKopID <> 0 Then
        '    Dim FR As New clsFacRgl

        '    repFacRgls.DataSource = FR.sFacRglsByFacKopID(iFacKopID, "artikel")
        '    repFacRgls.DataBind()
        'Else
        '    repFacRgls.Visible = False
        'End If

        'aAnnulering.HRef = sUrl.Substring(0, sUrl.Length - 1) & P.sPageUrlByGuid(sLanguage, "4e9a68aa-2ae1-4f82-be7a-f20227ac93ad")
        'aAnnulering.InnerHtml = P.sPageTitle
        'aVertraging.HRef = sUrl.Substring(0, sUrl.Length - 1) & P.sPageUrlByGuid(sLanguage, "0f90a364-9066-4b85-9f04-88b188dd4216")
        'aVertraging.InnerHtml = P.sPageTitle
        'aInstapweigering.HRef = sUrl.Substring(0, sUrl.Length - 1) & P.sPageUrlByGuid(sLanguage, "e9a8004e-34ef-4d34-8633-ce2cfdb2984e")
        'aInstapweigering.InnerHtml = P.sPageTitle

        'templateHeader.Style.Add("background-image", sUrl & "Uploads/Editor/background.jpg")
        'imgFacebook.Src = sUrl & "Resources/img/social_fb.png"
        'imgLinkedIn.Src = sUrl & "Resources/img/social_li.png"

        If PT.sTwitter = "" Then
            aTwitter.Visible = False
        Else
            aTwitter.HRef = PT.sTwitter
        End If

        If PT.sFacebook = "" Then
            aFacebook.Visible = False
        Else
            aFacebook.HRef = PT.sFacebook
        End If

        If PT.sInstagram = "" Then
            aInstagram.Visible = False
        Else
            aInstagram.HRef = PT.sInstagram
        End If

        If PT.sGoogle = "" Then
            aGoogle.Visible = False
        Else
            aGoogle.HRef = PT.sGoogle
        End If

        If PT.sLinkedIn = "" Then
            aLinkedIn.Visible = False
        Else
            aLinkedIn.HRef = PT.sLinkedIn
        End If

    End Sub

    'Dim ROUTE As New clsRoutes
    'Dim IMG As New clsImages
    'Private Sub repFacRgls_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repFacRgls.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
    '        Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
    '        ROUTE.dt = ROUTE.sRoutes("artikel", iArtikelID, iTaalID)
    '        If ROUTE.dt.Rows.Count > 0 Then
    '            ROUTE.dr = ROUTE.dt.Rows(0)
    '            aLink.HRef = ROUTE.dr.Item("sURL").ToString().Replace("~/", BP.sURL() & sLanguage & "/")
    '        Else
    '            aLink.HRef = "#"
    '        End If
    '        IMG.dt = IMG.sImageByTussTypeAndID("Artikel", iArtikelID)
    '        If IMG.dt.Rows.Count > 0 Then
    '            For Each IMG.dr In IMG.dt.Rows
    '                Select Case IMG.dr.Item("sSoort").ToString().ToLower()
    '                    Case "foto"
    '                        Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
    '                        imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", BP.sURL())
    '                        imgThumb.Alt = IMG.dr.Item("sData")
    '                End Select
    '            Next
    '        End If
    '    End If
    'End Sub
End Class
