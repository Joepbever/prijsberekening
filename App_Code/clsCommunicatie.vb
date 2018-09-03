Imports System.Data
Imports System.Data.SqlClient

Public Class clsCommunicatie
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function iCommunicatie(iPers As Long, iPartijID As Long, sType As String, sBericht As String, iAangemaaktDoorPersID As Long) As DataTable
        cmd.Parameters.Clear()

        Dim regDate As Date = Date.Now
        cmd.CommandText = "INSERT INTO Communicatie (iPersID, iPartijID, sType, sBericht, iAangemaaktDoorPersID, dtDatum) VALUES (@iPersID, @iPartijID, @sType, @sBericht, @iAangemaaktDoorPersID, @dtDatum)"
        cmd.Parameters.AddWithValue("@iPersID", iPers)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sBericht", sBericht)
        cmd.Parameters.AddWithValue("@iAangemaaktDoorPersID", iAangemaaktDoorPersID)
        cmd.Parameters.AddWithValue("@dtDatum", regDate)
        Return CON.sDatatable(cmd)
    End Function
End Class
