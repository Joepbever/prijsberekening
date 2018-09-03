Imports System.Data
Imports System.Data.SqlClient

Public Class clsPersonen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sPersoonByPersIDAndPartijIDBeheerder(iPersID As Long, iPartijIDBeheerder As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Personen WHERE (iPersID = @iPersID) and (iPartijIDBeheerder = @iPartijIDBeheerder)"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.sDatatable(cmd)
    End Function

    Public Function uPartijIDBeheerder(iPersID As Long, iPartijIDBeheerder As Long) As Boolean
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Personen SET iPartijIDBeheerder = @iPartijIDBeheerder WHERE (iPersID = @iPersID)"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Update(cmd)
    End Function

    Public Function sPersoonByPersID(iPersID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Personen WHERE (iPersID = @iPersID)"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sPersoonByUserID(sUserID As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT bActief, bFactuur, dDate, dUurloon, dtGeboorteDatum, iBrancheID, iFunctieID, iPersID, sAanhef, sAfdeling, sData, sEmail, sFax, sFunctie, sMobiel, sNaam, sNationaliteit, sOpmerking, sStatus, sTelefoon, sTitel, sTv, sUserID, sVoorletters FROM Personen WHERE (sUserID = @sUserID)"
        cmd.Parameters.AddWithValue("@sUserID", sUserID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function iscPersoon(sAanhef As String, sTitel As String, sVoorletters As String, sTv As String, sNaam As String, sOpmerking As String, sData As String, sAfdeling As String, sFunctie As String, sEmail As String, sTelefoon As String, sMobiel As String, sFax As String, bFactuur As Boolean, dDate As Date, sUserID As String, iBrancheID As Long, iFunctieID As Long, dtGeboorteDatum As Date, bActief As Boolean, sStatus As String, sNationaliteit As String, dUurloon As Decimal, iPartijIDBeheerder As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Personen (sAanhef, sTitel, sVoorletters, sTv, sNaam, sOpmerking, sData, sAfdeling, sFunctie, sEmail, sTelefoon, sMobiel, sFax, bFactuur, dDate, sUserID, iBrancheID, iFunctieID, dtGeboorteDatum, bActief, sStatus, sNationaliteit, dUurloon, iPartijIDBeheerder) VALUES (@sAanhef, @sTitel, @sVoorletters, @sTv, @sNaam, @sOpmerking, @sData, @sAfdeling, @sFunctie, @sEmail, @sTelefoon, @sMobiel, @sFax, @bFactuur, @dDate, @sUserID, @iBrancheID, @iFunctieID, @dtGeboorteDatum, @bActief, @sStatus, @sNationaliteit, @dUurloon, @iPartijIDBeheerder); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@sAanhef", sAanhef)
        cmd.Parameters.AddWithValue("@sTitel", sTitel)
        cmd.Parameters.AddWithValue("@sVoorletters", sVoorletters)
        cmd.Parameters.AddWithValue("@sTv", sTv)
        cmd.Parameters.AddWithValue("@sNaam", sNaam)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sAfdeling", sAfdeling)
        cmd.Parameters.AddWithValue("@sFunctie", sFunctie)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sTelefoon", sTelefoon)
        cmd.Parameters.AddWithValue("@sMobiel", sMobiel)
        cmd.Parameters.AddWithValue("@sFax", sFax)
        cmd.Parameters.AddWithValue("@bFactuur", bFactuur)
        cmd.Parameters.AddWithValue("@dDate", dDate)
        cmd.Parameters.AddWithValue("@sUserID", sUserID)
        cmd.Parameters.AddWithValue("@iBrancheID", iBrancheID)
        cmd.Parameters.AddWithValue("@iFunctieID", iFunctieID)
        cmd.Parameters.AddWithValue("@dtGeboorteDatum", dtGeboorteDatum)
        cmd.Parameters.AddWithValue("@bActief", bActief)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sNationaliteit", sNationaliteit)
        cmd.Parameters.AddWithValue("@dUurloon", dUurloon)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        Return CON.Scalar(cmd)
    End Function

    Public Function uPersoon(sAanhef As String, sTitel As String, sVoorletters As String, sTv As String, sNaam As String, sOpmerking As String, sData As String, sAfdeling As String, sFunctie As String, sEmail As String, sTelefoon As String, sMobiel As String, sFax As String, bFactuur As Boolean, dDate As Date, iBrancheID As Long, iFunctieID As Long, dtGeboorteDatum As Date, bActief As Boolean, sStatus As String, sNationaliteit As String, dUurloon As Decimal, iPartijIDBeheerder As Long, iPersID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Personen SET sAanhef = @sAanhef, sTitel = @sTitel, sVoorletters = @sVoorletters, sTv = @sTv, sNaam = @sNaam, sOpmerking = @sOpmerking, sData = @sData, sAfdeling = @sAfdeling, sFunctie = @sFunctie, sEmail = @sEmail, sTelefoon = @sTelefoon, sMobiel = @sMobiel, sFax = @sFax, bFactuur = @bFactuur, dDate = @dDate, iBrancheID = @iBrancheID, iFunctieID = @iFunctieID, dtGeboorteDatum = @dtGeboorteDatum, bActief = @bActief, sStatus = @sStatus, sNationaliteit = @sNationaliteit, dUurloon = @dUurloon, iPartijIDBeheerder = @iPartijIDBeheerder WHERE (iPersID = @iPersID)"
        cmd.Parameters.AddWithValue("@sAanhef", sAanhef)
        cmd.Parameters.AddWithValue("@sTitel", sTitel)
        cmd.Parameters.AddWithValue("@sVoorletters", sVoorletters)
        cmd.Parameters.AddWithValue("@sTv", sTv)
        cmd.Parameters.AddWithValue("@sNaam", sNaam)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sAfdeling", sAfdeling)
        cmd.Parameters.AddWithValue("@sFunctie", sFunctie)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sTelefoon", sTelefoon)
        cmd.Parameters.AddWithValue("@sMobiel", sMobiel)
        cmd.Parameters.AddWithValue("@sFax", sFax)
        cmd.Parameters.AddWithValue("@bFactuur", bFactuur)
        cmd.Parameters.AddWithValue("@dDate", dDate)
        cmd.Parameters.AddWithValue("@iBrancheID", iBrancheID)
        cmd.Parameters.AddWithValue("@iFunctieID", iFunctieID)
        cmd.Parameters.AddWithValue("@dtGeboorteDatum", dtGeboorteDatum)
        cmd.Parameters.AddWithValue("@bActief", bActief)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sNationaliteit", sNationaliteit)
        cmd.Parameters.AddWithValue("@dUurloon", dUurloon)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        Return CON.Update(cmd)
    End Function

    Public Function sPersoonByPartijIDAndUsername(iPartijIDBeheerder As Long, sTitel As String) As DataTable

        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT dtGeboorteDatum FROM Personen WHERE iPartijIDBeheerder = @iPartijIDBeheerder AND sTitel = @sTitel"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@sTitel", sTitel)

        Me.dt = CON.sDatatable(cmd)
        Return Me.dt
    End Function

    Public Function uExcludePersoon(iPersID As Long, sStatus As String, dDate As Date) As Long

        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Personen SET sStatus = @sStatus, dDate = @dDate WHERE iPersID = @iPersID"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@dDate", dDate)

        Return CON.Update(cmd)

    End Function

    Public Function sEcludedPersonen() As DataTable

        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iPersID, dDate FROM Personen WHERE sStatus = 'self-excluded'"

        Return CON.sDatatable(cmd)

    End Function

    Public Function uUnexcludePersoon(iPersID As Long) As Boolean

        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Personen SET sStatus = 'Actief' WHERE iPersID = @iPersID"
        cmd.Parameters.AddWithValue("@iPersID", iPersID)

        Return CON.Update(cmd)

    End Function

    Public Function sTestallPersonen() As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * From Personen WHERE sStatus = 'Excluded'"
        Return CON.sDatatable(cmd)
    End Function
End Class