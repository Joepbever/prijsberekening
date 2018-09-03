Imports System.Data
Imports System.Data.SqlClient

Public Class clsFacKop
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public iOrdernummer As String

    Public Function sFacKopByPartijAndStatus(iPartijID As Long, sStatus As String, sStatus2 As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iFacKopID, sFactuur, dDatum, dVervaldatum, sPartijGegevens, sVerzendGegevens, iFactuurBedrag, iBtwBedrag, sStatus, sBestand, sOpmerking, sData, sReferentie, iPersID, iPersID2, iAdresID, iAdresID2, iItemsID, iPartijID, sType FROM dbo.FacKop WHERE (iPartijID = @iPartijID) AND (sStatus = @sStatus) OR (iPartijID = @iPartijID) AND (sStatus = @sStatus2) ORDER BY iFacKopID DESC"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sStatus2", sStatus2)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacKopByFacKopID(iFacKopID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iFacKopID, sFactuur, dDatum, dVervaldatum, sPartijGegevens, sVerzendGegevens, iFactuurBedrag, iBtwBedrag, sStatus, sBestand, sOpmerking, sData, sReferentie, iPersID, iPersID2, iAdresID, iAdresID2, iItemsID, iPartijID, sType FROM FacKop WHERE (iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function uPartijIDBeheerder(iFacKopID As Long, iPartijIDBeheerder As Long) As Boolean
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE FacKop SET iPartijIDBeheerder = @iPartijIDBeheerder WHERE (iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Update(cmd)
    End Function

    Public Function uFactuurByID(ByVal iFacKopID As Long, sFactuur As String, sBestand As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE FacKop SET sFactuur = @sFactuur, sBestand = @sBestand WHERE (iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@sFactuur", sFactuur)
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sBestand", sBestand)
        Return CON.Update(cmd)
    End Function

    Public Function uStatusType(sType As String, sStatus As String, sData As String, iFacKopID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE FacKop SET sType = @sType, sStatus = @sStatus, sData = @sData WHERE (iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.Update(cmd)
    End Function

    Public Function iscFacKop(sFactuur As String, dDatum As Date, dVervaldatum As Date, sPartijGegevens As String, sVerzendGegevens As String, iFactuurBedrag As Decimal, iBtwBedrag As Decimal, sStatus As String, sBestand As String, sOpmerking As String, sData As String, sReferentie As String, iPersID As Long, iPersID2 As Long, iAdresID As Long, iAdresID2 As Long, iItemsID As Long, iPartijID As Long, sType As String, iFactuurBedragExcl As Decimal, iPartijIDBeheerder As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO FacKop (sFactuur, dDatum, dVervaldatum, sPartijGegevens, sVerzendGegevens, iFactuurBedrag, iBtwBedrag, sStatus, sBestand, sOpmerking, sData, sReferentie, iPersID, iPersID2, iAdresID, iAdresID2, iItemsID, iPartijID, sType, iFactuurBedragExcl, iPartijIDBeheerder, bWebshop) VALUES (@sFactuur, @dDatum, @dVervaldatum, @sPartijGegevens, @sVerzendGegevens, @iFactuurBedrag, @iBtwBedrag, @sStatus, @sBestand, @sOpmerking, @sData, @sReferentie, @iPersID, @iPersID2, @iAdresID, @iAdresID2, @iItemsID, @iPartijID, @sType, @iFactuurBedragExcl, @iPartijIDBeheerder, @bWebshop); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@sFactuur", sFactuur)
        cmd.Parameters.AddWithValue("@dDatum", dDatum)
        cmd.Parameters.AddWithValue("@dVervaldatum", dVervaldatum)
        cmd.Parameters.AddWithValue("@sPartijGegevens", sPartijGegevens)
        cmd.Parameters.AddWithValue("@sVerzendGegevens", sVerzendGegevens)
        cmd.Parameters.AddWithValue("@iFactuurBedrag", iFactuurBedrag)
        cmd.Parameters.AddWithValue("@iBtwBedrag", iBtwBedrag)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sBestand", sBestand)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sReferentie", sReferentie)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@iPersID2", iPersID2)
        cmd.Parameters.AddWithValue("@iAdresID", iAdresID)
        cmd.Parameters.AddWithValue("@iAdresID2", iAdresID2)
        cmd.Parameters.AddWithValue("@iItemsID", iItemsID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iFactuurBedragExcl", iFactuurBedragExcl)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@bWebshop", True)
        Return CON.Scalar(cmd)
    End Function

    Public Function iFacKop(ByVal sPartijGegevens As String, ByVal sVerzendGegevens As String, ByVal iFactuurBedrag As Decimal, ByVal iFactuurBedragExcl As Decimal, ByVal iBtwBedrag As Decimal, ByVal sOpmerking As String, ByVal sData As String, ByVal sReferentie As String, ByVal iPersID As Long, ByVal iPersID2 As Long, ByVal iAdresID As Long, ByVal iAdresID2 As Long, ByVal iPartijID As Long, ByVal sType As String, iPartijIDBeheerder As Long, Optional ByVal sStatus As String = "OPEN") As Long
        Dim BP As New BasePage
        Dim ST As New clsSettings

        Dim sOrdernummer As String = ST.scSettingByTypeAndGroup("webshop", sType & "teller", BP.iPartijIDBeheerder)
        ST.uSettingByTypeAndGroup(CInt(sOrdernummer) + 1, sType & "teller", "webshop", BP.iPartijIDBeheerder)
        Dim iBetaalTermijn As Integer = ST.scSettingByTypeAndGroup("webshop", sType & "termijn", BP.iPartijIDBeheerder)
        iOrdernummer = sOrdernummer

        Dim dVervalDatum As Date = Now.AddDays(iBetaalTermijn)
        Dim iFacKopID As Long = iscFacKop(sOrdernummer, Now, dVervalDatum, sPartijGegevens, sVerzendGegevens, iFactuurBedrag, iBtwBedrag, sStatus, "", sOpmerking, sData, sReferentie, iPersID, iPersID2, iAdresID, iAdresID2, 0, iPartijID, sType, iFactuurBedragExcl, iPartijIDBeheerder)

        Return iFacKopID
    End Function

    Public Function sByFacKopID(iFacKopID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = " SELECT * FROM FacKop WHERE  (iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.sDatatable(cmd)
    End Function

    'Public daFacKop As New dsFacKopTableAdapters.taFacKop
    'Public dtFacKop As New dsFacKop.dtFacKopDataTable
    'Public drFacKop As dsFacKop.dtFacKopRow


    'Public iOrdernummer As String
    'Dim SETTING As New clsSettings
    'Public Function iFacKop(ByVal sPartijGegevens As String, ByVal sVerzendGegevens As String, ByVal iFactuurBedrag As Decimal, ByVal iBtwBedrag As Decimal, ByVal sOpmerking As String, ByVal sData As String, ByVal sReferentie As String, ByVal iPersID As Long, ByVal iPersID2 As Long, ByVal iAdresID As Long, ByVal iAdresID2 As Long, ByVal iPartijID As Long, ByVal sType As String, Optional ByVal sStatus As String = "OPEN") As Long
    '    Dim iKMPartijID As Long = HttpContext.Current.Profile.Item("iPartijID")
    '    Dim sOrdernummer As String = SETTING.daSettings.scSettingByTypeAndGroup("webshop", "factuurteller", iKMPartijID)
    '    SETTING.daSettings.uSettingByTypeAndGroup(CInt(sOrdernummer) + 1, "factuurteller", "webshop", iKMPartijID)
    '    Dim iBetaalTermijn As Integer = SETTING.daSettings.scSettingByTypeAndGroup("webshop", "betaaltermijn", iKMPartijID)
    '    iOrdernummer = sOrdernummer

    '    Dim dVervalDatum As Date = Now.AddDays(iBetaalTermijn)
    '    Dim iFacKopID As Long = daFacKop.iscFacKop(sOrdernummer, Now, dVervalDatum, sPartijGegevens, sVerzendGegevens, iFactuurBedrag, iBtwBedrag, sStatus, "", sOpmerking, sData, sReferentie, iPersID, iPersID2, iAdresID, iAdresID2, 0, iPartijID, sType)

    '    Return iFacKopID

    'End Function
End Class
