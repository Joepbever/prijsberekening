
Imports System.Data

Partial Class _Default
    Inherits BasePage

    Dim IMG As New clsImages
    Dim LI As New clsLijstItems
    Dim P As New clsPage
    Dim U As New clsUtility
    Dim PI As New clsPageItems

    Public sLanguage, sQs, sParentPage, sSearchTerm As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            sSearchTerm = Request.QueryString("term").Replace("+", " ")

            ltlSearch.Text = sSearchTerm
            LI.dt = LI.sSearch(iTaalID, iPartijIDBeheerder, "Blog", sSearchTerm)
            If LI.dt.Rows.Count > 0 Then
                repBlogs.DataSource = LI.dt
                repBlogs.DataBind()
            End If

            LI.dt = LI.sSearch(iTaalID, iPartijIDBeheerder, "Klantenservice", sSearchTerm)
            If LI.dt.Rows.Count > 0 Then
                repCustomerService.DataSource = LI.dt
                repCustomerService.DataBind()
            End If

            U.dt = U.sSearch(iTaalID, sLanguage, sSearchTerm)
            If U.dt.Rows.Count > 0 Then
                repProducts.DataSource = U.dt
                repProducts.DataBind()
            End If

            PI.dt = PI.sPageItemsLIKEOutput(iPartijIDBeheerder, sSearchTerm, sLanguage)
            If PI.dt.Rows.Count > 0 Then
                repTexts.DataSource = PI.dt
                repTexts.DataBind()
            End If

        End If
    End Sub

    Protected Sub repTexts_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repTexts.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim aLink As HtmlAnchor = e.Item.FindControl("aLink")
            aLink.HRef = P.sPageUrlByGuid(sLanguage, "6787d76a-001a-44a2-8ebd-fcd9d0daf406")
        End If
    End Sub
End Class