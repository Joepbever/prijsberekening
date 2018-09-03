Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Data

Public Class clsMollie

    Dim BP As New BasePage
    Dim U As New clsUtility
    Dim ST As New clsSettings

    Public sPaymentURL As String = ""
    Public sPaymentID As String = ""
    Public Function CreatePayment(sPrijs As String, sLanguage As String, sPage As String, sDescription As String, iFacKopID As Long, iMailID As Long, iArtikelStaffelID As Long, sMethod As String, sIssuer As String, sSession As String, Optional ByVal iReserveringID As Long = 0, Optional ByVal sResponsePaymentTemplate As String = "") As String

        If sMethod.ToLower <> "ideal" Then
            sIssuer = ""
        End If
        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MollieURL", BP.iPartijIDBeheerder) & "payments"
        Dim sPassword As String = ""
        If U.Config("Test") = "True" Then
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePasswordTest", BP.iPartijIDBeheerder)
        Else
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePassword", BP.iPartijIDBeheerder)
        End If
        Dim sWebhookURL As String = BP.sURL()
        If sResponsePaymentTemplate = "" Then
            sWebhookURL = sWebhookURL & "MollieResponsePayment.aspx"
        Else
            sWebhookURL = sWebhookURL & sResponsePaymentTemplate
        End If
        Dim sRedirectURL As String = BP.sURL() & sPage
        'method issuer
        Dim data As String
        If sIssuer = "" Then
            data = "{""amount"": " & sPrijs.ToString().Replace(",", ".") & ", ""description"":""" & sDescription & """,""metadata"": { ""order_type"" : ""nieuw"",""order_id"": " _
                           & iFacKopID & ", ""lang"": """ & sLanguage & """, ""iMailID"": " & iMailID & ", ""session"": """ & sSession &
                      """ }, ""webhookUrl"": """ & sWebhookURL & """, ""method"":""" & sMethod.ToLower & """, ""redirectUrl"":""" & sRedirectURL & """, ""locale"":""" & sLanguage.ToLower() & """}"
        Else
            data = "{""amount"": " & sPrijs.ToString().Replace(",", ".") & ", ""description"":""" & sDescription & """,""metadata"": { ""order_type"" : ""nieuw"",""order_id"": " _
                    & iFacKopID & ", ""lang"": """ & sLanguage & """, ""iMailID"": " & iMailID & ", ""iReserveringID"": " & iReserveringID & ", ""session"": """ & sSession &
               """ }, ""webhookUrl"": """ & sWebhookURL & """, ""method"":""" & sMethod.ToLower & """, ""redirectUrl"":""" & sRedirectURL & """, ""issuer"":""" & sIssuer & """, ""locale"":""" & sLanguage.ToLower() & """}"
        End If

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "POST"
        myReq.ContentLength = data.Length
        myReq.ContentType = "application/json; charset=UTF-8"

        Dim enc As New UTF8Encoding()
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(sPassword)))


        Using ds As Stream = myReq.GetRequestStream()
            ds.Write(enc.GetBytes(data), 0, data.Length)
        End Using

        Dim wr As WebResponse = myReq.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        Dim content As String = reader.ReadToEnd()
        Dim json_serializer As New JavaScriptSerializer()
        Dim routes_list As Object = json_serializer.DeserializeObject(content)

        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "id"
                    sPaymentID = l.Value
                Case "links"
                    Dim oLinks As Object = l.Value
                    For Each kvp As KeyValuePair(Of String, Object) In oLinks
                        Select Case kvp.Key.ToLower()
                            Case "paymenturl"
                                sPaymentURL = kvp.Value
                            Case "redirecturl"
                                'Dim sRedirectURL As String = kvp.Value 'Wordt van te voren meegestuurd
                        End Select
                    Next
            End Select
        Next
        Return sPaymentURL
    End Function

    Public sType As String = ""
    Public sStatus As String = ""
    Public iFacKopID As String = ""
    Public iArtikelStaffelID As String = ""
    Public iMailID As String = ""
    Public sReserveringID As String = ""


    Public sMethod As String = ""
    Public sIBAN As String = ""
    Public sContent As String = ""
    Public sSession As String = ""
    Public sLanguage As String = ""
    Public Function GetPayment(sPaymentID As String) As String
        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MollieURL", BP.iPartijIDBeheerder) & "payments/" & sPaymentID
        Dim sPassword As String = ""
        If U.Config("Test") = "True" Then
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePasswordTest", BP.iPartijIDBeheerder)
        Else
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePassword", BP.iPartijIDBeheerder)
        End If

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "GET"
        myReq.ContentType = "application/json; charset=UTF-8"

        Dim enc As New UTF8Encoding()
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(sPassword)))

        Dim wr As WebResponse = myReq.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        sContent = reader.ReadToEnd()
        Dim json_serializer As New JavaScriptSerializer()
        Dim routes_list As Object = json_serializer.DeserializeObject(sContent)

        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "metadata"
                    Dim oLinks As Object = l.Value
                    For Each kvp As KeyValuePair(Of String, Object) In oLinks
                        Select Case kvp.Key.ToLower()
                            Case "order_id"
                                iFacKopID = kvp.Value.ToLower()
                            Case "iartikelstaffelid"
                                iArtikelStaffelID = kvp.Value.ToLower()
                            Case "order_type"
                                sType = kvp.Value.ToLower()
                            Case "imailid"
                                iMailID = kvp.Value.ToLower()
                            Case "ireserveringid"
                                sReserveringID = kvp.Value.ToLower()
                            Case "session"
                                sSession = kvp.Value.ToLower()
                            Case "lang"
                                sLanguage = kvp.Value.ToLower()

                        End Select
                    Next

                Case "status"
                    sStatus = l.Value
                Case "method"
                    sMethod = l.Value

                Case "details"
                    If sStatus.ToLower = "paid" Then
                        Dim oLinks As Object = l.Value
                        For Each kvp As KeyValuePair(Of String, Object) In oLinks
                            Select Case kvp.Key.ToLower()
                                Case "consumeraccount"
                                    sIBAN = kvp.Value.ToLower()

                            End Select
                        Next
                    End If
                Case Else

            End Select
        Next

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
        Return sStatus
    End Function

    Public Sub ListMethods(rblBetaalmethoden As RadioButtonList)
        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MollieURL", BP.iPartijIDBeheerder) & "methods"
        Dim sPassword As String = ""
        If U.Config("Test") = "True" Then
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePasswordTest", BP.iPartijIDBeheerder)
        Else
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePassword", BP.iPartijIDBeheerder)
        End If


        'Dim sData As String = "{""amount"": " & "100.00" & ", ""description"":""" & "asd asasd asd asd" & """,""metadata"": { ""order_type"": ""nieuw"",""order_id"": " _
        '                      & 0 & ", ""iArtikelStaffelID"": " & 0 & ", ""iArtikelID"": " & 0 & _
        '                      " },""webhookUrl"": """ & sWebhookURL & """, ""redirectUrl"":""" & sRedirectURL & """}"

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "GET"
        ' myReq.ContentLength = sData.Length
        myReq.ContentType = "application/json; charset=UTF-8"

        Dim enc As New UTF8Encoding()
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(sPassword)))


        'Using ds As Stream = myReq.GetRequestStream()
        '    ds.Write(enc.GetBytes(sData), 0, sData.Length)
        'End Using

        Dim wr As WebResponse = myReq.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        Dim content As String = reader.ReadToEnd()
        Dim json_serializer As New JavaScriptSerializer()
        Dim routes_list As Object = json_serializer.DeserializeObject(content)

        Dim oData As Object
        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "data"
                    oData = l.Value
                    For Each o As Object In oData

                        Dim opt As New ListItem
                        For Each kvp As KeyValuePair(Of String, Object) In o
                            Select Case kvp.Key.ToLower()
                                Case "id"
                                    opt.Value = kvp.Value
                                Case "description"
                                    opt.Text = kvp.Value
                            End Select
                        Next
                        rblBetaalmethoden.Items.Add(opt)
                    Next
            End Select
        Next
    End Sub



    Public Function ListMethods() As DataTable
        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MollieURL", BP.iPartijIDBeheerder) & "methods"
        Dim sPassword As String = ""
        If U.Config("Test") = "True" Then
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePasswordTest", BP.iPartijIDBeheerder)
        Else
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePassword", BP.iPartijIDBeheerder)
        End If

        'Dim sData As String = "{""amount"": " & "100.00" & ", ""description"":""" & "asd asasd asd asd" & """,""metadata"": { ""order_type"": ""nieuw"",""order_id"": " _
        '                      & 0 & ", ""iArtikelStaffelID"": " & 0 & ", ""iArtikelID"": " & 0 & _
        '                      " },""webhookUrl"": """ & sWebhookURL & """, ""redirectUrl"":""" & sRedirectURL & """}"

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "GET"
        ' myReq.ContentLength = sData.Length
        myReq.ContentType = "application/json; charset=UTF-8"

        Dim enc As New UTF8Encoding()
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(sPassword)))


        'Using ds As Stream = myReq.GetRequestStream()
        '    ds.Write(enc.GetBytes(sData), 0, sData.Length)
        'End Using

        Dim wr As WebResponse = myReq.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        Dim content As String = reader.ReadToEnd()
        Dim json_serializer As New JavaScriptSerializer()
        Dim routes_list As Object = json_serializer.DeserializeObject(content)

        Dim oData As Object

        Dim dt As New DataTable
        dt.Columns.Add("sID")
        dt.Columns.Add("sDescription")
        dt.Columns.Add("sImage")

        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "data"
                    oData = l.Value
                    For Each o As Object In oData

                        Dim sID As String = ""
                        Dim sDescription As String = ""
                        Dim sImage As String = ""
                        'Dim opt As New ListItem
                        For Each kvp As KeyValuePair(Of String, Object) In o
                            Select Case kvp.Key.ToLower()
                                Case "id"
                                    sID = kvp.Value
                                Case "description"
                                    sDescription = kvp.Value
                                Case "image"
                                    Dim oImages As Object = kvp.Value
                                    For Each kvp2 As KeyValuePair(Of String, Object) In oImages
                                        Select Case kvp2.Key.ToLower()
                                            Case "bigger"
                                                sImage = kvp2.Value
                                        End Select
                                    Next
                            End Select
                        Next
                        MaakRecord(dt, sID, sDescription, sImage)
                        'rblBetaalmethoden.Items.Add(opt)
                    Next
            End Select
        Next
        Return dt
    End Function

    Public Sub ListMethods(ddlPay As DropDownList)
        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MollieURL", BP.iPartijIDBeheerder) & "methods"
        Dim sPassword As String = ""
        If U.Config("Test") = "True" Then
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePasswordTest", BP.iPartijIDBeheerder)
        Else
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePassword", BP.iPartijIDBeheerder)
        End If


        'Dim sData As String = "{""amount"": " & "100.00" & ", ""description"":""" & "asd asasd asd asd" & """,""metadata"": { ""order_type"": ""nieuw"",""order_id"": " _
        '                      & 0 & ", ""iArtikelStaffelID"": " & 0 & ", ""iArtikelID"": " & 0 & _
        '                      " },""webhookUrl"": """ & sWebhookURL & """, ""redirectUrl"":""" & sRedirectURL & """}"

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "GET"
        ' myReq.ContentLength = sData.Length
        myReq.ContentType = "application/json; charset=UTF-8"

        Dim enc As New UTF8Encoding()
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(sPassword)))


        'Using ds As Stream = myReq.GetRequestStream()
        '    ds.Write(enc.GetBytes(sData), 0, sData.Length)
        'End Using

        Dim wr As WebResponse = myReq.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        Dim content As String = reader.ReadToEnd()
        Dim json_serializer As New JavaScriptSerializer()
        Dim routes_list As Object = json_serializer.DeserializeObject(content)

        Dim oData As Object
        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "data"
                    oData = l.Value
                    For Each o As Object In oData
                        Dim opt As New ListItem
                        For Each kvp As KeyValuePair(Of String, Object) In o
                            Select Case kvp.Key.ToLower()
                                Case "id"
                                    opt.Value = kvp.Value
                                Case "name"
                                    opt.Text = kvp.Value
                            End Select
                        Next
                        ddlPay.Items.Add(opt)
                    Next
            End Select
        Next
    End Sub

    Private Sub MaakRecord(ByRef dt As DataTable, ByVal sID As String, ByVal sDescription As String, ByVal sImage As String)
        Dim dr As DataRow
        dr = dt.NewRow()
        dr("sID") = sID
        dr("sDescription") = sDescription
        dr("sImage") = sImage
        dt.Rows.Add(dr)
    End Sub
    Public Sub ListIssuers(ddlBank As DropDownList)

        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MollieURL", BP.iPartijIDBeheerder) & "issuers"
        Dim sPassword As String = ""
        If U.Config("Test") = "True" Then
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePasswordTest", BP.iPartijIDBeheerder)
        Else
            sPassword = ST.scSettingByTypeAndGroup("Instellingen", "MolliePassword", BP.iPartijIDBeheerder)
        End If

        'Dim sData As String = "{""amount"": " & "100.00" & ", ""description"":""" & "asd asasd asd asd" & """,""metadata"": { ""order_type"": ""nieuw"",""order_id"": " _
        '                      & 0 & ", ""iArtikelStaffelID"": " & 0 & ", ""iArtikelID"": " & 0 & _
        '                      " },""webhookUrl"": """ & sWebhookURL & """, ""redirectUrl"":""" & sRedirectURL & """}"

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "GET"
        ' myReq.ContentLength = sData.Length
        myReq.ContentType = "application/json; charset=UTF-8"

        Dim enc As New UTF8Encoding()
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(sPassword)))


        'Using ds As Stream = myReq.GetRequestStream()
        '    ds.Write(enc.GetBytes(sData), 0, sData.Length)
        'End Using

        Dim wr As WebResponse = myReq.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        Dim content As String = reader.ReadToEnd()
        Dim json_serializer As New JavaScriptSerializer()
        Dim routes_list As Object = json_serializer.DeserializeObject(content)

        Dim oData As Object
        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "data"
                    oData = l.Value
                    For Each o As Object In oData

                        Dim opt As New ListItem
                        For Each kvp As KeyValuePair(Of String, Object) In o
                            Select Case kvp.Key.ToLower()
                                Case "id"
                                    opt.Value = kvp.Value
                                Case "name"
                                    opt.Text = kvp.Value
                            End Select
                        Next
                        ddlBank.Items.Add(opt)
                    Next
            End Select
        Next
    End Sub
End Class
