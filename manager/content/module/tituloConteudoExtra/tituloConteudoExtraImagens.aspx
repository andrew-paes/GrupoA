<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tituloConteudoExtraImagens.aspx.cs" 
Inherits="content_module_tituloConteudoExtra_tituloConteudoExtraImagens" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../css/ModalFormUploadFile.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../../../js/jquery-1.4.2.min.js"></script>

    <script src="../../../js/ag2/ag2ListFile.js" type="text/javascript"></script>
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        Upload de Arquivo</h1>
    <div style="overflow: auto; height: 180px;">
        <div>
            <div class="divCampo">
                <div runat="server" id="divThumb">
                    <asp:Image ID="imgThumb" Width="122px" Height="90px" Visible="false" runat="server" />
                </div>
                <div>
                    Arquivo
                    <asp:RequiredFieldValidator ID="reqArquivo" runat="server" ControlToValidate="FileUpload1"
                        ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>

                </div>
                <div>
                    <asp:FileUpload ID="FileUpload1" CssClass="frmborder" runat="server" />
                </div>
            </div>
            <div class="divCampo">
                    <table class="style1">
                        <tr>
                            <td>
                                Nome Conteúdo 
                                <asp:RequiredFieldValidator ID="rfvNomeConteudo" runat="server" Display="Dynamic" ControlToValidate="txtNomeConteudo" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator></td>
                            <td>
                                Somente Logado?</td>
                            <td>
                                Restrito Professor?</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtNomeConteudo" CssClass="frmborder" runat="server" Width="150px"  MaxLength="250"/></td>
                            <td>
                                <asp:CheckBox ID="chkSomenteLogado" CssClass="frmborder" runat="server"/></td>
                            <td>
                                <asp:CheckBox ID="chkRestritoProfessor" CssClass="frmborder" runat="server"/></td>
                        </tr>
                    </table>
            </div>
        </div>
    </div>
    <div class="divBarraBotao">
        <div>
            <ag2:Button ID="btnUploadFile" runat="server" Text="Enviar Arquivo" TipoLayout="LARANJA" />
        </div>
        <div class="divBtnCancelar">
            <ag2:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btnCancelarModal"
                ExecuteEventPostback="false" />
        </div>
    </div>
    </form>
</body>
</html>
