
Imports System.Data.SqlClient
Imports System.Data

Public Class clsOpeningstijden

    Public dt As New DataTable
    Public dr As DataRow

    Public Function sOpeningsTijden(iPartijID As Long, sSoort As String) As DataTable
        Dim con As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT iStart, iEind, dtStart, dtEnd, sType, iDayOfWeek FROM Openingstijden WHERE (iPartijID = @iPartijID) AND sSoort = @sSoort"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        Return con.sDatatable(cmd)
    End Function


End Class