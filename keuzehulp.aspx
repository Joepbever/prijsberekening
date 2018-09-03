<%@ Page Language="VB" AutoEventWireup="false" CodeFile="keuzehulp.aspx.vb" MasterPageFile="~/page.master" Inherits="_categorie" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" property="stylesheet" type="text/css" href="<%: Styles.Url("~/bundles/CSS-shop") %>" />
    <script type="text/javascript" src="<%: Scripts.Url("~/bundles/JS-shop") %>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <section class="shop">

        <div class="banner divBG">
            <div class="divImg">
                <img src="/Resources/img/banner-1.jpg" />
            </div>

            <div class="button JS-button">
            </div>
        </div>


        <div class="products divProducts dealer-login ">
            <div class="row">
                <div class="col-xs-12">
                    <div class="sort-products">
                        <div class="left">

<%--                            <label class="toggle-search ToggleSearch"></label>

                            <div class="form-group search-bar divSearch">
                                <label class="ToggleSearch"></label>
                                <asp:DropDownList ID="ddlBrillen" CssClass="form-control search-brillen" AppendDataBoundItems="true" DataTextField="sTitle" DataValueField="iLijstItemID" type='hidden' runat="server">
                                    <asp:ListItem>Caravan Monoqool</asp:ListItem>
                                    <asp:ListItem>Clubland Monoqool</asp:ListItem>
                                    <asp:ListItem>0254E-BKCY Anglo American</asp:ListItem>
                                    <asp:ListItem>Coventry Henry Jullien</asp:ListItem> 
                                </asp:DropDownList>
                                <div class="close-search CloseSearch"></div>
                            </div>

                            <div class="form-group">

                            </div>--%>

                        </div>
                        <div class="right">

                            <div class="filter-toggle FilterToggle">
                                <div class="text"><asp:Literal runat="server" ID="Literal5" text='<%$ Resources:Resource, ltlfilteren %>' /></div>
                                <div class="filter-menu">
                                    <div></div>
                                    <div></div>
                                    <div></div>
                                </div>
                            </div>

                            <label runat="server" id="lblSort" text='<%$ Resources:Resource, lblSort %>'></label>
                            <select id="SortOptions" runat="server">
                               <option value="1">prijs oplopend</option>
                               <option value="2">prijs aflopend</option>
                            </select>

                            <%-- <a class="new DivNewProducts" id="btnNew">
                                Nieuw
                            </a>--%>

                        </div>
                    </div>
                </div>
            </div>

            <div class="product-list" id="artikelen">
            </div>

            <div class="product-filter divFilters">

                <h4><asp:Literal runat="server" ID="Literal4" text='<%$ Resources:Resource, ltlfilteren %>' /></h4>

                <div class="mobile-version">
                    <div class="filter-toggle FilterToggle">
                        <div class="text"><asp:Literal runat="server" ID="Literal3" text='<%$ Resources:Resource, ltlfilteren %>' /></div>
                        <div class="filter-menu active">
                            <div></div>
                            <div></div>
                            <div></div>
                        </div>
                    </div>

                    <div class="clear-filters">
                        <i class="icon-reload icons"></i>
                    </div>
                </div>

                <div class="active-filters">
               
                </div>
                    <div class="clear-filters">
                    <asp:Literal runat="server" ID="Literal2" text='<%$ Resources:Resource, ltlremovefilters %>' />
                    <i class="icon-reload icons"></i>
                </div>

                <div id="categories">
                </div>

                <div class="filter range-slider divSlider" runat="server" id="divPriceRange">
                    <asp:Literal runat="server" ID="Literal1" text='<%$ Resources:Resource, ltlpriceineuros %>' />
                    <input type="text" id="PriceMin" class="min"/>
                    <input type="text" id="PriceMax" class="max"/>
                    <div id="slider"></div>
                </div>
            </div>
        </div>
    </section>

    <div class="FixedBack"></div>
    <div class="NavWhite"></div>

</asp:Content>