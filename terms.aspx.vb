
Partial Class _Default
    Inherits BasePage
    Dim LI As New clsLijstItems
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim sQs As String = RouteData.Values("page").ToString()
            Dim sCode As String = "nl"

            Dim PI As New clsPageItems
            PI.sPI(Me.Page, sCode, sQs, False, False, True)

            iTaalID = sTaalID(sCode)

            LI.dt = LI.sLIByTypeAndTaal("Voorwaarden", iTaalID, iPartijIDBeheerder, False)
            If LI.dt.Rows.Count > 0 Then
                repTerms.DataSource = LI.dt
                repTerms.DataBind()

                repTermsScrollSpy.DataSource = LI.dt
                repTermsScrollSpy.DataBind()
            End If
        End If
    End Sub
End Class