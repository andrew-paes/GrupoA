<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloCompleto.ascx.cs"
    Inherits="content_module_tituloCompleto_tituloCompleto" %>
<%--Hdd Necessário para guardar estado das tabelas ao salvar dados--%>
<asp:HiddenField runat="server" ID="hddTableTitulo" Value="1" />
<asp:HiddenField runat="server" ID="hddTableComentarioEspecialista" Value="0" />
<asp:HiddenField runat="server" ID="hddTableMaterialComplementar" Value="0" />
<asp:HiddenField runat="server" ID="hddTableAutor" Value="0" />
<asp:HiddenField runat="server" ID="hddTableInfoComplementares" Value="0" />
<asp:Panel ID="pnlTituloImpresso" runat="server">
    <fieldset style="width: 600px;">
        <legend><span id="fieldTitulo" style="cursor: pointer;">[+] Titulo</span></legend>
        <table id="tableTitulo" cellpadding="5" cellspacing="5">
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Título:" />
                    <br />
                    <asp:TextBox ID="txtNomeTitulo" Width="570px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeTitulo"
                        ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Sub-Título:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtSubtituloTitulo" Width="570px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubtituloTitulo"
                        ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="N° de Páginas:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNroPaginasTitulo" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Edição:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtEdicaoTitulo" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Data Lançamento:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtDataLancamentoTitulo" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Data Publicação:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtDataPublicacaoTitulo" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Formato:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtFormatoTitulo" Width="275px" CssClass="frmborder" MaxLength="200"
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
                    <asp:CheckBox ID="chkMaisVendidoTitulo" runat="server" Text="Mais Vendido" Enabled="false" />
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
<asp:Panel ID="Panel2" runat="server">
    <fieldset style="width: 600px;">
        <legend><span id="fieldComentarioEspecialista" style="cursor: pointer;">[+] Comentário
            Especialista</span></legend>
        <table id="tableComentarioEspecialista" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Nome do Especialista:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNomeEspecialista" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    <asp:CustomValidator ID="cvPnlComentarioEspecialista1" ClientValidationFunction="ValidateComentarioEspecialista"
                        runat="server" ValidationGroup="1" Display="Dynamic" Text="Campo Obrigatório" />
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Especialidade:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtEspecialidade" Width="275px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    <asp:CustomValidator ID="cvPnlComentarioEspecialista2" ClientValidationFunction="ValidateComentarioEspecialista"
                        runat="server" ValidationGroup="1" Display="Dynamic" Text="Campo Obrigatório" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Url do vídeo (youtube):"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtUrlComentarioEspecialista" Width="575px" CssClass="frmborder"
                        MaxLength="200" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <span><b>Resumo do Comentário do Especialista:</b></span>
                    <br />
                    <asp:TextBox ID="txtResumoComentario" Width="575px" CssClass="frmborder" TextMode="MultiLine"
                        Rows="3" runat="server" />
                    <asp:CustomValidator ID="cvPnlComentarioEspecialista3" ClientValidationFunction="ValidateComentarioEspecialista"
                        runat="server" ValidationGroup="1" Display="Dynamic" Text="Campo Obrigatório" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <span><b>Texto do Comentário do Especialista:</b></span>
                    <br />
                    <ag2:HtmlTextBox Width="590" runat="server" ID="htmlComentarioEspecialista" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <%--<tr>
                <td colspan="2">
                    <asp:CheckBoxList ID="cblCategorias" runat="server" />
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkDestaqueComentarioEspecialista" runat="server" Text="Destaque" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <span><b>Imagem do especialista (Dimensões 215x160):</b></span>
                    <br />
                    <ag2:ListFiles ID="ListFilesImagemEspecialista" runat="server" TargetFolder="comentarioEspecialista/"
                        Editable="true" MaxFileLength="10000" TipoArquivo="IMAGE" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="Panel3" runat="server">
    <fieldset style="width: 600px;">
        <legend><span id="fieldMaterialComplementar" style="cursor: pointer;">[+] Material Complementar</span></legend>
        <table id="tableMaterialComplementar" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Hotsite:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtUrlConteudoExtra" Width="575px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUrlConteudoExtra"
                        ErrorMessage="URL Inválida" ValidationGroup="1" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span><b>CD/DVD: </b></span>
                    <br />
                    <ag2:HtmlTextBox Width="590" runat="server" ID="htmlInformacaoConteudoExtra" />
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="padding: 10px">
                        <legend>Arquivos Material Complementar</legend>
                        <asp:HiddenField runat="server" ID="hddTituloConteudoExtraArquivoId" Value="0" />
                        <asp:HiddenField runat="server" ID="hddTituloId" Value="0" />
                        <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Título do Arquivo (Ex.: Exercícios Complementares):" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNomeArquivoConteudoExtraArquivo"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2" />
                        <br />
                        <asp:TextBox ID="txtNomeArquivoConteudoExtraArquivo" Width="560px" CssClass="frmborder"
                            MaxLength="300" runat="server" />
                        <br />
                        <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Caminho e Nome do Arquivo (Ex.: pasta/imagem.jpg):" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtArquivoConteudoExtraArquivo"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2" />
                        <br />
                        <asp:TextBox ID="txtArquivoConteudoExtraArquivo" Width="560px" CssClass="frmborder"
                            MaxLength="300" runat="server" />
                        <br />
                        <br />
                        <asp:CheckBox runat="server" ID="chkSomenteLogadoConteudoExtraArquivo" Text="Somente Logado" />
                        <br />
                        <asp:CheckBox runat="server" ID="chkRestritoProfessorConteudoExtraArquivo" Text="Restrito Professor" />
                        <br />
                        <asp:CheckBox runat="server" ID="chkAtivoConteudoExtraArquivo" Text="Ativo" />
                        <br />
                        <br />
                        <asp:ImageButton ID="btnAdicionarConteudoExtraArquivo" runat="server" ImageUrl="~/img/btn_Adicionar.png"
                            Width="73" Height="20" BorderWidth="0" AlternateText="Adicionar" ImageAlign="AbsMiddle"
                            CausesValidation="true" ValidationGroup="2" CssClass="btnSubmitForm" OnClick="btnAdicionarConteudoExtraArquivo_Click" />
                        <br />
                        <br />
                        <asp:Repeater runat="server" ID="rptTituloConteudoExtraArquivo" OnItemDataBound="rptTituloConteudoExtraArquivo_ItemDataBound">
                            <HeaderTemplate>
                                <table width="570px" cellspacing="2px" cellpadding="0px" border="0px">
                                    <tr height="20px">
                                        <td width="40%" style="padding-left: 5px; background-color: #F0F0F0;">
                                            <b>Nome</b>
                                        </td>
                                        <td width="10%" align="center" style="background-color: #F0F0F0;">
                                            <b>Logado</b>
                                        </td>
                                        <td width="10%" align="center" style="background-color: #F0F0F0;">
                                            <b>Professor</b>
                                        </td>
                                        <td width="10%" align="center" style="background-color: #F0F0F0;">
                                            <b>Ativo</b>
                                        </td>
                                        <td width="20%" align="center" style="background-color: #F0F0F0;">
                                            <b>Data Cadastro</b>
                                        </td>
                                        <td width="5%">
                                        </td>
                                        <td width="5%">
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="padding-left: 5px;">
                                        <asp:Literal runat="server" ID="ltrNome" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ltrSomenteLogado" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ltrRestritoProfessor" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ltrAtivo" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ltrDataHoraCadastro" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnEditarConteudoExtraArquivo" runat="server" ImageUrl="~/img/editar.jpg"
                                            OnClick="btnEditarConteudoExtraArquivo_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnExcluirConteudoExtraArquivo" runat="server" ImageUrl="~/img/excluir.jpg"
                                            OnClick="btnExcluirConteudoExtraArquivo_Click" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td style="padding-left: 5px; background-color: #F0F0F0;">
                                        <asp:Literal runat="server" ID="ltrNome" />
                                    </td>
                                    <td align="center" style="background-color: #F0F0F0;">
                                        <asp:Literal runat="server" ID="ltrSomenteLogado" />
                                    </td>
                                    <td align="center" style="background-color: #F0F0F0;">
                                        <asp:Literal runat="server" ID="ltrRestritoProfessor" />
                                    </td>
                                    <td align="center" style="background-color: #F0F0F0;">
                                        <asp:Literal runat="server" ID="ltrAtivo" Text="Sim" />
                                    </td>
                                    <td align="center" style="background-color: #F0F0F0;">
                                        <asp:Literal runat="server" ID="ltrDataHoraCadastro" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnEditarConteudoExtraArquivo" runat="server" ImageUrl="~/img/editar.jpg"
                                            OnClick="btnEditarConteudoExtraArquivo_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnExcluirConteudoExtraArquivo" runat="server" ImageUrl="~/img/excluir.jpg"
                                            OnClick="btnExcluirConteudoExtraArquivo_Click" />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="7">
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </fieldset>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="Panel4" runat="server">
    <fieldset style="width: 600px;">
        <legend><span id="fieldAutor" style="cursor: pointer;">[+] Autores</span></legend>
        <table id="tableAutor" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="Inclusão Rápida de Autor:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtInclusaoAutor" Width="475px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnInclusaoAutor" runat="server" ImageUrl="~/img/btn_Adicionar.png"
                        BorderWidth="0" AlternateText="Incluir Autor" ImageAlign="AbsMiddle" CssClass="btnSubmitForm"
                        OnClick="btnInclusaoAutor_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Pesquisa de Autor:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtPesquisaAutor" Width="475px" CssClass="frmborder" MaxLength="200"
                        runat="server" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnPesquisarAutor" runat="server" ImageUrl="~/img/btn_buscar.png"
                        BorderWidth="0" AlternateText="Pesquisar Autor" ImageAlign="AbsMiddle" CssClass="btnSubmitForm"
                        OnClick="btnPesquisarAutor_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:ListBox CssClass="frmborder" runat="server" Width="200px" Height="300px" ID="lstOrigem"
                                    SelectionMode="Multiple"></asp:ListBox>
                            </td>
                            <td>
                                <div style="vertical-align: top; margin: 10px">
                                    <center>
                                        <asp:Button runat="server" ID="btnAdicionarT" Text=">>" Width="65px" OnClick="btnAdicionarT_Click" />
                                        <br />
                                        <br />
                                        <asp:Button runat="server" ID="btnAdicionar" Text=">" Width="65px" OnClick="btnAdicionar_Click" />
                                    </center>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <div style="vertical-align: bottom;">
                                    <center>
                                        <asp:Button runat="server" ID="btnRemover" Text="<" Width="65px" OnClick="btnRemover_Click" />
                                        <br />
                                        <br />
                                        <asp:Button runat="server" ID="btnRemoverT" Text="<<" Width="65px" OnClick="btnRemoverT_Click" />
                                    </center>
                                </div>
                            </td>
                            <td>
                                <asp:ListBox CssClass="frmborder" runat="server" Width="200px" Height="300px" ID="lstDestino"
                                    SelectionMode="Multiple" Style="float: left"></asp:ListBox>
                            </td>
                            <td valign="top" style="padding-left: 15px;">
                                <asp:ImageButton runat="server" ID="btnAutorSubir" Text="+++" ImageUrl="~/img/btn_seta_up.gif"
                                    OnClick="btnAutorSubir_Click" />
                                <br />
                                <br />
                                <asp:ImageButton runat="server" ID="btnAutorDescer" Text="---" ImageUrl="~/img/btn_seta_down.gif"
                                    OnClick="btnAutorDescer_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>
<br />
<br />
<asp:Panel ID="Panel1" runat="server">
    <fieldset style="width: 600px;">
        <legend><span id="fieldInfoComplementares" style="cursor: pointer;">[+] Informações
            Complementares</span></legend>
        <table id="tableInfoComplementares" cellpadding="5" cellspacing="5" style="display: block;">
            <tr>
                <td>
                    <span><b>Resumo do título:</b></span>
                    <br />
                    <ag2:HtmlTextBox Width="590" runat="server" ID="htmlResumoInfoComplementares" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span><b>Texto sobre sumário:</b></span>
                    <br />
                    <ag2:HtmlTextBox Width="590" runat="server" ID="htmlSumarioInfoComplementares" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span><b>Equipe Revisora:</b></span>
                    <br />
                    <ag2:HtmlTextBox Width="590" runat="server" ID="htmlMaterialInfoComplementares" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span><b>Texto sobre os autores:</b></span>
                    <br />
                    <ag2:HtmlTextBox Width="590" runat="server" ID="htmlInformacaoSobreAutor" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span><b>Nuvem de Palavras:</b></span>
                    <br />
                    <ag2:ListFiles ID="ListFilesImagemResumo" runat="server" TargetFolder="imagensTitulo/"
                        Editable="true" MaxFileLength="10000" TipoArquivo="IMAGE" />
                </td>
            </tr>
            <%--<tr> Solicitação do cliente para não incluir, enviado por e-mail no dia 27/06/2011
                <td>
                    <br />
                    <span><b>Imagem do Autor:</b></span>
                    <br />
                </td>
            </tr>--%>
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
        //SetTable($('#fieldTitulo'), $('#tableTitulo'), 'Titulo');
        $('#fieldTitulo').click(function () {
            var field = $(this);
            var table = $('#tableTitulo');

            SetTable(field, table, "Titulo");

            if ($('input[id$="hddTableTitulo"]').val() == "0") {
                $('input[id$="hddTableTitulo"]').val('1');
            }
            else {
                $('input[id$="hddTableTitulo"]').val('0')
            }
        });

        //SetTable($('#fieldComentarioEspecialista'), $('#tableComentarioEspecialista'), 'Comentário Especialista');
        $('#fieldComentarioEspecialista').click(function () {
            var field = $(this);
            var table = $('#tableComentarioEspecialista');

            SetTable(field, table, "Comentário Especialista");

            if ($('input[id$="hddTableComentarioEspecialista"]').val() == "0") {
                $('input[id$="hddTableComentarioEspecialista"]').val('1');
            }
            else {
                $('input[id$="hddTableComentarioEspecialista"]').val('0')
            }
        });

        //SetTable($('#fieldMaterialComplementar'), $('#tableMaterialComplementar'), 'Material Complementar');
        $('#fieldMaterialComplementar').click(function () {
            var field = $(this);
            var table = $('#tableMaterialComplementar');

            SetTable(field, table, "Material Complementar");

            if ($('input[id$="hddTableMaterialComplementar"]').val() == "0") {
                $('input[id$="hddTableMaterialComplementar"]').val('1');
            }
            else {
                $('input[id$="hddTableMaterialComplementar"]').val('0')
            }
        });

        //SetTable($('#fieldAutor'), $('#tableAutor'), "Autores");
        $('#fieldAutor').click(function () {
            var field = $(this);
            var table = $('#tableAutor');

            SetTable(field, table, "Autores");

            if ($('input[id$="hddTableAutor"]').val() == "0") {
                $('input[id$="hddTableAutor"]').val('1');
            }
            else {
                $('input[id$="hddTableAutor"]').val('0')
            }
        });

        //setTimeout("SetTable($('#fieldInfoComplementares'), $('#tableInfoComplementares'), 'Informações Complementares');", 2000);
        //SetTable($('#fieldInfoComplementares'), $('#tableInfoComplementares'), 'Informações Complementares');
        $('#fieldInfoComplementares').click(function () {
            var field = $(this);
            var table = $('#tableInfoComplementares');

            SetTable(field, table, "Informações Complementares");

            if ($('input[id$="hddTableInfoComplementares"]').val() == "0") {
                $('input[id$="hddTableInfoComplementares"]').val('1');
            }
            else {
                $('input[id$="hddTableInfoComplementares"]').val('0')
            }
        });

        $('textarea[id$="txtResumoComentario"]').keyup(function () {
            var max = 300;
            if ($(this).val().length > max) {
                $(this).val($(this).val().substr(0, max));
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

    function ValidateComentarioEspecialista(oSrc, args) {
        var flag = false;

        if (
            ($('input[id$="txtNomeEspecialista"]').val() == "" && $('input[id$="txtEspecialidade"]').val() == "" && $('textarea[id$="txtResumoComentario"]').val() == "")
            ||
            ($('input[id$="txtNomeEspecialista"]').val() != "" && $('input[id$="txtEspecialidade"]').val() != "" && $('textarea[id$="txtResumoComentario"]').val() != "")
            ) {
            flag = true;
        }
        else {
            flag = false;
        }

        args.IsValid = flag;
    }
	//]]>
</script>
