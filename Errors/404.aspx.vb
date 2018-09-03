
Partial Class Registreren
    Inherits Page

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        MyBase.Render(writer)
        Response.TrySkipIisCustomErrors = True
        Response.StatusCode = 404
        Response.StatusDescription = "404 Page Not Found"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Title = "404 - File or directory not found."
        End If
    End Sub
End Class
