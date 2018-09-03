
Partial Class _Default
    Inherits BasePage
    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If RouteData.Values("language") IsNot Nothing Then
        '    sLanguage = RouteData.Values("language").ToString().ToUpper
        'Else
        '    sLanguage = sDefaultLanguage.ToUpper
        'End If

        ''iTaalID = sTaalID(sLanguage)

        'If Page.IsPostBack = False Then
        '    sQs = RouteData.Values("page").ToString()

        '    If RouteData.Values("parentpage") IsNot Nothing Then
        '        sParentPage = RouteData.Values("parentpage")
        '    End If

        '    Dim PI As New clsPageItems
        '    PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)
        'End If
    End Sub
End Class
