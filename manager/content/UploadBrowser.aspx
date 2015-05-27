<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadBrowser.aspx.cs" Inherits="content_UploadBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager</title>
    <link href="../css/uploadBrowser.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="../js/uploadBrowser.js"></script>

    <script type="text/javascript" src="../js/jquery.contextMenu.js"></script>

    <script type="text/javascript" src="../js/jquery.filestyle.js"></script>

    <script type="text/javascript" src="../js/jquery.url.packed.js"></script>

    <link href="../css/jquery.contextMenu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdnControleAccord" runat="server" Value="accFiltro" />
    <div class="divConteudo">
        <div class="divColuna1">
            <div class="accordion">
                <div class="itemAccordion">
                    Filtros
                </div>
                <div id="accFiltro" class="itemAccordionConteudo" style="display: none;">
                    <div>
                        <asp:LinkButton ID="lnkExecel" CssClass="lnkMenu" CommandArgument="excel" runat="server">Documentos Excel</asp:LinkButton>
                        <asp:LinkButton ID="lnkPdf" CssClass="lnkMenu" CommandArgument="pdf" runat="server">Documentos Pdf</asp:LinkButton>
                        <asp:LinkButton ID="lnkPowerPoint" CssClass="lnkMenu" CommandArgument="ppt" runat="server">Documentos PowerPoint</asp:LinkButton>
                        <asp:LinkButton ID="lnkWord" CssClass="lnkMenu" CommandArgument="doc" runat="server">Documentos Word</asp:LinkButton>
                        <asp:LinkButton ID="lnkZip" CssClass="lnkMenu" CommandArgument="zip" runat="server">Documentos Zip</asp:LinkButton>
                        <asp:LinkButton ID="lnkImg" CssClass="lnkMenu" CommandArgument="images" runat="server">Imagens</asp:LinkButton>
                        <asp:LinkButton ID="lnkOutros" CssClass="lnkMenu" CommandArgument="outros" runat="server">Outros</asp:LinkButton>
                        <asp:LinkButton ID="lnkTodos" CssClass="lnkMenu" CommandArgument="todos" runat="server">Todos</asp:LinkButton>
                    </div>
                </div>
                <div class="itemAccordion">
                    Busca
                </div>
                <div id="accPalavraChave" class="itemAccordionConteudo" style="display: none;">
                    <div>
                        <div>
                            Palavra chave
                        </div>
                        <div>
                            <asp:TextBox ID="txtPalavraChaveBusca" CssClass="txt" Width="142px" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="btnBuscar" ValidationGroup="busca" ImageUrl="~/img/btn_buscar.png"
                                runat="server" />
                            <asp:CustomValidator ID="cvBusca" Display="None" ClientValidationFunction="validaBusca"
                                ValidationGroup="busca" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
                <div class="itemAccordion">
                    Ordenações
                </div>
                <div id="accOrdenacao" class="itemAccordionConteudo" style="display: none;">
                    <div>
                        Em breve<br />
                    </div>
                </div>
            </div>
            <iframe id="ifrDownLoad" src="" style="display: none;"></iframe>
        </div>
        <div class="divColuna2">
            <asp:Repeater ID="rptArquivos" runat="server">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnFilePath" runat="server" />
                    <asp:HiddenField ID="hdnFileName" runat="server" />
                    <div class="divFiles" style="border: 1px solid #ccc; float: left; width: 125px; height: 154px;
                        margin: 5px; padding: 4px;">
                        <asp:CheckBox ID="chkArquivo" runat="server" />
                        <div style="border: 1px solid #ccc; width: 122px; height: 90px; margin-top: 4px;">
                            <asp:Image ID="imgThumb" runat="server" Width="100%" Height="100%" />
                        </div>
                        <div style="font-size: 9px; margin-top: 4px;">
                            Data:
                            <asp:Literal ID="ltrData" runat="server"></asp:Literal>
                        </div>
                        <div style="font-size: 9px;">
                            Tamanho:
                            <asp:Literal ID="ltrTamanho" runat="server"></asp:Literal>
                        </div>
                        <div style="font-size: 9px;">
                            Dimensão:
                            <asp:Literal ID="ltrDimensao" runat="server"></asp:Literal>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="rptArquivosLista" runat="server">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnFilePath" runat="server" />
                    <asp:HiddenField ID="hdnFileName" runat="server" />
                    <div class="divFilesLista">
                        <div class="divChkList">
                            <asp:CheckBox ID="chkArquivo" runat="server" />
                        </div>
                        <div id="divIconFiles" runat="server">
                            <!-- -->
                        </div>
                        <div class="divNomeArquivo">
                            <asp:Literal ID="ltrNomeArquivo" runat="server"></asp:Literal>
                        </div>
                        <div class="divFileInfo" style="margin-right: 3px;">
                            <asp:Literal ID="ltrDataArquivo" runat="server"></asp:Literal>
                        </div>
                        <div class="divFileInfo">
                            <asp:Literal ID="ltrTamanho" runat="server"></asp:Literal>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="divUpload">
            <div>
                <div class="divDadosArquivo">
                    Dados sobre o arquivo a ser enviado.
                </div>
                <div style="padding-top: 8px;">
                    <div style="float: left;">
                        Título
                        <br />
                        <asp:TextBox ID="txtTitulo" CssClass="txt" Width="298px" runat="server"></asp:TextBox>
                    </div>
                    <div style="float: right;">
                        Palavras Chave
                        <br />
                        <asp:TextBox ID="txtPalavraChave" CssClass="txt" Width="290px" runat="server"></asp:TextBox>
                    </div>
                    <div style="clear: both;">
                        <!-- -->
                    </div>
                    <div style="float: left;">
                        Arquivo
                        <br />
                        <asp:FileUpload ID="FileUpload1" CssClass="txt" runat="server" />
                    </div>
                    <div style="float: right; margin-right: 70px;">
                        &nbsp;
                        <br />
                        <asp:ImageButton ID="btnUpload" ImageUrl="~/img/btn_EnviarArquivo.png" ValidationGroup="form"
                            runat="server" />
                    </div>
                    <div class="divFecharSalvar">
                        <asp:ImageButton ID="btnFecharSalvar" ImageUrl="~/img/btn_FecharSalvar.png" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <asp:CustomValidator ID="cvTitulo" Display="None" ClientValidationFunction="validaTitulo"
        ValidationGroup="form" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
    <asp:CustomValidator ID="cvPalevraChave" Display="None" ClientValidationFunction="validaPalavraChave"
        ValidationGroup="form" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
    <asp:CustomValidator ID="cvArquivo" Display="None" ClientValidationFunction="validaArquivo"
        ValidationGroup="form" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
    <ul id="myMenu" class="contextMenu">
        <li class="delete"><a href="#delete">Delete</a></li>
        <li class="edit"><a href="#renomear">Renomear</a></li>
        <li class="download"><a href="#download">Download</a></li>
    </ul>
    </form>
</body>
</html>
