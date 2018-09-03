
Partial Class ACC_order_history
    Inherits BasePage

    Dim P As New clsPage

    Public sLanguage, sQs, sParentPage As String
    Dim iTaalID As Long

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        onlineCheck()


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

            Dim iPartijID As Long = InitPage()


            InitializeComponents()

            Dim P As New clsPage
            P.dt = P.sPagesByActief(sLanguage, "Account", 0, True, iPartijIDBeheerder)
            repMenu.DataSource = P.dt
            repMenu.DataBind()

        End If
    End Sub


    Private Sub InitializeComponents()
        Dim FK As New clsFacKop
        FK.dt = FK.sFacKopByPartijAndStatus(iPartijID, "betaald", "factuur")
        If FK.dt.Rows.Count > 0 Then
            repOrders.DataSource = FK.dt
            repOrders.DataBind()
        End If


        Dim P As New clsPage

        aEdit.NavigateUrl = P.sPageUrlByGuid(sLanguage, "66a5068b-9513-48c4-b270-0f85938a3407")
        aPassword.NavigateUrl = P.sPageUrlByGuid(sLanguage, "4f390a22-45d3-40da-baf9-539c43e2b5eb")

        Dim ACC As New clsAccounts
        ACC.dt = ACC.sAccountByPartijID(iPartijID)
        If ACC.dt.Rows.Count > 0 Then
            ACC.dr = ACC.dt.Rows(0)
            iPersID = ACC.dr.Item("iPersID")
        End If

        Dim PAR As New clsPartijen
        PAR.dt = PAR.sPartijByPartijID(iPartijID)
        If PAR.dt.Rows.Count > 0 Then
            PAR.dr = PAR.dt.Rows(0)
            'txtCompany.Text = PAR.dr.Item("sNaam")
            'ltlVAT.Text = PAR.dr.Item("sBtwNummer")
        End If

        Dim PERS As New clsPersonen
        PERS.dt = PERS.sPersoonByPersID(iPersID)
        If PERS.dt.Rows.Count > 0 Then
            PERS.dr = PERS.dt.Rows(0)
            ltlName.Text = PERS.dr.Item("sVoorletters") & " " & PERS.dr.Item("sNaam")
            ltlEmail.Text = PERS.dr.Item("sEmail")
        End If
        ltlTitle.Text = ltlName.Text

        Dim ADR As New clsAdressen
        ADR.dt = ADR.sAdresByPartijIDAndType(iPartijID, "factuur")
        If ADR.dt.Rows.Count > 0 Then
            ADR.dr = ADR.dt.Rows(0)
            ltlAddresLine1.Text = ADR.dr.Item("sStraat") & " " & ADR.dr.Item("sHuisNr") & "/" & ADR.dr.Item("sToev")
            ltlAddresLine2.Text = ADR.dr.Item("sPostCode") & " " & ADR.dr.Item("sPlaats")
            ltlLand.Text = ADR.dr.Item("sLand")
            'txtPostcode.Value = ADR.drADR.sPostCode 'ADR.drADR.sWijkCode & " " & ADR.drADR.sStraatCode.ToUpper()
        End If



    End Sub
End Class