<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="text.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="terms fs1 textpage">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    <div class="inner">
                        <asp:Literal ID="ltlTitle" runat="server" />
                    </div>
                    <div class="content inside divInside">
                        <asp:Literal runat="server" ID="ltlText" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>