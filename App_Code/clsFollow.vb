Imports Microsoft.VisualBasic
Imports System.Web.Script.Serialization
Imports System.Data
Imports System.Data.SqlClient
Imports Sessies

Public Class clsFollow
    Inherits MasterPage

    Dim ser As JavaScriptSerializer = New JavaScriptSerializer()
    Dim sCookieName As String
    Dim sReferrerHost As String
    Dim sUserIp As String
    Dim sCurrentSessionKey As String
    Dim cCookie As HttpCookie
    Dim seSessies As New Sessies
    Dim R As HttpRequest
    Dim sCurrentPage As String
    Dim sPrevPage As String

    Public Sub New(sSessionID As String, R As HttpRequest, Optional sCookieName As String = "Follow")
        Me.sCurrentSessionKey = sSessionID

        Me.R = R
        Me.sUserIp = R.UserHostAddress
        Me.sCurrentPage = R.Url.ToString()
        Me.sReferrerHost = GetReferrerHost()

        Me.sCookieName = sCookieName

    End Sub

    Public Function UpdateCookie() As HttpCookie
        If CookieExists(sCookieName) Then
            'cookie bestond al
            cCookie = GetCookie(sCookieName)
            seSessies = ser.Deserialize(Of Sessies)(cCookie.Value)

            'Check if data for this session already exists
            If seSessies.seSessies.Last.sSessionKey <> sCurrentSessionKey Then
                'If not add new session
                seSessies.seSessies.Add(New Sessie)
                seSessies.seSessies.Last.sSessionKey = sCurrentSessionKey
                seSessies.seSessies.Last.sIPAdress = sUserIp
                seSessies.seSessies.Last.sLastVisited = Now
            End If
        Else
            'cookie bestond nog niet
            cCookie = CreateCookie(sCookieName)

            'Add new session
            seSessies.seSessies.Add(New Sessie)
            seSessies.seSessies.Last.sSessionKey = sCurrentSessionKey
            seSessies.seSessies.Last.sIPAdress = sUserIp
            seSessies.seSessies.Last.sLastVisited = Now
        End If

        sPrevPage = GetPrevPage()

        If sReferrerHost = "NULL" Then
            sReferrerHost = "Direct"
        Else
            sReferrerHost = CheckReffererHost()
        End If

        If sReferrerHost <> "Local traffic" Then
            seSessies = AddHost(sReferrerHost, seSessies)
        End If

        seSessies = AddPage(sCurrentPage, seSessies)

        Dim SecondsDiff As Long
        Dim sTotalTimeSpend As Long
        If sPrevPage <> "NULL" And sPrevPage = GetReferrerAbsolutePath() Then
            SecondsDiff = DateDiff(DateInterval.Second, Date.Parse(seSessies.seSessies.Last.sLastVisited), Now)
            sTotalTimeSpend = sTotalTimeSpend + SecondsDiff
            seSessies.seSessies.Last.lTotalTimeSpent += SecondsDiff
        End If

        'Add totaltimespend to the previous page
        seSessies = AddTime(sPrevPage, sTotalTimeSpend, seSessies)

        'Update/Add Last visited
        seSessies.seSessies.Last.sLastVisited = Now

        seSessies.seSessies.Last.sLastPage = sCurrentPage

        'Save serialized string in cookie
        cCookie.Value = ser.Serialize(seSessies)

        Return cCookie
        'SaveCookie(cCookie)
    End Function

    Protected Function CookieExists(sName As String) As Boolean
        Try
            Dim cCookie As HttpCookie = R.Cookies(sName)

            If cCookie Is Nothing Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw
            Return False
        End Try
    End Function

    Protected Function AddHost(sName As String, seSessies As Sessies) As Sessies
        Dim bAdded As Boolean = False
        Dim lIncrementSize As Long = 1

        Dim lCount As Long = 0
        'Loop over all the hosts
        For Each hHost As Host In seSessies.seSessies.Last.liHosts
            'Check is the host exists
            If hHost.sKey = sName Then
                seSessies.seSessies.Last.liHosts(lCount).lValue += lIncrementSize
                bAdded = True
            End If
            lCount += 1
        Next

        If Not bAdded Then
            'Add host 'manually'
            seSessies.seSessies.Last.liHosts.Add(New Host(sName, 1))
        End If

        Return seSessies
    End Function

    Protected Function AddPage(sName As String, seSessies As Sessies) As Sessies
        Dim bAdded As Boolean = False
        Dim lIncrementSize As Long = 1

        Dim lCount As Long = 0
        'Loop over all the pages
        For Each pPage As Pagina In seSessies.seSessies.Last.liPages
            'Check is the Pagina exists
            If pPage.sKey = sName Then
                seSessies.seSessies.Last.liPages(lCount).lValue += lIncrementSize
                bAdded = True
            End If
            lCount += 1
        Next

        If Not bAdded Then
            'Add page 'manually'
            seSessies.seSessies.Last.liPages.Add(New Pagina(sName, 1))
        End If

        Return seSessies
    End Function

    Protected Function AddTime(sName As String, lValue As Long, seSessies As Sessies) As Sessies
        Dim lCount As Long = 0
        'Loop over all the pages
        For Each pPage As Pagina In seSessies.seSessies.Last.liPages
            'Get the right page.
            If pPage.sKey = sName Then
                seSessies.seSessies.Last.liPages(lCount).lTotalTimeSpend += lValue
            End If
            lCount += 1
        Next

        Return seSessies
    End Function

    Protected Sub SaveCookie(cCookie As HttpCookie)
        Response.Cookies.Add(cCookie)
    End Sub

    Protected Function GetCookie(sName As String) As HttpCookie
        Dim cCookie As HttpCookie
        cCookie = R.Cookies(sName)

        Return cCookie
    End Function

    Protected Function CreateCookie(sName As String) As HttpCookie
        Dim cCookie As HttpCookie = New HttpCookie(sName)
        cCookie.Expires = Now.AddYears(2)
        Return cCookie
    End Function

    Protected Function GetReferrerHost() As String
        Dim sHost As String
        Try
            sHost = R.UrlReferrer.ToString()
        Catch ex As Exception
            sHost = "NULL"
        End Try
        Return sHost
    End Function

    Protected Function GetReferrerAbsolutePath() As String
        Dim sHost As String

        Try
            sHost = R.UrlReferrer.AbsolutePath
        Catch ex As Exception
            sHost = "NULL"
        End Try

        Return sHost
    End Function

    Protected Function CheckReffererHost() As String
        Dim sPath As String = R.UrlReferrer.AbsoluteUri.Split("?").Last
        Dim sDomain As String = R.UrlReferrer.Host

        Select Case True
            Case sDomain = R.Url.Host
                Return "Local traffic"
            Case sPath.Contains("gclid")
                Return sDomain & ": " & "gclid"
            Case sPath.Contains("test")
                Return sDomain & ": " & "test"
            Case Else
                Return sDomain
        End Select
    End Function

    Protected Function GetPrevPage() As String
        Dim sPage As String

        Try
            sPage = seSessies.seSessies.Last.sLastPage
        Catch ex As Exception
            sPage = "NULL"
        End Try

        Return sPage
    End Function
End Class

'Classes used for the JSON object<
Public Class Sessies
    Public seSessies As New List(Of Sessie)
End Class

Public Class Sessie
    Private CON As New clsConnection
    Private cmd As New SqlCommand

    Public sSessionKey As String
    Public sIPAdress As String
    Public sLastVisited As DateTime
    Public sLastPage As String
    Public lTotalTimeSpent As Long
    Public liPages As New List(Of Pagina)
    Public liHosts As New List(Of Host)
    Dim BP As New BasePage

    Public Sub save(iPartijIDBeheerder As Long, sEmail As String, Optional sFunctie As String = "")
        Dim iPersID As Long = 0
        Dim iPartijID As Long
        Dim P As New clsPersonen

        If BP.IsOnline() Then
            iPartijID = BP.sPartijID()
            Dim TPP As New clsTussPersonenPartijen
            TPP.dt = TPP.sTussPersonenPartijenByPartijIDRight(iPartijID)
            If TPP.dt.Rows.Count > 0 Then
                TPP.dr = TPP.dt.Rows(0)
                iPersID = TPP.dr.Item("iPersID")
            Else
                iPersID = P.iscPersoon("", "", "", "", "", "", "", "", "", sEmail, "", "", "", False, Date.Now, "", 0, 0, Date.Now, False, "", "", 0, iPartijIDBeheerder)
            End If
        Else
            iPersID = P.iscPersoon("", "", "", "", "", "", "", "", "", sEmail, "", "", "", False, Date.Now, "", 0, 0, Date.Now, False, "", "", 0, iPartijIDBeheerder)
        End If

        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Follow (iPartijIDBeheerder, sSessionKey, sIPAdress, dtLastVisited, sLastPage, iTotalTimeSpent, sEmail, iPersID, sFunctie)" &
                            " VALUES (@iPartijIDBeheerder, @sSessionKey, @sIPAdress, @dtLastVisited, @sLastPage, @iTotalTimeSpent, @sEmail, @iPersID, @sFunctie); SELECT scope_identity()"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sSessionKey", sSessionKey)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sIPAdress", sIPAdress)
        cmd.Parameters.AddWithValue("@dtLastVisited", sLastVisited)
        cmd.Parameters.AddWithValue("@sLastPage", sLastPage)
        cmd.Parameters.AddWithValue("@iTotalTimeSpent", lTotalTimeSpent)
        cmd.Parameters.AddWithValue("@sFunctie", sFunctie)
        Dim iSessionID = CON.Scalar(cmd)

        For Each page As Pagina In liPages
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO FollowPages (iFollowID, sKey, iValue, iTotalTimeSpend)" &
                                " VALUES (@FollowID, @sKey, @lValue, @lTotalTimeSpend);"
            cmd.Parameters.AddWithValue("@FollowID", iSessionID)
            cmd.Parameters.AddWithValue("@sKey", page.sKey)
            cmd.Parameters.AddWithValue("@lValue", page.lValue)
            cmd.Parameters.AddWithValue("@lTotalTimeSpend", page.lTotalTimeSpend)
            CON.Scalar(cmd)
        Next

        For Each host As Host In liHosts
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO FollowHosts (iFollowID, sKey, iValue)" &
                                " VALUES (@FollowID, @sKey, @lValue);"
            cmd.Parameters.AddWithValue("@FollowID", iSessionID)
            cmd.Parameters.AddWithValue("@sKey", host.sKey)
            cmd.Parameters.AddWithValue("@lValue", host.lValue)
            CON.Scalar(cmd)
        Next
    End Sub


    Public Sub saveByIPersID(iPersID As Long, iPartijIDBeheerder As Long, sEmail As String, Optional sFunctie As String = "")

        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Follow (iPartijIDBeheerder, sSessionKey, sIPAdress, dtLastVisited, sLastPage, iTotalTimeSpent, sEmail, iPersID, sFunctie)" &
                            " VALUES (@iPartijIDBeheerder, @sSessionKey, @sIPAdress, @dtLastVisited, @sLastPage, @iTotalTimeSpent, @sEmail, @iPersID, @sFunctie); SELECT scope_identity()"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sSessionKey", sSessionKey)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sIPAdress", sIPAdress)
        cmd.Parameters.AddWithValue("@dtLastVisited", sLastVisited)
        cmd.Parameters.AddWithValue("@sLastPage", sLastPage)
        cmd.Parameters.AddWithValue("@iTotalTimeSpent", lTotalTimeSpent)
        cmd.Parameters.AddWithValue("@sFunctie", sFunctie)
        Dim iSessionID = CON.Scalar(cmd)

        For Each page As Pagina In liPages
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO FollowPages (iFollowID, sKey, iValue, iTotalTimeSpend)" &
                                " VALUES (@FollowID, @sKey, @lValue, @lTotalTimeSpend);"
            cmd.Parameters.AddWithValue("@FollowID", iSessionID)
            cmd.Parameters.AddWithValue("@sKey", page.sKey)
            cmd.Parameters.AddWithValue("@lValue", page.lValue)
            cmd.Parameters.AddWithValue("@lTotalTimeSpend", page.lTotalTimeSpend)
            CON.Scalar(cmd)
        Next

        For Each host As Host In liHosts
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO FollowHosts (iFollowID, sKey, iValue)" &
                                " VALUES (@FollowID, @sKey, @lValue);"
            cmd.Parameters.AddWithValue("@FollowID", iSessionID)
            cmd.Parameters.AddWithValue("@sKey", host.sKey)
            cmd.Parameters.AddWithValue("@lValue", host.lValue)
            CON.Scalar(cmd)
        Next
    End Sub

End Class

Public Class Host
    Public sKey As String
    Public lValue As Long

    Public Sub New()

    End Sub

    Public Sub New(sKey As String)
        Me.sKey = sKey
    End Sub

    Public Sub New(sKey As String, lValue As Long)
        Me.sKey = sKey
        Me.lValue = lValue
    End Sub
End Class

Public Class Pagina
    Public sKey As String
    Public lValue As Long
    Public lTotalTimeSpend As Long

    Public Sub New()

    End Sub

    Public Sub New(sKey As String)
        Me.sKey = sKey
    End Sub

    Public Sub New(sKey As String, lValue As Long)
        Me.sKey = sKey
        Me.lValue = lValue
    End Sub
End Class
'Classes used for the JSON object>