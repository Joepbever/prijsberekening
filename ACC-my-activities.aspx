<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="ACC-my-activities.aspx.vb" Inherits="oih_my_activities" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
        #hamburger {
            top: 30%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="my-account my-activities">
        <div class="container">
            <div class="row title">
                <ul class="fixed_list">
                    <asp:Repeater runat="server" ID="repMenu">
                        <ItemTemplate>
                            <li><a class="btn btn-underline" href='<%# "/" & Eval("sCode").ToString.ToLower & "/" & Eval("sQueryString") %>'>
                                <asp:Literal runat="server" Text='<%# Eval("sText") %>' /></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="col-xs-12">
                    <asp:Literal ID="preferences" runat="server" />
                    <h4>My activities</h4>

                    <div id="divError" runat="server" class="error-message loading">
                        <asp:Label ID="ltlError" runat="server" />
                    </div>
                </div>
            </div>
            <div class="content">
                <div class="row">
                    <asp:Literal ID="ltltitle" runat="server" />
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col"><asp:Literal runat="server" text="<%$ Resources:Resource, ltllogindate %>" /></th>
                                <th scope="col"><asp:Literal runat="server" text="<%$ Resources:Resource, ltllogoutdate %>" /></th>
                                <th scope="col"><asp:Literal runat="server" text="<%$ Resources:Resource, ltlipaddress %>" /></th>
                                <th scope="col"><asp:Literal runat="server" text="<%$ Resources:Resource, ltlbrowser %>" /></th>
                                <th scope="col"><asp:Literal runat="server" text="<%$ Resources:Resource, ltlOperatingSystem %>" /></th>
                                <th scope="col"><asp:Literal runat="server" text="<%$ Resources:Resource, ltlactie %>" /></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="repActivity">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("dtLogin") %>
                                        </td>

                                        <td>
                                            <%# Eval("dtLogout") %>
                                        </td>

                                        <td>
                                            <%# Eval("sIpAddress") %>
                                        </td>

                                        <td>
                                            <%# Eval("sBrowser") & Eval("sBrowserVersion") %>
                                        </td>

                                        <td>
                                            <%# Eval("sOperatingSystem") %>
                                        </td>

                                        <td>
                                            <%# Eval("sActie") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
