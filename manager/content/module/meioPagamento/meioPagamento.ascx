<%@ Control Language="C#" AutoEventWireup="true" CodeFile="meioPagamento.ascx.cs"
    Inherits="content_module_meioPagamento_meioPagamento" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtValorMinimo']").setMask({ mask: '99,999.9', type: 'reverse', defaultValue: '000' });
        $("input[id$='txtNumeroParcelas']").setMask({ mask: '999.999', type: 'reverse', defaultValue: '0' });
    });
</script>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMeioPagamentoId"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:RegularExpressionValidator ID="revMeioPagamentoId" runat="server" ControlToValidate="txtMeioPagamentoId"
    ErrorMessage="Somente Números" ValidationGroup="1" ValidationExpression="([0-9]+)"></asp:RegularExpressionValidator><br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Identificador:"></asp:Label>
<br />
<asp:TextBox ID="txtMeioPagamentoId" Width="200px" CssClass="frmborder" MaxLength="9"
    runat="server"></asp:TextBox>
<asp:HiddenField ID="hddMeioPagamentoId" runat="server" Value="" />
<br />
<br />
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Nome Meio de Pagamento:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNome"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox ID="txtNome" Width="200px" CssClass="frmborder" MaxLength="100" runat="server"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Código legado:"></asp:Label>
<br />
<asp:TextBox ID="txtCodigoLegado" Width="200px" CssClass="frmborder" MaxLength="100"
    runat="server"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Código Gateway:"></asp:Label>
<br />
<asp:TextBox ID="txtCodigoGateway" Width="200px" CssClass="frmborder" MaxLength="100" runat="server"></asp:TextBox>
<br />
<br />
<div id="pnlDescontos" runat="server" visible="false">
    <fieldset style="padding: 10px; width: 350px;">
        <legend>Faixas de Preços</legend>
        <div id="pnlFormDesconto" runat="server">
            <asp:HiddenField ID="hddIdGridView" Value="0" runat="server" />
            <br />
            <strong>Valor Mínimo: </strong>
            <br />
            <asp:TextBox ID="txtValorMinimo" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
            <br />
            <strong>Número de Parcelas: </strong>
            <br />
            <asp:TextBox ID="txtNumeroParcelas" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
            <br />
            <strong>Código Legado Faixa: </strong>
            <br />
            <asp:TextBox ID="txtCodigoLegadoFaixa" runat="server" Width="100px" MaxLength="50"></asp:TextBox>
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
        <asp:GridView ID="dgFaixas" runat="server" AutoGenerateColumns="False" DataKeyNames="idGridView,valorMinimo,numeroParcelas,codigoLegadoFaixa"
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" EmptyDataText="Sem valores  para exibir." ForeColor="Black" GridLines="Vertical"
            OnRowDeleting="dgFaixas_RowDeleting" OnSelectedIndexChanged="dgFaixas_SelectedIndexChanged">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Editar">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
                <asp:BoundField HeaderText="Valor Minimo" DataField="valorMinimo" ControlStyle-Width="250px">
                    <ControlStyle Width="100px"></ControlStyle>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Numero de Parcelas" DataField="numeroParcelas" ControlStyle-Width="200px">
                    <ControlStyle Width="100px"></ControlStyle>
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Codigo Legado" DataField="codigoLegadoFaixa" ControlStyle-Width="200px">
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
    <br />
    <asp:CheckBox runat="server" ID="chkAtivo" Text="Ativo" />
    <br />
    <br />
</div>
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
