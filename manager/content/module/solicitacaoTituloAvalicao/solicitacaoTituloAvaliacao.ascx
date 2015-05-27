<%@ Control Language="C#" AutoEventWireup="true" CodeFile="solicitacaoTituloAvaliacao.ascx.cs"
    Inherits="content_module_solicitacaoTituloAvalicao_solicitacaoTituloAvalicao" %>
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
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:HiddenField ID="hddSolicitacaoTituloId" runat="server" Value="0" />
<asp:HiddenField ID="hddUsuarioId" runat="server" Value="0" />
<!-- Nome -->
<h3>
    Solicitação</h3>
<br />
<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Data da Solicitação:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtDataSolicitacao" Width="70px" CssClass="frmborder"
    MaxLength="100" ReadOnly="true"></asp:TextBox>
<br />
<br />
<%--<asp:Label runat="server" ID="lblJustificativa" Font-Bold="true" Text="Justificativa:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtJustificativa" Width="250px" CssClass="frmborder"
	MaxLength="100" ReadOnly="true"></asp:TextBox>
<br />
<br />--%>
<div runat="server" id="divStatus">
    <asp:Label runat="server" ID="lblStatus" Font-Bold="true" Text="Status Editora:"></asp:Label>
    <br />
    <asp:DropDownList runat="server" ID="ddlStatus">
    </asp:DropDownList>
    <asp:TextBox runat="server" ID="txtStatusEditora" Width="100px" ReadOnly="true" CssClass="frmborder"
        Text=""></asp:TextBox>
    <br />
    <br />
</div>
<div id="divProfessor" runat="server">
    <fieldset style="padding: 10px; width: 700px;">
        <legend><span id="fieldProfessor" style="cursor: pointer;">[-] Professor</span></legend>
        <table id="tableProfessor" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <!-- Graduação -->
                    <div class="line">
                        <asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome do Professor:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtNomeProfessor" Width="250px" CssClass="frmborder"
                            MaxLength="100" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="lblEmail" Font-Bold="true" Text="E-mail:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtEmail" Width="250px" CssClass="frmborder" MaxLength="100"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="lblCPF" Font-Bold="true" Text="CPF:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtCPF" Width="250px" CssClass="frmborder" MaxLength="100"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <div class="control" runat="server" id="Div1">
                            <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Graduação:" />
                            <asp:TextBox runat="server" ID="txtGraduacao" CssClass="frmborder" ReadOnly="true"
                                Width="673" />
                        </div>
                    </div>
                    <div class="line">
                        <asp:CheckBox ID="chkAutorGrupoA" runat="server" Enabled="false" Text=" Autor Grupo A"
                            Font-Bold="true" />
                    </div>
                    <div class="line">
                        <asp:CheckBox ID="chkColaboradorGrupoA" runat="server" Enabled="false" Text=" Colaborador Grupo A"
                            Font-Bold="true" />
                    </div>
                    <div class="line">
                        <asp:CheckBox ID="chkPossuiPublicacao" runat="server" Enabled="false" Text=" Possui Publicação"
                            Font-Bold="true" />
                    </div>
                    <div id="divEnderecoLista" runat="server">
                        <fieldset style="padding: 10px; width: 680px;">
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
                                                    ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="line">
                                            <div class="control" style="width: 300px;">
                                                <asp:Label runat="server" ID="lblLogradouro" Font-Bold="true" Text="Logradouro:" />
                                                <asp:TextBox runat="server" ID="txtLogradouro" CssClass="frmborder" Width="300" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 50px;">
                                                <asp:Label runat="server" ID="lblNumero" Font-Bold="true" Text="N°:" />
                                                <asp:TextBox runat="server" ID="txtNumero" CssClass="frmborder" Width="50" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 80px;">
                                                <asp:Label runat="server" ID="lblComplemento" Font-Bold="true" Text="Complemento:" />
                                                <asp:TextBox runat="server" ID="txtComplemento" CssClass="frmborder" Width="80" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="line">
                                            <div class="control" style="width: 373px;">
                                                <asp:Label runat="server" ID="lblBairro" Font-Bold="true" Text="Bairro:" />
                                                <asp:TextBox runat="server" ID="txtBairro" CssClass="frmborder" Width="373" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 80px;">
                                                <asp:Label runat="server" ID="lblCEP" Font-Bold="true" Text="CEP:" />
                                                <asp:TextBox runat="server" ID="txtCEP" CssClass="frmborder" Width="80" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="line">
                                            <div class="control" style="width: 300px;">
                                                <asp:Label runat="server" ID="lblMunicipio" Font-Bold="true" Text="Município:" />
                                                <asp:TextBox runat="server" ID="txtMunicipio" CssClass="frmborder" Width="300" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 150px;">
                                                <asp:Label runat="server" ID="lblEstado" Font-Bold="true" Text="Estado:" />
                                                <asp:TextBox runat="server" ID="txtEstado" CssClass="frmborder" Width="150" ReadOnly="true" />
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
                                                    ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="line">
                                            <div class="control" style="width: 300px;">
                                                <asp:Label runat="server" ID="lblLogradouro" Font-Bold="true" Text="Logradouro:" />
                                                <asp:TextBox runat="server" ID="txtLogradouro" CssClass="frmborder" Width="300" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 50px;">
                                                <asp:Label runat="server" ID="lblNumero" Font-Bold="true" Text="N°:" />
                                                <asp:TextBox runat="server" ID="txtNumero" CssClass="frmborder" Width="50" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 80px;">
                                                <asp:Label runat="server" ID="lblComplemento" Font-Bold="true" Text="Complemento:" />
                                                <asp:TextBox runat="server" ID="txtComplemento" CssClass="frmborder" Width="80" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="line">
                                            <div class="control" style="width: 373px;">
                                                <asp:Label runat="server" ID="lblBairro" Font-Bold="true" Text="Bairro:" />
                                                <asp:TextBox runat="server" ID="txtBairro" CssClass="frmborder" Width="373" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 80px;">
                                                <asp:Label runat="server" ID="lblCEP" Font-Bold="true" Text="CEP:" />
                                                <asp:TextBox runat="server" ID="txtCEP" CssClass="frmborder" Text="90010-311" Width="80"
                                                    ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="line">
                                            <div class="control" style="width: 300px;">
                                                <asp:Label runat="server" ID="lblMunicipio" Font-Bold="true" Text="Município:" />
                                                <asp:TextBox runat="server" ID="txtMunicipio" CssClass="frmborder" Width="300" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 150px;">
                                                <asp:Label runat="server" ID="lblEstado" Font-Bold="true" Text="Estado:" />
                                                <asp:TextBox runat="server" ID="txtEstado" CssClass="frmborder" Width="150" ReadOnly="true" />
                                            </div>
                                        </div>
                                    </div>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </fieldset>
                    </div>
                    <br />
                    <div id="divComprovanteDocencia" runat="server">
                        <fieldset style="padding: 10px; width: 680px;">
                            <legend>Comprovante Docência</legend>
                            <asp:Repeater ID="rptComprovanteDocencia" runat="server" OnItemDataBound="rptComprovanteDocencia_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="line">
                                        <div class="control" style="width: 380px; padding: 2px; background-color: #F0F0F0;">
                                            &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrInstituicao" />
                                        </div>
                                        <div class="control" style="width: 280px; padding: 2px; background-color: #F0F0F0;">
                                            &nbsp;&nbsp;<asp:HyperLink runat="server" ID="lnkDownloadArquivo" Target="_blank"
                                                Text="Visualizar Arquivo" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div class="line">
                                        <div class="control" style="width: 380px; padding: 2px;">
                                            &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrInstituicao" />
                                        </div>
                                        <div class="control" style="width: 280px; padding: 2px;">
                                            &nbsp;&nbsp;<asp:HyperLink runat="server" ID="lnkDownloadArquivo" Target="_blank"
                                                Text="Visualizar Arquivo" />
                                        </div>
                                    </div>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </fieldset>
                    </div>
                    <br />
                    <div id="divProfessorInstituicao" runat="server">
                        <fieldset style="padding: 10px; width: 680px;">
                            <legend>Instituições</legend>
                            <asp:Repeater ID="rptProfessorInstituicao" runat="server" OnItemDataBound="rptProfessorInstituicao_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div id="divInstituicao" runat="server" style="background-color: #F0F0F0;">
                                        <asp:Label runat="server" ID="lblNomeInstituicao" Font-Bold="true" />
                                        <br />
                                        <div class="line">
                                            <div class="control" style="width: 250px;">
                                                <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="Campus:" />
                                                <asp:TextBox runat="server" ID="txtCampus" CssClass="frmborder" Width="250" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 200px;">
                                                <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="Departamento:" />
                                                <asp:TextBox runat="server" ID="txtDepartamento" CssClass="frmborder" Width="200"
                                                    ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 30px;">
                                                <asp:Label runat="server" ID="Label10" Font-Bold="true" Text="DDD:" />
                                                <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="frmborder" Width="30" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 100px;">
                                                <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Telefone:" />
                                                <asp:TextBox runat="server" ID="txtTelefone" CssClass="frmborder" Width="100" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div id="divProfessorCurso" runat="server">
                                            <fieldset style="padding: 10px;">
                                                <legend>Cursos de
                                                    <asp:Label runat="server" ID="lblNomeInstituicao2" /></legend>
                                                <asp:Repeater ID="rptProfessorCurso" runat="server" OnItemDataBound="rptProfessorCurso_ItemDataBound">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="background-color: #FFFFFF;">
                                                            <asp:Label runat="server" ID="lblNomeCurso" Font-Bold="true" />
                                                            <br />
                                                            <div class="line">
                                                                <div class="control" style="width: 80px;">
                                                                    <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Coordenador:" />
                                                                    <asp:CheckBox ID="chkCoordenadorCurso" runat="server" Enabled="false" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Nivel:" />
                                                                    <asp:TextBox runat="server" ID="txtNivel" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Cargo:" />
                                                                    <asp:TextBox runat="server" ID="txtCargo" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divProfessorDisciplina" runat="server">
                                                            <fieldset style="padding: 10px;">
                                                                <legend>Disciplinas de
                                                                    <asp:Label runat="server" ID="lblNomeCurso2" /></legend>
                                                                <asp:Repeater ID="rptProfessorDisciplina" runat="server" OnItemDataBound="rptProfessorDisciplina_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Disciplina</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Nro Aluno</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Indica Título</b>
                                                                            </div>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </fieldset>
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background-color: #F0F0F0;">
                                                            <asp:Label runat="server" ID="lblNomeCurso" Font-Bold="true" />
                                                            <br />
                                                            <div class="line">
                                                                <div class="control" style="width: 80px;">
                                                                    <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Coordenador:" />
                                                                    <asp:CheckBox ID="chkCoordenadorCurso" runat="server" Enabled="false" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Nivel:" />
                                                                    <asp:TextBox runat="server" ID="txtNivel" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Cargo:" />
                                                                    <asp:TextBox runat="server" ID="txtCargo" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divProfessorDisciplina" runat="server">
                                                            <fieldset style="padding: 10px;">
                                                                <legend>Disciplinas de
                                                                    <asp:Label runat="server" ID="lblNomeCurso2" /></legend>
                                                                <asp:Repeater ID="rptProfessorDisciplina" runat="server" OnItemDataBound="rptProfessorDisciplina_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Disciplina</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Nro Aluno</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Indica Título</b>
                                                                            </div>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </fieldset>
                                                        </div>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div id="divInstituicao" runat="server">
                                        <asp:Label runat="server" ID="lblNomeInstituicao" Font-Bold="true" />
                                        <br />
                                        <div class="line">
                                            <div class="control" style="width: 250px;">
                                                <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="Campus:" />
                                                <asp:TextBox runat="server" ID="txtCampus" CssClass="frmborder" Width="250" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 200px;">
                                                <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="Departamento:" />
                                                <asp:TextBox runat="server" ID="txtDepartamento" CssClass="frmborder" Width="200"
                                                    ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 30px;">
                                                <asp:Label runat="server" ID="Label10" Font-Bold="true" Text="DDD:" />
                                                <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="frmborder" Width="30" ReadOnly="true" />
                                            </div>
                                            <div class="control" style="width: 100px;">
                                                <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Telefone:" />
                                                <asp:TextBox runat="server" ID="txtTelefone" CssClass="frmborder" Width="100" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div id="divProfessorCurso" runat="server">
                                            <fieldset style="padding: 10px;">
                                                <legend>Cursos de
                                                    <asp:Label runat="server" ID="lblNomeInstituicao2" /></legend>
                                                <asp:Repeater ID="rptProfessorCurso" runat="server" OnItemDataBound="rptProfessorCurso_ItemDataBound">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="background-color: #FFFFFF;">
                                                            <asp:Label runat="server" ID="lblNomeCurso" Font-Bold="true" />
                                                            <br />
                                                            <div class="line">
                                                                <div class="control" style="width: 80px;">
                                                                    <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Coordenador:" />
                                                                    <asp:CheckBox ID="chkCoordenadorCurso" runat="server" Enabled="false" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Nivel:" />
                                                                    <asp:TextBox runat="server" ID="txtNivel" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Cargo:" />
                                                                    <asp:TextBox runat="server" ID="txtCargo" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divProfessorDisciplina" runat="server">
                                                            <fieldset style="padding: 10px;">
                                                                <legend>Disciplinas de
                                                                    <asp:Label runat="server" ID="lblNomeCurso2" /></legend>
                                                                <asp:Repeater ID="rptProfessorDisciplina" runat="server" OnItemDataBound="rptProfessorDisciplina_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Disciplina</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Nro Aluno</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Indica Título</b>
                                                                            </div>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </fieldset>
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background-color: #F0F0F0;">
                                                            <asp:Label runat="server" ID="lblNomeCurso" Font-Bold="true" />
                                                            <br />
                                                            <div class="line">
                                                                <div class="control" style="width: 80px;">
                                                                    <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Coordenador:" />
                                                                    <asp:CheckBox ID="chkCoordenadorCurso" runat="server" Enabled="false" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Nivel:" />
                                                                    <asp:TextBox runat="server" ID="txtNivel" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                                <div class="control" style="width: 250px;">
                                                                    <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="Cargo:" />
                                                                    <asp:TextBox runat="server" ID="txtCargo" CssClass="frmborder" Width="250" ReadOnly="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divProfessorDisciplina" runat="server">
                                                            <fieldset style="padding: 10px;">
                                                                <legend>Disciplinas de
                                                                    <asp:Label runat="server" ID="lblNomeCurso2" /></legend>
                                                                <asp:Repeater ID="rptProfessorDisciplina" runat="server" OnItemDataBound="rptProfessorDisciplina_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Disciplina</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Nro Aluno</b>
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<b>Indica Título</b>
                                                                            </div>
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #F0F0F0;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div class="line">
                                                                            <div class="control" style="width: 400px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNomeDisciplina" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrNroAlunos" />
                                                                            </div>
                                                                            <div class="control" style="width: 100px; padding: 2px; background-color: #FFFFFF;">
                                                                                &nbsp;&nbsp;<asp:Literal runat="server" ID="ltrIndicaTitulo" />
                                                                            </div>
                                                                        </div>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </fieldset>
                                                        </div>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                        </div>
                                    </div>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<br />
<div id="divTitulo" runat="server">
    <fieldset style="padding: 10px; width: 700px;">
        <legend><span id="fieldTitulo" style="cursor: pointer;">[-] Título</span></legend>
        <table id="tableTitulo" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <div class="line">
                        <asp:Label runat="server" ID="lblTitulo" Font-Bold="true" Text="Nome do Título:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtTitulo" Width="450px" CssClass="frmborder" MaxLength="100"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="lblEdicao" Font-Bold="true" Text="Edição:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtEdicao" Width="30px" CssClass="frmborder" MaxLength="100"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="lblISBN" Font-Bold="true" Text="ISBN 13:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtISBN" Width="95px" CssClass="frmborder" MaxLength="100"
                            ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="lblDataPublicacao" Font-Bold="true" Text="Data da Publicação:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtDataPublicacao" Width="70px" CssClass="frmborder"
                            MaxLength="100" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="Label20" Font-Bold="true" Text="Autores:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtAutorTitulo" Width="450px" CssClass="frmborder"
                            MaxLength="100" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<br />
<div id="divAvaliacao" runat="server">
    <fieldset style="padding: 10px; width: 700px;">
        <legend><span id="fieldAvaliacao" style="cursor: pointer;">[-] Avaliação</span></legend>
        <table id="tableAvaliacao" cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <div class="line">
                        <br />
                        <asp:Label runat="server" ID="label21" Text="Status Professor:"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtStatusProfessor" Width="70px" ReadOnly="true"
                            CssClass="frmborder" Text=""></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="trAvaliacao">
                <td>
                    <asp:HiddenField runat="server" ID="hddAvaliacaoId" />
                    <div class="line">
                        <div class="bordaDiv">
                            <table width="600px">
                                <tr>
                                    <td>
                                        <h3>
                                            ITENS DE AVALIAÇÃO</h3>
                                    </td>
                                    <td>
                                        <h3>
                                            1</h3>
                                    </td>
                                    <td>
                                        <h3>
                                            2</h3>
                                    </td>
                                    <td>
                                        <h3>
                                            3</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label" Text="Relevância da obra"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbRelevancia1" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbRelevancia2" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbRelevancia3" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtRelevancia" TextMode="MultiLine" Rows="2" Width="580px"
                                            ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label2" Text="Conteúdo atualizado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbConteudo1" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbConteudo2" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbConteudo3" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtConteudoAtualizado" TextMode="MultiLine" Rows="2"
                                            Width="580px" ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label3" Text="Qualidade do texto"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbQualidade1" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbQualidade2" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbQualidade3" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtQualidadeTexto" TextMode="MultiLine" Rows="2"
                                            Width="580px" ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label4" Text="Apresentação gráfica"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbApresentacao1" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbApresentacao2" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbApresentacao3" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtApresentacaoGrafica" TextMode="MultiLine" Rows="2"
                                            Width="580px" ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label5" Text="Material complementar para o professor"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbMaterial1" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbMaterial2" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbMaterial3" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtMaterialComplementar" TextMode="MultiLine" Rows="2"
                                            Width="580px" ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="label14" Text="Avaliação geral da obra"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbAvaliacao1" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbAvaliacao2" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbAvaliacao3" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox runat="server" ID="txtAvaliacaoGeral" TextMode="MultiLine" Rows="2"
                                            Width="580px" ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="line">
                        <br />
                        <asp:Label runat="server" ID="label15" Text="Pontos fortes da obra"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtPontosFortes" TextMode="MultiLine" Rows="2" Width="600px"
                            ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="label16" Text="Pontos fracos da obra"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtPontosFracos" TextMode="MultiLine" Rows="2" Width="600px"
                            ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                    </div>
                    <div class="line">
                        <asp:Label runat="server" ID="label17" Text="Sugestões"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtSugestoes" TextMode="MultiLine" Rows="2" Width="600px"
                            ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                    </div>
                    <br />
                    <div class="bordaDiv">
                        <div class="line">
                            <h3>
                                DADOS SOBRE ADOÇÃO</h3>
                        </div>
                        <div class="line">
                            <asp:Label runat="server" ID="label18" Text="A obra será adotada como livro-texto na disciplina?"></asp:Label>&nbsp;&nbsp;
                            <asp:RadioButton runat="server" ID="rbAdotadaSim" Enabled="false" Text="Sim" />
                            <asp:RadioButton runat="server" ID="rbAdotadaNao" Enabled="false" Text="Não" /><br />
                            <asp:Panel runat="server" ID="pnlAdotada">
                                <asp:Label runat="server" ID="lblAdotadaQuais" Text="Quais?"></asp:Label><br />
                                <asp:TextBox runat="server" ID="txtAdotadaQuais" TextMode="MultiLine" Rows="2" Width="580px"
                                    ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                            </asp:Panel>
                        </div>
                        <div class="line">
                            <asp:Label runat="server" ID="label19" Text="A obra será recomendada como texto complementar na disciplina?"></asp:Label>&nbsp;&nbsp;
                            <asp:RadioButton runat="server" ID="rbRecomendadaSim" Enabled="false" Text="Sim" />
                            <asp:RadioButton runat="server" ID="rbRecomendadaNao" Enabled="false" Text="Não" /><br />
                            <asp:Panel runat="server" ID="pnlRecomendada">
                                <asp:Label runat="server" ID="lblRecomendadaQuais" Text="Quais?"></asp:Label><br />
                                <asp:TextBox runat="server" ID="txtRecomendadaQuais" TextMode="MultiLine" Rows="2"
                                    Width="580px" ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                            </asp:Panel>
                        </div>
                        <div class="line">
                            <br />
                            <asp:CheckBox runat="server" ID="chkNaoSeAplica" Text=" A obra não se aplica à disciplina"
                                Enabled="false" />
                        </div>
                        <asp:Panel runat="server" ID="pnlPorque" CssClass="line">
                            <asp:Label runat="server" ID="label23" Text="Por quê?"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtPorque" TextMode="MultiLine" Rows="2" Width="580px"
                                ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlObraAdotada" CssClass="line">
                            <asp:Label runat="server" ID="label22" Text="Obra adotada"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtObraAdotada" TextMode="MultiLine" Rows="2" Width="580px"
                                ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlAutor" CssClass="line">
                            <asp:Label runat="server" ID="label24" Text="Autor"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtAutor" TextMode="MultiLine" Rows="2" Width="580px"
                                ReadOnly="true" CssClass="frmborder" Text="Relevância da obra"></asp:TextBox>
                        </asp:Panel>
                    </div>
                    <div class="line">
                        <br />
                        <asp:Label runat="server" ID="label25" Text="Tenho disponibilidade para parcerias com o Grupo A, tais como:"></asp:Label><br />
                        <asp:CheckBox runat="server" ID="chkRevisorTecnico" Text=" Revisor técnico" Enabled="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="label26" Text="Tradutor:"></asp:Label>
                        <asp:CheckBox runat="server" ID="chkIngles" Text=" Inglês" Enabled="false" />
                        <asp:CheckBox runat="server" ID="chkEspanhol" Text=" Espanhol" Enabled="false" />
                        <asp:CheckBox runat="server" ID="chkFrances" Text=" Francês" Enabled="false" />
                        <asp:CheckBox runat="server" ID="chkAlemao" Text=" Alemão" Enabled="false" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<asp:Panel runat="server" ID="pnlDestaqueAvaliacao">
    <div class="line">
        <br />
        <br />
        <asp:Label runat="server" ID="label28" Text="Dados do Avaliador:"></asp:Label>
        <asp:Label runat="server" ID="label29" Text="(nome/instituição)"></asp:Label>
        <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNomeAvaliador"
            ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
        <asp:TextBox runat="server" ID="txtNomeAvaliador" Width="380px" CssClass="frmborder" MaxLength="70"></asp:TextBox>
    </div>
    <div class="line">
        <asp:Label runat="server" ID="label27" Text="Destaque de Avaliação:"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAvaliacao"
            ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
        <asp:TextBox runat="server" ID="txtAvaliacao" TextMode="MultiLine" Rows="2" Width="580px"
                CssClass="frmborder"></asp:TextBox>
    </div>
</asp:Panel>
<br />
<br />
<div class="boxesc" runat="server" id="pnlBotao" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<script type="text/javascript">
	//<![CDATA[
    $(document).ready(function () {
        SetTable($('#fieldProfessor'), $('#tableProfessor'), 'Professor');
        SetTable($('#fieldTitulo'), $('#tableTitulo'), 'Título');
        SetTable($('#fieldAvaliacao'), $('#tableAvaliacao'), 'Avaliação');

        $('textarea[id$="txtAvaliacao"]').keyup(function () {
            var max = 390;
            if ($(this).val().length > max) {
                $(this).val($(this).val().substr(0, max));
            }
        });

        $('#fieldProfessor').click(function () {
            var field = $(this);
            var table = $('#tableProfessor');

            SetTable(field, table, "Professor");
        });

        $('#fieldTitulo').click(function () {
            var field = $(this);
            var table = $('#tableTitulo');

            SetTable(field, table, "Título");
        });

        $('#fieldAvaliacao').click(function () {
            var field = $(this);
            var table = $('#tableAvaliacao');

            SetTable(field, table, "Avaliação");
        });
    });

    function SetTable(field, table, nomeProduto) {
        table.is(":visible") ? HideTable(field, table, nomeProduto) : ShowTable(field, table, nomeProduto);
    }

    function HideTable(field, table, nomeProduto) {
        field.html("[+] " + nomeProduto);
        table.hide();
    }

    function ShowTable(field, table, nomeProduto) {
        field.html("[-] " + nomeProduto);
        table.show();
    }
	//]]>
</script>
