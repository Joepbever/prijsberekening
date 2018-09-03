
Partial Class _Nieuws
    Inherits BasePage
    Public sLanguage, sQs, sParentPage, sDomain As String
    Dim iTaalID As Long
    Dim P As New clsPage
    Dim IMG As New clsImages
    Dim LI As New clsLijstItems

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        sDomain = sURL()
        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

            sBreadcrumbs(ltlBreadCrumps)

            LI.dt = LI.sLijstItemsByTypeOrderByDate("Nieuws", iTaalID, iPartijIDBeheerder, True)
            If LI.dt.Rows.Count > 0 Then
                repNieuws.DataSource = LI.dt
                repNieuws.DataBind()
            End If

            LI.dt = LI.sLijstItemsByTypeOrderByDate("Nieuws", iTaalID, iPartijIDBeheerder, True)
            If LI.dt.Rows.Count > 0 Then
                repNieuwsIso.DataSource = LI.dt
                repNieuwsIso.DataBind()
            End If


            repCategorie.DataSource = LI.sLIByTypeAndTaal("Nieuws categorie", iTaalID, iPartijIDBeheerder, False)
            repCategorie.DataBind()
        End If

    End Sub

    Private Sub repNieuws_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repNieuws.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iLijstItemID As Long = DataBinder.Eval(e.Item.DataItem, "iLijstItemID")
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            Dim sAfbeelding As String = DataBinder.Eval(e.Item.DataItem, "sAfbeelding")
            If sParentPage IsNot Nothing Then
                aLink.HRef = "/" & sParentPage & "/" & sQs & "/" & sAfbeelding
            Else
                aLink.HRef = "/" & sQs & "/" & sAfbeelding
            End If
            IMG.dt = IMG.sImageByIDAndSoort(iLijstItemID, "Overzicht", DataBinder.Eval(e.Item.DataItem, "sType"))
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                Dim Desktop As HtmlImage = e.Item.FindControl("imgDesktop")
                Desktop.Src = IMG.dr.Item("sSmall").Replace("~/", sDomain)
                Desktop.Alt = IMG.dr.Item("sData")
            Else
                e.Item.Visible = False
            End If
        End If
    End Sub

    Private Sub repNieuwsIso_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repNieuwsIso.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iLijstItemID As Long = DataBinder.Eval(e.Item.DataItem, "iLijstItemID")
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            Dim sAfbeelding As String = DataBinder.Eval(e.Item.DataItem, "sAfbeelding")
            If sParentPage IsNot Nothing Then
                aLink.HRef = "/" & sParentPage & "/" & sQs & "/" & sAfbeelding
            Else
                aLink.HRef = "/" & sQs & "/" & sAfbeelding
            End If
            IMG.dt = IMG.sImageByIDAndSoort(iLijstItemID, "Overzicht", DataBinder.Eval(e.Item.DataItem, "sType"))
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                Dim Desktop As HtmlImage = e.Item.FindControl("imgDesktop")
                Desktop.Src = IMG.dr.Item("sSmall").Replace("~/", sDomain)
                Desktop.Alt = IMG.dr.Item("sData")
            Else
                e.Item.Visible = False
            End If
        End If
    End Sub
End Class