<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lijstitems.aspx.vb" MasterPageFile="~/page.master" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="container">
            <div class="col-md-10">
                <div class="news temp2">
                    <div class="container">
                        <div class="row">
                            <asp:Repeater runat="server" ID="repBlog">
                                <ItemTemplate>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="content">
                                            <a runat="server" id="aLink2">
                                                <img src="/Resources/img/newsicon.png" />
                                            </a>
                                            <h5><asp:Literal runat="server" Text='<%# Eval("sTitle") %>' /></h5>
                                            <div class="date"><asp:Literal runat="server" Text='<%#CDate(Eval("dtDatum")).ToString("MMM dd, yyyy") %>' /></div>
                                            <asp:Literal runat="server" Text='<%# Eval("sItem1") %>' />
                                            <a runat="server" id="aLink">Lees meer</a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
              </div>
    </section>
</asp:Content>