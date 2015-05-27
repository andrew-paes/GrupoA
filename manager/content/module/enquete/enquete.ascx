<%@ Control Language="C#" AutoEventWireup="true" CodeFile="enquete.ascx.cs" Inherits="content_module_evento_evento" %>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:HiddenField ID="hddEnqueteId" runat="server" Value="0" />
<!-- Nome -->
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtNome" Width="250px" CssClass="frmborder" MaxLength="100"></asp:TextBox>
<br />
<br />
<!-- Página -->
<asp:CustomValidator runat="server" ID="cvPaginas" OnServerValidate="cvValidarAtivacao_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:Label runat="server" ID="lblPagina" Font-Bold="true" Text="Páginas:"></asp:Label><br />
<asp:CheckBoxList ID="cblPaginas" runat="server" />
<br />
<br />
<!-- Pergunta -->
<asp:Label runat="server" ID="lblPergunta" Font-Bold="true" Text="Pergunta:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvPergunta" runat="server" ControlToValidate="txtPergunta"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtPergunta" TextMode="MultiLine" Rows="2" Width="580px"
                CssClass="frmborder"></asp:TextBox>
<br />
<br />
<!-- Opções -->
<div id="areaOpcoes" runat="server" visible="false">
	<h3>
		Opções:</h3>
	<fieldset style="padding: 10px; width: 600px;">
		<legend>Nova opção:</legend>
		<asp:CustomValidator runat="server" ID="cvValidarAtivacao" OnServerValidate="cvValidarAtivacao_ServerValidate"
			ValidationGroup="1"></asp:CustomValidator>
		<br />
		<asp:Label runat="server" ID="lblDescricaoOpcao" Font-Bold="true" Text="Descrição:" />
		<asp:RequiredFieldValidator ID="rfvDescricaoOpcao" runat="server" ValidationGroup="2"
			ControlToValidate="txtDescricaoOpcao" ErrorMessage="Campo Obrigatório" />
		<br />
		<asp:TextBox runat="server" ID="txtDescricaoOpcao" Width="300px" CssClass="frmborder" MaxLength="55" />
		<br />
		<br />
		<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/btn_adicionar.png"
			Width="73" Height="20" BorderWidth="0" AlternateText="Adicionar Imagem" ImageAlign="AbsMiddle"
			CausesValidation="true" ValidationGroup="2" OnClick="btnInserirOpcaoEvento_Click" />
		<br />
		<br />
		<asp:DataGrid runat="server" Width="500px" ID="dgGrid" CellPadding="4" ForeColor="Black"
			GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
			BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgGrid_DeleteCommand"
			OnItemCreated="dgGrid_ItemCreated">
			<FooterStyle BackColor="#CCCC99" />
			<EditItemStyle Width="150px" />
			<SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
			<AlternatingItemStyle BackColor="White" />
			<ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
				Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
			<Columns>
				<asp:BoundColumn HeaderText="Descrição" DataField="Descricao"></asp:BoundColumn>
				<asp:BoundColumn HeaderText="Contador" DataField="Contador"></asp:BoundColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "enqueteOpcaoId") %>'
							ImageUrl="~/img/delete.png" />
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
			<HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Font-Italic="False"
				Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
		</asp:DataGrid>
	</fieldset>
	<!-- Ativa -->
	<table width="300px">
		<tr>
			<td>
				<asp:Label runat="server" ID="lblAtiva" Font-Bold="true" Text="Ativa:" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:CheckBox ID="chkAtivo" runat="server" />
			</td>
		</tr>
	</table>
	<br />
	<br />
</div>
<br />
<br />
<!-- Controles -->
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" />
</div>
<script type="text/javascript">
	//<![CDATA[
    $(document).ready(function () {
        $('textarea[id$="txtPergunta"]').keyup(function () {
            var max = 210;
            if ($(this).val().length > max) {
                $(this).val($(this).val().substr(0, max));
            }
        });
    });
	//]]>
</script>