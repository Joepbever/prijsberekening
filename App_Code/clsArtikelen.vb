Imports System.Data.SqlClient
Imports System.Data

Public Class clsArtikelen


    Public Class drArtikel
        Public Property sArtikel As String
        Public Property sOmschrijving As String
        Public Property sSpecificatiegroep As String
        Public Property sArtikelGroep As String
        Public Property sPrijs As String
        Public Property sAantalperEenheid As String
        Public Property sAantalinVoorraad As String
        Public Property sArtikelcode As String
        Public Property sBTW As String
        Public Property sKorting As String
        Public Property sLevertijd As String
        Public Property sPrijsIngelogd As String
        Public Property sPrijsUitgelogd As String
        Public Property sFotos As String
        Public Property sCategorie As String
        Public Property sSpecificaties As String
    End Class

    'Hieronder staan aantal oude functies met datasets. Deze datasets kan je vinden in oude projecten zoals ninety-fout, a-touch-of-italy, phe-spareparts

    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sArtikelen(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(4) Artikelen.iArtikelID, Artikelen.iKortingPercentage, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd FROM Artikelen WHERE bActief = 1 AND iPartijID = @iPartijID ORDER BY Artikelen.iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelenByFacKopID(iFacKopID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.* FROM FacRgl INNER JOIN FacKop ON FacRgl.iFacKopID = FacKop.iFacKopID INNER JOIN Artikelen ON FacRgl.iArtikelID = Artikelen.iArtikelID WHERE (FacKop.iFacKopID = @iFacKopID)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelenBySessieID(sSession As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.* FROM FacRgl INNER JOIN Artikelen ON FacRgl.iArtikelID = Artikelen.iArtikelID WHERE (FacRgl.sSession = @sSession)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sArtikelByIDdt(iArtikelID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Artikelen WHERE iArtikelID = @iArtikelID"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sArtikelByID(iArtikelID As Long) As DataRow
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Artikelen WHERE iArtikelID = @iArtikelID"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        Return CON.sRow(cmd)
    End Function
    Public Function sArtikelAndItemsByID(iArtikelID As Long, iTaalID As Long, sType As String) As DataRow
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.iArtikelID, Artikelen.iPrijs, ArtikelItems.sWaarde, Artikelen.sOmschrijving, Artikelen.sBtw, Artikelen.sKorting FROM Artikelen INNER JOIN ArtikelItems ON Artikelen.iArtikelID = ArtikelItems.iArtikelID WHERE (Artikelen.iArtikelID = @iArtikelID) AND (ArtikelItems.sType = @sType) AND (ArtikelItems.iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sRow(cmd)
    End Function
    Public Function sArtikelgroepen(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT DISTINCT sArtikelGroep FROM Artikelen WHERE iPartijID = @iPartijID AND bActief = 1 ORDER BY sArtikelGroep"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelenByKorting(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.*, Routes.sURL FROM Artikelen INNER JOIN Routes ON Artikelen.iArtikelID = Routes.iID WHERE (Artikelen.iPartijID = @iPartijID) AND (Artikelen.bActief = 1) AND (Artikelen.sKorting <> '0') AND (Artikelen.sKorting <> '0.00') AND (Routes.sType = 'artikel') ORDER BY Artikelen.iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sArtikelenByArtikelgroep(iPartijID As Long, sArtikelGroep As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iArtikelID, sArtikel, sArtikel + ' (+' + CONVERT(varchar, iPrijs) + ')' AS VerzendMethode, sOmschrijving FROM Artikelen WHERE iPartijID = @iPartijID AND sArtikelGroep = @sArtikelGroep AND bActief = 1 ORDER BY iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sArtikelGroep", sArtikelGroep)
        Return CON.sDatatable(cmd)
    End Function


    'Public Function sArtikelenByCategorie(iTaalID As Long, iCatID As Long) As DataTable
    '    cmd.Parameters.Clear()
    '    cmd.CommandText = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage FROM  tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (tussCategorieArtikelen.iCatID = @iCatID) And (CategorieItems.iTaalID = @iTaalID) And (Artikelen.bActief = 1) ORDER BY Artikelen.iVolgorde"
    '    cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
    '    cmd.Parameters.AddWithValue("@iCatID", iCatID)
    '    Return CON.sDatatable(cmd)
    'End Function

    Public Function sArtikelenByCategorie(iTaalID As Long, iCatID As Long, Optional iCount As Long = 0) As DataTable
        cmd.Parameters.Clear()
        If iCount = 0 Then
            cmd.CommandText = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage FROM  tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (tussCategorieArtikelen.iCatID = @iCatID) And (CategorieItems.iTaalID = @iTaalID) And (Artikelen.bActief = 1) ORDER BY Artikelen.iVolgorde"
        Else
            cmd.CommandText = "SELECT TOP(@iCount) tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage FROM  tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (tussCategorieArtikelen.iCatID = @iCatID) And (CategorieItems.iTaalID = @iTaalID) And (Artikelen.bActief = 1) ORDER BY Artikelen.iVolgorde"
            cmd.Parameters.AddWithValue("@iCount", iCount)
        End If

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function



    Public Function sArtikelenAndRoutesByCategorie(iTaalID As Long, iCatID As Long, Optional iCount As Long = 0) As DataTable
        cmd.Parameters.Clear()
        If iCount = 0 Then
            cmd.CommandText = "SELECT Artikelen.sArtikel, tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.iPrijsInkoop, Artikelen.iPrijs, Artikelen.iKortingPercentage, Routes.sURL" &
                                " FROM tussCategorieArtikelen " &
                                " INNER JOIN Artikelen " &
                                " ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID " &
                                " INNER JOIN CategorieItems " &
                                " ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID " &
                                " INNER JOIN Routes " &
                                " ON Artikelen.iArtikelID = Routes.iID  " &
                                " WHERE (tussCategorieArtikelen.iCatID = @iCatID) " &
                                " AND (CategorieItems.iTaalID = @iTaalID) " &
                                " AND (Artikelen.bActief = 1) " &
                                " AND (Routes.iTaalID = @iTaalID) " &
                                " AND (Routes.sType = 'artikel') " &
                                " ORDER BY Artikelen.iVolgorde"
        Else
            cmd.CommandText = "SELECT TOP(@iCount) Artikelen.sArtikel, tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.iPrijsInkoop, Artikelen.iPrijs, Artikelen.iKortingPercentage, Routes.sURL" &
                                " FROM tussCategorieArtikelen " &
                                " INNER JOIN Artikelen " &
                                " ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID " &
                                " INNER JOIN CategorieItems " &
                                " ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID " &
                                " INNER JOIN Routes " &
                                " ON Artikelen.iArtikelID = Routes.iID  " &
                                " WHERE (tussCategorieArtikelen.iCatID = @iCatID) " &
                                " AND (CategorieItems.iTaalID = @iTaalID) " &
                                " AND (Artikelen.bActief = 1) " &
                                " AND (Routes.iTaalID = @iTaalID) " &
                                " AND (Routes.sType = 'artikel') " &
                                " ORDER BY Artikelen.iVolgorde"
            cmd.Parameters.AddWithValue("@iCount", iCount)
        End If

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sArtikelenByCategorieNaam(iTaalID As Long, sCatName As String, Optional iCount As Long = 0) As DataTable
        cmd.Parameters.Clear()
        If iCount = 0 Then
            cmd.CommandText = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage FROM  tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (CategorieItems.sTitle = @sCatName) And (CategorieItems.iTaalID = @iTaalID) And (Artikelen.bActief = 1) ORDER BY Artikelen.iVolgorde"
        Else
            cmd.CommandText = "SELECT TOP(@iCount) tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage FROM  tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (CategorieItems.sTitle = @sCatName) And (CategorieItems.iTaalID = @iTaalID) And (Artikelen.bActief = 1) ORDER BY Artikelen.iVolgorde"
            cmd.Parameters.AddWithValue("@iCount", iCount)
        End If

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sCatName", sCatName)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelenByCategorieNaamAndImages(iTaalID As Long, sCatName As String, Optional iCount As Long = 0) As DataTable
        cmd.Parameters.Clear()
        If iCount = 0 Then
            cmd.CommandText = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Images.*,  Routes.*, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage " &
                                " FROM  tussCategorieArtikelen " &
                                " INNER JOIN Artikelen " &
                                " ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID " &
                                " INNER JOIN CategorieItems " &
                                " ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID" &
                                " INNER JOIN Routes " &
                                " ON Routes.sType = 'Artikel' AND Routes.iID = Artikelen.iArtikelID AND Routes.iTaalID = @iTaalID" &
                                " LEFT JOIN tussImages" &
                                " ON tussImages.iID = Artikelen.iArtikelID AND tussImages.sTussType = 'Artikel'" &
                                " LEFT JOIN Images" &
                                " ON Images.iImageID = tussImages.iImageID" &
                                " WHERE (CategorieItems.sTitle = @sCatName) " &
                                " And (CategorieItems.iTaalID = @iTaalID) " &
                                " And (Artikelen.bActief = 1) " &
                                " AND (Images.sSoort = 'Thumb')" &
                                " ORDER BY Artikelen.iVolgorde"
        Else
            cmd.CommandText = "SELECT TOP(@iCount) tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Images.*, Routes.*, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage " &
                                " FROM  tussCategorieArtikelen " &
                                " INNER JOIN Artikelen " &
                                " ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID " &
                                " INNER JOIN CategorieItems " &
                                " ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID" &
                                " INNER JOIN Routes " &
                                " ON Routes.sType = 'Artikel' AND Routes.iID = Artikelen.iArtikelID AND Routes.iTaalID = @iTaalID" &
                                " LEFT JOIN tussImages" &
                                " ON tussImages.iID = Artikelen.iArtikelID AND tussImages.sTussType = 'Artikel'" &
                                " LEFT JOIN Images" &
                                " ON Images.iImageID = tussImages.iImageID" &
                                " WHERE (CategorieItems.sTitle = @sCatName) " &
                                " And (CategorieItems.iTaalID = @iTaalID) " &
                                " And (Artikelen.bActief = 1) " &
                                " AND (Images.sSoort = 'Thumb')" &
                                " ORDER BY Artikelen.iVolgorde"
            cmd.Parameters.AddWithValue("@iCount", iCount)
        End If

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sCatName", sCatName)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelenByCategorieIDAndImages(iTaalID As Long, iCatID As Long, Optional iCount As Long = 0) As DataTable
        cmd.Parameters.Clear()
        If iCount = 0 Then
            cmd.CommandText = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Images.*,  Routes.*, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage " &
                                "	FROM  tussCategorieArtikelen " &
                                "	INNER JOIN Artikelen " &
                                "	ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID " &
                                "	INNER JOIN CategorieItems " &
                                "	ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID" &
                                "	INNER JOIN Routes " &
                                "	ON Routes.sType = 'Artikel' AND Routes.iID = Artikelen.iArtikelID AND Routes.iTaalID = @iTaalID" &
                                "	LEFT JOIN tussImages" &
                                "	ON tussImages.iTussID = (" &
                                "							SELECT TOP 1 iTussID " &
                                "							FROM tussImages" &
                                "							WHERE tussImages.iID = Artikelen.iArtikelID AND sTussType = 'Artikel'" &
                                "							)LEFT JOIN Images" &
                                "	ON Images.iImageID = tussImages.iImageID" &
                                "	WHERE (CategorieItems.iCatID = @iCatID) " &
                                "	And (CategorieItems.iTaalID = @iTaalID) " &
                                "	And (Artikelen.bActief = 1) " &
                                "	AND (Images.sSoort = 'Thumb')" &
                                "	ORDER BY Artikelen.iVolgorde"

        Else
            cmd.CommandText = "SELECT TOP @iCount tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Images.*,  Routes.*, Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.sArtikelGroep, Artikelen.sOmschrijving, Artikelen.iPrijsInkoop, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.iTaalID, Artikelen.sArtikel, Artikelen.iPrijs, Artikelen.iKortingPercentage " &
                                "	FROM  tussCategorieArtikelen " &
                                "	INNER JOIN Artikelen " &
                                "	ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID " &
                                "	INNER JOIN CategorieItems " &
                                "	ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID" &
                                "	INNER JOIN Routes " &
                                "	ON Routes.sType = 'Artikel' AND Routes.iID = Artikelen.iArtikelID AND Routes.iTaalID = @iTaalID" &
                                "	LEFT JOIN tussImages" &
                                "	ON tussImages.iTussID = (" &
                                "							SELECT TOP 1 iTussID " &
                                "							FROM tussImages" &
                                "							WHERE tussImages.iID = Artikelen.iArtikelID AND sTussType = 'Artikel'" &
                                "							)LEFT JOIN Images" &
                                "	ON Images.iImageID = tussImages.iImageID" &
                                "	WHERE (CategorieItems.iCatID = @iCatID) " &
                                "	And (CategorieItems.iTaalID = @iTaalID) " &
                                "	And (Artikelen.bActief = 1) " &
                                "	AND (Images.sSoort = 'Thumb')" &
                                "	ORDER BY Artikelen.iVolgorde"
            cmd.Parameters.AddWithValue("@iCount", iCount)
        End If

        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sArtikelNaamByCategorie(iCatID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.sArtikel, Artikelen.iArtikelID FROM tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID WHERE (tussCategorieArtikelen.iCatID = @iCatID) ORDER BY Artikelen.iVolgorde"
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sArtikelenByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Artikelen WHERE iPartijID = @iPartijID AND bActief = 1 ORDER BY iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function ListArtikelgroepen(iPartijID As Long, sQs As String, sArtikelgroep As String) As String
        Dim s As New StringBuilder
        'later bekijken of we dit o.b.v. iTaalID 
        Dim dt As New DataTable
        dt = sArtikelgroepen(iPartijID)
        s.Append("<ul class='shopMenu'>")
        If sArtikelgroep = "" Then
            s.Append("<li class='active'><a href='/" & sQs & "'>All products</a></li>")
        Else
            s.Append("<li><a href='/" & sQs & "'>All products</a></li>")
        End If
        Dim BP As New BasePage
        For Each dr As DataRow In dt.Rows
            If sArtikelgroep = dr.Item("sArtikelGroep").ToString().ToLower() Then
                s.Append("<li class='active'><a href='/" & sQs & "/" & BP.FriendlyUrl(dr.Item("sArtikelGroep")) & "'>" & dr.Item("sArtikelGroep") & "</a></li>")
            Else
                s.Append("<li><a href='/" & sQs & "/" & BP.FriendlyUrl(dr.Item("sArtikelGroep")) & "'>" & dr.Item("sArtikelGroep") & "</a></li>")
            End If
        Next
        s.Append("</ul>")
        Return s.ToString()
    End Function



    'Public Function sArtikelen(iPartijID As Long) As dsArtikelen.dtArtikelenDataTable
    '    Try
    '        dtART = daART.sArtikelen(iPartijID)
    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function scArtikelNaam(ByVal iArtikelID As Long) As String
    '    Return daART.scArtikelnaam(iArtikelID)
    'End Function

    'Public Function sSamengesteldeArtikelenByItemType(ByVal sCode As String, ByVal sType As String, ByVal sWaarde As String) As dsArtikelen.dtArtikelenDataTable
    '    Try

    '        Dim BP As New BasePage
    '        Dim iTaalID As Long = BP.sTaalID(sCode)
    '        Dim CT As New clsCategorie
    '        dtART = daART.sArtikelenByItemType(sCode, sType, sWaarde, BP.iPartijIDBeheerder)

    '        Dim i As Integer = 0
    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sByArtikelIDAndTaalID(drART.iArtikelID, iTaalID)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems

    '                If dtART.Columns.Contains(ARTITEMS.drArtItems.sType) = False Then
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                End If
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde
    '            Next
    '            If i = 0 Then
    '                dtART.Columns.Add("sCode")
    '                dtART.Columns.Add("iCatID")
    '                dtART.Columns.Add("sHoofdCategorie")
    '                dtART.Columns.Add("sSubCategorie")
    '            End If
    '            drART.Item("sCode") = sCode
    '            Try
    '                CT.drCat = CT.daCat.sHoofdSubCategorieByArtikelID(drART.iArtikelID, iTaalID).Rows(0)
    '                drART.Item("iCatID") = CT.drCat.iCatID
    '                drART.Item("sHoofdCategorie") = CT.drCat.Item("sHoofdCategorie")
    '                drART.Item("sSubCategorie") = CT.drCat.Item("sSubCategorie")
    '            Catch ex As Exception
    '                drART.Item("iCatID") = ""
    '                drART.Item("sHoofdCategorie") = ""
    '                drART.Item("sSubCategorie") = ""
    '            End Try


    '            i = i + 1
    '        Next

    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function dArtikel(ByVal iArtikelID As Long) As Boolean
    '    Try
    '        Dim ARTITEMS As New clsArtikelItems
    '        Dim IMG As New clsImages

    '        'Artikel verwijderen
    '        daART.Delete(iArtikelID)

    '        'Artikel Items verwijderen
    '        ARTITEMS.daArtItems.dItemsByArtikelID(iArtikelID)

    '        'Ophalen van alle foto's en verwijderen van foto
    '        IMG.dtImg = IMG.daImg.sImageByTussTypeAndID("Artikel", iArtikelID)
    '        For Each IMG.drImg In IMG.dtImg
    '            File.Delete(HttpContext.Current.Server.MapPath(IMG.drImg.sSmall))
    '            File.Delete(HttpContext.Current.Server.MapPath(IMG.drImg.sBig))
    '            File.Delete(HttpContext.Current.Server.MapPath(IMG.drImg.sOriginal))
    '            IMG.daImg.Delete(IMG.drImg.iImageID)
    '        Next
    '        'Verwijderen uit Images tabel


    '        'Verwijderen uit tussImages tabel
    '        IMG.daTussImg.dByIDAndType(iArtikelID, "Artikel")

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Public Function sSamengesteldeArtikelenByLijstItemID(ByVal iLijstItemID As Long, ByVal sType As String, ByVal sCode As String, ByVal FilterArtikelGroep As Boolean) As dsArtikelen.dtArtikelenDataTable
    '    Try

    '        dtART = daART.sArtikelenByLijstItemID(iLijstItemID, sType)
    '        If FilterArtikelGroep = True Then
    '            Dim sArtikelGroep As String = ""
    '            Dim cnt As Integer = dtART.Rows.Count

    '            For i As Integer = 0 To cnt
    '                Try
    '                    If sArtikelGroep = dtART.Rows(i).Item("sArtikelGroep") Then
    '                        dtART.Rows.RemoveAt(i)
    '                        cnt = dtART.Rows.Count
    '                        i = i - 1
    '                    End If

    '                    sArtikelGroep = dtART.Rows(i).Item("sArtikelGroep")
    '                Catch ex As Exception
    '                    Exit For
    '                End Try
    '            Next
    '        End If

    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '                Try
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                Catch ex As Exception
    '                End Try
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '            Next

    '        Next

    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function sSamengesteldeArtikelenByCatIDs(ByVal sCatIDs As String, ByVal sCode As String, ByVal FilterArtikelGroep As Boolean, Optional ByVal sArt As String = "") As DataTable
    '    Try
    '        Dim UTIL As New clsUtility
    '        'Dim ids As String() = sCatIDs.Split("|")
    '        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
    '        'Dim query As String = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.iPrijs, Artikelen.sArtikel, Artikelen.sArtikelCode, Artikelen.sArtikelGroep FROM tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID WHERE (tussCategorieArtikelen.iCatID = " & sCatIDs & ")"
    '        'Dim query As String = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.iPrijs, Artikelen.sArtikel, Artikelen.sArtikelCode, Artikelen.sArtikelGroep, ArtikelItems.sWaarde AS Prijstonen FROM ArtikelItems INNER JOIN Talen ON ArtikelItems.iTaalID = Talen.iTaalID INNER JOIN tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID ON ArtikelItems.iArtikelID = Artikelen.iArtikelID WHERE (tussCategorieArtikelen.iCatID = " & sCatIDs & ") AND (Talen.sCode = '" & sCode & "') AND (ArtikelItems.sType = 'prijstonen') ORDER BY Artikelen.sArtikelGroep"
    '        'Dim query As String = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.iPrijs, Artikelen.sArtikel, Artikelen.sArtikelCode, Artikelen.sArtikelGroep, Artikelen.iVrdFysiek, Artikelen.sKorting, ArtikelItems.sWaarde AS Prijstonen, ArtikelItems_1.sWaarde AS ArtikelNaam FROM ArtikelItems INNER JOIN Talen ON ArtikelItems.iTaalID = Talen.iTaalID INNER JOIN tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID ON ArtikelItems.iArtikelID = Artikelen.iArtikelID INNER JOIN ArtikelItems AS ArtikelItems_1 ON Talen.iTaalID = ArtikelItems_1.iTaalID AND Artikelen.iArtikelID = ArtikelItems_1.iArtikelID WHERE (tussCategorieArtikelen.iCatID = " & sCatIDs & ") AND (Talen.sCode = '" & sCode & "') AND (ArtikelItems.sType = 'prijstonen') AND (ArtikelItems_1.sType = 'Artikelnaam') ORDER BY Artikelen.sArtikelGroep"
    '        'Dim query As String = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.iPrijs, Artikelen.sArtikelCode, Artikelen.sArtikelGroep, Artikelen.iVrdFysiek, Artikelen.sKorting, ArtikelItems.sWaarde AS Prijstonen, ArtikelItems_1.sWaarde AS ArtikelNaam FROM ArtikelItems INNER JOIN tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID ON ArtikelItems.iArtikelID = Artikelen.iArtikelID INNER JOIN ArtikelItems AS ArtikelItems_1 ON Artikelen.iArtikelID = ArtikelItems_1.iArtikelID WHERE (tussCategorieArtikelen.iCatID = @sCatID) AND (ArtikelItems.sType = 'prijstonen') AND (ArtikelItems_1.sType = 'Artikelnaam') AND (ArtikelItems_1.iTaalID = @iTaalID) AND (ArtikelItems.iTaalID = @iTaalID) ORDER BY Artikelen.sArtikelGroep"

    '        Dim query As String = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.iPrijs, Artikelen.sArtikelCode, Artikelen.sArtikelGroep, Artikelen.sKorting, ArtikelItems_1.sWaarde AS ArtikelNaam, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.iLevertijd FROM tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN ArtikelItems AS ArtikelItems_1 ON Artikelen.iArtikelID = ArtikelItems_1.iArtikelID WHERE (Artikelen.bActief = 1) AND (tussCategorieArtikelen.iCatID = @sCatID) AND (ArtikelItems_1.sType = 'Artikelnaam') AND (ArtikelItems_1.iTaalID = @iTaalID) AND (Artikelen.iPartijID = @iPartijID) ORDER BY Artikelen.sArtikelGroep"

    '        If sArt <> "" Then
    '            query = "SELECT tussCategorieArtikelen.iCatID, Artikelen.iArtikelID, Artikelen.iPrijs, Artikelen.sArtikelCode, Artikelen.sArtikelGroep, Artikelen.sKorting, ArtikelItems_1.sWaarde AS ArtikelNaam, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.iLevertijd FROM tussCategorieArtikelen INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID INNER JOIN ArtikelItems AS ArtikelItems_1 ON Artikelen.iArtikelID = ArtikelItems_1.iArtikelID WHERE (Artikelen.bActief = 1) AND (Artikelen.sArtikelGroep = @sArtikelGroep) AND (tussCategorieArtikelen.iCatID = @sCatID) AND (ArtikelItems_1.sType = 'Artikelnaam') AND (ArtikelItems_1.iTaalID = @iTaalID) AND (Artikelen.iPartijID = @iPartijID) ORDER BY Artikelen.sArtikelGroep"
    '        End If

    '        'Dim cnt2 As Integer = ids.Count()
    '        'Dim int As Integer = 1
    '        'For Each id In ids
    '        '    If int = cnt2 Then
    '        '        query &= " (tussCategorieArtikelen.iCatID = " & id & ")"
    '        '    Else
    '        '        query &= " (tussCategorieArtikelen.iCatID = " & id & ") OR"
    '        '        End If
    '        '    int = int + 1
    '        'Next

    '        Dim BP As New BasePage

    '        conn.Open()
    '        Dim cmd As New SqlCommand(query, conn)
    '        cmd.Parameters.AddWithValue("@sCatID", sCatIDs)
    '        cmd.Parameters.AddWithValue("@iPartijID", BP.iPartijIDBeheerder)
    '        cmd.Parameters.AddWithValue("@iTaalID", BP.sTaalID(sCode))
    '        If sArt <> "" Then
    '            cmd.Parameters.AddWithValue("@sArtikelGroep", sArt)
    '        End If
    '        Dim dt As New DataTable
    '        dt.Load(cmd.ExecuteReader())
    '        If FilterArtikelGroep = True Then
    '            Dim sArtikelGroep As String = ""
    '            Dim cnt As Integer = dtART.Rows.Count
    '            For i As Integer = 0 To cnt
    '                Try
    '                    If sArtikelGroep = dtART.Rows(i).Item("sArtikelGroep") Then
    '                        dtART.Rows.RemoveAt(i)
    '                        cnt = dtART.Rows.Count
    '                        i = i - 1
    '                    End If

    '                    sArtikelGroep = dtART.Rows(i).Item("sArtikelGroep")
    '                Catch ex As Exception
    '                    Exit For
    '                End Try
    '            Next
    '            For Each Me.drART In dtART
    '                ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '                For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '                    Try
    '                        dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                    Catch ex As Exception
    '                    End Try
    '                    drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde
    '                Next
    '            Next

    '        End If


    '        Return dt
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function sSamengesteldeArtikelenByPartijID(ByVal iPartijID As Long, ByVal sCode As String, ByVal FilterArtikelGroep As Boolean) As dsArtikelen.dtArtikelenDataTable
    '    Try

    '        dtART = daART.sArtikelenByPartijID(iPartijID)


    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '                Try
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                Catch ex As Exception
    '                End Try
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '            Next

    '        Next

    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function sSamengesteldeArtikelen(ByVal iCatID As Long, ByVal sCode As String, ByVal FilterArtikelGroep As Boolean) As dsArtikelen.dtArtikelenDataTable
    '    Try

    '        dtART = daART.sArtikelenByCatID(iCatID)
    '        If FilterArtikelGroep = True Then
    '            Dim sArtikelGroep As String = ""
    '            Dim cnt As Integer = dtART.Rows.Count

    '            For i As Integer = 0 To cnt
    '                Try
    '                    If sArtikelGroep = dtART.Rows(i).Item("sArtikelGroep") Then
    '                        dtART.Rows.RemoveAt(i)
    '                        cnt = dtART.Rows.Count
    '                        i = i - 1
    '                    End If

    '                    sArtikelGroep = dtART.Rows(i).Item("sArtikelGroep")
    '                Catch ex As Exception
    '                    Exit For
    '                End Try
    '            Next
    '        End If

    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '                Try
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                Catch ex As Exception
    '                End Try
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '            Next

    '        Next

    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function sSamengesteldeArtikelenByItemType(ByVal sCode As String, ByVal sType As String, ByVal sWaarde As String) As dsArtikelen.dtArtikelenDataTable
    '    Try

    '        Dim CT As New clsCategorie
    '        dtART = daART.sArtikelenByItemType(sCode, sType, sWaarde)

    '        Dim i As Integer = 0
    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems

    '                If dtART.Columns.Contains(ARTITEMS.drArtItems.sType) = False Then
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                End If
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde
    '            Next
    '            If i = 0 Then
    '                dtART.Columns.Add("sCode")
    '                dtART.Columns.Add("iCatID")
    '                dtART.Columns.Add("sCat")
    '            End If
    '            drART.Item("sCode") = sCode
    '            Dim UTIL As New clsUtility

    '            CT.drCat = CT.daCat.sCatByArtikelID(drART.iArtikelID, UTIL.GetLanguageID(sCode)).Rows(0)
    '            drART.Item("iCatID") = CT.drCat.iCatID

    '            drART.Item("sCat") = CT.drCat.Item("sQueryString")

    '            i = i + 1
    '        Next

    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function sSamengesteldArtikel(ByVal iArtikelID As Long, ByVal sCode As String) As dsArtikelen.dtArtikelenRow

    '    dtART = daART.sArtikel(iArtikelID)
    '    drART = dtART.Rows(0)
    '    If dtART.Rows.Count > 0 Then

    '        ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(iArtikelID, sCode)

    '        For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '            Try
    '                dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '            Catch ex As Exception
    '            End Try
    '            drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '        Next

    '    End If


    '    Return dtART.Rows(0)

    'End Function

    'Public Function sSamengesteldeArtikelenByArtikelCode(ByVal sArtikelCode As String, ByVal sCode As String) As dsArtikelen.dtArtikelenDataTable

    '    dtART = daART.sArtikelenByArtikelCode(sArtikelCode)
    '    drART = dtART.Rows(0)
    '    If dtART.Rows.Count > 0 Then

    '        ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '        For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '            Try
    '                dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '            Catch ex As Exception
    '            End Try
    '            drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '        Next

    '    End If


    '    Return dtART

    'End Function

    'Public Function sArtikelenByArtikelgroep(ByVal sArtikelgroep As String, ByVal sCode As String) As dsArtikelen.dtArtikelenDataTable
    '    Try
    '        dtART = daART.sArtikelenByArtikelgroep(sArtikelgroep)

    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '                Try
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                Catch ex As Exception
    '                End Try
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '            Next

    '        Next


    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function sArtikelenByArtikelgroepAndCatID(ByVal sArtikelgroep As String, ByVal iCatID As Long, ByVal sCode As String) As dsArtikelen.dtArtikelenDataTable
    '    Try
    '        dtART = daART.sArtikelenByArtikelGroepAndCatID(sArtikelgroep, iCatID)

    '        For Each Me.drART In dtART
    '            ARTITEMS.dtArtItems = ARTITEMS.daArtItems.sArtikelItemsByArtikelID(drART.iArtikelID, sCode)

    '            For Each ARTITEMS.drArtItems In ARTITEMS.dtArtItems
    '                Try
    '                    dtART.Columns.Add(ARTITEMS.drArtItems.sType)
    '                Catch ex As Exception
    '                End Try
    '                drART.Item(ARTITEMS.drArtItems.sType) = ARTITEMS.drArtItems.sWaarde

    '            Next

    '        Next


    '        Return dtART
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    'Public Function uArtikelVoorraad(ByVal iArtikelID As Long, ByVal iAantal As Integer, Optional ByVal sType As String = "beschikbaar") As Boolean
    '    Try
    '        Select Case sType
    '            Case "gereserveerd"
    '                Q.uVoorraadGereserveerdPlus(iAantal, iArtikelID)
    '                Return True
    '            Case "beschikbaar"
    '                Q.uVoorraadBeschikbaarMin(iAantal, iArtikelID)
    '                Return True
    '            Case "afgerekend"
    '                Q.uVoorraadBeschikbaarMin(iAantal, iArtikelID)
    '                Q.uVoorraadGereserveerdMin(iAantal, iArtikelID)
    '                Return True
    '            Case Else
    '                Return False
    '        End Select
    '    Catch ex As Exception
    '        Dim LOG As New clsLog
    '        LOG.iLog("clsArtikelen.vb", Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex.Message() & ex.StackTrace.ToString())
    '        Return False
    '    End Try


    'End Function

    'Public Function scVoorraadbeschikbaarMinGereserveerd(ByVal iArtikelID As Long) As Integer

    '    Try
    '        Return Q.scVoorraadbeschikbaarMinGereserveerd(iArtikelID)
    '    Catch ex As Exception
    '        Dim LOG As New clsLog
    '        LOG.iLog("clsArtikelen.vb", Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex.Message() & ex.StackTrace.ToString())
    '        Return 0
    '    End Try

    'End Function
End Class