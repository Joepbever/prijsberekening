<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="ACC-forgot-password.aspx.vb" Inherits="ACC_forgot_password" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container password centered">
        <div class="row">
            <div class="col-xs-12">

                <h4><asp:Literal runat="server" Text='<%$ Resources:Resource, lostPassword %>' /></h4>

                <div class="inner">
                    <asp:Literal ID="ltlForgotPassword" runat="server" />
                </div>

                <div class="error-message" runat="server" id="divError">
                    <asp:Literal runat="server" ID="ltlError" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <div class="form-group first">
                    <label for="txtEmail">
                        <asp:Literal runat="server" Text='<%$ Resources:Resource, formEmail %>' />
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <a runat="server" id="aLogin" class="forgot ">
                    <asp:Label Text="<%$ Resources:Resource, btnBackLogin %>" runat="server" />
                </a>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <asp:Button runat="server" ID="btnRetrievePassword" UseSubmitBehavior="False" CssClass="btn btn-underline-alt reposition" Text='<%$ Resources:Resource, btnSend %>' />
            </div>
        </div>
    </section>
</asp:Content>
