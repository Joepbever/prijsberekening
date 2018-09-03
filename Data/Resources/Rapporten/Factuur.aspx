<%@ Page Language="VB" AutoEventWireup="false" CodeFile="factuur.aspx.vb" Inherits="pdf_Factuur_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style>
        @font-face {
            font-family: 'Open Sans';
            font-style: normal;
            font-weight: 400;
            src: local('Open Sans'), local('OpenSans'), url(https://fonts.gstatic.com/s/opensans/v13/cJZKeOuBrn4kERxqtaUH3VtXRa8TVwTICgirnJhmVJw.woff2) format('woff2');
            unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2212, U+2215, U+E0FF, U+EFFD, U+F000;
        }

        body, html {
            font-family: 'Open Sans', sans-serif, sans-serif !important;
            background: #FFF;
            padding-top: 50px;
            padding-bottom: 50px;
            font-size: 16px;
        }

        h2 {
            text-transform: uppercase;
            font-size: 38px;
            font-weight: bolder;
            margin: 0px;
            color: #000;
        }
        .clearfix { clear: both; width: 100%; }
        p {
            margin: 5px 0px;
        }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        b { color: #000;}

        .row {
            clear: both;
            width: 100%;
        }

        .col-md-12 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 100%;
            float: left;
        }

        .col-md-11 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 91.66666667%;
            float: left;
        }

        .col-md-10 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 83.33333333%;
            float: left;
        }

        .col-md-9 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 75%;
            float: left;
        }

        .col-md-8 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 66.66666667%;
            float: left;
        }

        .col-md-7 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 58.33333333%;
            float: left;
        }

        .col-md-6 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 50%;
            float: left;
        }

        .col-md-5 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 41.66666667%;
            float: left;
        }

        .col-md-4 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 33.33333333%;
            float: left;
        }

        .col-md-3 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 25%;
            float: left;
        }

        .col-md-2 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 16.66666667%;
            float: left;
        }

        .col-md-1 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
            width: 8.33333333%;
            float: left;
        }

        .container {
            padding-right: 30px;
            padding-left: 30px;
            margin-right: auto;
            margin-left: auto;
            width: 950px;
        }

        .paddingless {
            padding: 0px;
        }

        .pull-right {
            float: right !important;
        }

        .pull-left {
            float: left !important;
        }

        .img-responsive {
            max-width: 100%;
            height: auto;
            display: block;
        }

        hr {
            margin: 0px;
            clear: both;
            margin-top: 5px;
            height: 2px;
            background-color: #000;
            border: 0;
            margin-bottom:14px;
        }

        .clearfix {
            clear: both;
            width: 100%;
        }

        .row.bordered {
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            clear: both;
        }

        .no-margin-p p {
            margin: 0px;
        }

        .padding-left {
            padding-left: 30px;
        }

        .align-right {
            text-align: right;
        }

        .align-center {
            text-align: center;
        }

        .white-space {
            width: 100%;
            padding-top: 30px;
            padding-bottom: 30px;
            clear: both;
        }

        .white-space-big {
            width: 100%;
            padding-top: 60px;
            padding-bottom: 60px;
            clear: both;
        }
    </style>
    <form id="form1" runat="server">
        <div class="container">

            <%-- Head --%>

            <div class="row">
                <div class="col-md-5">
                    <h2 class="pull-left">Invoice</h2>
                </div>

                <div class="col-md-3"></div>

                <div class="col-md-4 padding-left">
                    <img class="img-responsive pull-right" id="imgLogo" runat="server" />
                </div>
            </div>

            <%-- Head END --%>

            <div class="white-space"></div>

            <%-- Invoice Data --%>

            <div class="row">
                <div class="col-md-4">
                    <p><b>Delivery Address</b></p>
                    <p>
                        <asp:Literal ID="ltlBezorgadres" runat="server" /></p>
                </div>

                <div class="col-md-4">
                    <p><b>Invoice Address</b></p>
                    <p><asp:Literal ID="ltlFactuuradres" runat="server" /></p>
                </div>
                <div class="col-md-2">
                    <div class="pull-right">
                        <p>Invoice date:</p>
                        <p>Invoice number:</p>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="pull-right">
                        <p>
                            <asp:Literal ID="ltlDatum" runat="server" /></p>
                        <p>
                            <asp:Literal ID="ltlFactuur" runat="server" /></p>
                    </div>
                </div>
            </div>

            <%-- Invoice Data END --%>

            <div class="white-space-big"></div>

            <%-- Products --%>

            <div class="row no-margin-p">
                <div class="col-md-1">
                    <div class="pull-left">
                        <p><b>QTY</b></p>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="pull-left">
                        <p><b>PRODUCT</b></p>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="pull-left">
                        <p><b>PRICE</b></p>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="pull-left">
                        <p><b>VAT%</b></p>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="pull-right">
                        <p><b>TOTAL PRICE</b></p>
                    </div>
                </div>
            </div>



            <div class="row bordered">
                <asp:Repeater ID="repFacRgls" runat="server">
                    <ItemTemplate>

                        <div class="col-md-1">
                            <div class="pull-left">
                                <p>
                                    <asp:Literal Text='<%# CInt(Eval("iAantal")) %>' runat="server" /></p>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="pull-left">
                                <p>
                                    <asp:Literal Text='<%# Eval("sArtikel")%>' runat="server" /><br />
                                    Size:
                                    <asp:Literal Text='<%# Eval("sOmschrijving")%>' runat="server" />
                                </p>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="col-md-4 paddingless">
                                <div class="pull-left">
                                    <p>€</p>
                                </div>
                            </div>
                            <div class="col-md-6 paddingless">
                                <div class="pull-left">
                                    <p>
                                        <asp:Literal Text='<%# Eval("iStuksPrijs")%>' runat="server" /></p>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="pull-left">
                                <p>21%</p>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="col-md-6 paddingless">
                                <div class="pull-right custom-margin">
                                    <p>€</p>
                                </div>
                            </div>
                            <div class="col-md-6 paddingless">
                                <div class="pull-right">
                                    <p>
                                        <asp:Literal Text='<%# Eval("iBedrag")%>' runat="server" /></p>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clearfix"></div>
            </div>

            <%-- Products END --%>

            <%-- Total price --%>

            <div class="row">
                <div class="col-md-6">
                </div>

                <div class="col-md-6">
                    <div class="col-md-3"></div>
                    <div class="col-md-6 paddingless">
                        <div class="pull-left align-right">
                            <p runat="server" id="pDiscount">Discount:</p>
                            <p>Shipping:</p>
                            <p>Total excl. VAT:</p>
                            <p>Total VAT:</p>
                            <br />
                            <p>Total:</p>
                        </div>
                    </div>
                    <div class="col-md-3 paddingless">
                        <div class="col-md-2 paddingless">
                            <div class="pull-left">
                                <p runat="server" id="pEuroDiscount">€</p>
                                <p>€</p>
                                <p>€</p>
                                <p>€</p>
                            </div>
                        </div>
                        <div class="col-md-10 paddingless">
                            <div class="pull-right align-right">
                                <p runat="server" id="pBedragDiscount">
                                    <asp:Literal ID="ltlDiscount" runat="server" /></p>
                                <p>
                                    <asp:Literal ID="ltlShipping" runat="server" /></p>
                                <p>
                                    <asp:Literal ID="ltlTotaalExcl" runat="server" /></p>
                                <p>
                                    <asp:Literal ID="ltlVAT" runat="server" /></p>
                            </div>
                        </div>

                        <div class="col-md-2 paddingless">
                        </div>
                        <div class="col-md-10 paddingless">
                            <hr />
                        </div>
                        <div class="row"></div>
                        <div class="col-md-2 paddingless">
                            <div class="pull-left">
                                <p>€</p>
                            </div>
                        </div>
                        <div class="col-md-10 paddingless">
                            <div class="pull-right align-right">
                                <p>
                                    <asp:Literal ID="ltlTotal" runat="server" /></p>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- Total price END --%>
            </div>
          <%--  <div class="col-md-12 align-center" style="margin-top: 100px;"><br /><br /><br /><br /><br /><br />
                <asp:Literal runat="server" ID="ltlInfo" />
            </div>--%>
        </div>

    </form>
</body>
</html>
