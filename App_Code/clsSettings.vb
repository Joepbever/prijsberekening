Imports System.Data
Imports System.Data.SqlClient

Public Class clsSettings
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand


    Public bExact As Boolean = False
    Public bVoorraad As Boolean = False
    Public bMyParcel As Boolean = False
    Public bFactuurPDF As Boolean = False
    Public bControleArtikelVariantID As Boolean = False
    Public bOfferteAanvragen As Boolean = False
    Public bVerzendkostenGratis As Boolean = False
    Public bGerelateerdeArtikelenProduct As Boolean = False
    Public bGerelateerdeArtikelenWinkelwagen As Boolean = False
    Public bKortingsCode As Boolean = True
    Public bVerzendmethode As Boolean = False
    Public bCompany As Boolean = True
    Public bFollowCookie As Boolean = False


    Public Sub InitWebshopSettings(iPartijIDBeheerder As Long)
        dt = sSettingBySettingType("WebShop", iPartijIDBeheerder)
        If dt.Rows.Count > 0 Then
            For Each Me.dr In dt.Rows
                Dim bValue As Boolean = False
                Dim sValue As String = dr.Item("sValue").ToString().ToLower()
                If (sValue = "ja") Or sValue = "true" Then
                    bValue = True
                End If
                Select Case dr.Item("sGroup").ToString().ToLower()
                    Case "bexact"
                        bExact = bValue
                    Case "bvoorraad"
                        bVoorraad = bValue
                    Case "bmyparcel"
                        bMyParcel = bValue
                    Case "bfactuurpdf"
                        bFactuurPDF = bValue
                    Case "bcontroleartikelvariantid"
                        bControleArtikelVariantID = bValue
                    Case "verzendkosten gratis"
                        bVerzendkostenGratis = bValue
                    Case "bVerzendmethode"
                        bVerzendmethode = bValue
                    Case "bofferteaanvragen"
                        bOfferteAanvragen = bValue
                    Case "gerelateerde artikelen product"
                        bGerelateerdeArtikelenProduct = bValue
                    Case "gerelateerde artikelen winkelwagen"
                        bGerelateerdeArtikelenWinkelwagen = bValue
                    Case "bkortingscode"
                        bKortingsCode = bValue
                    Case "bcompany"
                        bCompany = bValue
                    Case "bfollowcookie"
                        bFollowCookie = bValue
                End Select
            Next
        End If
    End Sub

    Public Function scSettingByTypeAndGroup(sType As String, sGroup As String, iPartijID As Long) As String
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT sValue FROM tblSettings WHERE (sSettingType = @sType) AND (sGroup = @sGroup) AND (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sGroup", sGroup)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.Scalar(cmd)
    End Function

    Public Function sSettingBySettingType(sSettingType As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM tblSettings WHERE (sSettingType = @sSettingType) AND (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sSettingType", sSettingType)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function uSettingByTypeAndGroup(sValue As String, sGroup As String, sSettingType As String, iPartijID As Long) As Boolean
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE tblSettings SET sValue = @sValue WHERE (sGroup = @sGroup) AND (sSettingType = @sSettingType) AND (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sValue", sValue)
        cmd.Parameters.AddWithValue("@sGroup", sGroup)
        cmd.Parameters.AddWithValue("@sSettingType", sSettingType)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.Update(cmd)
    End Function
End Class