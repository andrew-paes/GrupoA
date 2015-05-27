<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2UploadBrowser.ascx.cs"
    Inherits="ctl_ag2UploadBrowser" %>
<asp:Panel ID="pnlUploadBrowser" CssClass="pnlUploadBrowser" runat="server">
    <asp:Panel ID="pnlListaArquivos" Style="overflow: auto;" runat="server">
        <asp:HyperLink ID="AdicionarUploadBrowser" CssClass="AdicionarUploadBrowser" runat="server">
            <asp:Image ID="imgUploadBrowser" ImageUrl="~/img/btn_Adicionar.png" runat="server" />
        </asp:HyperLink>
    </asp:Panel>
    <asp:Button ID="btnAcao" runat="server" Style="display: none;" Text="Button" />
    <asp:HiddenField ID="hdnIds" runat="server"></asp:HiddenField>
    <asp:Panel ID="pnlGrid" runat="server">
        <asp:GridView ID="gridArquivos" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" ForeColor="Black"
            GridLines="Vertical" Width="100%">
            <Columns>
                <asp:BoundField DataField="arquivoId" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="imgIco" Width="18px" Height="18px" runat="server" />
                    </ItemTemplate>
                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="nomeOriginal" HeaderText="Nome do Aquivo" />
                <asp:BoundField DataField="tamanho" HeaderText="Tamanho ">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" ForeColor="Red" Font-Bold="true"
                            runat="server">Apagar</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
    </asp:Panel>
</asp:Panel>
