
Partial Class _Default
    Inherits BasePage

    Dim LI As New clsLijstItems
    Dim P As New clsPage

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)
        Dim iLijstItemID As Long = RouteData.DataTokens("iLijstItemID")

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            If iLijstItemID = 0 Then
                LI.dt = LI.sLijstItemsByTypeOrderByDate("Blog", iTaalID, iPartijIDBeheerder, True)
            Else
                LI.dt = LI.sLijstItemsByTypeAndCategoryOrderByDate("Blog", iTaalID, iPartijIDBeheerder, iLijstItemID)
            End If
            If LI.dt.Rows.Count > 0 Then
                repBlog.DataSource = LI.dt
                repBlog.DataBind()
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

        End If
    End Sub

    Private Sub repBlog_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repBlog.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iLijstItemID As Long = DataBinder.Eval(e.Item.DataItem, "iLijstItemID")

            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            Dim aLink2 As HtmlAnchor = e.Item.FindControl("aLink2")
            Dim sQueryString As String = DataBinder.Eval(e.Item.DataItem, "sQueryString")
            Dim sAfbeelding As String = DataBinder.Eval(e.Item.DataItem, "sAfbeelding")
            If Not IsDBNull(DataBinder.Eval(e.Item.DataItem, "sQueryString2")) Then
                Dim sQueryString2 As String = DataBinder.Eval(e.Item.DataItem, "sQueryString2")
                aLink.HRef = "/" & sLanguage.ToLower() & "/" & sQueryString2 & "/" & sQueryString & "/" & sAfbeelding
                aLink2.HRef = "/" & sLanguage.ToLower() & "/" & sQueryString2 & "/" & sQueryString & "/" & sAfbeelding
            Else
                aLink.HRef = "/" & sLanguage.ToLower() & "/" & sQueryString & "/" & sAfbeelding
                aLink2.HRef = "/" & sLanguage.ToLower() & "/" & sQueryString & "/" & sAfbeelding
            End If
            Dim IMG As New clsImages

            'IMG.dt = IMG.sImageByIDAndSoort(iLijstItemID, "Thumb", DataBinder.Eval(e.Item.DataItem, "sType"))
            'If IMG.dt.Rows.Count > 0 Then
            '    IMG.dr = IMG.dt.Rows(0)
            '    Dim Thumb As HtmlImage = e.Item.FindControl("imgThumb")
            '    Thumb.Src = IMG.dr.Item("sSmall").Replace("~/", sURL())
            '    Thumb.Alt = IMG.dr.Item("sData")
            'Else
            '    e.Item.Visible = False
            'End If
        End If
    End Sub
End Class