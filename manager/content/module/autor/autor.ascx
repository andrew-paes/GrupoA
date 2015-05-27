<%@ Control Language="C#" AutoEventWireup="true" CodeFile="autor.ascx.cs" Inherits="content_module_autor_autor" %>
<label>
    <b>Nome:</b></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
    Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtNome" Width="200px" CssClass="frmborder" MaxLength="128"></asp:TextBox>
<br />
<br />
<label>
    <b>URL:</b></label>
<br />
<asp:TextBox runat="server" ID="txtUrl" Width="200px" CssClass="frmborder" MaxLength="256"></asp:TextBox>
<br />
<br />
<label>
    <b>E-mail:</b></label>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
    Display="Dynamic" ErrorMessage="Campo inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ValidationGroup="1"></asp:RegularExpressionValidator>
<br />
<asp:TextBox runat="server" ID="txtEmail" Width="200px" CssClass="frmborder" MaxLength="256"></asp:TextBox>
<br />
<br />
<label>
    <b>Blog:</b></label>
<br />
<asp:TextBox runat="server" ID="txtBlog" Width="200px" CssClass="frmborder" MaxLength="256"></asp:TextBox>
<br />
<br />
<label>
    <b>Biografia:</b></label>
<br />
<ag2:HtmlTextBox runat="server" ID="txtBiografia" />
<br />
<br />
<div runat="server" id="divTitulosRelacionados" visible="true">
    <label>
        <b>Títulos Relacionados:</b></label>
    <br />
    <asp:Repeater ID="rptTitulosRelacionados" runat="server" OnItemDataBound="rptTitulosRelacionados_ItemDataBound">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblNomeTitulo"></asp:Label>
            <br />
        </ItemTemplate>
    </asp:Repeater>

    <br />
    <br />
</div>
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" />
</div>
<br />
<asp:Panel ID="pnlArquivo" runat="server" Visible="false">
	<asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Imagem:" /><br />
	<ag2:ListFiles ID="upArquivoImagem" runat="server" TargetFolder="imagensAutor/"
		Editable="true" MaxFileLength="10000" TipoArquivo="IMAGE" />
</asp:Panel>
<br />