<%@ Page Language="VB" AutoEventWireup="false" CodeFile="stocklist.aspx.vb" MasterPageFile="~/page.master" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container stocklist centered">
        <div class="row">
            <asp:Literal ID="ltlAdressen1" runat="server" />
        </div>
        <div class="row">
            <asp:Literal ID="ltlAdressen2" runat="server" />
        </div>
        <div class="row">
            <asp:Literal ID="ltlAdressen3" runat="server" />
        </div>
        <div class="row">
            <asp:Literal ID="ltlAdressen4" runat="server" />
        </div>
    </section>
</asp:Content>
