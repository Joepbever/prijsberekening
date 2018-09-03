<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="shopthelook.aspx.vb" Inherits="ShopTheLook" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="lookbook fs1">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3">
                    <div class="inner">
                        <h5>Shop the look</h5>
                    </div>
                </div>

                <div class="col-sm-10 col-sm-offset-1">
                    <ul class="button-group filters-button-group" id="grid-items">
                         <asp:Repeater ID="repCategorie" runat="server">
                            <ItemTemplate>
                                <li class="item button" runat="server" id="liCategorie" data-filter='<%# "." & Eval("iCatID") %>'><img runat="server" id="imgThumb" class="img-responsive"/></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>

                <div id="shopItems"></div>

                <div class="col-sm-10 col-sm-offset-1 right-side ">
                    <div class="grid" >
                        <asp:Repeater runat="server" ID="repArtikelen" OnItemDataBound="repArtikelen_ItemDataBound1">
                            <ItemTemplate>
                                <div class='<%# "col-lg-6 element-item " & Eval("iCatID") %>' runat="server" id="divContent">
                                    <div class="row shop-item">
                                        <div class="col-sm-12">
                                            <div class="text"><asp:Literal runat="server" Text='<%# Eval("sArtikel") %>' /></div>
                                            <div class="price">
                                                <span runat="server" id="spPrice">&euro; <asp:Literal runat="server" Text='<%# Eval("iPrijs") %>' /></span><span class="price"><asp:Literal runat="server" id="ltlNewPrice" /></span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="item">
                                                <a runat="server" href='<%# Eval("sURL") %>' class="image">
                                                    <img runat="server" id="imgThumb" src="/UI/images/product_details.png" />
                                                </a> 
                                            </div>
                                        </div>
                                        <div class="col-sm-8 discription">
                                            <div class="sizes">
                                                <asp:Repeater ID="repArtikelVarianten" runat="server" OnItemDataBound="repArtikelVarianten_ItemDataBound1">
                                                    <ItemTemplate>
                                                            <div class="checkboxSizes JS-size">
                                                                <input type="radio" runat="server" value='<%# Eval("iArtikelVariantID")%>' name="sizes" id="rb" />
                                                                <label runat="server" for='<%# "rb_" & Eval("sWaarde") & "_" & iCount2.ToString() %>'><asp:Literal Text='<%# Eval("sWaarde")%>' runat="server" /></label> 
                                                            </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <a class="btn aProductLink shop-button" data-id='<%# Eval("iArtikelID") %>'>
                                                    <asp:Literal runat="server" text='<%$ Resources:Resource, AddToCart %>' />
                                                    <span class="hearthover"><i class="fa fa-heart" aria-hidden="true"></i></span>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="hearts"></div>
    
    <asp:HiddenField ID="hidCatID" runat="server" />
    <asp:HiddenField ID="hidArtikel" runat="server" />
    <asp:HiddenField ID="hidArtikelID" runat="server" />
    <asp:HiddenField ID="hidArtikelVariantID" runat="server" />
    <asp:HiddenField ID="hidTaalID" runat="server" />
</asp:Content>