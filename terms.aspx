<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/page.master" CodeFile="terms.aspx.vb" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
        #hamburger{
            top: 29%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="section clearfix section-kosten font-style-6">
        <div class="container terms">
            <div class="list divScrollSpyList">
                <ul class="list-group">
                    <asp:Repeater ID="repTermsScrollSpy" runat="server">
                        <ItemTemplate>
                            <li class="list-group-item">
                                <a class="smooth-scroll" href="#item<%# Container.ItemIndex %>">
                                    <asp:Literal Text='<%# Eval("sTitle")%>' runat="server" />
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Repeater ID="repTerms" runat="server">
                        <ItemTemplate>
                            <div class="col-md-12 terms_section " id="item<%# Container.ItemIndex %>">
                                <h4>
                                    <asp:Literal Text='<%# Eval("sTitle")%>' runat="server" />
\                                </h4>
                                <p>
                                    <asp:Literal Text='<%# Eval("sDescription")%>' runat="server" />
                                </p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
