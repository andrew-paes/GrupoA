<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlteraSenha.aspx.cs" Inherits="content_AlteraSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery-ui-personalized-1.6rc2.packed.js"></script>

    <script type="text/javascript" src="../js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="../js/jquery.meio.mask.js"></script>

    <script type="text/javascript" src="../js/default.js"></script>

    <script type="text/javascript" src="../js/sifr.js"></script>

    <script type="text/javascript" src="../js/forms.js"></script>

    <script type="text/javascript" src="../js/custom.js"></script>

    <script type="text/javascript" src="../js/swfobject.js"></script>

    <script type="text/javascript" src="../js/ui/i18n/ui.datepicker-pt-BR.js"></script>

    <script type="text/javascript" src="../js/progress.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#txtSenhaAtual").focus();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divForm" style="border: 5px solid #cccccc; padding: 10px; height: 225px;">
                <div id="statusMessage" class="ManagerMessage">
                </div>
                <br />
                Senha Atual
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSenhaAtual"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtSenhaAtual" TextMode="Password" CssClass="frmborder" runat="server"
                    Width="340px"></asp:TextBox>
                <br />
                <br />
                Nova Senha
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNovaSenha"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtSenhaAtual"
                    ControlToValidate="txtNovaSenha" Display="Dynamic" ErrorMessage="A nova senha não pode ser igual a senha atual"
                    Operator="NotEqual"></asp:CompareValidator>
                <br />
                <asp:TextBox ID="txtNovaSenha" TextMode="Password" CssClass="frmborder" runat="server"
                    Width="340px"></asp:TextBox>
                <br />
                <br />
                Confirmação de Senha
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmacaoSenha"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNovaSenha"
                    ControlToValidate="txtConfirmacaoSenha" Display="Dynamic" ErrorMessage="A confirmação de senha não confere"></asp:CompareValidator>
                <br />
                <asp:TextBox ID="txtConfirmacaoSenha" TextMode="Password" CssClass="frmborder" runat="server"
                    Width="340px"></asp:TextBox>
                <br />
                <br />
                <asp:ImageButton ID="btnEnviar" runat="server" ImageUrl="~/img/btn_executar.gif"
                    OnClick="btnEnviar_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
