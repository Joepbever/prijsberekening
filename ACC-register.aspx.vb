Partial Class _Register
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

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If IsOnline() Then
                Response.Redirect("/")
            End If

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

            InitializeComponents()

        End If
    End Sub

    Private Sub InitializeComponents()
        LI.dt = LI.sLIByTaalIDAndType("Security question", iTaalID)

        ddlSecurity.DataSource = LI.dt
        ddlSecurity.DataValueField = "sTitle"
        ddlSecurity.DataTextField = "sTitle"
        ddlSecurity.DataBind()


        cbTerms.Text = Resources.Resource.formTerms & " " & "<a href='" & P.sPageUrlByGuid(sDefaultLanguage, "df427103-94cb-48d5-a89f-edf58d7c270a") & "' target='_blank'>" & Resources.Resource.TermsConditions & ".</a>"

    End Sub




    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If cbTerms.Checked Then
            Dim V As New clsValidatie

            Dim bOk As Boolean = True
            Dim sFirstName As String = txtFirstName.Text
            Dim sSurname As String = txtSurname.Text
            'Dim sCompany As String = txtCompany.Text
            'Dim sVAT As String = txtVAT.Text
            'Dim sPhone As String = txtPhone.Text
            Dim sEmail As String = txtEmail.Text
            Dim sPassword As String = txtPasswordConfirm.Text
            Dim sUsername As String = txtEmail.Text

            'If Trim(sFirstName) = "" Then bOk = False
            'If Trim(sSurname) = "" Then bOk = False
            ''If Trim(sCompany) = "" Then bOk = False
            ''If Trim(sVAT) = "" Then bOk = False
            ''If Trim(sPhone) = "" Then bOk = False
            'If Trim(sEmail) = "" Then bOk = False
            'If Trim(sPassword) = "" Then bOk = False
            'If Trim(sUsername) = "" Then bOk = False

            If V.ValidatePassword(txtPassword, txtPasswordConfirm) = False Then
                ltlError.Text = V.msg
                divError.Style.Add("display", "block")
                Exit Sub
            End If

            If V.ValidateRegister(txtFirstName, txtSurname, txtEmail) = False Then
                ltlError.Text = V.msg
                divError.Style.Add("display", "block")
                Exit Sub
            End If

            'If Trim(tel) = "" Then bOk = False
            'If Trim(wijkcode) = "" Then bOk = False
            'If Trim(straatcode) = "" Then bOk = False
            'If Trim(nummer) = "" Then bOk = False
            'If Trim(straat) = "" Then bOk = False
            'If Trim(plaats) = "" Then bOk = False

            'Validate Question
            LI.dt = LI.sLIByTaalIDAndTypeAndsTitle("Security question", iTaalID, ddlSecurity.SelectedValue)

            If LI.count() = 0 Then
                ltlError.Text = Resources.Resource.SecurityQuestion
                divError.Style.Add("display", "block")
                Exit Sub
            End If


            Dim PER As New clsPersonen
            Dim PT As New clsPartijen
            Dim ADR As New clsAdressen
            Dim iPersID As Long = 0

            Dim createStatus As MembershipCreateStatus
            Dim newUser As MembershipUser = Membership.CreateUser(sUsername, sPassword, sEmail, ddlSecurity.SelectedValue, txtAnswer.Text, True, createStatus)

            Select Case createStatus
                Case MembershipCreateStatus.Success
                    Dim iPartijID As Long = PT.iscPartij(iPartijIDBeheerder, "", "", "", "", "", "", "", "", Date.Now, "", "", "", "", "", "bedrijf", "actief", "", "", "", "", "", "", "", "", "", "", "", "")

                    'Persoon toevoegen
                    iPersID = PER.iscPersoon("", sFirstName & " " & sSurname, sFirstName, "", sSurname, "", "", "", "", sEmail, "", "", "", False, Date.Now, newUser.ProviderUserKey.ToString(), 0, 0, Date.Now, True, "actief", "", Decimal.Parse("0,00"), iPartijIDBeheerder)

                    'Adres toevoegen aan persoon
                    Dim iAdresID As Long = ADR.iscAdres(iPersID, iPartijID, "factuur", "", "", "", "", "", "", "", "", "", "", "", 0, 0, "", 0, "", "", "", "", iPartijIDBeheerder)
                    Dim iAdresID2 As Long = ADR.iscAdres(iPersID, iPartijID, "lever", "", "", "", "", "", "", "", "", "", "", "", 0, 0, "", 0, "", "", "", "", iPartijIDBeheerder)

                    'Dim sPartijGegevens As String = bedrijfsnaam & "<br />"
                    'sPartijGegevens &= txtStraatnaam.Text & " " & txtNr.Text & " " & txtToev.Text & "<br />"
                    'sPartijGegevens &= txtPostcode.Text & " " & txtPlaatsnaam.Text & "<br />"
                    'sPartijGegevens &= txtProvince.Text & "<br /> Nederland"

                    Dim ACC As New clsAccounts
                    ACC.iscAccount(iPartijID, iPersID, "bedrijf", "actief", Now, Now.Date, newUser.ProviderUserKey.ToString(), iPartijIDBeheerder)

                    Dim TPP As New clsTussPersonenPartijen
                    TPP.iTussPersonenPartijen(iPersID, iPartijID)

                    Dim M As New clsMail
                    M.RegistratieMailVersturen(iPartijIDBeheerder, iTaalID, txtEmail.Text, txtFirstName.Text, txtSurname.Text)

                    FormsAuthentication.SetAuthCookie(sUsername, True)
                Case MembershipCreateStatus.DuplicateUserName
                    ltlError.Text = Resources.Resource.ValidateGebruikersnaamBestaat & sEmail
                    divError.Style.Add("display", "block")
                    'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.GebruikersnaamBestaat & username)
                    Exit Sub
                Case MembershipCreateStatus.DuplicateEmail
                    ltlError.Text = Resources.Resource.ValidateEmailadresBestaat & sEmail
                    divError.Style.Add("display", "block")
                    '' V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.EmailadresBestaat & email)
                    Exit Sub
                Case MembershipCreateStatus.InvalidEmail
                    ltlError.Text = Resources.Resource.ValidateEmailadresNietJuist & sEmail
                    divError.Style.Add("display", "block")
                    'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.EmailadresNietJuist & email)
                    Exit Sub
                Case MembershipCreateStatus.InvalidAnswer
                    ltlError.Text = Resources.Resource.ValidateBeveiligingsantwoordNietJuist
                    divError.Style.Add("display", "block")
                    'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.BeveiligingsantwoordNietJuist)
                    Exit Sub
                Case MembershipCreateStatus.InvalidPassword
                    ltlError.Text = Resources.Resource.ValidateWachtwoordEisen
                    divError.Style.Add("display", "block")
                    'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.WachtwoordEisen)
                    Exit Sub
                Case Else
                    ltlError.Text = Resources.Resource.ValidateOnbekendeFout
                    divError.Style.Add("display", "block")
                    'V.Message(Me.Master, Resources.Resource.LetOp, Resources.Resource.OnbekendeFout2)
                    Exit Sub
            End Select

            'Follow Cookie
            Dim cCookie As HttpCookie = Request.Cookies("Follow")
            If cCookie IsNot Nothing Then
                Dim sSessie As Sessie = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Sessies)(cCookie.Value).seSessies(0)
                sSessie.saveByIPersID(iPersID, iPartijIDBeheerder, sEmail, "Register")
            End If
            'Follow Cookie

            Response.Redirect(P.sPageUrlByGuid(sLanguage, "4adb0443-e8d2-4605-a7d1-c12f4302a5db"))
        Else
            ltlError.Text = Resources.Resource.ValidateTerms
            divError.Style.Add("display", "block")
        End If
    End Sub

End Class