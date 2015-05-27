<%@ Control Language="C#" AutoEventWireup="true" CodeFile="contatoSetor.ascx.cs"
	Inherits="content_module_contatoSetor" %>
<asp:HiddenField ID="hdnContatoSetorId" runat="server" Value="0" />
<strong>Nome da Seção:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtNomeSetor" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNomeSetor" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
