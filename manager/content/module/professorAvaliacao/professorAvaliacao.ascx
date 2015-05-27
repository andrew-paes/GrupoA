<%@ Control Language="C#" AutoEventWireup="true" CodeFile="professorAvaliacao.ascx.cs" Inherits="content_module_pessoa_pessoa" %>
<asp:HiddenField ID="hddUsuarioId" runat="server" Value="0" />
<style>
    div.line
        {
        display:block;
        margin-bottom: 5px;
        }
    div.control
        {
        width:250px;
        padding:10px 10px 10px 10px;
        display: inline-block;
        }
    div.control input
        {
        width:95%;
        }    
    div.control span
        {
        display:block;
        }
</style>
<!-- Nome/E-mail -->
<div class="line">
    <div class=control>
        <asp:Label runat="server" ID="lblNome" Font-Bold="true" Text="Nome:"></asp:Label>
        <asp:TextBox runat="server" ID="txtNome" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
    <div class=control>
        <asp:Label runat="server" ID="lblEmail" Font-Bold="true" Text="E-mail:"></asp:Label>
        <asp:TextBox runat="server" ID="txtEmail" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
</div>
<!-- Tipo Pessoa / CPF-CNPJ -->
<div class="line">
    <div class=control>
        <asp:Label runat="server" ID="lblCPF" Font-Bold="true" Text="CPF:"></asp:Label>
        <asp:TextBox runat="server" ID="txtCPF" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
</div>
<!-- Título e ISBN13 -->
<div class="line">
    <div class=control>
        <asp:Label runat="server" ID="lblNomeDoTitulo" Font-Bold="true" Text="Nome do Título:"></asp:Label>
        <asp:TextBox runat="server" ID="txtNomeDoTitulo" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
    <div class=control>
        <asp:Label runat="server" ID="lblISBN13" Font-Bold="true" Text="ISBN13:"></asp:Label>
        <asp:TextBox runat="server" ID="txtISBN13" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
</div>
<!-- Avaliação -->
<div class="line">
    <div class=control>
        <asp:Label runat="server" ID="lblAvaliacao" Font-Bold="true" Text="Avaliação:"></asp:Label>
        <asp:TextBox runat="server" ID="txtAvaliacao" CssClass="frmborder" Enabled="false" TextMode="MultiLine" Width="510px" MaxLength="2000" ></asp:TextBox>
        
    </div>
</div>
<!-- Finalizada e Data Realização Avaliação -->
<div class="line">
    <div class=control>
        <asp:Label runat="server" ID="lblFinalizada" Font-Bold="true" Text="Finalizada:"></asp:Label>
        <asp:TextBox runat="server" ID="txtFinalizada" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
    <div class=control>
        <asp:Label runat="server" ID="lblDataAvaliacao" Font-Bold="true" Text="Data realização avaliação:"></asp:Label>
        <asp:TextBox runat="server" ID="txtDataAvaliacao" CssClass="frmborder" Enabled="false"></asp:TextBox>
    </div>
</div>
<!-- Telefones -->
<div runat="server" id="areaTelefones">
    <br />
    <h3>Telefones</h3>
    <asp:DataGrid runat="server" Width="500px" ID="dgTelefones" CellPadding="4" ForeColor="Black"
        GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
        BorderWidth="1px" AutoGenerateColumns="False">
        <FooterStyle BackColor="#CCCC99" />
        <EditItemStyle Width="300px" />
        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
        <AlternatingItemStyle BackColor="White" />
        <ItemStyle Width="150px" BackColor="#FFEBD7" Font-Bold="False" Font-Italic="False"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        <Columns>
            <asp:TemplateColumn HeaderText="Culture" SortExpression="Tipo">
                  <ItemTemplate>
                    <asp:Label ID="lblCulture" runat="server" Text='<%# Eval("TelefoneTipo.TipoTelefone") %>' />
                  </ItemTemplate>
                  <ItemStyle Height="20px" Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="DDD" DataField="dddTelefone" ></asp:BoundColumn>
            <asp:BoundColumn HeaderText="Número" DataField="numeroTelefone" ></asp:BoundColumn>
        </Columns>
        <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" Font-Italic="False"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
    </asp:DataGrid>
    <br />
    <br />
</div>


<%--<div class="boxesc" style="vertical-align: bottom;">
    <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
        Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
        CausesValidation="true" OnClick="btnExecute_Click" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
--%>