<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="ntf-Product.aspx.vb" Inherits="Product" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <%--    <link rel="stylesheet" property="stylesheet" type="text/css" href="<%: Styles.Url("~/bundles/CSS-product") %>" />--%>
    <script type="text/javascript" src="<%: Scripts.Url("~/bundles/JS-ntf-product") %>"></script>
    <style>
        #hamburger {
            top: 45%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="product-detail container-fluid">
        <div class="row">
            <div class="col-md-3 col-xs-1">
            </div>
            <div class="col-md-6 col-xs-10">
                <div class="slider slider_one">
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/67dc0662-519e-46bb-8d4f-d97e38026288RICHARD-TSHIRT-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/4159274c-3653-4850-9fb4-baa25fda8e99RICHARD-TSHIRT-3.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/67dc0662-519e-46bb-8d4f-d97e38026288RICHARD-TSHIRT-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/cc2edf5b-ce66-427e-a597-931d51efa846NO-FACE-NO-CASE-1.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/88b797a5-8c09-419c-a399-a5f79df48aa7NO-FACE-NO-CASE-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/7fdd5493-b9eb-43ed-ab24-86700603a429NO-FACE-NO-CASE-3.jpg" />
                    </div>
                </div>
                <div class="arrows">
                    <div class="clearfix">
                        <button class="arrow arrowNext pull-right"><i class="fa fa-caret-right"></i></button>
                    </div>
                    <div class="clearfix">
                        <button class="arrow arrowBack pull-left"><i class="fa fa-caret-left"></i></button>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-xs-12">
                <div class="desc col-md-3">
                    <h1 class="title">NTF NO FACE NO CASE TSHIRT</h1>
                    <div>
                        <label class="price">€ 49.95</label>
                        <div class="size">
                            <b>size:</b>
                            <asp:Repeater ID="repArtikelVarianten" runat="server">
                                <ItemTemplate>
                                    <div class=" checkboxSizes">
                                        <input type="radio" runat="server" value='<%# Eval("iArtikelVariantID")%>' name="sizes" id="rb" />
                                        <label runat="server" for='<%# "rb" & Eval("sWaarde")%>'>
                                            <asp:Literal Text='<%# Eval("sWaarde")%>' runat="server" /></label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <div class="checkboxSizes">
                                <input type="radio" runat="server" value='s' name="sizes" />
                                <label runat="server">xs</label>
                            </div>
                            <div class="checkboxSizes">
                                <input type="radio" runat="server" value='s' name="sizes" />
                                <label runat="server">s</label>
                            </div>
                            <div class="checkboxSizes">
                                <input type="radio" runat="server" value='s' name="sizes" />
                                <label runat="server">M</label>
                            </div>
                            <div class="checkboxSizes">
                                <input type="radio" runat="server" value='s' name="sizes" />
                                <label runat="server">L</label>
                            </div>
                            <div class="checkboxSizes">
                                <input type="radio" runat="server" value='s' name="sizes" />
                                <label runat="server">xl</label>
                            </div>
                            <div class="checkboxSizes">
                                <input type="radio" runat="server" value='s' name="sizes" />
                                <label runat="server">xxl</label>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="options">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group quantity Qty">
                                        <input type='button' value="-" class='max txtQtyMin' />
                                        <asp:TextBox runat="server" ID="txtQuantity" value='1' CssClass="txtQuantity current" />
                                        <input type="button" value="+" class='min txtQtyPlus' />
                                    </div>
                                    <a class="btn-checkout aProductLink" runat="server" id="aProductLink">add to bag</a>
                                    <div class="hidden">
                                        <asp:Literal ID="ltlLink" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="slide_nav">
            <div class="col-xs-5 col-xs-offset-1">
                <div class="slider_two">
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/4159274c-3653-4850-9fb4-baa25fda8e99RICHARD-TSHIRT-3.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/67dc0662-519e-46bb-8d4f-d97e38026288RICHARD-TSHIRT-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/cc2edf5b-ce66-427e-a597-931d51efa846NO-FACE-NO-CASE-1.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/88b797a5-8c09-419c-a399-a5f79df48aa7NO-FACE-NO-CASE-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/7fdd5493-b9eb-43ed-ab24-86700603a429NO-FACE-NO-CASE-3.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/67dc0662-519e-46bb-8d4f-d97e38026288RICHARD-TSHIRT-2.jpg" />
                    </div>
                </div>
            </div>
            <div class="col-xs-1 on_hidden"></div>
            <div class="col-xs-5 on_hidden">
                <div class="slider_three">
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/67dc0662-519e-46bb-8d4f-d97e38026288RICHARD-TSHIRT-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/cc2edf5b-ce66-427e-a597-931d51efa846NO-FACE-NO-CASE-1.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/88b797a5-8c09-419c-a399-a5f79df48aa7NO-FACE-NO-CASE-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/7fdd5493-b9eb-43ed-ab24-86700603a429NO-FACE-NO-CASE-3.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/67dc0662-519e-46bb-8d4f-d97e38026288RICHARD-TSHIRT-2.jpg" />
                    </div>
                    <div>
                        <img src="https://ninety-four.com/Uploads/Images/Original/4159274c-3653-4850-9fb4-baa25fda8e99RICHARD-TSHIRT-3.jpg" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row product_content">
            <div class="col-md-3"></div>
            <div class="col-xs-12 col-md-6">
                <div class="row">
                    <h5 class="description_title">Tincidunt integer eu augue augue nunc elit dolor
                    </h5>
                    <div class="desciption">
                        Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
                    </div>
                </div>
                <div class="row">
                    <table class="table">
                        <caption>size & fit</caption>
                        <thead>
                            <tr>
                                <th scope="col" class="first">size</th>
                                <th scope="col">xs</th>
                                <th scope="col">s</th>
                                <th scope="col">m</th>
                                <th scope="col">l</th>
                                <th scope="col">xl</th>
                                <th scope="col">xxl</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row" class="first">sleeve length (from center back)</th>
                                <td>23</td>
                                <td>14</td>
                                <td>61</td>
                                <td>92</td>
                                <td>61</td>
                                <td>92</td>
                            </tr>
                            <tr>
                                <th scope="row" class="first">chest width</th>
                                <td>23</td>
                                <td>14</td>
                                <td>61</td>
                                <td>92</td>
                                <td>61</td>
                                <td>92</td>
                            </tr>
                            <tr>
                                <th scope="row" class="first">body length (from hsp)</th>
                                <td>23</td>
                                <td>14</td>
                                <td>61</td>
                                <td>92</td>
                                <td>61</td>
                                <td>92</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="col-xs-3"></div>
        </div>
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-9 col-sm-12 product_banner">
                <img src="http://via.placeholder.com/1400x600" alt="" class="banner" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-xs-0">
            </div>
            <div class="col-md-9 col-xs-12">
                <div class="info-shipping col-xs-offset-2 col-xs-8">
                    <div>
                        shipping  worldwide
                    </div>
                    <div>
                        free shipping on all canadian  orders over $200 cad
                    </div>
                </div>
            </div>
        </div>
        <div class="row footer">
            <div class="bottom_slider">
                <div>
                    <img src="http://via.placeholder.com/330x385" />
                </div>
                <div>
                    <img src="http://via.placeholder.com/330x385" />
                </div>
                <div>
                    <img src="http://via.placeholder.com/330x385" />
                </div>
                <div>
                    <img src="http://via.placeholder.com/330x385" />
                </div>
                <div>
                    <img src="http://via.placeholder.com/330x385" />
                </div>
                <div>
                    <img src="http://via.placeholder.com/330x385" />
                </div>
            </div>
        </div>

    </section>

    <%--    <section class="product-detail">
        <div class="container">
            <div class="row">
                <div class="col-sm-6">
                    <div class="divSlider owl-carousel">
                        <img src="http://via.placeholder.com/500x500" />
                        <img src="http://via.placeholder.com/500x500" />
                        <img src="http://via.placeholder.com/500x500" />
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="title">
                        <asp:Literal ID="ltlArtikel" runat="server" />
                        NTF Yellow sig. Tshirt
                    </div>

                    <div class="price">
                        <span runat="server" id="spanPrice" visible="false">&euro;
                            <asp:Literal ID="ltlPrijs" runat="server" />
                        </span>

                        <div class="discount" runat="server" id="divDiscount" visible="false">
                            <span class="old-price price">&euro;
                                <asp:Literal runat="server" ID="ltlOldPrijs" /></span>
                            <span class="price">&euro;
                                <asp:Literal runat="server" ID="ltlNewPrice" /></span>
                        </div>

                        <div class="request" runat="server" id="divRequest" visible="false">
                            <asp:Literal runat="server" Text='<%$ Resources:Resource, priceOnRequest %>' />
                        </div>
                    </div>

                    <div class="size">
                        <b>size:</b>
                        <asp:Repeater ID="repArtikelVarianten" runat="server">
                            <ItemTemplate>
                                <div class=" checkboxSizes">
                                    <input type="radio" runat="server" value='<%# Eval("iArtikelVariantID")%>' name="sizes" id="rb" />
                                    <label runat="server" for='<%# "rb" & Eval("sWaarde")%>'>
                                        <asp:Literal Text='<%# Eval("sWaarde")%>' runat="server" /></label>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="checkboxSizes">
                            <input type="radio" runat="server" value='s' name="sizes" />
                            <label runat="server">xs</label>
                        </div>
                        <div class="checkboxSizes">
                            <input type="radio" runat="server" value='s' name="sizes" />
                            <label runat="server">s</label>
                        </div>
                        <div class="checkboxSizes">
                            <input type="radio" runat="server" value='s' name="sizes" />
                            <label runat="server">M</label>
                        </div>
                        <div class="checkboxSizes">
                            <input type="radio" runat="server" value='s' name="sizes" />
                            <label runat="server">L</label>
                        </div>
                        <div class="checkboxSizes">
                            <input type="radio" runat="server" value='s' name="sizes" />
                            <label runat="server">xl</label>
                        </div>
                        <div class="checkboxSizes">
                            <input type="radio" runat="server" value='s' name="sizes" />
                            <label runat="server">xxl</label>
                        </div>
                    </div>

                    <div class="description">
                        <b>Description</b>
                        <asp:Literal ID="ltlDescription" runat="server" />
                        Unisex 100% Cotton Black<br />
                        <br />
                        <br />
                        Take on size up for oversized fit<br />
                        Wash inside out / Cold wash<br />
                        Don't iron on print
                        <br />
                        Made in Portugal
                    </div>--%>

    <%-- <div class="deliver">
                            <span class="item"><span class="black">Delivery time: </span><span class="green"><asp:Literal ID="ltlDeliveryTime" runat="server" /></span></span>
                            <asp:Literal ID="ltlDownloads" runat="server" />
                        </div>--%>

    <%--                    <div class="options">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group quantity Qty">
                                    <input type='button' value="-" class='max txtQtyMin' />
                                    <asp:TextBox runat="server" ID="txtQuantity" value='1' CssClass="txtQuantity current" />
                                    <input type="button" value="+" class='min txtQtyPlus' />
                                </div>

                                <a class="btn-checkout aProductLink" runat="server" id="aProductLink">add to bag</a>
                                <div class="hidden">
                                    <asp:Literal ID="ltlLink" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="ltlArtikelCode" runat="server" />--%>

    <%--                    <div class="row artikel">
                        <span class="nr">artikelnummer: </span>
                    </div>

                    <div class="row info-contact">
                        <a href="tel:" runat="server" id="aTel"><i class="icon-call-out"></i>Contact opnemen over dit product</a>
                        <a href="javascript:window.print()" ><i class="icon-printer"></i>Pagina afdrukken</a>
                    </div>--%>

    <%--                    <div class="heading" runat="server" visible="false" id="liProductDescription">
                        <b>product description</b>

                        <div runat="server" id="liProductDescriptionTab">
                            <asp:Literal ID="ltlProductDescription" runat="server" />
                        </div>
                    </div>

                    <div runat="server" visible="false" id="liAdditionalInformation">
                        <b>additional information</b>

                        <div class="tab-pane" visible="false" runat="server" id="liAdditionalInformationTab">
                            <asp:Literal ID="ltlAdditionalInformation" runat="server" />
                        </div>
                    </div>

                    <div runat="server" visible="false" id="liDocumentation">
                        <b>documentation</b>

                        <div class="tab-pane" visible="false" runat="server" id="liDocumentationTab">
                            <asp:Literal ID="ltlDocumentation" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>--%>
    <%--<asp:Repeater ID="repPictures" runat="server">
                            <ItemTemplate>
                                <div runat="server" id="divItem" class="productImage">
                                    <a href="<%# Eval("sOriginal").ToString().Replace("~/", sURL()) %>" data-lightbox="product">
                                        <img class="img-responsive" src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>' />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>--%>


    <%--<div class="productImage item">
                                <a href="/Resources/img/jurk1.png" data-lightbox="product">
                                    <i class="fa fa-search" aria-hidden="true"></i>
                                    <img class="img-responsive" src="/Resources/img/jurk1.png" />
                                    <div class="price">€ 34,95</div>
                                </a>
                            </div>

                            <div class="productImage item">
                                <a href="/Resources/img/jurk1.png" data-lightbox="product">
                                    <i class="fa fa-search" aria-hidden="true"></i>
                                    <img class="img-responsive" src="/Resources/img/jurk1.png" />
                                    <div class="price">€ 34,95</div>
                                </a>
                            </div>

                            <div class="productImage item">
                                <a href="/Resources/img/jurk1.png" data-lightbox="product">
                                    <i class="fa fa-search" aria-hidden="true"></i>
                                    <img class="img-responsive" src="/Resources/img/jurk1.png" />
                                    <div class="price">€ 34,95</div>
                                </a>
                            </div>

                            <div class="productImage">
                                <a href="/Resources/img/jurk1.png" data-lightbox="product">
                                    <i class="fa fa-search" aria-hidden="true"></i>
                                    <img class="img-responsive" src="/Resources/img/jurk1.png" />
                                    <div class="price">€ 34,95</div>
                                </a>
                            </div>--%>
    <%--                        <asp:Repeater ID="repPictures" runat="server">
                            <ItemTemplate>
                                <div runat="server" id="divItem" class="productImage">
                                    <a href="<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>" data-lightbox="product">
                                        <img class="img-responsive" src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>' />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>--%>

    <%--<div class="divThumbs thumbs owl-carousel" id="divThumbs" runat="server">--%>
    <%--<div class="item">
                            <img class="img-responsive" src="/Resources/img/jurk1.png" />
                        </div>
                        <div class="item">
                            <img class="img-responsive" src="/Resources/img/jurk1.png" />
                        </div>
                        <div class="item">
                            <img class="img-responsive" src="/Resources/img/jurk1.png" />
                        </div>
                        <div class="item">
                            <img class="img-responsive" src="/Resources/img/jurk1.png" />
                        </div>--%>
    <%--                        <asp:Repeater runat="server" ID="repThumbs">
                            <ItemTemplate>
                                <div class="item">
                                    <img runat="server" id="imgThumb" src='<%# Eval("sOriginal").ToString().Replace("~/", sURL()) %>' class="img-responsive" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>--%>



    <%--                <div id="arrow">
                    <a class="arrow-product smoothscroll hidden-xs" href="#slider">
                        <i class="fa fa-chevron-down movingTopToBottom" aria-hidden="true"></i>
                    </a>
                </div>--%>


    <%--            </div>--%>
    <%--            <div class="productRow">
                <div class="owl-carousel productSliderLarge">
                    <asp:Repeater ID="repImagesBig" runat="server">
                        <ItemTemplate>
                            <div class="productLargeImage">
                                <img class="img-responsive" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "Original") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>--%>

    <%--            <div class="row related" runat="server" id="divRelated">
                <div class="col-sm-12 ">
                    <div class="main">--%>
    <%--<asp:Literal runat="server" Text='<%$ Resources:Resource, Related %>' />--%>
    <%--                    </div>
                </div>
                <asp:Repeater ID="repRelated" runat="server">
                    <ItemTemplate>

                        <div class="col-md-3">
                            <div class="item">
                                <a runat="server" id="aFollow">
                                    <img id="imgThumb" class="img-responsive" runat="server" />
                                    <span class="title">
                                        <asp:Literal runat="server" Text='<%# Eval("sArtikel")%>' /></span>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>


    <%--    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                  <div class="modal-dialog">
                    <div class="modal-content modalProduct">
                      <div class="modal-body">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <div class="clearfix"></div>
                        <div class="col-xs-12 paddingless">
                            <h4 class="productTitle">Added to cart</h4><br /> <br />
                        </div>
                        <div class="col-sm-4 col-xs-6">
                            <img class="img-responsive" runat="server" id="imgBasket"/>
                        </div>
                        <div class="col-sm-8 col-xs-6 paddingless">
                            <strong>Item:</strong> <br />
                            <asp:Literal ID="ltlBasketArtikel" runat="server" /><br /><br />
                            <strong>Size:</strong> <br />
                            <span id="spBasketSize"></span><br /><br />
                            <a href="/shopping-cart" class="btn btn-default goCart">Shopping cart</a>
                            <a href="/checkout" class="btn btn-default btn-black goCart">To checkout</a>
                        </div>
                          <div class="clearfix"></div>
                      </div>
                      <div class="modal-footer">
                      </div>
                    </div>
                  </div>
                </div>--%>

    <%--            <div class="arrow">
                <a href="#slider">
                    <i class="fa fa-angle-double-down" aria-hidden="true"></i>
                </a>
            </div>--%>
    <%--        </div>--%>

    <%--            <div class="widget-owl-gallery divWidgetOwlprev full-slider" id="slider">
                <asp:Repeater runat="server" ID="repPicturesBig">
                    <ItemTemplate>
                        <div runat="server" id="divItem" class="productImage">
                            <div class="item"><img runat="server" id="imgThumb" src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>' class="img-responsive"/></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>

    <%--        <div class="hearts"></div>
    </section>--%>

    <asp:HiddenField ID="hidArtikel" runat="server" />
    <asp:HiddenField ID="hidArtikelID" runat="server" />
    <asp:HiddenField ID="hidArtikelVariantID" runat="server" />

    <%--        <div class="bottom-arrow">
            <div class="controllIndicator animateWB">
                <i class="fa fa-long-arrow-left"></i>
                <i class="fa fa-long-arrow-right"></i>
                <div class="clearfix"></div>
                <strong>Drag</strong>
            </div>
        </div>--%>
</asp:Content>
