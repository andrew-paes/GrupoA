<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2paging.ascx.cs" Inherits="ctl_paginacao" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" summary="">
    <tr>
        <td>
            <div class="pagprimeiras">
                <asp:ImageButton runat="server" ID="btnFirstPage" ImageUrl="~/img/btn_primeiras.gif"
                    AlternateText="Primeiras" BorderWidth="0" ImageAlign="AbsMiddle" OnClick="btnFirstPage_Click" /><asp:ImageButton
                        runat="server" ID="btnPreviousPage" ImageUrl="~/img/btn_anteriores.gif" AlternateText="Anteriores"
                        BorderWidth="0" ImageAlign="AbsMiddle" OnClick="btnPreviousPage_Click" />
            </div>
        </td>
        <td width="100%" align="center">
            <asp:PlaceHolder ID="phComboPaging" runat="server">Ir para a página
                <asp:DropDownList ID="cmbPaging" CssClass="cmdPaging" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="cmbPaging_SelectedIndexChanged">
                </asp:DropDownList>&nbsp;|&nbsp; 
            </asp:PlaceHolder>
            <asp:Label runat="server" ID="lblPaging" />
        </td>
        <td>
            <div class="pagultimas">
                <asp:ImageButton runat="server" ID="btnNextPage" ImageUrl="~/img/btn_proximas.gif"
                    AlternateText="Proximas" BorderWidth="0" ImageAlign="AbsMiddle" OnClick="btnNextPage_Click" /><asp:ImageButton
                        runat="server" ID="btnLastPage" ImageUrl="~/img/btn_ultimas.gif" AlternateText="Ultima"
                        BorderWidth="0" ImageAlign="AbsMiddle" OnClick="btnLastPage_Click" /></div>
        </td>
    </tr>
</table>
