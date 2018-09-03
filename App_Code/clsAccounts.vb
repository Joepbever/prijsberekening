Imports System.Data
Imports System.Data.SqlClient

Public Class clsAccounts
    Public dt As New DataTable
    Public dr As DataRow

    Private CON As New clsConnection
    Private cmd As New SqlCommand

    Public Function sAccountByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Accounts WHERE (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sAccountByUserID(sUserID As String, iPartijIDBeheerder As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Accounts WHERE (sUserID = @sUserID) AND iPartijIDBeheerder = @iPartijIDBeheerder"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sUserID", sUserID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function iscAccount(iPartijID As Long, iPersID As Long, sType As String, sStatus As String, dtDatumCreated As Date, dtDatumExpired As Date, sUserID As String, iPartijIDBeheerder As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Accounts (iPartijID, iPersID, sType, sStatus, dtDatumCreated, dtDatumExpired, sUserID, iPartijIDBeheerder) VALUES (@iPartijID, @iPersID, @sType, @sStatus, @dtDatumCreated, @dtDatumExpired, @sUserID, @iPartijIDBeheerder); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@dtDatumCreated", dtDatumCreated)
        cmd.Parameters.AddWithValue("@dtDatumExpired", dtDatumExpired)
        cmd.Parameters.AddWithValue("@sUserID", sUserID)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Scalar(cmd)
    End Function
End Class