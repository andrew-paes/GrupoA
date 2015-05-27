<%@ Control Language="C#" AutoEventWireup="true" CodeFile="clipping.ascx.cs" Inherits="content_module_clipping_clipping" %>
<asp:Label runat="server" ID="lblTitulo" Font-Bold="true" Text="Título:" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtTitulo" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="300px" CssClass="frmborder" MaxLength="150" />
<br />
<br />
<asp:Label runat="server" ID="lblIntegra" Font-Bold="true" Text="Integra:" />
<br />
<ag2:HtmlTextBox runat="server" ID="txtIntegra" />
<br />
<br />
<asp:Label ID="lblAutor" runat="server" Text="Autor:" Font-Bold="true" /><br />
<asp:TextBox runat="server" ID="txtAutor" Width="300px" CssClass="frmborder" MaxLength="100" /><br />
<br />
<br />
<asp:Label runat="server" ID="lblFonte" Font-Bold="true" Text="Fonte:" /><br />
<asp:TextBox runat="server" ID="txtFonte" Width="300px" CssClass="frmborder" MaxLength="250" /><br />
<br />
<asp:Label runat="server" ID="lblFonteUrl" Font-Bold="True" Text="Fonte URL:" />&nbsp;
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFonteUrl"
	ErrorMessage="URL Inválida" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator><br />
<asp:TextBox runat="server" ID="txtFonteUrl" Width="300px" CssClass="frmborder" MaxLength="512" /><br />
<br />
<asp:Label runat="server" ID="lblDataNoticia" Font-Bold="true" Text="Data Notícia:" /><br />
<ag2:DateField runat="server" ID="txtDataPublicacao" CssClass="frmborder" />
<br />
<br />
<table width="300px">
	<tr>
		<td>
			<asp:Label runat="server" ID="lblAtiva" Font-Bold="true" Text="Ativa:" />
		</td>
		<td>
			<asp:Label runat="server" ID="lblDestaque" Font-Bold="True" Text="Destaque:" />
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
<asp:Label runat="server" ID="lblDataExibicaoInicio" Font-Bold="true" Text="Data Exibição Início:" /><br />
<ag2:DateField runat="server" ID="txtDataExibicaoInicio" CssClass="frmborder" />
<br />
<br />
<asp:Label runat="server" ID="lblDataExibicaoFim" Font-Bold="true" Text="Data Exibição Fim:" /><br />
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
<asp:HiddenField ID="hddClippingId" runat="server" Value="0" />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<%--<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensClipping/" Editable="true"
	MaxFileLength="1500" ScriptModal="clippingImagens.aspx" MultiFile="true" TipoArquivo="ALL" />--%>
<asp:HiddenField ID="hddArquivoId" runat="server" Value="0" />
<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensClipping/" Editable="true"
	MaxFileLength="1500" TipoArquivo="ALL" />
