<%@ Page Language="VB" AutoEventWireup="false" CodeFile="category.aspx.vb" MasterPageFile="~/page.master" Inherits="_categorie" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="collection category fs1">
        <div class="container">

            <div runat="server" id="divPageTitle" class="inner ">
                <asp:Literal ID="Titel" runat="server" />
            </div>
            
            <asp:Repeater runat="server" ID="repCategories">
                <HeaderTemplate>
                    <div class="col-sm-12">
                        <div class="head">
                            <div class="owl-carousel owl-theme divOwlcategory">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="items">
                        <a id="aLink" href='<%# Eval("sURL") %>' runat="server" class="toggle" role="button" aria-haspopup="true" aria-expanded="true">
                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                        </a>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                            </div>
                        </div>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Repeater ID="repSubCategorieLabels" OnItemDataBound="repSubCategorieLabels_ItemDataBound" runat="server">
                <HeaderTemplate>
                    <div class="col-sm-12">
                        <ul class="sub-cat">
                </HeaderTemplate>
                <ItemTemplate>
                    <a href='<%# Eval("sURL") %>' runat="server">
                        <li id="liSubCatLabel" runat="server">
                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                        </li>
                    </a>
                </ItemTemplate>
                <FooterTemplate>
                        </ul>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            
            <div class="tekst" id="divTekst" runat="server">
                <asp:Literal ID="Tekst" runat="server" />
            </div>
         
            <asp:Repeater runat="server" ID="repArtikelen" OnItemDataBound="repArtikelen_ItemDataBound">
                <HeaderTemplate>
                    <div class="row">
                        <div class="widget-products widget-products-last">
                            <div id="products" class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divGridItem" class="col-md-3 col-sm-4 divItem" data-artikel-id='<%# Eval("iArtikelID") %>' runat="server">
                        <div class="item">
                            <a runat="server" id="aNoFollow" href='<%# Eval("sURL").ToString().Trim("~") %>' rel="nofollow" class="group list-group-image">
                                <div class="img-contain HasHover">
                                    <img runat="server" id="imgThumb" class="" src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>'/>
                                </div>
                            </a>

                            <div class="divPrice lead">
                                <span runat="server" id="spPrice" class="price">&euro;<asp:Literal runat="server" Text='<%# Eval("iPrijs") %>' /></span>
                                <span runat="server" id="spanPrice" class=""><asp:Literal runat="server" ID="ltlNewPrice" /></span>
                                <span class="direct" runat="server" id="spVoorraadInfo" visible="false"></span>
                            </div>
                        </div>

                        <span class="title" >
                            <asp:Literal runat="server" Visible="false" ID="ltlName" />
                            <asp:Literal runat="server" Visible="false" ID="ltlOmschrijving" />
                        </span>

                        <div class="request" runat="server" id="divRequest" visible="false">
                            <asp:Literal runat="server" Text='<%$ Resources:Resource, priceOnRequest %>' />
                            <a class="btn btn-black aProductLink" runat="server" id="aFollow">
                                <asp:Literal runat="server" ID="ltlLink" />
                            </a>
                        </div>
<%--                        <div class="caption">
                            <span class="group inner list-group-item-heading title"></span>
                            <p class="group inner list-group-item-text">
                                <div visible="false"></div>
                            </p>
                        </div>--%>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                        </div>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Repeater runat="server" ID="repCategorien">
                <HeaderTemplate>
                    <div class="row">
                        <div class="widget-products-last">
                            <div id="products" class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divGridItem" class="col-lg-3 col-sm-4 divItem" runat="server">
                        <div class="item">
                            <a runat="server" href='<%# Eval("sURL").ToString().Trim("~") %>' id="aNoFollow" rel="nofollow" class="group list-group-image">
                                <div class="img-contain HasHover">
                                    <img runat="server" class="category-img" id="imgCategorieThumb"  src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>'/>
                                </div>
                            </a>
                        </div>

                        <div class="divPrice">
                            <span runat="server" id="spPrice" class="categoryPrice">
                                <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                            </span> 
                            <span runat="server" id="spanPrice" class="">
                                    <asp:Literal runat="server" ID="ltlNewPrice" />
                            </span>
                            <span class="direct" runat="server" id="spVoorraadInfo" visible="false"></span>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                        </div>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </section>
     <asp:HiddenField ID="hidIndex" runat="server" />
</asp:Content>
