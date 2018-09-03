Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports MultiSafepay.Model
Imports MultiSafepay
Imports System.Web

Partial Class MultiSafePayResponse
    Inherits System.Web.UI.Page

    Dim m As New clsMultisafepay

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        m.getTestOrderStatus()
    End Sub

    Protected Sub createOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles createOrder.Click
        ' If you want to create an order you need to response redirect the order with payment

        Response.Redirect(m.createTestPayment("BLABLABLABALBALBALBALABLAAaaaaA", 51, "test"))
    End Sub

End Class
