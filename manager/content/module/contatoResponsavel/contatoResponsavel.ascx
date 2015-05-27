<%@ Control Language="C#" AutoEventWireup="true" CodeFile="contatoResponsavel.ascx.cs"
	Inherits="content_module_contatoResponsavel" %>
<asp:HiddenField ID="hdnContatoResponsavelId" runat="server" Value="0" />
<strong>Nome do Responsavel:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtContatoResponsavel" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtContatoResponsavel" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<br />
<strong>Email do Responsavel:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
	ControlToValidate="txtContatoResponsavelEmail" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtContatoResponsavelEmail" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<br />
<strong>Assunto Contato:</strong>
<asp:CustomValidator runat="server" ID="cvValidarAssunto" OnServerValidate="cvValidarAssunto_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:DropDownList ID="ddlAssunto" runat="server" Width="400px" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
