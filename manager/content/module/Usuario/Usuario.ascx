<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Usuario.ascx.cs" Inherits="content_module_Usuario_Usuario" %>
<div>
    Usuário
    <asp:RequiredFieldValidator ControlToValidate="txtUsuario" ValidationGroup="1" ID="RequiredFieldValidator1"
        runat="server" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox ID="txtUsuario" CssClass="frmborder" MaxLength="100" runat="server"
        Width="400px"></asp:TextBox>
    <br />
    <br />
    Login
    <asp:RequiredFieldValidator ControlToValidate="txtLogin" ValidationGroup="1" ID="RequiredFieldValidator6"
        runat="server" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox ID="txtLogin" CssClass="frmborder" MaxLength="100" runat="server" Width="400px"></asp:TextBox>
    <br />
    <br />
    E-mail
    <asp:RequiredFieldValidator ControlToValidate="txtEmail" ValidationGroup="1" ID="RequiredFieldValidator2"
        runat="server" ErrorMessage="Campo Obrigatório" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regEmail" Display="Dynamic" ControlToValidate="txtEmail"
        runat="server" ErrorMessage="E-mail incorreto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        ValidationGroup="1"></asp:RegularExpressionValidator>
    <br />
    <asp:TextBox ID="txtEmail" CssClass="frmborder" MaxLength="150" runat="server" Width="400px"></asp:TextBox>
    <br />
    <br />
    <asp:PlaceHolder ID="phSenha" runat="server">Senha
        <asp:RequiredFieldValidator ControlToValidate="txtSenha" Display="Dynamic" ValidationGroup="1"
            ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtSenha" ControlToCompare="txtSenhaConfirmacao"
            runat="server" ErrorMessage="Senha não confirmada" Operator="Equal" ValidationGroup="1"></asp:CompareValidator>
        <br />
        <asp:TextBox ID="txtSenha" CssClass="frmborder" TextMode="Password" MaxLength="150"
            runat="server" Width="400px"></asp:TextBox>
        <br />
        <br />
        Confirmação de Senha
        <asp:RequiredFieldValidator ControlToValidate="txtSenhaConfirmacao" ValidationGroup="1"
            ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtSenhaConfirmacao" TextMode="Password" CssClass="frmborder" MaxLength="150"
            runat="server" Width="400px"></asp:TextBox>
        <br />
        <br />
    </asp:PlaceHolder>
    <div style="margin-top: 10px;">
        <div>
            Perfis
        </div>
        <div>
            <asp:CheckBoxList ID="chkListPerfis" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px"
                Font-Bold="true" BackColor="#FFF7F2" CellPadding="0" CellSpacing="5" RepeatColumns="4"
                RepeatDirection="Horizontal" RepeatLayout="Table" runat="server">
            </asp:CheckBoxList>
        </div>
    </div>
    <br />
    <asp:CheckBox ID="chkBloqueado" Text="Usuário Bloqueado" runat="server" />
    <asp:PlaceHolder ID="phResetSenha" runat="server" Visible="false">
        <div style="border: 1px solid #ccc; background-color: #f4f4f4; padding: 3px; width: 400px;
            margin-top: 10px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lnkResetSenha" ValidationGroup="1" runat="server">Resetar a senha deste usuário</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblSenhaEnviada" Visible="false" ForeColor="Red" Font-Bold="true"
                        runat="server" Text="Senha enviada para o e-mail do usuário."></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:PlaceHolder>
</div>
<div class="boxesc" style="vertical-align: bottom; margin-top: 10px;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
