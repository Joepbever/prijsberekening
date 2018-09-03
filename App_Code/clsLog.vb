Imports System.Data.SqlClient

Public Class clsLog
    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Sub iLog(ByVal sPage As String, ByVal sFunctie As String, ByVal sOmschrijving As String)
        Dim U As New clsUtility
        If U.Config("Test") = "True" Then
            Dim sBrowserInfo As String = ""
            Try
                sBrowserInfo &= "UserAgent:  " & HttpContext.Current.Request.UserAgent & vbCrLf
                sBrowserInfo &= "Browser:  " & HttpContext.Current.Request.Browser.Browser & vbCrLf
                sBrowserInfo &= "Platform:  " & HttpContext.Current.Request.Browser.Platform & vbCrLf
                sBrowserInfo &= "Version:  " & HttpContext.Current.Request.Browser.Version & vbCrLf
                sBrowserInfo &= "MajorVersion:  " & HttpContext.Current.Request.Browser.MajorVersion() & vbCrLf
                sBrowserInfo &= "MinorVersion:  " & HttpContext.Current.Request.Browser.MinorVersion() & vbCrLf
                sBrowserInfo &= "UserHostAddress (IP):  " & HttpContext.Current.Request.UserHostAddress & vbCrLf
                sBrowserInfo &= "UserHostName:  " & HttpContext.Current.Request.UserHostName & vbCrLf
                sBrowserInfo &= "VBScript:  " & HttpContext.Current.Request.Browser.VBScript & vbCrLf
                sBrowserInfo &= "JScriptVersion:  " & HttpContext.Current.Request.Browser.JScriptVersion.Major & vbCrLf
                sBrowserInfo &= "Crawler:  " & HttpContext.Current.Request.Browser.Crawler & vbCrLf
                sBrowserInfo &= "AOL:  " & HttpContext.Current.Request.Browser.AOL & vbCrLf
                sBrowserInfo &= "Win32:  " & HttpContext.Current.Request.Browser.Win32 & vbCrLf
                sBrowserInfo &= "Frames:  " & HttpContext.Current.Request.Browser.Frames & vbCrLf
                sBrowserInfo &= "ScreenPixelsHeight:  " & HttpContext.Current.Request.Browser.ScreenPixelsHeight & vbCrLf
                sBrowserInfo &= "ScreenPixelsWidth:  " & HttpContext.Current.Request.Browser.ScreenPixelsWidth & vbCrLf
                sBrowserInfo &= "PhysicalPath:  " & HttpContext.Current.Request.PhysicalPath & vbCrLf
                sBrowserInfo &= "UrlReferrer:  " & HttpContext.Current.Request.UrlReferrer.ToString() & vbCrLf
            Catch ex As Exception

            End Try

            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO Log (sPage, sFunctie, sOmschrijving, dDate) VALUES (@sPage, @sFunctie, @sOmschrijving, @dDate)"
            cmd.Parameters.AddWithValue("@sPage", sPage)
            cmd.Parameters.AddWithValue("@sFunctie", sFunctie)
            cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
            cmd.Parameters.AddWithValue("@dDate", Now)
            CON.Update(cmd)
        End If

    End Sub
End Class
