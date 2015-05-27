<%@ Control Language="C#" AutoEventWireup="true" CodeFile="compraConjunta.ascx.cs"
    Inherits="content_module_compraConjunta_compraconjunta" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtISBN13']").setMask('9999999999999');
        $("input[id$='txtEstoqueSeguranca']").setMask({ mask: '999.999', type: 'reverse', defaultValue: '0' });
        $("input[id$='txtQuantidadeLimite']").setMask({ mask: '999.999', type: 'reverse', defaultValue: '' });
        $("input[id$='txtQuantidadeMinima']").setMask({ mask: '999.999', type: 'reverse', defaultValue: '0' });
        $("input[id$='txtPercentualDesconto']").setMask({ mask: '99,99', type: 'reverse', defaultValue: '000' });
        //$("input[id$='txtHoraInicialCompra']").setMask({ mask: '99:99', type: 'reverse', defaultValue: '00:00' });

        $("input[id$='txtHoraInicialCompra']").setMask("29:59").keypress(function () {
            var currentMask = $(this).data('mask').mask;
            var newMask = $(this).val().match(/^2.*/) ? "23:59" : "29:59";

            if (newMask != currentMask) {
                $(this).setMask(newMask);
            }
        });

        $("input[id$='txtHoraFinalCompra']").setMask("29:59").keypress(function () {
            var currentMask = $(this).data('mask').mask;
            var newMask = $(this).val().match(/^2.*/) ? "23:59" : "29:59";

            if (newMask != currentMask) {
                $(this).setMask(newMask);
            }
        });
    });
</script>
<asp:HiddenField ID="hddCompraConjuntaId" runat="server" Value="0" />
<asp:HiddenField ID="hddProdutoId" runat="server" Value="0" />
<asp:HiddenField ID="hddEstoqueSegurancaAnterior" runat="server" />
<asp:HiddenField ID="hddDataFinalCompra" runat="server" Value="0" />
<%--
<asp:Label runat="server" ID="lblTipoTitulo" Font-Bold="true" Text="Tipo Título:" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="1"
    ControlToValidate="ddlTipoTitulo" ErrorMessage="Campo Obrigatório" />
<br />
<asp:DropDownList ID="ddlTipoTitulo" runat="Server" CssClass="frmborder">
    <asp:ListItem Text="Selecione..." Value="0" />
    <asp:ListItem Text="Título Impresso" Value="1" />
    <asp:ListItem Text="Título Eletrônico" Value="2" />
</asp:DropDownList>
<br />
<br />
--%>
<asp:Label runat="server" ID="lblISBN13" Font-Bold="true" Text="Filtros ISBN13:" />
<table width="420px" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:TextBox runat="server" ID="txtISBN13" Width="300px" CssClass="frmborder" MaxLength="128"></asp:TextBox>
        </td>
        <td>
            &nbsp;<asp:Button runat="server" ID="btnPesquisar" Text="Pesquisar" Width="90px"
                OnClick="btnPesquisar_Click" />
        </td>
    </tr>
</table>
<br />
<asp:Label runat="server" ID="lblTitulo" Font-Bold="true" Text="Título:" />
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="300px" CssClass="frmborder" MaxLength="150"
    Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="lblValorUnitario" Font-Bold="true" Text="Valor Unitário:" />
<br />
<asp:TextBox runat="server" ID="txtValorUnitario" CssClass="frmborder" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="lblValorOferta" Font-Bold="true" Text="Valor Oferta:" />
<br />
<asp:TextBox runat="server" ID="txtValorOferta" CssClass="frmborder" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="lblDataPublicacao" Font-Bold="true" Text="Data Publicação:" />
<br />
<asp:TextBox runat="server" ID="txtDataPublicacao" CssClass="frmborder" Enabled="false" />
<br />
<br />
<asp:CustomValidator runat="server" ID="cvValidarDatasPublicacao" OnServerValidate="cvValidarDatasPublicacao_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:Label runat="server" ID="lblDataInicialCompra" Font-Bold="true" Text="Data Inicial de Compra:" /><br />
<ag2:DateField runat="server" ID="txtDataInicialCompra" CssClass="frmborder" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
    ControlToValidate="txtDataInicialCompra" ErrorMessage="Campo Obrigatório" />
<br />
<br />
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Hora Inicial de Compra (Ex.: 00:00):" />
<br />
<asp:TextBox runat="server" ID="txtHoraInicialCompra" CssClass="frmborder" Width="30"
    MaxLength="5" />
<br />
<br />
<asp:Label runat="server" ID="lblDataFinalCompra" Font-Bold="true" Text="Data Final de Compra:" /><br />
<ag2:DateField runat="server" ID="txtDataFinalCompra" CssClass="frmborder" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
    ControlToValidate="txtDataFinalCompra" ErrorMessage="Campo Obrigatório" /><br />
<br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Hora Final de Compra (Ex.: 23:59):" />
<br />
<asp:TextBox runat="server" ID="txtHoraFinalCompra" CssClass="frmborder" Width="30"
    MaxLength="5" />
<br />
<br />
<asp:Label runat="server" ID="lblEstoqueSeguranca" Font-Bold="true" Text="Estoque de Segurança:" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
    ControlToValidate="txtEstoqueSeguranca" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox runat="server" ID="txtEstoqueSeguranca" CssClass="frmborder" />
<br />
<br />
<asp:Label runat="server" ID="lblQuantidadeLimite" Font-Bold="true" Text="Quantidade Limite por Pedido:" />
<br />
<asp:TextBox runat="server" ID="txtQuantidadeLimite" CssClass="frmborder" />
<br />
<br />
<asp:Label runat="server" ID="lblCompraConjuntaStatus" Font-Bold="true" Text="Status:" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1"
    ControlToValidate="ddlCompraConjuntaStatus" ErrorMessage="Campo Obrigatório" />
<br />
<asp:DropDownList ID="ddlCompraConjuntaStatus" runat="server" CssClass="frmborder"
    Enabled="false">
</asp:DropDownList>
<br />
<br />
<asp:Label runat="server" ID="lblAtivo" Font-Bold="true" Text="Ativo:" Visible="false" />
<br />
<asp:CheckBox ID="chkAtivo" runat="server" Visible="false" />
<br />
<br />
<asp:Label runat="server" ID="lblDataHoraFinalizacao" Font-Bold="true" Text="Data de Finalização de Compra:"
    Visible="false" /><br />
<asp:TextBox runat="server" ID="txtDataHoraFinalizacao" CssClass="frmborder" Enabled="false"
    Visible="false" />
<br />
<div id="pnlDescontos" runat="server" visible="false">
    <fieldset style="padding: 10px; width: 350px;">
        <legend>Descontos</legend>
        <div id="pnlFormDesconto" runat="server">
            <asp:HiddenField ID="hddIdGridView" Value="0" runat="server" />
            <br />
            <strong>Quantidade Mínima: </strong>
            <br />
            <asp:TextBox ID="txtQuantidadeMinima" runat="server" Width="100px" MaxLength="200"></asp:TextBox>
            <br />
            <strong>Percentual Desconto: </strong>
            <br />
            <asp:TextBox ID="txtPercentualDesconto" runat="server" Width="100px" MaxLength="200"></asp:TextBox>
            <br />
            <div class="boxesc" style="background-color: #FFFFFF !important; vertical-align: bottom;">
                <asp:ImageButton ID="btnIncluir" runat="server" ImageUrl="~/img/btn_adicionar.png"
                    BorderWidth="0" AlternateText="Adicionar" ImageAlign="AbsMiddle" CausesValidation="true"
                    OnClick="btnIncluir_Click" />
                <br />
            </div>
        </div>
        <strong>Lista: </strong>
        <br />
        <asp:GridView ID="gdvQuantidadeDesconto" runat="server" AutoGenerateColumns="False"
            DataKeyNames="idGridView,QuantidadeMinima,PercentualDesconto" BackColor="White"
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" EmptyDataText="Sem descontos para exibir"
            ForeColor="Black" GridLines="Vertical" OnRowDeleting="gdvQuantidadeDesconto_RowDeleting"
            OnSelectedIndexChanged="gdvQuantidadeDesconto_SelectedIndexChanged">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Editar">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
                <asp:BoundField HeaderText="Quantidade Minima" DataField="QuantidadeMinima" ControlStyle-Width="250px">
                    <ControlStyle Width="100px"></ControlStyle>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Percentual Desconto" DataField="PercentualDesconto" ControlStyle-Width="200px">
                    <ControlStyle Width="100px"></ControlStyle>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Excluir">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
    </fieldset>
</div>
<br />
<br />
<div id="pnlCancelar" runat="server" visible="false">
    <fieldset style="padding: 10px; width: 350px;">
        <legend>Cancelar Compra Conjunta</legend>
        <div id="pnlFormCancelar">
            <table width="100%">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkCancelar" runat="server" />
                    </td>
                    <td width="70%" align="right">
                        <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/img/btn_executar.gif"
                            Width="73" Height="20" BorderWidth="0" AlternateText="Cancelar" ImageAlign="AbsMiddle"
                            CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</div>
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
