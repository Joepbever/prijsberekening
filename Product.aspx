<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="Product.aspx.vb" Inherits="Product" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="product-detail">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-5 widget-owl-gallery divWidgetOwlGallery">
                    <div class="home">
                        <a id="aBack" runat="server"><i class="fa fa-angle-left" aria-hidden="true"></i></a>
                    </div>
                    <div class="title-mobile title">
                         <asp:Literal ID="ltlArtikel1" runat="server" />
                    </div>
                    <div class="divSlider slider-detail owl-carousel owl-theme">
                        <asp:Repeater ID="repPictures" runat="server">
                            <ItemTemplate>
                                <div runat="server" id="divItem" class="productImage">
                                    <a href="<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>" data-lightbox="product">
                                        <img class="img-responsive" src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>' />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="divThumbs thumbs owl-carousel" id="divThumbs" runat="server">
                        <asp:Repeater runat="server" ID="repThumbs">
                            <ItemTemplate>
                                <div class="item">
                                    <img runat="server" id="imgThumb" src='<%# Eval("sOriginal").ToString().Replace("~/", sURL()) %>' class="img-responsive"/>
	                            </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="col-sm-6 col-sm-offset-1">
                 

                    <div class="title">
                        <asp:Literal ID="ltlArtikel" runat="server" />
                    </div>

                    <div class="description">
                        <asp:Literal ID="ltlDescription" runat="server" />
                    </div>
                    <div class="price">
                        <span runat="server" id="spanPrice" visible="false">
                          &euro; <asp:Literal ID="ltlPrijs" runat="server" />
                        </span>

                        <div class="discount" runat="server" id="divDiscount" visible="false">
                            <span class="old-price price">&euro; <asp:Literal runat="server" id="ltlOldPrijs" /></span>
                            <span class="price">&euro; <asp:Literal runat="server" id="ltlNewPrice" /></span>
                        </div>

                        <div class="request" runat="server" id="divRequest" visible="false">
                            <asp:Literal runat="server" text='<%$ Resources:Resource, priceOnRequest %>' />
                        </div>
                    </div>
                    <div class="size">
                        <asp:Repeater ID="repArtikelVarianten" runat="server">
                            <ItemTemplate>
                                    <div class="checkboxSizes">
                                        <input type="radio" runat="server" value='<%# Eval("iArtikelVariantID")%>' name="sizes" id="rb" />
                                        <label runat="server" for='<%# "rb" & Eval("sWaarde")%>'><asp:Literal Text='<%# Eval("sWaarde")%>' runat="server" /></label> 
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="options">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="articlenumber">Artikel nummer: <asp:Literal ID="ltlArtikelCode" runat="server" /></div>
                            </div>

                            <div class="col-lg-4 col-sm-12">
                                <div class="form-group quantity Qty">
                                    <input type='button' value="-" class='max txtQtyMinProduct' />
                                    <asp:TextBox runat="server" ID="txtQuantity" value='1' CssClass="txtQuantity current" />
                                    <input type="button" value="+" class='min txtQtyPlusProduct' />
                                </div>
                            </div>

                            <div class="col-sm-12 col-lg-8">
                                <a class="btn-checkout aProductLinkProduct" runat="server" id="aProductLinkProduct"><asp:Literal ID="ltlLink" runat="server" /></a>
                                <span><i class="fa fa-heart" aria-hidden="true"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row related autoHeightContainer" runat="server" id="divRelated">
                <div class="col-sm-12 ">
                    <div class="main">
                        <asp:Literal runat="server" text='<%$ Resources:Resource, Related %>' />
                    </div>
                </div>
                <asp:repeater id="repRelated" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3 col-sm-6 autoHeightChild">
                            <div class="item ">
                                <a runat="server" id="aFollow" href='<%# Eval("sURL")%>'>
                                    <img id="imgThumb" class="img-responsive" runat="server" />
                                </a>
                            </div>
                            <span class="title"><asp:Literal runat="server" Text='<%# Eval("sWaarde")%>' /></span>
                        </div>
                    </ItemTemplate>
                </asp:repeater>
            </div>
        </div>
        <div class="hearts"></div>
    </section>

    <asp:HiddenField ID="hidArtikel" runat="server" />
    <asp:HiddenField ID="hidArtikelID" runat="server" />
    <asp:HiddenField ID="hidArtikelVariantID" runat="server" />
    <asp:HiddenField ID="hidTaalID" runat="server" />

</asp:Content>