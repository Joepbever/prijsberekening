Imports System.Web.Routing

Public Class RouteConfig
    Public Shared Sub RegisterRoutes(routes As RouteCollection)
        Dim BP As New BasePage
        Dim P As New clsPage

        Dim sName As String = ""
        Dim sFile As String = ""
        Dim sRouteUrl As String = ""

        Dim IndexConstraints As New RouteValueDictionary() From {
                {"language", BP.sDefaultLanguage.ToLower},
                {"page", "home"}
            }
        routes.MapPageRoute("index", "", "~/default.aspx", False, IndexConstraints)

        'Voor meertalen controle
        Dim TAL As New clsTalen
        TAL.dt = TAL.sTalenByPartijID(BP.iPartijIDBeheerder)
        Dim bMultilanguage As Boolean = False
        If TAL.dt.Rows.Count > 1 Then
            bMultilanguage = True
        End If


        'Voor extra pagina's buiten de default pagina om
        P.dt = P.sChildPagesByPartijID(BP.iPartijIDBeheerder)
        For Each P.dr In P.dt.Rows
            Try
                sFile = "~/" & P.dr.Item("sURL").ToLower()
                sName = Trim(P.dr.Item("sCode") & P.dr.Item("sQueryString")).ToString().ToLower()
                If bMultilanguage Then
                    'meertalig
                    sRouteUrl = Trim(P.dr.Item("sCode") & "/" & P.dr.Item("sQueryString")).ToString().ToLower()
                Else
                    '1 taal
                    sRouteUrl = Trim(P.dr.Item("sQueryString")).ToString().ToLower()
                End If
                Dim PageConstraints As New RouteValueDictionary() From {
                    {"language", Trim(P.dr.Item("sCode")).ToLower()},
                    {"page", Trim(P.dr.Item("sQueryString")).ToLower()}
                }
                Dim PageDatatokens As New RouteValueDictionary() From {
                    {"sText", P.dr.Item("sText")},
                    {"sGuid", P.dr.Item("sGuid")}
                }
                routes.MapPageRoute(sName, sRouteUrl, sFile, False, PageConstraints, Nothing, PageDatatokens)
            Catch ex As Exception

            End Try
        Next

        'Voor dropdown pagina's
        P.dt = P.sParentPages(BP.iPartijIDBeheerder)
        For Each P.dr In P.dt.Rows
            Try
                sFile = "~/" & P.dr.Item("sURL").ToLower()
                sName = Trim(P.dr.Item("sCode") & P.dr.Item("sParentPage") & P.dr.Item("sQueryString")).ToString().ToLower()
                If bMultilanguage Then
                    'meertalig
                    sRouteUrl = Trim(P.dr.Item("sCode") & "/" & P.dr.Item("sParentPage") & "/" & P.dr.Item("sQueryString")).ToString().ToLower()
                Else
                    '1 taal
                    sRouteUrl = Trim(P.dr.Item("sParentPage") & "/" & P.dr.Item("sQueryString")).ToString().ToLower()
                End If

                Dim PageConstraints As New RouteValueDictionary() From {
                    {"language", Trim(P.dr.Item("sCode")).ToLower()},
                    {"parentpage", Trim(P.dr.Item("sParentPage")).ToLower()},
                    {"page", Trim(P.dr.Item("sQueryString")).ToLower()}
                }
                Dim PageDatatokens As New RouteValueDictionary() From {
                    {"sText", P.dr.Item("sText")},
                    {"sGuid", P.dr.Item("sGuid")}
                }
                routes.MapPageRoute(sName, sRouteUrl, sFile, False, PageConstraints, Nothing, PageDatatokens)
            Catch ex As Exception

            End Try
        Next


        Dim LI As New clsLijstItems
        Dim sLanguage As String = ""
        Dim sQueryString As String = ""

        Dim iIndex As Integer = 0

        ' BEGIN ROUTES VOOR NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA 
        For Each TAL.dr In TAL.dt.Rows
            sLanguage = TAL.dr.Item("sCode").ToString.ToLower
            P.sPageUrlByGuid(sLanguage, "f541e5cf-bb33-450d-9e9c-44eac5d2bc9c") 'GUID AANPASSEN'
            If P.sPageQs.ToLower = "home" Then
                sQueryString = ""
            Else
                sQueryString = P.sPageQs
            End If
            LI.dt = LI.sLIByTaalIDAndType("Nieuws", TAL.dr.Item("iTaalID"))
            For Each LI.dr In LI.dt.Rows
                iIndex = iIndex + 1
                Dim PageConstraints As New RouteValueDictionary() From {
                    {"language", sLanguage},
                    {"page", sQueryString},
                    {"safbeelding", LI.dr.Item("sAfbeelding")}
                }

                Dim PageDatatokens As New RouteValueDictionary() From {
                    {"iLijstItemID", LI.dr.Item("iLijstItemID")}
                }

                If bMultilanguage Then
                    If sQueryString = "" Then
                        sRouteUrl = sLanguage & "/"
                    Else
                        sRouteUrl = Trim(sLanguage & "/" & sQueryString).ToString().ToLower() & "/"
                    End If
                Else
                    If sQueryString = "" Then
                        sRouteUrl = ""
                    Else
                        sRouteUrl = Trim(sQueryString).ToString().ToLower() & "/"
                    End If
                End If

                sName = Trim(sLanguage & sQueryString) & iIndex
                routes.MapPageRoute(sName, sRouteUrl & LI.dr.Item("sAfbeelding"), "~/nieuws-detail.aspx", False, PageConstraints, Nothing, PageDatatokens)
            Next
        Next

        ' BEGIN ROUTES VOOR NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA NIEUWS DETAIL PAGINA 
        For Each TAL.dr In TAL.dt.Rows
            sLanguage = TAL.dr.Item("sCode").ToString.ToLower
            P.sPageUrlByGuid(sLanguage, "6d502c45-e087-4643-a848-a47d2238b703") 'GUID AANPASSEN'
            If P.sPageQs.ToLower = "home" Then
                sQueryString = ""
            Else
                sQueryString = P.sPageQs
            End If
            LI.dt = LI.sLIByTaalIDAndType("Blog", TAL.dr.Item("iTaalID"))
            For Each LI.dr In LI.dt.Rows
                iIndex = iIndex + 1
                Dim PageConstraints As New RouteValueDictionary() From {
                    {"language", sLanguage},
                    {"page", sQueryString},
                    {"safbeelding", LI.dr.Item("sAfbeelding")}
                }

                Dim PageDatatokens As New RouteValueDictionary() From {
                    {"iLijstItemID", LI.dr.Item("iLijstItemID")}
                }

                If bMultilanguage Then
                    If sQueryString = "" Then
                        sRouteUrl = sLanguage & "/"
                    Else
                        sRouteUrl = Trim(sLanguage & "/" & sQueryString).ToString().ToLower() & "/"
                    End If
                Else
                    If sQueryString = "" Then
                        sRouteUrl = ""
                    Else
                        sRouteUrl = Trim(sQueryString).ToString().ToLower() & "/"
                    End If
                End If

                sName = Trim(sLanguage & sQueryString) & iIndex
                routes.MapPageRoute(sName, sRouteUrl & LI.dr.Item("sAfbeelding"), "~/lijstitem-blog.aspx", False, PageConstraints, Nothing, PageDatatokens)
            Next
        Next

        For Each TAL.dr In TAL.dt.Rows
            sLanguage = TAL.dr.Item("sCode").ToString.ToLower
            P.sPageUrlByGuid(sLanguage, "7fbb265e-1980-461d-90f9-1cb4a351818f") 'GUID AANPASSEN'
            If P.sPageQs.ToLower = "home" Then
                sQueryString = ""
            Else
                sQueryString = P.sPageQs
            End If
            LI.dt = LI.sLIByTaalIDAndType("Blog category", TAL.dr.Item("iTaalID"))
            For Each LI.dr In LI.dt.Rows
                iIndex = iIndex + 1
                Dim PageConstraints As New RouteValueDictionary() From {
                    {"language", sLanguage},
                    {"page", sQueryString},
                    {"safbeelding", LI.dr.Item("sAfbeelding")}
                }

                Dim PageDatatokens As New RouteValueDictionary() From {
                    {"iLijstItemID", LI.dr.Item("iLijstItemID")}
                }

                If bMultilanguage Then
                    If sQueryString = "" Then
                        sRouteUrl = sLanguage & "/"
                    Else
                        sRouteUrl = Trim(sLanguage & "/" & sQueryString).ToString().ToLower() & "/"
                    End If
                Else
                    If sQueryString = "" Then
                        sRouteUrl = ""
                    Else
                        sRouteUrl = Trim(sQueryString).ToString().ToLower() & "/"
                    End If
                End If

                sName = Trim(sLanguage & sQueryString) & iIndex
                routes.MapPageRoute(sName, sRouteUrl & LI.dr.Item("sAfbeelding"), "~/lijstitems.aspx", False, PageConstraints, Nothing, PageDatatokens)
            Next
        Next

        ' BEGIN ROUTES VOOR PROJECT DETAIL PAGINA PROJECT DETAIL PAGINA PROJECT DETAIL PAGINA PROJECT DETAIL PAGINA PROJECT DETAIL PAGINA
        For Each TAL.dr In TAL.dt.Rows
            sLanguage = TAL.dr.Item("sCode").ToString.ToLower
            P.sPageUrlByGuid(sLanguage, "fafdc57f-2c03-4587-af2f-1109294100bc") 'GUID AANPASSEN'
            If P.sPageQs.ToLower = "home" Then
                sQueryString = ""
            Else
                sQueryString = P.sPageQs
            End If
            LI.dt = LI.sLIByTaalIDAndType("Projecten", TAL.dr.Item("iTaalID"))
            For Each LI.dr In LI.dt.Rows
                iIndex = iIndex + 1
                Dim PageConstraints As New RouteValueDictionary() From {
                    {"language", sLanguage},
                    {"page", sQueryString},
                    {"safbeelding", LI.dr.Item("sAfbeelding")}
                }

                Dim PageDatatokens As New RouteValueDictionary() From {
                    {"iLijstItemID", LI.dr.Item("iLijstItemID")}
                }

                If bMultilanguage Then
                    If sQueryString = "" Then
                        sRouteUrl = sLanguage & "/"
                    Else
                        sRouteUrl = Trim(sLanguage & "/" & sQueryString).ToString().ToLower() & "/"
                    End If
                Else
                    If sQueryString = "" Then
                        sRouteUrl = ""
                    Else
                        sRouteUrl = Trim(sQueryString).ToString().ToLower() & "/"
                    End If
                End If

                sName = Trim(sLanguage & sQueryString) & iIndex
                routes.MapPageRoute(sName, sRouteUrl & LI.dr.Item("sAfbeelding"), "~/projecten-detail.aspx", False, PageConstraints, Nothing, PageDatatokens)
            Next
        Next

        Dim ROUTE As New clsRoutes
        iIndex = 0
        For Each TAL.dr In TAL.dt.Rows
            sLanguage = TAL.dr.Item("sCode").ToString.ToLower

            ROUTE.dt = ROUTE.sRoutesByTaalID(BP.iPartijIDBeheerder, "artikel", TAL.dr.Item("iTaalID"))
            For Each ROUTE.dr In ROUTE.dt.Rows
                Dim sQs As String = BP.FriendlyUrl(ROUTE.dr.Item("sTitle"))
                sName = sQs
                iIndex = iIndex + 1
                Dim Constraints As New RouteValueDictionary() From {
                                    {"language", sLanguage},
                                    {"artikel", sQs}
                            }
                Dim Datatokens As New RouteValueDictionary() From {
                                    {"artikelid", ROUTE.dr.Item("iID")}
                            }
                Dim sRoute As String = ROUTE.dr.Item("sURL").ToString.Replace("~/", "")
                routes.MapPageRoute(sName & "_" & iIndex, sRoute, "~/Product.aspx", False, Constraints, Nothing, Datatokens)
            Next

            ROUTE.dt = ROUTE.sRoutesByTaalID(BP.iPartijIDBeheerder, "categorie", TAL.dr.Item("iTaalID"))
            For Each ROUTE.dr In ROUTE.dt.Rows
                Dim sTitle As String = ROUTE.dr.Item("sTitle")
                Dim sQs As String = BP.FriendlyUrl(sTitle)
                sName = sQs
                iIndex = iIndex + 1
                Dim Constraints As New RouteValueDictionary() From {
                    {"language", sLanguage},
                    {"page", P.sPageQs}
                }
                Dim Datatokens As New RouteValueDictionary() From {
                    {"sText", sTitle},
                    {"categorieid", ROUTE.dr.Item("iID")}
                }
                Dim sRoute As String = ROUTE.dr.Item("sURL").ToString.Replace("~/", "")
                routes.MapPageRoute(sName & "_" & iIndex, sRoute, "~/category.aspx", False, Constraints, Nothing, Datatokens)
            Next


            Dim sBevestigingPage As String = P.sPageUrlByGuid(sLanguage, "f2257328-0d38-40dd-9dd0-176c79dd0512")
            ROUTE.dt = ROUTE.sRoutesByTaalID(BP.iPartijIDBeheerder, "idealbevestiging", TAL.dr.Item("iTaalID"))
            For Each ROUTE.dr In ROUTE.dt.Rows
                Dim sTitle As String = ROUTE.dr.Item("sTitle")
                Dim sQs As String = BP.FriendlyUrl(sTitle)
                sName = sQs
                iIndex = iIndex + 1

                Dim sRoutessURL() As String = ROUTE.dr.Item("sURL").ToString.Replace("~/", "").Split("/")
                Dim sTimestamp = sRoutessURL(1)
                Dim iPartijID = sRoutessURL(2)
                Dim iPersID = sRoutessURL(3)

                Dim Constraints As New RouteValueDictionary() From {
                    {"language", sLanguage},
                    {"page", P.sPageQs},
                    {"ifackopid", ROUTE.dr.Item("iID")},
                    {"timestamp", sTimestamp},
                    {"ipartijid", iPartijID},
                    {"ipersid", iPersID}
                }
                Dim Datatokens As New RouteValueDictionary() From {
                    {"sText", sTitle},
                    {"categorieid", ROUTE.dr.Item("iID")}
                }
                Dim sRoute As String = ROUTE.dr.Item("sURL").ToString.Replace("~/", "")
                routes.MapPageRoute(sName & "_" & iIndex, sRoute, "~/Bevestiging.aspx", False, Constraints, Nothing, Datatokens)
            Next
        Next

        'Dim mollieredirect As New RouteValueDictionary() From {
        '    {"page", "[a-z|0-9|-]*"},
        '    {"id", "[a-z|0-9|-]*"},
        '    {"timestamp", "[a-z|0-9|-]*"},
        '    {"ipartijid", "[a-z|0-9|-]*"},
        '    {"ipersid", "[a-z|0-9|-]*"}
        '}

        'Dim MollieDatatokens As New RouteValueDictionary() From {
        '    {"sText", "Bedankt voor uw bestelling"},
        '    {"sGuid", "58a8219f-0c5a-4b44-a89f-02231ecf769b"}
        '}

        'routes.MapPageRoute("mollieredirect", "{page}/{id}/{timestamp}/{ipartijid}/{ipersid}", "~/Bevestiging.aspx", False, mollieredirect, Nothing, MollieDatatokens)

    End Sub
End Class