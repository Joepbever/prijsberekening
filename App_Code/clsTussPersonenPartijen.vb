Imports System.Data
Imports System.Data.SqlClient

Public Class clsTussPersonenPartijen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function iTussPersonenPartijen(iPersID As Long, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO tussPersonenPartijen (iPersID, iPartijID) VALUES (@iPersID, @iPartijID)"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sTussPersonenPartijenByPartijIDRight(iPartijID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand

        cmd.CommandText = "SELECT * FROM tussPersonenPartijen RIGHT JOIN Personen ON tussPersonenPartijen.iPersID = Personen.iPersID WHERE (tussPersonenPartijen.iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function
End Class
