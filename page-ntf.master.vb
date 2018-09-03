Imports Newtonsoft.Json

Partial Class page
    Inherits MasterPage
    Protected BP As New BasePage
    Dim P As New clsPage
    Dim ART As New clsArtikelen
    Dim OT As New clsOpeningstijden
    Dim ROUTE As New clsRoutes
    Dim CI As New clsCategorieItems
    Dim CAT As New clsCategorie
    Public sLanguage, sQs, sParent As String


    Public sWhatsAppNummer As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.RouteData.Values("language") IsNot Nothing Then
            sLanguage = Page.RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = BP.sDefaultLanguage.ToUpper
        End If

        InitFollowCookie()



        iTaalID = BP.sTaalID(sLanguage)
        hidLanguage.Value = sLanguage
        If Page.IsPostBack = False Then
            Try
                sQs = BP.RouteData.Values("page").ToString()
            Catch ex As Exception
                sQs = ""
            End Try

            Try
                sParent = BP.RouteData.Values("parentpage").ToString()
            Catch ex As Exception
                sParent = ""
            End Try

            Dim P As New clsPage
            P.dt = P.sPagesByActief(sLanguage, "Hoofdmenu", 0, True, BP.iPartijIDBeheerder)
            repMenu.DataSource = P.dt
            repMenu.DataBind()

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, "Algemeen", False, True, False)

            Dim PT As New clsPartijen
            Dim ADR As New clsAdressen
            Dim PartijGegevens As String = PT.sPartij(BP.iPartijIDBeheerder)
            sWhatsAppNummer = PT.sTelefoon

            aCheckout.NavigateUrl = P.sPageUrlByGuid(sLanguage, "deee4d8c-ff21-4bb9-b11d-4d1c0b43c57c")

            'ADR.dt = ADR.sAdresByPartijIDAndType(BP.iPartijIDBeheerder, "Bezoek")
            'If ADR.dt.Rows.Count > 0 Then
            '    ADR.dr = ADR.dt.Rows(0)
            '    ltlMaps.Text = ADR.dr.Item("sStraat") & " " & ADR.dr.Item("sHuisNr") & ADR.dr.Item("sToev") & "<br>" & ADR.dr.Item("sPostCode") & " " & ADR.dr.Item("sPlaats")
            'End If

            ''aInstagram.HRef = PT.sInstagram
            'aFacebook.HRef = PT.sFacebook
            'aTwitter.HRef = PT.sTwitter
            'aLinkedIn.HRef = PT.sLinkedIn

            'ltlTelefoon.Text = PT.sTelefoon
            'ltlEmail.Text = PT.sEmail
            'ltlKVK.Text = PT.sKvkNummer


            'OT.dt = OT.sOpeningsTijden(BP.iPartijIDBeheerder, "Algemeen")
            'If OT.dt.Rows.Count > 0 Then
            '    repOpeningstijden.DataSource = OT.dt
            '    repOpeningstijden.DataBind()
            'Else
            '    divOpeningstijden.Visible = False
            'End If





        End If

        Dim sSessieID As String = ""
        If Request.Cookies("SessieID") IsNot Nothing Then
            'Cookie bestaat return SessieID
            sSessieID = Request.Cookies("SessieID").Value
        Else
            'Cookie bestaat niet, maak aan
            Dim guid As Guid = Guid.NewGuid
            sSessieID = guid.ToString()
            Response.Cookies("SessieID").Value = sSessieID
            Response.Cookies("SessieID").Expires = Date.Now.AddDays(1)
            Response.Cookies("SessieID").Path = "/"
        End If

        InitWw(sSessieID)
    End Sub

    Private Sub InitFollowCookie()
        Dim ST As New clsSettings
        ST.InitWebshopSettings(BP.iPartijIDBeheerder)
        If ST.bFollowCookie Then
            Dim F As New clsFollow(Session.SessionID, Request)
            Dim cCookie As HttpCookie = F.UpdateCookie()
            Response.Cookies.Add(cCookie)
        Else
            Dim cCookie As HttpCookie = Request.Cookies("Follow")
            If cCookie IsNot Nothing Then
                cCookie.Expires = DateTime.Now.AddDays(-1D)
                Response.Cookies.Add(cCookie)
            End If
        End If
    End Sub

    Private Sub InitWw(ByVal sSession As String)
        Dim FR As New clsFacRgl
        Dim U As New clsUtility
        FR.dt = FR.sFacRgls_SessionAndImg(sSession, BP.iPartijIDBeheerder, "artikel")
        If FR.dt.Rows.Count > 0 Then
            Dim iCountProducts As Integer = 0
            For Each FR.dr In FR.dt.Rows
                iCountProducts = iCountProducts + FR.dr.Item("iAantal")
            Next
            ltlCountProducts.Text = iCountProducts
            'ltlAantalArtikelenMobiel.Text = iCountProducts
            repWinkelwagen.DataSource = FR.dt
            repWinkelwagen.DataBind()
        End If
        FR.sInfo(sSession, BP.iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            ltlTotalTop.Text = FR.sBedragMinusKorting
            ltlSubTotal.Text = FR.sBedrag
            ltlTotal.Text = FR.sBedragMinusKorting
            ltlDiscount.Text = FR.sDiscount
        Else
            ltlTotalTop.Text = "0,00"
            ltlSubTotal.Text = "0,00"
            ltlTotal.Text = "0,00"
            ltlDiscount.Text = "0,00"
        End If
    End Sub

    'Dim bOpen As Boolean = False
    'Private Sub repOpeningstijden_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repOpeningstijden.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        'hier wordt de betreffende literal opgehaald in de aspx pagina 
    '        Dim ltlDay As Literal = e.Item.FindControl("ltlDayOfWeek")

    '        Dim day As Integer = Date.Now.DayOfWeek
    '        If DataBinder.Eval(e.Item.DataItem, "iDayOfWeek") = day Then
    '            Dim trDay As HtmlTableRow = e.Item.FindControl("trDay")
    '            trDay.Attributes.Add("class", "active")

    '            Dim open As String = CDate(DataBinder.Eval(e.Item.DataItem, "dtStart")).ToString("HH:mm")
    '            Dim close As String = CDate(DataBinder.Eval(e.Item.DataItem, "dtEnd")).ToString("HH:mm")
    '            ltlVandaag.Text = "Wij zijn vandaag geopend van [open] tot [close]."
    '            ltlVandaag.Text = ltlVandaag.Text.Replace("[open]", open)
    '            ltlVandaag.Text = ltlVandaag.Text.Replace("[close]", close)

    '            bOpen = True
    '        End If

    '        Select Case DataBinder.Eval(e.Item.DataItem, "iDayOfWeek")
    '            Case 0
    '                ltlDay.Text = "Zondag"
    '            Case 1
    '                ltlDay.Text = "Maandag"
    '            Case 2
    '                ltlDay.Text = "Dinsdag"
    '            Case 3
    '                ltlDay.Text = "Woensdag"
    '            Case 4
    '                ltlDay.Text = "Donderdag"
    '            Case 5
    '                ltlDay.Text = "Vrijdag"
    '            Case 6
    '                ltlDay.Text = "Zaterdag"
    '            Case Else
    '        End Select
    '    End If
    'End Sub
    Public Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubscribe.Click
        If Page.RouteData.Values("language") IsNot Nothing Then
            sLanguage = Page.RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = BP.sDefaultLanguage.ToUpper
        End If

        iTaalID = BP.sTaalID(sLanguage)
        Dim LI As New clsLijstItems
        LI.dt = LI.sLijstItemByTitleAndType("Nieuwsbrief bevestiging", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            Dim email As String = LI.dr.Item("sItem1")
            'sBericht = sBericht.Replace("[name]", txtName.Text)
            'sBericht = sBericht.Replace("[phone]", txtPhone.Text)
            'sBericht = sBericht.Replace("[message]", txtMessage.Text)

            Dim iBcorePartijID = 0
            Dim iPatijBeheerder As Long = BP.iPartijIDBeheerder
            Dim sStatus As String = "versturen"
            Dim sType As String = LI.dr.Item("sTitle")
            Dim sItem As String = ""
            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", email)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", email)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", email)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", email)
            Dim sOnderwerp As String = LI.dr.Item("sSubTitle").replace("[email]", email)
            Dim sBericht As String = LI.dr.Item("sDescription")
            sBericht = sBericht.Replace("[email]", txtEmailModal.Text)
            Dim sBijlagen As String = ""
            Dim sData As String = ""
            Dim sInfo As String = ""

            Dim M As New clsMail
            Dim iMailID As Long = M.iscMail(iBcorePartijID, iPatijBeheerder, sStatus, sType, sItem, sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, sData, sInfo, Now)

            LI.dt = LI.sLijstItemByTitleAndType("Nieuwsbrief bevestiging", iTaalID, "Mailing")
            If LI.dt.Rows.Count > 0 Then
                LI.dr = LI.dt.Rows(0)
                sBericht = LI.dr.Item("sDescription")
                email = LI.dr.Item("sItem1")
                'sBericht = sBericht.Replace("[name]", txtName.Text)
                sBericht = sBericht.Replace("[email]", txtEmailModal.Text)
                sBericht = sBericht.Replace("[bedrijfsnaam]", "Basis")
                'sBericht = sBericht.Replace("[phone]", txtPhone.Text)
                'sBericht = sBericht.Replace("[message]", txtMessage.Text.Replace(Environment.NewLine, "<br />"))

                sType = LI.dr.Item("sTitle")
                sVan = LI.dr.Item("sItem1").replace("[email]", email)
                sNaar = txtEmailModal.Text
                sCC = LI.dr.Item("sItem4").replace("[email]", email)
                sBCC = LI.dr.Item("sItem5").replace("[email]", email)
                sOnderwerp = LI.dr.Item("sSubTitle").replace("[email]", email)
                sBijlagen = ""
                sInfo = "website.nl"

                iMailID = M.iscMail(0, BP.iPartijIDBeheerder, "versturen", sType, "", sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, "", sInfo, Now)
                'Follow Cookie
                Dim cCookie As HttpCookie = Request.Cookies("Follow")
                If cCookie IsNot Nothing Then
                    Dim sSessie As Sessie = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Sessies)(cCookie.Value).seSessies(0)
                    sSessie.save(BP.iPartijIDBeheerder, txtEmailModal.Text, "PageMaster Modal")
                End If
                'Follow Cookie
                Response.Redirect(P.sPageUrlByGuid(sLanguage, "1d6fb722-2352-44f4-a91e-9c9c176912aa"))

            Else

            End If
        End If
    End Sub

    'Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
    '    'lastlockedout date langer dan een half uur geleden dan de lock verwijderen

    '    Dim V As New clsValidatie
    '    Dim bOk As Boolean = True
    '    If Trim(txtGn.Text) = "" Then bOk = False
    '    If Trim(txtLoginPassword.Text) = "" Then bOk = False
    '    If bOk = False Then
    '        Exit Sub
    '    End If
    '    If Membership.ValidateUser(txtGn.Text, txtLoginPassword.Text) Then
    '        FormsAuthentication.SetAuthCookie(txtGn.Text, False)
    '        Response.Redirect(P.sPageUrlByGuid(sLanguage, "4adb0443-e8d2-4605-a7d1-c12f4302a5db"))
    '    Else
    '        ltlError.Text = Resources.Resource.Cantlogin
    '        divError.Style.Add("display", "block")
    '    End If
    'End Sub

    'Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
    '    If Trim(txtSearch.Text) <> "" Then
    '        Response.Redirect(P.sPageUrlByGuid(sLanguage, "3736bb39-ab94-496f-957a-cde26ed5f22c") & "?term=" & txtSearch.Text.Replace(" ", "+"))
    '    End If
    'End Sub

    Dim sMenusQs As String = ""
    Protected Sub repMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim sCurrentsQs As String = "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString").ToString.ToLower
            Dim sUrl = DataBinder.Eval(e.Item.DataItem, "sUrl").ToString.ToLower
            Dim aFirst As HtmlAnchor = e.Item.FindControl("aFirst")

            If (sUrl.StartsWith("http")) Then
                sCurrentsQs = sUrl
                aFirst.HRef = sUrl
                aFirst.Attributes.Add("target", "_blank")
            Else
                If sQs = DataBinder.Eval(e.Item.DataItem, "sQueryString").ToString.ToLower Then
                    aFirst.Attributes.Add("class", "active")
                End If
                If sLanguage.ToLower = BP.sDefaultLanguage.ToLower And sCurrentsQs = "/home" Then
                    aFirst.HRef = "/"
                Else
                    'check hoeveel talen
                    If BP.bMultilanguage Then
                        aFirst.HRef = "/" & sLanguage.ToLower & "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                    Else
                        aFirst.HRef = "/" & DataBinder.Eval(e.Item.DataItem, "sQueryString")
                    End If
                End If
            End If



            P.dt2 = P.sPagesByActief(sLanguage, "hoofdmenu", DataBinder.Eval(e.Item.DataItem, "iPageID"), True, BP.iPartijIDBeheerder)
            If P.dt2.Rows.Count > 0 Then
                Dim liFirst As HtmlGenericControl = e.Item.FindControl("liFirst")
                liFirst.Attributes.Add("class", "dropdown")
                aFirst.Attributes.Add("class", "dropdown-toggle smooth-scroll")
                aFirst.Attributes.Add("data-toggle", "data-toggle smooth-scroll")
                aFirst.Attributes.Add("role", "button")
                aFirst.Attributes.Add("aria-haspopup", "true")
                aFirst.Attributes.Add("aria-expanded", "false")

                Dim repSubMenu As Repeater = e.Item.FindControl("repSubMenu")
                sMenusQs = sCurrentsQs
                repSubMenu.DataSource = P.dt2
                repSubMenu.DataBind()
            End If


        End If
    End Sub

    Protected Sub repSubMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim sQsSecond As String = DataBinder.Eval(e.Item.DataItem, "sQueryString").ToString.ToLower
            Dim aSecond As HtmlAnchor = e.Item.FindControl("aSecond")

            Dim sUrl = DataBinder.Eval(e.Item.DataItem, "sUrl").ToString.ToLower

            If (sUrl.StartsWith("http")) Then
                aSecond.HRef = sUrl
                aSecond.Attributes.Add("target", "_blank")
            Else
                aSecond.HRef = "~/" & sMenusQs & "/" & sQsSecond
                If sQs = DataBinder.Eval(e.Item.DataItem, "sQueryString").ToString.ToLower Then
                    aSecond.Attributes.Add("class", "active")
                End If
            End If

            ' HIER LATER CATEGORIE KOPPELEN AAN PAGINA EN UIT DB TABEL PAGES HALEN ALS CATID 0 IS DAN NIKS ANDERS REPEATER VULLEN
            Dim iPageID As Long = DataBinder.Eval(e.Item.DataItem, "iPageID")
            If iPageID = 1140 Then ' 4771                              4743 
                Dim repCategories As Repeater = e.Item.FindControl("repCategories")
                CI.dt = CI.sCategorieItemsByParentIDMenu(BP.iPartijIDBeheerder, iTaalID, 0, True, 10)
                If CI.dt.Rows.Count > 0 Then
                    Dim liSecond As HtmlGenericControl = e.Item.FindControl("liSecond")
                    liSecond.Visible = False
                    repCategories.DataSource = CI.dt
                    repCategories.DataBind()
                End If
            End If
            ' HIER LATER CATEGORIE KOPPELEN AAN PAGINA EN UIT DB TABEL PAGES HALEN

        End If
    End Sub

    Protected Sub repCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")

            Dim aTitle2 As HtmlAnchor = e.Item.FindControl("aTitle2")
            aTitle2.Visible = False
            'CAT.dt = CAT.sCategorieByParentIDTOP(BP.iPartijIDBeheerder, iTaalID, iCatID, 5)
            'If CAT.dt.Rows.Count > 0 Then
            '    Dim rep As Repeater = e.Item.FindControl("repSubCategories")
            '    rep.DataSource = CAT.dt
            '    rep.DataBind()
            'Else
            '    aTitle2.Visible = False
            'End If

            ROUTE.dt = ROUTE.sRoutes("categorie", iCatID, iTaalID)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aTitle1 As HtmlAnchor = e.Item.FindControl("aTitle1")
                aTitle1.HRef = ROUTE.dr.Item("sURL")
                aTitle2.HRef = ROUTE.dr.Item("sURL")
            End If
        End If
    End Sub

    Protected Sub repSubCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim iCategoryID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            ROUTE.dt = ROUTE.sRoutes("categorie", iCategoryID, iTaalID)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aSub As HtmlAnchor = e.Item.FindControl("aSub")
                aSub.HRef = ROUTE.dr.Item("sURL")
            End If
        End If
    End Sub

    'INDIEN ER ARTIKELEN ALS SUBMENU MOETEN LADEN
    'Protected Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")
    '        ROUTE.dt = ROUTE.sRoutes("artikel", iArtikelID, iTaalID)
    '        If ROUTE.dt.Rows.Count > 0 Then
    '            ROUTE.dr = ROUTE.dt.Rows(0)
    '            Dim aArtikelLink As HtmlAnchor = e.Item.FindControl("aArtikelLink")
    '            aArtikelLink.HRef = ROUTE.dr.Item("sURL")
    '        End If
    '    End If
    'End Sub

    'Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
    '    If Trim(txtSearch1.Text) <> "" Then
    '        Response.Redirect(P.sPageUrlByGuid(sLanguage, "07a18234-6953-447e-acc6-02a228c39562") & "?term=" & txtSearch1.Text.Replace(" ", "+"))
    '    End If
    'End Sub
End Class