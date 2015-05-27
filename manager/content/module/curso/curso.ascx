<%@ Control Language="C#" AutoEventWireup="true" CodeFile="curso.ascx.cs"
	Inherits="content_module_curso" %>
<strong>Nome do Curso:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
	ControlToValidate="txtNome" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNome" Width="300px" CssClass="frmborder"
	MaxLength="150" />
<br />
<strong>Código:</strong>
<br />
<asp:TextBox runat="server" ID="txtCodigoCurso" Width="300px" CssClass="frmborder"
	MaxLength="50" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
