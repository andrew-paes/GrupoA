<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tituloConteudoExtra.ascx.cs"
    Inherits="content_module_tituloConteudoExtra_tituloConteudoExtra" %>
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Título:"></asp:Label>
<br />
<asp:Label ID="txtTituloLivro" Width="350px" CssClass="frmborder" MaxLength="256"
    runat="server">Título</asp:Label>
<br />
<br />
<asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Data Lançamento:"></asp:Label>
<br />
<asp:Label ID="lblDtLancamento" runat="server" CssClass="frmborder" Text="" Width="100px"></asp:Label>
<br />
<br />
<asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Subtítulo:"></asp:Label>
<br />
<asp:Label ID="lblSubtitulo" runat="server" CssClass="frmborder" Text="" Width="350px"></asp:Label>
<br />
<br />
<asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Informações de Mídia: "></asp:Label>
<br />
<ag2:HtmlTextBox runat="server" ID="htmlTextoMidia" />
<br />
<asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Hotsite:"></asp:Label>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUrlConteudoExtra"
    ErrorMessage="URL Inválida" ValidationGroup="1" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?">
</asp:RegularExpressionValidator>
<br />
<asp:TextBox ID="txtUrlConteudoExtra" Width="200px" CssClass="frmborder" MaxLength="256"
    runat="server"></asp:TextBox>
<br />
<br />
<fieldset style="padding: 10px">
    <legend>Arquivos</legend>
    <asp:HiddenField runat="server" ID="hddTituloConteudoExtraArquivoId" Value="0" />
    <asp:HiddenField runat="server" ID="hddTituloId" Value="0" />
    <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Título do Arquivo (Ex.: Exercícios Complementares):"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeArquivo"
        Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox ID="txtNomeArquivo" Width="350px" CssClass="frmborder" MaxLength="300"
        runat="server"></asp:TextBox>
    <br />
    <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Caminho e Nome do Arquivo (Ex.: pasta/imagem.jpg):"></asp:Label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtArquivo"
        Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox ID="txtArquivo" Width="350px" CssClass="frmborder" MaxLength="300" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:CheckBox runat="server" ID="cbSomenteLogado" />
    <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="Somente Logado"></asp:Label>
    <br />
    <asp:CheckBox runat="server" ID="cbRestritoProfessor" />
    <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Restrito Professor"></asp:Label>
    <br />
    <asp:CheckBox runat="server" ID="cbAtivo" />
    <asp:Label runat="server" ID="Label10" Font-Bold="true" Text="Ativo"></asp:Label>
    <br />
    <br />
    <asp:ImageButton ID="btnSalvar" runat="server" ImageUrl="~/img/btn_Adicionar.png"
        Width="73" Height="20" BorderWidth="0" AlternateText="Salvar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnSalvar_Click" ValidationGroup="2" CssClass="btnSubmitForm" />
    <br />
    <br />
    <asp:Repeater runat="server" ID="rptArquivos" OnItemDataBound="rptArquivos_ItemDataBound">
        <HeaderTemplate>
            <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Arquivos Adicionados:"></asp:Label>
            <br />
            <table cellspacing="15px">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Nome"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Somente Logado"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label14" Font-Bold="true" Text="Restrito Professor"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label15" Font-Bold="true" Text="Ativo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label21" Font-Bold="true" Text="Data de Cadastro"></asp:Label>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Literal runat="server" ID="lNome" Text="teste.jpg"></asp:Literal>
                </td>
                <td align="center">
                    <asp:Literal runat="server" ID="lSomenteLogado" Text="Sim"></asp:Literal>
                </td>
                <td align="center">
                    <asp:Literal runat="server" ID="lRestritoProfessor" Text="Não"></asp:Literal>
                </td>
                <td align="center">
                    <asp:Literal runat="server" ID="lAtivo" Text="Sim"></asp:Literal>
                </td>
                <td>
                    <asp:Literal runat="server" ID="lDataHoraCadastro" Text="15/04/2011 16:52"></asp:Literal>
                </td>
                <td>
                    <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/img/editar.jpg" OnClick="btnEditar_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnExcluirArquivo" runat="server" ImageUrl="~/img/excluir.jpg"
                        OnClick="btnExcluirArquivo_Click" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
</fieldset>
<br />
<!-- Controles -->
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
