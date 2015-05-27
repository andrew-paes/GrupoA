<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2CkEditor.ascx.cs" Inherits="ctl_ag2CkEditor" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<CKEditor:CKEditorControl ID="txtCKEditor" runat="server"></CKEditor:CKEditorControl>


<%--<div style="margin-top: 10px;">
    <asp:Label ID="lblLabel" runat="server" Font-Bold="true" Text=""></asp:Label>
    <asp:CustomValidator ID="cvCKEditor" ValidationGroup="1" ClientValidationFunction="validaCKEditor"
        runat="server" ErrorMessage="Campo Obrigatório"></asp:CustomValidator>
    <div>
        <asp:TextBox ID="txtCKEditor" runat="server" Height="153px" TextMode="MultiLine"
            Width="100%"></asp:TextBox>
    </div>
</div>
--%>