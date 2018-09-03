
Partial Class Registreren
    Inherits Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Title = "500 - Internal server error."
        End If
    End Sub
End Class
