<%@ Page Language="C#" MasterPageFile="~/content/MasterPage.master" ValidateRequest="false"
    Trace="false" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="content_edit"
    Theme="Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="holderPrincipal" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" summary="" class="mb10">
        <tr>
            <td width="100%">
                <h1>
                    <asp:Label runat="server" ID="lblModuleTitle" /></h1>
            </td>
            <td width="120">
                <asp:HyperLink ID="lnkListagem" runat="server">
                    <img id="Img1" src="~/img/btn_listar.gif" border="0" alt="Listar Lorem ipsum" runat="server" />
                </asp:HyperLink>
            </td>
        </tr>
    </table>
    <div id="statusMessage" class="ManagerMessage">
        [texto]</div>
    <br />
    <div>
        <asp:PlaceHolder ID="fieldsHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="buttonBar" runat="server">
            <div class="boxesc">
                <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
                    Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
                    CausesValidation="true" OnClick="btnExecute_Click" CssClass="btnSubmitForm" />
            </div>
        </asp:PlaceHolder>
        <ag2:RegisterNavigator ID="RegisterNavigator1" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScripts" runat="Server">

</asp:Content>
