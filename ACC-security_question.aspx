<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="ACC-security_question.aspx.vb" Inherits="ACC_security_question" %>

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
    <section class="container password centered">
        <div class="error-message loading" runat="server" id="divError">
            <asp:Label runat="server" ID="ltlError" />
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" Text="Security question"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlQuestions" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Label runat="server" Text="Your answer"></asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtAnswer" TextMode="SingleLine" placeholder="Antwoord" />
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:TextBox CssClass="form-control" runat="server" ID="txtPassword" type="password" TextMode="SingleLine" placeholder="Password" />
            </div>
        </div>

        <div class="inline-buttons">
            <asp:Button runat="server" CssClass="btn btn-black" ID="btnSave" UseSubmitBehavior="False" OnClientClick="return isSequrityquestionValid()" Text='Verstuur' />
        </div>
    </section>
</asp:Content>
