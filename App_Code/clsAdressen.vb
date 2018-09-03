Imports System.Data
Imports System.Data.SqlClient

Public Class clsAdressen
    Public dt As New DataTable
    Public dr As DataRow

    Public dt2 As New DataTable
    Public dr2 As DataRow

    Private CON As New clsConnection
    Private cmd As New SqlCommand

    Public Function sAdresAndLandByPartijIDAndType(iPartijID As Long, sType As String, sCode As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Adressen.sPostCode, Adressen.sHuisNr, Adressen.sToev, Adressen.sStraat, Adressen.sPlaats, Adressen.Addressline1, Adressen.Addressline2, tussTaalLanden.sLandNaam FROM Adressen LEFT OUTER JOIN tussTaalLanden ON Adressen.iLandID = tussTaalLanden.iLandID WHERE (Adressen.iPartijID = @iPartijID) AND (Adressen.sType = @sType) AND (tussTaalLanden.sCode = @sCode)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sCode", sCode)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sAdresByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Adressen WHERE (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sAdresByPartijIDAndType(iPartijID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(1) * FROM Adressen WHERE (iPartijID = @iPartijID) AND (sType = @sType)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sAdressenByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(1) * FROM Adressen WHERE (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sAdresByPersID(iPartijID As Long, iPersID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Adressen WHERE iPartijID = @iPartijID AND iPersID = @iPersID AND sType = @sType"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function iscAdres(iPers As Long, iPartijID As Long, sType As String, sWijkCode As String, sStraatCode As String, sPostCode As String, sHuisNr As String,
                           sToev As String, sStraat As String, sAdres As String, sPlaats As String, sGemeente As String, sProvincie As String, sLand As String,
                           iLandID As Long, rLatitudeX As Long, sLandcode As String, rLongitudeY As Long, sData As String, sInfo As String, Addressline1 As String, Addressline2 As String, iPartijIDBeheerder As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Adressen (iPersID, iPartijID, sType, sWijkCode, sStraatCode, sPostCode, sHuisNr, sToev, sStraat, sAdres, sPlaats, sGemeente, sProvincie, sLand, iLandID, rLatitudeX, sLandcode, rlongitudeY, sData, sInfo, Addressline1, Addressline2, iPartijIDBeheerder) VALUES (@iPersID, @iPartijID, @sType, @sWijkCode, @sStraatCode, @sPostCode, @sHuisNr, @sToev, @sStraat, @sAdres, @sPlaats, @sGemeente, @sProvincie, @sLand, @iLandID, @rLatitudeX, @sLandcode, @rlongitudeY, @sData, @sInfo, @Addressline1, @Addressline2, @iPartijIDBeheerder); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iPersID", iPers)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sWijkCode", sWijkCode)
        cmd.Parameters.AddWithValue("@sStraatCode", sStraatCode)
        cmd.Parameters.AddWithValue("@sPostCode", sPostCode)
        cmd.Parameters.AddWithValue("@sHuisNr", sHuisNr)
        cmd.Parameters.AddWithValue("@sToev", sToev)
        cmd.Parameters.AddWithValue("@sStraat", sStraat)
        cmd.Parameters.AddWithValue("@sAdres", sAdres)
        cmd.Parameters.AddWithValue("@sPlaats", sPlaats)
        cmd.Parameters.AddWithValue("@sGemeente", sGemeente)
        cmd.Parameters.AddWithValue("@sProvincie", sProvincie)
        cmd.Parameters.AddWithValue("@sLand", sLand)
        cmd.Parameters.AddWithValue("@iLandID", iLandID)
        cmd.Parameters.AddWithValue("@rLatitudeX", rLatitudeX)
        cmd.Parameters.AddWithValue("@sLandcode", sLandcode)
        cmd.Parameters.AddWithValue("@rlongitudeY", rLongitudeY)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@Addressline1", Addressline1)
        cmd.Parameters.AddWithValue("@Addressline2", Addressline2)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Scalar(cmd)
    End Function


    Public Function uAdresByPersIDAndType(iPers As Long, iPartijID As Long, sType As String, sWijkCode As String, sStraatCode As String, sPostCode As String, sHuisNr As String,
                       sToev As String, sStraat As String, sAdres As String, sPlaats As String, sGemeente As String, sProvincie As String, sLand As String,
                       iLandID As Long, rLatitudeX As Long, sLandcode As String, rLongitudeY As Long, sData As String, sInfo As String, Addressline1 As String, Addressline2 As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Adressen SET sWijkCode = @sWijkCode, sStraatCode = @sStraatCode, sPostCode = @sPostCode, sHuisNr = @sHuisNr, sToev = @sToev, sStraat = @sStraat, sAdres = @sAdres, sPlaats = @sPlaats, sGemeente = @sGemeente, sProvincie = @sProvincie, sLand = @sLand, rLatitudeX = @rLatitudeX, rlongitudeY = @rlongitudeY, sData = @sData, sInfo = @sInfo, Addressline1 = @Addressline1, Addressline2 = @Addressline2, iLandID = @iLandID, sLandcode = @sLandcode WHERE (iPersID = @iPersID) AND sType = @sType"
        cmd.Parameters.AddWithValue("@iPersID", iPers)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sWijkCode", sWijkCode)
        cmd.Parameters.AddWithValue("@sStraatCode", sStraatCode)
        cmd.Parameters.AddWithValue("@sPostCode", sPostCode)
        cmd.Parameters.AddWithValue("@sHuisNr", sHuisNr)
        cmd.Parameters.AddWithValue("@sToev", sToev)
        cmd.Parameters.AddWithValue("@sStraat", sStraat)
        cmd.Parameters.AddWithValue("@sAdres", sAdres)
        cmd.Parameters.AddWithValue("@sPlaats", sPlaats)
        cmd.Parameters.AddWithValue("@sGemeente", sGemeente)
        cmd.Parameters.AddWithValue("@sProvincie", sProvincie)
        cmd.Parameters.AddWithValue("@sLand", sLand)
        cmd.Parameters.AddWithValue("@iLandID", iLandID)
        cmd.Parameters.AddWithValue("@rLatitudeX", rLatitudeX)
        cmd.Parameters.AddWithValue("@sLandcode", sLandcode)
        cmd.Parameters.AddWithValue("@rlongitudeY", rLongitudeY)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@Addressline1", Addressline1)
        cmd.Parameters.AddWithValue("@Addressline2", Addressline2)
        Return CON.Update(cmd)
    End Function
    Public Function uAdresByPersIDAndTypeAndPartijIDBeheerder(iPersID As Long, sType As String, sWijkCode As String, sStraatCode As String, sPostCode As String, sHuisNr As String,
                       sToev As String, sStraat As String, sAdres As String, sPlaats As String, sGemeente As String, sProvincie As String, sLand As String,
                       iLandID As Long, rLatitudeX As Long, sLandcode As String, rLongitudeY As Long, sData As String, sInfo As String, Addressline1 As String, Addressline2 As String, iPartijIDBeheerder As Long) As Long
        cmd.Parameters.Clear()
        If iPersID = 0 Then
            Return 0
        End If
        cmd.CommandText = "UPDATE Adressen SET sWijkCode = @sWijkCode, sStraatCode = @sStraatCode, sPostCode = @sPostCode, sHuisNr = @sHuisNr, sToev = @sToev, sStraat = @sStraat, sAdres = @sAdres, sPlaats = @sPlaats, sGemeente = @sGemeente, sProvincie = @sProvincie, sLand = @sLand, rLatitudeX = @rLatitudeX, rlongitudeY = @rlongitudeY, sData = @sData, sInfo = @sInfo, Addressline1 = @Addressline1, Addressline2 = @Addressline2, iLandID = @iLandID, sLandcode = @sLandcode OUTPUT inserted.iAdresID WHERE iPersID = @iPersID AND sType = @sType AND sStatus = @sStatus AND iPartijIDBeheerder = @iPartijIDBeheerder"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sWijkCode", sWijkCode)
        cmd.Parameters.AddWithValue("@sStraatCode", sStraatCode)
        cmd.Parameters.AddWithValue("@sPostCode", sPostCode)
        cmd.Parameters.AddWithValue("@sHuisNr", sHuisNr)
        cmd.Parameters.AddWithValue("@sToev", sToev)
        cmd.Parameters.AddWithValue("@sStraat", sStraat)
        cmd.Parameters.AddWithValue("@sAdres", sAdres)
        cmd.Parameters.AddWithValue("@sPlaats", sPlaats)
        cmd.Parameters.AddWithValue("@sGemeente", sGemeente)
        cmd.Parameters.AddWithValue("@sProvincie", sProvincie)
        cmd.Parameters.AddWithValue("@sLand", sLand)
        cmd.Parameters.AddWithValue("@iLandID", iLandID)
        cmd.Parameters.AddWithValue("@rLatitudeX", rLatitudeX)
        cmd.Parameters.AddWithValue("@sLandcode", sLandcode)
        cmd.Parameters.AddWithValue("@rlongitudeY", rLongitudeY)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@Addressline1", Addressline1)
        cmd.Parameters.AddWithValue("@Addressline2", Addressline2)
        cmd.Parameters.AddWithValue("@sStatus", "actief")
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Scalar(cmd)
    End Function

    Public Function uAdresByPartijIDAndTypeAndPartijIDBeheerder(iPartijID As Long, sType As String, sWijkCode As String, sStraatCode As String, sPostCode As String, sHuisNr As String,
                   sToev As String, sStraat As String, sAdres As String, sPlaats As String, sGemeente As String, sProvincie As String, sLand As String,
                   iLandID As Long, rLatitudeX As Long, sLandcode As String, rLongitudeY As Long, sData As String, sInfo As String, Addressline1 As String, Addressline2 As String, iPartijIDBeheerder As Long) As Long
        cmd.Parameters.Clear()
        If iPartijID = 0 Then
            Return 0
        End If
        cmd.CommandText = "UPDATE Adressen SET sWijkCode = @sWijkCode, sStraatCode = @sStraatCode, sPostCode = @sPostCode, sHuisNr = @sHuisNr, sToev = @sToev, sStraat = @sStraat, sAdres = @sAdres, sPlaats = @sPlaats, sGemeente = @sGemeente, sProvincie = @sProvincie, sLand = @sLand, rLatitudeX = @rLatitudeX, rlongitudeY = @rlongitudeY, sData = @sData, sInfo = @sInfo, Addressline1 = @Addressline1, Addressline2 = @Addressline2, iLandID = @iLandID, sLandcode = @sLandcode OUTPUT inserted.iAdresID WHERE iPartijID = @iPartijID AND sType = @sType AND sStatus = @sStatus AND iPartijIDBeheerder = iPartijIDBeheerder"
        'cmd.Parameters.AddWithValue("@iPersID", iPers)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sWijkCode", sWijkCode)
        cmd.Parameters.AddWithValue("@sStraatCode", sStraatCode)
        cmd.Parameters.AddWithValue("@sPostCode", sPostCode)
        cmd.Parameters.AddWithValue("@sHuisNr", sHuisNr)
        cmd.Parameters.AddWithValue("@sToev", sToev)
        cmd.Parameters.AddWithValue("@sStraat", sStraat)
        cmd.Parameters.AddWithValue("@sAdres", sAdres)
        cmd.Parameters.AddWithValue("@sPlaats", sPlaats)
        cmd.Parameters.AddWithValue("@sGemeente", sGemeente)
        cmd.Parameters.AddWithValue("@sProvincie", sProvincie)
        cmd.Parameters.AddWithValue("@sLand", sLand)
        cmd.Parameters.AddWithValue("@iLandID", iLandID)
        cmd.Parameters.AddWithValue("@rLatitudeX", rLatitudeX)
        cmd.Parameters.AddWithValue("@sLandcode", sLandcode)
        cmd.Parameters.AddWithValue("@rlongitudeY", rLongitudeY)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@Addressline1", Addressline1)
        cmd.Parameters.AddWithValue("@Addressline2", Addressline2)
        cmd.Parameters.AddWithValue("@sStatus", "actief")
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Scalar(cmd)
    End Function

    Public Function sAdresByPartijAndPersoonID(iPartijId As Long, iPersId As Long) As DataTable

        cmd.Parameters.Clear()

        cmd.CommandText = "SELECT * FROM Adressen WHERE iPartijID = @iPartijID AND iPersId = @iPersID"
        cmd.Parameters.AddWithValue("@iPersId", iPersId)
        cmd.Parameters.AddWithValue("iPartijId", iPartijId)

        Return CON.sDatatable(cmd)

    End Function
End Class