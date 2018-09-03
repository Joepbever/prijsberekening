<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ACC-order_history.aspx.vb" MasterPageFile="~/page.master" Inherits="ACC_order_history" %>

<%@ Import Namespace="System.Web.Optimization" %>

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
    <div class="container orders">
        <div class="row padding">
            <div class="col-md-6 offset-sm-0 col-10 offset-xs-1">
                <h5>
                    <asp:Literal Text="<%$ Resources:Resource, ltlWelcome %>" runat="server" />
                    <asp:Literal ID="ltlTitle" runat="server" /></h5>
                <h6>Find here a summary of your account information.</h6>
            </div>
            <div class="col-md-6 offset-sm-0 col-10 offset-xs-1">
                <div class="personal_info">
                    <h5><asp:Literal Text="<%$ Resources:Resource, Contactgegevens %>" runat="server" /></h5>
                    <h6>
                        <asp:Literal ID="ltlName" runat="server" /></h6>
                    <h6>
                        <asp:Literal ID="ltlEmail" runat="server" /></h6>
                    <h6>
                        <asp:Literal ID="ltlAddresLine1" runat="server" /></h6>
                    <h6>
                        <asp:Literal ID="ltlAddresLine2" runat="server" /></h6>
                    <h6>
                        <asp:Literal ID="ltlLand" runat="server" /></h6>
                </div>
                <div>
                    <asp:HyperLink ID="aEdit" CssClass="btn btn-underline" runat="server" Text="<%$ Resources:Resource, editDetails %>"/>
                    <asp:HyperLink ID="aPassword" CssClass="btn btn-underline" runat="server" Text="<%$ Resources:Resource, changePassword %>"/>
                </div>
            </div>
        </div>

        <div class="row justify-content-center">
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col"><asp:Literal Text="<%$ Resources:Resource, ordernumber %>" runat="server" /></th>
                        <th scope="col"><asp:Literal Text="<%$ Resources:Resource, dateOfPurchase3 %>" runat="server" /></th>
                        <th scope="col"><asp:Literal Text="<%$ Resources:Resource, status %>" runat="server" /></th>
                        <th scope="col"><asp:Literal Text="<%$ Resources:Resource, trackingnumber %>" runat="server" /></th>
                        <th scope="col"><asp:Literal Text="<%$ Resources:Resource, Total %>" runat="server" /></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="repOrders">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal Text='<%# Eval("sFactuur").ToString() %>' runat="server" /></td>
                                <td><asp:Literal ID="Literal3" Text='<%# Eval("dDatum", "{0:d}")%>' runat="server" /></td>
                                <td></td>
                                <td></td>
                                <td>&euro; <asp:Literal Text='<%# Eval("iFactuurBedrag")%>' runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
