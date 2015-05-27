<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="src_Header" %>
<div id="topo">
    <asp:PlaceHolder ID="phLogoLink" Visible="false" runat="server">
        <asp:HyperLink ID="lnkHome" ToolTip="Página Principal" NavigateUrl="~/content/default.aspx" runat="server">
            <asp:Image runat="server" ID="imgLogo1" ToolTip="Página Principal" ImageUrl="~/img/img_logo_cliente.jpg" CssClass="cliente">
            </asp:Image>
        </asp:HyperLink>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phLogo" Visible="true" runat="server">
        <asp:Image runat="server" ID="imgLogo2" ImageUrl="~/img/img_logo_cliente.jpg" CssClass="cliente">
        </asp:Image>
    </asp:PlaceHolder>
    <asp:Image ID="Image1" runat="server" AlternateText="AG2 | Content Manager" CssClass="ag2"
        ImageUrl="~/img/img_ag2_content_manager.jpg"></asp:Image>
</div>
