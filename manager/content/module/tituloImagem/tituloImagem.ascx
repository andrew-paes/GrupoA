<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloImagem.ascx.cs" Inherits="content_module_tituloImagem_tituloImagem" %>
<label><b>Título:</b></label><br />
<asp:Label runat="server" ID="lblTitulo" CssClass="frmborder" Width="350px" />
<br />
<br />
<label><b>Arquivo Tamanho Grande:</b></label><br />
<ag2:ListFiles ID="ListFiles1" runat="server" TargetFolder="imagensTitulo/" Editable="true"
    MaxFileLength="5000" MultiFile="true" TipoArquivo="ALL" />
