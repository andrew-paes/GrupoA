<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nossosSites.ascx.cs" Inherits="content_module_nossosSites_nossosSites" %>
<strong>Nome do Link:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
    ControlToValidate="txtNomeLink" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtNomeLink" Width="300px" CssClass="frmborder" MaxLength="100" />
<br />
<br />
<strong>Url do Link:</strong>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
    ControlToValidate="txtUrlLink" ErrorMessage="Campo Obrigatório" />
<br />
Ex.: http://www.grupoa.com.br
<br />
<asp:TextBox runat="server" ID="txtUrlLink" Width="300px" CssClass="frmborder" MaxLength="250" />
<br />
<br />
<strong>Ativo:</strong>
<br />
<asp:CheckBox ID="chkAtivo" runat="server" />
<br />
<br />
<strong>Nova Janela:</strong>
<br />
<asp:CheckBox ID="chkTargetBlank" runat="server" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
