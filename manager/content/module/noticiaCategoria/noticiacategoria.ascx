<%@ Control Language="C#" AutoEventWireup="true" CodeFile="noticiacategoria.ascx.cs" Inherits="content_module_noticiaCategoria_noticiacategoria" %>
<asp:HiddenField ID="hddCategoriaEventoId" runat="server" Value="0" />
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtNome" Width="250px" CssClass="frmborder" MaxLength="50"></asp:TextBox>
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>