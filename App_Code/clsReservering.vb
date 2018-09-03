Imports System.Data
Imports System.Data.SqlClient

Public Class clsReservering
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function iscReservering(iPersID As Long, iPartijID As Long, sTypeID As String, iID As Long, sTitle As String, sEigenaar As String, dtStart As Date, dtEnd As Date, sData As String, sInfo As String, sZoekData As String,
                                   sAanhef As String, sTitel As String, sVoornaam As String, sVoorletters As String, sTv As String, sNaam As String, sPostCode As String, sHuisNr As String, sToev As String, sStraat As String,
                                   sAdres As String, sPlaats As String, sEmail As String, sTelefoon As String, sMobiel As String, sStatus As String, iStart As Long, iEind As Long, sType As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Reservering (iPersID, iPartijID, sTypeID, iID, sTitle, sEigenaar, dtStart, dtEnd, sData, sInfo, sZoekData, sAanhef, sTitel, sVoornaam, sVoorletters, sTv, sNaam, sPostCode, sHuisNr, sToev, sStraat, sAdres, sPlaats, sEmail, sTelefoon, sMobiel, sStatus, iStart, iEind, sType) VALUES (@iPersID,@iPartijID,@sTypeID,@iID,@sTitle,@sEigenaar,@dtStart,@dtEnd,@sData,@sInfo,@sZoekData,@sAanhef,@sTitel,@sVoornaam,@sVoorletters,@sTv,@sNaam,@sPostCode,@sHuisNr,@sToev,@sStraat,@sAdres,@sPlaats,@sEmail,@sTelefoon,@sMobiel,@sStatus,@iStart,@iEind,@sType); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sTypeID", sTypeID)
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@sEigenaar", sEigenaar)
        cmd.Parameters.AddWithValue("@dtStart", dtStart)
        cmd.Parameters.AddWithValue("@dtEnd", dtEnd)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@sZoekData", sZoekData)
        cmd.Parameters.AddWithValue("@sAanhef", sAanhef)
        cmd.Parameters.AddWithValue("@sTitel", sTitel)
        cmd.Parameters.AddWithValue("@sVoornaam", sVoornaam)
        cmd.Parameters.AddWithValue("@sVoorletters", sVoorletters)
        cmd.Parameters.AddWithValue("@sTv", sTv)
        cmd.Parameters.AddWithValue("@sNaam", sNaam)
        cmd.Parameters.AddWithValue("@sPostCode", sPostCode)
        cmd.Parameters.AddWithValue("@sHuisNr", sHuisNr)
        cmd.Parameters.AddWithValue("@sToev", sToev)
        cmd.Parameters.AddWithValue("@sStraat", sStraat)
        cmd.Parameters.AddWithValue("@sAdres", sAdres)
        cmd.Parameters.AddWithValue("@sPlaats", sPlaats)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sTelefoon", sTelefoon)
        cmd.Parameters.AddWithValue("@sMobiel", sMobiel)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@iStart", iStart)
        cmd.Parameters.AddWithValue("@iEind", iEind)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.Scalar(cmd)
    End Function

    Public Function iReserveringItem(iReserveringID As Long, iPartijID As Long, iPersID As Long, sType As String, iAantal As Long, iAantalPersonen As Long, sHandicap As String, sOpmerkingen As String,
                                     sBijzonderheden As String, iKinderstoelen As Long, bVegetarisch As Boolean, bRaam As Boolean, sTafel As String, sMenu As String, sStoel As String, sPlek As String,
                                     sData As String, sInfo As String, sZoekData As String, sWedstrijd As String, sLeiding As String, sVorm As String, sQualifying As String, sDeelnemers As String,
                                     sVoorWie As String, sHoles As String, sItems As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO ReserveringItems (iReserveringID, iPartijID, iPersID, sType, iAantal, iAantalPersonen, sHandicap, sOpmerkingen, sBijzonderheden, iKinderstoelen, bVegetarisch, bRaam, sTafel, sMenu, sStoel, sPlek, sData, sInfo, sZoekData, sWedstrijd, sLeiding, sVorm, sQualifying, sDeelnemers, sVoorWie, sHoles, sItems) VALUES (@iReserveringID,@iPartijID,@iPersID,@sType,@iAantal,@iAantalPersonen,@sHandicap,@sOpmerkingen,@sBijzonderheden,@iKinderstoelen,@bVegetarisch,@bRaam,@sTafel,@sMenu,@sStoel,@sPlek,@sData,@sInfo,@sZoekData,@sWedstrijd,@sLeiding,@sVorm,@sQualifying,@sDeelnemers,@sVoorWie,@sHoles,@sItems)"
        cmd.Parameters.AddWithValue("@iReserveringID", iReserveringID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iAantal", iAantal)
        cmd.Parameters.AddWithValue("@iAantalPersonen", iAantalPersonen)

        cmd.Parameters.AddWithValue("@sHandicap", sHandicap)
        cmd.Parameters.AddWithValue("@sOpmerkingen", sOpmerkingen)
        cmd.Parameters.AddWithValue("@sBijzonderheden", sBijzonderheden)
        cmd.Parameters.AddWithValue("@iKinderstoelen", iKinderstoelen)
        cmd.Parameters.AddWithValue("@bVegetarisch", bVegetarisch)

        cmd.Parameters.AddWithValue("@bRaam", bRaam)
        cmd.Parameters.AddWithValue("@sTafel", sTafel)
        cmd.Parameters.AddWithValue("@sMenu", sMenu)
        cmd.Parameters.AddWithValue("@sStoel", sStoel)
        cmd.Parameters.AddWithValue("@sPlek", sPlek)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@sZoekData", sZoekData)
        cmd.Parameters.AddWithValue("@sWedstrijd", sWedstrijd)

        cmd.Parameters.AddWithValue("@sLeiding", sLeiding)
        cmd.Parameters.AddWithValue("@sVorm", sVorm)
        cmd.Parameters.AddWithValue("@sQualifying", sQualifying)
        cmd.Parameters.AddWithValue("@sDeelnemers", sDeelnemers)
        cmd.Parameters.AddWithValue("@sVoorWie", sVoorWie)
        cmd.Parameters.AddWithValue("@sHoles", sHoles)
        cmd.Parameters.AddWithValue("@sItems", sItems)
        Return CON.Update(cmd)
    End Function
End Class