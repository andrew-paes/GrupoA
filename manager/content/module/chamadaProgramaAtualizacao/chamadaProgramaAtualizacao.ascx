<%@ Control Language="C#" AutoEventWireup="true" CodeFile="chamadaProgramaAtualizacao.ascx.cs"
    Inherits="content_module_chamada_atualizacao" %>
<script language="javascript">
    $(document).ready(function () {

        $('textarea[id$="txtPrimeiraChamadaTexto"]').keyup(function () {
            var max = 500;
            if ($(this).val().length > max) {
                $(this).val($(this).val().substr(0, max));
            }
        });
    });
</script>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:HiddenField ID="hddProgramaAtualizacaoChamadaId" runat="server" Value="0" />
<!-- Primeira Chamada - Titulo -->
<asp:Label runat="server" ID="lblTitulo" Font-Bold="true" Text="Título da Primeira Chamada:"></asp:Label>
<asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtPrimeiraChamadaTitulo"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtPrimeiraChamadaTitulo" Width="350px" CssClass="frmborder"
    MaxLength="100"></asp:TextBox>
<br />
<br />
<!-- Primeira Chamada - Texto -->
<asp:Label runat="server" ID="lblPrimeiraChamadaTexto" Font-Bold="true" Text="Texto da Primeira Chamada:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrimeiraChamadaTexto"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtPrimeiraChamadaTexto" TextMode="MultiLine" Height="98px"
    Width="480px" CssClass="frmborder" MaxLength="500"></asp:TextBox>
<br />
<br />
<!-- Primeira Chamada - URL -->
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="URL da Primeira Chamada:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrimeiraChamadaURL"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtPrimeiraChamadaURL" Width="350px" CssClass="frmborder"
    MaxLength="500"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrimeiraChamadaURL"
    ErrorMessage="URL Inválida" ValidationGroup="1" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator>
<br />
<br />
<!-- Primeira Chamada - Nova Janela -->
<asp:Label runat="server" ID="lblPrimeiraChamadaNovaJanela" Font-Bold="true" Text="Nova Janela?" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1"
    ControlToValidate="ddlPrimeiraChamadaNovaJanela" InitialValue=":: Selecione ::"
    ErrorMessage="Campo Obrigatório" />
<br />
<asp:DropDownList ID="ddlPrimeiraChamadaNovaJanela" runat="server" Width="100px"
    CssClass="frmborder" />
<br />
<br />
<%-- Html comentado após o change do sistema onde o box que exibe este conteúdo no site não apresenta informações da segunda chamada.
<!-- Segunda Chamada - Titulo -->
<asp:Label runat="server" ID="lblSegundaChamadaTitulo" Font-Bold="true" Text="Título da Segunda Chamada:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtSegundaChamadaTitulo" Width="350px" CssClass="frmborder"
	MaxLength="100"></asp:TextBox>
<br />
<br />
<!-- Segunda Chamada - Texto -->
<asp:Label runat="server" ID="lblSegundaChamadaText" Font-Bold="true" Text="Texto da Segunda Chamada:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtSegundaChamadaTexto" Width="350px" CssClass="frmborder"
	MaxLength="200"></asp:TextBox>
<br />
<br />
<!-- Segunda Chamada - URL -->
<asp:Label runat="server" ID="lblSegundaChamadaURL" Font-Bold="true" Text="URL da Segunda Chamada:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtSegundaChamadaURL" Width="350px" CssClass="frmborder"
	MaxLength="200"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSegundaChamadaURL"
	ErrorMessage="URL Inválida" ValidationGroup="1" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator><br />
<br />
<!-- Segunda Chamada - Nova Janela -->
<asp:Label runat="server" ID="lblSegundaChamadaNovaJanela" Font-Bold="true" Text="Nova Janela?" />
<br />
<asp:DropDownList ID="ddlSegundaChamadaNovaJanela" runat="server" Width="100px" CssClass="frmborder" />
<br />--%>
<br />
<!-- Página -->
<asp:Label runat="server" ID="lblPagina" Font-Bold="true" Text="Páginas:"></asp:Label><br />
<asp:CheckBoxList ID="cblPaginas" runat="server" />
<asp:CustomValidator runat="server" ID="cvPaginas" OnServerValidate="cvValidarAtivacao_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<br />
<br />
<!-- Ativa  -->
<table width="300px">
    <tr>
        <td>
            <asp:Label runat="server" ID="lblAtiva" Font-Bold="true" Text="Ativa:" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkAtivo" runat="server" />
            <asp:CustomValidator runat="server" ID="cvValidarAtivacao" OnServerValidate="cvValidarAtivacao_ServerValidate"
                ValidationGroup="1"></asp:CustomValidator>
        </td>
    </tr>
</table>
<br />
<br />
<!-- Controles -->
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<asp:Panel ID="pnlArquivo" runat="server" Visible="false">
    <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Imagem:" /><br />
    <ag2:ListFiles ID="upArquivoImagem" runat="server" TargetFolder="chamadas/" Editable="true"
        MaxFileLength="10000" TipoArquivo="IMAGE" />
</asp:Panel>
<br />
