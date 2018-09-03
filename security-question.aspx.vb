
Partial Class ACC_securityquestion
    Inherits BasePage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        InitPage()

    End Sub
    Public Sub btnSecurityQuestion_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim newAnswer As String = txtAnswer.Text
        Dim newQuestion As String = ddlQuestions.Text

        If newAnswer.Length > 100 Then
            ltlError.Text = "Answer should contain 1-100 characters"
            Exit Sub
        ElseIf newAnswer.Length < 0 Then
            ltlError.Text = "Answer should contain 1-100 characters"
            Exit Sub
        End If

        Dim user As MembershipUser = Membership.GetUser()
        Dim result = user.ChangePasswordQuestionAndAnswer(user.GetPassword(), newQuestion, newAnswer)

        If result = True Then
            Response.Redirect("/ChangesSaved.aspx")
        End If

    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)

        Response.Redirect("/")

    End Sub
End Class
