Imports Newtonsoft.Json
'Imports Sessies

Partial Class cookie
    Inherits System.Web.UI.Page
    Dim sCookieName As String
    Dim cCookie As HttpCookie
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        sCookieName = "Follow"
        cCookie = Request.Cookies(sCookieName)

        'Dim json = cCookie.Value
        'Response.Clear()
        'Response.ContentType = "application/json; charset=utf-8"
        'Response.Write(json)
        'Response.End()

    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim sSessie As Sessie = JsonConvert.DeserializeObject(Of Sessies)(cCookie.Value).seSessies(0)
        Dim BP As New BasePage

        sSessie.save(BP.iPartijIDBeheerder, "lukasz@websentiment.nl")

    End Sub
End Class
'Dim ser As JavaScriptSerializer = New JavaScriptSerializer()
'Dim sCurrentPage As String = HttpContext.Current.Request.Url.AbsolutePath
'Dim sReferrerHost As String = GetReferrerHost()
'Dim sUserIp As String = Request.UserHostAddress
'Dim sCurrentSessionKey As String = Session.SessionID

'If CookieExists(sCookieName) Then
'    'cookie bestond al
'    cCookie = GetCookie(sCookieName)
'    seSessies = ser.Deserialize(Of Sessies)(cCookie.Value)

'    'Check if data for this session already exists
'    If seSessies.seSessies.Last.sSessionKey <> sCurrentSessionKey Then
'        'If not add new session
'        seSessies.seSessies.Add(New Sessie)
'        seSessies.seSessies.Last.sSessionKey = sCurrentSessionKey
'        seSessies.seSessies.Last.sIPAdress = sUserIp
'        seSessies.seSessies.Last.sLastVisited = Now
'    End If
'Else
'    'cookie bestond nog niet
'    cCookie = CreateCookie(sCookieName)

'    'Add new session
'    seSessies.seSessies.Add(New Sessie)
'    seSessies.seSessies.Last.sSessionKey = sCurrentSessionKey
'    seSessies.seSessies.Last.sIPAdress = sUserIp
'    seSessies.seSessies.Last.sLastVisited = Now
'End If

'seSessies = AddHost(sReferrerHost, seSessies)
'seSessies = AddPage(sCurrentPage, seSessies)

'Dim SecondsDiff As Long
'Dim sTotalTimeSpend As Long
'If GetPrevPage() <> "NULL" And GetPrevPage() = GetReferrerAbsolutePath() Then
'    SecondsDiff = DateDiff(DateInterval.Second, Date.Parse(seSessies.seSessies.Last.sLastVisited), Now)
'    sTotalTimeSpend = sTotalTimeSpend + SecondsDiff
'End If

''Add totaltimespend to the previous page
'seSessies = AddTime(GetPrevPage(), sTotalTimeSpend, seSessies)

''Update/Add Last visited
'seSessies.seSessies.Last.sLastVisited = Now

'seSessies.seSessies.Last.sLastPage = sCurrentPage

''Save serialized string in cookie
'cCookie.Value = ser.Serialize(seSessies)

'SaveCookie(cCookie)

'Protected Function AddHost(sName As String, seSessies As Sessies) As Sessies
'    Dim bAdded As Boolean = False
'    Dim lIncrementSize As Long = 1

'    Dim lCount As Long = 0
'    'Loop over all the hosts
'    For Each hHost As Host In seSessies.seSessies.Last.liHosts
'        'Check Is the host exists
'        If hHost.sKey = sName Then
'            seSessies.seSessies.Last.liHosts(lCount).lValue += lIncrementSize
'            bAdded = True
'        End If
'        lCount += 1
'    Next

'    If Not bAdded Then
'        'Add Host 'manually'
'        seSessies.seSessies.Last.liHosts.Add(New Host(sName, 1))
'    End If

'    Return seSessies
'End Function

'Protected Function AddPage(sName As String, seSessies As Sessies) As Sessies
'    Dim bAdded As Boolean = False
'    Dim lIncrementSize As Long = 1

'    Dim lCount As Long = 0
'    'Loop over all the pages
'    For Each pPage As Pagina In seSessies.seSessies.Last.liPages
'        'Check Is the Pagina exists
'        If pPage.sKey = sName Then
'            seSessies.seSessies.Last.liPages(lCount).lValue += lIncrementSize
'            bAdded = True
'        End If
'        lCount += 1
'    Next

'    If Not bAdded Then
'        'Add Page 'manually'
'        seSessies.seSessies.Last.liPages.Add(New Pagina(sName, 1))
'    End If

'    Return seSessies
'End Function

'Protected Function AddTime(sName As String, lValue As Long, seSessies As Sessies) As Sessies
'    Dim lCount As Long = 0
'    'Loop over all the pages
'    For Each pPage As Pagina In seSessies.seSessies.Last.liPages
'        'Get the right page.
'        If pPage.sKey = sName Then
'            seSessies.seSessies.Last.liPages(lCount).lTotalTimeSpend += lValue
'        End If
'        lCount += 1
'    Next

'    Return seSessies
'End Function

'Protected Sub SaveCookie(cCookie As HttpCookie)
'    Response.Cookies.Add(cCookie)
'End Sub

'Protected Function CookieExists(sName As String) As Boolean
'    If Request.Cookies(sName) Is Nothing Then
'        Return False
'    Else
'        Return True
'    End If
'End Function
'Protected Function GetReferrerHost() As String
'    Dim sHost As String

'    Try
'        sHost = Request.UrlReferrer.Host
'    Catch ex As Exception
'        sHost = "Direct/Refesh"
'    End Try

'    Return sHost
'End Function

'Protected Function GetReferrerAbsolutePath() As String
'    Dim sHost As String

'    Try
'        sHost = Request.UrlReferrer.AbsolutePath
'    Catch ex As Exception
'        sHost = "NULL"
'    End Try

'    Return sHost
'End Function

'Protected Function GetPrevPage() As String
'    Dim sPage As String

'    Try
'        sPage = seSessies.seSessies.Last.sLastPage
'    Catch ex As Exception
'        sPage = "NULL"
'    End Try

'    Return sPage
'End Function


'Public Class Sessies
'    Public seSessies As New List(Of Sessie)
'End Class

'Public Class Sessie
'    Public sSessionKey As String
'    Public sIPAdress As String
'    Public sLastVisited As String
'    Public sLastPage As String
'    Public liPages As New List(Of Pagina)
'    Public liHosts As New List(Of Host)
'End Class

'Public Class Host
'    Public sKey As String
'    Public lValue As Long

'    Public Sub New()

'    End Sub

'    Public Sub New(sKey As String)
'        Me.sKey = sKey
'    End Sub

'    Public Sub New(sKey As String, lValue As Long)
'        Me.sKey = sKey
'        Me.lValue = lValue
'    End Sub
'End Class

'Public Class Pagina
'    Public sKey As String
'    Public lValue As Long
'    Public lTotalTimeSpend As Long

'    Public Sub New()

'    End Sub

'    Public Sub New(sKey As String)
'        Me.sKey = sKey
'    End Sub

'    Public Sub New(sKey As String, lValue As Long)
'        Me.sKey = sKey
'        Me.lValue = lValue
'    End Sub
'End Class
