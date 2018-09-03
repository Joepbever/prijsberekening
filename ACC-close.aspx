<%@ Page Title="" Language="VB" MasterPageFile="~/page.master" AutoEventWireup="false" CodeFile="ACC-close.aspx.vb" Inherits="ACC_CloseAccount" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="my-account close-account">
        <div class="container">

            <div class="row title">
                <div class="col-xs-12">
                    <h4><asp:Label runat="server" Text="<%$ Resources:Resource, CloseAccount %>"></asp:Label></h4>
                    <div class="error-message loading" runat="server" id="divError">
                        <asp:Label runat="server" ID="ltlError" />
                    </div>
                </div>
            </div>
            <asp:Literal ID="PageTitle" runat="server" />
            <h4>
                <asp:Label runat="server" Text="<%$ Resources:Resource, BeAware %>"></asp:Label></h4>

            <div>
                <asp:Literal id="ltlDescription" runat="server" />
            </div>

            <div class="form-group">
                <asp:DropDownList ID="ddlReasons" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="inline-buttons">
                <asp:HyperLink ID="aCancel" runat="server" CssClass="btn btn-black" Text="<%$ Resources:Resource, Cancel%>"/>
                <button type="button" class="btn btn-black" data-toggle="modal" data-target="#AreYouSure">
                    <asp:Label Text="<%$ Resources:Resource, CloseAccount %>" runat="server" />
                </button>
            </div>

            <div class="modal fade" id="AreYouSure" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" id="exampleModalLabel"><asp:Literal runat="server" Text="<%$ Resources:Resource, ltlZekerVraag%>"></asp:Literal></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Literal id="ltlCloseText" runat="server" />
                        </div>
                        <div class="modal-footer inline-buttons">
                            <asp:Button Text="Close account" ID="btnClose" UseSubmitBehavior="false" runat="server" CssClass="btn btn-green" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
            </div>
        </div>
    </section>
</asp:Content>
