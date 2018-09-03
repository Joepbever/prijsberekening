Public Class clsAuth

    Dim V As New clsValidatie
    Dim P As New clsPage

    Public email As String
    Public password As String
    Public username As String
    Public url As String
    Public user As MembershipUser
    Public err As String

    Public Sub New(txtemail As TextBox, txtpassword As TextBox, sLanguage As String)
        email = txtemail.Text.Trim()
        password = txtpassword.Text.Trim()
        username = Membership.GetUserNameByEmail(txtemail.Text.Trim())
        If username IsNot Nothing Then
            user = Membership.GetUser(username)
        Else
            username = txtemail.Text.Trim()
            user = Membership.GetUser(username)
        End If
        url = P.sPageUrlByGuid(sLanguage, "16180f45-9a77-4347-8d7f-52cdea726ed4")
    End Sub

    Public Function Login() As Boolean
        If Membership.ValidateUser(username, password) Then
            FormsAuthentication.SetAuthCookie(username, False)
            Return True
        End If

        err = Resources.Resource.ErrorLoginInvalid
        Return False
    End Function

    'Public Function checkForExclusion() As Boolean
    '    If username IsNot "" Then
    '        Dim P As New clsPersonen
    '        P.dt = P.sEcludedPersoonByEmail(email)

    '        If P.dt.Rows.Count > 0 Then
    '            P.dr = P.dt.Rows(0)
    '            Dim exclusionData = DateTime.Parse(P.dr.Item("dDate"))

    '            If exclusionData > DateTime.Now() Then
    '                err = "Account is excluded."
    '                Return False
    '            Else
    '                If username IsNot Nothing Then
    '                    user.IsApproved = True
    '                    Membership.UpdateUser(user)
    '                    P.uUnexcludePersoon(P.dr.Item("iPersID"))
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return True
    'End Function

    Public Function ValidateLogin() As Boolean

        If username Is Nothing Then
            err = Resources.Resource.ErrorLoginNotFound
            Return False
        End If

        Dim bOk As Boolean = True

        If email = "" Then bOk = False
        If password = "" Then bOk = False

        If bOk = False Then
            err = Resources.Resource.ErrorLoginRequierdField
            Return False
        End If

        Return True
    End Function
End Class
