<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2RelacionarList.ascx.cs" Inherits="ctl_ag2RelacionarList" %>

<table>
<tr>
<td>
    <asp:Label runat="server" ID="lblTitulo1"></asp:Label>
</td>
<td></td>
<td>
    <asp:Label runat="server" ID="lblTitulo2"></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:ListBox CssClass="frmborder"  runat="server" Width="200px" Height="300px" ID="lstOrigem"></asp:ListBox>
</td>
<td>
    
        <div style="vertical-align:top;">
            <center>
            <asp:Button runat="server" ID="btnAdicionarT" Text="Adicionar Todos >>" OnClick="btnAdicionarT_Click" Width="130px" />
            <br /><br />
            <asp:Button runat="server" ID="btnAdicionar" Text="Adicionar >" OnClick="btnAdicionar_Click" Width="130px"/>
            </center>
        </div>
    <br /><br /><br /><br />
        <div  style="vertical-align:bottom;">
            <center>
            <asp:Button runat="server" ID="btnRemover" Text="< Remover" OnClick="btnRemover_Click" Width="130px"/>
            <br /><br />
            <asp:Button runat="server" ID="btnRemoverT" Text="<< Remover Todos" OnClick="btnRemoverT_Click" Width="130px"/>
            </center>
        </div>
    
</td>
<td>
    <asp:ListBox CssClass="frmborder" runat="server" Width="200px" Height="300px" ID="lstDestino"></asp:ListBox>
</td>
</tr>

</table>