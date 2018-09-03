Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services

Partial Class Data_LeadsOverzicht
    Inherits BasePage
    Dim CAT As New clsCategorie
    'Dim U As New clsUtility

    'Dim sStatus, sZoekterm As String
    'Dim iMaxRows, currentPageNumber, iPartijIDIngelogd, iParentID As Long
    'Dim sSort As String = "iVolgorde"
    'Dim iArtikelID As Long = 0
    'Dim sType As String
    Dim iCatIDs() As String
    Public sCatIDs As String = ""
    Public count As Long = 0
    Public iTaalID As Long = 0
    Public bIsOnline As Boolean

    Public bFirstLoad As Boolean


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Request.QueryString("iTaalID") IsNot Nothing Then
            iTaalID = Request.QueryString("iTaalID")
        Else
            iTaalID = sTaalID("NL")
        End If

        If Request.QueryString("firstLoad") IsNot Nothing Then
            bFirstLoad = Request.QueryString("firstLoad")
        Else
            bFirstLoad = False
        End If
        If Request.QueryString("iCatIDs") IsNot Nothing Then
            sCatIDs = Request.QueryString("iCatIDs")
            iCatIDs = Split(Request.QueryString("iCatIDs"), ",")
        End If


        If Request.QueryString("count") IsNot Nothing Then
            count = Request.QueryString("count")
        End If


        'Extra categorie toevoegen aan de variabelen i.v.m. "NIEUW"
        'If Request.QueryString("bNew") = True Then
        '    If sCatIDs <> "" Then
        '        ReDim Preserve iCatIDs(iCatIDs.Length)
        '        iCatIDs(iCatIDs.Length - 1) = 403
        '        sCatIDs = sCatIDs & ",403"
        '        count = count + 1
        '    Else
        '        sCatIDs = "403"
        '        count = 1
        '    End If
        'End If



        bIsOnline = IsOnline()
        CreateButton()
        BindCategories()
        BindArtikelen()


    End Sub

    Private Sub CreateButton()


        If sCatIDs = "" Then
            aDealerButton.Visible = False
        Else
            Dim iCount As Integer = iCatIDs.Count
            Dim iCatID As Long = 0
            Dim sTitle As String = ""
            Dim sQs As String = ""
            If iCatIDs.Contains(622) Then
                'HJ
                iCatID = 622
                sTitle = "Henry Jullien"
                sQs = "henry-jullien"
            End If
            If iCatIDs.Contains(379) Then
                'TS
                iCatID = 379
                sTitle = "Tree Spectacles"
                sQs = "treespectacles"
            End If
            If iCatIDs.Contains(378) Then
                'MQ
                iCatID = 378
                sTitle = "Monoqool"
                sQs = "monoqool"
            End If
            If iCatIDs.Contains(377) Then
                'CF
                iCatID = 377
                sTitle = "Clayton Franklin"
                sQs = "claytonfranklin"
            End If
            If iCatIDs.Contains(376) Then
                'CZ
                iCatID = 376
                sTitle = "C-Zone"
                sQs = "czone"
            End If
            If iCatIDs.Contains(375) Then
                'AA
                iCatID = 375
                sTitle = "Anglo American"
                sQs = "anglo-american"

            End If
            ' #locator

            If iCatID <> 0 Then
                aDealerButton.HRef = "/" & sQs & "#locator"
                ltlDealer.Text = sTitle
            Else
                aDealerButton.Visible = False
            End If
            'If iCount > 0 Then
            '    If iCount > 1 Then
            '        aDealerButton.Visible = False
            '    Else



            '    End If
            'End If
        End If


    End Sub

    Private Sub BindArtikelen()

        Dim prijsMin As Integer = Request.QueryString("prijsMin")
        Dim prijsMax As Integer = Request.QueryString("prijsMax")
        Dim sort As String = Request.QueryString("sort")

        Dim ART As New clsArtikelen

        If Request.QueryString("pageIndex") IsNot Nothing Then
            'saved search
            Dim iPageIndex As Long = Request.QueryString("pageIndex")
            ART.dt = sArtikelenByCatIDsSearch(iPageIndex, sCatIDs, count, prijsMin, prijsMax).Tables.Item("Artikelen")
        Else
            ART.dt = sArtikelenByCatIDs(1, sCatIDs, count, prijsMin, prijsMax).Tables.Item("Artikelen")
        End If

        'Dim dv As DataView = ART.dt.DefaultView
        'If sort = "1" Then
        '    sort = "iPrijsInkoop ASC"
        'Else
        '    sort = "iPrijsInkoop DESC"
        'End If
        'dv.Sort = sort
        ' ltlAantal.Text = ART.dt.Rows.Count
        If ART.dt.Rows.Count > 0 Then
            ART.dr = ART.dt.Rows(0)
            repArtikelen.DataSource = ART.dt
            repArtikelen.DataBind()
        End If


    End Sub

    Public Sub BindCategories()
        CAT.dt = CAT.sCategorieByParentID(iPartijIDBeheerder, iTaalID, 0)

        repCategorie.DataSource = CAT.dt
        repCategorie.DataBind()

    End Sub


    <WebMethod()>
    Public Shared Function sArtikelen(ByVal pageIndex As Integer, sCatIDs As String, iCount As Long, iPrijsMin As Integer, iPrijsMax As Integer, sort As String) As String ' , bNew As Boolean
        'SCROLL FOR NEW FUNCTION
        'If bNew = True Then
        '    If sCatIDs <> "" Then
        '        sCatIDs = sCatIDs & ",403"
        '    Else
        '        sCatIDs = "403"
        '    End If
        '    iCount = iCount + 1
        'End If

        Return sArtikelenByCatIDs(pageIndex, sCatIDs, iCount, iPrijsMin, iPrijsMax).GetXml
    End Function
    Public Shared Function sArtikelenByCatIDsSearch(ByVal pageIndex As Integer, sCatIDs As String, iCount As Long, iPrijsMin As Integer, iPrijsMax As Integer) As DataSet
        Dim query As String = "[sArtikelenByCatIDs3]"
        Dim cmd As SqlCommand = New SqlCommand(query)
        cmd.CommandType = CommandType.StoredProcedure

        Dim iPagesize As Integer = pageIndex * 9 'Hoeveel artikelen
        Dim iStartRowIndex As Long = 1 'Bij welk artikel moet deze beginnen
        'If pageIndex = 1 Then
        '    iStartRowIndex = 1
        'Else
        '    iStartRowIndex = ((pageIndex - 1) * iPagesize) + 1
        'End If

        Dim BP As New BasePage
        cmd.Parameters.AddWithValue("@iPartijID", BP.iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@startRowIndex", iStartRowIndex)
        cmd.Parameters.AddWithValue("@iCount", iCount)
        cmd.Parameters.AddWithValue("@sCatIDs", sCatIDs)
        cmd.Parameters.AddWithValue("@PageIndex", 1)
        cmd.Parameters.AddWithValue("@PageSize", iPagesize)
        cmd.Parameters.AddWithValue("@iPrijsMin", iPrijsMin)
        cmd.Parameters.AddWithValue("@iPrijsMax", iPrijsMax)
        cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
        Return GetData(cmd)
    End Function
    Public Shared Function sArtikelenByCatIDs(ByVal pageIndex As Integer, sCatIDs As String, iCount As Long, iPrijsMin As Integer, iPrijsMax As Integer) As DataSet
        Dim query As String = "[sArtikelenByCatIDs3]"
        Dim cmd As SqlCommand = New SqlCommand(query)
        cmd.CommandType = CommandType.StoredProcedure

        Dim iPagesize As Integer = 9
        Dim iStartRowIndex As Long = 1
        If pageIndex = 1 Then
            iStartRowIndex = 1
        Else
            iStartRowIndex = ((pageIndex - 1) * iPagesize) + 1
        End If

        Dim BP As New BasePage

        cmd.Parameters.AddWithValue("@iPartijID", BP.iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@startRowIndex", iStartRowIndex)
        cmd.Parameters.AddWithValue("@iCount", iCount)
        cmd.Parameters.AddWithValue("@sCatIDs", sCatIDs)
        cmd.Parameters.AddWithValue("@PageIndex", pageIndex)
        cmd.Parameters.AddWithValue("@PageSize", iPagesize)
        cmd.Parameters.AddWithValue("@iPrijsMin", iPrijsMin)
        cmd.Parameters.AddWithValue("@iPrijsMax", iPrijsMax)
        cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
        Return GetData(cmd)
    End Function

    Private Shared Function GetData(ByVal cmd As SqlCommand) As DataSet
        Dim strConnString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As SqlConnection = New SqlConnection(strConnString)
        Dim sda As SqlDataAdapter = New SqlDataAdapter
        cmd.Connection = con
        sda.SelectCommand = cmd
        Dim ds As DataSet = New DataSet
        sda.Fill(ds, "Artikelen")
        Dim dt As DataTable = ds.Tables.Item("Artikelen")
        dt.Columns.Add("sImage")
        dt.Columns.Add("sURL")
        dt.Columns.Add("bNew")


        Dim IMG As New clsImages
        Dim ROUTE As New clsRoutes
        Dim BP As New BasePage
        Dim sLink As String = BP.sURL()
        Dim iTaalID As Long = BP.sTaalID("NL")

        For Each dr As DataRow In dt.Rows
            'Afbeelding tonen
            IMG.dt = IMG.sImageByIDAndSoort(dr.Item("iArtikelID"), "klein", "Artikel")
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                dr("sImage") = IMG.dr.Item("sSmall").replace("~/", sLink)
            Else
                dr("sImage") = "/Resources/img/placehold-small.jpg"
            End If

            ROUTE.dt = ROUTE.sRoutes("artikel", dr.Item("iArtikelID"), BP.iPartijIDBeheerder)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                dr("sURL") = ROUTE.dr.Item("sURL")
            Else
                dr("sURL") = ""
            End If

            Dim CAT As New clsCategorie
            CAT.dt = CAT.sCatByArtikelID(dr.Item("iArtikelID"), iTaalID, 403)
            If CAT.dt.Rows.Count > 0 Then
                dr("bNew") = "ja"
            Else
                dr("bNew") = "nee"
            End If
        Next

        Dim dt2 As DataTable = New DataTable("PageCount")
        dt2.Columns.Add("PageCount")
        dt2.Rows.Add()
        dt2.Rows(0)(0) = cmd.Parameters("@PageCount").Value
        ds.Tables.Add(dt2)

        Dim dt3 As DataTable = New DataTable("RecordCount")
        dt3.Columns.Add("RecordCount")
        dt3.Rows.Add()
        dt3.Rows(0)(0) = cmd.Parameters("@RecordCount").Value
        ds.Tables.Add(dt3)



        Dim bIsOnline As Boolean = BP.IsOnline()
        Dim dt4 As DataTable = New DataTable("bIsOnline")
        dt4.Columns.Add("bIsOnline")
        dt4.Rows.Add()
        dt4.Rows(0)(0) = bIsOnline
        ds.Tables.Add(dt4)



        Return ds
    End Function

    Private Sub repCategorie_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCategorie.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim cblSubCat As CheckBoxList = e.Item.FindControl("cblSubCat")
            Dim iCatID As Long = DataBinder.Eval(e.Item.DataItem, "iCatID")
            'If iCatID = 372 Or iCatID = 373 Then
            '    e.Item.Visible = False
            '    Exit Sub
            'End If

            If iCatID <> 367 Then 'Collectie 
                cblSubCat.Items.Clear()
            End If

            CAT.dt = CAT.sCategorieByParentID(iPartijIDBeheerder, iTaalID, iCatID)
            cblSubCat.DataSource = CAT.dt
            cblSubCat.DataBind()
            cblSubCat.ID = "cblSubCat_" & e.Item.ItemIndex

            Dim bKeuze As Boolean = False 'Om de lijst in en uitklapbaar te maken
            If iCatIDs IsNot Nothing Then
                For Each cb As ListItem In cblSubCat.Items
                    'If cb.Value = "694" And bFirstLoad = True Then 'De eerste load van de pagina moet top 500 aan staan.
                    '    cb.Selected = True
                    '    cb.Attributes.Add("class", "active")
                    'End If
                    If iCatIDs.Contains(cb.Value) Then
                        cb.Selected = True
                        bKeuze = True
                        cb.Attributes.Add("class", "active")
                    End If
                Next
            End If
            If CAT.dt.Rows.Count > 5 And iCatID <> 367 And bKeuze = False Then  'Collectie 694
                Dim divCategorie As HtmlGenericControl = e.Item.FindControl("divCategorie")
                divCategorie.Attributes.Add("class", "filter checkbox-list CBList has-dropdown")
                Dim divAll As HtmlGenericControl = e.Item.FindControl("divAll")
                divAll.Visible = True
            End If
        End If
    End Sub

    Dim IMG As New clsImages
    Dim ROUTE As New clsRoutes
    Private Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim iArtikelID As Long = DataBinder.Eval(e.Item.DataItem, "iArtikelID")

            'Afbeelding tonen
            IMG.dt = IMG.sImageByIDAndSoort(iArtikelID, "klein", "Artikel")
            Dim imgThumb As HtmlImage = e.Item.FindControl("imgThumb")
            If IMG.dt.Rows.Count > 0 Then
                IMG.dr = IMG.dt.Rows(0)
                imgThumb.Src = IMG.dr.Item("sSmall").replace("~/", sURL())
                imgThumb.Alt = IMG.dr.Item("sData")
            Else
                imgThumb.Src = "/Resources/img/placehold-small.jpg"
            End If

            'URL van het artikel
            ROUTE.dt = ROUTE.sRoutes("artikel", iArtikelID, iPartijIDBeheerder)
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                Dim aFollow As HtmlAnchor = e.Item.FindControl("aLink")
                aFollow.HRef = ROUTE.dr.Item("sURL").ToString.Replace("~", "")
                aFollow.ID = "aLink" & iArtikelID.ToString()
            End If

            'Is het artikel nieuw
            Dim CAT As New clsCategorie
            CAT.dt = CAT.sCatByArtikelID(iArtikelID, iTaalID, 403)
            Dim divNew As HtmlGenericControl = e.Item.FindControl("divNew")
            If CAT.dt.Rows.Count > 0 Then
                divNew.Attributes.Add("class", "label")
            Else
                divNew.Attributes.Add("class", "label hidden")
            End If

        End If
    End Sub
End Class