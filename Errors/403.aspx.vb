
Partial Class Registreren
    Inherits Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Title = "403 - Forbidden: Access is denied."
        End If
    End Sub
End Class
