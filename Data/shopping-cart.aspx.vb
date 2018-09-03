
Imports System.Web.Services



Partial Class Data_shopping_cart
    Inherits System.Web.UI.Page

    Protected BP As New BasePage
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        Dim sLanguage As String = Request.QueryString("lang")
        Dim P As New clsPage
        'aAfrekenen.HRef = P.sPageUrlByGuid(sLanguage, "deee4d8c-ff21-4bb9-b11d-4d1c0b43c57c")
        'aCart.HRef = P.sPageUrlByGuid(sLanguage, "3659abe8-3b74-41a4-9889-c2d8638ba811")

        Dim FR As New clsFacRgl
        Dim U As New clsUtility
        FR.dt = FR.sFacRgls_SessionAndImg(sSessieID, BP.iPartijIDBeheerder, "artikel")
        If FR.dt.Rows.Count > 0 Then
            Dim iCountProducts As Integer = 0
            For Each FR.dr In FR.dt.Rows
                iCountProducts = iCountProducts + FR.dr.Item("iAantal")
            Next
            'ltlCountProducts.Text = iCountProducts
            'ltlAantalArtikelenMobiel.Text = iCountProducts
            repWinkelwagen.DataSource = FR.dt
            repWinkelwagen.DataBind()
        End If
        FR.sInfo(sSessieID, BP.iPartijIDBeheerder)
        If FR.iAantalRegels > 0 Then
            ltlTotalTop.Text = FR.sBedragMinusKorting
            ltlSubTotal.Text = FR.sBedrag
            ltlTotal.Text = FR.sBedragMinusKorting
            ltlDiscount.Text = FR.sDiscount
        Else
            ltlTotalTop.Text = "0,00"
            ltlSubTotal.Text = "0,00"
            ltlTotal.Text = "0,00"
            ltlDiscount.Text = "0,00"
        End If
    End Sub

    <WebMethod>
    Public Shared Function iFacRgl(iArtikelID As Long, iArtikelVariantID As Long, iAantal As Long, iTaalID As Long) As Object
        Dim R As New oResponse
        R.type = "winkelwagen"

        Dim sSession As String = HttpContext.Current.Request.Cookies("SessieID").Value
        If iAantal < 1 Then
            R.code = 0
            R.msg = "Required field quantity."
            Return R
        End If

        If IsNumeric(iAantal) = False Then
            R.code = 0
            R.msg = "Required field quantity."
            Return R
        End If

        Dim ST As New clsSettings
        Dim BP As New BasePage
        ST.InitWebshopSettings(BP.iPartijIDBeheerder)
        If ST.bControleArtikelVariantID Then
            If CInt(iArtikelVariantID) < 1 Then
                R.code = 0
                R.msg = "Required field size."
                Return R
            End If
        End If

        Dim FR As New clsFacRgl
        Dim AV As New clsArtikelVarianten



        If ST.bControleArtikelVariantID Then
            AV.dt = AV.sArtikelVariantByID(iArtikelVariantID)
            If AV.dt.Rows.Count > 0 Then
                AV.dr = AV.dt.Rows(0)
                FR.iArtikelInFacRgl(iArtikelID, iArtikelVariantID, sSession, iAantal, BP.iPartijIDBeheerder, AV.dr.Item("sWaarde"), iTaalID)
                R.maat = AV.dr.Item("sWaarde")
                R.code = 1
                R.msg = "Product added to shopping cart."
            Else
                R.code = 0
                R.msg = "Product not found."
            End If
        Else
            FR.iArtikelInFacRgl(iArtikelID, iArtikelVariantID, sSession, iAantal, BP.iPartijIDBeheerder, "", iTaalID)
            R.code = 1
            R.msg = "Product added to shopping cart."
        End If

        Dim iAantalArtikelen As String = CInt(FR.scAantalArtikelen_Session(sSession, BP.iPartijIDBeheerder, "artikel"))
        If CInt(iAantal) > 0 Then
            R.aantal = iAantalArtikelen
        Else
            R.aantal = "0"
        End If

        Return R
    End Function

End Class
