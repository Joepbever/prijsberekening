Partial Class _Contact
    Inherits BasePage
    Dim P As New clsPage
    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long
    Public partijEmail, partijTelefoon As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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

            Dim PT As New clsPartijen
            Dim ADR As New clsAdressen
            Dim PartijGegevens As String = PT.sPartij(iPartijIDBeheerder)
            partijEmail = PT.sEmail
            partijTelefoon = PT.sTelefoon
            'sBreadcrumbs(ltlBreadCrumps)
        End If

    End Sub

    Public Sub btnSubmit1_Click(sender As Object, e As EventArgs) Handles btnSubmit1.Click
        If Page.RouteData.Values("language") IsNot Nothing Then
            sLanguage = Page.RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        If txtLeeg.Text <> "" Then
            Exit Sub
        End If

        iTaalID = sTaalID(sLanguage)
        Dim LI As New clsLijstItems
        LI.dt = LI.sLijstItemByTitleAndType("Contactformulier", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            Dim sBericht As String = LI.dr.Item("sDescription")
            Dim email As String = LI.dr.Item("sItem1")
            sBericht = sBericht.Replace("[name]", txtName.Text)
            sBericht = sBericht.Replace("[email]", txtEmail.Text)
            sBericht = sBericht.Replace("[phone]", txtPhone.Text)
            sBericht = sBericht.Replace("[message]", txtMessage.Text)

            Dim sType As String = LI.dr.Item("sTitle")
            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", email)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", email)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", email)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", email)
            Dim sOnderwerp As String = LI.dr.Item("sSubTitle").replace("[email]", email)
            Dim sBijlagen As String = ""
            Dim sInfo As String = txtName.Text

            Dim M As New clsMail
            Dim iMailID As Long = M.iscMail(0, iPartijIDBeheerder, "versturen", sType, "", sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, "", sInfo, Now)

            LI.dt = LI.sLijstItemByTitleAndType("Contactformulier bevestiging", iTaalID, "Mailing")
            If LI.dt.Rows.Count > 0 Then
                LI.dr = LI.dt.Rows(0)
                sBericht = LI.dr.Item("sDescription")
                email = LI.dr.Item("sItem1")
                sBericht = sBericht.Replace("[name]", txtName.Text)
                sBericht = sBericht.Replace("[email]", txtEmail.Text)
                sBericht = sBericht.Replace("[phone]", txtPhone.Text)
                sBericht = sBericht.Replace("[message]", txtMessage.Text.Replace(Environment.NewLine, "<br />"))

                sType = LI.dr.Item("sTitle")
                sVan = LI.dr.Item("sItem1").replace("[email]", email)
                sNaar = txtEmail.Text
                sCC = LI.dr.Item("sItem4").replace("[email]", email)
                sBCC = LI.dr.Item("sItem5").replace("[email]", email)
                sOnderwerp = LI.dr.Item("sSubTitle").replace("[email]", email)
                sBijlagen = ""
                sInfo = "website.nl"

                iMailID = M.iscMail(0, iPartijIDBeheerder, "versturen", sType, "", sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, "", sInfo, Now)

                'Follow Cookie
                Dim cCookie As HttpCookie = Request.Cookies("Follow")
                If cCookie IsNot Nothing Then
                    Dim sSessie As Sessie = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Sessies)(cCookie.Value).seSessies(0)
                    sSessie.save(iPartijIDBeheerder, txtEmail.Text, "Bestellen stappen")
                End If
                'Follow Cookie

                Response.Redirect(P.sPageUrlByGuid(sLanguage, "1d6fb722-2352-44f4-a91e-9c9c176912aa"))
            Else

            End If
        End If
    End Sub


End Class