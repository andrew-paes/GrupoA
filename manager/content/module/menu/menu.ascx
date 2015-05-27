<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="content_module_menu_menu" %>
<asp:Label ID="msg" runat="server" Text="" ForeColor="red" Font-Bold="true" />
<asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Módulo Pai:"></asp:Label><br />
<asp:DropDownList ID="ddlModulo" runat="server" CssClass="frmborder">
</asp:DropDownList>
<br />
<br />
<asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtNome" Width="250px" CssClass="frmborder" MaxLength="30"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="lblNomeModulo" Font-Bold="true" Text="Nome do módulo:"></asp:Label>
<br />
<asp:TextBox runat="server" ID="txtNomeModulo" Width="250px" CssClass="frmborder"
    MaxLength="80"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label2" Font-Bold="true" Text="ToolTip:"></asp:Label><br />
<asp:TextBox runat="server" ID="txtTooltip" Width="250px" CssClass="frmborder" MaxLength="80"></asp:TextBox>
<br />
<br />
<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Ordem:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOrdem"
    ErrorMessage="Campo Obrigatório" ValidationGroup="1"></asp:RequiredFieldValidator><br />
<asp:TextBox runat="server" ID="txtOrdem" Width="25px" CssClass="frmborder" MaxLength="10"></asp:TextBox>
<br />
<br />
<asp:CheckBox runat="server" ID="cbAtivo" Text="Ativo" />
<br />
<br />
<b>Permissões:</b>
<br />
<asp:GridView GridLines="None" ID="gvMenu" runat="server" AutoGenerateColumns="false"
    OnRowDataBound="getMarcado_ItemDataBound" CellSpacing="0" AlternatingRowStyle-BackColor="#eeeeee"
    HeaderStyle-BackColor="#dddddd">
    <Columns>
        <asp:BoundField DataField="perfilId" />
        <asp:BoundField DataField="name" HeaderText="Perfil" ItemStyle-Width="180px" ItemStyle-Font-Bold="true"
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
        <asp:TemplateField HeaderText="Excluir"  HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:CheckBox ID="chkDelete" CssClass="chkGeral" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
