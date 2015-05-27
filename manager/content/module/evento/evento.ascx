<%@ Control Language="C#" AutoEventWireup="true" CodeFile="evento.ascx.cs" Inherits="content_module_evento_evento" %>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:HiddenField ID="hddEventoId" runat="server" Value="0" />
<asp:HiddenField ID="hddEventoArquivoIdDelete" runat="server" Value="0" />
<asp:HiddenField ID="hddEventoArquivoId" runat="server" Value="0" />
<!-- Nome -->
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtNome" Width="250px" CssClass="frmborder" MaxLength="200"></asp:TextBox>
<br />
<br />
<!-- Chamada -->
<asp:Label runat="server" ID="lblResumo" Font-Bold="true" Text="Resumo:"></asp:Label><br />
<asp:TextBox runat="server" ID="txtResumo" Width="350px" TextMode="MultiLine" CssClass="frmborder" MaxLength="512"></asp:TextBox>
<br />
<br />
<!-- Texto -->
<asp:Label runat="server" ID="lblTexto" Font-Bold="true" Text="Texto:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtTexto" Width="350px"  TextMode="MultiLine" CssClass="frmborder"></asp:TextBox>
<br />
<br />
<!-- Local -->
<asp:Label runat="server" ID="lblLocal" Font-Bold="true" Text="Local:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtLocal" Width="250px" CssClass="frmborder" MaxLength="200"></asp:TextBox>
<br />
<br />
<table width="500px">
	<tr>
		<td>
			<asp:Label runat="server" ID="lblDataEventoInicio" Font-Bold="true" Text="Data de Início do Evento:" />
		</td>
		<td>
			<asp:Label runat="server" ID="lblDataEventoFim" Font-Bold="true" Text="Data de Fim do Evento:" /><br />
		</td>
	</tr>
	<tr>
		<td>
			<ag2:DateField runat="server" ID="txtDataEventoInicio" CssClass="frmborder" />
		</td>
		<td>
			<ag2:DateField runat="server" ID="txtDataEventoFim" CssClass="frmborder" />
		</td>
	</tr>
	<tr>
		<td colspan="100%">
			<asp:CustomValidator runat="server" ID="cvValidarDatasEvento" OnServerValidate="cvValidarDatasEvento_ServerValidate"
				ValidationGroup="1"></asp:CustomValidator>
		</td>
	</tr>
</table>
<br />
<!-- Datas Evento -->
<table width="500px">
	<tr>
		<td>
			<asp:Label runat="server" ID="lblDataPublicacaoInicio" Font-Bold="true" Text="Data Início da Publicação:" /><br />
		</td>
		<td>
			<asp:Label runat="server" ID="lblDataPublicacaoFim" Font-Bold="true" Text="Data Fim de Publicação:" /><br />
		</td>
	</tr>
	<tr>
		<td>
			<ag2:DateField runat="server" ID="txtDataPublicacaoInicio" CssClass="frmborder" />
		</td>
		<td>
			<ag2:DateField runat="server" ID="txtDataPublicacaoFim" CssClass="frmborder" />
		</td>
	</tr>
	<tr>
		<td colspan="100%">
			<asp:CustomValidator runat="server" ID="cvValidarDatasPublicacao" OnServerValidate="cvValidarDatasPublicacao_ServerValidate"
				ValidationGroup="1"></asp:CustomValidator>
		</td>
	</tr>
</table>
<br />
<!-- Fonte -->
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Fonte:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtFonte" Width="250px" CssClass="frmborder" MaxLength="250"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Fonte URL:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtFonteURL" Width="250px" CssClass="frmborder" MaxLength="512"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFonteURL"
	ErrorMessage="URL Inválida" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"></asp:RegularExpressionValidator>
<br />
<br />
<asp:Label runat="server" ID="lblTotalAlertas" Font-Bold="true" Text="Total de Alertas:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtTotalAlertas" Width="20px" Enabled="false" CssClass="frmborder"
	MaxLength="512"></asp:TextBox>
<br />
<br />
<!-- Ativa e Destaque -->
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

<strong>Exibe formulário de contato</strong><br />
<asp:CheckBox ID="chkExibeFormulario" runat="server" /><br /><br />
<!-- Categorias -->
<asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:"></asp:Label>
<br />
<asp:CheckBoxList ID="cblCategorias" runat="server" />
<!-- Controles -->
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensEvento/" Editable="true"
	MaxFileLength="5000" TipoArquivo="IMAGE" />
