<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revistaArtigo.ascx.cs"
	Inherits="content_module_revista_artigo" %>
<asp:HiddenField ID="hdnRevistaArtigoId" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoIdDelete" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoIdDeleteP" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoId" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoIdP" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoCapaIdDelete" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoCapaId" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoLateralIdDelete" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoLateralId" runat="server" Value="0" />
<asp:HiddenField ID="hddRevistaArtigoArquivoNome" runat="server" Value="0" />
<asp:HiddenField ID="hddSecao" runat="server" Value="" />
<div id="divArtigo" runat="server">
	<fieldset style="padding: 10px; width: 700px;">
		<legend><span id="fieldArtigo" style="cursor: pointer;">[-] Artigo</span></legend>
		<table id="tableArtigo" cellpadding="5" cellspacing="5">
			<tr>
				<td>
					<strong>Revista:</strong>
					<asp:CustomValidator runat="server" ID="cvValidarRevista" OnServerValidate="cvValidarRevista_ServerValidate"
						ValidationGroup="1"></asp:CustomValidator>
					<br />
					<asp:DropDownList ID="ddlRevistaAux" runat="server" Width="200px" OnSelectedIndexChanged="ddlRevistaAux_SelectedIndexChanged"
						AutoPostBack="true">
					</asp:DropDownList>
					<br />
					<br />
					<strong>Edição:</strong>
					<br />
					<asp:DropDownList ID="ddlEdicao" runat="server" Width="200px" Enabled="false" OnSelectedIndexChanged="ddlEdicao_SelectedIndexChanged"
						AutoPostBack="true" />
					<br />
					<br />
					<strong>Título do Artigo:</strong>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
						ControlToValidate="txtTituloArtigo" ErrorMessage="Campo Obrigatório" />
					<br />
					<asp:TextBox runat="server" ID="txtTituloArtigo" Rows="2" TextMode="MultiLine" Width="400px"
						CssClass="frmborder" MaxLength="500" Enabled="false" />
					<br />
					<br />
					<%--<strong>Subtítulo do Artigo:</strong>
                    <br />
                    <asp:TextBox runat="server" ID="txtSubTituloArtigo" Rows="2" TextMode="MultiLine" Width="400px" CssClass="frmborder"
	                    MaxLength="200" Enabled="false" />
                    <br />
                    <br />--%>
					<strong>Resumo:</strong>
					<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                        ControlToValidate="txtResumoArtigo" ErrorMessage="Campo Obrigatório" />--%>
					<br />
					<%--<asp:TextBox runat="server" ID="txtResumoArtigo" Width="400px" TextMode="MultiLine"
                        CssClass="frmborder" Enabled="false" />--%>
					<ag2:HtmlTextBox runat="server" ID="txtResumoArtigo" ToolBar="Basic" />
					<br />
					<br />
					<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Texto do Artigo:" />
					<br />
					<ag2:HtmlTextBox runat="server" ID="txtTextoArtigo" ToolBar="Full" />
					<br />
					<br />
					<asp:Label runat="server" ID="lblAutores" Font-Bold="true" Text="Autores:" />
					<br />
					<ag2:HtmlTextBox runat="server" ID="txtAutores" ToolBar="Basic" />
					<br />
					<br />
					<strong>Seção:</strong>
					<asp:DropDownList ID="ddlSecao" runat="server" Width="200px" Enabled="false" />
					<br />
					<br />
					<strong>Permite Visualização:</strong>
					<asp:DropDownList ID="ddlPermiteVisualizacao" runat="server" Width="200px" Enabled="false" />
					<br />
					<br />
					<strong>Referências:</strong>
					<br />
					<ag2:HtmlTextBox runat="server" ID="txtBibliografia" />
					<br />
					<br />
					<strong>Data Publicação:</strong>
					<asp:CustomValidator runat="server" ID="cvValidarData" OnServerValidate="cvValidarData_ServerValidate"
						ValidationGroup="1"></asp:CustomValidator>
					<br />
					<ag2:DateField runat="server" ID="txtDataPublicacao" CssClass="frmborder" />
					<br />
					<br />
					<asp:CheckBox runat="server" ID="chkAtivo" Text="Ativo" />
					<br />
					<br />
					<asp:CheckBox runat="server" ID="chkDestaqueHome" Text="Destaque na Home" />
					<br />
					<br />
					<asp:CheckBox runat="server" ID="chkDestaquePrincipal" Text="Destaque Principal de Capa" />
					<br />
					<br />
					<%--<asp:CheckBox runat="server" ID="chkConteudoOnline" Text="Conteúdo Exclusivo Online" />
					<br />
					<br />--%>
					<strong>Artigo Principal:</strong>
					<br />
					<asp:DropDownList ID="ddlArtigoPrincipal" runat="server" Width="200px" Enabled="false" />
					<br />
					<br />
					<!-- Categorias -->
					<asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:"></asp:Label>
					<br />
					<asp:CheckBoxList ID="cblCategorias" runat="server" />
					<br />
					<asp:Panel runat="server" ID="pnlVisualizacao">
						<asp:HyperLink runat="server" ID="hlVisualizarArtigo" NavigateUrl="" Target="_blank">Clique aqui para pré-visualizar este artigo</asp:HyperLink>
						<br />
						<br />
					</asp:Panel>
					<asp:Panel runat="server" ID="pnlArquivoCapa">
						<strong>
							<asp:Label ID="lblArquivoCapaRevistaArtigo" runat="server" Text="Imagem Capa: (720 x 280)"
								Visible="false" /></strong>&nbsp;<br />
						<br />
						<ag2:ListFiles ID="lfArquivoCapa" runat="server" TargetFolder="imagensRevista/" Editable="true"
							MaxFileLength="5000" TipoArquivo="IMAGE" />
						<br />
					</asp:Panel>
					<strong>
						<asp:Label ID="lblArquivoRevistaArtigo" runat="server" Text="Imagem Thumbmail: (220 x 162)"
							Visible="false" /></strong>&nbsp;<br />
					<br />
					<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensRevista/" Editable="true"
						MaxFileLength="5000" TipoArquivo="IMAGE" />
					<br />
					<asp:Panel runat="server" ID="pnlArquivoLateral">
						<strong>
							<asp:Label ID="lblArquivoLateralRevistaArtigo" runat="server" Text="Imagem Lateral: (200 px)"
								Visible="false" /></strong>&nbsp;<br />
						<br />
						<ag2:ListFiles ID="lfArquivoLateral" runat="server" TargetFolder="imagensRevista/"
							Editable="true" MaxFileLength="5000" TipoArquivo="IMAGE" />
						<br />
					</asp:Panel>
				</td>
			</tr>
		</table>
	</fieldset>
</div>
<br />
<div id="divSim" runat="server" visible="false">
	<fieldset style="padding: 10px; width: 700px;">
		<legend><span id="fieldSim" style="cursor: pointer;">[-] Sim</span></legend>
		<table id="tableSim" cellpadding="5" cellspacing="5">
			<tr>
				<td>
					<asp:HiddenField runat="server" ID="hddControversiaSimId" Value="0" />
					<strong>Título:</strong>
					<br />
					<asp:TextBox runat="server" ID="txtTituloSim" Width="400px" CssClass="frmborder"
						MaxLength="200" />
					<br />
					<br />
					<strong>Texto:</strong>
					<asp:CustomValidator runat="server" ID="cvValidarSim" OnServerValidate="cvValidarSim_ServerValidate"
						ValidationGroup="1"></asp:CustomValidator>
					<br />
					<ag2:HtmlTextBox runat="server" ID="txtTextoSim" ToolBar="Full" />
					<br />
					<br />
					<strong>Autor:</strong>
					<br />
					<asp:TextBox runat="server" ID="txtAutorSim" Width="400px" CssClass="frmborder" MaxLength="200" />
					<br />
					<br />
				</td>
			</tr>
		</table>
	</fieldset>
</div>
<br />
<div id="divNao" runat="server" visible="false">
	<fieldset style="padding: 10px; width: 700px;">
		<legend><span id="fieldNao" style="cursor: pointer;">[-] Não</span></legend>
		<table id="tableNao" cellpadding="5" cellspacing="5">
			<tr>
				<td>
					<asp:HiddenField runat="server" ID="hddControversiaNaoId" Value="0" />
					<strong>Título:</strong>
					<br />
					<asp:TextBox runat="server" ID="txtTituloNao" Width="400px" CssClass="frmborder"
						MaxLength="200" />
					<br />
					<br />
					<strong>Texto:</strong>
					<asp:CustomValidator runat="server" ID="cvValidarNao" OnServerValidate="cvValidarNao_ServerValidate"
						ValidationGroup="1"></asp:CustomValidator>
					<br />
					<ag2:HtmlTextBox runat="server" ID="txtTextoNao" ToolBar="Full" />
					<br />
					<br />
					<strong>Autor:</strong>
					<br />
					<asp:TextBox runat="server" ID="txtAutorNao" Width="400px" CssClass="frmborder" MaxLength="200" />
					<br />
					<br />
				</td>
			</tr>
		</table>
	</fieldset>
</div>
<br />
<div id="pnlRestricaoProduto" runat="server" visible="false">
	<!-- Pesquisa -->
	<fieldset style="padding: 10px; width: 600px;">
		<legend>Produto</legend>
		<h4>
			Pesquisar</h4>
		<asp:Label runat="server" ID="lblEAN" Font-Bold="true" Text="ISBN13:"></asp:Label>
		<asp:RequiredFieldValidator ID="rfvEAN" runat="server" ControlToValidate="txtISBN13"
			ErrorMessage="Campo Obrigatório" ValidationGroup="3"></asp:RequiredFieldValidator>
		<br />
		<asp:TextBox runat="server" ID="txtISBN13" Width="250px" CssClass="frmborder" MaxLength="50"></asp:TextBox>
		<asp:ImageButton ID="btnPesquisarProduto" runat="server" ImageUrl="~/img/btn_buscar.png"
			Width="64" Height="22" BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle"
			CausesValidation="true" OnClick="btnPesquisarProduto_Click" ValidationGroup="3" />
		<br />
		<br />
		<!-- Resultado da Pesquisa  -->
		<div id="pnlProdutosEncontrados" runat="server" visible="false">
			<h4>
				Produtos Encontrados</h4>
			<asp:Label ID="lblTextoPesquisaProdutos" runat="server" Text="Nenhum produto encontrado!"
				Visible="false" />
			<asp:DataGrid runat="server" Width="500px" ID="dgProdutosEncontrados" CellPadding="4"
				ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
				BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnEditCommand="dgProdutosEncontrados_EditCommand">
				<FooterStyle BackColor="#CCCC99" />
				<EditItemStyle Width="150px" />
				<SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
				<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
				<AlternatingItemStyle BackColor="White" />
				<ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
					Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
				<Columns>
					<asp:BoundColumn HeaderText="Nome" DataField="nomeProduto"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Cód. EAN" DataField="codigoEAN13"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Valor Unitário" DataField="valorUnitario"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Valor Oferta" DataField="valorOferta"></asp:BoundColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton ID="btnInserir" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "produtoId") %>'
								ImageUrl="~/img/add.png" />
						</ItemTemplate>
						<ItemStyle Width="17px" />
					</asp:TemplateColumn>
				</Columns>
				<HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Font-Italic="False"
					Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
			</asp:DataGrid>
		</div>
		<br />
		<br />
		<!-- Produtos ja adicionados  -->
		<div id="Div4">
			<h4>
				Produtos Adicionados</h4>
			<asp:Label ID="lblTextoProdutosAdicionados" runat="server" Text="Nenhum produto adicionado!" />
			<asp:DataGrid runat="server" Width="500px" ID="dgProdutosAdicionados" CellPadding="4"
				ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
				BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgProdutosAdicionados_DeleteCommand"
				OnItemCreated="grids_ItemCreated">
				<FooterStyle BackColor="#CCCC99" />
				<EditItemStyle Width="150px" />
				<SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
				<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
				<AlternatingItemStyle BackColor="White" />
				<ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
					Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
				<Columns>
					<asp:BoundColumn HeaderText="Nome" DataField="nomeProduto"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Cód. EAN" DataField="codigoEAN13"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Valor Unitário" DataField="valorUnitario"></asp:BoundColumn>
					<asp:BoundColumn HeaderText="Valor Oferta" DataField="valorOferta"></asp:BoundColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton ID="btnInserir" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "produtoId") %>'
								ImageUrl="~/img/delete.png" />
						</ItemTemplate>
						<ItemStyle Width="17px" />
					</asp:TemplateColumn>
				</Columns>
				<HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Font-Italic="False"
					Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
			</asp:DataGrid>
		</div>
	</fieldset>
</div>
<br />
<%--<strong>
	<asp:Label ID="lblArquivoRevistaArtigoGaleria" runat="server" Text="Galeria de Imagens:"
		Visible="false" /></strong>&nbsp;<br />
<br />
<ag2:ListFiles ID="ListFiles2" runat="server" TargetFolder="revista/" Editable="true"
	MaxFileLength="5000" TipoArquivo="IMAGE" ScriptModal="revistaArtigoImagens.aspx"
	MultiFile="true" />--%>
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<script type="text/javascript">
	$(document).ready(function () {
		$("input[id$='chkDestaquePrincipal']").change(function () {
			GrupoA.ControlaTela.init();
		});

		$('textarea[id$="txtTituloArtigo"]').keyup(function () {
			var max = 500;
			if ($(this).val().length > max) {
				$(this).val($(this).val().substr(0, max));
			}
		});

		GrupoA.ControlaTela.init();

		SetTable($('#fieldSim'), $('#tableSim'), 'Sim');
		SetTable($('#fieldNao'), $('#tableNao'), 'Não');

		$('#fieldArtigo').click(function () {
			var field = $(this);
			var table = $('#tableArtigo');

			SetTable(field, table, "Artigo");
		});

		$('#fieldSim').click(function () {
			var field = $(this);
			var table = $('#tableSim');

			SetTable(field, table, "Sim");
		});

		$('#fieldNao').click(function () {
			var field = $(this);
			var table = $('#tableNao');

			SetTable(field, table, "Não");
		});
	});

	var GrupoA = {};

	GrupoA.ControlaTela = {
		init: function () {

			if ($("input[id$='hdnRevistaArtigoId']").val() != '0' && $("input[id$='chkDestaquePrincipal']").attr("checked")) {
				$("div[id$='pnlArquivoCapa']").show();
			} else {
				$("div[id$='pnlArquivoCapa']").hide();
			}
		}
	}

	function SetTable(field, table, nomeProduto) {
		table.is(":visible") ? HideTable(field, table, nomeProduto) : ShowTable(field, table, nomeProduto);
	}

	function HideTable(field, table, nomeProduto) {
		field.html("[+] " + nomeProduto);
		table.hide();
	}

	function ShowTable(field, table, nomeProduto) {
		field.html("[-] " + nomeProduto);
		table.show();
	}
</script>
