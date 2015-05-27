<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revistaPagina.ascx.cs"
    Inherits="content_module_revista_pagina" %>
<asp:HiddenField ID="hdnRevistaPaginaId" runat="server" Value="0" />
<strong>Nome da Revista:</strong>
<asp:CustomValidator runat="server" ID="cvValidarRevista" OnServerValidate="cvValidarRevista_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:DropDownList ID="ddlRevista" runat="server" Width="200px" />
<br />
<br />
<strong>Título da Página:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
    ControlToValidate="txtTitulo" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="250px" CssClass="frmborder" MaxLength="100" />
<asp:Panel runat="server" ID="pnlUrl" Visible="false">
    <br />
    <label>Nome da página: <asp:Literal runat="server" ID="ltUrl"></asp:Literal></label>
    [<asp:HyperLink runat="server" ID="hlVisualizar" Target="_blank">Visualizar</asp:HyperLink>]
</asp:Panel>
<br />
<br />
<strong>Texto da Página:</strong>
<asp:CustomValidator runat="server" ID="cvValidarTexto" OnServerValidate="cvValidarTexto_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<ag2:HtmlTextBox runat="server" ID="txtTexto" ToolBar="Full" />
<br />
<br />
<strong>Ativo:</strong>
<br />
<asp:CheckBox ID="chkAtivo" runat="server" />
<br />
<br />
<strong>Exibir no Menu:</strong>
<br />
<asp:CheckBox ID="chkExibirMenu" runat="server" />
<br />
<br />
<strong>Ordem:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
    ControlToValidate="txtOrdem" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtOrdem" Width="20px" CssClass="frmborder" MaxLength="3" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtOrdem']").setMask({ mask: '999', type: '', defaultValue: '' });
    });
</script>
