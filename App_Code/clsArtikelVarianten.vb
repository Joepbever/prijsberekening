Imports System.Data.SqlClient
Imports System.Data

Public Class clsArtikelVarianten

    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Dim FR As New clsFacRgl

    Dim iArtikelVariantID As Long

    Public sMelding As String = ""
    Public Function CheckVoorraad(sSession As String, iPartijID As Long) As Boolean
        Dim bOk As Boolean = True
        Dim FR As New clsFacRgl
        Dim dt As New DataTable
        dt = FR.sFacRglsByTypeAndSession("artikel", sSession, iPartijID)
        Dim iArtikelVariantID As Long
        Dim iBeschikbaar, iBeschikbaarMinAantal As Integer
        For Each dr As DataRow In dt.Rows
            iArtikelVariantID = dr.Item("iArtikelVariantID")
            iBeschikbaar = scBeschikbaar(iArtikelVariantID)
            iBeschikbaarMinAantal = iBeschikbaar - CInt(dr.Item("iAantal"))

            If iBeschikbaarMinAantal < 0 Then
                If iBeschikbaar > 0 Then
                    'Nog een aantal over, aantal updaten
                    FR.uFacRglAantal(iBeschikbaar, dr.Item("iFacRglID"), sSession)
                    bOk = False
                    sMelding &= "- " & dr.Item("sArtikel") & " size: " & dr.Item("sOmschrijving") & "<br />"
                Else
                    'Geenv voorraad meer. De hele record verwijderen
                    sMelding &= "- " & dr.Item("sArtikel") & " size: " & dr.Item("sOmschrijving") & "<br />"
                    FR.dFacRglByFacRglID(dr.Item("iFacRglID"))
                    bOk = False
                End If
            End If
        Next
        Return bOk
    End Function

    Public Function uVoorraadGereserveerd(sSession As String, iPartijID As Long, bOptellen As Boolean) As Boolean
        Dim bOk As Boolean = True
        dt = FR.sFacRglsByTypeAndSession("artikel", sSession, iPartijID)
        For Each Me.dr In dt.Rows
            iArtikelVariantID = dr.Item("iArtikelVariantID")
            uGereserveerd(iArtikelVariantID, CInt(dr.Item("iAantal")), bOptellen)
        Next
        Return bOk
    End Function

    Public Function uVoorraadGereserveerdByFacKopID(iFacKopID As String, bOptellen As Boolean) As Boolean
        Dim bOk As Boolean = True
        dt = FR.sFacRglsByFacKopID(iFacKopID, "artikel")
        For Each Me.dr In dt.Rows
            iArtikelVariantID = dr.Item("iArtikelVariantID")
            uGereserveerd(iArtikelVariantID, CInt(dr.Item("iAantal")), bOptellen)
        Next
        Return bOk
    End Function

    Public Function uVoorraadBeschikbaarByFacKopID(iFacKopID As String, bOptellen As Boolean) As Boolean
        Dim bOk As Boolean = True
        dt = FR.sFacRglsByFacKopID(iFacKopID, "artikel")
        For Each Me.dr In dt.Rows
            iArtikelVariantID = dr.Item("iArtikelVariantID")
            uBeschikbaar(iArtikelVariantID, CInt(dr.Item("iAantal")), bOptellen)
        Next
        Return bOk
    End Function

    Public Function sArtikelVariantenBySoort(iArtikelID As Long, sSoort As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT *, CONVERT(varchar, CONVERT(int, iHoogte)) + ' cm' AS sHoogte FROM ArtikelVarianten WHERE iArtikelID = @iArtikelID AND sSoort = @sSoort AND (iVrdBeschikbaar - iVrdGereserveerd) > 0 AND bActief = 1 ORDER BY iVolgorde"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelVariantByID(iArtikelVariantID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM ArtikelVarianten WHERE iArtikelVariantID = @iArtikelVariantID ORDER BY iVolgorde DESC"
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelVariantenByArtikelID(iArtikelID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM ArtikelVarianten WHERE iArtikelID = @iArtikelID ORDER BY iVolgorde DESC"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sArtikelVariantenBeschikbaar(iArtikelID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM ArtikelVarianten WHERE iArtikelID = @iArtikelID AND (iVrdBeschikbaar - iVrdGereserveerd) > 0 AND bActief = 1 ORDER BY iVolgorde"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function scBeschikbaar(iArtikelVariantID As Long) As Integer
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT SUM(iVrdBeschikbaar - iVrdGereserveerd) AS iVrdMinimaal FROM ArtikelVarianten WHERE (iArtikelVariantID = @iArtikelVariantID)"
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        Return CON.Scalar(cmd)
    End Function

    Public Function uBeschikbaar(iArtikelVariantID As Long, iAantal As Integer, bOptellen As Boolean) As Boolean
        cmd.Parameters.Clear()
        If bOptellen Then
            cmd.CommandText = "UPDATE ArtikelVarianten SET iVrdBeschikbaar = iVrdBeschikbaar + @iAantal WHERE iArtikelVariantID = @iArtikelVariantID"
        Else
            cmd.CommandText = "UPDATE ArtikelVarianten SET iVrdBeschikbaar = iVrdBeschikbaar - @iAantal WHERE iArtikelVariantID = @iArtikelVariantID"
        End If
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        cmd.Parameters.AddWithValue("@iAantal", iAantal)
        Return CON.Update(cmd)
    End Function

    Public Function uGereserveerd(iArtikelVariantID As Long, iAantal As Integer, bOptellen As Boolean) As Boolean
        cmd.Parameters.Clear()
        If bOptellen Then
            cmd.CommandText = "UPDATE ArtikelVarianten SET iVrdGereserveerd = iVrdGereserveerd + @iAantal WHERE iArtikelVariantID = @iArtikelVariantID"
        Else
            cmd.CommandText = "UPDATE ArtikelVarianten SET iVrdGereserveerd = iVrdGereserveerd - @iAantal WHERE iArtikelVariantID = @iArtikelVariantID"
        End If
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        cmd.Parameters.AddWithValue("@iAantal", iAantal)
        Return CON.Update(cmd)
    End Function
End Class