
Imports System.Web.Routing

Partial Class Autoprocessen_Routes
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Request.QueryString("id") <> "sUlKriojwse3800Vq12!-" Then
            Exit Sub
        End If

        'type
        'language
        'page
        'subpage


        Dim routes = RouteTable.Routes
        Using routes.GetWriteLock()
            'Dim defaultRoute = routes.Last()
            'routes.Remove(defaultRoute)
            routes.Clear()

            RouteConfig.RegisterRoutes(routes)
            'get last route (default).  ** by convention, it is the standard route.

            'add some new route for a cms page
            'routes.MapPageRoute(yadayada)
            'Dim IndexConstraints As New RouteValueDictionary() From {
            '    {"language", "nl"},
            '    {"page", "alibc"}
            '}
            'routes.MapPageRoute("alib", "alibc", "~/route.aspx", False, IndexConstraints, Nothing, New RouteValueDictionary(New With {.account = "1234", .subaccount = "5678"}))


            'Dim PageConstraints As New RouteValueDictionary() From {
            '    {"parentpage", Trim(P.drPage.Item("sParentPage")).ToLower()},
            '    {"language", Trim(P.drPage.Item("sCode")).ToLower()},
            '    {"page", Trim(P.drPage.Item("sQueryString")).ToLower()}
            '}

            '            routes.MapPageRoute("ExpenseDetailRoute", "ExpenseReportDetail/{locale}/{year}/{*queryvalues}", "~/expenses.aspx", False, New RouteValueDictionary(New With {.locale = "US", .year = DateTime.Now.Year.ToString(), .asdas = "asdsad"}),
            '        New RouteValueDictionary(New With
            '            {.locale = "[a-z]{2}", .year = "\d{4}"}),
            ')
            'add back default route
            'routes.Add(defaultRoute)
        End Using
    End Sub
End Class
