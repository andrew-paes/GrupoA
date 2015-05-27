<%@ Control Language="C#" AutoEventWireup="true" CodeFile="logCliente.ascx.cs" Inherits="content_module_logCliente_logCliente" %>
<label>
    <b>Categoria:</b></label>
<br />
<asp:TextBox runat="server" ID="txtCategoria" Width="600px" CssClass="frmborder"
    MaxLength="300" />
<br />
<label>
    <b>Evento:</b></label>
<br />
<asp:TextBox runat="server" ID="txtEvento" Width="600px" CssClass="frmborder" MaxLength="300" />
<br />
<label>
    <b>Tipo Cliente:</b></label>
<br />
<asp:TextBox runat="server" ID="txtTipoCliente" Width="600px" CssClass="frmborder" MaxLength="300" />
<br />
<label>
    <b>Data Hora:</b></label>
<br />
<asp:TextBox runat="server" ID="txtDataHora" Width="200px" CssClass="frmborder" MaxLength="128" />
<br />
<label>
    <b>Conteúdo XML:</b></label>
<br />
<asp:TextBox runat="server" ID="txtConteudoXML" TextMode="MultiLine" Rows="25" Width="600" />
