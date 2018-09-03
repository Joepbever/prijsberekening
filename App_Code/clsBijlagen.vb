Imports System.Data
Imports System.Data.SqlClient

Public Class clsBijlagen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function iBijlagen(iPartijID As Long, sTypeID As String, iID As Long, sSoort As String, sTitle As String, sOmschrijving As String, sLocatie As String, sData As String, sInfo As String, iVolgorde As Integer) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Bijlagen (iPartijID, sTypeID, iID, sSoort, sTitle, sOmschrijving, sLocatie, sData, sInfo, iVolgorde) VALUES (@iPartijID, @sTypeID, @iID, @sSoort, @sTitle, @sOmschrijving, @sLocatie, @sData, @sInfo, @iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sTypeID", sTypeID)
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
        cmd.Parameters.AddWithValue("@sLocatie", sLocatie)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@iVolgorde", iVolgorde)
        Return CON.sDatatable(cmd)
    End Function
End Class
