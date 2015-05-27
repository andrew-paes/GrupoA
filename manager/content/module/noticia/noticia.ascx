<%@ Control Language="C#" AutoEventWireup="true" CodeFile="noticia.ascx.cs" Inherits="content_module_noticia_noticia" %>
<script type="text/javascript">
	$(document).ready(function () {

		$("input[id$='txtOrdemApresentacao']").setMask('9999');
	});
</script>
<strong>Título:</strong><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtTitulo" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="300px" CssClass="frmborder" MaxLength="150" />
<br />
<br />
<strong>Integra:</strong><br />
<br />
<ag2:HtmlTextBox runat="server" ID="txtIntegra" />
<br />
<br />
<strong>Autor:</strong><br />
<asp:TextBox runat="server" ID="txtAutor" Width="300px" CssClass="frmborder" MaxLength="100" /><br />
<br />
<br />
<strong>Fonte:</strong><br />
<asp:TextBox runat="server" ID="txtFonte" Width="300px" CssClass="frmborder" MaxLength="250" /><br />
<br />
<strong>Fonte URL:</strong>&nbsp;
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFonteUrl"
	ErrorMessage="URL Inválida" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator><br />
<asp:TextBox runat="server" ID="txtFonteUrl" Width="300px" CssClass="frmborder" MaxLength="512" /><br />
<br />
<strong>Data Noticia:</strong><br />
<ag2:DateField runat="server" ID="txtDataPublicacao" CssClass="frmborder" />
<br />
<br />
<table width="300px">
	<tr>
		<td>
			<strong>Ativa:</strong>
		</td>
		<td>
			<strong>Destaque:</strong>
		</td>
	</tr>
	<tr>
		<td>
			<asp:CheckBox ID="chkAtivo" runat="server" />
		</td>
		<td>
			<asp:CheckBox ID="chkDestaque" runat="server" />
		</td>
	</tr>
</table>
<br />
<br />
<strong>Categoria Notícia:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1"
	ControlToValidate="txtTitulo" ErrorMessage="Campo Obrigatório" />
<br />
<asp:DropDownList ID="ddlCategoria" runat="server" Width="200px" />
<br />
<br />
<strong>Data Exibicao Inicio:</strong><br />
<ag2:DateField runat="server" ID="txtDataExibicaoInicio" CssClass="frmborder" />
<br />
<br />
<strong>Data Exibicao Fim:</strong><br />
<ag2:DateField runat="server" ID="txtDataExibicaoFim" CssClass="frmborder" />
<br />
<asp:CustomValidator runat="server" ID="cvValidarDatasPublicacao" OnServerValidate="cvValidarDatasPublicacao_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator>
<br />
<br />
<!-- Categorias -->
<asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:"></asp:Label>
<br />
<asp:CheckBoxList ID="cblCategorias" runat="server" />
<asp:HiddenField ID="hddNoticiaId" runat="server" Value="0" />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensNoticia/" Editable="true"
	MaxFileLength="1500" ScriptModal="noticiaImagens.aspx" MultiFile="true" TipoArquivo="ALL" />
