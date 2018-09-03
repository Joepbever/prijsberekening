<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="winkelwagen.aspx.vb" Inherits="Winkelwagen" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="big-container cart-ww">
        <div class="center-height IfLarger">
            <div class="">
                <div class="col-md-6 col-sm-12">

                    <div class=" empty" runat="server" id="divLeeg">
                        <h6 id="spInfo" runat="server" class="spanInfo">
                            <asp:Literal ID="ltlInfoLeeg" runat="server" />
                        </h6>
                    </div>

                    <span id="Span1" runat="server" style="display: none;">
                        <asp:Literal ID="ltlInfo" Text="Your shopping cart is empty." runat="server" />
                    </span>

                    <div class="row">
                        <asp:Repeater ID="repWinkelwagen" runat="server">
                            <ItemTemplate>
                                <div class="col-xs-12 noPadd-all shoppingItem divShopItem">
                                    <div class="row padding-mob d-flex align-items-center">
                                        <div class="col-4 shoppingImg paddingless">
                                            <a runat="server" id="aLink">
                                                <img class="img-fluid" runat="server" id="imgThumb" src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>' />
                                            </a>
                                        </div>

                                        <div class="col-md-6 col-xs-8 paddingless">
                                            <div class="row">
                                                <div class="col-xs-12 paddingless">

                                                    <a data-id='<%# Eval("iFacRglID")%>' class="remove aRemoveItem"><i class="fas fa-times"></i></a>

                                                    <a class="wwProdLink" runat="server" id="aLink2">
                                                        <span class="ww-title">
                                                            <asp:Literal ID="Literal2" Text='<%# Eval("sArtikel")%>' runat="server" />
                                                        </span>
                                                    </a>

                                                    <span class="ww-size">
                                                        <asp:Literal ID="Literal6" Text='<%# Eval("sArtikelOmschrijving")%>' runat="server" />
                                                    </span>

                                                    <span class="code">
                                                        <asp:Literal ID="Literal1" Text='<%# "Size: " & Eval("sOmschrijving")%>' runat="server" />
                                                    </span>
                                                </div>

                                                <div class="col-xs-12 paddingless">
                                                    <div class="form-group Qty">
                                                        <input type='button' data-id='<%# Eval("iFacRglID") %>' value='-' class="txtQtyMin" />
                                                        <asp:TextBox runat="server" ID="txtQuantity" data-id='<%# Eval("iFacRglID") %>' value='<%# CInt(Eval("iAantal")) %>' CssClass="txtQuantity current" />
                                                        <input type="button" data-id='<%# Eval("iFacRglID") %>' value="+" class="txtQtyPlus" />
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 paddingless right">
                                                    <span class="total">
                                                        <asp:Literal ID="Literal5" Text='<%# "€ " & Eval("iBedrag")%>' runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="row">

                        <div class="grey-bg">
                            <div class="clearfix">
                                <div class="col-sm-2 xs-hidden"></div>
                                <div class="col-sm-5 col-xs-6">
                                    <label>
                                        <b>
                                            <asp:Literal Text="<%$ Resources:resource, Subtotaal %>" runat="server" /></b>
                                    </label>
                                </div>
                                <div class="col-sm-5 col-xs-6 lblTotalExcl">
                                    &euro;
                                    <asp:Literal ID="ltlBedragEx" runat="server" />
                                </div>
                            </div>

                            <div class="clearfix">
                                <div class="col-sm-2 xs-hidden paddingless"></div>
                                <div class="col-sm-5 col-xs-6 paddingless">
                                    <label>
                                        <asp:Literal Text="<%$ Resources:resource, Discount %>" runat="server" />
                                    </label>
                                </div>
                                <div class="col-sm-5 col-xs-6 paddingless lblDiscount">
                                    &euro;
                                    <asp:Literal ID="ltlDiscount" runat="server" />
                                </div>
                            </div>

                            <div class="col-sm-9 offset-sm-2 col-xs-12 paddingless">
                                <hr class="blackHr" />
                            </div>

                            <div class="clearfix">
                                <div class="col-sm-2 xs-hidden"></div>
                                <div class="col-sm-5 col-xs-6">
                                    <b>
                                        <asp:Literal Text="<%$ Resources:resource, Total %>" runat="server" /></b>
                                </div>
                                <div class="col-sm-5 col-xs-6 lblTotalIncl">
                                    &euro;
                                    <asp:Literal ID="ltlTotaalIn" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="discount divGlobalForm">
                            <div class="">
                                <div class="col-sm-6 paddingless">
                                    <div class="form-group">
                                        <label><b>Discount code</b></label>
                                        <asp:TextBox ID="txtKortingscode" CssClass="form-control korting" runat="server" />
                                    </div>

                                    <div class="form-group" id="divDiscountMsg" runat="server">
                                        <asp:Literal ID="ltlDiscountMsg" runat="server" />
                                    </div>
                                </div>

                                <div class="col-sm-6 paddingless">
                                    <asp:Button CssClass="btn btn-default btn-black " UseSubmitBehavior="false" ID="btnKortingscode" OnClientClick="return isDiscountValid()" Text="<%$ Resources:Resource, Kortingscode %>" runat="server" />
                                </div>
                            </div>

                            <div class="form-group no-margin" id="div1" runat="server" style="display: none;">
                                <label>
                                    <b>
                                        <asp:Literal ID="Literal10" runat="server" /></b>
                                </label>
                            </div>
                        </div>
                        <div id="divOffer" runat="server" Visible="false">
                            <div class="form-inline">
                                <asp:TextBox data-enter-focus="#txtMail" ID="txtName" CssClass="form-control" placeholder="<%$ Resources:resource, formName%>" runat="server" />
                                <asp:TextBox data-enter-trigger="#btnOfferte" ID="txtMailOfferte" CssClass="form-control" placeholder="<%$ Resources:resource, formEmail%>" runat="server" />
                                <asp:TextBox data-enter-trigger="#btnOfferte" ID="txtTel" CssClass="form-control" placeholder="<%$ Resources:resource, formTelephone%>" runat="server" />
                                <asp:Button CssClass="btn btn-default" UseSubmitBehavior="false" ID="btnOfferte" Text="<%$ Resources:resource, requestQoute%>" runat="server" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 paddingless">
                                <div class="col-md-6 paddingless ">
                                    <a href="/shop" class="btn btn-naked back-to-shop" id="btnBack">
                                        <asp:Literal ID="Literal11" Text="<%$ Resources:Resource, DoorgaanWinkelen %>" runat="server" />
                                    </a>
                                </div>
                                <div class="col-md-6 paddingless">
                                    <asp:Button CssClass="btn btn-default btn-black pull-right btn-checkout" UseSubmitBehavior="false" ID="btnCheckout" Text="<%$ Resources:resource, btnCheckout %>" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <h2 class="float-xs-right title">
                <asp:Literal ID="Literal4" Text="<%$ Resources:resource, summary %>" runat="server" />
            </h2>--%>
            </div>
        </div>
    </div>
</asp:Content>
