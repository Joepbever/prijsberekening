Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json

Public Class clsRNG

    Public Class drRNGResult
        Public Property jsonrpc As String
        Public Property drawId As String
        Public Property status As String
        Public Property entryCount As Integer
        Public Property winners As String
        Public Property completionTime As String
        Public Property recordUrl As String
        Public Property errorCode As String
        Public Property id As Integer
    End Class

    Public jsonString As String
    Public result As New drRNGResult

    Public Function CreateJson(title As String, recordType As String, entries As String(), entriesDigest As String, winnerCount As Integer, id As Integer) As String

        Dim entriesString As String = ""
        Dim username As String = "websnti"
        Dim password As String = "BVyJADJANu"
        For index = 0 To entries.Length - 1
            Dim jsonToAdd As String
            If index = 0 Then
                jsonToAdd = "[ """ + entries(index) + ""","
            ElseIf index = entries.Length - 1 Then
                jsonToAdd = " """ + entries(index) + """]"
            Else
                jsonToAdd = """" + entries(index) + ""","
            End If

            entriesString = jsonToAdd
        Next

        Me.jsonString = "{ ""jsonrpc"":  ""2.0"", ""method"": ""holdDraw"", ""params"":{ ""credentials"":{ ""login"": "" " + username + " "", ""password"": "" " + password + " ""}, ""title"": "" " + title + " "", ""recordType"": "" " + recordType + " "", ""entries"": " + entriesString + ", ""entriesDigest"": "" " + entriesDigest + " "", ""winnerCount"":  " + winnerCount.ToString() + " }, ""id"": " + id.ToString() + "}"
        Return Me.jsonString
    End Function

    Public Sub StartDraw()

        'Dim httpWebRequest As WebRequest = WebRequest.Create("https://draws.random.org/api/json-rpc/2/invoke")
        'httpWebRequest.ContentType = "application/json-rpc"
        'httpWebRequest.Method = "POST"

        'Using StreamWriter As New StreamWriter(httpWebRequest.GetRequestStream())
        '    StreamWriter.Write(jsonString)
        '    StreamWriter.Flush()
        '    StreamWriter.Close()

        '    Dim HttpResponse = httpWebRequest.GetResponse()

        '    Using StreamReader As New StreamReader(HttpResponse.GetResponseStream())
        '        Dim result = StreamReader.ReadToEnd()
        '        GetResult(result)
        '    End Using
        'End Using

    End Sub

    Public Sub GetResult(result As String)

        Dim jsonResult = JsonConvert.DeserializeObject(result)
        Me.result.completionTime = jsonResult("result")("completionTime")
        Me.result.drawId = jsonResult("result")("drawId")
        Me.result.entryCount = jsonResult("result")("entryCount")
        Me.result.errorCode = jsonResult("error").ToString()
        Me.result.id = jsonResult("id")
        Me.result.jsonrpc = jsonResult("jsonrpc")
        Me.result.recordUrl = jsonResult("result")("recordUrl")
        Me.result.status = jsonResult("result")("status")
        Me.result.winners = jsonResult("result")("winners")(0)

    End Sub

End Class
