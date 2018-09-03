<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" MasterPageFile="~/page.master" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="slider" id="slider" style="display:none">
        <asp:Repeater runat="server" ID="repSlider">
            <ItemTemplate>
                <div id="sliderItem" runat="server">
                    <img src="" id="sliderImg" alt="" runat="server" />
                    <%--<asp:Button ID="btnSlider" CommandArgument="test" UseSubmitBehavior="false" Text="something" runat="server" />--%>
                    <h1><%# Eval("sTitle") %></h1>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

        <div class="container" style="display:none">
            <asp:Repeater runat="server" ID="repCategories">
                <ItemTemplate>
                    <div class="col-6">
                        <h1><%# Eval("sTitle") %></h1>
                        <ul>
                            <asp:Repeater runat="server" ID="repArtikelen">
                                <ItemTemplate>
                                    <li>
                                        <a href='<%# Eval("sURL").ToString().Replace("~", "") %>' id="aLink" runat="server"><%# Eval("sArtikel") %></a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    <section class="header">
        <div class="container-fluid header-box">
            <img class="header-img" src="http://via.placeholder.com/1920x400"/>
        </div>
    </section>
    <section class="overview">
        <div class="container-fluid section-boxes">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-12 overview-item">
                         <a href="http://www.shop.zonweringwebwinkel.nu/" target="_blank">
                            <img class="overview-icons"src= "Resources/img/icon1.png"/>
                            <h2>Zonwering</h2>
                            <p>Eenvoudig en snel zonwering berekenen</p>
                        </a>
                    </div>
                    <div class="col-md-3 col-sm-6 col-12 overview-item">
                        <a href="https://kozijnenwebwinkel.nu/" target="_blank">
                            <img class="overview-icons"src= "Resources/img/icon1.png"/>
                            <h2>Kozijnen</h2>
                            <p>Eenvoudig en snel kozijnen berekenen</p>
                        </a>
                    </div>
                    <div class="col-md-3 col-sm-6 col-12 overview-item">
                        <a href="http://www.shop.garagedeurenwebwinkel.nu/" target="_blank">
                            <img class="overview-icons"src= "Resources/img/icon1.png"/>
                            <h2>Garagedeuren</h2>
                            <p>Eenvoudig en snel garagedeuren berekenen</p>
                        </a>
                    </div>
                    <div class="col-md-3 col-sm-6 col-12 overview-item">
                        <a href="http://shop.terrasoverkappingwebwinkel.nu/" target="_blank">
                            <img class="overview-icons"src= "Resources/img/icon1.png"/>
                            <h2>Terrasoverkapping</h2>
                            <p>Eenvoudig en snel terrasoverkappingen berekenen</p>
                        </a>
                    </div>
                    <div style="display:none;">
                    <asp:Literal ID="ltlLiteral1" runat="server" />
                    <asp:Literal ID="ltlLiteral2" runat="server" />
                    </div>
                </div>
        </div>
    </section>
    <section class="prijsberekening">
        <div class="container section-boxes">
            <div class="col-md-12 box-title bt-black">
                <h5>Prijsberekening Meppel</h5>
            </div>
            <div class="col-md-12 box-content bc-black">
                <p>
                    Prijsberekening Meppel biedt u via een Online platform de meest kwalitatieve producten tegen de meest Goedkope prijzen. U kunt bij Prijsberekening Meppel Online terecht voor: Kunststof Kozijnen, Rolluiken, Zonwering, een Garagedeur of Roldeur en een Terrasoverkapping. Onze klanten bevinden zich in de grote regio rond Meppel. Onze Webwinkels geven u de gelegenheid om alle gewenste producten Online samen te stellen en te Bestellen. En ook kunt u er voor kiezen om het inmeten en Monteren ook voor uw rekening te nemen. Mocht u toch liever terugvallen op de vakmensen van Prijsberekening Meppel dan kunnen wij u ook van dienst zijn bij het inmeten en Monteren van alle producten in onze Webshops. 
                    <br /><br />
                    U kunt direct Online Bestellen en afrekenen en zelf aan de slag met de Montage. Maar u kunt ook een extra bedenktijd inlassen door eerst te kiezen om ons een vrijblijvende Offerte naar uw mail te sturen. Dat is een extra service van Prijsberekening Meppel waar onze klanten graag gebruik van maken. Onze klantgerichtheid vinden wij belangrijk en u maximaal van dienst zijn staat voorop. Laat het ons dus gerust weten als u hulp wil bij het Aanschaffen, meten en Monteren van al onze producten. Dus deze service geldt voor een Garagedeur, Roldeur, Terrasoverkapping, Kunststof Kozijnen, Zonwering, Rolluiken enzovoort.   
                </p>
            </div>
            <div class="col-md-12 no-padding row">
                <div class="col-md-6 contact-buttons"><a class="btn btn-default btn-margin btn-white">Ik kom bij u langs <i class="fa fa-angle-double-right"></i></a></div>
                <div class="col-md-6 contact-buttons"><a class="btn btn-default btn-margin btn-white">Offerte aanvragen <i class="fa fa-angle-double-right"></i></a></div>
            </div>
        </div>
    </section>
    <section class="contact">
        <div class="container-fluid section-boxes">
            <div class="col-md-12 box-title bt-white">
                <h5>Contact</h5>
            </div>
            <div class="col-md-12 box-content bc-white">
                <p>
                    Prijsberekening Meppel
                    <br />
                    Oudeweg 8A | 8316 AC  Marknesse  
                    <br />
                    06-51361522
                    <br />
                    info@PrijsberekeningMeppel.nl
                    <br />
                    Bovenstaand adres is geen showroomadres. 
                    <br />
                    Bezoek op afspraak.
                </p>
                <div class="no-padding"><a class="btn btn-default btn-margin"><i class="fa fa-phone fa-rotate-90"></i> Neem contact op</a></div>
            </div>
        </div>
    </section>

    <div class="row" style="display:none">
        <div class="form-inline">
            <div class="form-group">
                <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtNameHome" placeholder='Naam' />
            </div>
            <div class="form-group">
                <asp:TextBox runat="server" type="text" CssClass="form-control" ID="txtEmailHome" placeholder='Email' />
            </div>
            <asp:Button runat="server" ID="btnSubscribeHome" class="btn btn-dark" UseSubmitBehavior="false" Text='Submit' />
            <asp:Literal runat="server" ID="ltlMessage" />
        </div>
    </div>
</asp:Content>

<%-- Tallullabele content --%>

<%--    <section class="slider">
        <div class="container">--%>
<%--<div runat="server" id="divSlider" >
                <asp:Repeater runat="server" ID="repSlider">
                    <ItemTemplate>
                        <div class="item ">
                            <img runat="server" id="imgMobile" class="image image-mobile img-responsive" />
                            <img runat="server" id="imgDesktop" class="image image-desktop img-responsive hidden" />
                            <div class="container">
                                <div class="textslider">
                                    <h5><asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></h5>
                                    <asp:Literal runat="server" Text='<%# Eval("sSubtitle") %>' />
                                    <asp:Literal runat="server" Text='<%# Eval("sDescription") %>' />
                                    <a href='<%# "/" & Eval("sColor").ToString.ToLower %>' type="button">Lees meer</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>

<%--            <div class="owl-carousel widget-owl-carousel divWidgetOwlCarousel">
                <asp:Repeater ID="repSliderCategories" OnItemDataBound="repSliderCategories_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <a id="aCategoryLink" runat="server">
                                <div class="inner">--%>
<%--<img src="<%# "/ui/images/" & Eval("sQueryString") & ".svg" %>" alt="Alternate Text" />--%>
<%--                                        <img id="imgCategorieThumb" runat="server" />
                                    <asp:Label CssClass="textslider" Text='<%# Eval("sTitle").ToString.ToLower %>' runat="server" />
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>--%>

<%--<section class="usp">
        <div class="container-fluid">
            <div class="row">
                <div class="col-xs-3  first item active">
                    <a runat="server" id="aUsp1">
                        <div class="box box1"><img src="/UI/images/usp1.png" /></div>
                        <p><asp:Literal ID="ltlUsp1" runat="server" /></p>
                    </a>
                </div>

                <div class="col-xs-3 item">
                    <a runat="server" id="aUsp2">
                        <div class="box"><img src="/UI/images/usp2.png" /></div>
                        <p><asp:Literal ID="ltlUsp2" runat="server" /></p>
                    </a>
                </div>

                <div class="col-xs-3 item">
                    <a runat="server" id="aUsp3">
                        <div class="box"><img src="/UI/images/usp3.png" /></div>
                        <p ><asp:Literal ID="ltlUsp3" runat="server" /></p>
                    </a>
                </div>

                <div class="col-xs-3 item">
                    <a runat="server" id="aUsp4">
                        <div class="box"><img src="/UI/images/usp4.png" /></div>
                        <p><asp:Literal ID="ltlUsp4" runat="server" /></p>
                    </a>
                </div>
            </div>
        </div>
    </section> --%>

<%--    <section class="collection fs1" id="collection">
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="inner">
                        <asp:Literal ID="ltlFsTitle" runat="server" />
                    </div>
                </div>
                <asp:Repeater ID="repArtikelenFeatured" OnItemDataBound="repArtikelenFeatured_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3 col-sm-6">
                            <div class="item" runat="server" id="divItem">
                                <a runat="server" id="aFollow">
                                    <img runat="server" id="imgThumb" />
                                    <span class="title">
                                        <asp:Literal runat="server" ID="ltlName" />
                                    </span>

                                    <div class="lead">
                                        <span runat="server" id="spPrice" class="price">&euro;
                                            <asp:Literal runat="server" Text='<%# Eval("iPrijs") %>' />
                                        </span>

                                        <span runat="server" id="spanPrice" class="price">
                                            <asp:Literal runat="server" ID="ltlNewPrice" /></span>
                                        <span class="direct" runat="server" id="spVoorraadInfo" visible="false"></span>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>--%>

<%--      <section class="about-us">
      <div class="container-fluid autoHeightContainer">
            <div class="row">
                <div class="col-lg-3 col-sm-6 mrg-bottom parent">
                    <div class="item first-item autoHeightChild">
                        <img src="/UI/images/Talulabelle-versiering-vraagtekenen.png" class="lg-image" />
                        <asp:Literal ID="ltlSubtitleAboutus" runat="server" />
                        <asp:Literal ID="ltlAboutUsText" runat="server" />
                    </div>
                </div>

                <div class="col-lg-6 col-sm-12 parent">
                    <div class="item first-item autoHeightChild">
                        <img src="/UI/images/Talulabelle-versiering-vraagtekenen.png" class="lg-image" />
                        <asp:Literal ID="ltlSubtitleWhatDoTheySayAboutUs" runat="server" />
                        <div class="owl-carousel owl-theme divWidgetReviews">
                            <asp:Repeater ID="repReviews" runat="server" OnItemDataBound="repReviews_ItemDataBound">
                                <ItemTemplate>
                                    <div class="item">
                                        <div class="title">
                                            <asp:Literal runat="server" ID="ltlDatum" />
                                        </div>
                                        <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                                        <asp:Literal runat="server" Text='<%# Eval("sDescription") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <div class="col-lg-3 col-sm-6 mrg-bottom parent last">
                    <div class="item first-item second autoHeightChild">
                        <img src="/UI/images/talulabelle-dress-icon1.png" />
                        <asp:Literal ID="ltlSubtitleWhatDoWeDo" runat="server" />
                        <asp:Literal ID="ltlWhatDoWeDo" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </section> --%>