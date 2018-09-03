
Partial Class _Default
    Inherits BasePage
    Dim LI As New clsLijstItems
    Dim PI As New clsPageItems
    Dim IMG As New clsImages
    Dim P As New clsPage

    Dim iTaalID As Long
    Public sQs, sLanguage, sType, sDomain As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sQs = RouteData.Values("page")
        Dim responseBack As String = "/" & sQs

        If RouteData.Values("parentpage") IsNot Nothing Then
            responseBack = "/" & RouteData.Values("parentpage") & responseBack
        End If
        Dim iLijstItemID As Long = RouteData.DataTokens("iLijstItemID")
        sLanguage = RouteData.Values("language")
        'responseBack = "/" & sLanguage & responseBack

        sType = "Blog" 'HIER TYPE AANPASSEN

        sDomain = sURL()
        iTaalID = sTaalID(sLanguage)

        Dim PI As New clsPageItems
        PI.sPI(Page, sLanguage, sQs, False, False, False)

        LI.dt = LI.sLijstItemByID(iLijstItemID)
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)

            ltlTitleBlog.Text = LI.dr.Item("sTitle")
            ltlHtml1.Text = LI.dr.Item("sHtml1")


            'HIER VERDER MEER VARIABELEN VULLEN VANUIT LI.dr.Item("<>")




            Title = LI.dr.Item("sPageTitle")
            MetaDescription = LI.dr.Item("sPageDescription")
            MetaKeywords = LI.dr.Item("sKeywords")

            IMG.dt = IMG.sImageByIDAndSoort(iLijstItemID, "Header", sType)
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                'imgThumb.Src = IMG.dr.Item("sSmall").Replace("~/", sDomain)
                'imgThumb.Alt = IMG.dr.Item("sData")
                ltlImgHeader.Text = "<img src='" & IMG.dr.Item("sSmall").Replace("~/", sDomain) & "' alt='" & IMG.dr.Item("sData") & "' />"
            End If

            Dim og_Image, og_Sitename, og_Description, og_url, og_Title, og_Type As New HtmlMeta
            Dim U As New clsUtility

            'Facebook afbeelding
            IMG.dt = IMG.sImageByIDAndSoort(iLijstItemID, "Facebook", sType)
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                og_Image.Attributes.Add("property", "og:image")
                og_Image.Content = IMG.dr.Item("sSmall").Replace("~/", sDomain)
                Page.Header.Controls.Add(og_Image)
            End If

            'FB title
            og_Title.Attributes.Add("property", "og:title")
            og_Title.Content = LI.dr.Item("sPageTitle")
            Page.Title = LI.dr.Item("sPageTitle")
            Page.Header.Controls.Add(og_Title)

            'FB description
            og_Description.Attributes.Add("property", "og:description")
            og_Description.Content = LI.dr.Item("sPageDescription")
            Page.Header.Controls.Add(og_Description)

            'FB type
            og_Type.Attributes.Add("property", "og:type")
            og_Type.Content = "website"
            Page.Header.Controls.Add(og_Type)

            'FB URL
            og_url.Attributes.Add("property", "og:url")
            og_url.Content = Page.Request.Url.ToString()
            Page.Header.Controls.Add(og_url)

            'FB site name
            og_Sitename.Attributes.Add("property", "og:site_name")
            og_Sitename.Content = U.Config("URL")
            Page.Header.Controls.Add(og_Sitename)
        Else
            Response.Redirect(responseBack)
        End If
    End Sub


End Class