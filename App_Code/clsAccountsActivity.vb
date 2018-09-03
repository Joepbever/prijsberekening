Imports System.Data.SqlClient
Imports System.Data

Public Class clsAccountsActivity
    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public dt As New DataTable
    Public dr As DataRow

    Public Function iActivity(iPartijIDBeheerder As Long, iPartijID As Long, dtLogin As DateTime, dtLogout As DateTime, sActie As String) As Boolean

        Dim iPersID = 0
        Dim sUserID = ""

        Dim ACC As New clsAccounts

        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
            sUserID = ACC.dr.Item("sUserID")
        End If

        Dim sIpAddress = HttpContext.Current.Request.UserHostAddress
        Dim sBrouwser = HttpContext.Current.Request.Browser.Browser
        Dim sOperatingSysteem = HttpContext.Current.Request.Browser.Platform
        Dim sBrouwserVersion = HttpContext.Current.Request.Browser.Version

        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO AccountsActivity VALUES (@iPartijIDBeheerder, @iPersID, @sUserID, @dtLogin, @dtLogout, @sIpAddress, @sBrouwser, @sBrouwserVersion, @sOperatingSysteem, @clear, @clear, @clear, @sActie)"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sUserID", sUserID)
        cmd.Parameters.AddWithValue("@dtLogin", dtLogin)
        cmd.Parameters.AddWithValue("@dtLogout", dtLogout)
        cmd.Parameters.AddWithValue("@sIpAddress", sIpAddress)
        cmd.Parameters.AddWithValue("@sBrouwser", sBrouwser)
        cmd.Parameters.AddWithValue("@sBrouwserVersion", sBrouwserVersion)
        cmd.Parameters.AddWithValue("@clear", "")
        cmd.Parameters.AddWithValue("@sOperatingSysteem", sOperatingSysteem)
        cmd.Parameters.AddWithValue("@sActie", sActie)
        Return CON.Update(cmd)

    End Function

    Public Function sActivityByiPartijID(iPartijID As Long) As DataTable

        Dim iPersID = -1
        Dim ACC As New clsAccounts

        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
        End If

        If iPersID > 0 Then
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM AccountsActivity WHERE iPersID = @iPersID ORDER BY iAccountsActivityID"
            cmd.Parameters.AddWithValue("@iPersID", iPersID)
            Return CON.sDatatable(cmd)
        Else
            Return Nothing
        End If

    End Function

    Public Function sActivityByiPartijID(iPartijID As Long, page As Integer) As DataTable

        Dim iPersID = -1
        Dim ACC As New clsAccounts

        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
        End If


        Dim limit = 5
        Dim offset = 0

        If page = 0 Or page = 1 Then
            offset = 0
        Else
            offset = (page - 1) * limit
        End If

        If iPersID > 0 Then
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM AccountsActivity WHERE iPersID = @iPersID ORDER BY iAccountsActivityID OFFSET @off ROWS FETCH NEXT @limit ROWS ONLY;"
            cmd.Parameters.AddWithValue("@iPersID", iPersID)
            cmd.Parameters.AddWithValue("@limit", limit)
            cmd.Parameters.AddWithValue("@off", offset)

            Return CON.sDatatable(cmd)
        Else
            Return Nothing
        End If

    End Function

    Public Function sNumberOfPages(iPartijID As Long, limit As Integer) As Integer
        Dim iPersID = -1
        Dim ACC As New clsAccounts

        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
        End If

        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT COUNT(*) / @limit FROM AccountsActivity WHERE iPersID = @iPersID"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@limit", limit)
        Return CON.Scalar(cmd)

    End Function
End Class
