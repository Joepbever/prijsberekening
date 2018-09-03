
Imports System.Web.Routing

Partial Class Bevestiging
    Inherits BasePage

    Dim LOG As New clsLog
    Dim UTIL As New clsUtility
    Dim PI As New clsPageItems
    Dim P As New clsPage

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

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)
            Dim iFacKopID As String = RouteData.Values("ifackopid")
            If iFacKopID = Nothing Then
                Exit Sub
            End If
            'If iFacKopID = "rekening" Then
            '    rekening.Visible = True
            '    Exit Sub
            'End If

            If IsOnline() = False Then
                divAccount.Visible = False
            End If

            Dim FACITEMS As New clsFacItems
            FACITEMS.sFacItems(iFacKopID)
            If FACITEMS.sTimestamp <> RouteData.Values("timestamp") Then
                Response.Redirect("~/")
            End If

            hidFacKopID.Value = iFacKopID
            Dim FACKOP As New clsFacKop
            FACKOP.dt = FACKOP.sFacKopByFacKopID(iFacKopID)
            If FACKOP.dt.Rows.Count > 0 Then
                FACKOP.dr = FACKOP.dt.Rows(0)
                ltlFactuur.Text = FACKOP.dr.Item("sFactuur")
                ltlBezorgadres.Text = FACKOP.dr.Item("sVerzendGegevens").Replace(Environment.NewLine, "<br/>")
                ltlFactuuradres.Text = FACKOP.dr.Item("sPartijGegevens").Replace(Environment.NewLine, "<br/>")
                'tr_4MQ5VgBzSf

                Dim PT As New clsPartijen
                PT.sPartij(iPartijIDBeheerder)
                Dim sContact As String = PT.sPartijGegevens
                sContact &= "<br />" & PT.sTelefoon
                sContact &= "<br />" & PT.sEmail
                Contact.Text = sContact.Replace(Environment.NewLine, "<br/>")
                Dim P As New clsPage

                aAccount.HRef = P.sPageUrlByGuid(sLanguage, "3bf5cc4b-e3b2-4574-a13d-9250d50920e6")
                aBestellingen.HRef = P.sPageUrlByGuid(sLanguage, "7f13d727-9439-46e2-9678-7764371c9315")

                Select Case FACKOP.dr.Item("sStatus").ToLower()
                    Case "betaald"
                        Success.Visible = True
                    Case "geannuleerd"
                        cancelled.Visible = True
                        showPayment()
                    Case "open"
                        open.Visible = True
                        showPayment()
                    Case "verlopen"
                        expired.Visible = True
                    Case "pending"
                        pending.Visible = True
                    Case "paidout"
                    Case "refunded"
                    Case "charged_back"
                End Select
            End If
        End If
    End Sub

    Public Sub showPayment()
        Dim MOLLIE As New clsMollie
        divPayment.Visible = True

        MOLLIE.ListMethods(rblBetaalmethode)
        rblBetaalmethode.Items(0).Selected = True

        MOLLIE.ListIssuers(ddlBank)
        ddlBank.Items(0).Selected = True
    End Sub

    Protected Sub btnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        'Dim V As New clsValidatie
        Dim FI As New clsFacItems
        FI.sFacItems(hidFacKopID.Value)
        Dim sSessieID As String = FI.sSession

        Dim sBetaalmethode As String = rblBetaalmethode.SelectedValue
        If sBetaalmethode = "" Then
            ltlInfo.Text = Resources.Resource.SelecteerBetaalmethode
            Exit Sub
        End If

        Dim ST As New clsSettings
        ST.InitWebshopSettings(iPartijIDBeheerder)

        Dim AV As New clsArtikelVarianten
        If ST.bVoorraad Then
            If AV.CheckVoorraad(sSessieID, iPartijIDBeheerder) = False Then
                ltlInfo.Text = "During checkout, the following items are no longer in stock: <br />" & AV.sMelding
                Exit Sub
            End If
            AV.uVoorraadGereserveerd(sSessieID, iPartijIDBeheerder, True)
        End If

        Dim FACRGL As New clsFacRgl
        FACRGL.sInfo(sSessieID, iPartijIDBeheerder)
        If FACRGL.iAantalRegels > 0 Then
        Else
            ltlInfo.Text = "No Items in your shopping cart."
            Exit Sub
        End If

        Dim FACKOP As New clsFacKop
        FACKOP.dt = FACKOP.sFacKopByFacKopID(hidFacKopID.Value)
        If FACKOP.dt.Rows.Count > 0 Then
            FACKOP.dr = FACKOP.dt.Rows(0)
            Dim MOLLIE As New clsMollie

            Dim sResponsePage As String = P.sPageUrlByGuid(sLanguage, "58a8219f-0c5a-4b44-a89f-02231ecf769b") ' "/order-information"
            sResponsePage = sResponsePage.Substring(1, sResponsePage.Length - 1)
            sResponsePage = sResponsePage & "/" & hidFacKopID.Value & "/" & RouteData.Values("timestamp").ToString() & "/" & FACKOP.dr.Item("iPartijID") & "/" & FACKOP.dr.Item("iPersID")

            Dim ROUTE As New clsRoutes
            ROUTE.iRoute(iPartijIDBeheerder, iTaalID, hidFacKopID.Value, "idealbevestiging", "", "~/" & sResponsePage)

            Dim routes = RouteTable.Routes
            Using routes.GetWriteLock()
                routes.Clear()
                RouteConfig.RegisterRoutes(routes)
            End Using
            Dim U As New clsUtility
            Dim sPaymentURL As String = MOLLIE.CreatePayment(FACKOP.dr.Item("iFactuurBedrag").ToString().Replace(",", "."), sLanguage, sResponsePage, U.Name(), CLng(hidFacKopID.Value), 0, 0, rblBetaalmethode.SelectedValue, ddlBank.SelectedValue, sSessieID)
            Response.Redirect(sPaymentURL)
        End If
    End Sub
End Class