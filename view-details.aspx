<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="view-details.aspx.vb" Inherits="ACC_view_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<section class="container">
    <div class="row">
        <div class="col-md-10 col-sm-6">
            <div id="divDetails">
                <asp:Label runat="server" Text="Personal details"></asp:Label>
                <br />
                <br />
                <asp:Label runat="server" Text="Full Name: "></asp:Label>
                <asp:Label runat="server" ID="lblName" Text="<Player's full name>"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Date of birth: "></asp:Label>
                <asp:Label runat="server" ID="lblDateOfBirth" Text="<Player's date of birth>"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Adress: "></asp:Label>
                <asp:Label runat="server" ID="lblAdress1" Text="<Player's adress line 1>"></asp:Label>
                <br />
                <asp:Label runat="server" ID="lblAdress2" Text="<Player's adress line 2>"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Zip code & City"></asp:Label>
                <asp:Label runat="server" ID="lblZipcodeCity" Text="<Player's zip code & city>"></asp:Label>
                <br />
                <asp:Label runat="server" Text="State & Country"></asp:Label>
                <asp:Label runat="server" ID="lblStateCountry" Text="<Player's state & country>"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Mobile number"></asp:Label>
                <asp:Label runat="server" ID="lblMobile" Text="<Player's mobile number>"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Email"></asp:Label>
                <asp:Label runat="server" ID="lblEmail" Text="<Player's email address>"></asp:Label>
                <br />
                <asp:Button runat="server" ID="btnEditDetails" Text="Edit details" UseSubmitBehavior="false" OnClick="btnEditDetails_Click" />
            </div>

            <br />
            <br />

            <div id="Security">
                <asp:Label runat="server" Text="Security"></asp:Label>
                <br />
                <asp:Button runat="server" ID="btnChangePassword" Text="Change password" OnClick="btnChangePassword_Click" UseSubmitBehavior="false"></asp:Button>
                <asp:Button runat="server" ID="btnSecurity" Text="change security question" OnClick="btnSecurity_Click" UseSubmitBehavior="false" />
            </div>

            <br />
            <br />

            <div id="CloseAccount">
                <asp:Label runat="server" Text="Account closure"></asp:Label>
                <br />
                <asp:Label runat="server" Text="If you want to close your Unibel account you can do so here"></asp:Label>
                <br />
                <asp:Button runat="server" ID="btnCloseAccount" Text="Close account" OnClick="btnCloseAccount_Click" UseSubmitBehavior="false" />
            </div>

        </div>
    </div>
</section>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>