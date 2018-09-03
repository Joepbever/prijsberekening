Imports System.Data
Imports System.Data.SqlClient

Public Class clsTalen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sTalenByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iTaalID, iPartijID, sCulture, sCode, sLand, sInfo, sData, iVolgorde FROM Talen WHERE (iPartijID = @iPartijID) ORDER BY iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sTaalByLanguage(sLanguage As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Talen WHERE (sCode = @sLanguage) And (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sLanguage", sLanguage)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function
End Class