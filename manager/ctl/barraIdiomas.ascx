<%@ Control Language="C#" AutoEventWireup="true" CodeFile="barraIdiomas.ascx.cs"
    Inherits="ctl_barraIdiomas" %>
<div class="barraIdioma">
    <asp:Repeater ID="rptIdiomas" runat="server">
        <ItemTemplate>
            <asp:Panel ID="pnlFlags" runat="server">
                <asp:ImageButton ID="imgIdioma" OnClick="imgIdioma_Click" CausesValidation="false"
                    runat="server" />
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
</div>
