<%@ Control Language="C#" AutoEventWireup="true" CodeFile="logPagamento.ascx.cs"
    Inherits="content_module_logPagamento_logPagamento" %>
<label>
    <b>Código Pedido:</b></label>
<br />
<asp:TextBox runat="server" ID="txtCodigoPedido" Width="600px" CssClass="frmborder" MaxLength="128"></asp:TextBox>
<br />
<label>
    <b>Conteúdo Parametros:</b></label>
<br />
<asp:TextBox TextMode="MultiLine" runat="server" ID="txtConteudoParametros" Rows="20" Width="600" />
<br />
<label>
    <b>Data Hora:</b></label>
<br />
<asp:TextBox runat="server" ID="txtDataHora" Width="200px" CssClass="frmborder" MaxLength="128"></asp:TextBox>
<br />
<label>
    <b>Conteúdo XML:</b></label>
<br />
<asp:TextBox TextMode="MultiLine" runat="server" ID="txtConteudoXML" Rows="20" Width="600" />
