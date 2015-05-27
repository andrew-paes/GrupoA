<%@ Control Language="C#" AutoEventWireup="true" CodeFile="idioma.ascx.cs" Inherits="content_module_idioma_idioma" %>
<div>
    <div>
        <h1>
            Configuração de Idiomas</h1>
    </div>
    <div style="margin-top: 10px; padding: 3px; border: 1px solid #ccc; width: 500px;
        background-color: #f4f4f4; overflow: auto;">
        <div style="padding: 1px 0 1px 3px; background-color: #ddd;">
            Idiomas do manager
        </div>
        <asp:UpdatePanel ID="upIdioma" runat="server">
            <ContentTemplate>
                <div style="margin-top: 10px;">
                    <asp:Repeater ID="rptIdiomas" runat="server">
                        <ItemTemplate>
                            <div style="float: left; padding: 3px; border: 1px solid #ccc; margin-right: 3px;
                                width: 155px; margin-top: 2px;">
                                <asp:CheckBox ID="chkIdioma" AutoPostBack="true" OnCheckedChanged="chkIdioma_CheckedChanged"
                                    runat="server" />
                                <asp:Image ID="imgFlag" ImageUrl="~/img/flag_portugues.png" runat="server" />
                                <asp:Literal ID="ltrIdioma" runat="server"></asp:Literal>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
