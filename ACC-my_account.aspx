<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="ACC-my_account.aspx.vb" Inherits="ACC_my_account" %>

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
                    <div class="col-md-12">
                        <ul>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, FirstName %>" runat="server" />
                                <asp:Literal ID="txtFirstName" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, LastName %>" runat="server" />
                                <asp:Literal ID="txtSurname" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, Email %>" runat="server" />
                                <asp:Literal ID="txtEmail" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, PhoneNumber %>" runat="server" />
                                <asp:Literal ID="txtPhone" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ZipCode %>" runat="server" />
                                <asp:Literal ID="txtZipCode" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ltlHuisNr %>" runat="server" />
                                <asp:Literal ID="txtHouseNr" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ltlToev %>" runat="server" />
                                <asp:Literal ID="txtAdd" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ltlStraat %>" runat="server" />
                                <asp:Literal ID="txtAddress" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, City %>" runat="server" />
                                <asp:Literal ID="txtResidence" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, Land %>" runat="server" />
                                <asp:Literal ID="ddlLand" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ZipCode %>" runat="server" />
                                <asp:Literal ID="txtZipCode2" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ltlHuisNr %>" runat="server" />
                                <asp:Literal ID="txtHouseNr2" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ltlToev %>" runat="server" />
                                <asp:Literal ID="txtAdd2" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, ltlStraat %>" runat="server" />
                                <asp:Literal ID="txtAddress2" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, City %>" runat="server" />
                                <asp:Literal ID="txtResidence2" runat="server" />
                            </li>
                            <li>
                                <asp:Literal Text="<%$ Resources:Resource, Land %>" runat="server" />
                                <asp:Literal ID="ddlLand2" runat="server" />
                            </li>
                        </ul>
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
