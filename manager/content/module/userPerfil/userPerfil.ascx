<%@ Control Language="C#" AutoEventWireup="true" CodeFile="userPerfil.ascx.cs" Inherits="content_module_atendimento_atendimento" %>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtNome" Width="250px" CssClass="frmborder" MaxLength="80"></asp:TextBox>
<br />
<br />
<div style="border: 1px solid #ccc; padding: 3px; background-color: #f4f4f4; width: 252px;">
    <asp:CheckBox ID="chkPermitirResetPassword" Text="Permitir resetar a senha de usuários"
        runat="server" />
</div>
<div style="margin-top: 10px;">
    <b>Permissões:</b>
    <br />
    <asp:Repeater ID="rptMenu" runat="server" OnItemDataBound="getInfoPonto_ItemDataBound">
        <ItemTemplate>
            <div class="fio">
                <!--  -->
            </div>
            <table width="508" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" style="margin-left: 1px;
                margin-top: 10px;">
                <tr>
                    <td class="lista" height="20" style="padding-left: 10px">
                        <strong>
                            <%#DataBinder.Eval(Container.DataItem, "name")%>
                        </strong>
                    </td>
                </tr>
            </table>
            <div style="margin-left: 25px;">
                <asp:GridView GridLines="None" ID="gvSubMenu" runat="server" AutoGenerateColumns="false"
                    OnRowDataBound="getMarcado_ItemDataBound" CellSpacing="0" AlternatingRowStyle-BackColor="#eeeeee"
                    HeaderStyle-BackColor="#dddddd">
                    <Columns>
                        <asp:BoundField DataField="menuId" />
                        <asp:BoundField DataField="name" HeaderText="Módulo" ItemStyle-Width="180px" ItemStyle-Font-Bold="true"
                            ItemStyle-ForeColor="Gray" />
                        <asp:TemplateField HeaderText="Controle total" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkFull" CssClass="chkFull" runat="server" Text="" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ler" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRead" CssClass="chkGeral" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inserir" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkInsert" CssClass="chkGeral" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUpdate" CssClass="chkGeral" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDelete" CssClass="chkGeral" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:GridView ID="grdLista" runat="server" Width="100%" CssClass="frmborder" AutoGenerateColumns="false"
        DataKeyNames="menuId">
        <RowStyle CssClass="listaInternaNormal" />
        <AlternatingRowStyle CssClass="listaInternaCinza" />
        <HeaderStyle CssClass="listaInternaCabecalho" />
        <Columns>
            <asp:BoundField HeaderText="Nome" DataField="name" Visible="true" HeaderStyle-CssClass="listaInternaCabecalhoPrimeiro"
                ItemStyle-HorizontalAlign="center" />
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckAdicionarParticipante" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <br />
</div>
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
