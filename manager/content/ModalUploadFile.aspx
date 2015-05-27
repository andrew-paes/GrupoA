<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModalUploadFile.aspx.cs"
    Inherits="content_ModalUploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divConteudo">
        <div>
            <h2>
                Upload de Arquivo</h2>
        </div>
        <div style="margin-top: 10px;">
            <div>
                Arquivo
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="FileUpload1"
                    runat="server" Display="Dynamic" ErrorMessage="Campo obrigatório"></asp:RequiredFieldValidator>
            </div>
            <asp:FileUpload ID="FileUpload1" Width="410px" CssClass="classeUpload" BackColor="#cccccc"
                BorderStyle="Solid" BorderWidth="1px" BorderColor="#333333" runat="server" />
            <asp:Button ID="btnEnviar" runat="server" BackColor="#cccccc" BorderStyle="Solid"
                BorderWidth="1px" BorderColor="#333333" CssClass="btnEnviar" Text="Enviar Arquivo"
                Height="18px" />
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function() {

            $('.classeUpload').change(function() {

                $('.btnEnviar').click();

            });

        });
    </script>

</body>
</html>
