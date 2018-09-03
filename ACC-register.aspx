<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ACC-register.aspx.vb" MasterPageFile="~/page.master" Inherits="_Register" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="container register centered">
        <div class="row">
            <div class="col-xs-12">
                <h3 class="section_lbl">
                    <asp:Literal Text="<%$ Resources:Resource, createAccount %>" runat="server" />
                </h3>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="content inside divInside">
                    <div class="inner">
                        <asp:Literal runat="server" text="<%$ Resources:Resource, ltlRegister %>" />
                    </div>
                    <div class="error-message" runat="server" id="divError">
                        <asp:Literal runat="server" ID="ltlError" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 paddingless">
                <div class="form-group first">
                    <label for="txtFirstName">
                        <asp:Literal ID="Literal3" Text="<%$ Resources:Resource, formFirstName %>" runat="server" />
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtFirstName" />
                </div>
            </div>

            <div class="col-sm-12 paddingless">
                <div class="form-group first">
                    <label for="txtSurname">
                        <asp:Literal ID="Literal4" Text="<%$ Resources:Resource, formSurname %>" runat="server" />
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtSurname" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 paddingless">
                <div class="form-group">
                    <label for="txtEmail">
                        <asp:Literal ID="Literal5" Text="<%$ Resources:Resource, formEmail %>" runat="server" />
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" TextMode="Email" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6 paddingless">
                <div class="form-group">
                    <label for="txtPassword">
                        <asp:Literal ID="Literal6" Text="<%$ Resources:Resource, formPassword %>" runat="server" />
                    </label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPassword" TextMode="password" />
                </div>
            </div>

            <div class="col-sm-6 paddingless">
                <div class="form-group">
                    <label for="txtPasswordConfirm">
                        <asp:Literal ID="Literal7" Text="<%$ Resources:Resource, formPasswordConfirm %>" runat="server" /></label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtPasswordConfirm" TextMode="password" />
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-sm-6 paddingless">
                <div class="form-group">
                    <label for="txtPassword">
                        <asp:Literal ID="Literal1" Text="<%$ Resources:Resource, SecurityQuestion %>" runat="server" />
                    </label>
                     <asp:DropDownList ID="ddlSecurity" CssClass="form-control" runat="server">

                     </asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-6 paddingless">
                <div class="form-group">
                    <label for="txtPasswordConfirm">
                        <asp:Literal ID="Literal2" Text="<%$ Resources:Resource, YourAnswer %>" runat="server" /></label>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtAnswer" />
                </div>
            </div>
        </div>

        <div class="row align">
            <div class="col-xs-12">
                <div class="form-group cb pull-left">
                    <div class="checkbox">
                        <asp:CheckBox runat="server" ID="cbTerms" />
                    </div>
                </div>

                <asp:Button runat="server" ID="btnRegister" UseSubmitBehavior="False" OnClientClick="return isRegisterValid()" CssClass="btn btn-underline-alt pull-right" Text='<%$ Resources:Resource, createAccount %>' />
            </div>
        </div>
    </section>
</asp:Content>
