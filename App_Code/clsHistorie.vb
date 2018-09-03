Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class clsHistorie
    Public dt As New DataTable
    Public dr As DataRow

    Private CON As New clsConnection
    Private cmd As New SqlCommand

    Public Function iHistorie(iPartijID As Long, iPersID As Long, sTypeID As String, iID As Long, sSoort As String, sTitle As String, sOmschrijving As String, sData As String, sInfo As String) As String
        cmd.Parameters.Clear()

        cmd.CommandText = "INSERT INTO Historie (iPartijID, iPersID, sTypeID, iID, sSoort, sTitle, sOmschrijving, sData, sInfo, iVolgorde, dtDatum) VALUES (@iPartijID, @iPersID, @sTypeID, @iID, @sSoort, @sTitle, @sOmschrijving, @sData, @sInfo, @iVolgorde, @dtDatum)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sTypeID", sTypeID)
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@iVolgorde", 0)
        cmd.Parameters.AddWithValue("@dtDatum", Now.AddMonths(3))

        Return CON.Scalar(cmd)

    End Function

    Public Function sHistorieByPersIdAndStatus(iPersID As Long, sTypeID As String) As DataTable

        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Historie WHERE iPersID = @iPersID AND sTypeID = @sTypeID ORDER BY iHistorieID DESC"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sTypeID", sTypeID)

        Return CON.sDatatable(cmd)

    End Function
End Class
