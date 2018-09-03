
Imports System.Data
Imports System.Data.SqlClient

Public Class clsFacItems
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand


    Public Function iFacItems(iFacKopID As Long, sType As String, sWaarde As String, sOmschrijving As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO FacItems (iFacKopID, sType, sWaarde, sOmschrijving) VALUES (@iFacKopID, @sType, @sWaarde, @sOmschrijving)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sWaarde", sWaarde)
        cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacItemsByFackKopID(iFacKopID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iFacItemID, iFacKopID, sType, sWaarde, sOmschrijving FROM FacItems WHERE (iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.sDatatable(cmd)
    End Function

    Public sTimestamp As String = ""
    Public sVerzendkosten As String = ""
    Public sSession As String = ""
    Public Sub sFacItems(iFacKopID As Long)
        dt = sFacItemsByFackKopID(iFacKopID)
        For Each Me.dr In dt.Rows
            Select Case dr.Item("sType").ToLower()
                Case "timestamp"
                    sTimestamp = dr.Item("sWaarde")
                Case "verzendkosten"
                    sVerzendkosten = dr.Item("sWaarde")
                Case "session"
                    sSession = dr.Item("sWaarde")
            End Select
        Next
    End Sub
End Class
