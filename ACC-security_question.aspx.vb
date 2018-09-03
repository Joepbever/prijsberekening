
Partial Class ACC_security_question
    Inherits BasePage

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        onlineCheck()


        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            InitializeComponents()

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage) 'literals vullen

            Dim P As New clsPage
            P.dt = P.sPagesByActief(sLanguage, "Account", 0, True, iPartijIDBeheerder)
            repMenu.DataSource = P.dt
            repMenu.DataBind()

        End If
    End Sub

    Private Sub InitializeComponents()
        Dim LI As New clsLijstItems
        LI.dt = LI.sLIByTaalIDAndType("Security question", iTaalID)

        ddlQuestions.DataSource = LI.dt
        ddlQuestions.DataValueField = "sTitle"
        ddlQuestions.DataTextField = "sTitle"
        ddlQuestions.DataBind()
    End Sub

    Public Sub btnSecurityQuestion_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim newAnswer As String = txtAnswer.Text
        Dim password As String = txtPassword.Text.Trim()
        Dim newQuestion As String = ddlQuestions.SelectedValue
        Dim questionList As New List(Of String)
        Dim validQuestion As Boolean

        For Each question In ddlQuestions.Items
            questionList.Add(question.ToString())
        Next

        For Each questionInList In questionList
            If questionInList = newQuestion Then
                validQuestion = True
            End If
        Next

        If validQuestion = True Then
            If newAnswer.Length > 100 Then
                ltlError.Text = Resources.Resource.ErrorAnswer
                Exit Sub
            ElseIf newAnswer.Length < 0 Then
                ltlError.Text = Resources.Resource.ErrorAnswer
                Exit Sub
            End If

            Dim mu As MembershipUser = Membership.GetUser(True)
            Dim result = mu.ChangePasswordQuestionAndAnswer(password, newQuestion, newAnswer)

            If result = True Then
                Dim P As New clsPage
                Response.Redirect(P.sPageUrlByGuid(sLanguage, "30603b0f-56d2-40ce-8126-5f3a3e8a0917"))
            Else
                ltlError.Text = Resources.Resource.ErrorWachtwordInvalid
            End If
        End If
    End Sub
End Class
