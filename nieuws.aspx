<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nieuws.aspx.vb" MasterPageFile="~/page.master" Inherits="_Nieuws" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="col-xs-12">
        <div class="widget-breadcrumps" id="divBreadCrumps" runat="server">
            <ul>
                <asp:Literal runat="server" ID="ltlBreadCrumps" />
            </ul>
        </div>
    </div>

    <asp:Literal ID="Titel" runat="server" />

    <%-- BEGIN NIEUWS OVERZICHT --%>

    <div class="news temp2">
        <div class="container">
            <div class="row">
                <asp:Repeater runat="server" ID="repNieuws">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-6">
                            <a runat="server" id="aLink">
                                <div class="content">
                                    <img runat="server" id="imgDesktop" class="img-responsive" />
                                    <h5><asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></h5>
                                    <div class="date">
                                        <asp:Literal runat="server" Text='<%#CDate(Eval("dtDatum")).ToString("MMM dd, yyyy") %>' />
                                    </div>
                                    <asp:Literal runat="server" Text='<%# Eval("sItem1") %>' />
                                    <a>Lees meer</a>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <%-- EINDE NIEUWS OVERZICHT --%>

    <%-- BEGIN NIEUWS OVERZICHT MET ISOTOOP --%>

    <div class="row">
        <div class="button-group filters-button-group">
            <div class="col-sm-10 col-sm-offset-1 category">
                <span class="portfolio-categorie button is-checked" data-filter="*">all</span>
                <asp:Repeater ID="repCategorie" runat="server">
                    <ItemTemplate>
                        <span class="portfolio-categorie button" data-filter='<%# "." & FriendlyUrl(Eval("sTitle")).ToLower %>'>
                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></span>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="grid">
            <asp:Repeater runat="server" ID="repNieuwsIso">
                <ItemTemplate>
                    <div runat="server" id="divItem" class="col-sm-4 col-sm-6 element-item one">
                        <a runat="server" id="aLink2">
                            <div class="item">
                                <div class="img-contain">
                                    <img runat="server" id="imgDesktop" class="img-responsive" />
                                    <div class="tag golf">Golf</div>
                                </div>

                                <div class="content">
                                    <h5><asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></h5>
                                    <div class="date">
                                        <asp:Literal runat="server" Text='<%#CDate(Eval("dtDatum")).ToString("d MMMM yyyy") %>' /></div>
                                    <asp:Literal runat="server" Text='<%# Eval("sItem1") %>' />
                                    <a runat="server" href="#" rel="nofollow" id="aLink">Lees meer</a>
                                </div>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>



</asp:Content>
