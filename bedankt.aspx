<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="bedankt.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="errors bg-error ">
        <div class="overlay">
            <div class="container">
                <div class="content">
                    <asp:Literal ID="ltlBedankt" runat="server" />
                    <a href="/" class="btn btn-default btn-white"><asp:Literal runat="server" Text='<%$ Resources:Resource, bedanktTerug %>' /></a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>