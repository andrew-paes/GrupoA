<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2SubForm.ascx.cs" Inherits="ctl_ag2SubForm" %>
<asp:Panel ID="pnlConteudo" CssClass="ctrlSubform" runat="server">
    <div class="divTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
        <asp:CustomValidator ID="ctSubForm" ClientValidationFunction="ValidaControlSubForm"
            Enabled="false" runat="server" ErrorMessage="Campo Obrigatório"></asp:CustomValidator>
    </div>
    <div class="divConteudoSubForm">
        <asp:Panel ID="pnlConteudoGrid" runat="server">
            <asp:GridView ID="gvSubForm" runat="server" Width="100%">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkItem" CssClass="chkAllSubForm" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItem" CssClass="chkItemSubForm" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#cccccc" />
            </asp:GridView>
        </asp:Panel>
        <asp:HyperLink ID="btnSelecionar" runat="server" NavigateUrl="javascript:;">Selecionar Registros</asp:HyperLink>
        &nbsp;|&nbsp;
        <asp:LinkButton ID="btnApagar" CausesValidation="false" CssClass="btnApagarSubForm"
            runat="server">Apagar Selecionados</asp:LinkButton>
    </div>
    <asp:Button ID="btnPostBak" CssClass="btnPostBak" runat="server" CausesValidation="false"
        Text="btnPostBak" />
    <asp:HiddenField ID="hdnIds" runat="server" />
</asp:Panel>
