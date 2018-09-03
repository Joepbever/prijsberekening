
Partial Class _Default
    Inherits BasePage
    Public sLanguage As String
    Dim IMG As New clsImages
    Dim MOLLIE As New clsMollie
    Dim ART As New clsArtikelen
    Dim iTaalID As Long
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        iTaalID = sTaalID(sLanguage)

        If Page.IsPostBack = False Then
            Dim sQs As String = RouteData.Values("page").ToString()
            Dim PI As New clsPageItems
            PI.sPI(Me.Page, sLanguage, sQs, False, False, True)
            cbxPrivacy.Text = Resources.Resource.formTerms & " " & "<a href='/privacy-verklaring' target='_blank'>" & Resources.Resource.formPrivacy & ".</a>"
            ART.dt = ART.sArtikelenByArtikelgroep(iPartijIDBeheerder, "Cadeaubonnen")
            repArtikelen.DataSource = ART.dt
            repArtikelen.DataBind()
        End If
    End Sub

    Protected Sub btnAfronden_Click(sender As Object, e As EventArgs) Handles btnAfronden.Click

        Dim iArtikelID As Long = hidArtikelID.Value.Replace("rb", "")
        Dim iArtikelVariantID As Long = 0
        ART.dt = ART.sArtikelByIDdt(iArtikelID)
        If ART.dt.Rows.Count > 0 Then
            ART.dr = ART.dt.Rows(0)

            Dim AV As New clsArtikelVarianten
            AV.dt = AV.sArtikelVariantenByArtikelID(iArtikelID)
            If AV.dt.Rows.Count > 0 Then
                AV.dr = AV.dt.Rows(0)
                iArtikelVariantID = AV.dr.Item("iArtikelVariantID")
            End If
        End If

        Dim sPrijs As String = ART.dr.Item("iPrijs").ToString()

        Dim sSessieID As String = ""
        If Request.Cookies("SessieID") IsNot Nothing Then
            'Cookie bestaat return SessieID
            sSessieID = Request.Cookies("SessieID").Value
        Else
            'Cookie bestaat niet, maak aan
            Dim guid As Guid = Guid.NewGuid
            sSessieID = guid.ToString()
            Response.Cookies("SessieID").Value = sSessieID
            Response.Cookies("SessieID").Expires = Date.Now.AddDays(1)
            Response.Cookies("SessieID").Path = "/"
        End If

        Dim FR As New clsFacRgl
        FR.iArtikelInFacRgl(iArtikelID, iArtikelVariantID, sSessieID, 1, iPartijIDBeheerder, txtTekst.Text, iTaalID)

        Dim P As New clsPage
        Response.Redirect(P.sPageUrlByGuid(sLanguage, "deee4d8c-ff21-4bb9-b11d-4d1c0b43c57c"))
    End Sub

    Dim iCount As Long = 0
    Protected Sub repArtikelen_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repArtikelen.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rb As HtmlInputRadioButton = e.Item.FindControl("rb")
            rb.ID = "rb" & DataBinder.Eval(e.Item.DataItem, "iArtikelID")
            If iCount = 0 Then
                hidArtikelID.Value = DataBinder.Eval(e.Item.DataItem, "iArtikelID") 'standaard waarde 
                rb.Checked = True
            End If
            iCount = iCount + 1
        End If
    End Sub

End Class