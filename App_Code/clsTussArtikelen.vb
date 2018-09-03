Imports System.Data
Imports System.Data.SqlClient

Public Class clsTussArtikelen
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sTussArtikelenJoinArtikelen(iArtikelID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM tussArtikelen INNER JOIN Artikelen ON Artikelen.iArtikelID = tussArtikelen.iArtikelID_2 WHERE (tussArtikelen.iArtikelID_1 = @iArtikelID) AND (Artikelen.bActief = 1) ORDER BY Artikelen.iVolgorde"

        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sDatatable(cmd)
    End Function



    Public Function sTussArtikelenJoinArtikelenWithRoutes(iArtikelID As Long, iTaalID As Long, sRouteType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussArtikelen.iTussID, tussArtikelen.iArtikelID_1, tussArtikelen.iArtikelID_2, Artikelen.sArtikel, Artikelen.iArtikelID, Routes.iTaalID, Routes.sType, Routes.sURL FROM tussArtikelen INNER JOIN Artikelen ON Artikelen.iArtikelID = tussArtikelen.iArtikelID_2 INNER JOIN Routes ON Artikelen.iArtikelID = Routes.iID WHERE (tussArtikelen.iArtikelID_1 = @iArtikelID) AND (Artikelen.bActief = 1) AND (Routes.iTaalID = @iTaalID) AND (Routes.sType = @sRouteType) ORDER BY Artikelen.iVolgorde"

        cmd.Parameters.AddWithValue("@sRouteType", sRouteType)

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sTussArtikelenJoinArtikelenWithRoutesAndName(iArtikelID As Long, iTaalID As Long, sRouteType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.iArtikelID, Routes.sURL, ArtikelItems.sWaarde FROM            tussArtikelen INNER JOIN                         Artikelen ON Artikelen.iArtikelID = tussArtikelen.iArtikelID_2 INNER JOIN                         Routes ON Artikelen.iArtikelID = Routes.iID INNER JOIN                         ArtikelItems ON Routes.iID = ArtikelItems.iArtikelID WHERE        (tussArtikelen.iArtikelID_1 = @iArtikelID) AND (Artikelen.bActief = 1) AND (Routes.iTaalID = @iTaalID) AND (Routes.sType = @sRouteType) AND (ArtikelItems.sType = 'naam') AND (ArtikelItems.iTaalID = @iTaalID) ORDER BY Artikelen.iVolgorde"

        cmd.Parameters.AddWithValue("@sRouteType", sRouteType)

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sDatatable(cmd)
    End Function

End Class
