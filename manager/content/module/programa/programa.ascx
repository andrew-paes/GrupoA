<%@ Control Language="C#" AutoEventWireup="true" CodeFile="programa.ascx.cs" Inherits="content_module_programa" %>
<asp:HiddenField ID="hddCursoPanamericanoId" runat="server" Value="0" />
<asp:HiddenField ID="hddCursoPanamericanoArquivoIdDelete" runat="server" Value="0" />
<asp:HiddenField ID="hddCursoPanamericanoArquivoId" runat="server" Value="0" />
<asp:HiddenField ID="hddCursoPanamericanoArquivoNome" runat="server" Value="0" />
<strong>Título do curso:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtTituloCurso" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtTituloCurso" Width="300px" CssClass="frmborder"
	MaxLength="50" />
<br />
<br />
<strong>Subtítulo do curso:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
	ControlToValidate="txtSubTituloCurso" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtSubTituloCurso" Width="300px" CssClass="frmborder"
	MaxLength="50" />
<br />
<br />

<strong>Descrição:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
	ControlToValidate="txtDescricao"  ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtDescricao"  textmode="MultiLine" Width="600px" CssClass="frmborder"
	MaxLength="50" />
<br />
<br />
<strong>Abrir em outra página:</strong>
<asp:CheckBox ID="chkTargetBlank" runat="server" />
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
<asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:" Visible="false"></asp:Label><br />
<div style="height:250px; overflow:auto">
<asp:CheckBoxList ID="cblCategorias" runat="server" />
<asp:CustomValidator runat="server" ID="cvCategorias" ValidationGroup="1"></asp:CustomValidator>
</div>
<strong>
	<asp:Label ID="lblArquivoCurso" runat="server" Text="Arquivo Curso:" Visible="false" /></strong>&nbsp;<br />
<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensCursoPanamericano/" Editable="true"
	MaxFileLength="5000" TipoArquivo="ALL" />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<br />
