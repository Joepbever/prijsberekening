<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="ACC-new-password.aspx.vb" Inherits="newPassowrd" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="password reset">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Literal runat="server" id="ltlPassword" />
                    <h4><asp:Literal runat="server" text="<%$ Resources:Resource, ltlPasswordReset %>" /></h4>

                    <div class="error-message loading" runat="server" id="divError">
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Literal runat="server" ID="ltlError" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="contain">
                        <div class="form-group">
                            <label><asp:Literal runat="server" text="<%$ Resources:Resource, formNewPassword %>" /></label>
                            <asp:TextBox CssClass="form-control" runat="server" TextMode="Password" ID="txtNewPassword" placeholder='<%$ Resources:Resource, formNewPassword %>' />
                        </div>
                        <div class="form-group">
                            <label><asp:Literal runat="server" text="<%$ Resources:Resource, formPasswordConfirm %>" /></label>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtNewPasswordConfirm" TextMode="Password" placeholder='<%$ Resources:Resource, formNewPassword %>' />
                            <asp:Button runat="server" id="btnUpdatePassword" UseSubmitBehavior="False" OnClientClick="return isPasswordValid()" CssClass="btn btn-green" Text='<%$ Resources:Resource, changePassword %>' />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidUserID" runat="server" />
    <asp:HiddenField ID="hidSSOID" runat="server" />

</asp:Content>