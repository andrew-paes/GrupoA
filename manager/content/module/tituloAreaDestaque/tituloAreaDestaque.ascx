<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloAreaDestaque.ascx.cs"
	Inherits="content_module_tituloAreaDestaque_tituloAreaDestaque" %>
<%@ Register Src="../../../ctl/ag2RelacionarList.ascx" TagName="ag2RelacionarList"
	TagPrefix="uc1" %>
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Nome da Área:"></asp:Label><br />
<asp:TextBox ID="txtNomeArea" runat="server" Enabled="false" Width="300" />
<br />
<br />
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Filtros da Listagem:" />
<table width="420px" border="0" cellpadding="10" cellspacing="10">
	<tr>
		<td>
			<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="ISBN13 do título:"></asp:Label><br />
			<asp:TextBox ID="txtISBN13" runat="server" CssClass="frmborder" MaxLength="13"></asp:TextBox>
			<td>
				&nbsp;<asp:Button runat="server" ID="btnPesquisar" Text="Pesquisar" Width="90px"
					OnClick="btnPesquisar_Click" />
			</td>
	</tr>
</table>
<br />
<uc1:ag2RelacionarList ID="listRel" runat="server" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
