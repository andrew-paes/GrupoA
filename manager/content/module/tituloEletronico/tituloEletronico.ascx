<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloEletronico.ascx.cs" Inherits="content_module_tituloEletronico_tituloEletronico" %>

<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Título:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtTituloLivro"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox ID="txtTituloLivro" Width="350px" CssClass="frmborder" 
    MaxLength="200" runat="server">Título</asp:TextBox>
<asp:HiddenField runat="server" ID="hddProdutoId" Value="" />
<asp:HiddenField runat="server" ID="hddTituloId" Value="" />
<br />
<br />
<asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Resumo:"></asp:Label>
<%--<asp:RequiredFieldValidator ID="rfvSub" runat="server" ControlToValidate="txtSubtituloLivro"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
<br />
<asp:TextBox ID="txtSubtituloLivro" Width="350px" CssClass="frmborder" MaxLength="200"
	runat="server">Subtítulo</asp:TextBox>
<br />
<br />
<strong>Área de Conhecimento</strong>
<br />
<asp:Label ID="txtNomeCategoria" Width="350px" CssClass="frmborder" 
    MaxLength="256" runat="server"></asp:Label>
<br />
<br />
<asp:Panel ID="pnlCadastro" runat="server">
	<table cellpadding="5" cellspacing="5">
		<tr>
			<td>
				<asp:Label ID="Label38" runat="server" Font-Bold="true" Text="Nº Páginas:"></asp:Label>
                <br />
                <asp:Label ID="lblPaginas" runat="server" CssClass="frmborder" Width="200px"></asp:Label>
                <br />
				</td>
			<td>
				<asp:Label ID="Label40" runat="server" Font-Bold="true" Text="Edição:"></asp:Label>
                <br />
                <asp:Label ID="lblEdicao" runat="server" CssClass="frmborder" Text="" 
                    Width="200px"></asp:Label>
            </td>
		</tr>
		<tr>
            <td>
                <asp:Label ID="Label41" runat="server" Font-Bold="true" Text="Data Lançamento:"></asp:Label>
                <br />
                <asp:Label ID="lblDtLancamento" runat="server" CssClass="frmborder" Text="" 
                    Width="200px" Height="15px"></asp:Label>
                <br />
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="ISBN 13:"></asp:Label>
                <br />
                <asp:Label ID="lblISBN13" runat="server" CssClass="frmborder" Text="" 
                    Width="200px"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label39" runat="server" Font-Bold="true" Text="Data Cadastro:"></asp:Label>
                <br /><asp:Label ID="lblDtCadastro" runat="server" CssClass="frmborder" Text="" 
                    Width="200px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label42" runat="server" Font-Bold="true" Text="Data Publicação:"></asp:Label>
                <br /><asp:Label ID="lblDtPublicacao" runat="server" CssClass="frmborder" Text="" 
                    Width="200px" Height="15px"></asp:Label>
            </td>
        </tr>
		<tr>
			<td>
				
			    <asp:Label ID="Label43" runat="server" Font-Bold="True" Text="Valor Unitário:"></asp:Label>
                <br /><asp:Label ID="lblValorUnitario" runat="server" CssClass="frmborder" Text="" 
                    Width="200px"></asp:Label>
				
			</td>
			<td>
                 <asp:Label ID="Label20" runat="server" Font-Bold="true" 
                    Text="Valor Promoção:"></asp:Label>
                <br /><asp:Label ID="lblValorPromocao" runat="server" CssClass="frmborder" Text="" 
                    Width="200px"></asp:Label>
				<br />
			</td>
		</tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="chkHomologado" runat="server" Text="Homologado" />
            </td>
        </tr>
		</table>
</asp:Panel>
<br />
<h3>Autores</h3>
<br />
<div style="padding-left:15">

    <asp:GridView ID="gvAutores" runat="server" Width="600px" CellPadding="4" ForeColor="#333333"
        AutoGenerateColumns="False" CaptionAlign="Left">
        <PagerSettings Mode="NumericFirstLast" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Verdana" Font-Size="7pt"
            Height="20px" Wrap="False" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Autor">
                <ItemTemplate>
                    <asp:Label ID="lblNomeProduto" runat="server" 
                        Text='<%# Eval("Autor.NomeAutor") %>' />
                </ItemTemplate>
                <ItemStyle Height="20px" Width="150px" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" 
                        Text='<%# Eval("Autor.Email") %>' />
                </ItemTemplate>
                <ItemStyle Height="20px" Width="150px" HorizontalAlign="Left" 
                    VerticalAlign="Middle" />
            </asp:TemplateField>
            
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="verdana"
            Font-Size="8pt" Height="20px" />
        <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" Font-Bold="True"
            Font-Names="verdana" Font-Size="8pt" Height="25px" VerticalAlign="Middle" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
        <EmptyDataTemplate>
            <center>
                <br />
                <br />
                <p>Nenhum Autor encontrado.</p>
                <br />
                <br />
            </center>
        </EmptyDataTemplate>
    </asp:GridView>

</div>
<!-- Controles -->
<br />
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>