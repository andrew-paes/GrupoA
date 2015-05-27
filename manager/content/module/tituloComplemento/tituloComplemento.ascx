<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloComplemento.ascx.cs"
    Inherits="content_module_tituloComplemento_tituloComplemento" %>
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Nome do título:"></asp:Label><br />
<asp:Label ID="ltrNomeTitulo" Width="350px" CssClass="frmborder" runat="server"></asp:Label>
<br />
<br />
<span><b>ISBN13 do título:</b></span>
<br />
<asp:Label ID="ltrISBN13" CssClass="frmborder" runat="server"></asp:Label>
<br />
<br />
<span><b>Texto sobre os autores:</b></span>
<br />
<ag2:HtmlTextBox runat="server" ID="htmlTextoAutor" />
<br />
<br />
<span><b>Resumo do título:</b></span>
<br />
<ag2:HtmlTextBox runat="server" ID="htmlTextoResumo" />
<br />
<br />
<span><b>Texto sobre sumário:</b></span>
<br />
<ag2:HtmlTextBox runat="server" ID="HtmlTextoSumario" />
<br />
<br />
<span><b>Equipe Revisora:</b></span>
<br />
<ag2:HtmlTextBox runat="server" ID="htmlTextoMaterial" />
<br />
<br />
<asp:CheckBox ID="chkExibirSite" runat="server" Text="Exibir no Site" Enabled="false" />
<br />
<br />
<asp:CheckBox ID="chkDisponivel" runat="server" Text="Disponível para Venda" Enabled="false" />
<br />
<br />
<asp:CheckBox ID="chkHomologado" runat="server" Text="Homologado para o site" Font-Bold="true" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<br />
<span><b>Imagem do Autor:</b></span>
<br />
<ag2:ListFiles ID="upArquivoAudioAutor" runat="server" TargetFolder="imagensAutor/"
    Editable="true" MaxFileLength="10000" TipoArquivo="IMAGE" />
<br />
<%--<span><b>Arquivo do Sumário:</b></span>
<br />
<ag2:ListFiles ID="upArquivoSumario" runat="server" TargetFolder="imagensTitulo/"
    Editable="true" MaxFileLength="10000" TipoArquivo="PDF" />
<br />--%>
<span><b>Arquivo de Tags:</b></span>
<br />
<ag2:ListFiles ID="upArquivoTag" runat="server" TargetFolder="imagensTitulo/" Editable="true"
    MaxFileLength="10000" TipoArquivo="IMAGE" />
