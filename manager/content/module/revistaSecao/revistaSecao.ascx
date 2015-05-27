<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revistaSecao.ascx.cs"
	Inherits="content_module_revista_secao" %>
<asp:HiddenField ID="hdnRevistaSecaoId" runat="server" Value="0" />
<strong>Nome da Seção:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	ControlToValidate="txtNomeSecao" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNomeSecao" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<br />
<strong>Nome da Revista:</strong>
<asp:CustomValidator runat="server" ID="cvValidarRevista" OnServerValidate="cvValidarRevista_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:DropDownList ID="ddlRevista" runat="server" Width="200px" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
