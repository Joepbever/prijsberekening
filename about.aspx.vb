Partial Class _About
    Inherits BasePage

    Dim IMG As New clsImages
    Dim LI As New clsLijstItems
    Dim P As New clsPage
    Dim U As New clsUtility

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            sQs = RouteData.Values("page").ToString()

            If RouteData.Values("parentpage") IsNot Nothing Then
                sParentPage = RouteData.Values("parentpage")
            End If

            Dim PI As New clsPageItems
            PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)

        End If

        'If RouteData.Values("language") IsNot Nothing Then
        '    sLanguage = RouteData.Values("language").ToString().ToUpper
        'Else
        '    sLanguage = sDefaultLanguage.ToUpper
        'End If

        'iTaalID = sTaalID(sLanguage)

        ''Master.FindControl("sectionBreadcrumps").Visible = False

        'If Page.IsPostBack = False Then
        '    If IsOnline() Then
        '        'Response.Redirect(P.sPageUrlByGuid(sLanguage, "5b594390-434b-4688-aa51-725bffe1d2d0"))
        '    End If

        '    sQs = RouteData.Values("page").ToString()

        '    If RouteData.Values("parentpage") IsNot Nothing Then
        '        sParentPage = RouteData.Values("parentpage")
        '    End If

        '    Dim PI As New clsPageItems
        '    PI.sPI(Page, sLanguage, sQs, False, False, True, sParentPage)
        'End If
    End Sub

End Class