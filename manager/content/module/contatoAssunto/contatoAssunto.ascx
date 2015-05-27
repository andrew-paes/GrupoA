<%@ Control Language="C#" AutoEventWireup="true" CodeFile="contatoAssunto.ascx.cs"
	Inherits="content_module_contatoAssunto" %>
<asp:HiddenField ID="hdnContatoAssuntoId" runat="server" Value="0" />
<strong>Nome do Assunto:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtContatoAssunto" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtContatoAssunto" Width="300px" CssClass="frmborder"
	MaxLength="200" />
<br />
<br />
<strong>Setor Responsável:</strong>
<asp:CustomValidator runat="server" ID="cvValidarSetor" OnServerValidate="cvValidarSetor_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:DropDownList ID="ddlSetor" runat="server" Width="200px" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
