Imports System.Data

Partial Class ACC_forgot_password
    Inherits BasePage

    Dim P As New clsPage
    Dim PI As New clsPageItems
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

        If Page.IsPostBack = False Then
            iPartijID = sPartijID()
            sQs = RouteData.Values("page").ToString()

            PI.sPI(Page, sLanguage, sQs, False, False, True)

            aLogin.HRef = P.sPageUrlByGuid(sLanguage, "c3c09cfd-d57c-4dd2-9874-1db863cb7d01")


        End If
    End Sub

    Protected Sub btnRetrievePassword_Click(sender As Object, e As EventArgs) Handles btnRetrievePassword.Click
        Dim sName As String = U.Config("name")
        Dim V As New clsValidatie
        Dim bOk As Boolean = True
        Dim username As String = txtEmail.Text

        If Trim(username) = "" Then bOk = False

        If bOk = False Then
            ltlError.Text = "The username can only consist of alphabetical, number and underscore."
            divError.Visible = True
            Exit Sub
        End If

        Dim Users As MembershipUserCollection = Membership.FindUsersByEmail(username)
        If Users.Count < 1 Then
            ltlError.Text = Resources.Resource.UserWithUsername & " " & Server.HtmlEncode(username) & " " & Resources.Resource.userNotInSystem
            divError.Visible = True
            Exit Sub
        End If

        Dim sGegevens As String = ""
        Dim sEmail As String = ""
        Dim sUserID As String = ""
        Dim iPersID As Long = 0
        Dim sVoornaam As String = ""
        For Each u As MembershipUser In Users
            sUserID = u.ProviderUserKey.ToString()
        Next

        Dim ACC As New clsAccounts
        ACC.dt = ACC.sAccountByUserID(sUserID, iPartijIDBeheerder)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
            Dim PER As New clsPersonen
            PER.dt = PER.sPersoonByPersID(iPersID)
            If PER.dt.Rows.Count > 0 Then
                PER.dr = PER.dt.Rows(0)
                sEmail = PER.dr.Item("sEmail")
                sVoornaam = PER.dr.Item("sVoorletters")
            End If
        Else
            ltlError.Text = "Account is niet bekend."
            divError.Visible = True
            Exit Sub
        End If


        Dim M As New clsMail
        Dim LI As New clsLijstItems
        LI.dt = LI.sLijstItemByTitleAndType("Wachtwoord vergeten", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)

            Dim sMsg As String = LI.dr.Item("sDescription")
            Dim sSubject As String = LI.dr.Item("sSubTitle")
            sMsg = sMsg.Replace("[wachtwoord]", sGegevens)
            sMsg = sMsg.Replace("[naam]", sVoornaam)

            Dim SSO As New clsSSO
            Dim guid As Guid = Guid.NewGuid()
            Dim iTicks As Long = DateTime.Now.Ticks
            SSO.iSSO(iPartijIDBeheerder, sUserID, guid.ToString(), "", Now, True, iTicks)

            Dim U As New clsUtility
            Dim sId As String = U.Encrypt(iTicks.ToString())

            sMsg = sMsg.Replace("[url]", sURL().Trim().Remove(sURL().Length - 1) & P.sPageUrlByGuid(sLanguage, "7bf88b46-d68c-4890-8d65-bd3d7a9ae171") & "?guid=" & guid.ToString() & "&i=" & iTicks & "&id=" & sId)

            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", txtEmail.Text)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", txtEmail.Text)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", txtEmail.Text)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", txtEmail.Text)

            M.iscMail(0, iPartijIDBeheerder, "versturen", LI.dr.Item("sTitle"), "", sVan, sNaar, sCC, sBCC, LI.dr.Item("sSubTitle"), sMsg, "", "", sName, Now)

            Response.Redirect(P.sPageUrlByGuid(sLanguage, "df535ba9-5c97-4e74-a221-c911a6e62996") & "?from=password")
        Else
            ltlError.Text = Resources.Resource.defaultError
            divError.Visible = True
        End If
    End Sub
End Class