<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="Bevestiging.aspx.vb" Inherits="Bevestiging" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="/scripts/checkout2.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="login-temp bevestiging terms">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3 fs1">
                    <div class="inner">
                        <h5>Uw bestelling</h5>
                    </div>
                </div>

                <div class="col-md-12 order-complete-message">
                    <asp:Literal ID="cancelled" Visible="false" runat="server" />
                    <asp:Literal ID="Success" Visible="false" runat="server" />
                    <asp:Literal ID="open" Visible="false" runat="server" />
                    <asp:Literal ID="expired" Visible="false" runat="server" />
                    <asp:Literal ID="pending" Visible="false" runat="server" />
                    <asp:Literal ID="rekening" Visible="false" runat="server" />
                    <br />
                    <b><asp:Literal ID="Literal2" Text="<%$ Resources:Resource, OrderNummerIs %>" runat="server" /> <asp:Literal ID="ltlFactuur" runat="server" /></b>
                </div>

                <div class=" payment" runat="server" id="divPayment" visible="false" >
                    <div class="col-md-4 col-sm-6">
                        <div class="form-group inner rblPadd">
                            <h4><asp:Literal ID="Literal4" Text="<%$ Resources:Resource, SelecteerBetaalmethode %>" runat="server" /> </h4>
                            <asp:RadioButtonList ID="rblBetaalmethode" CssClass="checkbox" DataTextField="sTitle" DataValueField="sTitle" runat="server">
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="col-md-4 col-sm-6">
                        <div id="divIdeal">
                            <div class="form-group inner">
                                <h4><asp:Literal ID="Literal6" Text="<%$ Resources:Resource, SelecteerBank %>" runat="server" /> </h4>
                                <asp:DropDownList ID="ddlBank" CssClass="form-control land2" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button id="btnOrder" UseSubmitBehavior="false" CssClass="btn btn-default btn-black" Text="<%$ Resources:Resource, Betalen %>" runat="server" />
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </div>

                <div class="col-sm-4">
                    <div class="content">
                        <h4><asp:Literal ID="Literal5" Text="<%$ Resources:Resource, Bezorgadres %>" runat="server" /></h4>
                        <p><asp:Literal ID="ltlBezorgadres" runat="server" /></p>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="content">
                        <h4><asp:Literal ID="Literal3" Text="<%$ Resources:Resource, Factuuradres %>" runat="server" /></h4>
                        <p><asp:Literal ID="ltlFactuuradres" runat="server" /></p>
                    </div>
                </div>

                <div class="col-sm-4 ">
                    <div class="content">
                        <h4><asp:Literal ID="Literal7" Text="<%$ Resources:Resource, ContactGegevens %>" runat="server" /></h4>
                        <p><asp:Literal ID="Contact" runat="server" /></p>
                    </div>
                </div>

                <div class="col-md-6 col-sm-offset-3 " runat="server" id="divAccount">
                    <p><asp:Literal ID="ltlInfo" runat="server" /></p>
                    <div class="row">
                        <div class="col-sm-6">
                                <div class="link">
                                <a id="aAccount" runat="server" class="btn btn-send"><asp:Literal ID="Literal23" Text="<%$ Resources:Resource, MijnAccount %>" runat="server" /></a>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="link">
                                <a id="aBestellingen" runat="server" class="btn btn-send"><asp:Literal ID="Literal1" Text="<%$ Resources:Resource, MijnBestellingen %>" runat="server" /></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <asp:HiddenField ID="hidFacKopID" runat="server" />
</asp:Content>