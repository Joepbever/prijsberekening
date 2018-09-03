Imports System.Data
Imports System.Data.SqlClient

Public Class clsRoutes

    Public dt As New DataTable
    Public dr As DataRow

    Public Function sRoutes(sType As String, iID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM Routes WHERE (sType = @sType) AND iID = @iID"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iID", iID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sRoutes(sType As String, iID As Long, iTaalID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM Routes WHERE (sType = @sType) AND iID = @iID AND iTaalID = @iTaalID"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sRoutesByTaalID(iPartijIDBeheerder As Long, sType As String, iTaalID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM Routes WHERE iPartijIDbeheerder = @iPartijIDbeheerder AND sType = @sType AND iTaalID = @iTaalID"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    'Public Function uPartijItem(iRouteID As Long, iPartijIDbeheerder As Long, iTaalID As String, sWaarde As String, sType As String, sType As String) As Long
    '    Dim CON As New clsConnection
    '    Dim cmd As New SqlCommand
    '    cmd.CommandText = "UPDATE PartijItems SET iRouteID = @iRouteID, iPartijIDbeheerder = @iPartijIDbeheerder, iTaalID = @iTaalID, iID = @iID, sType = @sType WHERE (iPartijItemID = @iPartijItemID) AND (iPartijID = @iPartijID)"
    '    cmd.Parameters.AddWithValue("@iRouteID", iRouteID)
    '    cmd.Parameters.AddWithValue("@iPartijIDbeheerder", iPartijIDbeheerder)
    '    cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
    '    cmd.Parameters.AddWithValue("@iID", iID)
    '    cmd.Parameters.AddWithValue("@iPartijItemID", iPartijItemID)
    '    cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
    '    Return CON.Scalar(cmd)
    'End Function

    Public Function iRoute(iPartijIDbeheerder As Long, iTaalID As Long, iID As Long, sType As String, sTitle As String, sURL As String) As Long

        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "INSERT INTO Routes (iPartijIDbeheerder, iTaalID, iID, sType, sTitle, sURL) VALUES (@iPartijIDbeheerder, @iTaalID, @iID, @sType, @sTitle, @sURL)"
        cmd.Parameters.AddWithValue("@iPartijIDbeheerder", iPartijIDbeheerder)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@sURL", sURL)
        Return CON.Scalar(cmd)
    End Function

    Public Function dRoute(sType As String, iID As Long) As Boolean
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "DELETE FROM Routes WHERE sType = @sType AND iID = @iID"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iID", iID)
        Return CON.Update(cmd)
    End Function


    Public Function sPartijItemsByPartijID(iPartijIDbeheerder As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM Routes WHERE (iPartijIDbeheerder = @iPartijIDbeheerder)"
        cmd.Parameters.AddWithValue("@iPartijIDbeheerder", iPartijIDbeheerder)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sRoutes(iPartijIDBeheerder As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM Routes WHERE iPartijIDbeheerder = @iPartijIDbeheerder"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.sDatatable(cmd)
    End Function
End Class