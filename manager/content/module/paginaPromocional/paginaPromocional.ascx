<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paginaPromocional.ascx.cs"
	Inherits="content_module_paginaPromocional_paginaPromocional" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtLarguraArquivo']").setMask({ mask: '999', type: 'reverse', defaultValue: '' });
        $("input[id$='txtAlturaArquivo']").setMask({ mask: '999', type: 'reverse', defaultValue: '' });
    });
</script>
<label>
	<b>Nome da Página: (www.grupoa.com.br/site/nome-pagina)</b></label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNomePagina"
	Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1" />
<br />
<asp:TextBox runat="server" ID="txtNomePagina" Width="200px" CssClass="frmborder"
	MaxLength="100"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNomePagina"
    ErrorMessage="Campo possui caracteres especiais ou espaços em brancos." ValidationGroup="1" ValidationExpression="[a-zA-Z0-9_-]+"></asp:RegularExpressionValidator>
<asp:Panel runat="server" ID="pnlUrl" Visible="false">
    <br />
    <label>Url da página promocional: <asp:Literal runat="server" ID="ltUrl"></asp:Literal></label>
    [<asp:HyperLink runat="server" ID="hlVisualizar" Target="_blank">Visualizar</asp:HyperLink>]
</asp:Panel>
<br />
<br />
<strong>Ativo:</strong>
<br />
<asp:CheckBox ID="chkAtivo" runat="server" />
<br />
<br />
<label>
	<b>Título da Página:</b></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTituloPagina"
	Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1" />
<br />
<asp:TextBox runat="server" ID="txtTituloPagina" Width="230px" CssClass="frmborder"
	MaxLength="128"></asp:TextBox>
<br />
<br />
<label>
	<b>Subtítulo da Página:</b></label>
<br />
<asp:TextBox runat="server" ID="txtSubtituloPagina" Width="200px" CssClass="frmborder"
	MaxLength="256"></asp:TextBox>
<br />
<br />
<label>
	<b>Resumo:</b></label>
<ag2:HtmlTextBox runat="server" ID="HtmlTextoResumo" />
<br />
<br />
<label>
	<b>Link URL:</b></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLinkURL"
	Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtLinkURL" Width="200px" CssClass="frmborder" MaxLength="256"></asp:TextBox>
<br />
<br />
<strong>Abrir o link em uma nova janela:</strong>
<br />
<asp:CheckBox ID="chkTargetBlank" runat="server" />
<br />
<br />
<asp:Panel ID="pnlArquivo" runat="server" Visible="false">
    <label>
	    <b>Largura do Arquivo:</b></label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLarguraArquivo"
	    Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox runat="server" ID="txtLarguraArquivo" Width="40px" CssClass="frmborder" MaxLength="3"></asp:TextBox>
    <asp:CustomValidator runat="server" ID="cvValidarLargura" OnServerValidate="cvValidarLargura_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
    <br />
    <br />
    <label>
	    <b>Altura do Arquivo:</b></label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAlturaArquivo"
	    Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox runat="server" ID="txtAlturaArquivo" Width="40px" CssClass="frmborder" MaxLength="3"></asp:TextBox>
    <asp:CustomValidator runat="server" ID="cvValidarAltura" OnServerValidate="cvValidarAltura_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
    <br />
    <br />
    <label>
	    <b>Imagem:</b></label>
    <asp:HiddenField ID="hddArquivoId" runat="server" Value="0" />
    <ag2:ListFiles ID="upArquivoImagem" runat="server" TargetFolder="paginaPromocional/"
	    Editable="true" MaxFileLength="10000" TipoArquivo="ALL" />
    <br />
    <br />
</asp:Panel>
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" />
</div>

