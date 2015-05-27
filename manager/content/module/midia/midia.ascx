<%@ Control Language="C#" AutoEventWireup="true" CodeFile="midia.ascx.cs" Inherits="content_module_midia_midia" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("select[id$='ddlTipo']").change(function (e) {
            GrupoA.ControlaTela.init();
        });

        GrupoA.ControlaTela.init();
    });

    var GrupoA = {};

    GrupoA.ControlaTela = {
        init: function () {
            if ($("select[id$=ddlTipo]").attr("value") == '3') {
                $("div[id$='pnlUrl']").show();
            } else {
                $("div[id$='pnlUrl']").hide();
            }
        }
    }
</script>
<label>
    <b>Título:</b></label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo"
    Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="200px" CssClass="frmborder" MaxLength="100"></asp:TextBox>
<br />
<br />
<label>
    <b>Descrição:</b></label>
<br />
<ag2:HtmlTextBox runat="server" ID="txtDescricao" ToolBar="Basic" />
<br />
<br />
<label>
    <b>Autor:</b></label>
<br />
<asp:TextBox runat="server" ID="txtAutor" Width="300px" CssClass="frmborder" MaxLength="200"></asp:TextBox>
<br />
<br />
<label>
    <b>Tipo:</b></label>
<br />
<asp:DropDownList runat="server" ID="ddlTipo" CssClass="frmborder"></asp:DropDownList>
<br />
<br />
<asp:Panel runat="server" ID="pnlUrl">
    <label>
    <b>Url (youtube):</b></label>
    <br />
    <asp:TextBox runat="server" ID="txtUrl" Width="300px" CssClass="frmborder" MaxLength="512"></asp:TextBox>
    <br />
    <br />
</asp:Panel>
<asp:CheckBox runat="server" ID="cbAtivo" /> <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Ativo"></asp:Label>
<br />
<br />
<asp:Label runat="server" ID="lblCategorias" Font-Bold="true" Text="Categorias:"></asp:Label>
<br />
<asp:CheckBoxList ID="cblCategorias" runat="server" />
<br />
<br />
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Revistas:"></asp:Label>
<br />
<asp:CheckBoxList ID="cblRevistas" runat="server" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" />
</div>
<br />
<asp:Panel ID="pnlArquivo" runat="server" Visible="false">
	<asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Arquivo:" /><br />
	<ag2:ListFiles ID="upArquivo" runat="server" TargetFolder="midia/"
		Editable="true" MaxFileLength="40000" TipoArquivo="ALL" />
</asp:Panel>
<br />