<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2menu.ascx.cs" Inherits="content_ag2menu" %>
<div id="Div1" class="menu2">
    <div id="menu" style="display: block;">
        <asp:PlaceHolder runat="server" ID="menuItemContainer" />
        <a href="http://www.artmed.com.br/" target="_blank">
            <asp:Image ID="imgSiteFrontEnd" runat="server" ImageUrl="~/img/img_site_frontend.jpg"
                AlternateText="Site Front-End"></asp:Image></a>
    </div>
</div>
