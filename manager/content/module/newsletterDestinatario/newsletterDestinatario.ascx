<%@ Control Language="C#" AutoEventWireup="true" CodeFile="newsletterDestinatario.ascx.cs"
	Inherits="content_module_newsletterDestinatario" %>
<strong>Nome:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
	ControlToValidate="txtNome" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNome" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>E-mail:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
	ControlToValidate="txtEmail" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtEmail" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
