Imports System.Data
Imports System.Data.SqlClient

Public Class oKorting
    Public iFactuurBedrag_ As Decimal = 0
    Public iFactuurBedragExcl_ As Decimal = 0
    Public iFactuurBedrag As Decimal = 0
    Public iBtwBedrag As Decimal = 0
    Public iBtwBedrag_ As Decimal = 0
    Public iBedrag As Decimal = 0
    Public iKortingsBedrag As Decimal = 0
    Public iPercentage As Decimal = 0
    Public sKortingsCode As String = ""
    Public sOmschrijving As String = ""
    Public sMelding As String = ""
    Public sSoort As String = ""
    Public bOk As Boolean = False
End Class

Public Class clsKortingCodes
    Inherits oKorting
    Private F As New clsFacRgl

    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sKortingsCodeByStatus(iPartijID As Long, sKortingsCode As String, sStatus As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM KortingCodes WHERE (iPartijID = @iPartijID) AND (sKortingsCode = @sKortingsCode) AND (sStatus = @sStatus) AND (iAantalKeer > 0)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sKortingsCode", sKortingsCode)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sKortingsCodesByFacKop(iPartijIDBeheerder As Long, iFacKopID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT KortingCodes.iAantalKeer, KortingCodes.iKortingsCodeID FROM FacRgl INNER JOIN KortingCodes ON FacRgl.sArtikel = KortingCodes.sKortingsCode AND FacRgl.iPartijID = KortingCodes.iPartijID WHERE (FacRgl.iPartijID = @iPartijIDBeheerder) AND (FacRgl.iFacKopID = @iFacKopID) ORDER BY FacRgl.iFacRglID DESC"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function uStatus(iKortingsCodeID As Long, sStatus As String, iAantalKeer As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE KortingCodes SET sStatus = @sStatus, iAantalKeer = @iAantalKeer WHERE (iKortingsCodeID = @iKortingsCodeID)"
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@iAantalKeer", iAantalKeer)
        cmd.Parameters.AddWithValue("@iKortingsCodeID", iKortingsCodeID)
        Return CON.Update(cmd)
    End Function
    Public Function KortingsBedragViaSession(ByVal iPartijID As Long, ByVal sKortingsCode As String, ByVal sSession As String) As Decimal

        ' TODO meertaligheid

        iKortingsBedrag = 0

        ' Kortingscode data ophalen
        dt = sKortingsCodeByStatus(iPartijID, sKortingsCode, "Actief")

        If dt.Rows.Count = 0 Then
            sMelding = Resources.Resource.KortingNietgeldig
            bOk = False
            iKortingsBedrag = 0
            Return iKortingsBedrag
        Else
            dr = dt.Rows(0)
            sSoort = dr.Item("sSoort")
            sOmschrijving = dr.Item("sOmschrijving")
            sKortingsCode = dr.Item("sKortingsCode")

            F.dt = F.sFacRglsByTypeAndSession("korting", sSession, iPartijID)
            For Each F.dr In F.dt.Rows
                If F.dr.Item("sArtikel") = sKortingsCode Then
                    sMelding = Resources.Resource.KortingscodeBestaatal
                    bOk = False
                    Return iKortingsBedrag
                End If
            Next
            If F.dt.Rows.Count > 0 Then
                sMelding = Resources.Resource.KortingscodeBestaatal
                bOk = False
                Return iKortingsBedrag
            End If
            F.dt.Clear()
            F.dt = F.sFacRglsByTypeAndSession("artikel", sSession, iPartijID)
            For Each F.dr In F.dt.Rows
                iBedrag += F.dr.Item("iBedrag")
                iBtwBedrag += F.dr.Item("iBtwBedrag")
            Next



            iFactuurBedrag_ = iBedrag ' + PiBtwBedrag
            iFactuurBedragExcl_ = iBedrag
            iBtwBedrag_ = iBtwBedrag

            If dr.Item("sSoort") = "percentage" Then
                iKortingsBedrag = FormatCurrency((iFactuurBedrag_ / 100) * dr.Item("iWaarde"))
                iFactuurBedrag = iFactuurBedrag_ + iKortingsBedrag
                sMelding = Resources.Resource.KortingToegevoegd
                bOk = True
                Return iKortingsBedrag
            Else
                iKortingsBedrag = dr.Item("iWaarde")
                iFactuurBedrag = iFactuurBedrag_ + iKortingsBedrag
                sMelding = Resources.Resource.KortingToegevoegd
                bOk = True
                Return dr.Item("iWaarde")
            End If
        End If
    End Function
End Class