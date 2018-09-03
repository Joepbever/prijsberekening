Imports System.Data
Imports System.Data.SqlClient

Public Class clsPartijen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sPartijByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(1) * FROM Partijen WHERE (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function iscPartij(iBcorePartijID As String, sNaam As String, sKlantNummer As String, sRekeningNummer As String, sRekeningHouder As String, sKvkNummer As String, sBtwNummer As String, sOpmerking As String, sData As String, sDatum As String, sIBAN As String, sBIC As String, sCreditcard As String, sInfo As String, sBarcode As String, sType As String, sStatus As String, sTwitter As String, sGoogle As String, sGoogleAnalytics As String, sFacebook As String, sLinkedIn As String, sInstagram As String, sPinterest As String, sLogo As String, sTelefoon As String, sMobiel As String, sEmail As String, sWebsite As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Partijen (iBcorePartijID, sNaam, sKlantNummer, sRekeningNummer, sRekeningHouder, sKvkNummer, sBtwNummer, sOpmerking, sData, sDatum, sIBAN, sBIC, sCreditcard, sInfo, sBarcode, sType, sStatus, sTwitter, sGoogleAnalytics, sGoogle, sFacebook, sLinkedIn, sInstagram, sPinterest, sLogo, sTelefoon, sMobiel, sEmail, sWebsite) VALUES (@iBcorePartijID, @sNaam, @sKlantNummer, @sRekeningNummer, @sRekeningHouder, @sKvkNummer, @sBtwNummer, @sOpmerking, @sData, @sDatum, @sIBAN, @sBIC, @sCreditcard, @sInfo, @sBarcode, @sType, @sStatus, @sTwitter, @sGoogleAnalytics, @sGoogle, @sFacebook, @sLinkedIn, @sInstagram, @sPinterest, @sLogo, @sTelefoon, @sMobiel, @sEmail, @sWebsite) SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iBcorePartijID", iBcorePartijID)
        cmd.Parameters.AddWithValue("@sNaam", sNaam)
        cmd.Parameters.AddWithValue("@sKlantNummer", sKlantNummer)
        cmd.Parameters.AddWithValue("@sRekeningNummer", sRekeningNummer)
        cmd.Parameters.AddWithValue("@sRekeningHouder", sRekeningHouder)
        cmd.Parameters.AddWithValue("@sKvkNummer", sKvkNummer)
        cmd.Parameters.AddWithValue("@sBtwNummer", sBtwNummer)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sDatum", sDatum)
        cmd.Parameters.AddWithValue("@sIBAN", sIBAN)
        cmd.Parameters.AddWithValue("@sBIC", sBIC)
        cmd.Parameters.AddWithValue("@sCreditcard", sCreditcard)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@sBarcode", sBarcode)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sTwitter", sTwitter)
        cmd.Parameters.AddWithValue("@sGoogle", sGoogle)
        cmd.Parameters.AddWithValue("@sGoogleAnalytics", sGoogleAnalytics)
        cmd.Parameters.AddWithValue("@sFacebook", sFacebook)
        cmd.Parameters.AddWithValue("@sLinkedIn", sLinkedIn)
        cmd.Parameters.AddWithValue("@sInstagram", sInstagram)
        cmd.Parameters.AddWithValue("@sPinterest", sPinterest)
        cmd.Parameters.AddWithValue("@sLogo", sLogo)
        cmd.Parameters.AddWithValue("@sTelefoon", sTelefoon)
        cmd.Parameters.AddWithValue("@sMobiel", sMobiel)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sWebsite", sWebsite)
        Return CON.Scalar(cmd)
    End Function

    Public Function uNaam(iBcorePartijID As String, sNaam As String, iPartijID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Partijen SET sNaam = @sNaam WHERE (iPartijID = @iPartijID) AND iBcorePartijID = @iBcorePartijID"
        cmd.Parameters.AddWithValue("@iBcorePartijID", iBcorePartijID)
        cmd.Parameters.AddWithValue("@sNaam", sNaam)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.Scalar(cmd)
    End Function

    Public Function uPartij(iBcorePartijID As String, sNaam As String, sKlantNummer As String, sRekeningNummer As String, sRekeningHouder As String, sKvkNummer As String, sBtwNummer As String, sOpmerking As String, sData As String, sDatum As String, sIBAN As String, sBIC As String, sCreditcard As String, sInfo As String, sBarcode As String, sType As String, sStatus As String, sTwitter As String, sGoogle As String, sGoogleAnalytics As String, sFacebook As String, sLinkedIn As String, sInstagram As String, sPinterest As String, sLogo As String, sTelefoon As String, sMobiel As String, sEmail As String, sWebsite As String, iPartijID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Partijen SET iBcorePartijID = @iBcorePartijID, sNaam = @sNaam, sKlantNummer = @sKlantNummer, sRekeningNummer = @sRekeningNummer, sRekeningHouder = @sRekeningHouder, sKvkNummer = @sKvkNummer, sBtwNummer = @sBtwNummer, sOpmerking = @sOpmerking, sData = @sData, sDatum = @sDatum, sIBAN = @sIBAN, sBIC = @sBIC, sCreditcard = @sCreditcard, sInfo = @sInfo, sBarcode = @sBarcode, sType = @sType, sStatus = @sStatus, sTwitter = @sTwitter, sGoogle = @sGoogle, sGoogleAnalytics = @sGoogleAnalytics, sFacebook = @sFacebook, sLinkedIn = @sLinkedIn, sInstagram = @sInstagram, sPinterest = @sPinterest, sLogo = @sLogo, sTelefoon = @sTelefoon, sMobiel = @sMobiel, sEmail = @sEmail, sWebsite = @sWebsite WHERE (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iBcorePartijID", iBcorePartijID)
        cmd.Parameters.AddWithValue("@sNaam", sNaam)
        cmd.Parameters.AddWithValue("@sKlantNummer", sKlantNummer)
        cmd.Parameters.AddWithValue("@sRekeningNummer", sRekeningNummer)
        cmd.Parameters.AddWithValue("@sRekeningHouder", sRekeningHouder)
        cmd.Parameters.AddWithValue("@sKvkNummer", sKvkNummer)
        cmd.Parameters.AddWithValue("@sBtwNummer", sBtwNummer)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sDatum", sDatum)
        cmd.Parameters.AddWithValue("@sIBAN", sIBAN)
        cmd.Parameters.AddWithValue("@sBIC", sBIC)
        cmd.Parameters.AddWithValue("@sCreditcard", sCreditcard)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@sBarcode", sBarcode)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sTwitter", sTwitter)
        cmd.Parameters.AddWithValue("@sGoogle", sGoogle)
        cmd.Parameters.AddWithValue("@sGoogleAnalytics", sGoogleAnalytics)
        cmd.Parameters.AddWithValue("@sFacebook", sFacebook)
        cmd.Parameters.AddWithValue("@sLinkedIn", sLinkedIn)
        cmd.Parameters.AddWithValue("@sInstagram", sInstagram)
        cmd.Parameters.AddWithValue("@sPinterest", sPinterest)
        cmd.Parameters.AddWithValue("@sLogo", sLogo)
        cmd.Parameters.AddWithValue("@sTelefoon", sTelefoon)
        cmd.Parameters.AddWithValue("@sMobiel", sMobiel)
        cmd.Parameters.AddWithValue("@sEmail", sEmail)
        cmd.Parameters.AddWithValue("@sWebsite", sWebsite)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.Scalar(cmd)
    End Function

    Public sAdres As String = ""
    Public sBedrijfsnaam As String = ""
    Public sBtwNummer As String = ""
    Public sKvkNummer As String = ""
    Public sPartijGegevens As String = ""
    Public sMobiel As String = ""
    Public sTelefoon As String = ""
    Public sEmail As String = ""
    Public sContactpersoon As String = ""
    Public sTwitter As String = ""
    Public sFacebook As String = ""
    Public sLinkedIn As String = ""
    Public sInstagram As String = ""
    Public sPinterest As String = ""
    Public sWebsite As String
    Public sGoogle As String = ""
    Public sGoogleAnalytics As String = ""
    Public sLogo As String = ""

    Public Function sPartij(ByVal iPartijID As Long) As String
        dt = sPartijByPartijID(iPartijID)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            sBedrijfsnaam = dr.Item("sNaam")
            sBtwNummer = dr.Item("sBtwNummer")
            sKvkNummer = dr.Item("sKvkNummer")
            sTwitter = dr.Item("sTwitter")
            sFacebook = dr.Item("sFacebook")
            sLinkedIn = dr.Item("sLinkedin")
            sInstagram = dr.Item("sInstagram")
            sPinterest = dr.Item("sPinterest")
            sGoogle = dr.Item("sGoogle")
            sGoogleAnalytics = dr.Item("sGoogleAnalytics")
            sMobiel = dr.Item("sMobiel")
            sTelefoon = dr.Item("sTelefoon")
            sWebsite = dr.Item("sWebsite")
            sEmail = dr.Item("sEmail")
            sLogo = dr.Item("sLogo")

            Dim ADR As New clsAdressen
            ADR.dt = ADR.sAdresByPartijIDAndType(iPartijID, "factuur")
            If ADR.dt.Rows.Count > 0 Then
                ADR.dr = ADR.dt.Rows(0)
                sAdres = ADR.dr.Item("sStraat") & " " & ADR.dr.Item("sHuisNr") & " " & ADR.dr.Item("sToev") & "<br />" & ADR.dr.Item("sPostCode") & "<br />" & ADR.dr.Item("sPlaats") & "<br />" & ADR.dr.Item("sLand")

                Dim PERS As New clsPersonen
                PERS.dt = PERS.sPersoonByPersID(ADR.dr.Item("iPersID"))
                If PERS.dt.Rows.Count > 0 Then
                    PERS.dr = PERS.dt.Rows(0)
                    sContactpersoon = PERS.dr.Item("sVoorletters") & " " & PERS.dr.Item("sNaam")
                End If
            End If
        End If
        sPartijGegevens = sBedrijfsnaam & "<br />" & sAdres
        Return sPartijGegevens
    End Function


    'Public Function sLogo(iPartijID As Long) As String
    '    Dim CON As New clsConnection
    '    Dim cmd As New SqlCommand
    '    cmd.CommandText = "SELECT TOP(1) sLogo FROM Partijen WHERE iPartijID = @iPartijID"
    '    cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
    '    Dim dt As New DataTable
    '    dt = CON.sDatatable(cmd)
    '    If dt.Rows.Count > 0 Then
    '        Dim dr As DataRow = dt.Rows(0)
    '        Return dr.Item("sLogo")
    '    Else
    '        Return ""
    '    End If
    'End Function

    Public Function sPartijBeheerder(iPartijID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT * FROM Partijen WHERE iPartijID = @iPartijID"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

End Class
