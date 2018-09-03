<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="social-update.aspx.vb" Inherits="_Default" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .grid:after {
            content: '';
            display: block;
            clear: both;
        }

        .grid-sizer {
            width: 33.33333333%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="update">
        <div class="container-fluid ">
            <div class="row ">
                <div class="col-xs-12 fs1">
                    <div class="inner">
                        <asp:Literal ID="Titel" runat="server" />
                    </div>

                    <ul class="button-group filters-button-group">
                        <li class="is-checked button" data-filter=".facebook">Facebook</li>
                        <li class="button" data-filter=".instagram">Instagram</li>
                    </ul>
                </div>

                <div class="grid ">
                <div class="grid-sizer"></div>
                <asp:Repeater ID="repFacebookPosts" runat="server">
                    <ItemTemplate>
                        <div class='<%# "col-md-4 col-sm-6 element-item " & Eval("sType")%>'>
                                <div class="item">
                                            <img src='<%# Eval("ImageUrl")%>' class="img-responsive" />
                                        <div class="content">
                                            <img src="/ui/images/logo-social.png" class="logo" />
                                            <p>
                                                <b>Talulabelle</b>
                                                <span class="date"><%# CDate(Eval("PostDate")).ToLongDateString%></span>

                                                <span><%# Eval("PostMessage")%></span>
                                                <b class="likes"><%# Eval("PostLikes")%></b>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                    </ItemTemplate>
                </asp:Repeater>

                </div>
            </div>
        </div>
    </section>
</asp:Content>