<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="bestellen-stappen.aspx.vb" Inherits="Bestellen_stappen" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="checkout">
        <div class="container">


            <div class="row">
                <div class="col-8 d-flex justify-content-between">
                    <h4 class="section_lbl">1.Bag</h4>
                    <asp:HyperLink id="aWinkelwagen" runat="server" Text="Edit bag"/>
                </div>
            </div>

            <%-- Winkelwagen --%>
            <div class="row">
                <asp:Repeater ID="repWinkelwagen" ClientIDMode="AutoID" runat="server">
                    <ItemTemplate>
                        <div class="col-8 d-flex justify-content-between">
                            <div class="col">
                                <img src='<%# Eval("sSmall").ToString().Replace("~/", sURL()) %>' class="img-fluid" />
                            </div>
                            <div class="col">
                                <asp:Literal ID="Literal1" Text='<%# Eval("sArtikel") & "<br /> Size: " & Eval("sOmschrijving")%>' runat="server" />
                                <asp:Literal ID="ltlArtikelID" Visible="false" Text='<%# Eval("iArtikelID")%>' runat="server" />
                                <asp:Literal ID="ltlFacRglID" Visible="false" Text='<%# Eval("iFacRglID")%>' runat="server" />
                            </div>
                            <div class="col">
                                <label class="pull-right">
                                    <asp:Literal ID="Literal28" Text='<%# CInt(Eval("iAantal"))%>' runat="server" />
                                </label>
                            </div>
                            <div class="col">
                                &euro;
                                <asp:Literal ID="Literal4" Text='<%# Eval("iBedrag")%>' runat="server" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="row">
                <div class="col-8 d-flex justify-content-end">
                    <ul class="col-3">
                        <li>
                            <asp:Literal Text="<%$ Resources:Resource, Subtotaal %>" runat="server" />
                            &euro;<asp:Literal ID="ltlBedragEx" runat="server" />
                        </li>
                        <li id="divKortingTonen" runat="server">
                            <asp:Literal Text="discount" runat="server" />
                            &euro;<asp:Literal ID="ltlDiscount" runat="server" />
                        </li>
                        <li>
                            <asp:Literal Text="shipping costs" runat="server" />
                            &euro;<asp:Literal ID="ltlVerzendkosten" Text="0,00" runat="server" />
                        </li>
                        <li>
                            <asp:Literal Text="total" runat="server" />
                            &euro;<asp:Literal ID="ltlTotaalInclVerzenden" Text="0,00" runat="server" />
                        </li>
                        <li>
                            <asp:Literal Text="including VAT" runat="server" />
                            &euro;<asp:Literal ID="ltlInclVat" Text="0,00" runat="server" />
                        </li>
                    </ul>
                </div>
            </div>
            <%-- /Winkelwagen --%>


            <%-- Kotringscode --%>
            <div class="row" id="divKorting" runat="server">
                <div class="col-8">
                    <div class="from-inline">
                        <div class="form-group">
                            <asp:Label Text="I have a discount code" runat="server" />
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtKortingscode" />
                            <div id="divDiscountMsg" runat="server" visible="false">
                                <asp:Literal ID="ltlDiscountMsg" runat="server" />
                            </div>
                        </div>
                        <asp:Button Text="submit" CssClass="btn btn-underline" UseSubmitBehavior="false" OnClientClick="return isDiscountValid()" ID="btnKortingscode" runat="server" />
                    </div>
                </div>
            </div>
            <%-- /Kotringscode --%>

            <%-- Pesonal gegevens --%>
            <div class="row">
                <div class="col-12">
                    <h4 class="section_lbl">
                        <asp:Literal Text="<%$ Resources:resource, TitleInfo %>" runat="server" />
                    </h4>
                </div>

                <div class="form-group col-6">
                    <asp:Label ID="Literal3" Text="<%$ Resources:Resource, formFirstName %>" runat="server" />
                    <asp:TextBox CssClass="form-control" ID="txtFirstName" runat="server" />
                </div>

                <div class="form-group col-6">
                    <asp:Label ID="Literal4" Text="<%$ Resources:Resource, formSurname %>" runat="server" />
                    <asp:TextBox CssClass="form-control" ID="txtSurname" runat="server" />
                </div>

                <div class="form-group col-6">
                    <asp:Label Text="Phone number" runat="server" />
                    <asp:TextBox ID="txtPhone" CssClass="form-control" onKeyPress="return isGetal()" TextMode="Phone" runat="server" />
                </div>

                <div class="form-group col-6">
                    <asp:Label ID="Literal5" Text="<%$ Resources:Resource, formEmail %>" runat="server" />
                    <asp:TextBox ID="txtEmail" CssClass="form-control" TextMode="Email" runat="server" />
                </div>
            </div>
            <%-- /Pesonal gegevens --%>


            <%-- Comapny --%>
            <div class="row" id="divCompany" runat="server">
                <div class="form-group col-6">
                    <asp:Label Text="Company" runat="server" />
                    <asp:TextBox ID="txtCompany" CssClass="form-control " runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="Btw nummer" runat="server" />
                    <asp:TextBox ID="txtVat" CssClass="form-control " runat="server" />
                </div>
            </div>

            <%-- Comapny --%>

            <%-- Adres gegevens --%>
            <div class="row">
                <div class="col-12">
                    <h4 class="section_lbl">
                        <asp:Literal Text="<%$ Resources:resource, Adress %>" runat="server" />
                    </h4>
                </div>

                <div class="form-group col-6">
                    <asp:Label Text="<%$ Resources:Resource, formZipCode %>" runat="server" />
                    <asp:TextBox ID="txtZipCode" CssClass="form-control txtZipCode" runat="server" />
                </div>

                <div class="form-group col-6">
                    <asp:Label ID="Literal21" Text="<%$ Resources:Resource, formAddress %>" runat="server" />
                    <asp:TextBox ID="txtAddress" CssClass="form-control txtAddress" runat="server" />
                </div>

                <div class="form-group col-3">
                    <asp:Label Text="<%$ Resources:Resource, FormHouseNr %>" runat="server" />
                    <asp:TextBox ID="txtHouseNr" CssClass="form-control txtHouseNr adres" TextMode="Phone" onkeypress="return isGetal();" runat="server" />
                </div>

                <div class="form-group col-3">
                    <asp:Label Text="<%$ Resources:Resource, formAddition %>" runat="server" />
                    <asp:TextBox ID="txtAdd" CssClass="form-control number" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label ID="Literal22" Text="<%$ Resources:Resource, formResidence %>" runat="server" />
                    <asp:TextBox ID="txtResidence" CssClass="form-control txtResidence" runat="server" />
                </div>

                <div class="form-group col-6">
                    <asp:Label Text="country" runat="server" />
                    <asp:DropDownList ID="ddlLand" CssClass="form-control ddlCountry" DataTextField="sLandNaam" DataValueField="iLandID" runat="server">
                    </asp:DropDownList>
                </div>

            </div>
            <%-- /Adres gegevens --%>

            <div class="row">
                <div class="checkbox">
                    <asp:CheckBox Text="<%$ Resources:Resource, AnderAdres %>" ID="cbAnderAdres" CssClass="cbOtherAddress" runat="server" />
                </div>
                <div class="checkbox" id="divCbAccount" runat="server">
                    <asp:CheckBox Text="Make an account?" ID="cbAccount" CssClass="cbOtherAddress" runat="server" />
                </div>
            </div>

            <%-- Login --%>

            <div class="row d-none" id="divAccount" runat="server">
                <div class="form-group col-6">
                    <asp:Label ID="Literal6" Text="<%$ Resources:Resource, formPassword %>" runat="server" />
                    <asp:TextBox ID="txtNewPasswordOrder" CssClass="form-control txtPassword" TextMode="Password" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label ID="Literal2" Text="<%$ Resources:Resource, formPasswordConfirm %>" runat="server" />
                    <asp:TextBox ID="txtNewPasswordConfirmOrder" CssClass="form-control txtPasswordConfirm" TextMode="Password" runat="server" />
                </div>
                <div class="col-12">
                    <a runat="server" id="aInloggen" href="/login?from=checkout">You already have an account? Log in here</a>
                </div>
            </div>

            <%-- /Login --%>

            <%-- Verzend adres --%>
            <div class="row d-none" id="DivDiffrent">
                <div class="col-12">
                    <h4 class="section_lbl">
                        <asp:Literal Text="Verzend adres" runat="server" />
                    </h4>
                </div>
                <div class="form-group col-12">
                    <asp:Label Text="Other information" runat="server" />
                    <asp:TextBox ID="txtAddressline2" CssClass="form-control" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="<%$ Resources:Resource, formFirstName %>" runat="server" />
                    <asp:TextBox ID="txtFirstName2" CssClass="form-control txtFirstName2" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label ID="Literal11" Text="<%$ Resources:Resource, formSurname %>" runat="server" />
                    <asp:TextBox ID="txtSurname2" CssClass="form-control" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="country" runat="server" />
                    <asp:DropDownList ID="ddlLand2" CssClass="form-control ddlCountry2" DataTextField="sLandNaam" DataValueField="iLandID" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="<%$ Resources:Resource, formZipCode %>" runat="server" />
                    <asp:TextBox ID="txtZipCode2" CssClass="form-control txtZipCode2" runat="server" />
                </div>
                <div class="form-group col-3">
                    <asp:Label Text="<%$ Resources:Resource, FormHouseNr %>" runat="server" />
                    <asp:TextBox ID="txtHouseNr2" CssClass="form-control txtHouseNr2" onkeypress="return isGetal(event.which);" TextMode="Phone" runat="server" />
                </div>
                <div class="form-group col-3">
                    <asp:Label ID="Literal14" Text="<%$ Resources:Resource, formAddition %>" runat="server" />
                    <asp:TextBox ID="txtAdd2" CssClass="form-control number " runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label ID="Literal15" Text="<%$ Resources:Resource, formAddress %>" runat="server" />
                    <asp:TextBox ID="txtAddress2" CssClass="form-control txtAddress2" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label ID="Literal16" Text="<%$ Resources:Resource, formResidence %>" runat="server" />
                    <asp:TextBox ID="txtResidence2" CssClass="form-control txtResidence2" runat="server" />
                </div>
                <div class="form-group col-6">
                    <asp:Label ID="Literal17" Text="<%$ Resources:Resource, formExtraFieldOrInformation %>" runat="server" />
                    <asp:TextBox ID="txtAddressline22" CssClass="form-control" runat="server" />
                </div>
                <div class="form-group col-12">
                    <asp:Label ID="Literal7" Text="<%$ Resources:Resource, Opmerking1 %>" runat="server" />
                    <asp:TextBox ID="txtOpmerking" TextMode="MultiLine" CssClass="form-control" placeholder="" runat="server" />
                </div>
            </div>

            <%-- /Verzend adres --%>



            <%-- Betaalmethoden --%>
            <div class="row">
                <div class="col-12">
                    <h4 class="section_lbl">
                        <asp:Literal ID="Literal23" Text="<%$ Resources:Resource, TitlePayment %>" runat="server" />
                    </h4>
                </div>
                <div class="form-group col-6">
                    <asp:RadioButtonList ID="rblBetaalmethode" CssClass="checkbox .rblBetaalmethode" DataTextField="sTitle" DataValueField="sTitle" runat="server">
                    </asp:RadioButtonList>
                </div>

                <div class="form-group col-6">
                    <asp:DropDownList ID="ddlBank" CssClass="form-control land2" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <%-- /Betaalmethoden --%>
            <div class="row">
                <div class="col-6" id="divVerzendMethode" runat="server">
                    <h4 class="section_lbl">
                        <asp:Literal Text="Verzendmethode" runat="server" />
                    </h4>
                    <asp:RadioButtonList ID="rblVerzendmethode" CssClass="checkbox rblVerzendmethode" DataTextField="sArtikel" DataValueField="iArtikelID" runat="server">
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">
                    <div class="checkbox checkbox-primary TermsCheck">
                        <input runat="server" id="terms" type="checkbox" />
                        <label for="terms">Yes, I agree with the <a href="algemene-voorwaarden" runat="server" id="aAV" target="_blank">general terms and conditions</a></label>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Literal ID="ltlInfo" runat="server" />
                <asp:Button ID="btnOrder" UseSubmitBehavior="false" OnClientClick="return isGegegevensValid()" CssClass="btn btn-underline pull-right" Text="<%$ Resources:Resource, Betalen %>" runat="server" />
            </div>
        </div>
    </section>

    <asp:HiddenField runat="server" ID="hdfProvincie" />
    <asp:HiddenField runat="server" ID="hdfLatitudeX" />
    <asp:HiddenField runat="server" ID="hdfLongitudeY" />

    <asp:HiddenField runat="server" ID="hdfProvincie2" />
    <asp:HiddenField runat="server" ID="hdfLatitudeX2" />
    <asp:HiddenField runat="server" ID="hdfLongitudeY2" />
    <asp:HiddenField runat="server" ID="hidArtikelVerzendID" />
    <asp:HiddenField runat="server" ID="hidBetaalmethode" />

    <asp:HiddenField ID="hidPartijID" Value="" runat="server" />
    <input type="hidden" runat="server" value="0" id="hidLat" />
    <input type="hidden" runat="server" value="0" id="hidLon" />
    <input type="hidden" runat="server" value="0" id="hidLat2" />
    <input type="hidden" runat="server" value="0" id="hidLon2" />

    <input type="hidden" runat="server" value="false" id="hidValidatePostback" />
    <input type="hidden" runat="server" value="true" id="hidAccount" />
    <input type="hidden" runat="server" value="false" id="hidAddress" />
    <input type="hidden" runat="server" value="1" id="hidCheckout" />
</asp:Content>
