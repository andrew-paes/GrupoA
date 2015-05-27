<%@ Control Language="C#" AutoEventWireup="true" CodeFile="assinaturaRevista.ascx.cs" Inherits="content_module_assinaturaRevista" %>

<asp:HiddenField ID="hdnAssinaturaRevistaId" runat="server" Value="0" />
<asp:HiddenField ID="hdnRevistaId" runat="server" Value="0" />

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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
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
    <legend><span id="Span3">Assinatura</span></legend>
    <table id="table3" cellpadding="5" cellspacing="5">
        <tr>
            <td>
                <strong>Número de Exemplares:</strong>
                <br />
                <asp:TextBox runat="server" ID="txtNumeroExemplares" Width="50px" CssClass="frmborder" MaxLength="7" Enabled="false" />
                <br />
                <br />

                <strong>Descrição da Assinatura:</strong>
                <br />
                <ag2:HtmlTextBox runat="server" ID="txtDescricao" />
            </td>
        </tr>
    </table>
</fieldset>

<br />
<br />

<ag2:ListFiles ID="ListFilesRevista" runat="server" TargetFolder="imagensRevista/" Editable="true"
    MaxFileLength="5000" ScriptModal="assinaturaRevistaImagem.aspx" TipoArquivo="IMAGE"/>


<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>

