Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization

Partial Class MollieResponsePayment
    Inherits System.Web.UI.Page

    Dim LOG As New clsLog
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim LOG As New clsLog
        ' 77.245.85.226 en 77.245.85.245. 
        'LOG.iLog("Mollieresponse", "start", "")

        If Page.IsPostBack = False Then
            Dim sPaymentID As String = Request.Form("id")
            If sPaymentID = Nothing Then
                sPaymentID = Request.QueryString("id")
            End If

            Dim AV As New clsArtikelVarianten
            Dim MOLLIE As New clsMollie
            Dim FACKOP As New clsFacKop
            Dim PT As New clsPartijen
            Dim FACITEMS As New clsFacItems
            Dim BP As New BasePage
            Dim FACRGL As New clsFacRgl
            Dim ST As New clsSettings
            ST.InitWebshopSettings(BP.iPartijIDBeheerder)


            MOLLIE.GetPayment(sPaymentID)
            FACITEMS.iFacItems(MOLLIE.iFacKopID, "Mollie", MOLLIE.sContent, "")

            Dim sUserID As String = ""
            FACKOP.dt = FACKOP.sFacKopByFacKopID(MOLLIE.iFacKopID)
            If FACKOP.dt.Rows.Count > 0 Then
                FACKOP.dr = FACKOP.dt.Rows(0)
            End If
            Select Case MOLLIE.sStatus.ToLower()
                Case "paid"
                    Try
                        If FACKOP.dr.Item("sStatus").ToLower() = "betaald" Then
                            FACITEMS.iFacItems(MOLLIE.iFacKopID, "Al betaald", "", "")
                            Exit Sub
                        End If
                    Catch ex As Exception

                    End Try
                    Dim sType As String = "factuur"
                    Dim sFactuurnummer As String = ST.scSettingByTypeAndGroup("webshop", sType & "teller", BP.iPartijIDBeheerder)
                    ST.uSettingByTypeAndGroup(CInt(sFactuurnummer) + 1, sType & "teller", "webshop", BP.iPartijIDBeheerder)
                    Dim iBetaalTermijn As Integer = ST.scSettingByTypeAndGroup("webshop", sType & "termijn", BP.iPartijIDBeheerder)


                    FACITEMS.iFacItems(MOLLIE.iFacKopID, "status", MOLLIE.sStatus, "")
                    FACITEMS.iFacItems(MOLLIE.iFacKopID, "PaymentID", sPaymentID, "")

                    FACKOP.uStatusType("factuur", "betaald", sPaymentID, MOLLIE.iFacKopID)
                    FACKOP.uFactuurByID(MOLLIE.iFacKopID, sFactuurnummer, "")

                    FACRGL.uFacRglBySession(MOLLIE.iFacKopID, sFactuurnummer, sPaymentID, MOLLIE.sSession) 'sessie verwijderen


                    LOG.iLog("Mollieresponse", "start", "2")
                    If ST.bMyParcel Then
                        Try
                            Dim MYPARCEL As New clsMyParcel
                            Dim Z As New clsZendingen
                            Z.dt = Z.sZendingByFacKopID(MOLLIE.iFacKopID)
                            If Z.dt.Rows.Count > 0 Then
                                Z.dr = Z.dt.Rows(0)
                                MYPARCEL.AddShipment(Z.dr.Item("sCountryCode"), Z.dr.Item("sCity"), Z.dr.Item("sStreet"), Z.dr.Item("sNumber"), Z.dr.Item("sAddition").ToString().ToLower(), Z.dr.Item("sPostalcode"), Z.dr.Item("sPerson"), Z.dr.Item("sPhone"), Z.dr.Item("sEmail"))
                            End If
                        Catch ex As Exception
                            LOG.iLog("response", "addshipment", ex.Message())
                        End Try
                    End If

                    LOG.iLog("Mollieresponse", "start", "3")
                    If ST.bFactuurPDF Then
                        LOG.iLog("Mollieresponse", "start", "4")
                        LOG.iLog("Mollieresponse", "start", "4.1" & MOLLIE.iFacKopID & "" & sFactuurnummer)
                        Dim PDF As New clsPDF
                        PDF.sFooterUrl = BP.sURL() & "Data/Resources//Rapporten/factuur_footer.aspx?id=" & MOLLIE.iFacKopID
                        LOG.iLog("MOLLIE", PDF.sFooterUrl, "")
                        PDF.sPDF(Me.Page, "/Data/Resources/Rapporten/Factuur.aspx?id=" & MOLLIE.iFacKopID, MOLLIE.iFacKopID & "_" & sFactuurnummer, "", False, True, "opslaan")
                        FACKOP.uFactuurByID(MOLLIE.iFacKopID, sFactuurnummer, "~/facturen/" & MOLLIE.iFacKopID.ToString() & "_" & sFactuurnummer & ".pdf")

                    End If

                    LOG.iLog("Mollieresponse", "Start Exact", "5")

                    If ST.bExact Then

                        LOG.iLog("Mollieresponse", "start", "6. Exact")
                        Dim request As WebRequest = WebRequest.Create("https://exact.websentiment.nl/Exact/ExactDataKoppeling.aspx?type=verkooporder&id=" & MOLLIE.iFacKopID & "&iPartijIDBeheerder=" & BP.iPartijIDBeheerder)
                        Dim response As WebResponse = request.GetResponse()
                        response.Close()
                    End If

                    LOG.iLog("Mollieresponse", "start", "7. Mail")

                    Dim M As New clsMail
                    M.MailOrder(MOLLIE.iFacKopID, MOLLIE.sLanguage, ST.bFactuurPDF)

                    If ST.bVoorraad Then
                        LOG.iLog("Mollieresponse", "Start voorraad", "7. Voorraad")
                        AV.uVoorraadBeschikbaarByFacKopID(MOLLIE.iFacKopID, False)
                        AV.uVoorraadGereserveerdByFacKopID(MOLLIE.iFacKopID, False)
                    End If

                    'Eventueel kortingscode verwijderen
                    Dim KT As New clsKortingCodes
                    KT.dt = KT.sKortingsCodesByFacKop(BP.iPartijIDBeheerder, MOLLIE.iFacKopID)
                    For Each KT.dr In KT.dt.Rows
                        Dim iAantal As Long = KT.dr.Item("iAantalKeer") - 1
                        If iAantal > 0 Then
                            KT.uStatus(KT.dr.Item("iKortingsCodeID"), "actief", iAantal)
                        Else
                            KT.uStatus(KT.dr.Item("iKortingsCodeID"), "inactief", iAantal)
                        End If
                    Next

                Case "cancelled"
                    FACKOP.uStatusType("order", "geannuleerd", sPaymentID, MOLLIE.iFacKopID)

                    If ST.bVoorraad Then
                        AV.uVoorraadGereserveerd(MOLLIE.sSession, BP.iPartijIDBeheerder, False)
                    End If
                Case "open"
                    FACKOP.uStatusType("order", "open", sPaymentID, MOLLIE.iFacKopID)
                Case "expired"
                    'Als deze na 15 min Expired. 
                    'Kan in de tussentijd al iets gekocht zijn. 
                    'Bestelling hier cancellen?

                    FACKOP.uStatusType("order", "verlopen", sPaymentID, MOLLIE.iFacKopID)
                    If ST.bVoorraad Then
                        AV.uVoorraadGereserveerd(MOLLIE.sSession, BP.iPartijIDBeheerder, False)
                    End If
                Case "pending"
                    FACKOP.uStatusType("order", "pending", sPaymentID, MOLLIE.iFacKopID)
                Case "paidout"
                    FACKOP.uStatusType("order", "paidout", sPaymentID, MOLLIE.iFacKopID)
                Case "refunded"
                    FACKOP.uStatusType("order", "refunded", sPaymentID, MOLLIE.iFacKopID)
                Case "charged_back"
                    FACKOP.uStatusType("order", "charged_back", sPaymentID, MOLLIE.iFacKopID)
            End Select
        End If
        '$ curl -X GET https://api.mollie.nl/v1/payments/tr_WDqYK6vllg \
        '    -H "Authorization: Bearer test_TwRVMyD6UKLGBPsJWzY8apJWA3cA5p"
        '        Response
        'HTTP/1.1 200 OK
        'Content-Type: application/json; charset=utf-8

        '{
        '    "id": "tr_WDqYK6vllg",
        '    "mode": "test",
        '    "createdDatetime": "2014-09-05T14:36:31.0Z",
        '    "status": "paid",
        '    "paidDatetime": "2014-09-05T14:37:35.0Z",
        '    "amount": 35.07,
        '    "description": "Order 33",
        '    "method": "ideal",
        '    "metadata": {
        '        "order_id": "33"
        '    },
        '    "details": {
        '        "consumerName": "Hr E G H K\u00fcppers en\/of MW M.J. K\u00fcppers-Veeneman",
        '        "consumerAccount": "NL53INGB0618365937",
        '        "consumerBic": "INGBNL2A"
        '    },
        '    "locale": "nl",
        '    "profileId": "pfl_QkEhN94Ba",
        '    "links": {
        '        "webhookUrl": "https://webshop.example.org/payments/webhook",
        '        "redirectUrl": "https://webshop.example.org/order/33/"
        '    }
        '}
    End Sub
End Class
