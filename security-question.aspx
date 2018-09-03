<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="security-question.aspx.vb" Inherits="ACC_securityquestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div>
           <asp:Label runat="server" ID="ltlError"></asp:Label>
           <br />
            <asp:Label runat="server" Text="Edit details" Font-Size="Large"></asp:Label>
            <br />
            <asp:Label runat="server" Text="Change security question" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server" Text="Security question*" Font-Size="Small"></asp:Label>
            <br />
            <asp:DropDownList runat="server" ID="ddlQuestions">
                <asp:ListItem Text="The maidens name of your mother" />
                <asp:ListItem Text="The name of your last pet" />
                <asp:ListItem Text="The name of your favorite celebrity" />
                <asp:ListItem Text="The brand of your first car" />
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label runat="server" Text="Your answer"></asp:Label>
            <div class="row">
                <div class="col-xs-12">
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtAnswer" TextMode="SingleLine" placeholder="Antwoord" />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:Button runat="server" CssClass="btn" ID="btnCancel" UseSubmitBehavior="False" Text="Cancel" OnClick="btnCancel_Click"/>
            <asp:Button runat="server" CssClass="btn" ID="btnSave" UseSubmitBehavior="False" OnClientClick="return isSecurityQuestionValid()" Text='Verstuur' />
        </div>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">  

</asp:Content>

