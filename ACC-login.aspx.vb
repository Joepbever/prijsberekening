Partial Class ACC_Login
    Inherits BasePage

    Dim IMG As New clsImages
    Dim LI As New clsLijstItems
    Dim P As New clsPage
    Dim U As New clsUtility

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        'Master.FindControl("sectionBreadcrumps").Visible = False
        aRegister.NavigateUrl = P.sPageUrlByGuid(sLanguage, "27afdc34-d22e-42d0-a334-3f90e764aa6a")
        If Page.IsPostBack = False Then
            If IsOnline() Then
                Response.Redirect(P.sPageUrlByGuid(sLanguage, "16180f45-9a77-4347-8d7f-52cdea726ed4"))
            End If

            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

            'aRegister.HRef = P.sPageUrlByGuid(sLanguage, "cf1bfeec-94ab-4108-887c-ccda35e94f92")
            aForgotPassword.HRef = P.sPageUrlByGuid(sLanguage, "0a3bada1-e811-40ec-a7d4-e822bf5f5676")
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ltlError.Text = ""

        Dim A As New clsAuth(txtEmail, txtLoginPassword, sLanguage)

        If A.ValidateLogin = False Then
            ltlError.Text = A.err
            Exit Sub
        End If

        If A.Login() = False Then
            ltlError.Text = Resources.Resource.ErrorLoginInvalid
            divError.Visible = True
        Else
            Dim url = A.url
            Dim from As String = Request.QueryString("d")
            If from IsNot Nothing Then
                url = Request.QueryString("d")
            End If
            Dim AA As New clsAccountsActivity
            AA.iActivity(iPartijIDBeheerder, iPartijID, DateTime.Now, DateTime.Now, "Login")
            Response.Redirect(url)
        End If

        'ltlError.Text = ""

        ''lastlockedout date langer dan een half uur geleden dan de lock verwijderen

        'Dim V As New clsValidatie
        'Dim bOk As Boolean = True
        'If Trim(txtEmail.Text) = "" Then bOk = False
        'If Trim(txtLoginPassword.Text) = "" Then bOk = False
        'If bOk = False Then
        '    Exit Sub
        'End If
        'If Membership.ValidateUser(txtEmail.Text, txtLoginPassword.Text) Then
        '    FormsAuthentication.SetAuthCookie(txtEmail.Text, False)
        '    Response.Redirect(P.sPageUrlByGuid(sLanguage, "4adb0443-e8d2-4605-a7d1-c12f4302a5db"))
        'Else
        '    ltlError.Text = Resources.Resource.ValidateIncorrectLogin
        '    divError.Visible = True
        'End If
    End Sub
End Class