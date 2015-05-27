<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2ListFiles.ascx.cs"
    Inherits="ctl_ag2ListFiles" %>
<div class="divListFiles">
    <asp:HiddenField ID="hdnScriptModal" runat="server" />
    <asp:HiddenField ID="hdnModuleName" runat="server" />
    <asp:HiddenField ID="hdnMaxFileLength" runat="server" />
    <asp:HiddenField ID="hdnTargetFolder" runat="server" />
    <asp:HiddenField ID="hdnRegistroId" runat="server" />
    <asp:HiddenField ID="hdnTipoArquivo" runat="server" />
    <asp:HyperLink ID="lnkInserirArquivo" CssClass="lnkInserirArquivo" NavigateUrl="javascript:;"
        runat="server"><img src="../img/btn_EnviarArquivo.png" alt="Enviar Arquivo" /></asp:HyperLink>
    <asp:Button ID="lnkhidden" Text="lnkhidden" CssClass="lnkInserirArquivoHidden" runat="server" />
    <asp:HiddenField ID="hdnArquivoId" runat="server" />
    <asp:Panel ID="pnlConteudoLista" runat="server">
        <asp:Repeater ID="rptListagem" runat="server">
            <ItemTemplate>
                <div class="divItemListaArquivo">
                    <div runat="server" id="divThumb"><asp:Image ID="imgThumb" Width="122px" Height="90px" Visible="false" runat="server" /></div>
                    <div class="divDeleteFile">
                        <div>
                            <asp:ImageButton ID="imgDelete" ImageUrl="~/img/excluir.jpg" OnClick="imgDelete_Click" AlternateText="Apagar Arquivo" ToolTip="Apagar Arquivo" runat="server" />
                        </div>
                        <div>
                            <asp:ImageButton ID="imgDownload" ImageUrl="~/img/icoDownload.png" OnClick="imgDownload_Click" AlternateText="Download" ToolTip="Download" runat="server" />
                        </div>
                        <asp:PlaceHolder ID="phEdit" runat="server">
                            <div class="divEditFile">
                                <asp:ImageButton ID="imgEdit" ImageUrl="~/img/editar.jpg" AlternateText="Editar Registro" ToolTip="Editar Registro" runat="server" />
                            </div>
                        </asp:PlaceHolder>
                    </div>
                    <div class="divInfoArquivo">
                        <div><strong style="font-size: 14px">Nome do arquivo:</strong> <span><asp:Literal ID="ltrArquivoNome" runat="server"></asp:Literal></span></div>
                        <div><strong style="font-size: 14px">Tamanho:</strong> <span><asp:Literal ID="ltrTamanhoArquivo" runat="server"></asp:Literal></span></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</div>
