Imports System.Data
Imports System.Data.SqlClient

Public Class clsCategorie
    Public dt As New DataTable
    Public dtHoofd As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand


    Public sPublicCategorieURL As String = ""
    Dim sParentsQs As String = ""
    Dim sNiveau1, sNiveau2, sNiveau3, sNiveau4 As String
    Public Function sCategorieURL(iTaalID As Long, iCatID As Long) As String
        sNiveau1 = ""
        sNiveau2 = ""
        sNiveau3 = ""
        sNiveau4 = ""
        dt = sCategorie(iCatID, iTaalID)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            sNiveau1 = "/" & dr.Item("sQueryString")
            sPublicCategorieURL = sNiveau1
            If dr.Item("iParentID") <> 0 Then
                dt = sCategorie(dr.Item("iParentID"), iTaalID)
                If dt.Rows.Count > 0 Then
                    dr = dt.Rows(0)
                    sNiveau2 = "/" & dr.Item("sQueryString")
                    sPublicCategorieURL = sNiveau2 & sNiveau1
                    If dr.Item("iParentID") <> 0 Then
                        dt = sCategorie(dr.Item("iParentID"), iTaalID)
                        If dt.Rows.Count > 0 Then
                            dr = dt.Rows(0)
                            sNiveau3 &= "/" & dr.Item("sQueryString")
                            sPublicCategorieURL = sNiveau3 & sNiveau2 & sNiveau1
                            If dr.Item("iParentID") <> 0 Then
                                dt = sCategorie(dr.Item("iParentID"), iTaalID)
                                If dt.Rows.Count > 0 Then
                                    dr = dt.Rows(0)
                                    sNiveau4 &= "/" & dr.Item("sQueryString")
                                    sPublicCategorieURL = sNiveau4 & sNiveau3 & sNiveau2 & sNiveau1
                                End If

                            End If
                        End If

                    End If
                End If

            End If
        End If
        Return sPublicCategorieURL
    End Function


    Public Function sCategorie(iCatID As Long, iTaalID As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT CategorieItems.iCatItemID, CategorieItems.iTaalID, CategorieItems.iCatID, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.sPageItem1, CategorieItems.sPageItem2, Categorie.iParentID FROM CategorieItems INNER JOIN Categorie ON CategorieItems.iCatID = Categorie.iCatID WHERE (CategorieItems.iCatID = @iCatID) AND (CategorieItems.iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategoriesByParentName(iTaalID As Long, sParentName As String) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT CategorieItems.iCatItemID, CategorieItems.iTaalID, CategorieItems.iCatID, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.sPageItem1, CategorieItems.sPageItem2, Categorie.iParentID " &
                            " FROM CategorieItems " &
                            " INNER JOIN Categorie " &
                            " ON CategorieItems.iCatID = Categorie.iCatID " &
                            " WHERE Categorie.iParentID = (SELECT iCatID " &
                            "								FROM CategorieItems" &
                            "								WHERE sTitle = @iParentName)" &
                            " AND CategorieItems.iTaalID = @iTaalID"
        cmd.Parameters.AddWithValue("@iParentName", sParentName)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCatByArtikelID(iArtikelID As Long, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussCategorieArtikelen.iArtikelID, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.iCatID, CategorieItems.iTaalID FROM tussCategorieArtikelen INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (tussCategorieArtikelen.iArtikelID = @iArtikelID) AND (CategorieItems.iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCatByArtikelID(iArtikelID As Long, iTaalID As Long, iCatID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussCategorieArtikelen.iArtikelID, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.iCatID, CategorieItems.iTaalID FROM tussCategorieArtikelen INNER JOIN CategorieItems ON tussCategorieArtikelen.iCatID = CategorieItems.iCatID WHERE (tussCategorieArtikelen.iArtikelID = @iArtikelID) AND (CategorieItems.iTaalID = @iTaalID) AND CategorieItems.iCatID = @iCatID"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieByCatID(iPartijID As Long, iTaalID As Long, iCatID As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Categorie.*, CategorieItems.* FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iCatID = @iCatID) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sCategorieByQueryString(iPartijID As Long, iTaalID As Long, sQueryString As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (CategorieItems.sQueryString = @sQueryString) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sQueryString", sQueryString)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieByParentID(iPartijID As Long, iTaalID As Long, iParentID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sCategorieAndRoutesByParentID(iPartijID As Long, iTaalID As Long, iParentID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Categorie.iCatID, Categorie.iPartijID, Categorie.iParentID, Categorie.sStatus, Categorie.bActief, Categorie.iImageID, Categorie.iVolgorde, Categorie.sTemplate, Categorie.bWebsite, Categorie.bWebshop, CategorieItems.iCatItemID, CategorieItems.iTaalID, CategorieItems.iCatID AS Expr1, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.sPageItem1, CategorieItems.sPageItem2, CategorieItems.sMetaTitle, CategorieItems.sMetaDescription, Routes.sURL FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID INNER JOIN                         Routes ON Categorie.iCatID = Routes.iID WHERE        (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID) AND (Routes.sType = 'categorie') AND (Routes.iTaalID = @iTaalID) ORDER BY Categorie.iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sCategorieByParentID(iPartijID As Long, iTaalID As Long, iParentID As Long, bWebsite As Boolean) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID) AND (Categorie.bWebsite = @bWebsite) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@bWebsite", bWebsite)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sCategorieByParentIDTOP(iPartijID As Long, iTaalID As Long, iParentID As Long, iCount As Integer) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(@iCount) * FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@iCount", iCount)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieRoutes(iPartijID As Long, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT (SELECT TOP (1) sQueryString "
        cmd.CommandText &= "FROM CategorieItems WHERE (iCatID = root.iCatID) And (iTaalID = @iTaalID)) As niveau1, "
        cmd.CommandText &= "(SELECT TOP (1) sQueryString "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems_4 "
        cmd.CommandText &= "WHERE (iCatID = down1.iCatID) AND (iTaalID = @iTaalID)) AS niveau2, "
        cmd.CommandText &= "(SELECT TOP (1) sQueryString "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems_3 "
        cmd.CommandText &= "WHERE (iCatID = down2.iCatID) And (iTaalID = @iTaalID)) AS niveau3, "
        cmd.CommandText &= "(SELECT TOP (1) sQueryString "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems_2 "
        cmd.CommandText &= "WHERE (iCatID = down3.iCatID) AND (iTaalID = @iTaalID)) AS niveau4, "
        cmd.CommandText &= "(SELECT TOP (1) iCatID "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems "
        cmd.CommandText &= "WHERE (iCatID = root.iCatID) And (iTaalID = @iTaalID)) AS niveau1ID, "
        cmd.CommandText &= "(SELECT TOP (1) iCatID "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems_4 "
        cmd.CommandText &= "WHERE (iCatID = down1.iCatID) AND (iTaalID = @iTaalID)) AS niveau2ID, "
        cmd.CommandText &= "(SELECT TOP (1) iCatID "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems_3 "
        cmd.CommandText &= "WHERE (iCatID = down2.iCatID) And (iTaalID = @iTaalID)) AS niveau3ID, "
        cmd.CommandText &= "(SELECT TOP (1) iCatID "
        cmd.CommandText &= "FROM CategorieItems AS CategorieItems_2 "
        cmd.CommandText &= "WHERE (iCatID = down3.iCatID) And (iTaalID = @iTaalID)) As niveau4ID, "
        cmd.CommandText &= "(SELECT TOP (1) sTemplate "
        cmd.CommandText &= "FROM Categorie AS CategorieItems_4 "
        cmd.CommandText &= "WHERE (iCatID = root.iCatID)) As template1, "
        cmd.CommandText &= "(SELECT TOP (1) sTemplate "
        cmd.CommandText &= "FROM Categorie AS CategorieItems_3 "
        cmd.CommandText &= "WHERE (iCatID = down1.iCatID)) As template2, "
        cmd.CommandText &= "(SELECT TOP (1) sTemplate "
        cmd.CommandText &= "FROM Categorie AS CategorieItems_2 "
        cmd.CommandText &= "WHERE (iCatID = down2.iCatID)) As template3, "
        cmd.CommandText &= "(SELECT TOP (1) sTemplate "
        cmd.CommandText &= "FROM Categorie AS CategorieItems "
        cmd.CommandText &= "WHERE (iCatID = down3.iCatID)) As template4 "
        cmd.CommandText &= "FROM Categorie AS root LEFT OUTER JOIN "
        cmd.CommandText &= "Categorie AS down1 ON down1.iParentID = root.iCatID LEFT OUTER JOIN "
        cmd.CommandText &= "Categorie AS down2 ON down2.iParentID = down1.iCatID LEFT OUTER JOIN "
        cmd.CommandText &= "Categorie As down3 On down3.iParentID = down2.iCatID "
        cmd.CommandText &= "WHERE (root.iParentID = 0) AND (root.iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function



    'Public Function sCategories(ByVal sCode As String, ByVal iPartijID As Long) As String
    '    Dim s As New StringBuilder
    '    'later bekijken of we dit o.b.v. iTaalID 
    '    dt = sCategorieByCatID(0)
    '    For Each dr In dt.rows
    '        s.Append("<li class='dropdown'>")
    '        s.Append("<a href='/categories/" & sCode & "/" & drCat.Item("sQueryString").ToString().ToLower() & "/" & drCat.Item("iCatID") & "'>" & drCat.Item("sTitle") & "<i class='fa fa-angle-down'></i></a>")
    '        dtSubCat = daCat.sCategories(iPartijID, sCode, drCat.iCatID)
    '        If dtSubCat.Rows.Count > 0 Then
    '            s.Append("<ul role='menu' class='sub-menu'>")
    '            For Each drSubCat In dtSubCat
    '                s.Append("<li><a href='/products/" & sCode & "/" & drCat.Item("sQueryString").ToString().ToLower() & "/" & drSubCat.Item("sQueryString").ToString().ToLower() & "/" & drSubCat.Item("iCatID") & "'>" & drSubCat.Item("sTitle") & "</a></li>")
    '            Next
    '            s.Append("</ul>")
    '        End If
    '        s.Append("</li>")
    '    Next
    '    Return s.ToString()
    'End Function


End Class