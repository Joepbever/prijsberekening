<%@ Page Language="VB" MasterPageFile="~/Errors/Error.Master" AutoEventWireup="false" CodeFile="403.aspx.vb" Inherits="Registreren" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <h4>403 - Forbidden: Access is denied.</h4>
        <p>You do not have permission to view this directory or page using the credentials that you supplied.</p>
        <a href="/" class="btn btn-default btn-white">Terug naar website</a>
    </div>
</asp:Content>