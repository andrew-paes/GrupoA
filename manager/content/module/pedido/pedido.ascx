<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pedido.ascx.cs" Inherits="content_module_pedido_pedido" %>
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="N° do Pedido:" />
<br />
<asp:TextBox ID="txtPedidoId" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label24" Font-Bold="true" Text="Status do Pedido:" />
<br />
<asp:TextBox ID="txtPedidoStatus" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Data do Pedido:" />
<br />
<asp:TextBox ID="txtPedidoData" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Data de Sincronização de Pedido:" />
<br />
<asp:TextBox ID="txtPedidoDataSincronizacao" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Valor do Frete:" />
<br />
<asp:TextBox ID="txtPedidoValorFrete" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Valor do Pedido:" />
<br />
<asp:TextBox ID="txtPedidoValor" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Código do pedido:" />
<br />
<asp:TextBox ID="txtPedidoCodigo" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Panel ID="pnlUsuario" runat="server" Visible="true">
    <fieldset style="width: 600px;">
        <legend><span id="fieldTituloImpresso" style="cursor: pointer;">Dados do Cliente</span></legend>
        <table id="tableTituloImpresso" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Nome:" />
                    <br />
                    <asp:TextBox ID="txtUsuarioNome" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="CPF/CNPJ:" />
                    <br />
                    <asp:TextBox ID="txtUsuarioCadastroPessoa" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Email:" />
                    <br />
                    <asp:TextBox ID="txtUsuarioEmail" Width="570px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HyperLink ID="hpLnkUsuarioCompleto" Target="_blank" runat="server">Verificar Dados Completo do Usuário</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="Panel1" runat="server" Visible="true">
    <fieldset style="width: 600px;">
        <legend><span id="Span1" style="cursor: pointer;">Dados do Pagamento</span></legend>
        <table id="table1" cellpadding="5" cellspacing="5">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Forma Pagamento:" />
                    <br />
                    <asp:TextBox ID="txtPedidoMeioPagamentoNome" Width="570px" CssClass="frmborder" MaxLength="50"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="N° Parcelas:" />
                    <br />
                    <asp:TextBox ID="txtPagamentoNroParcelas" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Código da Transação:" />
                    <br />
                    <asp:TextBox ID="txtPagamentoCodigoTransacao" Width="275px" CssClass="frmborder"
                        MaxLength="200" runat="server" Enabled="false" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="Panel2" runat="server" Visible="true">
    <fieldset style="width: 600px;">
        <legend><span id="Span2" style="cursor: pointer;">Dados de Entrega</span></legend>
        <table id="table2" cellpadding="5" cellspacing="5">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Tipo:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoTipo" Width="570px" CssClass="frmborder" MaxLength="50"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="Estado:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoEstado" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="Município:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoMunicipio" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="CEP:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoCEP" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Bairro:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoBairro" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Logradouro:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoLogradouro" Width="570px" CssClass="frmborder" MaxLength="50"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Text="Número:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoNro" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Complemento:" />
                    <br />
                    <asp:TextBox ID="txtEnderecoComplemento" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="Panel3" runat="server" Visible="true">
    <fieldset style="width: 1250px;">
        <legend><span id="Span3" style="cursor: pointer;">Itens do Pedido</span></legend>
        <asp:Repeater runat="server" ID="rptPedidoItem" OnItemDataBound="rptPedidoItem_ItemDataBound">
            <HeaderTemplate>
                <table width="1245px" cellspacing="2px" cellpadding="0px" border="0px">
                    <tr height="20px">
                        <td width="7%" align="center" style="background-color: #F0F0F0;">
                            <b>ISBN</b>
                        </td>
                        <td width="12%" align="center" style="background-color: #F0F0F0;">
                            <b>Area Conhecimento</b>
                        </td>
                        <td width="10%" align="center" style="background-color: #F0F0F0;">
                            <b>Tipo do Produto</b>
                        </td>
                        <td width="20%" align="center" style="background-color: #F0F0F0;">
                            <b>Nome Produto</b>
                        </td>
                        <td width="5%" align="center" style="background-color: #F0F0F0;">
                            <b>Quant</b>
                        </td>
                        <td width="8%" align="center" style="background-color: #F0F0F0;">
                            <b>Valor</b>
                        </td>
                        <td width="%" align="center" style="background-color: #F0F0F0;">
                            <b>Valor com Desc</b>
                        </td>
                        <td width="7%" align="center" style="background-color: #F0F0F0;">
                            <b>Valor do Desc</b>
                        </td>
                        <td width="7%" align="center" style="background-color: #F0F0F0;">
                            <b>Valor Total</b>
                        </td>
                        <td width="12%" align="center" style="padding-left: 5px; background-color: #F0F0F0;">
                            <b>Promoção</b>
                        </td>
                        <td width="7%" align="center" style="background-color: #F0F0F0;">
                            <b>Frete Grátis</b>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="right" style="padding-right: 5px;">
                        <asp:Literal runat="server" ID="ltrISBN13" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrAreaConhecimento" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrProdutoTipo" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:HyperLink ID="hpLnkProdutoNome" Target="_blank" runat="server" />
                    </td>
                    <td align="right" style="padding-right: 5px;">
                        <asp:Literal runat="server" ID="ltrPedidoItemQuantidade" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorBase" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorBaseDesconto" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorDesconto" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorTotal" />
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:HyperLink ID="hpLnkPromocao" Target="_blank" runat="server" />
                    </td>
                    <td align="center" style="padding-left: 5px;">
                        <asp:Literal runat="server" ID="ltrPedidoItemFreteGratis" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td align="right" style="padding-right: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrISBN13" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrAreaConhecimento" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrProdutoTipo" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:HyperLink ID="hpLnkProdutoNome" Target="_blank" runat="server" Style="font: #000000;" />
                    </td>
                    <td align="right" style="padding-right: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrPedidoItemQuantidade" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorBase" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorBaseDesconto" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorDesconto" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrPedidoItemValorTotal" />
                    </td>
                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:HyperLink ID="hpLnkPromocao" Target="_blank" runat="server" />
                    </td>
                    <td align="center" style="padding-left: 5px; background-color: #F0F0F0;">
                        <asp:Literal runat="server" ID="ltrPedidoItemFreteGratis" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="9">
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Panel>
<br />
<br />
<div style="vertical-align: bottom; text-align: right;">
    <img style="border: 0; cursor: pointer;" id="btnListar" runat="server" src="~/img/btn_listar.gif"
        width="53" height="20" alt="Executar" onclick="history.back();" />
</div>
<%--<asp:HyperLink ID="hpLnkUsuarioCompleto" CssClass="hpLnkUsuarioCompleto" NavigateUrl="javascript:;"
    runat="server">Verificar Dados Completo do Usuário</asp:HyperLink>--%>
<script type="text/javascript">
    $(document).ready(function () {

        $('.hpLnkUsuarioCompleto').click(function () {
            alert('Buu');
            var urlPath = '';
            var registroId = $(this).prev().prev();
            var hdnModuleName = 'tituloProduto';
            var hdnScriptModal = 'tituloProdutoImagem.aspx';

            if ($(hdnScriptModal).val() != '')
                urlPath = 'module/tituloProduto/tituloProdutoImagem.aspx';
            else
                urlPath = 'module/modalArquivo/modalArquivo.aspx';

            $.fancybox({
                'type': 'iframe',
                'href': urlPath + '?id=' +
                                '2',
                'width': 400,
                'height': 250,
                'autoScale': false,
                'scrolling': 'no',
                'centerOnScroll': true,
                'overlayOpacity': 0.5,
                'hideOnOverlayClick': false,
                'showCloseButton': false
            });

        });

        $('.btnCancelarModal').click(function () {

            window.parent.globalArquivoId = 0;
            window.parent.$.fancybox.close();
        });
    });
</script>
