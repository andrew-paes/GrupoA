<%@ Page Language="C#" MasterPageFile="~/content/MasterPage.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="content_default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="holderPrincipal" runat="Server">
    <asp:PlaceHolder ID="phDemos" Visible="false" runat="server">
        <ag2:UploadBrowser ID="UploadBrowser1" AllowedExtensions="*.jpg,*.png,*.gif" runat="server" />
        <ag2:UploadBrowser ID="UploadBrowser2" AllowedExtensions="*.pdf" runat="server" />
        <ag2:UploadBrowser ID="UploadBrowser3" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
