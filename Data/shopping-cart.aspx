<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shopping-cart.aspx.vb" Inherits="Data_shopping_cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="shoppingcart" class="selector">
            <div class="list">
                <asp:Repeater ID="repWinkelwagen" runat="server">
                    <ItemTemplate>
                        <div class="article">
                            <div class="col-xs-12">
                                <div class="article-top col-xs-12">
                                    <div class="col-xs-12 article-title">
                                        <span class="">
                                            <asp:Literal ID="Literal5" Text='<%# Eval("sArtikelOmschrijving")%>' runat="server" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-xs-12" style="text-align: center;">
                                    <img src='<%# Eval("sSmall").ToString().Replace("~/", BP.sURL()) %>' alt="" class="img-fluid" />
                                </div>
                            </div>
                            <div class="article-bottom col-xs-12">
                                <div class="col-xs-6">
                                    <span class="article-qty  pull-left">QTY &nbsp;&nbsp;&nbsp;
                                                <asp:Literal Text='<%# CInt(Eval("iAantal")) %>' runat="server" /></span>
                                </div>
                                <div class="col-xs-6">
                                    <span class="article-price pull-right">&euro;<asp:Literal ID="Literal1" Text='<%# Eval("iStuksPrijs")%>' runat="server" /></span>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-md-12">
                <hr class="blackHr" />
            </div>
            <div class="amount">
                <div class="item">
                    <div class="col-xs-6">
                        <span class="pull-left">amount</span>
                    </div>
                    <div class="col-xs-6">
                        <span class="price pull-right">&euro;
                                <asp:Literal ID="ltlTotalTop" runat="server" /></span>
                    </div>
                </div>
                <div class="item">
                    <div class="col-xs-6">
                        <span class="pull-left">subtotal</span>
                    </div>
                    <div class="col-xs-6">
                        <span class="price pull-right">&euro;
                                <asp:Literal ID="ltlSubTotal" runat="server" /></span>
                    </div>
                </div>
                <div class="item">
                    <div class="col-xs-6">
                        <span class="pull-left">discount</span>
                    </div>
                    <div class="col-xs-6">
                        <span class="price pull-right">&euro;
                                <asp:Literal ID="ltlDiscount" runat="server" /></span>
                    </div>
                </div>
                <div class="item">
                    <div class="col-xs-6">
                        <span class="pull-left">total</span>
                    </div>
                    <div class="col-xs-6">
                        <span class="price pull-right">&euro;
                                <asp:Literal ID="ltlTotal" runat="server" /></span>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 btn-checkout">
                <asp:HyperLink ID="aCheckout" CssClass="pull-right btn btn-underline" runat="server" Text="Checkout" />
            </div>
    </form>
</body>
</html>
