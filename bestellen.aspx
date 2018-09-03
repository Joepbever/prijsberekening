<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="bestellen.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="/scripts/bestellen.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <section class="locatie-intro speelvormContainer">
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <h5><asp:Literal runat="server" ID="ltlTitle" /></h5>
                </div>
            <div runat="server" id="divMessage" class="col-md-12 alert alert-danger alert-dismissible hide-message col-md-9" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label ID="lblMessage" runat="server" />
            </div>

            <div class="col-md-12">
                <asp:Literal runat="server" ID="kadobonTekst" />
            </div>

            <div class="clearfixPadd"></div>

            <div class="col-md-4 col-sm-12 step past" runat="server" id="divGegegevens">
                <div class="row">
                    <div class="col-md-12 extraPaddBottom"><h3>1 - vul uw persoonsgegevens in of log in met Facebook</h3></div>
            
                    <div class="col-md-12">
                       
                        <div id="status">
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-12 extraPaddBottom">
                            <label for="txtVoornaam" class="">Voornaam*</label>
                            <input type="text" class="form-control" runat="server" id="txtVoornaam" />
                        </div>
                    </div> 
            
                    <div class="form-group">
                        <div class="col-md-12 extraPaddBottom">
                            <label for="txtAchternaam" class="req">Achternaam*</label>
                            <input type="text" class="form-control" runat="server" id="txtAchternaam" />
                        </div>
                    </div> 
            
                    <div class="form-group">
                        <div class="col-md-5 extraPaddBottom">
                            <label for="txtPostcode" class="req">Postcode*</label>
                            <input type="text" class="form-control"  id="txtPostcode" runat="server" />
                        </div>

                        <div class="col-md-4 extraPaddBottom">
                            <label for="txtNr" class="req">Nr*</label>
                            <input type="text" onkeypress="return isGetal(event.which);" class="form-control adres" id="txtNr" runat="server" />
                        </div>
                
                        <div class="col-md-3 extraPaddBottom">
                            <label for="txtToev" class="req">Toev.</label>
                            <input type="text" class="form-control" id="txtToev" runat="server" />
                        </div>
                    </div> 
            
                    <div class="form-group">
                        <div class="col-md-12 extraPaddBottom">
                            <label for="txtStraatnaam" class="req">Adres*</label>
                            <input type="text" class="form-control"  id="txtStraatnaam" runat="server" />
                        </div>
                    </div> 
            
                     <div class="form-group">
                        <div class="col-md-12 extraPaddBottom">
                            <label for="txtPlaatsnaam" class="req">Plaats*</label>
                            <input type="text" class="form-control"  id="txtPlaatsnaam" runat="server" />
                        </div>
                    </div> 

                    <div class="form-group">
                        <div class="col-md-12 extraPaddBottom">
                            <label for="txtEmail" class="req">E-mail*</label>
                            <input type="text" class="form-control"  id="txtEmail" runat="server" />
                        </div>
                    </div> 
                    <div class="col-md-12"><p>Velden met een (*) zijn verplicht om in te vullen</p></div>
                </div>
            </div>

            <div class="col-md-4 col-sm-12 step current" runat="server" id="divBedrag">
                <div class="row">
                    <div class="col-md-12 extraPaddBottom"><h3>​2 - kies een bedrag of tekst die op de kadobon komt te staan. </h3></div>
                    <asp:Repeater ID="repArtikelen" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6 col-xs-6">
                                <div class="bedrag">
                                    <div class="radio radio-bedrag" hidden="hidden">
                                        <input type="radio" runat="server" name="bedrag" id="rb" />
                                        <label for='<%# "rb" & Eval("iArtikelID") %>' class="betalingBox"><span><asp:Literal Text='<%# Eval("sOmschrijving") %>' runat="server" /></span><i><asp:Literal Text='<%# Eval("sArtikel") %>' runat="server" /></i></label>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="clearfix"></div>
                </div>
            </div>

            <div class="col-md-4 col-sm-12 step" id="divCheckout">
                <div class="row">
                    <div class="col-md-12 extraPaddBottom"><h3>3 - kies een betaalmethode</h3></div>

                      <asp:Repeater ID="repBetaalmethodes" runat="server">
                        <ItemTemplate>
                            <div class="col-xs-6">
                                <div class="betaling">
                                    <label for='<%# "rb" & Eval("sID") %>'  class="betalingBox"><img class="img-responsive" runat="server" src='<%# Eval("sImage") %>' /></label>
                                    <div class="radio radio-primary">
                                        <input type="radio" name="betalingsmogelijkheid" runat="server" id="rb" /><label  for='<%# "rb" & Eval("sID") %>' runat="server"></label>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="clearfix extraPaddBottom"></div>

                    <div class="col-md-12 gegevens extraPaddBottom bankInp">
                        <p>Bank</p>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlBank" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="clearfix"></div>

                    <div class="col-md-12 privacy-checkbox">
                        <div class="form-group">
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBox ID="cbxPrivacy" runat="server" Text="Ik ga akkoord met de <a href='/privacy-verklaring' target='_blank'>privacy statement.</a>" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <asp:Button ID="btnAfronden" CssClass="btn btn-default btn-red" UseSubmitBehavior="false" OnClientClick="return isBestellenValid()"  runat="server" Text="Bestelling voltooien" /> 
                    </div>
            
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        </div>
        <asp:HiddenField ID="hidArtikelID" runat="server" />
        <asp:HiddenField ID="hidBetaalmethode" runat="server" />
        
        <asp:HiddenField ID="hidLat" runat="server" />
        <asp:HiddenField ID="hidLon" runat="server" />

    </section>

</asp:Content>

