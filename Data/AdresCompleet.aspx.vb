Imports System.Web.Services

Partial Class Data_AdresCompleet
    Inherits Page

    Public Class oResponse
        Public Property type As String = ""
        Public Property code As String = ""
        Public Property msg As String = ""
        Public Property straatnaam As String = ""
        Public Property plaatsnaam As String = ""
        Public Property gemeentenaam As String = ""
        Public Property provincienaam As String = ""
        Public Property huisnrtoev As String = ""
        Public Property lat As String = ""
        Public Property lon As String = ""
    End Class

    <WebMethod>
    Public Shared Function sAdres(sWijkCode As String, sStraatCode As String, sNummer As String) As Object
        Dim R As New oResponse
        Dim PC As New clsPostcodeNL
        PC.AdresVanPostcode(sWijkCode & sStraatCode, sNummer.Trim)
        If PC.bOK = True Then
            R.code = "1"
            R.msg = "Adres gevonden."
            R.straatnaam = PC.sStraat
            R.plaatsnaam = PC.sPlaats
            R.gemeentenaam = PC.sGemeente
            R.provincienaam = PC.sProvincie
            R.huisnrtoev = PC.sHuisnummer
            R.lat = PC.iLatitude
            R.lon = PC.iLongitude
            R.type = "postcode.nl"
        Else
            Dim ADR As New clsAdressenCompleet
            Dim sAdresCompleet As String = ADR.sAdres(sWijkCode, sStraatCode, sNummer)
            If Trim(sAdresCompleet) = "" Then
                R.code = "0"
                R.msg = "Geen adres gevonden."
            Else
                R.code = "1"
                R.msg = "Adres gevonden."
                R.straatnaam = sAdresCompleet.Split("|")(0)
                R.plaatsnaam = sAdresCompleet.Split("|")(1)
                R.gemeentenaam = sAdresCompleet.Split("|")(2)
                R.provincienaam = sAdresCompleet.Split("|")(3)
                R.huisnrtoev = sAdresCompleet.Split("|")(4)
                R.lat = sAdresCompleet.Split("|")(5)
                R.lon = sAdresCompleet.Split("|")(6)
                R.type = "db pc"
            End If
        End If
        Return R
    End Function
End Class
