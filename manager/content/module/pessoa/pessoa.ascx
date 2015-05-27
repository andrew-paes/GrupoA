<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pessoa.ascx.cs" Inherits="content_module_pessoa_pessoa" %>
<asp:HiddenField ID="hddUsuarioId" runat="server" Value="0" />
<style>
    div.line
    {
        display: block;
        margin-bottom: 5px;
    }
    div.control
    {
        width: 250px;
        padding: 10px 10px 10px 10px;
        display: inline-block;
    }
    div.control input
    {
        width: 95%;
    }
    div.control span
    {
        display: block;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtDtNascimentoD']").setMask({ mask: '99', type: 'reverse', defaultValue: '' });
        $("input[id$='txtDtNascimentoM']").setMask({ mask: '99', type: 'reverse', defaultValue: '' });
        $("input[id$='txtDtNascimentoA']").setMask({ mask: '9999', type: 'reverse', defaultValue: '' });
        $("input[id$='txtNumeroEdicao']").setMask({ mask: '9999999999', type: 'reverse', defaultValue: '' });
        $("input[id$='txtCEPEdicao']").setMask({ mask: '99999-999', type: '', defaultValue: '' });
        $("input[id$='txtTelefoneDDDEdicao']").setMask({ mask: '99999', type: '', defaultValue: '' });
        $("input[id$='txtTelefoneEdicao']").setMask({ mask: '99999999', type: '', defaultValue: '' });
        $("input[id$='txtRamalEdicao']").setMask({ mask: '9999999999', type: '', defaultValue: '' });
    });
</script>
<!-- Nome/E-mail -->
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
    Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtNome" MaxLength="200" Width="550px" CssClass="frmborder" />
<br />
<br />
<asp:Label runat="server" ID="lblEmail" Font-Bold="true" Text="E-mail:" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
    Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator>
<br />
<asp:TextBox runat="server" ID="txtEmail" MaxLength="200" Width="550px" CssClass="frmborder" />
<br />
<br />
<!-- Senha -->
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Senha:" />
<br />
<asp:TextBox runat="server" ID="txtSenha" TextMode="Password" MaxLength="20" Width="200px"
    CssClass="frmborder" />
<span>No mínimo 6 (seis) caracteres</span>
<br />
<br />
<asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Confirmação de Senha:" />
<br />
<asp:TextBox runat="server" ID="txtConfirmacaoSenha" TextMode="Password" MaxLength="20"
    Width="200px" CssClass="frmborder" />
<br />
<br />
<!-- Tipo Pessoa / CPF-CNPJ -->
<asp:Label runat="server" ID="lblTipoPessoa" Font-Bold="true" Text="Tipo Pessoa:" />
<br />
<asp:TextBox runat="server" ID="txtTipoPessoa" CssClass="frmborder" Width="25px"
    Enabled="false" />
<br />
<br />
<asp:Label runat="server" ID="lblCPF" Font-Bold="true" Text="CPF:" />
<br />
<asp:TextBox runat="server" ID="txtCPF" CssClass="frmborder" Enabled="false" />
<br />
<br />
<%--<!-- Login/Senha -->
<asp:Label runat="server" ID="lblLogin" Font-Bold="true" Text="Login:" />
<br />
<asp:TextBox runat="server" ID="txtLogin" MaxLength="50" Width="300px" CssClass="frmborder" />
<br />
<br />--%>
<asp:Panel runat="server" ID="pnlSexo">
    <asp:Label runat="server" ID="lblSexo" Font-Bold="true" Text="Sexo:" />
    <br />
    <asp:RadioButton runat="server" ID="rdMasculino" Text="Masculino" GroupName="rdSexo" />
    <br />
    <asp:RadioButton runat="server" ID="rdFeminino" Text="Feminino" GroupName="rdSexo" />
    <br />
    <br />
</asp:Panel>
<!-- Dt. Nasc./Dt Cadastro -->
<asp:Label runat="server" ID="lblDtNascimento" Font-Bold="true" Text="Data Nascimento:" />
<br />
<asp:TextBox runat="server" ID="txtDtNascimentoD" MaxLength="2" Width="15px" CssClass="frmborder" />
<asp:TextBox runat="server" ID="txtDtNascimentoM" MaxLength="2" Width="15px" CssClass="frmborder" />
<asp:TextBox runat="server" ID="txtDtNascimentoA" MaxLength="4" Width="30px" CssClass="frmborder" />
<br />
<br />
<asp:Label runat="server" ID="lblDtCadastro" Font-Bold="true" Text="Data Cadastro:" />
<br />
<asp:TextBox runat="server" ID="txtDtCadastro" CssClass="frmborder" Enabled="false" />
<br />
<br />
<!-- Ocupacao -->
<asp:Panel runat="server" ID="pnlOcupacao">
    <asp:Label runat="server" ID="lblOcupacao" Font-Bold="true" Text="Ocupação:" />
    <br />
    <asp:DropDownList runat="server" ID="ddlOcupacao" CssClass="frmborder" />
    <br />
    <br />
</asp:Panel>
<!-- News / SMS -->
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="SMS:" />
<br />
<asp:CheckBox ID="chkSMS" runat="server" />
<br />
<br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="News:" />
<br />
<asp:CheckBox ID="chkNews" runat="server" />
<br />
<br />
<asp:Label runat="server" ID="lblAtiva" Font-Bold="true" Text="Ativa:" />
<br />
<asp:CheckBox ID="chkAtivo" runat="server" />
<br />
<br />
<%--<asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Professor:" />
<br />
<asp:CheckBox ID="chkProfessor" runat="server" />
<br />
<br />--%>
<div id="divEnderecoLista" runat="server">
    <fieldset style="padding: 10px; width: 500px;">
        <legend>Endereços</legend>
        <asp:Repeater ID="rptEndereco" runat="server" OnItemDataBound="rptEndereco_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div id="divEndereco" runat="server" style="background-color: #F0F0F0;">
                    <asp:Label runat="server" ID="lblTipoEndereco" Font-Bold="true" />
                    <asp:Label runat="server" ID="lblPreferencialEntrega" Visible="false" Font-Bold="true"
                        Text="( Preferencial Para Entrega )" />
                    <br />
                    <div class="line">
                        <div class="control" style="width: 475px;">
                            <asp:Label runat="server" ID="lblNomeEndereco" Font-Bold="true" Text="Nome para Entrega:" />
                            <asp:TextBox runat="server" ID="txtNomeEntrega" CssClass="frmborder" Width="475"
                                Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 300px;">
                            <asp:Label runat="server" ID="lblLogradouro" Font-Bold="true" Text="Logradouro:" />
                            <asp:TextBox runat="server" ID="txtLogradouro" CssClass="frmborder" Width="300" Enabled="false" />
                        </div>
                        <div class="control" style="width: 50px;">
                            <asp:Label runat="server" ID="lblNumero" Font-Bold="true" Text="N°:" />
                            <asp:TextBox runat="server" ID="txtNumero" CssClass="frmborder" Width="50" Enabled="false" />
                        </div>
                        <div class="control" style="width: 80px;">
                            <asp:Label runat="server" ID="lblComplemento" Font-Bold="true" Text="Complemento:" />
                            <asp:TextBox runat="server" ID="txtComplemento" CssClass="frmborder" Width="80" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 373px;">
                            <asp:Label runat="server" ID="lblBairro" Font-Bold="true" Text="Bairro:" />
                            <asp:TextBox runat="server" ID="txtBairro" CssClass="frmborder" Width="373" Enabled="false" />
                        </div>
                        <div class="control" style="width: 80px;">
                            <asp:Label runat="server" ID="lblCEP" Font-Bold="true" Text="CEP:" />
                            <asp:TextBox runat="server" ID="txtCEP" CssClass="frmborder" Width="80" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 300px;">
                            <asp:Label runat="server" ID="lblMunicipio" Font-Bold="true" Text="Município:" />
                            <asp:TextBox runat="server" ID="txtMunicipio" CssClass="frmborder" Width="300" Enabled="false" />
                        </div>
                        <div class="control" style="width: 150px;">
                            <asp:Label runat="server" ID="lblEstado" Font-Bold="true" Text="Estado:" />
                            <asp:TextBox runat="server" ID="txtEstado" CssClass="frmborder" Width="150" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbEditar" Text="Editar" OnClick="lbEditar_Click" />
                        </div>
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbExcluir" Text="Excluir" OnClick="lbExcluir_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div id="divEndereco" runat="server">
                    <asp:Label runat="server" ID="lblTipoEndereco" Font-Bold="true" />
                    <asp:Label runat="server" ID="lblPreferencialEntrega" Font-Bold="true" Text="( Preferencial Para Entrega )" />
                    <br />
                    <div class="line">
                        <div class="control" style="width: 475px;">
                            <asp:Label runat="server" ID="lblNomeEndereco" Font-Bold="true" Text="Nome para Entrega:" />
                            <asp:TextBox runat="server" ID="txtNomeEntrega" CssClass="frmborder" Width="475"
                                Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 300px;">
                            <asp:Label runat="server" ID="lblLogradouro" Font-Bold="true" Text="Logradouro:" />
                            <asp:TextBox runat="server" ID="txtLogradouro" CssClass="frmborder" Width="300" Enabled="false" />
                        </div>
                        <div class="control" style="width: 50px;">
                            <asp:Label runat="server" ID="lblNumero" Font-Bold="true" Text="N°:" />
                            <asp:TextBox runat="server" ID="txtNumero" CssClass="frmborder" Width="50" Enabled="false" />
                        </div>
                        <div class="control" style="width: 80px;">
                            <asp:Label runat="server" ID="lblComplemento" Font-Bold="true" Text="Complemento:" />
                            <asp:TextBox runat="server" ID="txtComplemento" CssClass="frmborder" Width="80" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 373px;">
                            <asp:Label runat="server" ID="lblBairro" Font-Bold="true" Text="Bairro:" />
                            <asp:TextBox runat="server" ID="txtBairro" CssClass="frmborder" Width="373" Enabled="false" />
                        </div>
                        <div class="control" style="width: 80px;">
                            <asp:Label runat="server" ID="lblCEP" Font-Bold="true" Text="CEP:" />
                            <asp:TextBox runat="server" ID="txtCEP" CssClass="frmborder" Text="90010-311" Width="80"
                                Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 300px;">
                            <asp:Label runat="server" ID="lblMunicipio" Font-Bold="true" Text="Município:" />
                            <asp:TextBox runat="server" ID="txtMunicipio" CssClass="frmborder" Width="300" Enabled="false" />
                        </div>
                        <div class="control" style="width: 150px;">
                            <asp:Label runat="server" ID="lblEstado" Font-Bold="true" Text="Estado:" />
                            <asp:TextBox runat="server" ID="txtEstado" CssClass="frmborder" Width="150" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbEditar" Text="Editar" OnClick="lbEditar_Click" />
                        </div>
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbExcluir" Text="Excluir" OnClick="lbExcluir_Click" />
                        </div>
                    </div>
                </div>
            </AlternatingItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="pnlEndereco" Visible="false">
            <asp:HiddenField runat="server" ID="hddEnderecoId" />
            <div id="divEndereco" runat="server">
                <div class="line">
                    <div class="control" style="width: 475px;">
                        <asp:Label runat="server" ID="lblNomeEndereco" Font-Bold="true" Text="Nome para Entrega:" />
                        <asp:TextBox runat="server" ID="txtNomeEntrega" CssClass="frmborder" Width="475"
                            MaxLength="50" />
                    </div>
                </div>
                <div class="line">
                    <div class="control" style="width: 300px;">
                        <asp:Label runat="server" ID="lblLogradouro" Font-Bold="true" Text="Logradouro:" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLogradouro"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtLogradouro" CssClass="frmborder" Width="300" MaxLength="200" />
                    </div>
                    <div class="control" style="width: 50px;">
                        <asp:Label runat="server" ID="lblNumero" Font-Bold="true" Text="N°:" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNumeroEdicao"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtNumeroEdicao" CssClass="frmborder" Width="50"
                            MaxLength="10" />
                    </div>
                    <div class="control" style="width: 80px;">
                        <asp:Label runat="server" ID="lblComplemento" Font-Bold="true" Text="Complemento:" />
                        <asp:TextBox runat="server" ID="txtComplemento" CssClass="frmborder" Width="80" MaxLength="200" />
                    </div>
                </div>
                <div class="line">
                    <div class="control" style="width: 373px;">
                        <asp:Label runat="server" ID="lblBairro" Font-Bold="true" Text="Bairro:" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBairro"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtBairro" CssClass="frmborder" Width="373" MaxLength="100" />
                    </div>
                    <div class="control" style="width: 80px;">
                        <asp:Label runat="server" ID="lblCEP" Font-Bold="true" Text="CEP:" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCEPEdicao"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="2"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtCEPEdicao" CssClass="frmborder" Width="80" MaxLength="9" />
                    </div>
                </div>
                <div class="line">
                    <div class="control" style="width: 150px;">
                        <asp:Label runat="server" ID="lblEstado" Font-Bold="true" Text="Estado:" />
                        <asp:DropDownList runat="server" ID="ddlEstado" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"
                            CssClass="frmborder" Width="150" />
                    </div>
                    <div class="control" style="width: 300px;">
                        <asp:Label runat="server" ID="lblMunicipio" Font-Bold="true" Text="Município:" />
                        <asp:DropDownList runat="server" ID="ddlMunicipio" CssClass="frmborder" Width="300" />
                    </div>
                </div>
                <div class="line">
                    <div class="control" style="width: 75px;">
                        <asp:ImageButton ID="btnSalvarEndereco" runat="server" ImageUrl="~/img/btn_executar.gif"
                            Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
                            CausesValidation="true" OnClick="btnSalvarEndereco_Click" ValidationGroup="2"
                            CssClass="btnSubmitForm" />
                    </div>
                    <div class="control" style="width: 75px;">
                        <asp:ImageButton ID="btnCancelarEndereco" runat="server" ImageUrl="~/img/btn_cancelar.gif"
                            Width="73" Height="20" BorderWidth="0" AlternateText="Cancelar" ImageAlign="AbsMiddle"
                            CausesValidation="true" OnClick="btnCancelarEndereco_Click" CssClass="btnSubmitForm" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </fieldset>
</div>
<br />
<br />
<div id="divTelefoneLista" runat="server">
    <fieldset style="padding: 10px; width: 500px;">
        <legend>Telefones</legend>
        <asp:Repeater ID="rptTelefone" runat="server" OnItemDataBound="rptTelefone_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div id="divEndereco" runat="server" style="background-color: #F0F0F0;">
                    <asp:Label runat="server" ID="lblTipoTelefone" Font-Bold="true" />
                    <br />
                    <div class="line">
                        <div class="control" style="width: 50px;">
                            <asp:Label runat="server" ID="lblTelefoneDDD" Font-Bold="true" Text="DDD:" />
                            <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="frmborder" Width="50" Enabled="false" />
                        </div>
                        <div class="control" style="width: 100px;">
                            <asp:Label runat="server" ID="lblTelefone" Font-Bold="true" Text="Telefone:" />
                            <asp:TextBox runat="server" ID="txtTelefone" CssClass="frmborder" Width="100" Enabled="false" />
                        </div>
                        <div class="control" style="width: 100px;">
                            <asp:Label runat="server" ID="lblRamal" Font-Bold="true" Text="Ramal:" />
                            <asp:TextBox runat="server" ID="txtRamal" CssClass="frmborder" Width="100" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbEditarTel" Text="Editar" OnClick="lbEditarTel_Click" />
                        </div>
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbExcluirTel" Text="Excluir" OnClick="lbExcluirTel_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div id="divEndereco" runat="server">
                    <asp:Label runat="server" ID="lblTipoTelefone" Font-Bold="true" />
                    <br />
                    <div class="line">
                        <div class="control" style="width: 50px;">
                            <asp:Label runat="server" ID="lblTelefoneDDD" Font-Bold="true" Text="DDD:" />
                            <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="frmborder" Width="50" Enabled="false" />
                        </div>
                        <div class="control" style="width: 100px;">
                            <asp:Label runat="server" ID="lblTelefone" Font-Bold="true" Text="Telefone:" />
                            <asp:TextBox runat="server" ID="txtTelefone" CssClass="frmborder" Width="100" Enabled="false" />
                        </div>
                        <div class="control" style="width: 100px;">
                            <asp:Label runat="server" ID="lblRamal" Font-Bold="true" Text="Ramal:" />
                            <asp:TextBox runat="server" ID="txtRamal" CssClass="frmborder" Width="100" Enabled="false" />
                        </div>
                    </div>
                    <div class="line">
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbEditarTel" Text="Editar" OnClick="lbEditarTel_Click" />
                        </div>
                        <div class="control" style="width: 30px;">
                            <asp:LinkButton runat="server" ID="lbExcluirTel" Text="Excluir" OnClick="lbExcluirTel_Click" />
                        </div>
                    </div>
                </div>
            </AlternatingItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="pnlTelefone" Visible="false">
            <asp:HiddenField runat="server" ID="hddTelefoneId" />
            <div id="divTelefone" runat="server">
                <div class="line">
                    <div class="control" style="width: 50px;">
                        <asp:Label runat="server" ID="lblTelefoneDDD" Font-Bold="true" Text="DDD:" />
                        <asp:TextBox runat="server" ID="txtTelefoneDDDEdicao" CssClass="frmborder" Width="50"
                            MaxLength="5" />
                    </div>
                    <div class="control" style="width: 100px;">
                        <asp:Label runat="server" ID="lblTelefone" Font-Bold="true" Text="Telefone:" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTelefoneEdicao"
                            Display="Dynamic" ErrorMessage="Campo Obrigatório" ValidationGroup="3"></asp:RequiredFieldValidator>
                        <asp:TextBox runat="server" ID="txtTelefoneEdicao" CssClass="frmborder" Width="100"
                            MaxLength="8" />
                    </div>
                    <div class="control" style="width: 100px;">
                        <asp:Label runat="server" ID="lblRamal" Font-Bold="true" Text="Ramal:" />
                        <asp:TextBox runat="server" ID="txtRamalEdicao" CssClass="frmborder" Width="100"
                            MaxLength="10" />
                    </div>
                </div>
                <div class="line">
                    <div class="control" style="width: 75px;">
                        <asp:ImageButton ID="btnSalvarTel" runat="server" ImageUrl="~/img/btn_executar.gif"
                            Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
                            CausesValidation="true" OnClick="btnSalvarTel_Click" ValidationGroup="3" CssClass="btnSubmitForm" />
                    </div>
                    <div class="control" style="width: 75px;">
                        <asp:ImageButton ID="btnCancelarTel" runat="server" ImageUrl="~/img/btn_cancelar.gif"
                            Width="73" Height="20" BorderWidth="0" AlternateText="Cancelar" ImageAlign="AbsMiddle"
                            CausesValidation="true" OnClick="btnCancelarTel_Click" CssClass="btnSubmitForm" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </fieldset>
</div>

<a href="javascript: simulaAcesso();" class="frmborder"> <b>Simular navegação no site</b></a>
    <script>
        function simulaAcesso() {
            var form = document.getElementById('aspnetForm');
            var actionAnt = form.action;
            form.target = "_blank";
            form.action = document.getElementById('ctl00_holderPrincipal_ctl00_hddUrl').value;
            form.submit();

            form.target = "";
            form.action = actionAnt;
        }
    </script>
<asp:HiddenField runat="server" ID="hddUrl" />
<asp:HiddenField runat="server" ID="hddChave" />
<br />
<br />
<asp:Button runat="server" ID="btnEnviarSenha" Text="Enviar Senha" OnClick="btnEnviarSenha_Click" />
<asp:HiddenField runat="server" ID="hddCadastroPessoa" />
<asp:HiddenField runat="server" ID="hddEmail" />
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
