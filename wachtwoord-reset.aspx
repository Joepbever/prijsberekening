<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="wachtwoord-reset.aspx.vb" Inherits="Mijn_account" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="maincontent">
        <div class="container">
            <div class="row">
                <div class="col-md-3 first-item">
                    <asp:Literal runat="server" id="ltlPassword" />
                </div>
                <div class="col-md-9 dealer-sign">
                    <div class="content">
                        <div class="error-message" runat="server" id="divError">
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Literal runat="server" ID="ltlError" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group first">
                            <asp:TextBox CssClass="form-control" runat="server" TextMode="Password" ID="txtNewPassword" placeholder='<%$ Resources:Resource, formNewPassword %>' />
                        </div>
                        <div class="form-group first">
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtNewPasswordConfirm" TextMode="Password" placeholder='<%$ Resources:Resource, formNewPassword %>' />
                        </div>
                        <asp:Button runat="server" id="btnUpdatePassword" UseSubmitBehavior="False" OnClientClick="return isPasswordValid()" CssClass="btn btn-default btn-contacthome btn-dealer" Text='<%$ Resources:Resource, btnRetrievePassword %>' />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidUserID" runat="server" />
    <asp:HiddenField ID="hidSSOID" runat="server" />

</asp:Content>