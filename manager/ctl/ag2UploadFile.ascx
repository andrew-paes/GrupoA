<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ag2UploadFile.ascx.cs"
    Inherits="ctl_ag2UploadFile" %>
<div style="background-color: #f4f4f4; border: 1px solid #ccc; padding: 5px; width: 450px;
    margin-top: 10px;">
    <div>
        <strong>
            <asp:Literal ID="ltrLabel" runat="server"></asp:Literal>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtArquivo"
                runat="server" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
        </strong>
    </div>
    <div>
        <asp:TextBox ID="txtArquivo" runat="server" Width="320px" BorderStyle="Solid" BorderWidth="1px"
            BorderColor="#cccccc" Height="18px"></asp:TextBox>
        <asp:HyperLink ID="lnkUploadModal" CssClass="ModalUploadFile" ForeColor="#333333"
            Font-Bold="true" runat="server">Selecionar Arquivo</asp:HyperLink>
    </div>
    <div style="padding: 5px;">
        <strong>Tamanho máximo de arquivo: </strong>
        <asp:Literal ID="ltrTamanhoMaximo" runat="server"></asp:Literal>
        &nbsp;&nbsp;|&nbsp;&nbsp; <strong>Tipos de arquivos: </strong>
        <asp:Literal ID="ltrTipoArquivo" runat="server"></asp:Literal>
    </div>
</div>
