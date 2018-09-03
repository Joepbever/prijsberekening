<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ACC-login.aspx.vb" MasterPageFile="~/page.master" Inherits="ACC_Login" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="login-temp divGlobalForm container centered">
        <div class="row justify-content-between">
            <div class="col-md-4 ">
                <h3 class="section_lbl">
                    <asp:Literal Text="<%$ Resources:Resource, ltlLogin %>" runat="server" />
                </h3>
                <div class="content inside divInside">
                    <div class="inner">
                        <asp:Literal runat="server" text="<%$ Resources:Resource, login %>" />
                    </div>

                    <div class=" error-message" runat="server" id="divError" visible="false">
                        <asp:Literal runat="server" ID="ltlError" />
                    </div>

                    <div class="form-group first">
                        <label for="txtEmail">
                        <asp:Literal Text="<%$ Resources:Resource, formFirstName %>" runat="server" /></label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" />
                    </div>
                    <div class="form-group">
                        <label for="txtLoginPassword">
                        <asp:Literal ID="Literal6" Text="<%$ Resources:Resource, formPassword %>" runat="server" /></label>
                        <asp:TextBox CssClass="form-control" TextMode="Password" runat="server" ID="txtLoginPassword" />
                    </div>

                    <div class="link float-xs-right">
                        <asp:Button runat="server" ID="btnLogin" UseSubmitBehavior="False" OnClientClick="return isLoginValid()" CssClass="btn btn-underline" Text='<%$ Resources:Resource, login %>' />
                    </div>
                    <a runat="server" id="aForgotPassword" class="forgot">
                        <asp:Literal runat="server" ID="forgotPassword" Text='<%$ Resources:Resource, ForgotPasswordTitle %>' />
                    </a>
                </div>
            </div>
            <div class="col-md-4">
                <asp:Literal id="ltlDesc" runat="server" />
                <asp:HyperLink id="aRegister" CssClass="btn btn-underline" Text="<%$ Resources:Resource, btnRegisterNow %>" runat="server" />
            </div>
        </div>
    </section>

    <div class="NavWhite"></div>
    <div class="HideRedirect"></div>

</asp:Content>
