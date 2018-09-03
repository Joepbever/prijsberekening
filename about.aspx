<%@ Page Language="VB" AutoEventWireup="false" CodeFile="about.aspx.vb" MasterPageFile="~/page.master" Inherits="_About" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="about container-fluid">
        <div class="about_title" data-aos="zoom-in">
            <h5>a best friends label</h5>
            <h6>identity / philosophy</h6>
            <h6>&quot;The striking mix of cultures.&quot;</h6>
        </div>
        <div class="about_content relative">
            <div class="row about_section">
                <div class="col-lg-8 float-left">
                    <div class="col-lg-7 col-6 paddingless-m" data-aos="fade-left" data-aos-duration="600"
                        data-aos-once="true">
                        <asp:Literal ID="ltlImgTopLeft" runat="server" />
                    </div>
                    <div class="col-lg-5 col-6 paddingless-m" data-aos="fade-right" data-aos-duration="600"
                        data-aos-once="true">
                        <p class="align-left">
                            <asp:Literal id="ltlTopLeft" runat="server" />
                        </p>
                    </div>
                </div>
            </div>
            <div class="row about_section flow" data-aos="fade-left" data-aos-duration="500"
                data-aos-once="true">
                <div class="col-lg-11 float-right">
                    <div class="col-lg-5 col-6 paddingless-m static">
                        <div class="vertical_bottom">
                            <p class="align-right">
                                <asp:Literal id="ltlTopRight" runat="server" />
                            </p>
                        </div>
                    </div>
                    <div class="col-lg-7 col-6 paddingless-m float-right" id="ScrollImg">
                        <asp:Literal ID="ltlImgScrollImg" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row about_section last">
            <div class="col-lg-8 col-12 float-right">
                <div class="col-lg-5 col-12 static">
                    <div class="full-height" data-aos="fade-left" data-aos-duration="600"
                        data-aos-once="true">
                        <div class="vertical_center">
                            <asp:Literal id="ltlBottom" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-7 col-12 float-right" data-aos="fade-right" data-aos-duration="600"
                    data-aos-once="true">
                    <asp:Literal id="ltlImgBottom" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <img data-aos="zoom-in-up" data-aos-duration="600" data-aos-once="true"
        class="about_background" src="/Resources/img/ninetyfour/about_background.jpg"
        alt="">
</asp:Content>
