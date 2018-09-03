<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default-ntf.aspx.vb" MasterPageFile="~/page.master" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- LOOKBOOK BACKGROUND --%>
    <div class="home-lookbook">
        <div class="home-lookbook__bg-container js-printed-checker printed">
            <div class="home-lookbook__bg" style="background-color: #fff;"></div>
        </div>

        <a href="" class="home-lookbook__product js-printed-checker printed">
            <div class="home-lookbook__product-bg" style="background-image: url(<%: sBackgroundURL %>)"></div>
        </a>
    </div>
    <%-- /LOOKBOOK BACKGROUND --%>

    <%-- PARALAX --%>
    <div class="scroll-content">
        <div class="home-product-grid">
            <div class="home-product__wrap">
                <ul class="home-product__list parallax parallax5">
                    <asp:Repeater runat="server" ID="repArtikel1">
                        <ItemTemplate>
                            <li class="home-product__item js-scroll js-stack-scroll is-show ">
                                <div class="home-product__img">
                                    <img class="js-stack-image" src='<%# sURL() & Eval("sOriginal").ToString.TrimStart("~") %>' alt="reversible silver/navy" />
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="home-product-grid">
            <div class="home-product__wrap">
                <ul class="home-product__list parallax parallax4">
                    <asp:Repeater runat="server" ID="repArtikel2">
                        <ItemTemplate>
                            <li class="home-product__item js-scroll js-stack-scroll is-show ">
                                <div class="home-product__img">
                                    <img class="js-stack-image" src='<%# sURL() & Eval("sOriginal").ToString.TrimStart("~") %>' alt="reversible silver/navy" />
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="home-product-grid">
            <div class="home-product__wrap">
                <ul class="home-product__list parallax parallax3">
                    <asp:Repeater runat="server" ID="repArtikel3">
                        <ItemTemplate>
                            <li class="home-product__item js-scroll js-stack-scroll is-show ">
                                <div class="home-product__img">
                                    <img class="js-stack-image" src='<%# sURL() & Eval("sOriginal").ToString.TrimStart("~") %>' alt="reversible silver/navy" />
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="home-product-grid">
            <div class="home-product__wrap">
                <ul class="home-product__list parallax parallax2">
                    <asp:Repeater runat="server" ID="repArtikel4">
                        <ItemTemplate>
                            <li class="home-product__item js-scroll js-stack-scroll is-show ">
                                <div class="home-product__img">
                                    <img class="js-stack-image" src='<%# sURL() & Eval("sOriginal").ToString.TrimStart("~") %>' alt="reversible silver/navy" />
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <%-- /PARALAX --%>

        <%-- MAIN CONTENT --%>
        <div class="home-product-grid">
            <div class="home-product__wrap" id="mainContent">
                <ul class="home-product__list parallax1">
                    <asp:Repeater runat="server" ID="repArtikel">
                        <ItemTemplate>
                            <li class="home-product__item js-scroll js-stack-scroll is-show ">
                                <a href='<%# sURL() & Eval("sURL").ToString.TrimStart("~") %>' class="home-product__img">
                                    <img class="js-stack-image" src='<%# sURL() & Eval("sOriginal").ToString.TrimStart("~") %>' alt="reversible silver/navy" />
                                    <div class="hover_product">
                                        <h6><%# Eval("sArtikel") %></h6>
                                        <p>&euro; <%# Eval("iPrijs") %></p>
                                    </div>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <%-- /MAIN CONTENT --%>

        <%-- FOOTER --%>
        <div class="footer">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-3">
                        <b>contact@ninetyfour.com</b>

                        <div class="form-group hidden">
                            <label>Newsletter</label>
                            <asp:TextBox ID="txtEmailHome" CssClass="form-control" TextMode="Email" runat="server" />
                            <asp:Button ID="btnSubscribeHome" UseSubmitBehavior="false" runat="server" Text="Subscribe" />
                        </div>
                    </div>
                    <div class="col-sm-4 links">
                        <ul>
                            <li><a href="#" id="aHome" runat="server">Home</a></li>
                            <li><a href="#" id="aShop" runat="server">Shop</a></li>
                            <li><a href="#" id="aAbout" runat="server">About</a></li>
                            <li><a href="#" id="aLookbook" runat="server">Lookbook</a></li>
                        </ul>

                        <ul>
                            <li><a href="#" id="aStocklist" runat="server">Stocklist</a></li>
                            <li><a href="#" id="aContact" runat="server">Contact</a></li>
                            <li><a href="#" id="aTerms" runat="server">Terms</a></li>
                        </ul>
                    </div>
                    <div class="col-sm-4 col-sm-offset-1 social">
                        <a href="#" id="aFacebook" runat="server" target="_blank"><i class="fab fa-facebook-f" aria-hidden="true"></i></a>
                        <a href="#" id="aYoutube" runat="server" target="_blank"><i class="fab fa-youtube" aria-hidden="true"></i></a>
                        <a href="#" id="aTwitter" runat="server" target="_blank"><i class="fab fa-twitter" aria-hidden="true"></i></a>
                        <a href="#" id="aInstagram" runat="server" target="_blank"><i class="fab fa-instagram" aria-hidden="true"></i></a>
                        <p>Ninetyfour, 2018 Realisatie door <a href="https://websentiment.nl">WebSentiment</a></p>
                    </div>
                </div>
            </div>
        </div>
        <%-- /FOOTER --%>
    </div>
</asp:Content>
