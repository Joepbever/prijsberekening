Imports System.Data
Imports System.Data.SqlClient

Public Class clsZendingen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sZendingByID(iZendingID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Zendingen FROM FacKop WHERE iZendingID = @iZendingID"
        cmd.Parameters.AddWithValue("@iZendingID", iZendingID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sZendingByFacKopID(iFacKopID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Zendingen WHERE iFacKopID = @iFacKopID"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function iZending(iFacKopID As Long, ByVal sMyParcelID As String, ByVal sCountryCode As String, ByVal sCity As String, ByVal sStreet As String,
                            ByVal sNumber As String, ByVal sAddition As String, sPostalcode As String, ByVal sPerson As String, ByVal sPhone As String, ByVal sEmail As String) As Long

        cmd.CommandText = "INSERT INTO Zendingen (iFacKopID, sMyParcelID, sCountryCode, sCity, sStreet, sNumber, sAddition, sPostalcode, sPerson, sPhone, sEmail) VALUES (@iFacKopID, @sMyParcelID, @sCountryCode, @sCity, @sStreet, @sNumber, @sAddition, @sPostalcode, @sPerson, @sPhone, @sEmail); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sMyParcelID", sMyParcelID)
        cmd.Parameters.AddWithValue("@sCountryCode", sCountryCode)
        cmd.Parameters.AddWithValue("@sCity", sCity)
        cmd.Parameters.AddWithValue("@sStreet", sStreet)
        cmd.Parameters.AddWithValue("@sNumber", sNumber)
        cmd.Parameters.AddWithValue("@sAddition", sAddition)
        cmd.Parameters.AddWithValue("@sPostalcode", sPostalcode)
        cmd.Parameters.AddWithValue("@sPerson", sPerson)
        cmd.Parameters.AddWithValue("@sPhone", sPhone)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        Return CON.Scalar(cmd)
    End Function
End Class