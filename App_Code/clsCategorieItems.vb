Imports System.Data
Imports System.Data.SqlClient

Public Class clsCategorieItems
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sCategorieItemsByCatID(iPartijID As Long, iTaalID As Long, iCatID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Categorie.*, CategorieItems.sPageItem1 AS sPageItem1, CategorieItems.* FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iCatID = @iCatID)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iCatID", iCatID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sCategorieItemsByParentID(iPartijID As Long, iTaalID As Long, iParentID As Long, bWebsite As Boolean) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Categorie.*, CategorieItems.sPageItem1 AS sPageItem1, CategorieItems.* FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID)  AND (Categorie.bWebsite = @bWebsite) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@bWebsite", bWebsite)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieItemsAndRoutesByParentID(iPartijID As Long, iTaalID As Long, iParentID As Long, bWebsite As Boolean, sSoort As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Images.* ,Categorie.iCatID, Categorie.iPartijID, Categorie.iParentID, Categorie.sStatus, Categorie.bActief, Categorie.iImageID, Categorie.iVolgorde, Categorie.sTemplate, Categorie.bWebsite, Categorie.bWebshop, CategorieItems.sPageItem1, CategorieItems.iCatItemID, CategorieItems.iTaalID, CategorieItems.iCatID AS Expr1, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.sPageItem1 AS Expr2, CategorieItems.sPageItem2, CategorieItems.sMetaTitle, CategorieItems.sMetaDescription, Routes.sURL " &
                            " FROM Categorie " &
                            " INNER JOIN CategorieItems " &
                            " ON Categorie.iCatID = CategorieItems.iCatID " &
                            " INNER JOIN Routes " &
                            " ON Categorie.iCatID = Routes.iID " &
                            " LEFT JOIN tussImages" &
                            " ON tussImages.sTussType = 'Categorie' AND tussImages.iID = Categorie.iCatID" &
                            " INNER JOIN Images" &
                            " ON Images.iImageID = tussImages.iImageID" &
                            " WHERE (Categorie.iPartijID = @iPartijID) " &
                            " AND (Categorie.bActief = 1) " &
                            " AND (CategorieItems.iTaalID = @iTaalID) " &
                            " AND (Categorie.iParentID = @iParentID) " &
                            " AND (Categorie.bWebsite = @bWebsite) " &
                            " AND (Routes.sType = 'categorie') " &
                            " AND (Routes.iTaalID = @iTaalID)" &
                            " AND (Images.sSoort = @sSoort)" &
                            " ORDER BY Categorie.iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@bWebsite", bWebsite)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieItemsAndRoutesByParentID(iPartijID As Long, iTaalID As Long, iParentID As Long, bWebsite As Boolean) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Categorie.iCatID, Categorie.iPartijID, Categorie.iParentID, Categorie.sStatus, Categorie.bActief, Categorie.iImageID, Categorie.iVolgorde, Categorie.sTemplate, Categorie.bWebsite, Categorie.bWebshop,                          CategorieItems.sPageItem1, CategorieItems.iCatItemID, CategorieItems.iTaalID, CategorieItems.iCatID AS Expr1, CategorieItems.sTitle, CategorieItems.sQueryString, CategorieItems.sText, CategorieItems.sPageItem1 AS Expr2,                          CategorieItems.sPageItem2, CategorieItems.sMetaTitle, CategorieItems.sMetaDescription, Routes.sURL FROM            Categorie INNER JOIN                          CategorieItems ON Categorie.iCatID = CategorieItems.iCatID INNER JOIN                         Routes ON Categorie.iCatID = Routes.iID WHERE        (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID) AND (Categorie.bWebsite = @bWebsite) AND (Routes.sType = 'categorie') AND (Routes.iTaalID = @iTaalID) ORDER BY Categorie.iVolgorde"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@bWebsite", bWebsite)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieItemsByParentIDMenu(iPartijID As Long, iTaalID As Long, iParentID As Long, bWebsite As Boolean, Optional iCount As Long = 0) As DataTable
        cmd.Parameters.Clear()
        If iCount = 0 Then
            cmd.CommandText = "SELECT Categorie.*, CategorieItems.sPageItem1 AS sPageItem1, CategorieItems.* FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID)  AND (Categorie.bWebsite = @bWebsite) ORDER BY (Categorie.iVolgorde)"
        Else
            cmd.CommandText = "SELECT TOP(@iCount) Categorie.*, CategorieItems.sPageItem1 AS sPageItem1, CategorieItems.* FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID)  AND (Categorie.bWebsite = @bWebsite) ORDER BY (Categorie.iVolgorde)"
            cmd.Parameters.AddWithValue("@iCount", iCount)
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@bWebsite", bWebsite)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sCategorieItemsByParentIDTop(iPartijID As Long, iTaalID As Long, iParentID As Long, quantity As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT TOP(@quantity) * FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (Categorie.iParentID = @iParentID) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@quantity", quantity)
        Return CON.sDatatable(cmd)
    End Function


    Public Function sCategorieItemsAndArtikelenByQueryString(iPartijID As Long, iTaalID As Long, sQueryString As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Categorie INNER JOIN CategorieItems ON Categorie.iCatID = CategorieItems.iCatID INNER JOIN tussCategorieArtikelen ON Categorie.iCatID = tussCategorieArtikelen.iCatID INNER JOIN Artikelen ON tussCategorieArtikelen.iArtikelID = Artikelen.iArtikelID WHERE (Categorie.iPartijID = @iPartijID) AND (Categorie.bActief = 1) AND (CategorieItems.iTaalID = @iTaalID) AND (CategorieItems.sQueryString = @sQueryString) ORDER BY (Categorie.iVolgorde)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sQueryString", sQueryString)
        Return CON.sDatatable(cmd)
    End Function
End Class