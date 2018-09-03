Imports System.Data
Imports System.Data.SqlClient

Public Class clsArtikelItems
    Public dt As New DataTable
    Public dr As DataRow

    Public dt2 As New DataTable
    Public dr2 As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand



    Public sNaam As String = ""
    Public sOmschrijving As String = ""

    Public Sub sArtikel(iArtikelID As Long, iTaalID As Long)
        dt = sArtikelItemsByArtikelID(iArtikelID, iTaalID)
        For Each Me.dr In dt.Rows
            Select Case dr.Item("sType").ToString.ToLower
                Case "naam"
                    sNaam = dr.Item("sWaarde")
                Case "omschrijving"
                    sOmschrijving = dr.Item("sWaarde")
            End Select
        Next
    End Sub

    Public Function sArtikelItemsLIKEWaarde(sWaarde As String, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iArtikelItemID, iArtikelID, iTaalID, sType, sWaarde, sComponent FROM ArtikelItems WHERE (sWaarde LIKE '%' + @sWaarde + '%') AND (iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@sWaarde", sWaarde)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function
    'Public Function sByArtikelIDAndTaalID(iArtikelID As Long, iTaalID As Long) As DataTable
    '    cmd.Parameters.Clear()
    '    cmd.CommandText = "SELECT ArtikelItems.iTaalID, ArtikelItems.sType, ArtikelItems.sWaarde, ArtikelItems.sComponent, SpecificatieGroepen.iVolgorde FROM ArtikelItems INNER JOIN SpecificatieGroepen ON ArtikelItems.sType = SpecificatieGroepen.Specificatie WHERE (ArtikelItems.iArtikelID = @iArtikelID) AND (ArtikelItems.iTaalID = @iTaalID) ORDER BY SpecificatieGroepen.iVolgorde"
    '    cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
    '    cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
    '    Return CON.sDatatable(cmd)
    'End Function
    Public Function sByArtikelIDAndTaalID(iArtikelID As Long, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iTaalID, sType, sWaarde, sComponent FROM ArtikelItems WHERE (iArtikelID = @iArtikelID) AND (iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelItemsByArtikelID(iArtikelID As Long, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM ArtikelItems WHERE (iArtikelID = @iArtikelID) AND (iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function
End Class