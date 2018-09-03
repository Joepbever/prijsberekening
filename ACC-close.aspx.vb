
Partial Class ACC_CloseAccount
    Inherits BasePage
    Dim P As New clsPage

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

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

            InitializeComponents()

            aCancel.NavigateUrl = P.sPageUrlByGuid(sLanguage, "16180f45-9a77-4347-8d7f-52cdea726ed4")
        End If

    End Sub

    Private Sub InitializeComponents()
        Dim LI As New clsLijstItems
        LI.dt = LI.sLIByTaalIDAndType("Close account reasons", iTaalID)

        ddlReasons.DataSource = LI.dt
        ddlReasons.DataValueField = "sTitle"
        ddlReasons.DataTextField = "sTitle"
        ddlReasons.DataBind()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim historie As New clsHistorie
        Dim reason As String = ddlReasons.Text

        Dim questionList As New List(Of String)
        Dim validQuestion As Boolean

        For Each question In ddlReasons.Items
            questionList.Add(question.ToString())
        Next

        For Each questionInList In questionList
            If questionInList = reason Then
                validQuestion = True
            End If
        Next

        If validQuestion = True Then
            historie.iHistorie(iPartijID, iPersID, "ClosingReason", 0, "ClosingReason", "ClosingReason", reason, "Deregistered", "ClosingReason")

            Dim user As MembershipUser = Membership.GetUser(True)
            user.IsApproved = False
            Membership.UpdateUser(user)

            FormsAuthentication.SignOut()
            Dim P As New clsPage
            Response.Redirect(P.sPageUrlByGuid(sLanguage, "28dda6f1-3727-4221-82da-d539f62450e1"))
        Else
            ltlError.Text = "That is not one of the reason that we have in our list. Please choose one of the reasons in the list."
        End If
    End Sub
End Class
