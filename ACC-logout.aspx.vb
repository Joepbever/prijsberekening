
Partial Class uitloggen
    Inherits System.Web.UI.Page

    Dim BP As New BasePage
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        Dim sLanguage As String = Page.RouteData.Values("language")
        Dim P As New clsPage

        Dim AA As New clsAccountsActivity
        AA.iActivity(BP.iPartijIDBeheerder, BP.sPartijID(), DateTime.Now, DateTime.Now, "Logout")

        Response.Redirect(P.sPageUrlByGuid(sLanguage, "941fdbf3-c305-4a1a-8020-e9aaa28d3294"))
    End Sub
End Class
