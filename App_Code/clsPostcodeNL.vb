Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Math
Imports System.Xml

Public Class oData
    Public bOK As Boolean = False
    Public sStraat As String = ""
    Public sHuisnummer As String = ""
    Public sToevoeging As String = ""
    Public sPostCode As String = ""
    Public sWijkCode As String = ""
    Public sStraatCode As String = ""
    Public sPlaats As String = ""
    Public sGemeente As String = ""
    Public sProvincie As String = ""
    Public sLand As String = ""
    Public iLatitude As Double = 0
    Public iLongitude As Double = 0
End Class

Public Class clsPostcodeNL
    Inherits oData
    Private U As New clsUtility

    Public Sub AdresVanPostcode(ByVal sPostCode As String, sHuisnummer As String)
        Try
            Dim sUrl As String = "https://api.postcode.nl/xmlrpc"
            Dim oRequest As WebRequest = WebRequest.Create("https://api.postcode.nl/xmlrpc")

            oRequest.Method = "POST"
            Dim sPostData As String = File.ReadAllText(U.Locatie() & "Data\Resources\PostcodeNL\Adres.xml") 'E:\internet\gebaren-tolken\
            sPostData = sPostData.Replace("[key]", "USbKDOOIB097verkbehF4ALgDfs8XJGvTcwKzoZJahF")
            sPostData = sPostData.Replace("[secret]", "FlW8UjzGylc2vTZknPfMmllN5ysrqKls29QSXwkfsn3FNlUK7C")
            sPostData = sPostData.Replace("[postcode]", sPostCode)
            sPostData = sPostData.Replace("[huisnummer]", sHuisnummer)

            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(sPostData)
            oRequest.ContentLength = byteArray.Length

            Dim dataStream As Stream = oRequest.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

            Using oResponse As HttpWebResponse = TryCast(oRequest.GetResponse(), HttpWebResponse)
                ' Get the response stream  
                Dim reader As New StreamReader(oResponse.GetResponseStream())
                ' Read the whole contents and return as a string  
                Dim sXml As String = reader.ReadToEnd()

                Dim xmlDoc As New XmlDocument()
                xmlDoc.LoadXml(sXml)

                Dim xpath As String = "methodResponse/params/param/value/struct/member"
                Dim nodes = xmlDoc.SelectNodes(xpath)

                Dim sNaam As String

                For Each childrenNode As XmlNode In nodes
                    sNaam = childrenNode.SelectSingleNode("name").InnerText
                    Select Case sNaam
                        Case "street"
                            bOK = True
                            sStraat = childrenNode.SelectSingleNode("value").InnerText
                        Case "houseNumber"
                            sHuisnummer = childrenNode.SelectSingleNode("value").InnerText
                        Case "houseNumberAddition"
                            sToevoeging = childrenNode.SelectSingleNode("value").InnerText
                        Case "postcode"
                            sPostCode = childrenNode.SelectSingleNode("value").InnerText
                            sWijkCode = Mid(sPostCode, 1, 4)
                            sStraatCode = Mid(sPostCode, 5, 2)
                        Case "city"
                            sPlaats = childrenNode.SelectSingleNode("value").InnerText
                        Case "municipality"
                            sGemeente = childrenNode.SelectSingleNode("value").InnerText
                        Case "province"
                            sProvincie = childrenNode.SelectSingleNode("value").InnerText
                        Case "latitude"
                            iLatitude = childrenNode.SelectSingleNode("value").InnerText.Replace(".", ",")
                        Case "longitude"
                            iLongitude = childrenNode.SelectSingleNode("value").InnerText.Replace(".", ",")
                    End Select
                Next
                sLand = "Nederland"
            End Using
        Catch ex As Exception
            bOK = False
        End Try
    End Sub

    Public Function GetStringBetween2Strings(s As String, s1 As String, s2 As String) As String
        Dim rx As New Regex(s1 & "(.+?)" & s2)
        Dim m As Match = rx.Match(s)
        If m.Success Then
            Return m.Groups(1).ToString()
        Else
            Return ""
        End If
    End Function
End Class