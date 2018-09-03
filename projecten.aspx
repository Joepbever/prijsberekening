<%@ Page Language="VB" AutoEventWireup="false" CodeFile="projecten.aspx.vb" MasterPageFile="~/page.master" Inherits="_Projecten" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" property="stylesheet" type="text/css" href="<%: Styles.Url("~/bundles/CSS-portfolio")%>" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-xs-12">
        <div class="widget-breadcrumps" id="divBreadCrumps" runat="server">
            <ul>
                <asp:Literal runat="server" ID="ltlBreadCrumps" />
            </ul>
        </div>
    </div>

    <section class="section-banner img-responsive divBG">
        <div class="container">
            <div class="row">
                <div class="divImg">
                    <asp:Literal ID="ltlImgHeader" runat="server" />
                    <img src="/Resources/img/header.png" />
                </div>
                <asp:Literal ID="Titel" runat="server" />
            </div>
        </div>
    </section>

    <section class="widget-portfolio">
        <div class="container">
            <div class="row">
                <%--                <div class="col-md-12 category hidden">
                    <asp:Repeater ID="repPortfolioCategorie" runat="server">
                        <ItemTemplate>
                    <span class="portfolio-categorie"><asp:Literal Text='<%# Eval("sTitle")%>' runat="server" /></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>--%>
            </div>

            <div class="row">
                <%--               <div class="button-group filters-button-group">
                  <div class="col-md-12 category">
                    <span class="portfolio-categorie button is-checked" data-filter="*">all</span>
                    <span class="portfolio-categorie button" data-filter=".website">Websites</span>
                    <span class="portfolio-categorie button" data-filter=".webshop">Webshops</span>
                    <span class="portfolio-categorie button" data-filter=".design">Webapplicaties op maat</span>
                </div>
                </div>--%>

                <div class="grid">
                    <asp:Repeater ID="repProjecten" runat="server">
                        <ItemTemplate>
                            <a href="#" id="aLink" runat="server">
                                <div class="element-item website col-sm-6" data-category="website">
                                    <img runat="server" id="imgDesktop" class="img-responsive" />
                                    <div class="detail">
                                        <h5><asp:Literal Text='<%# Eval("sTitle")%>' runat="server" /></h5>
                                        <h6><asp:Literal Text='<%# Eval("sColor")%>' runat="server" /></h6>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
