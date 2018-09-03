<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lijstitem-blog.aspx.vb" MasterPageFile="~/page.master" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="<%: Scripts.Url("~/bundles/JS-lijstitem-blog") %>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Literal runat="server" ID="ltlImgHeader" />

    <asp:Literal runat="server" ID="ltlHtml1" />

    <asp:Literal runat="server" ID="ltlTitleBlog" />
   
</asp:Content>
