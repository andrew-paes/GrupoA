<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revistaBrinde.ascx.cs"
    Inherits="content_module_revistaBrinde" %>
<strong>Revista:</strong>
<br />
<asp:TextBox runat="server" ID="txtNome" Width="300px" CssClass="frmborder" MaxLength="150"
    Enabled="false" />
<br />
<br />
<fieldset style="padding: 10px; width: 600px;">
    <legend>Produto</legend>
    <h4>
        Pesquisar</h4>
    <asp:Label runat="server" ID="lblEAN" Font-Bold="true" Text="ISBN13:" />
    <asp:RequiredFieldValidator ID="rfvEAN" runat="server" ControlToValidate="txtISBN13"
        ErrorMessage="Campo Obrigatório" ValidationGroup="3" />
    <br />
    <asp:TextBox runat="server" ID="txtISBN13" Width="250px" CssClass="frmborder" MaxLength="50" />
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
                <asp:BoundColumn HeaderText="Nome" DataField="nomeProduto" />
                <asp:BoundColumn HeaderText="Cód. EAN" DataField="codigoEAN13" />
                <asp:BoundColumn HeaderText="Valor Unitário" DataField="valorUnitario" />
                <asp:BoundColumn HeaderText="Valor Oferta" DataField="valorOferta" />
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
            BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnItemCreated="grids_ItemCreated"
            OnDeleteCommand="dgProdutosAdicionados_DeleteCommand">
            <FooterStyle BackColor="#CCCC99" />
            <EditItemStyle Width="150px" />
            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn HeaderText="Nome" DataField="nomeProduto" />
                <asp:BoundColumn HeaderText="Cód. EAN" DataField="codigoEAN13" />
                <asp:BoundColumn HeaderText="Valor Unitário" DataField="valorUnitario" />
                <asp:BoundColumn HeaderText="Valor Oferta" DataField="valorOferta" />
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
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
