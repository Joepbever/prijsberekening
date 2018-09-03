<%@ Page Language="VB" AutoEventWireup="false" CodeFile="factuur_footer.aspx.vb" EnableViewState="false" Inherits="pdf_Factuur_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style>

        body, html {
            font-family: 'Arial', sans-serif, sans-serif !important;
            background: #FFF;
            padding-top: 50px;
            padding-bottom: 50px;
            font-size: 12px;
            margin-bottom:0px;
            padding:0px;
        }
        .align-center {
            text-align:center;
        }
    
    </style>
    <form id="form1" runat="server">
        <div class="container">

            <div class="col-md-12 align-center">
               <asp:Literal ID="ltlID" runat="server" />
            </div>
        </div>

    </form>
</body>
</html>
