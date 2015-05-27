<%@ Control Language="C#" AutoEventWireup="true" CodeFile="autorTitulo.ascx.cs" Inherits="content_module_autorTitulo_autorTitulo" %>
<label>
    <b>Título:</b></label>
<br />
<asp:TextBox runat="server" ID="txtTitulo" Width="420px" CssClass="frmborder" Enabled="false"></asp:TextBox>
<br />
<br />
<label>
    <b>Subtítulo:</b></label>
<br />
<asp:TextBox runat="server" ID="txtSubtitulo" Width="420px" CssClass="frmborder"
    Enabled="false"></asp:TextBox>
<br />
<br />
<label>
    <b>ISBN:</b></label>
<br />
<asp:TextBox runat="server" ID="txtIsbn" Width="90px" CssClass="frmborder" Enabled="false"></asp:TextBox>
<br />
<br />
<br />
<div runat="server" id="divTeste" class="bordaDiv">
    <table width="720px">
        <tr>
            <td colspan="2">
                <fieldset style="padding:5px; margin:5px 10px 5px 0px">
                    <legend>Pesquisa de Autor</legend>
                    <asp:TextBox runat="server" ID="txtFiltroAutor" MaxLength="200" Width="150px" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/img/btn_buscar.png"
                        BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle" OnClick="btnPesquisar_Click"
                        CssClass="btnSubmitForm" />
                </fieldset>
            </td>
            <td colspan="2">
                <fieldset style="padding:5px; margin:5px 0px 5px 0px">
                    <legend>Inclusão Rápida de Autor</legend>
                    <asp:TextBox runat="server" ID="txtInclusaoRapida" MaxLength="200" Width="150px" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="btnInclusaoRapida" runat="server" ImageUrl="~/img/btn_Adicionar.png"
                        BorderWidth="0" AlternateText="Pesquisar" ImageAlign="AbsMiddle" 
                        CssClass="btnSubmitForm" onclick="btnInclusaoRapida_Click" />
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="4"><asp:Label ID="lblMensagem" runat="server" ForeColor="Red" /></td>
        </tr>
        <tr>
            <td width="250px">
                <asp:ListBox CssClass="frmborder" runat="server" Width="250px" Height="300px" ID="lstOrigem"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td colspan="2">
                <div style="vertical-align: top; margin:20px">
                    <center>
                        <asp:Button runat="server" ID="btnAdicionarT" Text="Adicionar Todos >>" OnClick="btnAdicionarT_Click"
                            Width="130px" />
                        <br />
                        <br />
                        <asp:Button runat="server" ID="btnAdicionar" Text="Adicionar >" OnClick="btnAdicionar_Click"
                            Width="130px" />
                    </center>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div style="vertical-align: bottom;">
                    <center>
                        <asp:Button runat="server" ID="btnRemover" Text="< Remover" OnClick="btnRemover_Click"
                            Width="130px" />
                        <br />
                        <br />
                        <asp:Button runat="server" ID="btnRemoverT" Text="<< Remover Todos" OnClick="btnRemoverT_Click"
                            Width="130px" />
                    </center>
                </div>
            </td>
            <td>
                <asp:ListBox CssClass="frmborder" runat="server" Width="250px" Height="300px" ID="lstDestino"
                    SelectionMode="Multiple" style="float:left"></asp:ListBox>
                <div style="vertical-align: center; float: left; padding:5px">
                    <center>
                        <asp:ImageButton runat="server" ID="btnSubir" Text="+++" ImageUrl="../../../img/btn_seta_up.gif"
                            OnClick="btnSubir_Click" />
                        <br />
                        <br />
                        <asp:ImageButton runat="server" ID="btnDescer" Text="---" ImageUrl="../../../img/btn_seta_down.gif"
                            OnClick="btnDescer_Click" />
                    </center>
                </div>
            </td>
        </tr>
    </table>
</div>
<br />
<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" />
</div>
