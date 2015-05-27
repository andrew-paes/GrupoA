<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloProduto.ascx.cs"
    Inherits="content_module_tituloProduto_tituloProduto" %>
<asp:HiddenField runat="server" ID="hddTableTituloImpresso" Value="1" />
<asp:HiddenField runat="server" ID="hddTableTituloEletronico" Value="1" />
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Nome Título:" />
<br />
<asp:TextBox ID="txtTituloLivro" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Sub-Título:"></asp:Label>
<br />
<asp:TextBox ID="txtSubtituloLivro" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label ID="Label18" runat="server" Font-Bold="True" Text="Edição:"></asp:Label>
<br />
<asp:TextBox ID="txtEdicaoLivro" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Data Lançamento:"></asp:Label>
<br />
<asp:TextBox ID="txtDataLancamentoLivro" Width="280px" CssClass="frmborder" MaxLength="200"
    runat="server" Enabled="false" />
<br />
<br />
<asp:Repeater runat="server" ID="rptAutor" OnItemDataBound="rptAutor_ItemDataBound">
    <HeaderTemplate>
        <table width="292px" cellspacing="2px" cellpadding="0px" border="0px">
            <tr height="20px">
                <td width="20%" align="center" style="background-color: #F0F0F0;">
                    <b>ID</b>
                </td>
                <td width="80%" style="padding-left: 5px; background-color: #F0F0F0;">
                    <b>Nome Autor</b>
                </td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td style="padding-left: 5px;">
                <asp:Literal runat="server" ID="ltrAutorId" />
            </td>
            <td style="padding-left: 5px;">
                <asp:Literal runat="server" ID="ltrAutorNome" />
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr>
            <td style="padding-left: 5px;">
                <asp:Literal runat="server" ID="ltrAutorId" />
            </td>
            <td style="padding-left: 5px;">
                <asp:Literal runat="server" ID="ltrAutorNome" />
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        </table>
    </FooterTemplate>
</asp:Repeater>
<br />
<br />
<asp:Panel ID="pnlTituloImpresso" runat="server" Visible="false">
    <fieldset style="width: 600px;">
        <legend><span id="fieldTituloImpresso" style="cursor: pointer;">[+] Produto Titulo Impresso</span></legend>
        <table id="tableTituloImpresso" cellpadding="5" cellspacing="5">
            <tr>
                <td colspan="2">
                    Ao salvar os dados do fomulário, o nome do Título irá sobrescrever o nome do produto
                    Título Impresso.
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Nome Titulo Impresso:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNomeTituloImpresso" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                    <asp:RequiredFieldValidator ID="rfvNomeTituloImpresso" runat="server" ControlToValidate="txtNomeTituloImpresso"
                        ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Código:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtCodigoTituloImpresso" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="Área de Conhecimento:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtAreaTituloImpresso" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ISBN 13:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtISBN13TituloImpresso" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Formato:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtFormatoTituloImpresso" Width="150px" CssClass="frmborder" MaxLength="50"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Valor Unitario:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtValorUnitarioTituloImpresso" Width="150px" CssClass="frmborder"
                        MaxLength="200" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Valor Oferta:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtValorOfertaTituloImpresso" Width="150px" CssClass="frmborder"
                        MaxLength="200" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Peso:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtPesoTituloImpresso" Width="150px" CssClass="frmborder" MaxLength="200"
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
                    <asp:CheckBox ID="chkUtilizarFreteTituloImpresso" runat="server" Text="Utitlizar Frete"
                        Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkDisponivelTituloImpresso" runat="server" Text="Disponivel" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkExibirSiteTituloImpresso" runat="server" Text="Exibir no Site"
                        Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkHomologadoTituloImpresso" runat="server" Text="Homologado" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ag2:ListFiles ID="ListFilesTituloImpresso" runat="server" TargetFolder="imagensTitulo/"
                        Editable="true" MaxFileLength="5000" ScriptModal="tituloProdutoImagem.aspx" MultiFile="true"
                        TipoArquivo="ALL" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="pnlTituloEletronico" runat="server" Visible="false">
    <fieldset style="width: 600px;">
        <legend><span id="fieldTituloEletronico" style="cursor: pointer;">[+] Produto Titulo
            eBook</span></legend>
        <table id="tableTituloEletronico" cellpadding="5" cellspacing="5">
            <tr>
                <td colspan="2">
                    Ao salvar os dados do fomulário, o nome do Título irá sobrescrever o nome do produto
                    Título eBook.
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Nome Titulo eBook:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNomeTituloEletronico" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Código:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtCodigoTituloEletronico" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Área de Conhecimento:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtAreaTituloEletronico" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="ISBN 13:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtISBN13TituloEletronico" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="Formato:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtFormatoTituloEletronico" Width="150px" CssClass="frmborder" MaxLength="50"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Valor Unitario:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtValorUnitarioTituloEletronico" Width="150px" CssClass="frmborder"
                        MaxLength="200" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Valor Oferta:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtValorOfertaTituloEletronico" Width="150px" CssClass="frmborder"
                        MaxLength="200" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkDisponivelTituloEletronico" runat="server" Text="Disponivel"
                        Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkExibirSiteTituloEletronico" runat="server" Text="Exibir no Site"
                        Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkHomologadoTituloEletronico" runat="server" Text="Homologado" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ag2:ListFiles ID="ListFilesTituloEletronico" runat="server" TargetFolder="imagensTitulo/"
                        Editable="true" MaxFileLength="5000" ScriptModal="tituloProdutoImagem.aspx" MultiFile="true"
                        TipoArquivo="ALL" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<script type="text/javascript">
	//<![CDATA[
    $(document).ready(function () {
        //SetTable($('#fieldTituloImpresso'), $('#tableTituloImpresso'), "Titulo Impresso");
        $('#fieldTituloImpresso').click(function () {
            var field = $(this);
            var table = $('#tableTituloImpresso');

            SetTable(field, table, "Titulo Impresso");

            if ($('input[id$="hddTableTituloImpresso"]').val() == "0") {
                $('input[id$="hddTableTituloImpresso"]').val('1');
            }
            else {
                $('input[id$="hddTableTituloImpresso"]').val('0')
            }
        });

        //SetTable($('#fieldTituloEletronico'), $('#tableTituloEletronico'), "Titulo eBook");
        $('#fieldTituloEletronico').click(function () {
            var field = $(this);
            var table = $('#tableTituloEletronico');

            SetTable(field, table, "Titulo eBook");

            if ($('input[id$="hddTableTituloEletronico"]').val() == "0") {
                $('input[id$="hddTableTituloEletronico"]').val('1');
            }
            else {
                $('input[id$="hddTableTituloEletronico"]').val('0')
            }
        });
    });

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
	//]]>
</script>
