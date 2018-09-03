Imports System.Data
Imports System.IO

Partial Class pdf_Factuur_
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iFacKopID As Long = Request.QueryString("id")
        ltlID.Text = iFacKopID.ToString()

        Dim FACKOP As New clsFacKop
        Dim dt As New DataTable
        dt = FACKOP.sByFacKopID(iFacKopID)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim iPartijID As Long = dr.Item("iPartijIDBeheerder")
            Dim val As String
            Dim ST As New clsSettings
            Dim U As New clsUtility

            Dim PT As New clsPartijen
            PT.dt = PT.sPartijBeheerder(iPartijID)
            If PT.dt.Rows.Count > 0 Then
                PT.dr = PT.dt.Rows(0)
                Dim ADR As New clsAdressen
                ADR.dt = ADR.sAdresAndLandByPartijIDAndType(iPartijID, "bezoek", "EN")
                val = ST.scSettingByTypeAndGroup("Text", "Factuurinformatie", iPartijID)
                If ADR.dt.Rows.Count > 0 Then
                    ADR.dr = ADR.dt.Rows(0)
                    ltlID.Text = U.ReplaceVariables(val, Nothing, PT.dr, dr, Nothing, Nothing, Nothing, ADR.dt)
                Else
                    ltlID.Text = U.ReplaceVariables(val, Nothing, PT.dr, dr, Nothing, Nothing, Nothing, Nothing)
                End If
            End If
        End If

    End Sub
End Class
