Imports System.Data
Imports System.IO

Partial Class pdf_Factuur_
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim LOG As New clsLog
        'LOG.iLog("Factuur", "PageLoad", Request.QueryString("id"))
        Dim iFacKopID As Long = Request.QueryString("id")
        Dim FACKOP As New clsFacKop
        Dim P As New clsPartijen
        Dim U As New clsUtility
        Dim dt As New DataTable
        dt = FACKOP.sByFacKopID(iFacKopID)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow
            dr = dt.Rows(0)
            ltlFactuur.Text = dr.Item("sFactuur")
            ltlBezorgadres.Text = dr.Item("sVerzendGegevens").ToString().Replace(Environment.NewLine, "<br/>") 'FACKOP.drFacKop.sVerzendGegevens.Replace(Environment.NewLine, "<br/>")
            ltlFactuuradres.Text = dr.Item("sPartijGegevens").ToString().Replace(Environment.NewLine, "<br/>") ' FACKOP.drFacKop.sPartijGegevens.Replace(Environment.NewLine, "<br/>")
            ltlDatum.Text = CDate(dr.Item("dDatum")).ToShortDateString ' FACKOP.drFacKop.dDatum.ToShortDateString
            ltlTotal.Text = dr.Item("iFactuurBedrag") ' FACKOP.drFacKop.iFactuurBedrag
            ltlTotaalExcl.Text = dr.Item("iFactuurBedragExcl") ' FACKOP.drFacKop.iFactuurBedragExcl
            ltlVAT.Text = dr.Item("iBtwBedrag")
            Dim FR As New clsFacRgl

            repFacRgls.DataSource = FR.sFacRglsByFacKopID(iFacKopID, "artikel")
            repFacRgls.DataBind()

            FR.sKortingByFacKopID(iFacKopID)
            If FR.sDiscount = "" Then
                pDiscount.Visible = False
                pBedragDiscount.Visible = False
                pEuroDiscount.Visible = False
            Else
                ltlDiscount.Text = FR.sDiscount
            End If
            Dim FI As New clsFacItems
            FI.sFacItems(iFacKopID)
            ltlShipping.Text = FI.sVerzendkosten


            Dim iPartijID As Long = dr.Item("iPartijIDBeheerder")
            Dim sSrc As String = U.LocatieUrlCms(P.sLogo(iPartijID))
            imgLogo.Src = sSrc


        End If
    End Sub
End Class