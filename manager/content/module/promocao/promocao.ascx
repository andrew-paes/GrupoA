<%@ Control Language="C#" AutoEventWireup="true" CodeFile="promocao.ascx.cs" Inherits="content_module_promocao_promocao" %>
<style>
    table tr td
    {
        vertical-align: top;
    }
</style>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:HiddenField ID="hddPromocaoId" runat="server" Value="0" />
<!-- Nome -->
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome da Promoção:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtNome" Width="250px" CssClass="frmborder" MaxLength="200"></asp:TextBox>
<br />
<br />
<!-- Descrição -->
<asp:Label runat="server" ID="lblDescricao" Font-Bold="true" Text="Descrição da Promoção: (Será exibido para o cliente quando o mesmo preencher o campo de cupom no carrinho)"></asp:Label>
<asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtDescricao" Columns="200" Rows="6" Width="250px"
    CssClass="frmborder" MaxLength="200"></asp:TextBox>
<br />
<br />
<div runat="server" id="divQuantidadeCupons">
    <asp:Label runat="server" ID="lblNumeroMaximoCupom" Font-Bold="true" Text="Quantidade de cupons a serem gerados:"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumeroMaximoCupom"
        ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
    <asp:TextBox runat="server" ID="txtNumeroMaximoCupom" Width="30px" CssClass="frmborder"
        MaxLength="10"></asp:TextBox>
    <asp:HiddenField runat="server" ID="hddNumeroMaximoCupom" Value="" />
    <asp:CustomValidator runat="server" ID="cvValidaNumeroMaximoCupom" OnServerValidate="cvValidaNumeroMaximoCupom_ServerValidate"
        ValidationGroup="1"></asp:CustomValidator>
    <br />
    <br />
</div>
<table width="300px">
    <tr>
        <td>
            <asp:CheckBox ID="chkReutilizavel" runat="server" Font-Bold="true" Text=" Cupom sem limite de utilizações" />
        </td>
    </tr>
</table>
<br />
<div runat="server" id="divAutomatica" style="display: none">
    <asp:CheckBox ID="chkAutomatica" runat="server" Text=" Aplicar promoção automaticamente sem o preenchimento do cupom?"
        Font-Bold="true" />
    <br />
    <br />
</div>
<!-- Tipo de Promocaoo -->
<asp:Label runat="server" ID="lblTipoPromocao" Font-Bold="true" Text="Tipo de Promoção:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvPromocaoTipo" runat="server" ControlToValidate="rblPromocaoTipo"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:RadioButtonList ID="rblPromocaoTipo" OnSelectedIndexChanged="rblPromocaoTipo_SelectedIndexChanged"
    runat="server">
</asp:RadioButtonList>
<br />
<table width="500px">
    <tr>
        <td>
            <asp:Label runat="server" ID="lblDataPromocaoInicio" Font-Bold="true" Text="Data de Início do Promoção:" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblDataPromocaoFim" Font-Bold="true" Text="Data de Fim do Promoção:" /><br />
        </td>
    </tr>
    <tr>
        <td>
            <ag2:DateField runat="server" ID="txtDataPromocaoInicio" CssClass="frmborder" />
        </td>
        <td>
            <ag2:DateField runat="server" ID="txtDataPromocaoFim" CssClass="frmborder" />
        </td>
    </tr>
    <tr>
        <td colspan="100%">
            <asp:CustomValidator runat="server" ID="cvValidarDatasPromocao" OnServerValidate="cvValidarDatasPromocao_ServerValidate"
                ValidationGroup="1"></asp:CustomValidator>
        </td>
    </tr>
</table>
<br />
<!-- Ativa e Destaque -->
<table width="300px">
    <tr>
        <td>
            <asp:CheckBox ID="chkAtivo" runat="server" Text=" Promoção Ativa" Font-Bold="true" />
        </td>
    </tr>
</table>
<br />
<!-- Implementacao das faixas de desconto  -->
<div id="pnlFaixasPromocao" runat="server" visible="false">
    <!-- Pesquisa -->
    <fieldset style="padding: 10px; width: 620px;">
        <legend>Faixas de Promoção</legend>
        <h4>
            Nova faixa de promoção</h4>
        <!-- Validacoes -->
        <asp:CustomValidator runat="server" ID="cvValidaFaixaDuplicada" OnServerValidate="cvValidaFaixaDuplicada_ServerValidate"
            ValidationGroup="5"></asp:CustomValidator><br />
        <!-- Valor Minimo -->
        <asp:Label runat="server" ID="lblValorMinimo" Font-Bold="true" Text="Quantidade ou Valor Mínimo:"></asp:Label>
        <asp:RequiredFieldValidator ID="rfvValorMinimo" runat="server" ControlToValidate="txtValorMinimo"
            ErrorMessage="Campo Obrigatório" ValidationGroup="5"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rvValorMinimo" runat="server" MinimumValue="1" MaximumValue="999999999999"
            ControlToValidate="txtValorMinimo" Text="O valor mínimo deve ser maior que zero (0)."
            ValidationGroup="5" />
        <br />
        <asp:TextBox runat="server" ID="txtValorMinimo" Width="50px" CssClass="frmborder"
            MaxLength="15"></asp:TextBox>
        <br />
        <br />
        <!-- Percentual Desconto -->
        <asp:Panel ID="pnlPercentualDesconto" runat="server">
            <asp:Label runat="server" ID="lblPercentualDesconto" Font-Bold="true" Text="Percentual Desconto:"></asp:Label>
            <asp:RangeValidator ID="rvValidaPercentual" runat="server" MinimumValue="0" MaximumValue="100"
                ControlToValidate="txtPercentualDesconto" Text="O percentual de desconto deve ser um valor entre 0 e 100."
                ValidationGroup="5" Type="Integer" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPercentualDesconto"
                ErrorMessage="Campo Obrigatório" ValidationGroup="5"></asp:RequiredFieldValidator><br />
            <asp:TextBox runat="server" ID="txtPercentualDesconto" Width="50px" CssClass="frmborder"
                MaxLength="6"></asp:TextBox>%<br />
            <br />
        </asp:Panel>
        <!-- Valor Desconto -->
        <asp:Panel ID="pnlValorDesconto" runat="server">
            <asp:Label runat="server" ID="lblValorDesconto" Font-Bold="true" Text="Valor do Desconto:"></asp:Label><br />
            <asp:TextBox runat="server" ID="txtValorDesconto" Width="50px" CssClass="frmborder"
                MaxLength="15"></asp:TextBox>
            <br />
            <br />
        </asp:Panel>
        <!-- Frete Gratis -->
        <asp:CheckBox ID="chkFreteGratis" runat="server" Font-Bold="true" Text=" Frete Grátis" />
        <br />
        <br />
        <!-- Botao Inserir -->
        <asp:ImageButton ID="btnInserirFaixa" runat="server" ImageUrl="~/img/btn_executar.gif"
            Width="73" Height="20" BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle"
            CausesValidation="true" OnClick="btnInserirFaixa_Click" ValidationGroup="5" />
        <br />
        <br />
        <!-- Resultado da Pesquisa  -->
        <div id="Div3" runat="server">
            <h4>
                Faixas Adicionadas</h4>
            <asp:Label ID="lblNenhumaFaixa" runat="server" Text="Nenhuma faixa adicionada!" Visible="false" />
            <asp:DataGrid runat="server" Width="500px" ID="dgFaixasPromocao" CellPadding="4"
                ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgFaixasPromocao_DeleteCommand"
                OnItemCreated="grids_ItemCreated" OnItemDataBound="dgFaixasPromocao_ItemDataBound">
                <FooterStyle BackColor="#CCCC99" />
                <EditItemStyle Width="150px" />
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn HeaderText="Quantidade ou Valor Mínimo" DataField="valorMinimo">
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="% Desconto" DataField="percentualDesconto"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Valor do Desconto" DataField="valorDesconto"></asp:BoundColumn>
                    <asp:BoundColumn Visible="false" HeaderText="Frete Grátis" DataField="freteGratis">
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            Frete Grátis</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFreteGratisStr" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInserir" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "promocaoFaixaId") %>'
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
<!-- Implementacao das faixas de desconto - FIM  -->
<br />
<div id="pnlNivelRestricao" visible="false" runat="server">
    <fieldset style="padding: 10px; width: 620px;">
        <legend>Restrições por Nível</legend>
        <br />
        <asp:RadioButtonList ID="rblNivelRestricao" runat="server" OnSelectedIndexChanged="rblNivelRestricao_SelectedIndexChanged"
            AutoPostBack="true">
            <asp:ListItem Text="Nenhuma" Value="NENHUM" Selected="True" />
            <asp:ListItem Text="Usuário" Value="USUARIO" />
            <asp:ListItem Text="Perfil" Value="PERFIL" />
            <asp:ListItem Text="Assinatura Revista" Value="REVISTA" />
        </asp:RadioButtonList>
        <br />
        <asp:HiddenField ID="hddNivelRestricaoAnterior" runat="server" Value="NENHUM" />
        <!-- Painel de Restricoes : Usuarios -->
        <div id="pnlRestricaoNivelUsuario" runat="server" visible="false">
            <!-- Pesquisa -->
            <fieldset style="padding: 10px; width: 600px;">
                <legend>Usuários</legend>
                <h4>
                    Pesquisar</h4>
                <asp:Label runat="server" ID="lblCadastroPessoa" Font-Bold="true" Text="CPF/CNPJ:"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvCadastroPessoa" runat="server" ControlToValidate="txtCadastroPessoa"
                    ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox runat="server" ID="txtCadastroPessoa" Width="250px" CssClass="frmborder"
                    MaxLength="50"></asp:TextBox>
                <asp:ImageButton ID="btnPesquisarUsuario" runat="server" ImageUrl="~/img/btn_buscar.png"
                    Width="64" Height="22" BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle"
                    CausesValidation="true" OnClick="btnPesquisarUsuario_Click" ValidationGroup="2" />
                <br />
                <br />
                <!-- Resultado da Pesquisa -->
                <div id="pnlUsuariosEncontrados" runat="server" visible="false">
                    <h4>
                        Usuários Encontrados</h4>
                    <asp:Label ID="lblTextoPesquisaUsuarios" runat="server" Text="Nenhum usuário encontrado!"
                        Visible="false" />
                    <asp:DataGrid runat="server" Width="500px" ID="dgUsuariosEncontrados" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnEditCommand="dgUsuariosEncontrados_EditCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <EditItemStyle Width="150px" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="Nome" DataField="nomeUsuario"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="CPF/CNPJ" DataField="cadastroPessoa"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInserir" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "usuarioId") %>'
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
                <!-- Usuarios ja adicionados -->
                <div id="pnlUsuariosAdicionados" runat="server">
                    <h4>
                        Usuários Adicionados</h4>
                    <asp:Label ID="lblTextoUsuariosAdicionados" runat="server" Text="Nenhum usuário adicionado!" />
                    <asp:DataGrid runat="server" Width="500px" ID="dgUsuariosAdicionados" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgUsuariosAdicionados_DeleteCommand"
                        OnItemCreated="grids_ItemCreated">
                        <FooterStyle BackColor="#CCCC99" />
                        <EditItemStyle Width="150px" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="Nome" DataField="nomeUsuario"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="CPF/CNPJ" DataField="cadastroPessoa"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInserir" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "usuarioId") %>'
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
        <!-- Painel de Restricoes : Usuarios - FIM -->
        <!-- Painel de Restricoes : Perfis  -->
        <div id="pnlRestricaoNivelPerfil" runat="server" visible="false">
            <!-- Pesquisa -->
            <fieldset style="padding: 10px; width: 600px;">
                <legend>Perfis</legend>
                <!-- Perfis nao incluidos  -->
                <div id="pnlPerfisNaoAdicionados" runat="server">
                    <h4>
                        Perfis</h4>
                    <asp:Label runat="server" ID="lblTextoPerfis"></asp:Label>
                    <asp:DataGrid runat="server" Width="500px" ID="dgPerfisNaoAdicionados" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnEditCommand="dgPerfisNaoAdicionados_EditCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <EditItemStyle Width="150px" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="Nome" DataField="perfilNome"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInserir" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "perfilId") %>'
                                        ImageUrl="~/img/add.png" />
                                </ItemTemplate>
                                <ItemStyle Width="17px" />
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid>
                    <br />
                    <br />
                </div>
                <!-- Usuarios ja adicionados  -->
                <div id="pnlPerfisAdicionados" runat="server">
                    <h4>
                        Perfis Adicionados</h4>
                    <asp:Label ID="lblTextoPerfisAdicionados" runat="server" Text="" />
                    <asp:DataGrid runat="server" Width="500px" ID="dgPerfisAdicionados" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgPerfisAdicionados_DeleteCommand"
                        OnItemCreated="grids_ItemCreated">
                        <FooterStyle BackColor="#CCCC99" />
                        <EditItemStyle Width="150px" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="Nome" DataField="perfilNome"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInserir" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PerfilId") %>'
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
        <!-- Painel de Restricoes : Perfis - FIM  -->
        <!-- Painel de Restricoes : Assinantes Revistas  -->
        <div id="pnlRestricaoNivelRevista" runat="server" visible="false">
            <!-- Pesquisa -->
            <fieldset style="padding: 10px; width: 600px;">
                <legend>Revistas</legend>
                <!-- Revistas nao incluidas -->
                <div id="pnlRevistasNaoAdicionadas" runat="server">
                    <h4>
                        Revistas</h4>
                    <asp:Label runat="server" ID="lblTextoRevistas"></asp:Label>
                    <asp:DataGrid runat="server" Width="500px" ID="dgRevistasNaoAdicionadas" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnEditCommand="dgRevistasNaoAdicionadas_EditCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <EditItemStyle Width="150px" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="Nome" DataField="nomeRevista"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInserir" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RevistaId") %>'
                                        ImageUrl="~/img/add.png" />
                                </ItemTemplate>
                                <ItemStyle Width="17px" />
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid>
                    <br />
                    <br />
                </div>
                <!-- Revistas ja adicionadas -->
                <div id="pnlRevistasAdicionadas" runat="server">
                    <h4>
                        Revistas Adicionadas</h4>
                    <asp:Label ID="lblTextoRevistasAdicionadas" runat="server" Text="" />
                    <asp:DataGrid runat="server" Width="500px" ID="dgRevistasAdicionadas" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgRevistasAdicionadas_DeleteCommand"
                        OnItemCreated="grids_ItemCreated">
                        <FooterStyle BackColor="#CCCC99" />
                        <EditItemStyle Width="150px" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="Nome" DataField="nomeRevista"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInserir" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RevistaId") %>'
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
        <!-- Painel de Restricoes : Assinantes Revistas - FIM  -->
    </fieldset>
</div>
<!-- Painel de Restricoes - Nivel  - FIM  -->
<br />
<!-- Painel de Restricoes - Tipo  -->
<div id="pnlTipoRestricao" visible="false" runat="server">
    <h3>
        Restrições por Tipo</h3>
    <asp:RadioButtonList ID="rblTipoRestricao" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblTipoRestricao_SelectedIndexChanged">
        <asp:ListItem Text="Nenhuma" Value="NENHUM" Selected="True" />
        <asp:ListItem Text="Por Categoria" Value="CATEGORIA" />
        <asp:ListItem Text="Por Produto" Value="PRODUTO" />
        <asp:ListItem Text="Por Tipo de Produto" Value="TIPOPRODUTO" />
    </asp:RadioButtonList>
    <asp:HiddenField ID="hddNivelRestricaoTipoAnterior" runat="server" Value="NENHUM" />
    <!-- Painel de Restricoes : Produto  -->
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
                <asp:Label ID="lblTextoPesquisaProdutos" runat="server" Text="Nenhum usuário encontrado!"
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
    <!-- Painel de Restricoes : Produto - FIM  -->
    <!-- Painel de Restricoes : ProdutoTipo  -->
    <div id="pnlRestricaoProdutoTipo" runat="server" visible="false">
        <!-- Pesquisa -->
        <fieldset style="padding: 10px; width: 600px;">
            <legend>ProdutoTipo</legend>
            <h4>
                Pesquisar</h4>
            <br />
            <!-- Resultado da Pesquisa  -->
            <div id="pnlProdutoTiposEncontrados" runat="server">
                <h4>
                    Tipos de Produto</h4>
                <asp:Label ID="lblTextoPesquisaProdutoTipos" runat="server" Text="Nenhum Tipo de Produto disponível!"
                    Visible="false" />
                <asp:DataGrid runat="server" Width="500px" ID="dgProdutoTiposEncontrados" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnEditCommand="dgProdutoTiposEncontrados_EditCommand">
                    <FooterStyle BackColor="#CCCC99" />
                    <EditItemStyle Width="150px" />
                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn HeaderText="Tipo" DataField="tipo"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInserir" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "produtoTipoId") %>'
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
            <!-- ProdutoTipos ja adicionados  -->
            <div id="Div1">
                <h4>
                    ProdutoTipos Adicionados</h4>
                <asp:Label ID="lblTextoProdutoTiposAdicionados" runat="server" Text="Nenhum Tpo de Produto adicionado!" />
                <asp:DataGrid runat="server" Width="500px" ID="dgProdutoTiposAdicionados" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" OnDeleteCommand="dgProdutoTiposAdicionados_DeleteCommand"
                    OnItemCreated="grids_ItemCreated">
                    <FooterStyle BackColor="#CCCC99" />
                    <EditItemStyle Width="150px" />
                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn HeaderText="Tipo" DataField="tipo"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInserir" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "produtoTipoId") %>'
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
    <!-- Painel de Restricoes : Categoria  -->
    <div id="pnlRestricaoCategoria" runat="server" visible="false">
        <div runat="server" id="divCategorias" class="bordaDiv">
            <asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:"></asp:Label>
            <br />
            <div style="height: 250px; overflow: auto">
                <asp:Repeater runat="server" ID="rptCatNivel1" OnItemDataBound="rptCatNivel1_ItemDataBound">
                    <HeaderTemplate>
                        <ul class="tree">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:CheckBox runat="server" ID="cbCatNivel1" />
                            <asp:Repeater runat="server" ID="rptCatNivel2" OnItemDataBound="rptCatNivel2_ItemDataBound">
                                <HeaderTemplate>
                                    <ul class="tree">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <asp:CheckBox runat="server" ID="cbCatNivel2" />
                                        <asp:Repeater runat="server" ID="rptCatNivel3" OnItemDataBound="rptCatNivel3_ItemDataBound">
                                            <HeaderTemplate>
                                                <ul class="tree">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="cbCatNivel3" />
                                                </li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:Label runat="server" ID="lblDivisao" Text="-----------------------------------------------------------------------------------------" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField runat="server" ID="hddCategorias" Value="" />
                <br />
                <br />
            </div>
        </div>
        <br />
        <asp:ImageButton ID="btnAtualizarCategorias" runat="server" ImageUrl="~/img/btn_executar.gif"
                Width="73" Height="20" BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle"
                CausesValidation="true" OnClick="btnAtualizarCategorias_Click" ValidationGroup="4" />
    </div>
</div>
<!-- Painel de Restricoes - Nivel - FIM  -->
<br />
<br />
<!-- Controles -->

<div id="divCupom" runat="server" visible="false">
    <!-- Pesquisa -->
    <fieldset style="padding: 10px; width: 800px;">
        <legend>Cupons da Promoção</legend>
        <asp:Panel ID="pnlCodigoAmigavel" runat="server" Visible="false">
            <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Código Amigável:"></asp:Label><br />
            <asp:HiddenField runat="server" ID="hddPromocaoCupomId" />
            <asp:TextBox runat="server" ID="txtCogidoAmigavel" Width="200px" CssClass="frmborder"
                MaxLength="20"></asp:TextBox>
            <br />
            <br />
            <asp:ImageButton ID="btnEditarPromocaoCupom" runat="server" ImageUrl="~/img/btn_executar.gif"
            Width="73" Height="20" BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle"
            CausesValidation="true" OnClick="btnEditarPromocaoCupom_Click" ValidationGroup="99" />
            <br />
            <br />
        </asp:Panel>
        <!-- Resultado da Pesquisa  -->
        <div id="Div5" runat="server">
            <h4>
                Cupons Gerados</h4>

            <asp:GridView ID="gvCupom" runat="server" AutoGenerateColumns="False" BackColor="White"
			    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" EmptyDataText="Sem cupons para exibir"
			    ForeColor="Black" GridLines="Vertical" OnRowDataBound="gvCupom_RowDataBound">
                <RowStyle BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False"  />
                <Columns>
                    <asp:TemplateField HeaderText="Cupom">
				        <ItemTemplate>
					        <asp:Label ID="lblCupom" runat="server" Text='' />
				        </ItemTemplate>
				        <ItemStyle Height="20px" Width="250px" HorizontalAlign="Center" VerticalAlign="Middle" />
			        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cupom Amigável">
				        <ItemTemplate>
					        <asp:Label ID="lblAmigavel" runat="server" Text='' />
				        </ItemTemplate>
				        <ItemStyle Height="20px" Width="200px" HorizontalAlign="Center" VerticalAlign="Middle" />
			        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pedido">
				        <ItemTemplate>
					        <asp:Label ID="lblPedido" runat="server" Text='' />
				        </ItemTemplate>
				        <ItemStyle Height="20px" Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
			        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nome">
				        <ItemTemplate>
					        <asp:Label ID="lblNome" runat="server" Text='' />
				        </ItemTemplate>
				        <ItemStyle Height="20px" Width="190px" HorizontalAlign="Center" VerticalAlign="Middle" />
			        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usuário">
				        <ItemTemplate>
					        <asp:Label ID="lblUsuario" runat="server" Text='' />
				        </ItemTemplate>
				        <ItemStyle Height="20px" Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
			        </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
				        <ItemTemplate>
					        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/img/editar.jpg" OnClick="btnEditar_Click" />
				        </ItemTemplate>
				        <ItemStyle Height="20px" Width="17px" HorizontalAlign="Center" VerticalAlign="Middle" />
			        </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
			    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
			    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
			    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
			    <AlternatingRowStyle BackColor="White" />
		    </asp:GridView>
        </div>
    </fieldset>
</div>

<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtValorMinimo']").setMask({ mask: '9999999', type: 'reverse', defaultValue: '0' });
        $("input[id$='txtNumeroMaximoCupom']").setMask({ mask: '999999', type: 'reverse', defaultValue: '0' });
        $("input[id$='txtCogidoAmigavel']").setMask({ mask: '********************', type: 'fixed', defaultValue: '' });

        $("input[id$='chkReutilizavel']").click(function () {
            if ($("input[id$='chkReutilizavel']").attr("checked")) {
                $("div[id$='divAutomatica']").show();
                $("div[id$='divQuantidadeCupons']").hide();
            }
            else {
                $("div[id$='divAutomatica']").hide();
                $("div[id$='divQuantidadeCupons']").show();
            }
        });

        if ($("input[id$='hddNivelRestricaoTipoAnterior']").val() == "CATEGORIA") {
            var categorias = $("input[id$=hddCategorias]").attr("value");
            if (categorias !== "") {
                var vetCategorias = categorias.split(',');

                for (var i = 0; i < (vetCategorias.length - 1); i++) {
                    if (vetCategorias[i] !== "") {
                        var chk = $('input:checkbox[value=' + vetCategorias[i] + ']');
                        chk.attr("checked", true);
                    }
                }
            }
        }

        $("input:checkbox[id$=cbCatNivel1]").click(function () {
            $(this).parent().find("input:checkbox[id$=cbCatNivel2]").attr("checked", $(this).attr("checked"));
            $(this).parent().find("input:checkbox[id$=cbCatNivel3]").attr("checked", $(this).attr("checked"));
        });

        $("input:checkbox[id$=cbCatNivel2]").click(function () {
            $(this).parent().find("input:checkbox[id$=cbCatNivel3]").attr("checked", $(this).attr("checked"));
        });

        $("input:checkbox[id$=cblTitulos_0]").click(function () {
            $("input:checkbox[id*=cblTitulos]").attr("checked", $(this).attr("checked"));
        })
    });
</script>