<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listaCupom.aspx.cs" Inherits="content_module_promocao_listaCupom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Cupons</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvCupom" runat="server" AutoGenerateColumns="False" BackColor="White"
			BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" EmptyDataText="Sem cupons para exibir"
			ForeColor="Black" GridLines="Vertical" OnRowDataBound="gvCupom_RowDataBound">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:TemplateField HeaderText="Cupom">
				    <ItemTemplate>
					    <asp:Label ID="lblCupom" runat="server" Text='' />
				    </ItemTemplate>
				    <ItemStyle Height="20px" Width="300px" HorizontalAlign="Center" VerticalAlign="Middle" />
			    </asp:TemplateField>
                <asp:TemplateField HeaderText="Pedido">
				    <ItemTemplate>
					    <asp:Label ID="lblPedido" runat="server" Text='' />
				    </ItemTemplate>
				    <ItemStyle Height="20px" Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
			    </asp:TemplateField>
                <asp:TemplateField HeaderText="Nome">
				    <ItemTemplate>
					    <asp:Label ID="lblNome" runat="server" Text='' />
				    </ItemTemplate>
				    <ItemStyle Height="20px" Width="300px" HorizontalAlign="Center" VerticalAlign="Middle" />
			    </asp:TemplateField>
                <asp:TemplateField HeaderText="Usuário">
				    <ItemTemplate>
					    <asp:Label ID="lblUsuario" runat="server" Text='' />
				    </ItemTemplate>
				    <ItemStyle Height="20px" Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
			    </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
			<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
			<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
			<AlternatingRowStyle BackColor="White" />
		</asp:GridView>
    </div>
    </form>
</body>
</html>
