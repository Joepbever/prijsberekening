
Partial Class oih_my_activities
    Inherits BasePage

    Dim PI As New clsPageItems


    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Public aantalPages, iPage As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        onlineCheck()

        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)
        iPartijID = sPartijID()

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            PI.sPI(Page, sLanguage, sQs, False, False, True)

            InitializeComponents()
        End If

    End Sub

    Private Sub InitializeComponents()

        Dim P As New clsPage
        P.dt = P.sPagesByActief(sLanguage, "Account", 0, True, iPartijIDBeheerder)
        repMenu.DataSource = P.dt
        repMenu.DataBind()

        Dim AA As New clsAccountsActivity

        AA.dt = AA.sActivityByiPartijID(iPartijID)
        repActivity.DataSource = AA.dt
        repActivity.DataBind()

    End Sub

End Class
