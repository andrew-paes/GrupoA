<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revistaEdicao.ascx.cs" Inherits="content_module_revistaEdicao" %>

<asp:HiddenField ID="hdnRevistaEdicaoId" runat="server" Value="0" />

<fieldset style="width: 600px;">
    <legend><span id="Span2">Revista</span></legend>
    <table id="table2" cellpadding="5" cellspacing="5">
        <tr>
            <td>
                <strong>Nome da Revista:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtNomeRevista" Width="500px" CssClass="frmborder" MaxLength="100" enabled="false"/>
                <br />
                <br />

                <strong>ISSN da Revista:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtISSN" Width="150px" CssClass="frmborder" MaxLength="100" enabled="false"/>
            </td>
        </tr>
    </table>
</fieldset>

<br />
<br />

<fieldset style="width: 600px;">
    <legend><span id="Span1">Produto</span></legend>
    <table id="table1" cellpadding="5" cellspacing="5">
        <tr>
            <td>
                <strong>Código do Produto:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtCodigoProduto" Width="500px" CssClass="frmborder" MaxLength="100" ReadOnly="true"/>
                <br />
                <br />

                <strong>Nome do Produto:</strong>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
	                ControlToValidate="txtNomeProduto" ErrorMessage="Campo Obrigatório" />
                <br />
                <asp:TextBox runat="server" ID="txtNomeProduto" Width="500px" CssClass="frmborder" MaxLength="100"/>
                <br />
                <br />

                <strong>Valor Unitário:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtValorUnitario" Width="80px" CssClass="frmborder" MaxLength="100" enabled="false"/>
                <br />
                <br />

                <strong>Valor Oferta:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtValorOferta" Width="80px" CssClass="frmborder" MaxLength="100" enabled="false"/>
                <br />
                <br />

                <strong>Peso:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtPeso" Width="50px" CssClass="frmborder" MaxLength="100" enabled="false"/>
                <br />
                <br />

                <asp:CheckBox ID="chkHomologado" runat="server" Text="Homologado"/>
                <br />
                <br />

                <asp:CheckBox ID="chkFreteGratis" runat="server" Text="Frete Grátis" enabled="false"/>
                <br />
                <br />

                <asp:CheckBox ID="chkDisponivel" runat="server" Text="Disponível" enabled="false"/>
                <br />
                <br />

                <asp:CheckBox ID="chkExibirSite" runat="server" Text="Exibir no Site" enabled="false"/>
            </td>
        </tr>
    </table>
</fieldset>

<br />
<br />

<fieldset style="width: 600px;">
    <legend><span id="fieldTitulo">Edição da Revista</span></legend>
    <table id="tableTitulo" cellpadding="5" cellspacing="5">
        <tr>
            <td>
                <asp:CheckBox ID="chkAtivo" runat="server" Text=" Ativo" Font-Bold="true" /> (Esta Edição será disponibilizada no site da Revista.)
                <br />
                <br />

                <strong>Número da Edição:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtNumeroEdicao" Width="50px" CssClass="numero frmborder" MaxLength="7" enabled="false" />
                <br />
                <br />
              
                <strong>Ano de Publicação:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtAnoPublicacao" Width="50px" CssClass="frmborder" enabled="false" />
                <br />
                <br />

                <strong>Mês de Publicação:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtMesPublicacao" Width="50px" CssClass="frmborder" enabled="false" />
                <br />
                <br />

                <strong>Período de Publicação:</strong>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
	                ControlToValidate="txtPeriodoPublicacao" ErrorMessage="Campo Obrigatório" />
                <br />
                <asp:TextBox runat="server" ID="txtPeriodoPublicacao" Width="130px" CssClass="frmborder" enabled="true" />
                <br />
                <br />

                <strong>Ano da Edição:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtAnoEdicao" Width="130px" CssClass="frmborder" MaxLength="20" enabled="true"/>
                <br />
                <br />

                <strong>Número de Páginas:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtNumPaginas" Width="50px" CssClass="frmborder" enabled="false"/>
                <br />
                <br />

                <strong>Título da Edição:</strong>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1"
	                ControlToValidate="txtTitulo" ErrorMessage="Campo Obrigatório" />
                <br />
                <asp:TextBox runat="server" ID="txtTitulo" Width="500px" MaxLength="200" CssClass="frmborder" />
                <br />
                <br />

                <strong>Descrição da Edição:</strong>
                <br />
                <ag2:HtmlTextBox runat="server" ID="txtDescricao" />
            </td>
        </tr>
    </table>
</fieldset>

<br />
<br />

<ag2:ListFiles ID="ListFilesRevista" runat="server" TargetFolder="imagensRevista/"
    Editable="true" MaxFileLength="5000" ScriptModal="RevistaEdicaoImagem.aspx" MultiFile="true"
    TipoArquivo="ALL" />

<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>

