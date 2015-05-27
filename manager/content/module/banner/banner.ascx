<%@ Control Language="C#" AutoEventWireup="true" CodeFile="banner.ascx.cs" Inherits="content_module_banner_banner" %>
<asp:HiddenField ID="hddBannerId" runat="server" Value="0" />
<asp:HiddenField ID="hddBannerArquivoIdDelete" runat="server" Value="0" />
<asp:HiddenField ID="hddBannerArquivoId" runat="server" Value="0" />
<asp:HiddenField ID="hddBannerArquivoNome" runat="server" Value="0" />
<strong>Nome Banner:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtNomeBanner" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNomeBanner" Width="300px" CssClass="frmborder"
	MaxLength="50" />
<br />
<br />
<strong>Ativo:</strong>
<asp:CheckBox ID="chkAtivo" runat="server" />
<br />
<br />
<strong>Target Blank:</strong>
<asp:CheckBox ID="chkTargetBlank" runat="server" />
<br />
<br />
<strong>Tempo de Exibição (em segundos):</strong>
<br />
<asp:TextBox runat="server" ID="txtTempoExibicao" Width="30px" CssClass="frmborder" MaxLength="3" Text="10" />
<asp:RequiredFieldValidator ID="rfvTempoExibicao" runat="server" ControlToValidate="txtTempoExibicao" ErrorMessage="Preenchimento obrigatório" SetFocusOnError="true" ValidationGroup="1" />
<br />
<br />
<strong>Data Exibição Inicio:</strong><br />
<ag2:DateField runat="server" ID="txtDataExibicaoInicio" CssClass="frmborder" />
<br />
<br />
<strong>Data Exibição Fim:</strong>
<br />
<ag2:DateField runat="server" ID="txtDataExibicaoFim" CssClass="frmborder" />
<br />
<asp:CustomValidator runat="server" ID="cvValidarDatasPublicacao" OnServerValidate="cvValidarDatasPublicacao_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator>
<br />
<br />
<strong>Fonte URL:</strong>&nbsp;
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFonteUrl"
	ErrorMessage="URL Inválida" ValidationGroup="1" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator>
<br />
<asp:TextBox runat="server" ID="txtFonteUrl" Width="300px" CssClass="frmborder" MaxLength="1024" />
<br />
<br />
<asp:Label runat="server" ID="lblBannerArea" Font-Bold="true" Text="Área:" Visible="false"></asp:Label><br />
<asp:CheckBoxList ID="cblBannerArea" runat="server" CellPadding="1" CellSpacing="1"/>
<asp:CustomValidator runat="server" ID="cvBannerArea" ValidationGroup="1"></asp:CustomValidator>
<br />
<br />
<strong>
	<asp:Label ID="lblArquivoBanner" runat="server" Text="Arquivo Banner:" Visible="false" /></strong>&nbsp;<br />
<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensBanner/" Editable="true"
	MaxFileLength="5000" TipoArquivo="ALL" />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
