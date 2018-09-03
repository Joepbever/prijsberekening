Imports System.Net
Imports System.Security.Cryptography

Public Class clsOmniKassa


    'Public PostUrl As String = "https://payment-webinit.omnikassa.rabobank.nl/paymentServlet"
    'Public SecurityKey As String = "Q3jLfLbCcVflREZv5ZUMmOI71a5cgouQxPWSw6oy4EM"
    'Public MerchantId As String = "220120723510002"
    'Public SecurityKeyVersion As String = "2"
     'Test
    Public PostUrl As String = System.Configuration.ConfigurationManager.AppSettings.Item("OmniPostUrl").ToString
    Public SecurityKey As String = System.Configuration.ConfigurationManager.AppSettings.Item("OmniSecurityKey").ToString
    Public MerchantId As String = System.Configuration.ConfigurationManager.AppSettings.Item("OmniMerchantID").ToString
    Public SecurityKeyVersion As String = System.Configuration.ConfigurationManager.AppSettings.Item("OmniSecurityKeyVersion").ToString
    Public CurrencyCode As String = "978" ' Euro
    Public LanguageCode As String = "nl" ' Euro 


    'geeft transactionreference terug
    Public Sub PostNaarBetaalpagina(ByVal sOrdernummer As String, ByVal sReferentie As String, ByVal sReturnUrl As String, ByVal sResponseUrl As String, ByVal sAmount As String, ByVal pPage As Page, Optional ByVal PaymentMeanBrandList As String = "")

        Try
            Dim sData As String = "merchantId=" & MerchantId
            sData &= "|orderId=" & sOrdernummer
            sData &= "|amount=" & sAmount
            sData &= "|customerLanguage=" & LanguageCode
            sData &= "|keyVersion=" & SecurityKeyVersion
            sData &= "|currencyCode=" & CurrencyCode

            'Als er geen betaalmethode van te voren wordt aangegeven worden de methoden 
            'op de betaalpagina weergegeven en kan daar een keuze gemaakt worden
            If PaymentMeanBrandList <> "" Then
                sData &= "|PaymentMeanBrandList=" & PaymentMeanBrandList
            End If

            sData &= "|normalReturnUrl=" & sReturnUrl
            sData &= "|automaticResponseUrl=" & sResponseUrl
            sData &= "|transactionReference=" & sReferentie

            Dim sha256 As SHA256 = sha256.Create()
            Dim hashValue As Byte() = sha256.ComputeHash(New UTF8Encoding().GetBytes(sData + SecurityKey))


            Dim postData As New NameValueCollection()
            postData.Add("Data", sData)
            postData.Add("Seal", ByteArrayToHexString(hashValue))
            postData.Add("InterfaceVersion", "HP_1.0")

            Dim response As Byte()
            Using client As New WebClient()
                response = client.UploadValues(PostUrl, postData)
            End Using
            pPage.Master.FindControl("form1").Visible = False
            pPage.Response.Write(Encoding.UTF8.GetString(response))

        Catch ex As Exception
        End Try


    End Sub

    Public Function VerifierBetaling(ByVal sData As String, ByVal sSeal As String) As String()


        Dim sDataArray() As String

        Dim sha256 As SHA256 = sha256.Create()
        Dim hashValue As Byte() = sha256.ComputeHash(New UTF8Encoding().GetBytes(sData + SecurityKey))

        If sSeal.ToLower() = ByteArrayToHexString(hashValue).ToLower() Then
            sDataArray = sData.Split("|")
        Else
            Return Nothing
        End If

        Return sDataArray

    End Function

    Public Function GetTransactionStatus(ByVal sTransactionCode As String) As String

        Select Case sTransactionCode
            Case "00"
                Return "SUCCESS"
            Case "60"
                Return "PENDING"
            Case "90"
                Return "PENDING"
            Case "EXPIRED"
                Return "EXPIRED"
            Case "17"
                Return "CANCELLED"
            Case Else
                Return "FAILED"
        End Select

    End Function

    Private Function ByteArrayToHexString(ByVal bytes As Byte()) As String

        Dim result As New StringBuilder(bytes.Length * 2)
        Const hexAlphabet As String = "0123456789ABCDEF"

        For Each b As Byte In bytes
            result.Append(hexAlphabet(b >> 4))
            result.Append(hexAlphabet(b And &HF))
        Next

        Return result.ToString()
    End Function
End Class
