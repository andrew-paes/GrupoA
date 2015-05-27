<%@ Control Language="C#" AutoEventWireup="true" CodeFile="comentarioEspecialista.ascx.cs"
    Inherits="content_module_comentarioEspecialista_comentarioEspecialista" %>
<script language="javascript">
    $(document).ready(function () {

        $('textarea[id$="txtResumoComentario"]').keyup(function () {
            var max = 352;
            if ($(this).val().length > max) {
                $(this).val($(this).val().substr(0, max));
            }
        });
    });
</script>
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Nome do título:"></asp:Label><br />
<asp:Label ID="ltrNomeTitulo" Width="350px" CssClass="frmborder" runat="server"></asp:Label>
<br />
<br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="ISBN13 do título:"></asp:Label><br />
<asp:Label ID="ltrISBN13" CssClass="frmborder" runat="server"></asp:Label>
<br />
<br />
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Nome do Especialista:"></asp:Label>
<asp:CustomValidator runat="server" ID="cvValidaNome" OnServerValidate="cvValidaNome_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNomeEspecialista"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox ID="txtNomeEspecialista" Width="180px" CssClass="frmborder" MaxLength="30"
    runat="server"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Especialidade:" />
<asp:CustomValidator runat="server" ID="cvValidaEspecialidade" OnServerValidate="cvValidaEspecialidade_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEspecialidade"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox ID="txtEspecialidade" Width="350px" CssClass="frmborder" MaxLength="63"
    runat="server"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label8" Font-Bold="true" Text="Resumo do Comentário:" />
<asp:CustomValidator runat="server" ID="cvValidaResumo" OnServerValidate="cvValidaResumo_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtResumoComentario"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox ID="txtResumoComentario" Width="500px" CssClass="frmborder" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Comentário do especialista:" />
<asp:CustomValidator runat="server" ID="cvValidaHTML" OnServerValidate="cvValidaHTML_ServerValidate"
    ValidationGroup="1"></asp:CustomValidator>
<ag2:HtmlTextBox runat="server" ID="HtmlComentarioEspecialista" />
<br />
<br />
<asp:Label runat="server" ID="Label10" Font-Bold="true" Text="Url do vídeo (youtube):"></asp:Label>
<br />
<asp:TextBox ID="txtUrlMidia" Width="200px" CssClass="frmborder" MaxLength="100"
    runat="server"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label7" Font-Bold="true" Text="Destaque:"></asp:Label>
<br />
<asp:CheckBox ID="chkDestaque" runat="server" />
<br />
<br />
<!-- Categorias -->
<asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:"></asp:Label>
<br />
<asp:CheckBoxList ID="cblCategorias" runat="server" />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<br />
<br />
<asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Imagem do especialista (Dimensões 215x160):" /><br />
<ag2:ListFiles ID="upArquivoImagem" runat="server" TargetFolder="comentarioEspecialista/"
    Editable="true" MaxFileLength="10000" TipoArquivo="IMAGE" />
<%--<br />
<asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Arquivo de áudio:" /><br />
<ag2:ListFiles ID="upArquivoAudio" runat="server" TargetFolder="arquivosAudio/" Editable="true"
    MaxFileLength="10000" TipoArquivo="AUDIO" />--%>
