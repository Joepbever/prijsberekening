
Partial Class _Default
    Inherits BasePage

    Dim LI As New clsLijstItems
    Dim P As New clsPage
    Dim PI As New clsPageItems


    Dim iTaalID As Long
    Public sLanguage, sQs, sParentPage As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            iPartijID = sPartijID()
            sQs = RouteData.Values("page").ToString()

            PI.sPI(Page, sLanguage, sQs, False, False, True)
            InitializeComponents()
        End If
    End Sub

    Private Sub InitializeComponents()

    End Sub

End Class