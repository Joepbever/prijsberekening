<%@ Page Language="VB" AutoEventWireup="false" EnableSessionState="True" ClientIDMode="Static" EnableViewState="false" CodeFile="Categorie.aspx.vb" Inherits="Data_LeadsOverzicht" %><!DOCTYPE html>
<body>
    <form id="form1" runat="server">
        <div class="selector" id="categories">
            <asp:Repeater EnableViewState="false" ID="repCategorie" runat="server">
                <ItemTemplate>
                    <div runat="server" id="divCategorie" class="filter checkbox-list CBList"> <%-- has-dropdown--%>
                        <h6><asp:Literal Text='<%# Eval("sTitle")%>' runat="server" /></h6>
                        <asp:CheckBoxList id="cblSubCat" AppendDataBoundItems="true" EnableViewState="false" CssClass="options CBOptions" DataValueField="iCatID" DataTextField="sTitle" runat="server">
                            <asp:ListItem Value="all" Text="Alle merken"></asp:ListItem>
                        </asp:CheckBoxList>
                        <div class="show-all divShow" id="divAll" runat="server" visible="false">
                            Toon alle opties
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

         <div class="selector artikelenselector" id="artikelen">
         <%--    Aantal Artikelen: <asp:Literal ID="ltlAantal" runat="server" />--%>
             <asp:Repeater runat="server" ID="repArtikelen">
                <ItemTemplate>
                    <div  class="col-lg-4 col-md-6 col-sm-12 col-xs-12 col-custom product">
                        <a class="item DivProduct JS-Link" id="aLink" runat="server" href="#">
                            <img  class="bg-product BgProduct" id="imgThumb" runat="server"  />
                            <div class="label" id="divNew" runat="server">Nieuw</div>
                            <div class="info">
                               <%-- <span class="brand">Clubland</span>--%>
                                <span class="name"><b><asp:Literal Text='<%# Eval("iArtikelID") & " " & Eval("sArtikel")%>' runat="server" /></b></span>
                                <span class="price" runat="server" visible='<%# bIsOnline %>'><b>€ <asp:Literal runat="server" Text='<%# Eval("iPrijsInkoop") %>' /></b></span>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <div class="selector" id="button">
            <a href="#" id="aDealerButton" runat="server">
                <div class="circle">
                    <img src="/Resources/img/popup_location.png" />
                </div>
                <div class="title btn-seo-video">
                    <span>Vind een <b><asp:Literal ID="ltlDealer" runat="server" /></b><br />dealer bij jou in de buurt</span>
                </div>
            </a>
        </div>
    </form>
</body>