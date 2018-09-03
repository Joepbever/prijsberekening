Imports System.Data
Imports System.Data.SqlClient

Public Class clsPage
    Dim PI As New clsPageItems

    Dim BP As New BasePage

    Public dt As New DataTable
    Public dr As DataRow

    Public dt2 As New DataTable
    Public dr2 As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sLijstItemRoutes(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT DISTINCT Talen.sCode, tblSettings.sDescription, tussTaalPages.sQueryString, tussTaalPages.iPageID, tussTaalPages2.iPageID AS iPageID2, tussTaalPages2.sQueryString AS sQueryString2, Pages.iParentID, tussTaalPages2.iTaalID AS iTaalID2, tussTaalPages.iTaalID FROM tussTaalPages INNER JOIN tblSettings ON tussTaalPages.sGuid = tblSettings.sPageGuid INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID INNER JOIN Talen ON tussTaalPages.iTaalID = Talen.iTaalID LEFT JOIN tussTaalPages AS tussTaalPages2 ON Pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages.iTaalID = tussTaalPages2.iTaalID WHERE (tblSettings.iPartijID = @iPartijID) AND (tblSettings.sPageGuid <> '') AND (tblSettings.sSettingType = 'Dashboard')"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sPageByID(ByVal sCode As String, ByVal iPageID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussTaalPages.sText, tussTaalPages.sQueryString, Talen.sCode, Pages.sURL, Pages.iPageID, Pages.bActief, Pages.iParentID FROM Talen INNER JOIN tussTaalPages ON Talen.iTaalID = tussTaalPages.iTaalID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID WHERE (Talen.sCode = @sCode) AND (tussTaalPages.iPageID = @iPageID)"
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@iPageID", iPageID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sPageByGuid(ByVal sCode As String, ByVal sGuid As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussTaalPages.sText, tussTaalPages.sQueryString, Talen.sCode, Talen.iPartijID, Pages.sURL, Pages.iPageID, Pages.bActief, Pages.iParentID FROM Talen INNER JOIN tussTaalPages ON Talen.iTaalID = tussTaalPages.iTaalID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID WHERE (Talen.sCode = @sCode) AND (tussTaalPages.sGuid = @sGuid) AND (Talen.iPartijID = @iPartijiD) ORDER BY Pages.iVolgorde"
        cmd.Parameters.AddWithValue("@sGuid", sGuid)
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sPagesByActief(sCode As String, ByVal sMenu As String, iParentID As Long, bActief As Boolean, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussTaalPages.sText, tussTaalPages.sQueryString, Talen.sCode, Pages.sURL, Pages.iPageID, Pages.bActief FROM Talen INNER JOIN tussTaalPages ON Talen.iTaalID = tussTaalPages.iTaalID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID WHERE (Talen.sCode = @sCode) AND (Pages.sMenu = @sMenu) AND (Pages.iParentID = @iParentID) AND (Pages.bActief = @bActief) AND (Talen.iPartijID = @iPartijID) ORDER BY Pages.iVolgorde"
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@sMenu", sMenu)
        cmd.Parameters.AddWithValue("@iParentID", iParentID)
        cmd.Parameters.AddWithValue("@bActief", bActief)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sParentPages(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Pages.sURL, tussTaalPages.sQueryString, tussTaalPages.sGuid, tussTaalPages.sText, Talen.sCode, tussTaalPages_1.sQueryString AS sParentPage FROM Talen INNER JOIN tussTaalPages ON Talen.iTaalID = tussTaalPages.iTaalID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID INNER JOIN tussTaalPages AS tussTaalPages_1 ON Pages.iParentID = tussTaalPages_1.iPageID WHERE (Talen.iPartijID = @iPartijID) AND (Pages.iParentID <> 0)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sChildPagesByPartijID(iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Pages.sURL, tussTaalPages.sQueryString, tussTaalPages.sGuid, tussTaalPages.sText, Talen.sCode FROM Talen INNER JOIN tussTaalPages ON Talen.iTaalID = tussTaalPages.iTaalID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID WHERE (Talen.iPartijID = @iPartijID) AND (Pages.iParentID) = 0"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function


    Public sPageTitle As String
    Public sPageQs As String
    Public Function sPageUrlByGuid(ByVal sLanguage As String, ByVal sGuid As String) As String
        sLanguage = sLanguage.ToLower
        dt = sPageByGuid(sLanguage, sGuid, BP.iPartijIDBeheerder)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            sPageTitle = dr.Item("sText")
            sPageQs = dr.Item("sQueryString").ToString.ToLower
            Dim T As New clsTalen

            T.dt = T.sTalenByPartijID(BP.iPartijIDBeheerder)

            If T.dt.Rows.Count > 1 Then
                'Er zijn meerdere talen
                If CLng(dr.Item("iParentID")) > 0 Then
                    'Dit is een subpage
                    Dim sChildPage As String = dr.Item("sQueryString").ToString.ToLower
                    dt = sPageByID(sLanguage, CLng(dr.Item("iParentID")))
                    If dt.Rows.Count > 0 Then
                        dr = dt.Rows(0)
                        Return "/" & sLanguage & "/" & dr.Item("sQueryString").ToString.ToLower & "/" & sChildPage
                    End If

                    If dr.Item("sQueryString").toLower = "home" And sLanguage.ToLower() = BP.sDefaultLanguage Then
                        Return "/"
                    Else
                        Return "/" & sLanguage & "/" & dr.Item("sQueryString").ToString.ToLower
                    End If
                Else
                    If dr.Item("sQueryString").toLower = "home" And sLanguage.ToLower() = BP.sDefaultLanguage Then
                        Return "/"
                    Else
                        Return "/" & sLanguage & "/" & dr.Item("sQueryString").ToString.ToLower
                    End If
                End If
            Else
                'Er is maar 1 taal
                If CLng(dr.Item("iParentID")) > 0 Then
                    'Dit is een subpage
                    Dim sChildPage As String = dr.Item("sQueryString").ToString.ToLower
                    dt = sPageByID(sLanguage, CLng(dr.Item("iParentID")))
                    If dt.Rows.Count > 0 Then
                        dr = dt.Rows(0)
                        Return dr.Item("sQueryString").ToString.ToLower & "/" & sChildPage
                    End If

                    If dr.Item("sQueryString").toLower = "home" And sLanguage.ToLower() = BP.sDefaultLanguage Then
                        Return "/"
                    Else
                        Return dr.Item("sQueryString").ToString.ToLower
                    End If
                Else
                    If dr.Item("sQueryString").toLower = "home" And sLanguage.ToLower() = BP.sDefaultLanguage Then
                        Return "/"
                    Else
                        Return "/" & dr.Item("sQueryString").ToString.ToLower
                    End If
                End If
            End If
        End If
        Return "/"
    End Function

    'Public Function sPageUrlByGuid(ByVal sLanguage As String, ByVal sGuid As String) As String
    '    dt = sPageByGuid(sLanguage, sGuid, BP.iPartijIDBeheerder)
    '    If dt.Rows.Count > 0 Then
    '        dr = dt.Rows(0)
    '        sPageTitle = dr.Item("sText")
    '        sPageQs = dr.Item("sQueryString").ToString.ToLower
    '        If CLng(dr.Item("iParentID")) > 0 Then
    '            'Dit is een subpage
    '            Dim sChildPage As String = dr.Item("sQueryString").ToString.ToLower
    '            dt = sPageByID(sLanguage, CLng(dr.Item("iParentID")))
    '            If dt.Rows.Count > 0 Then
    '                dr = dt.Rows(0)
    '                Return "/" & sLanguage.ToLower & "/" & dr.Item("sQueryString").ToString.ToLower & "/" & sChildPage
    '            End If

    '            If dr.Item("sQueryString").toLower = "home" And sLanguage.ToLower() = BP.sDefaultLanguage Then
    '                Return "/"
    '            Else
    '                Return "/" & sLanguage.ToLower & "/" & dr.Item("sQueryString").ToString.ToLower
    '            End If
    '        Else
    '            If dr.Item("sQueryString").toLower = "home" And sLanguage.ToLower() = BP.sDefaultLanguage Then
    '                Return "/"
    '            Else
    '                Return "/" & sLanguage.ToLower & "/" & dr.Item("sQueryString").ToString.ToLower
    '            End If
    '        End If
    '    End If
    '    Return "/"
    'End Function

    Public Function CreateMenu(sCode As String, iPartijID As Long, sMenuSoort As String, sQs As String, ByVal sParent As String) As String
        Dim sMenu As New StringBuilder
        'Hier wordt het hoofdmenu opgehaald
        dt = sPagesByActief(sCode, sMenuSoort, 0, True, iPartijID)
        'normaal menu
        'Dim BP As New BasePage
        'Dim sQs As String = BP.RouteData.Values("page").ToString()
        If sQs = "" Then
            sQs = "home"
        End If


        Dim bMeertalig As Boolean = False
        Dim TAL As New clsTalen
        TAL.dt = TAL.sTalenByPartijID(iPartijID)
        If TAL.dt.Rows.Count > 1 Then
            'meertalig
            bMeertalig = True
        Else
            '1 taal
            bMeertalig = False
        End If


        Dim count As Integer = 0
        For Each Me.dr In dt.Rows
            Dim sQueryString As String = dr.Item("sQueryString").ToString().ToLower()
            dt2 = sPagesByActief(sCode, sMenuSoort, dr.Item("iPageID"), True, iPartijID)

            Dim sLink As String = ""

            'If count = 1 Then
            '    Dim CAT As New clsCategorie
            '    sMenu.Append(CAT.sCategories(sCode, iPartijID))
            'End If
            If dt2.Rows.Count > 0 Then
                sMenu.Append("<li class='dropdown'>")
                If sQueryString.Split("#")(0) <> "home" Or sCode.ToLower <> BP.sDefaultLanguage.ToLower Then
                    If dr.Item("sQueryString") <> "" Then
                        If bMeertalig Then
                            sLink = "/" & sCode.ToLower & "/" & sQueryString
                        Else
                            sLink = "/" & sQueryString
                        End If
                    End If
                Else
                    If sQueryString.Contains("#") Then
                        sLink = "/" & sQueryString.Split("#")(1)
                    Else
                        sLink = "/"
                    End If
                End If

                If sParent = sQueryString Or sQs = sQueryString Then
                    sMenu.Append("<a href='" & sLink & "' class='dropdown-toggle smooth-scroll active' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>" & dr.Item("sText") & "</a>")
                Else
                    sMenu.Append("<a href='" & sLink & "' class='dropdown-toggle smooth-scroll' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>" & dr.Item("sText") & "</a>")
                End If


                'sMenu.Append("<a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>" & P.drPage.Item("sText") & "<span class='caret'></span></a>")
                sMenu.Append("<ul class='dropdown-menu'>")

                For Each Me.dr2 In dt2.Rows
                    'Hier wordt de link van de subpagina opgebouwd: nl/nieuwe-verpakkingen/stalen-vaten + tekst
                    Dim sQueryStringChild As String = dr2.Item("sQueryString").ToString().ToLower()
                    If bMeertalig Then
                        If sCode.ToLower <> BP.sDefaultLanguage.ToLower Then
                            If sQueryStringChild.Contains("#") Then
                                sLink = "/" & sCode.ToLower & "/" & sQueryString & sQueryStringChild
                            Else
                                sLink = "/" & sCode.ToLower & "/" & sQueryString & "/" & sQueryStringChild
                            End If
                        Else
                            If sQueryStringChild.Contains("#") Then
                                sLink = "/" & sQueryStringChild
                            Else
                                sLink = "/" & sCode.ToLower & "/" & sQueryString & "/" & sQueryStringChild
                            End If
                        End If
                    Else
                        If sQueryString.Contains("#") Then
                            sLink = "/" & sQueryString & sQueryStringChild
                        Else
                            sLink = "/" & sQueryString & "/" & sQueryStringChild
                        End If
                    End If

                    If sQs = sQueryStringChild Then
                        sMenu.Append("<li><a class='smooth-scroll active'  href='" & sLink & "' title='" & dr.Item("sText") & "'>" & dr2.Item("sText") & "</a>")
                    Else
                        sMenu.Append("<li><a class='smooth-scroll'  href='" & sLink & "' title='" & dr.Item("sText") & "'>" & dr2.Item("sText") & "</a>")
                    End If
                Next
                sMenu.Append("</ul>")
            Else
                sMenu.Append("<li>")
                If sQueryString.Split("#")(0) <> "home" Or sCode.ToLower <> BP.sDefaultLanguage.ToLower Then
                    If bMeertalig Then
                        sLink = sCode.ToLower & "/" & sQueryString
                    Else
                        sLink = sQueryString
                    End If
                Else
                    If sQueryString.Contains("#") Then
                        sLink = "#" & sQueryString.Split("#")(1)
                    End If
                End If
                If sQs = sQueryString Then
                    If sQueryString.Contains("#") Then
                        sMenu.Append("<a class='smooth-scroll active' href='/" & sLink & "' title='" & dr.Item("sText") & "'>" & dr.Item("sText") & "</a>")
                    Else
                        sMenu.Append("<a class='active' href='/" & sLink & "' title='" & dr.Item("sText") & "'>" & dr.Item("sText") & "</a>")
                    End If
                Else
                    If sQueryString.Contains("#") Then
                        sMenu.Append("<a class='smooth-scroll' href='/" & sLink & "' title='" & dr.Item("sText") & "'>" & dr.Item("sText") & "</a>")
                    Else
                        sMenu.Append("<a href='/" & sLink & "' title='" & dr.Item("sText") & "'>" & dr.Item("sText") & "</a>")
                    End If
                End If
            End If
            sMenu.Append("</li>")

            count = count + 1
        Next

        Return sMenu.ToString()
    End Function


    Public Function CreateSitemap(sLanguage As String, iPartijID As Long, sMenuSoort As String) As String
        Dim sMenu As New StringBuilder
        dt = sPagesByActief(sLanguage, sMenuSoort, 0, True, iPartijID)

        Dim bMeertalig As Boolean = False
        Dim TAL As New clsTalen
        TAL.dt = TAL.sTalenByPartijID(iPartijID)
        If TAL.dt.Rows.Count > 1 Then
            bMeertalig = True
        Else
            bMeertalig = False
        End If

        sMenu.Append("<ul class='widget-sitemap'>")

        Dim count As Integer = 0
        For Each Me.dr In dt.Rows
            Dim sQueryString As String = dr.Item("sQueryString").ToString().ToLower()
            dt2 = sPagesByActief(sLanguage, sMenuSoort, dr.Item("iPageID"), True, iPartijID)

            Dim sLink As String = ""

            If dt2.Rows.Count > 0 Then
                sMenu.Append("<li>")

                If sQueryString.Split("#")(0) <> "home" Or sLanguage.ToLower <> BP.sDefaultLanguage.ToLower Then
                    If dr.Item("sQueryString") <> "" Then
                        If bMeertalig Then
                            sLink = "/" & sLanguage.ToLower & "/" & sQueryString
                        Else
                            sLink = "/" & sQueryString
                        End If
                    End If
                Else
                    If sQueryString.Contains("#") Then
                        sLink = "/" & sQueryString.Split("#")(1)
                    Else
                        sLink = "/"
                    End If
                End If

                sMenu.Append("<a href='" & sLink & "'>" & dr.Item("sText") & "</a>")

                sMenu.Append("<ul>")
                For Each Me.dr2 In dt2.Rows
                    Dim sQueryStringChild As String = dr2.Item("sQueryString").ToString().ToLower()
                    If bMeertalig Then
                        If sLanguage.ToLower <> BP.sDefaultLanguage.ToLower Then
                            If sQueryStringChild.Contains("#") Then
                                sLink = "/" & sLanguage & "/" & sQueryString & sQueryStringChild
                            Else
                                sLink = "/" & sLanguage & "/" & sQueryString & "/" & sQueryStringChild
                            End If
                        Else
                            If sQueryStringChild.Contains("#") Then
                                sLink = "/" & sQueryStringChild
                            Else
                                sLink = "/" & sLanguage & "/" & sQueryString & "/" & sQueryStringChild
                            End If
                        End If
                    Else
                        If sQueryString.Contains("#") Then
                            sLink = "/" & sQueryString & sQueryStringChild
                        Else
                            sLink = "/" & sQueryString & "/" & sQueryStringChild
                        End If
                    End If

                    sMenu.Append("<li><a href='" & sLink & "' title='" & dr.Item("sText") & "'>" & dr2.Item("sText") & "</a>")
                Next
                sMenu.Append("</ul>")
            Else
                sMenu.Append("<li>")
                If sQueryString.Split("#")(0) <> "home" Or sLanguage.ToLower <> BP.sDefaultLanguage.ToLower Then
                    If bMeertalig Then
                        sLink = sLanguage & "/" & sQueryString
                    Else
                        sLink = sQueryString
                    End If
                Else
                    If sQueryString.Contains("#") Then
                        sLink = "#" & sQueryString.Split("#")(1)
                    End If
                End If

                If sQueryString.Contains("#") Then
                    sMenu.Append("<a href='/" & sLink & "' title='" & dr.Item("sText") & "'>" & dr.Item("sText") & "</a>")
                Else
                    sMenu.Append("<a href='/" & sLink & "' title='" & dr.Item("sText") & "'>" & dr.Item("sText") & "</a>")
                End If
            End If
            sMenu.Append("</li>")

            count = count + 1
        Next

        sMenu.Append("</ul>")

        Return sMenu.ToString()
    End Function
End Class