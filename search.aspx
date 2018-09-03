<%@ Page Language="VB" AutoEventWireup="false" CodeFile="search.aspx.vb" MasterPageFile="~/page.master" Inherits="_Default" %>

<%--<%@ Import Namespace="System.Web.Optimization" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
<%--    <link rel="stylesheet" property="stylesheet" type="text/css" href="<%: Styles.Url("~/bundles/CSS-Default") %>" />
    <script type="text/javascript" src="<%: Scripts.Url("~/bundles/JS-Default") %>"></script>--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="container search-result">

        <div class="row fs1">
            <div class="col-xs-12 criteria">
                <asp:Literal runat="server" Text='' /><b><asp:Literal ID="ltlSearch" runat="server" /></b>
            </div>
        </div>

        <div class="row lg-row-i1 md-row-i1 sm-row-i1 xs-row-i1 widget-search-list">
            
            <asp:Repeater runat="server" ID="repProducts">
                <ItemTemplate>
                    <div class="col-xs-12 item">
                        <div class="row">
                            <div class="col-xs-12 fs1">
                                <span class="title">
                                    <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                                </span>
                            </div>
                            <div class="col-xs-12">
                                <span class="title">
                                    <asp:Literal runat="server" Text='<%# Eval("iPrijs") %>' />
                                </span>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="text">
                                    <asp:Literal runat="server" Text='<%# Left(Regex.Replace(Eval("sDescription"), "<.*?>", ""), 300) %>' />
                                </div>
                            </div>
                        </div>

                        <a runat="server" id="aLink" href='<%# Eval("sLink").ToString.Replace("~", "")%>' class="btn">Naar product<asp:Literal runat="server" Text='' /></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Repeater runat="server" ID="repCustomerService">
                <ItemTemplate>
                    <div class="col-xs-12 item">
                        <div class="row">
                            <div class="col-xs-12 fs1">
                                <span class="title">
                                    <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                                </span>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="text">
                                    <asp:Literal runat="server" Text='<%# Left(Regex.Replace(Eval("sDescription"), "<.*?>", ""), 300) %>' />
                                </div>
                            </div>
                        </div>

                        <a runat="server" id="aLink" class="btn"><asp:Literal runat="server" Text='' /></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Repeater runat="server" ID="repBlogs">
                <ItemTemplate>
                    <div class="col-xs-12 item">
                        <div class="row">
                            <div class="col-xs-12 fs1">
                                <span class="title">
                                    <asp:Literal runat="server" Text='<%# Eval("sTitle") %>' />
                                </span>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="text">
                                    <asp:Literal runat="server" Text='<%# Left(Regex.Replace(Eval("sDescription"), "<.*?>", ""), 300) %>' />
                                </div>
                            </div>
                        </div>

                        <a runat="server" id="aLink" class="btn"><asp:Literal runat="server" Text='' /></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


            <asp:Repeater runat="server" ID="repTexts">
                <ItemTemplate>
                    <div class="col-xs-12 item">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="text">
                                    <asp:Literal runat="server" Text='<%# Eval("sText") %>' />
                                    <asp:Literal runat="server" Text='<%# Left(Regex.Replace(Eval("sOutput"), "<.*?>", ""), 300) %>' />
                                </div>
                            </div>
                        </div>

                        <a runat="server" id="aLink" class="btn"><asp:Literal runat="server" Text='' /></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </section>
</asp:Content>