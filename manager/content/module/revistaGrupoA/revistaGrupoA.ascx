<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revistaGrupoA.ascx.cs"
	Inherits="content_module_revistaGrupoA_revistaGrupoA" %>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:CustomValidator ID="cvValidarExists" runat="server" OnServerValidate="cvValidarExists_ServerValidate"
	ValidationGroup="1" /><br />
<asp:HiddenField ID="hddRevistaId" runat="server" Value="0" />
<!-- Mês -->
<asp:Label runat="server" ID="lblTitulo" Font-Bold="true" Text="Título:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="250px" CssClass="frmborder" MaxLength="100"></asp:TextBox>
<br />
<br />
<!-- Mês -->
<asp:Label runat="server" ID="lblMesPublicacao" Font-Bold="true" Text="Mês Publicação:"></asp:Label><br />
<asp:TextBox runat="server" ID="txtMes" Width="250px" CssClass="frmborder" MaxLength="100"></asp:TextBox>
<br />
<br />
<!-- Ano -->
<asp:Label runat="server" ID="lblAno" Font-Bold="true" Text="Ano Publicação:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="txtAnoPublicacao"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revAno" runat="server" ControlToValidate="txtAnoPublicacao"
	ErrorMessage="Somente Números" ValidationGroup="1" ValidationExpression="([0-9]+)"></asp:RegularExpressionValidator><br />
<asp:TextBox runat="server" ID="txtAnoPublicacao" Width="70px" CssClass="frmborder"
	MaxLength="4"></asp:TextBox>
<br />
<br />
<!-- Edicao -->
<asp:Label runat="server" ID="lblNumeroEdicao" Font-Bold="true" Text="Ordem:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvNumeroEdicao" runat="server" ControlToValidate="txtNumeroEdicao"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="revNumeroEdicao" runat="server" ControlToValidate="txtNumeroEdicao"
	ErrorMessage="Somente Números" ValidationGroup="1" ValidationExpression="([0-9]+)"></asp:RegularExpressionValidator><br />
<asp:TextBox runat="server" ID="txtNumeroEdicao" Width="70px" CssClass="frmborder"
	MaxLength="10"></asp:TextBox>
<br />
<br />
<!-- Chamada -->
<asp:Label runat="server" ID="lblChamada" Font-Bold="true" Text="Chamada:"></asp:Label>
<br />
<ag2:HtmlTextBox runat="server" ID="txtChamada" />
<br />
<br />
<!-- url -->
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Url da Revista:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtUrlRevista" Width="250px" MaxLength="500" CssClass="frmborder"></asp:TextBox>
&nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUrlRevista"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>&nbsp;
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
    ControlToValidate="txtUrlRevista" ErrorMessage="URL Inválida" ValidationGroup="1" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator>
<br />
<br />
<!-- Controles -->
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<asp:Panel ID="pnlImagemRevista" Visible="false" runat="server">
	<asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Imagem:" /><br />
	<ag2:ListFiles ID="upArquivoImagem" runat="server" TargetFolder="imagensRevista/"
		Editable="true" MaxFileLength="10000" TipoArquivo="IMAGE" />
</asp:Panel>
