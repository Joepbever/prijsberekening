<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="ACC-change_password.aspx.vb" Inherits="ACC_ChangePassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
        <style>
        #hamburger {
            top: 30%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="fixed_list">
        <asp:Repeater runat="server" ID="repMenu">
            <ItemTemplate>
                <li><a class="btn btn-underline" href='<%# "/" & Eval("sCode").ToString.ToLower & "/" & Eval("sQueryString") %>'>
                    <asp:Literal runat="server" Text='<%# Eval("sText") %>' /></a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <section class="container password centered">

        <div class="row">
            <div class="col-xs-12">
                <h4><asp:Literal Text="<%$ Resources:Resource, changePassword %>" runat="server" /></h4>
                <asp:Literal ID="ltlTekst" runat="server" />
                <asp:Literal ID="subtitle" runat="server" />
                <asp:Label runat="server" ID="ltlError"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <asp:Label Text="<%$ Resources:Resource, oldPassword %>" runat="server" />
                    <asp:TextBox runat="server" ID="txtOldPassword" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <asp:Label Text="<%$ Resources:Resource, formNewPassword %>" runat="server" />
                    <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <asp:Label Text="<%$ Resources:Resource, formNewPasswordConfirm %>" runat="server" />
                    <asp:TextBox runat="server" ID="txtNewPasswordRepeat" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <asp:Button runat="server" ID="btnCancel" Text="<%$ Resources:Resource, btnCancel %>" CssClass="btn btn-underline-alt" />
                    <asp:Button runat="server" ID="btnSavePassword" Text="<%$ Resources:Resource, btnSave %>" OnClientClick="return isChangePasswordValid()" CssClass="btn btn-underline-alt" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
