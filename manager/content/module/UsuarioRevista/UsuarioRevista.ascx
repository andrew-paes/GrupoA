<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsuarioRevista.ascx.cs" Inherits="content_module_UsuarioRevista_UsuarioRevista" %>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:HiddenField ID="hddUsuarioRevistaId" runat="server" Value="0" />

<asp:Panel runat="server" ID="pnlInclusao">
    <fieldset style="width: 600px;">
        <legend><span id="Span2">Usuário</span></legend>
        <table id="table2" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="CPF/CNPJ:"></asp:Label><br />
                    <asp:TextBox runat="server" ID="txtCadastroPessoa" Width="150px" CssClass="frmborder" MaxLength="14"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="label" Font-Bold="true" Text="Nome/Razão Social:"></asp:Label><br />
                    <asp:TextBox runat="server" ID="txtNome" Width="300px" CssClass="frmborder" MaxLength="200"></asp:TextBox>
                    <br />
                    <br />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/btn_filtrar.gif"
		                Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		                CausesValidation="true" OnClick="btnFiltrar_Click" CssClass="btnSubmitForm" />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Usuários:"></asp:Label>
                    <asp:CustomValidator runat="server" ID="cvUsuarios" OnServerValidate="cvUsuarios_ServerValidate"
	                     ValidationGroup="1"></asp:CustomValidator><br />
                    <asp:Panel runat="server" ID="pnlUsuarios" height="150px" Width="590px" style="overflow: auto;" Visible="false">    
                        <asp:RadioButtonList runat="server" ID="rblUsuario"></asp:RadioButtonList>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Panel>

<asp:Panel runat="server" ID="pnlEdicao">
    <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="CPF/CNPJ:"></asp:Label><br />
    <asp:Label runat="server" ID="lblCadastroPessoa" Font-Bold="true" CssClass="frmborder" Text=""></asp:Label>
    <br />
    <br />
    <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Nome/Razão Social:"></asp:Label><br />
    <asp:Label runat="server" ID="lblNome" Font-Bold="true" CssClass="frmborder" Text=""></asp:Label>
    <br />
</asp:Panel>

<br />

<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Revista:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRevista"
	ErrorMessage="Campo Obrigatório" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator><br />
<asp:DropDownList ID="ddlRevista" runat="server" Width="200px"></asp:DropDownList>
<br />
<br />
<asp:Label runat="server" ID="Label7" Font-Bold="true" Text="Data de Início da Assinatura:" />
<asp:CustomValidator runat="server" ID="cvValidarDataInicio" OnServerValidate="cvValidarDataInicio_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator><br />
<ag2:DateField runat="server" ID="txtDataInicio" CssClass="frmborder" />
<br />
<br />
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Data de Fim da Assinatura:" />
<asp:CustomValidator runat="server" ID="cvValidarDataFim" OnServerValidate="cvValidarDataFim_ServerValidate"
	ValidationGroup="1"></asp:CustomValidator><br />
<ag2:DateField runat="server" ID="txtDataFim" CssClass="frmborder" />
<br />
<br />
<!-- Controles -->
<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
