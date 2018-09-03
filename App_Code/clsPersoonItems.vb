Imports System.Data
Imports System.Data.SqlClient

Public Class clsPersoonItems
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function iPersoonItem(iPersID As Long, sType As String, sWaarde As String, sOmschrijving As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO PersoonItems (iPersID, sType, sWaarde, sOmschrijving) VALUES (@iPersID, @sType, @sWaarde, @sOmschrijving)"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sWaarde", sWaarde)
        cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sPersoonItemsByPersIDAndType(iPersID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM PersoonItems WHERE (iPersID = @iPersID) AND sType = @sType"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sPersoonItemsByPersID(iPersID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM PersoonItems WHERE (iPersID = @iPersID)"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        Return CON.sDatatable(cmd)
    End Function
End Class