<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contact.aspx.vb" MasterPageFile="~/page.master" Inherits="_Contact" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="contact">
        <div class="contact_background"></div>
        <div class="info_placeholder">
            <div class="contact_info">
                <div class="center">
                    <h5>Contact:</h5>
                    <h6>email: <%: partijEmail %></h6>
                    <h6>phone: <%: partijTelefoon %></h6>
                </div>
            </div>
        </div>
        <div class="connect">
            <h6><a class="closeToggle connect">Connect</a> with us.</h6>
        </div>
        <div class="contact_steps">
            <img class="close-toggle closeToggle" src="/Resources/img/ninetyfour/close-white.png" alt="" />
            <div id="contact1" class="contact_form col-md-8">
                <h5>Whats your name?</h5>
                <div class="form-group">
                    <asp:TextBox placeholder="Last name, First name." CssClass="form-control" runat="server" ID="txtName" />
                </div>
                <h6>Please enter your name</h6>
            </div>
            <div id="contact" class="hidden contact_form col-md-8">
                <h5>Whats your name?</h5>
                <div class="form-group">
                    <asp:TextBox placeholder="Last name, First name." CssClass="form-control" runat="server" ID="txtLeeg" />
                </div>
                <h6>Please enter your name</h6>
            </div>
            <div id="contact2" class="contact_form col-md-8">
                <h5>Whats your email?</h5>
                <div class="form-group">
                    <asp:TextBox placeholder="youremail@mail.com" CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email" />
                </div>
                <h6>Please enter your email</h6>
            </div>
            <div id="contact3" class="contact_form col-md-8">
                <h5>Whats your number?</h5>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPhone" placeholder="06-12345678" />
                </div>
                <h6>Please enter your phone number</h6>
            </div>
            <div id="contact4" class="contact_form col-md-8">
                <h5>Whats your question?</h5>
                <div class="form-group">
                    <asp:TextBox placeholder="Your question" runat="server" ID="txtMessage" CssClass="form-control" />
                </div>
                <h6>Please enter your question</h6>
            </div>
            <div id="contact5" class="contact_form col-md-8">
                <h5>We will connect with you very soon.</h5>
            </div>
            <asp:Button runat="server" CssClass="btn btn-next" ID="btnSubmit1" UseSubmitBehavior="False" OnClientClick="return isContactValid1()" Text='verzenden' />
            <a id="nextStep" class="btn btn-next">Next Step<i class="fa fa-long-arrow-right"></i></a>
        </div>
    </section>
</asp:Content>
