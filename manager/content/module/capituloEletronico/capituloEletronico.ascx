<%@ Control Language="C#" AutoEventWireup="true" CodeFile="capituloEletronico.ascx.cs" Inherits="content_module_capituloEletronico_capituloEletronico" %>
<%@ Register src="../../../ctl/ag2RelacionarList.ascx" tagname="ag2RelacionarList" tagprefix="uc1" %>

<h3>Capítulo Eletrônico</h3><br />
<div style="padding-left:15">
<strong>Nome do Capítulo:</strong>
<asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNomeCapitulo"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox ID="txtNomeCapitulo" Width="350px" CssClass="frmborder" runat="server" MaxLength="200" /><br /><br />

<strong>Exibir no Site?</strong><br />
<asp:CheckBox ID="chkExibirSite" runat="server" /><br /><br />

<strong>Homologado?</strong><br />
<asp:CheckBox ID="chkHomologado" runat="server" /><br /><br />

<strong>Disponível?</strong><br />
<asp:TextBox ID="txtDisponivel" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />


<strong>Quantidade de Páginas</strong><br />
<asp:TextBox ID="txtQtdPaginas" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>Resumo do Capítulo</strong><br />
<asp:TextBox ID="txtResumoCapitulo"  TextMode="MultiLine" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>Valor Unitário</strong><br />
<asp:TextBox ID="txtValorUnitario" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>Valor Oferta</strong><br />
<asp:TextBox ID="txtValorOferta" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br />
</div>
<br />
<h3>Título Eletrônico</h3>
<br />
<div style="padding-left:15">
<strong>Nome do Título:</strong><br />
<asp:TextBox ID="txtTilulo" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>Subtítulo:</strong><br />
<asp:TextBox ID="txtSubtitulo" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>Edição:</strong><br />
<asp:TextBox ID="txtEdicao" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>ISBN 13:</strong><br />
<asp:TextBox ID="txtIsbn13" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />

<strong>Data Publicação:</strong><br />
<asp:TextBox ID="txtDataPublicacao" Width="350px" CssClass="frmborder" runat="server" ReadOnly="true" /><br /><br />
</div>
<br />
<h3>Autores</h3>
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Filtros da Listagem:" />

<table width="420px" border="0" cellpadding="10" cellspacing="10">
    <tr>
        <td>
            <asp:TextBox runat="server" ID="txtTitulo" Width="200px" CssClass="frmborder" MaxLength="128"></asp:TextBox>
        </td>
        <td>
            &nbsp;<asp:Button runat="server" ID="btnPesquisar" Text="Pesquisar" Width="90px"
                OnClick="btnPesquisar_Click" />
        </td>
    </tr>
</table>
<br />
<uc1:ag2RelacionarList ID="listRel" runat="server" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" ValidationGroup="1" OnClick="btnExecute_Click" />
</div>