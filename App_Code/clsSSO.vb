Imports System.Data
Imports System.Data.SqlClient

Public Class clsSSO
    Public dt As New DataTable
    Public dr As DataRow

    Public Function iSSO(iPartijIDBeheerder As Long, sUserID As String, sGuid As String, sPageGuid As String, dtDatum As Date, bActive As Boolean, iTicks As Long) As Boolean
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "INSERT INTO SSO (iPartijIDBeheerder, sUserID, sGuid, sPageGuid, dtDatum, bActive, iTicks) VALUES (@iPartijIDBeheerder, @sUserID, @sGuid, @sPageGuid, @dtDatum, @bActive, @iTicks)"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sUserID", sUserID)
        cmd.Parameters.AddWithValue("@sGuid", sGuid)
        cmd.Parameters.AddWithValue("@sPageGuid", sPageGuid)
        cmd.Parameters.AddWithValue("@dtDatum", dtDatum)
        cmd.Parameters.AddWithValue("@bActive", bActive)
        cmd.Parameters.AddWithValue("@iTicks", iTicks)
        CON.Update(cmd)
        Return True
    End Function

    Public Function sSSO(sGuid As String, iPartijIDBeheerder As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM SSO WHERE (sGuid = @sGuid) AND (iPartijIDBeheerder = @iPartijIDBeheerder)"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sGuid", sGuid)
        Return CON.sDatatable(cmd)
    End Function

    Public Function uActive(bActive As Boolean, iSSOID As Long, iPartijIDBeheerder As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "UPDATE SSO SET bActive = @bActive WHERE iSSOID = @iSSOID AND iPartijIDBeheerder = @iPartijIDBeheerder"
        cmd.Parameters.AddWithValue("@iSSOID", iSSOID)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@bActive", bActive)
        Return CON.sDatatable(cmd)
    End Function
End Class
