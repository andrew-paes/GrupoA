<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oferta.ascx.cs" Inherits="content_module_oferta_oferta" %>
<script type="text/javascript">
    $(document).ready(function () {
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

        $("select[id$=ddlFormaOferta]").change(function (e) {
            controlaCampoForma();

            e.preventDefault();
        });

        $("select[id$='ddlTipo']").change(function (e) {
            if ($("select[id$=ddlTipo]").attr("value") == 1) {
                $("div[id$=divTitulos]").show();
                $("div[id$=divCategorias]").hide();
                $("select[id$=ddlFormaOferta]").attr("disabled", false);
                $("input[id$=hddFormaOferta]").attr("value", "0");

                controlaCampoForma();
            }
            else if ($("select[id$=ddlTipo]").attr("value") == 3) {
                $("div[id$=divTitulos]").hide();
                $("div[id$=divCategorias]").show();

                $("select[id$=ddlFormaOferta]").attr("value", "1");
                $("input[id$=hddFormaOferta]").attr("value", "1");
                $("select[id$=ddlFormaOferta]").attr("disabled", true);

                controlaCampoForma();
            }
            else {
                $("div[id$=divTitulos]").hide();
                $("div[id$=divCategorias]").hide();
                $("select[id$=ddlFormaOferta]").attr("value", "1");
                $("input[id$=hddFormaOferta]").attr("value", "1");
                $("select[id$=ddlFormaOferta]").attr("disabled", true);

                controlaCampoForma();
            }

            e.preventDefault();
        });

        if ($("select[id$=ddlTipo]").attr("value") == 1) {
            $("div[id$=divTitulos]").show();
            $("div[id$=divCategorias]").hide();
            $("select[id$=ddlFormaOferta]").attr("disabled", false);
            $("input[id$=hddFormaOferta]").attr("value", "0");
        }
        else if ($("select[id$=ddlTipo]").attr("value") == 3) {
            $("div[id$=divTitulos]").hide();
            $("div[id$=divCategorias]").show();

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

            $("select[id$=ddlFormaOferta]").attr("value", "1");
            $("input[id$=hddFormaOferta]").attr("value", "1");
            $("select[id$=ddlFormaOferta]").attr("disabled", true);

            controlaCampoForma();
        }
        else {
            $("div[id$=divTitulos]").hide();
            $("div[id$=divCategorias]").hide();

            $("select[id$=ddlFormaOferta]").attr("value", "1");
            $("input[id$=hddFormaOferta]").attr("value", "1");
            $("select[id$=ddlFormaOferta]").attr("disabled", true);

            controlaCampoForma();
        }

        controlaCampoForma();

        $("input[id$='txtPercentual']").setMask({ mask: '99,99', type: 'reverse', defaultValue: '000' });
        $("input[id$='txtPreco']").setMask({ mask: '99,99', type: 'reverse', defaultValue: '000' });
    });

    function controlaCampoForma() {
        if ($("select[id$=ddlFormaOferta]").attr("value") == '0') {
            $("div[id$=divPreco]").show();
            $("div[id$=divPercentual]").hide();
            $("input[id$=hddFormaOferta]").attr("value", "0");
        }
        else {
            $("div[id$=divPreco]").hide();
            $("div[id$=divPercentual]").show();
            $("input[id$=hddFormaOferta]").attr("value", "1");
        }
    }
</script>
<strong>Tipo:</strong>
<br />
<asp:DropDownList ID="ddlTipo" runat="server" Width="200px" />
<br />
<br />
<div runat="server" id="divTitulos" class="bordaDiv">
    <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Títulos"></asp:Label>
    <br />
    <asp:TextBox runat="server" ID="txtFiltroTitulo" MaxLength="200" Width="250px"></asp:TextBox>
    <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/img/btn_filtrar.gif"
        BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle" OnClick="btnPesquisar_Click"
        CssClass="btnSubmitForm" />
    <br />
    <div runat="server" id="divTitulosNovos" style="height: 150px; overflow: auto;" visible="false">
        <asp:CheckBoxList ID="cblTitulos" runat="server" />
    </div>
    <asp:ImageButton ID="btnSalvar" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Salvar" ImageAlign="AbsMiddle"
        OnClick="btnSalvar_Click" CssClass="btnSubmitForm" />
    <br />
    <br />
    <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Títulos Adicionados:"></asp:Label>
    <br />
    <div runat="server" id="divTitulosAdiocionados" style="height: 150px; overflow: auto;">
        <asp:Repeater runat="server" ID="rptTitulos" OnItemDataBound="rptTitulos_ItemDataBound">
            <ItemTemplate>
                <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/img/delete.png" AlternateText="Excluir"
                    OnClick="btnExcluir_Click" CssClass="btnSubmitForm" />
                <asp:Literal runat="server" ID="ltTitulo"></asp:Literal>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br />
    <br />
</div>
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
<asp:CustomValidator runat="server" ID="cvValidarTipo" OnServerValidate="cvValidarTipo_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:DropDownList runat="server" ID="ddlFormaOferta">
    <asp:ListItem Value="0" Text="Preço"></asp:ListItem>
    <asp:ListItem Value="1" Text="Percentual"></asp:ListItem>
</asp:DropDownList>
<asp:HiddenField runat="server" ID="hddFormaOferta" />
<div id="divPercentual">
    <asp:TextBox runat="server" ID="txtPercentual" Width="30px" CssClass="frmborder"
        MaxLength="100" />%
    
</div>
<div id="divPreco">
    <asp:TextBox runat="server" ID="txtPreco" Width="30px" CssClass="frmborder" MaxLength="100" />
</div>
<asp:CustomValidator runat="server" ID="cvValidarOferta" OnServerValidate="cvValidarOferta_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<strong>Nome:</strong><br />
<asp:TextBox runat="server" ID="txtNome" Width="300px" CssClass="frmborder" MaxLength="250" />
<br />
<br />
<strong>Data Início:</strong><br />
<ag2:DateField runat="server" ID="txtDataInicio" CssClass="frmborder" />
<br />
<br />
<strong>Data Fim:</strong><br />
<ag2:DateField runat="server" ID="txtDataFim" CssClass="frmborder" />
<br />
<asp:CustomValidator runat="server" ID="cvValidarDatasPublicacao" OnServerValidate="cvValidarDatasPublicacao_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<asp:HiddenField ID="hddOfertaId" runat="server" Value="0" />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
