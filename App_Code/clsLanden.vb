Imports System.Data.SqlClient
Imports System.Data
Imports System.Globalization

Public Class clsLanden

    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sLandByID(iLandID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Landen WHERE (iLandID = @iLandID)"
        cmd.Parameters.AddWithValue("@iLandID", iLandID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function scLandCodeByID(iLandID As Long) As String
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT sLandCode FROM Landen WHERE (iLandID = @iLandID)"
        cmd.Parameters.AddWithValue("@iLandID", iLandID)
        Return CON.Scalar(cmd)
    End Function

    Public Function sLanden(sCode As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT tussTaalLanden.sLandNaam, tussTaalLanden.iLandID, Landen.sData, Landen.dVerzendkosten, Landen.dVerzendkostenGratis, Landen.iPartijID, Landen.iLandID, Landen.sLandCode FROM Landen INNER JOIN tussTaalLanden ON Landen.iLandID =  tussTaalLanden.iLandID WHERE (tussTaalLanden.sCode = @sCode) AND (Landen.iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sCode", sCode)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public bGratisVerzenden As Boolean = False
    Public bGebruikVerzendKostenLand As Boolean = False
    Public Function BepaalVerzendkosten(iLandID As Long) As Decimal
        Dim BP As New BasePage
        Dim FR As New clsFacRgl
        Dim rVerzendkosten, rGratisVerzenden, rTotaalBedrag As Decimal

        dt = sLandByID(iLandID)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            Dim sSessieID As String = HttpContext.Current.Request.Cookies("SessieID").Value
            FR.dt = FR.sFacRglsByTypeAndSession("artikel", sSessieID, BP.iPartijIDBeheerder) 'check of er regels zijn
            If FR.dt.Rows.Count > 0 Then
                rTotaalBedrag = FR.scTotaalBedrag_Session(sSessieID, BP.iPartijIDBeheerder, "artikel")
                rGratisVerzenden = dr.Item("dVerzendkostenGratis")
                If rTotaalBedrag > rGratisVerzenden Then
                    bGratisVerzenden = True
                    rVerzendkosten = "0,00"
                Else
                    rVerzendkosten = dr.Item("dVerzendkosten")
                    If dr.Item("sLandCode").ToString.ToUpper = BP.sDefaultLanguage Then 'om te bepalen of we verzendkosten van het land moeten hanteren
                        bGebruikVerzendKostenLand = False
                    Else
                        bGebruikVerzendKostenLand = True
                    End If
                End If
            Else
                rVerzendkosten = "0,00"
            End If

        End If

        'ltlVerzendkosten.Text = rVerzendkosten.ToString("N", nfi)
        'Dim nfi As NumberFormatInfo = New CultureInfo("nl-NL", False).NumberFormat
        'nfi.NumberDecimalDigits = 2
        Return rVerzendkosten ' .ToString("N", nfi)
        'Return rVerzendkosten
    End Function
End Class
