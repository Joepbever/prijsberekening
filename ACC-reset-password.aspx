<%@ Page Language="VB" MasterPageFile="~/page.Master" AutoEventWireup="false" CodeFile="ACC-reset-password.aspx.vb" Inherits="Mijn_account" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="password security">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <h4><asp:Literal runat="server" text="<%$ Resources:Resource, ltlSecurityQuestion %>" /></h4>
                    <p><asp:Literal runat="server" id="ltlQuestion" /></p>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="contain">
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtAnswer" placeholder='Answer' />
                        </div>
                        <asp:Button runat="server" id="btnCheckAnswer" OnClientClick="return isAnswerValid()" CssClass="btn btn-green" Text='<%$ Resources:Resource, btnNextStep1 %>' />
                        <div class="error-message loading" runat="server" id="divError">
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Literal runat="server" ID="ltlError" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidUserID" runat="server" />
    <asp:HiddenField ID="hidSSOID" runat="server" />

</asp:Content>