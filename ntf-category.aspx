<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ntf-category.aspx.vb" MasterPageFile="~/page.master" Inherits="_categorie" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #hamburger{
            top: 30%;
        }
    </style>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="collection category">
        <div class="container">
            <ul class="fixed_list hidden-xs">
                <li class="active"><a href="#">ALL PRODUCTS</a></li>
                <li><a href="#">ACCESSOIRES</a></li>
                <li><a href="#">PANTS</a></li>
                <li><a href="#">SHIRTS</a></li>
                <li><a href="#">SWEATERS</a></li>
            </ul>
            <asp:Literal ID="Tekst" runat="server" />
             <div runat="server" id="divPageTitle" class="inner">
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
                        <%--href='<%# "/" & sLanguage.ToLower() & "/" & Eval("sQueryString") %>'--%>
                        <a id="aLink" runat="server" class="toggle" role="button" aria-haspopup="true" aria-expanded="true">
                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                        </a>
                    </div>
                    <%--Sub categorieen staan onderaan in de labels--%>
                    <%--<asp:Repeater runat="server" ID="repSubCategories" OnItemDataBound="repSubCategories_ItemDataBound">
                                        <HeaderTemplate>
                                            <ul class="sub">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <a runat="server" id="aLink">
                                                    <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></a>
                                                <asp:Repeater runat="server" ID="repSubSubCategories" OnItemDataBound="repSubSubCategories_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <ul class="subsub">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li>
                                                            <a runat="server" id="aLink">
                                                                <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></a>
                                                            <asp:Repeater runat="server" ID="repSubSubSubCategories" OnItemDataBound="repSubSubSubCategories_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <ul class="subsubsub">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <a runat="server" id="aLink">
                                                                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></a>
                                                                    </li>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </ul>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </li>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </ul>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                            </div>
                        </div>
                </FooterTemplate>
            </asp:Repeater>

            <asp:Repeater ID="repSubCategorieLabels" runat="server">
                <HeaderTemplate>
                    <div class="col-sm-12">
                        <ul class="sub-cat">
                </HeaderTemplate>
                <ItemTemplate>
                    <a id="aSubCatLink" runat="server">
                        <li>
                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                        </li>
                    </a>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                        </div>
                </FooterTemplate>
            </asp:Repeater>

            <%--<ul class="sub-cat">
                        <li><a href="#">wikkel jurken</a></li>
                        <li><a href="#">mini-jurken</a></li>
                        <li><a href="#">onder jurken</a></li>
                        <li><a href="#">maxi-jurken</a></li>
                        <li><a href="#">tricot</a></li>
                    </ul>--%>
            <%--<div class=" widget-products">
                    <h4 runat="server" visible="false" class="align">
                        <asp:Literal ID="ltlActiveCategorie" runat="server" />
                    </h4>
                    <span class="fs3">
                        <asp:Literal ID="ltlCategoriePageItem" runat="server" />
                    </span>
                    <asp:Repeater runat="server" ID="repSubCategoriesContent" OnItemDataBound="repSubCategoriesContent_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-6 item">
                                <div class="Img">
                                    <a id="aLink" runat="server">
                                        <img src="/Resources/img/slide.png" class="img-responsive" />
                                        <img runat="server" id="imgThumb" class="hidden" />
                                    </a>

                                    <div class="after">
                                        <span class="title">
                                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>--%>

            <asp:Repeater runat="server" ID="repArtikelen" OnItemDataBound="repArtikelen_ItemDataBound">
                <HeaderTemplate>
                    <div class="row">
                        <div class=" widget-products widget-products-last">
                            <div id="products" class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divGridItem" class="col-sm-6 col-xs-12 divItem" data-artikel-id='<%# Eval("iArtikelID") %>' runat="server">
                        <div class="item">
                            <a runat="server" id="aNoFollow" rel="nofollow" class="group list-group-image">
                                <div class="img-contain HasHover">
                                    <img runat="server" id="imgThumb" class="" />
                                    <img runat="server" id="imgThumb2" class="hidden" />
                                </div>
                            </a>
                            <div class="after">
                                <span class="title">
                                    <asp:Literal runat="server" ID="ltlName" />
                                        NTF Test Product
                                    <h5>
                                        <asp:Literal runat="server" ID="ltlOmschrijving" />
                                    </h5>
                                </span>
                                <p class="lead hidden">
                                    <span runat="server" id="spPrice" class="price">
                                        &euro;
                                        <asp:Literal runat="server" Text='<%# Eval("iPrijs") %>' /></span> <span runat="server" id="spanPrice" class="price">
                                        <asp:Literal runat="server" ID="ltlNewPrice" />
                                    </span>
                                    <span class="direct" runat="server" id="spVoorraadInfo" visible="false"></span>
                                </p>
                                <%--<div class="discount">
                                        <span class="price old-price"  runat="server" id="divDiscount">&euro; <asp:Literal runat="server" Text='<%# Eval("iPrijs") %>' /></span>
                                    </div>--%>
                                <div class="request hidden" runat="server" id="divRequest" visible="false">
                                    <asp:Literal runat="server" Text='<%$ Resources:Resource, priceOnRequest %>' />
                                    <a class="btn btn-black aProductLink" runat="server" id="aFollow">
                                        <asp:Literal runat="server" ID="ltlLink" />
                                    </a>
                                </div>

                                <div class="caption hidden">

                                    <span class="group inner list-group-item-heading title"></span>

                                    <p class="group inner list-group-item-text">
                                        <div visible="false"></div>
                                    </p>
                                </div>
                            </div>
                        </div>
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
                        <div class=" widget-products widget-products-last">
                            <div id="products" class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divGridItem" class="col-md-6 item divItem" runat="server">
                        <a runat="server" id="aNoFollow" rel="nofollow" class="group list-group-image">
                            <div class="img-contain HasHover">
                                <img runat="server" id="imgCategorieThumb" class="" />
                            </div>
                        </a>
                        <div class="after">

                            <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />

                            <%--<div class="caption">
                                <span class="group inner list-group-item-heading title"></span>

                                <p class="group inner list-group-item-text">
                                    <div visible="false"></div>
                                </p>
                            </div>--%>
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

    <%--<ul class="pagination">
        <asp:Repeater ID="repPaging" runat="server" OnItemCommand="repPaging_ItemCommand">
            <ItemTemplate>
                <li runat="server" id="liPager">
                    <asp:LinkButton ID="btnPage" CssClass="page" CommandName="Page" CommandArgument="<%# Container.DataItem %>" runat="server"><%# Container.DataItem %></asp:LinkButton>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>--%>
    
     <asp:HiddenField ID="hidIndex" runat="server" />
</asp:Content>