Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Data
Imports MultiSafepay
Imports MultiSafepay.Model

Public Class clsMultisafepay
    Inherits System.Web.UI.Page

    'Documentation (.NET): https://www.multisafepay.com/documentation/doc/API-Reference/ and https://www.multisafepay.com/documentation/doc/Step-by-Step/.

    Public Function client() As MultiSafepayClient

        ' Creating new client (key, url and languagecode)

        Dim multi As New MultiSafepayClient(
        "a49ae3421bda5a44ea7c0a757bbc92f6d3f50d29",
        "https://api.multisafepay.com/v1/json/",
        "639-1"
        )

        Return multi

    End Function

    Public Function testClient() As MultiSafepayClient

        Dim multi As New MultiSafepayClient(
        "bdecf97b4bf788091d0be88167319051c4a2a768",
        "https://testapi.multisafepay.com/v1/json/",
        "639-1"
        )

        Return multi

    End Function

    ''' <summary>
    '''     Explanation of how to create a payment
    ''' </summary>
    ''' <param name="sOrderId">The unique identifier from your system for the order.</param>
    ''' <param name="sPrijs">The amount (in cents) that the customer needs to pay.</param>
    ''' <param name="sDescription">A free text description which will be shown with the order in MultiSafepay Control. If the customers bank supports it this description will also be shown on the customers bank statement.</param>
    ''' <returns>Returns paymentlink to pay the order.</returns>

    Public Function createPayment(sOrderId As Long, sPrijs As Int64, sDescription As String) As String

        ' Getting all gateways (countrycode, currencycode and amount{cents})

        client().GetGateways(
        "3166-2:NL",
        "EUR",
        sPrijs
        )

        ' Creating order (id, description, amount{cents}, currencycode and paymentoptions)
        ' Payment urls (notificationurl, succesredirecturl and cancelledredirecturl)

        Dim pl As PaymentLink = client().CreateOrder(
            OrderRequest.CreateRedirect(
                sOrderId, sDescription, sPrijs, "EUR",
                New PaymentOptions(
                    "https://tcc-spares.com/MultiSafePayResponse.aspx",
                    "https://websentiment.nl/",
                    "https://www.google.nl/"
                )
            )
        )
        Return pl.PaymentUrl

    End Function

    Public Function createTestPayment(sOrderId As String, sPrijs As Int64, sDescription As String) As String

        testClient().GetGateways(
        "3166-2:NL",
        "EUR",
        sPrijs
        )

        Dim pl As PaymentLink = testClient().CreateOrder(
            OrderRequest.CreateRedirect(
                sOrderId, sDescription, sPrijs, "EUR",
                New PaymentOptions(
                    "https://tcc-spares.com/MultiSafePayResponse.aspx",
                    "http://localhost:51705/MultiSafePayResponse.aspx",
                    "http://localhost:51705/MultiSafePayResponse.aspx"
                )
            )
        )
        Return pl.PaymentUrl

    End Function

    Public Function getTestOrderStatus() As String

        ' Checks the order status of an order. It requires the orderID to check it

        Dim multiSafepayClient As MultiSafepayClient = testClient()
        Dim context = HttpContext.Current.Request.QueryString("transactionid")

        If context IsNot Nothing Then
            Dim order As Object = multiSafepayClient.GetOrder(context)
            Select Case order.Status
                Case "initialized"
                    Return order.Status
                Case "completed"
                    Return order.Status
                Case "uncleared"
                    Return order.Status
                Case "void"
                    Return order.Status
                Case "declined"
                    Return order.Status
                Case "refunded"
                    Return order.Status
                Case "expired"
                    Return order.Status
                Case Else
                    Return ""
            End Select
        End If

        Return ""

    End Function

    Public Function getOrderStatus() As String

        Dim multiSafepayClient As MultiSafepayClient = client()
        Dim context = HttpContext.Current.Request.QueryString("transactionid")

        If context IsNot Nothing Then
            Dim order As Object = multiSafepayClient.GetOrder(context)
            Select Case order.Status
                Case "initialized"
                    Return order.Status
                Case "completed"
                    Return order.Status
                Case "uncleared"
                    Return order.Status
                Case "void"
                    Return order.Status
                Case "declined"
                    Return order.Status
                Case "refunded"
                    Return order.Status
                Case "expired"
                    Return order.Status
                Case Else
                    Return ""
            End Select
        End If

        Return ""

    End Function

End Class