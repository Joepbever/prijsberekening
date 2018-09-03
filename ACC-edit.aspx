<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="ACC-edit.aspx.vb" Inherits="ACC_Edit" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
        #hamburger {
            top: 30%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="account divGlobalForm">
        <div class="container account">
            <ul class="fixed_list">
                <asp:Repeater runat="server" ID="repMenu">
                    <ItemTemplate>
                        <li><a class="btn btn-underline" href='<%# "/" & Eval("sCode").ToString.ToLower & "/" & Eval("sQueryString") %>'>
                            <asp:Literal runat="server" Text='<%# Eval("sText") %>' /></a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="row">
                <div class="inside fs1">
                    <div class="col-md-12">
                        <div class="inner">
                            <h5 class="section_lbl">
                                <asp:Literal runat="server" ID="ltlTitle" /></h5>
                        </div>
                    </div>
                    <div class="col-xs-12 buttons">
                        <div class="account-links accounts pull-right">
                        </div>
                    </div>
                    <div class="col-md-12">
                        <b>
                            <asp:Literal runat="server" Text='<%$ Resources:Resource, myInformation %>' /></b>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="needed" for="txtFirstName">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formfirstName %>' /></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtFirstName" />
                                </div>

                                <div class="form-group">
                                    <label class="needed" for="txtSurname">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formsurName %>' /></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtSurname" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="needed" for="txtEmail"><asp:Literal runat="server" Text='<%$ Resources:Resource, Email %>' /></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email" />
                                </div>

                                <div class="form-group">
                                    <label class="needed" for="txtPhone">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formTelephone %>' /></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPhone" onkeypress="return isGetal()" TextMode="Phone" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="col-md-12">
                        <b>
                            <asp:Literal runat="server" Text='<%$ Resources:Resource, shippingAddress %>' /></b>
                        <div class="row">
                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label>
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formResidence%>' /></label>
                                    <asp:DropDownList ID="ddlLand" CssClass="form-control ddlCountry" DataTextField="sLandNaam" DataValueField="iLandID" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div class="col-md-6 txtPostcode1 divZipCode  ">
                                <div class="form-group ">
                                    <label class="needed" for="txtZipCode">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formZipcode %>' /></label>
                                    <asp:TextBox CssClass="form-control txtZipCode" runat="server" ID="txtZipCode" />
                                </div>
                            </div>

                            <div class="col-xs-6 col-md-4  divHouseNr txtNr1 ">
                                <div class="form-group">
                                    <label class="needed" for="txtHouseNr">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formHouseNr %>' /></label>
                                    <asp:TextBox CssClass="form-control txtHouseNr" runat="server" ID="txtHouseNr" onkeypress="return isGetal()" />
                                </div>
                            </div>

                            <div class="col-xs-6 col-md-2 divAdd txtToev1 ">
                                <div class="form-group">
                                    <label class="needed" for="txtAdd">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formAddition %>' /></label>
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtAdd" />
                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label class="needed" for="txtAddress">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formAddress %>' /></label>
                                    <asp:TextBox CssClass="form-control txtAddress" runat="server" ID="txtAddress" />
                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div class="col-md-6 ">
                                <div class="form-group">
                                    <label class="needed" for="txtResidence">
                                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formProvince %>' /></label>
                                    <asp:TextBox CssClass="form-control txtState" runat="server" ID="txtResidence" />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12  shipping">
                            <b>
                                <asp:Literal runat="server" Text='<%$ Resources:Resource, Bezorgadres %>' /></b>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            <asp:Literal runat="server" Text='<%$ Resources:Resource, formResidence%>' /></label>
                                        <asp:DropDownList ID="ddlLand2" CssClass="form-control ddlCountry2" DataTextField="sLandNaam" DataValueField="iLandID" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6  divZipCode2 col-xs-6">
                                    <div class="form-group ">
                                        <label class="needed" for="txtZipCode2">
                                            <asp:Literal runat="server" Text='<%$ Resources:Resource, formZipcode %>' /></label>
                                        <asp:TextBox CssClass="form-control txtZipCode2" runat="server" ID="txtZipCode2" />
                                    </div>
                                </div>
                                <div class="col-md-4  divHouseNr2 col-xs-6">
                                    <div class="form-group">
                                        <label class="needed" for="">
                                            <asp:Literal runat="server" Text='<%$ Resources:Resource, formHouseNr %>' /></label>
                                        <asp:TextBox CssClass="form-control txtHouseNr2" runat="server" ID="txtHouseNr2" onkeypress="return isGetal()" />
                                    </div>
                                </div>
                                <div class="col-md-2  divAdd2">
                                    <div class="form-group">
                                        <label class="needed" for="txtAdd2">
                                            <asp:Literal runat="server" Text='<%$ Resources:Resource, formAddition %>' /></label>
                                        <asp:TextBox CssClass="form-control" runat="server" ID="txtAdd2" />
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6 ">
                                    <div class="form-group">
                                        <label class="needed" for="txtAddress2">
                                            <asp:Literal runat="server" Text='<%$ Resources:Resource, formAddress %>' /></label>
                                        <asp:TextBox CssClass="form-control txtAddress2" runat="server" ID="txtAddress2" />
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-12 ">
                                    <div class="form-group">
                                        <label class="needed" for="txtResidence2">
                                            <asp:Literal runat="server" Text='<%$ Resources:Resource, formProvince %>' /></label>
                                        <asp:TextBox CssClass="form-control txtResidence2" runat="server" ID="txtResidence2" />
                                    </div>
                                </div>

                                <div class="error-message" runat="server" id="divError" visible="false">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <asp:Literal runat="server" ID="ltlError" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div class="col-md-12">
                                <div class="link">
                                    <asp:Button runat="server" ID="btnUpdate" UseSubmitBehavior="False" CssClass="btn btn-underline active" OnClientClick="return isUpdateValid()" Text='<%$ Resources:Resource, btnEdit %>' />
                                    <asp:HyperLink runat="server" ID="aClose" CssClass="btn btn-underline active" Text='<%$ Resources:Resource, btnClose %>' />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hdfProvincie" />
            <asp:HiddenField runat="server" ID="hdfLatitudeX" />
            <asp:HiddenField runat="server" ID="hdfLongitudeY" />
            <asp:HiddenField runat="server" ID="hdfProvincie2" />
            <asp:HiddenField runat="server" ID="hdfLatitudeX2" />
            <asp:HiddenField runat="server" ID="hdfLongitudeY2" />
    </section>

    <asp:HiddenField ID="hidPartijID" runat="server" />
    <input type="hidden" runat="server" value="0" id="hidLat" />
    <input type="hidden" runat="server" value="0" id="hidLon" />
    <input type="hidden" runat="server" value="0" id="hidLat2" />
    <input type="hidden" runat="server" value="0" id="hidLon2" />
    <input type="hidden" runat="server" value="1" id="hidAccountValidation" />

</asp:Content>
