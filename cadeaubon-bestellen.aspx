<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="cadeaubon-bestellen.aspx.vb" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <section class="locatie-intro speelvormContainer divGlobalForm">
        <div class="container">
            <div class="row">
                <div class="col-sm-12 fs1">
                    <div class="inner">
                        <h5><asp:Literal runat="server" ID="ltlTitle" /></h5>
                    </div>
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
                    <div class="col-md-12 extraPaddBottom"><asp:Literal runat="server" ID="ltlKop1" /></div>
                   
                </div>
                <div class="row">
                     <asp:Repeater ID="repArtikelen" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6 col-xs-6">
                                <div class="bedrag">
                                    <div class="radio radio-bedrag form-group">
                                        <input type="radio" class="form-control" runat="server" name="bedrag" id="rb" />
                                        <label for='<%# "rb" & Eval("iArtikelID") %>' class="betalingBox"><span><asp:Literal Text='<%# Eval("sOmschrijving") %>' runat="server" /></span><i><asp:Literal Text='<%# Eval("sArtikel") %>' runat="server" /></i></label>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="clearfix"></div>
                </div>
            </div>

            <div class="col-md-4 col-sm-12 step current" runat="server" id="divBedrag">
                <div class="row">
                    <div class="col-md-12 extraPaddBottom"><asp:Literal runat="server" ID="ltlKop2" /></div>
                </div>
                <div class="row">
                    <div class="form-group past">
                        <div class="col-md-12 extraPaddBottom">
                            <asp:TextBox class="form-control" id="txtTekst" TextMode="MultiLine" placeholder="<%$ Resources:Resource, formTekstCadeaubon %>" runat="server" />
                        </div>
                    </div> 
                </div>
            </div>

            <div class="col-md-4 col-sm-12 step" id="divCheckout">
                   <div class="row">
                    <div class="col-md-12 extraPaddBottom"><asp:Literal runat="server" ID="ltlKop3" /></div>
                </div>
                <div class="row">
                    <div class="col-md-12 privacy-checkbox">
                        <div class="form-group">
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBox ID="cbxPrivacy" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <asp:Button ID="btnAfronden" CssClass="btn btn-default btn-red" UseSubmitBehavior="false"   runat="server" Text="<%$ Resources:Resource, btnBestelingVoltooien %>" /> 
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

