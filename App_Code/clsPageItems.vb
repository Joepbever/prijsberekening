Imports System.Data
Imports System.Data.SqlClient

Public Class clsPageItems
    Dim U As New clsUtility
    Dim IMG As New clsImages

    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sPageItems(sCode As String, sQueryString As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT PageItems.* FROM tussTaalPages INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID INNER JOIN PageItems ON Pages.iPageID = PageItems.iPageID INNER JOIN Talen ON tussTaalPages.iTaalID = Talen.iTaalID WHERE (Talen.sCode = @sCode) And (tussTaalPages.sQueryString = @sQueryString) AND (Talen.iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@sQueryString", sQueryString)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sPageItemsByParent(sCode As String, sQueryString As String, sParentPage As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT PageItems.* FROM tussTaalPages INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID INNER JOIN PageItems ON Pages.iPageID = PageItems.iPageID INNER JOIN Talen ON tussTaalPages.iTaalID = Talen.iTaalID INNER JOIN Pages AS Pages2 ON tussTaalPages.iPageID = Pages2.iPageID INNER JOIN tussTaalPages AS tussTaalPages2 ON Pages2.iParentID = tussTaalPages2.iPageID AND tussTaalPages2.sQueryString = @sParentPage WHERE (Talen.sCode = @sCode) And (tussTaalPages.sQueryString = @sQueryString) AND (Talen.iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@sQueryString", sQueryString)
        cmd.Parameters.AddWithValue("@sParentPage", sParentPage)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function scSubPageItem(sCode As String, sQueryString As String, sSubQueryString As String, sItem As String) As String
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(1) PageItems.sContent FROM Talen INNER JOIN tussTaalPages ON Talen.iTaalID = tussTaalPages.iTaalID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID INNER JOIN Pages AS Pages_1 ON Pages.iPageID = Pages_1.iParentID INNER JOIN tussTaalPages AS tussTaalPages_1 ON Pages_1.iPageID = tussTaalPages_1.iPageID INNER JOIN PageItems ON tussTaalPages_1.iPageID = PageItems.iPageID WHERE (Talen.sCode = @sCode) And (tussTaalPages.sQueryString = @sQs) And (tussTaalPages_1.sQueryString = @sSub) And (PageItems.sItem = @sItem)"
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@sQueryString", sQueryString)
        cmd.Parameters.AddWithValue("@sSubQueryString", sSubQueryString)
        cmd.Parameters.AddWithValue("@sItem", sItem)
        Return CON.Scalar(cmd)
    End Function

    Public Function sPageItemsLIKEOutput(iPartijID As Long, Term As String, sCode As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT PageItems.sOutput, Talen.iPartijID, tussTaalPages.sQueryString, Talen.sCode, tussTaalPages.sText, tussTaalPages.iPageID, tussTaalPages.sGuid FROM tussTaalPages INNER JOIN Talen ON tussTaalPages.iTaalID = Talen.iTaalID INNER JOIN PageItems ON tussTaalPages.iPageID = PageItems.iPageID WHERE (Talen.iPartijID = @iPartijID) AND (PageItems.sOutput LIKE '%' + @Term + '%') AND (Talen.sCode = @sCode) AND tussTaalPages.sQueryString <> 'algemeen'"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@Term", Term)
        cmd.Parameters.AddWithValue("@sCode", sCode)
        Return CON.sDatatable(cmd)
    End Function

    Public Sub sPI(ByVal Mij As Page, ByVal sCode As String, ByVal sQs As String, Optional ByVal bPage As Boolean = False, Optional ByVal bMaster As Boolean = False, Optional ByVal bMetatags As Boolean = False, Optional ByVal sParentPage As String = Nothing)
        Dim KeyWords, Description, Abstract, Robots, Language, Revisit, Author, og_Image, og_Sitename, og_Description, og_url, og_Title, og_Type,
            ios_Title, ms_Tooltip, ms_Title, twitter_Title, twitter_Description, twitter_site, twitter_creator, google_publisher, google_author As New HtmlMeta
        Dim BP As New BasePage

        If sParentPage IsNot Nothing Then
            dt = sPageItemsByParent(sCode, sQs, sParentPage, BP.iPartijIDBeheerder)
        Else
            dt = sPageItems(sCode, sQs, BP.iPartijIDBeheerder)
        End If

        Dim ltl As Literal

        Dim P As New clsPartijen
        P.sPartij(BP.iPartijIDBeheerder)
        If P.dt.Rows.Count > 0 Then
            P.dr = P.dt.Rows(0)
        End If

        Dim ADR As New clsAdressen
        ADR.dt = ADR.sAdresByPartijID(BP.iPartijIDBeheerder)

        For Each Me.dr In dt.Rows
            Select Case dr.Item("sItem").ToLower()
                Case "description"
                    If bMetatags = True Then
                        'FB description
                        og_Description.Attributes.Add("property", "og:description")
                        og_Description.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(og_Description)

                        'Twitter description
                        twitter_Description.Attributes.Add("name", "twitter:description")
                        twitter_Description.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(twitter_Description)

                        'metatag description
                        Description.Name = "description"
                        Description.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(Description)
                    End If
                Case "title"
                    If bMetatags = True Then
                        'page title
                        Mij.Title = dr.Item("sContent")
                        ' Mij.Header.Title = dr.item("sItem").sContent
                        'FB Title
                        og_Title.Attributes.Add("property", "og:title")
                        og_Title.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(og_Title)

                        'iOS Title
                        ios_Title.Attributes.Add("name", "apple-mobile-web-app-title")
                        ios_Title.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(ios_Title)

                        'MS Tooltip
                        ms_Tooltip.Attributes.Add("name", "msapplication-tooltip")
                        ms_Tooltip.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(ms_Tooltip)

                        'MS Title
                        ms_Title.Attributes.Add("name", "msapplication-name")
                        ms_Title.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(ms_Title)

                        'Twitter Title
                        twitter_Title.Attributes.Add("name", "twitter:title")
                        twitter_Title.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(twitter_Title)
                    End If
                Case "keywords"
                    If bMetatags = True Then
                        'metatag keywords
                        KeyWords.Name = "keywords"
                        KeyWords.Content = dr.Item("sContent")
                        Mij.Header.Controls.Add(KeyWords)
                    End If
                Case Else
                    If bPage = True Then
                        ltl = Mij.Page.FindControl(dr.Item("sItem"))
                    ElseIf bMaster = True Then
                        ltl = Mij.Page.Master.FindControl(dr.Item("sItem"))
                    Else
                        ltl = Mij.Page.Master.FindControl("ContentPlaceHolder1").FindControl(dr.Item("sItem"))
                    End If
                    If ltl IsNot Nothing Then
                        ltl.Text = HttpUtility.HtmlDecode(U.ReplaceVariables(dr.Item("sOutput"), Nothing, P.dr, Nothing, Nothing, Nothing, Nothing, ADR.dt))
                    End If
            End Select
        Next

        If sParentPage IsNot Nothing Then
            IMG.dt = IMG.sImageByPageAndParentAndTussType(sQs, "_pages", BP.sTaalID(sCode), sParentPage)
        Else
            IMG.dt = IMG.sImageByPageAndTussType(sQs, "_pages", BP.sTaalID(sCode))
        End If
        Dim sImage As String = ""
        For Each IMG.dr In IMG.dt.Rows
            sImage = "<img src='" & BP.sURL() & IMG.dr.Item("sSmall").ToString().Replace("~/", "") & "' alt='" & IMG.dr.Item("sData") & "' />"
            If bPage = True Then
                ltl = Mij.Page.FindControl("ltlImg" & IMG.dr.Item("sSoort").Replace(" ", ""))
            ElseIf bMaster = True Then
                ltl = Mij.Page.Master.FindControl("ltlImg" & IMG.dr.Item("sSoort").Replace(" ", ""))
            Else
                ltl = Mij.Page.Master.FindControl("ContentPlaceHolder1").FindControl("ltlImg" & IMG.dr.Item("sSoort").Replace(" ", ""))
            End If
            If ltl IsNot Nothing Then
                ltl.Text = sImage
            End If
        Next

        If bMetatags = True Then
            'FB type
            og_Type.Attributes.Add("property", "og:type")
            og_Type.Content = "website"
            Mij.Header.Controls.Add(og_Type)

            'FB Image
            og_Image.Attributes.Add("property", "og:image")
            og_Image.Content = U.Config("URL") & "/Resources/img/meta-icons/1200x630.png"
            Mij.Header.Controls.Add(og_Image)

            'FB URL
            og_url.Attributes.Add("property", "og:url")
            og_url.Content = Mij.Request.Url.ToString()
            Mij.Header.Controls.Add(og_url)

            'FB site name
            og_Sitename.Attributes.Add("property", "og:site_name")
            og_Sitename.Content = U.Config("URL")
            Mij.Header.Controls.Add(og_Sitename)

            'Google analytics
            'Dim google_analytics As New HiddenField
            'google_analytics.ID = "hdfGoogleAnalytics"
            'google_analytics.Value = P.sGoogleAnalytics
            'Mij.Form.Controls.Add(google_analytics)

            'Google+ author tag
            Dim sGoogle As String = P.sGoogle
            If sGoogle <> "" Then
                google_author.Attributes.Add("rel", "author")
                google_author.Attributes.Add("href", "https://plus.google.com/" & sGoogle)
                Mij.Header.Controls.Add(google_author)
            End If

            'Google+ publisher tag
            google_publisher.Attributes.Add("rel", "publisher")
            google_publisher.Attributes.Add("href", "https://plus.google.com/103237995058186185351")
            Mij.Header.Controls.Add(google_publisher)

            'TWITTER site account
            twitter_site.Name = "twitter:site"
            Dim sTwitter As String = P.sTwitter
            If sTwitter <> "" Then
                twitter_site.Content = "@" & sTwitter.Replace("@", "")
            Else
                twitter_site.Content = "@WebSentiment_"
            End If
            Mij.Header.Controls.Add(twitter_site)

            'TWITTER creator account
            twitter_creator.Name = "twitter:creator"
            twitter_creator.Content = "@WebSentiment_"
            Mij.Header.Controls.Add(twitter_creator)

            'metatag revisit-after
            Revisit.Name = "revisit-after"
            Revisit.Content = "30"
            Mij.Header.Controls.Add(Revisit)

            'STATIC METATAGS
            'metatag author
            Author.Name = "author"
            Author.Content = "WebSentiment B.V."
            Mij.Header.Controls.Add(Author)

            'metatag robots
            Robots.Name = "robots"
            Robots.Content = "index, follow"
            Mij.Header.Controls.Add(Robots)

            'metatag language
            Language.Name = "language"
            Language.Content = sCode
            Mij.Header.Controls.Add(Language)
        End If
    End Sub


    Public Function sSubPageMetatags(ByVal Mij As Page, ByVal sCode As String, ByVal sQs As String, ByVal sSubPage As String) As String
        Dim KeyWords, Description, Abstract, Robots, Language, Revisit, Author As New HtmlMeta

        'DYNAMIC METATAGS
        'page title
        Mij.Header.Title = scSubPageItem(sCode, sQs, sSubPage, "title")

        'metatag keywords
        KeyWords.Name = "keywords"
        KeyWords.Content = scSubPageItem(sCode, sQs, sSubPage, "keywords")

        Mij.Header.Controls.Add(KeyWords)

        'metatag description
        Description.Name = "description"
        Description.Content = scSubPageItem(sCode, sQs, sSubPage, "description")
        Mij.Header.Controls.Add(Description)

        'metatag revisit-after
        Revisit.Name = "revisit-after"
        Revisit.Content = scSubPageItem(sCode, sQs, sSubPage, "updatefrequentie")
        Mij.Header.Controls.Add(Revisit)

        'STATIC METATAGS
        'metatag author
        Author.Name = "author"
        Author.Content = "WebSentiment B.V."
        Mij.Header.Controls.Add(Author)

        'metatag robots
        Robots.Name = "robots"
        Robots.Content = "index, follow"
        Mij.Header.Controls.Add(Robots)

        'metatag language
        Language.Name = "language"
        Language.Content = sCode
        Mij.Header.Controls.Add(Language)
        Return True
    End Function
End Class