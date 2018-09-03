Imports System.Data
Imports System.Data.SqlClient

Public Class clsAdressenCompleet
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sAdresByStraatcodeAndNr(sWijkCode As String, sStraatcode As String, sNr As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(1) huisnr_bag_letter, lengtegraad, breedtegraad, provincienaam, gemeentenaam, plaatsnaam, straatnaam FROM tblAdressenCompleet WHERE (wijkcode = @sWijkCode) And (lettercombinatie = @sStraatcode) And (huisnr = @sNr)"
        cmd.Parameters.AddWithValue("@sWijkCode", sWijkCode)
        cmd.Parameters.AddWithValue("@sStraatcode", sStraatcode)
        cmd.Parameters.AddWithValue("@sNr", sNr)
        Return CON.sDatatableWebservices(cmd)
    End Function

    Public Function sAdres(sWijkCode As String, sStraatCode As String, sNummer As String) As String
        Dim sAdresVerrijking As String = ""
        Dim sToev As String = ""
        Dim ADR As New clsAdressenCompleet

        ADR.dt = ADR.sAdresByStraatcodeAndNr(sWijkCode, sStraatCode, sNummer)
        If ADR.dt.Rows.Count > 0 Then
            ADR.dr = ADR.dt.Rows(0)
            'straatnaam|plaatsnaam|gemeentenaam|provincienaam|huisnrtoev
            sAdresVerrijking = ADR.dr.Item("straatnaam") & "|" & ADR.dr.Item("plaatsnaam") & "|" & ADR.dr.Item("gemeentenaam") & "|" & ADR.dr.Item("provincienaam") & "|"
            If ADR.dt.Rows.Count > 1 Then
                For Each ADR.dr In ADR.dt.Rows
                    If ADR.dr.Item("huisnr_bag_letter") <> "" Then
                        sToev &= ADR.dr.Item("huisnr_bag_letter") & ","
                    End If
                Next
                sAdresVerrijking &= sToev
            Else
                sAdresVerrijking &= ADR.dr.Item("huisnr_bag_letter")
            End If
            sAdresVerrijking &= "|" & ADR.dr.Item("lengtegraad") & "|" & ADR.dr.Item("breedtegraad")
        End If
        Return sAdresVerrijking
    End Function
End Class