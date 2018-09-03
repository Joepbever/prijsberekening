Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization

Public Class clsMyParcel

    Dim BP As New BasePage
    Public Function AddShipment(sCountryCode As String, sCity As String, sStreet As String, sNumber As String, sAddition As String, sPostalcode As String, sPerson As String, sPhone As String, sEmail As String) As String

        Dim ST As New clsSettings

        Dim sUrl As String = ST.scSettingByTypeAndGroup("Instellingen", "MyParcelURL", BP.iPartijIDBeheerder) & "shipments"
        Dim sPassword As String = ST.scSettingByTypeAndGroup("Instellingen", "MyParcelPassword", BP.iPartijIDBeheerder)

        Dim data As String = "{	""data"": {	""shipments"": [ { ""recipient"": { ""cc"": """ & sCountryCode & """, ""city"":	""" & sCity & """, ""street"":	""" & sStreet & """, ""number"": """ & sNumber & """, ""number_suffix"": """ & sAddition & """, ""postal_code"":	""" & sPostalcode & """, ""person"":	""" & sPerson & """,	""phone"":	""" & sPhone & """, ""email"":	""" & sEmail & """ }, ""options"": { ""package_type"":	1, ""only_recipient"":	0, ""signature"": 0, ""return"": 0, ""large_format"": 0 }, ""carrier"":	1 } ] } } "

        Dim myReq As WebRequest = WebRequest.Create(sUrl)
        myReq.Method = "POST"
        myReq.ContentLength = data.Length
        myReq.ContentType = "application/vnd.shipment+json;	charset=utf-8"

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

        Dim iMyParcelID As Long = 0
        For Each l As KeyValuePair(Of String, Object) In routes_list
            Select Case l.Key.ToLower()
                Case "id"
                Case "data"
                    Dim oLinks As Object = l.Value
                    For Each kvp As KeyValuePair(Of String, Object) In oLinks
                        Select Case kvp.Key.ToLower()
                            Case "id"
                                iMyParcelID = kvp.Value
                            Case "redirecturl"
                                'Dim sRedirectURL As String = kvp.Value 'Wordt van te voren meegestuurd
                        End Select
                    Next
            End Select
        Next
        Return iMyParcelID

    End Function
End Class
