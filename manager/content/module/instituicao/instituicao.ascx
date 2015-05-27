<%@ Control Language="C#" AutoEventWireup="true" CodeFile="instituicao.ascx.cs"
	Inherits="content_module_instituicao" %>
<strong>Nome da Instituição:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
	ControlToValidate="txtNomeInstituicao" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNomeInstituicao" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>CNPJ:</strong>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
	ControlToValidate="txtCnpj" ErrorMessage="Campo Obrigatório" />--%>
<br />
<asp:TextBox runat="server" ID="txtCnpj" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>Telefone:</strong>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtTelefone" ErrorMessage="Campo Obrigatório" />--%>
<br />
<asp:TextBox runat="server" ID="txtTelefone" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>E-mail:</strong>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1"
	ControlToValidate="txtEmail" ErrorMessage="Campo Obrigatório" />--%>
<br />
<asp:TextBox runat="server" ID="txtEmail" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>Site:</strong>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="1"
	ControlToValidate="txtUrlSite" ErrorMessage="Campo Obrigatório" />--%>
<br />
<asp:TextBox runat="server" ID="txtUrlSite" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>Código:</strong>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1"
	ControlToValidate="txtCodigo" ErrorMessage="Campo Obrigatório" />--%>
<br />
<asp:TextBox runat="server" ID="txtCodigo" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
