'**
' * CONTENTS
' *
' * QUERY FUNCTIONS
' * Declare publics
' * Basic select query's
' * Select query's with inner joins
' * Insers query's
' * 
' **

Imports System.Data
Imports System.Data.SqlClient

Public Class clsLijstItems
    ' *------------------------------------*
    ' * #Declare publics
    ' *------------------------------------*
    Public dt As New DataTable
    Public dr As DataRow

    'indien je tweede selectie moet doen zoals previous en next buttons
    Public dt2 As New DataTable
    Public dr2 As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand


    Public Function count() As Integer
        Return dt.Rows.Count
    End Function


    ' *------------------------------------*
    ' * #Basic select query's
    ' *------------------------------------*
    Public Function sLijstItemByID(iLijstItemID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM LijstItems WHERE (iLijstItemID = @iLijstItemID)"
        cmd.Parameters.AddWithValue("@iLijstItemID", iLijstItemID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemByTypeAndAfbeelding(sType As String, sAfbeelding As String, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (LijstItems.sAfbeelding = @sAfbeelding) ORDER BY LijstItems.iVolgorde"
        Else
            cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (sAfbeelding = @sAfbeelding)  AND (iTaalID = @iTaalID) AND (iPartijID = @iPartijID) AND (bActief = 1) ORDER BY iVolgorde"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sAfbeelding", sAfbeelding)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLIByYear(sType As String, iTaalID As Long, year As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (YEAR(dtDatum) = @year) AND (bActief = 1) ORDER BY dtDatum DESC"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@year", year)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLIByTypeAndTaal(sType As String, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) ORDER BY LijstItems.iVolgorde"
        Else
            cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (iPartijID = @iPartijID) AND (bActief = 1) ORDER BY iVolgorde"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sLIByTaalIDAndType(sType As String, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) ORDER BY dtDatum DESC"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLIByTaalIDAndTypeAndsTitle(sType As String, iTaalID As Long, sTitle As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT sTitle, iLijstItemID, dtDatum, sAfbeelding FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND sTitle = @sTitle ORDER BY dtDatum DESC"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLIByTypeAndTaalTop(sType As String, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean, iQuantity As Long) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT TOP(@iQuantity) LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) ORDER BY LijstItems.iVolgorde"
        Else
            cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (iPartijID = @iPartijID) AND (bActief = 1) ORDER BY iVolgorde"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iQuantity", iQuantity)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLIByLijstItem(sLijstItem As String, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (sLijstItem = @sLijstItem) ORDER BY LijstItems.iVolgorde"
        Else
            cmd.CommandText = "SELECT * FROM LijstItems WHERE (sLijstItem = @sLijstItem) AND (iTaalID = @iTaalID) AND (iPartijID = @iPartijID) AND (bActief = 1) ORDER BY iVolgorde"
        End If
        cmd.Parameters.AddWithValue("@sLijstItem", sLijstItem)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemByTitleAndType(sTitle As String, iTaalID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM LijstItems WHERE (sTitle = @sTitle) AND (iTaalID = @iTaalID) AND (sType = @sType) AND (bActief = 1) ORDER BY iVolgorde"
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemsByTypeOrderByDate(sType As String, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (dtDatum <= @Now) ORDER BY dtDatum DESC"
        Else
            cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (iPartijID = @iPartijID) AND (bActief = 1) AND (dtDatum <= @Now) ORDER BY dtDatum DESC"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)

        Dim dateNow As Date = Date.Now
        cmd.Parameters.AddWithValue("@Now", dateNow)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemsByTypeAndCategoryOrderByDate(sType As String, iTaalID As Long, iPartijID As Long, iCategoryID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 " &
                            " FROM LijstItems " &
                            " INNER JOIN tblSettings " &
                            " ON LijstItems.sType = tblSettings.sGroup" &
                            " INNER JOIN tussTaalPages " &
                            " ON tblSettings.sPageGuid = tussTaalPages.sGuid " &
                            " INNER JOIN Pages " &
                            " ON tussTaalPages.iPageID = Pages.iPageID " &
                            " LEFT JOIN tussTaalPages AS tussTaalPages2 " &
                            " ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID " &
                            " INNER JOIN tussLijstItems" &
                            " ON tussLijstItems.iLijstItemID_1 = LijstItems.iLijstItemID" &
                            " WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (dtDatum <= @Now)" &
                            " AND tussLijstItems.iLijstItemID_2 = @iCategoryID" &
                            " ORDER BY dtDatum DESC"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iCategoryID", iCategoryID)

        Dim dateNow As Date = Date.Now
        cmd.Parameters.AddWithValue("@Now", dateNow)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemsByTypeOrderByDate_NotSameiLijstitemID(sType As String, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean, iLijstItemID As Long) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (dtDatum <= @Now) AND iLijstItemID <> @iLijstItemID ORDER BY dtDatum DESC"
        Else
            cmd.CommandText = "SELECT * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (iPartijID = @iPartijID) AND (bActief = 1) AND (dtDatum <= @Now) AND iLijstItemID <> @iLijstItemID ORDER BY dtDatum DESC"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iLijstItemID", iLijstItemID)

        Dim dateNow As Date = Date.Now
        cmd.Parameters.AddWithValue("@Now", dateNow)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemsByTypeOrderByDateTop(sType As String, iTaalID As Long, iQuantity As Long, iPartijID As Long, bDetailPage As Boolean) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT TOP(@iQuantity) LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (dtDatum <= @Now) ORDER BY dtDatum DESC"
        Else
            cmd.CommandText = "SELECT TOP(@iQuantity) * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (bActief = 1) AND (dtDatum <= @Now) ORDER BY dtDatum DESC"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iQuantity", iQuantity)

        Dim dateNow As Date = Date.Now
        cmd.Parameters.AddWithValue("@Now", dateNow)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemsByTypeOrderByDateTop_NotSameiLijstitemID(sType As String, iTaalID As Long, iQuantity As Long, iPartijID As Long, bDetailPage As Boolean, iLijstItemID As Long) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT TOP(@iQuantity) LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM LijstItems INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (LijstItems.sType = @sType) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) AND (dtDatum <= @Now) AND iLijstItemID <> @iLijstItemID ORDER BY dtDatum DESC"
        Else
            cmd.CommandText = "SELECT TOP(@iQuantity) * FROM LijstItems WHERE (sType = @sType) AND (iTaalID = @iTaalID) AND (bActief = 1) AND (dtDatum <= @Now) AND iLijstItemID <> @iLijstItemID ORDER BY dtDatum DESC"
        End If
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iQuantity", iQuantity)
        cmd.Parameters.AddWithValue("@iLijstItemID", iLijstItemID)

        Dim dateNow As Date = Date.Now
        cmd.Parameters.AddWithValue("@Now", dateNow)
        Return CON.sDatatable(cmd)
    End Function



    ' *------------------------------------*
    ' * #Select query's with inner joins
    ' *------------------------------------*

    Public Function sLIByLI_2(iLijstItemID As Long, iTaalID As Long, iPartijID As Long, bDetailPage As Boolean) As DataTable
        cmd.Parameters.Clear()
        If bDetailPage Then
            cmd.CommandText = "SELECT tussLijstItems.*, LijstItems.*, tussTaalPages.sQueryString, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages2.sQueryString AS sQueryString2 FROM tussLijstItems INNER JOIN LijstItems ON tussLijstItems.iLijstItemID_2 = LijstItems.iLijstItemID INNER JOIN tblSettings ON LijstItems.sType = tblSettings.sGroup INNER JOIN tussTaalPages ON tblSettings.sPageGuid = tussTaalPages.sGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (tussLijstItems.iLijstItemID_1 = @iLijstItemID) And (LijstItems.bActief = 1) AND (LijstItems.iTaalID = @iTaalID) AND (LijstItems.bActief = 1) AND (tblSettings.iPartijID = @iPartijID) AND (tussTaalPages.iTaalID = @iTaalID) ORDER BY LijstItems.iVolgorde"
        Else
            cmd.CommandText = "SELECT * FROM tussLijstItems INNER JOIN LijstItems ON tussLijstItems.iLijstItemID_2 = LijstItems.iLijstItemID WHERE (tussLijstItems.iLijstItemID_1 = @iLijstItemID) And (LijstItems.bActief = 1) ORDER BY LijstItems.iVolgorde"
        End If

        cmd.Parameters.AddWithValue("@iLijstItemID", iLijstItemID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLIByTypeAndTaalAndsLIByID(sType As String, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT *, LijstItems2.sTitle AS sCatTitle FROM LijstItems INNER JOIN LijstItems AS LijstItems2 ON LijstItems.sLijstItem = LijstItems2.iLijstItemID WHERE (LijstItems.sType = @sType) And (LijstItems.iTaalID = @iTaalID) And (LijstItems.bActief = 1) And (LijstItems2.bActief = 1) ORDER BY LijstItems.iVolgorde"
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLijstItemByTypeAndTitleAndLI_2(sType As String, sTitle As String, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT LijstItems.iLijstItemID AS iLijstItemID_2, LijstItems.sType AS sType_2, LijstItems.sTitle AS sTitle_2, LijstItems.bActief AS bActief_2, tussLijstItems.iLijstItemID_1, tussLijstItems.iTussID, LijstItems_1.* FROM LijstItems INNER JOIN tussLijstItems ON LijstItems.iLijstItemID = tussLijstItems.iLijstItemID_2 INNER JOIN LijstItems AS LijstItems_1 ON tussLijstItems.iLijstItemID_1 = LijstItems_1.iLijstItemID WHERE (LijstItems.sType = @sType) AND (LijstItems.sTitle = @sTitle) AND (LijstItems.bActief = 1) AND (LijstItems_1.iTaalID = @iTaalID) AND (LijstItems_1.bActief = 1) ORDER BY LijstItems_1.iVolgorde"
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sLI_2BysTitleAndLijstitemID(sTitle As String, iTaalID As Long, iLijstitemID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT lijstitems.ilijstitemid, lijstitems.stype, lijstitems.slijstitem, lijstitems.stitle, lijstitems.ssubtitle, lijstitems.bactief, tusslijstitems.ilijstitemid_2, tusslijstitems.ilijstitemid_1, LijstItems_2.ilijstitemid AS iLijstItemID2, LijstItems_2.stitle AS sTitle2, LijstItems_2.ssubtitle AS sSubTitle2, LijstItems_2.sdescription AS sDescription2, LijstItems_2.itaalid AS iTaalID2, LijstItems_2.bactief AS bActief2, LijstItems_2.ivolgorde AS iVolgorde2 FROM lijstitems INNER JOIN tusslijstitems ON lijstitems.ilijstitemid = tusslijstitems.ilijstitemid_2 INNER JOIN lijstitems AS LijstItems_2 ON tusslijstitems.ilijstitemid_1 = LijstItems_2.ilijstitemid WHERE ( lijstitems_2.bactief = 1 ) AND ( lijstitems.stitle = @sTitle ) AND ( lijstitems_2.itaalid = @iTaalID ) AND ( tusslijstitems.ilijstitemid_2 = @iLijstitemID ) ORDER BY ivolgorde2"
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iLijstitemID", iLijstitemID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sSearch(iTaalID As Long, iPartijID As Long, sType As String, sSearchTerm As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM LijstItems WHERE (LijstItems.iTaalID = @iTaalID) AND (LijstItems.iPartijID = @iPartijID) AND (LijstItems.sType = @sType) AND (LijstItems.sTitle + ' ' + LijstItems.sDescription LIKE '%' + @sSearchTerm + '%')"
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sSearchTerm", sSearchTerm)
        Return CON.sDatatable(cmd)
    End Function





    ' *------------------------------------*
    ' * #Insert query's
    ' *------------------------------------*

    Public Function iscLijstitem(iTaalID As Long, iPartijID As Long, sType As String, sLijstItem As String, sTitle As String, sSubTitle As String, sDescription As String, sColor As String, sItem1 As String,
                               sItem2 As String, sItem3 As String, sItem4 As String, sItem5 As String, sDatum As String, bActief As Boolean, iVolgorde As Integer, sPageTitle As String,
                               sPageDescription As String, sKeywords As String, sSummary As String, sAfbeelding As String, dtDatum As Date, sHtml1 As String, sHtml2 As String,
                               sHtml3 As String, sHtml4 As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO LijstItems (iTaalID, iPartijID, sType, sLijstItem, sTitle, sSubTitle, sDescription, sColor, sItem1, sItem2, sItem3, sItem4, sItem5, sDatum, bActief, iVolgorde, sPageTitle, sPageDescription, sKeywords, sSummary, sAfbeelding, dtDatum, sHtml1, sHtml2, sHtml3, sHtml4) VALUES (@iTaalID, @iPartijID, @sType, @sLijstItem, @sTitle, @sSubTitle, @sDescription, @sColor, @sItem1, @sItem2, @sItem3, @sItem4, @sItem5, @sDatum, @bActief, @iVolgorde, @sPageTitle, @sPageDescription, @sKeywords, @sSummary, @sAfbeelding, @dtDatum, @sHtml1, @sHtml2, @sHtml3, @sHtml4); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sLijstItem", sLijstItem)
        cmd.Parameters.AddWithValue("@sTitle", sTitle)
        cmd.Parameters.AddWithValue("@sSubTitle", sSubTitle)
        cmd.Parameters.AddWithValue("@sDescription", sDescription)
        cmd.Parameters.AddWithValue("@sColor", sColor)
        cmd.Parameters.AddWithValue("@sItem1", sItem1)
        cmd.Parameters.AddWithValue("@sItem2", sItem2)
        cmd.Parameters.AddWithValue("@sItem3", sItem3)
        cmd.Parameters.AddWithValue("@sItem4", sItem4)
        cmd.Parameters.AddWithValue("@sItem5", sItem5)
        cmd.Parameters.AddWithValue("@sDatum", sDatum)
        cmd.Parameters.AddWithValue("@bActief", bActief)
        cmd.Parameters.AddWithValue("@iVolgorde", iVolgorde)
        cmd.Parameters.AddWithValue("@sPageTitle", sPageTitle)
        cmd.Parameters.AddWithValue("@sPageDescription", sPageDescription)
        cmd.Parameters.AddWithValue("@sKeywords", sKeywords)
        cmd.Parameters.AddWithValue("@sSummary", sSummary)
        cmd.Parameters.AddWithValue("@sAfbeelding", sAfbeelding)
        cmd.Parameters.AddWithValue("@dtDatum", dtDatum)
        cmd.Parameters.AddWithValue("@sHtml1", sHtml1)
        cmd.Parameters.AddWithValue("@sHtml2", sHtml2)
        cmd.Parameters.AddWithValue("@sHtml3", sHtml3)
        cmd.Parameters.AddWithValue("@sHtml4", sHtml4)
        Return CON.Scalar(cmd)
    End Function
End Class