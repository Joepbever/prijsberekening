Imports System.Data
Imports System.Data.SqlClient

Public Class clsBtwTarieven
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function scBtwTarief(sBtw As String) As String
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iBtwTarief FROM BtwTarieven WHERE (sBtw = @sBtw)"
        cmd.Parameters.AddWithValue("@sBtw", sBtw)
        Return CON.Scalar(cmd)
    End Function
End Class