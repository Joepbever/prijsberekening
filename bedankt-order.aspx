<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="bedankt-order.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
        #hamburger div{
            background-color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="bedankt">
        <div class="container-fluid position">
            <div class="col-md-6 left">
                <h5>Thank you for your order!</h5>
                <h6>You will get an email soon</h6>
                <div class="row">
                    <div class="col-xs-12">
                        <a href="#" class="btn btn-underline">MY Account</a>
                        <a href="#" class="btn btn-underline">My Orders</a>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="customer col-xs-6 col-md-12">
                    <h6>NIKKI VAN TRIER</h6>
                    <h6>Bredaseweg 106</h6>
                    <h6>4902 NS Oosterhout</h6>
                    <h6>Noord-Brabant Netherlands</h6>
                </div>
                <div class="company col-xs-6 col-md-12">
                    <h6>NINETYFOUR</h6>
                    <h6>info@ninety-four.com</h6>
                    <h6>06 47 32 88 33</h6>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
