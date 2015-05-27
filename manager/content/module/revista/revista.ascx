<%@ Control Language="C#" AutoEventWireup="true" CodeFile="revista.ascx.cs" Inherits="content_module_revistas" %>

<asp:HiddenField ID="hdnRevistaId" runat="server" Value="0" />

<asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Nome:"></asp:Label>
<br />
<asp:TextBox ID="txtNomeRevista" Width="350px" CssClass="frmborder" Enabled="false"
    MaxLength="256" runat="server"></asp:TextBox>
<br />
<br />

<asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Descrição:"></asp:Label>
<asp:CustomValidator runat="server" ID="cvValidarDescricao" OnServerValidate="cvValidarDescricao_ServerValidate"
ValidationGroup="1"></asp:CustomValidator>
<br />
<ag2:HtmlTextBox runat="server" ID="txtDescricao" />
<br />
<br />

<asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Periodicidade:"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
    ControlToValidate="txtPeriodicidade" ErrorMessage="Campo Obrigatório" />
<br />
<asp:TextBox ID="txtPeriodicidade" Width="350px" CssClass="frmborder" MaxLength="256" runat="server"></asp:TextBox>
<br />
<br />

<asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Público Alvo:"></asp:Label>
<br />
<asp:TextBox ID="txtPublicoAlvo" Width="350px" CssClass="frmborder" TextMode="MultiLine" Rows="3" MaxLength="256" runat="server"></asp:TextBox>
<br />
<br />

<asp:Label runat="server" ID="Label6" Font-Bold="true" Text="ISSN:"></asp:Label>
<br />
<asp:TextBox ID="txtIssnRevista" Width="350px" CssClass="frmborder" MaxLength="150"  Enabled="false"
	runat="server"></asp:TextBox>
<br />
<br />
<br />

<div class="boxesc" style="vertical-align: bottom;">
	<asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
		Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
		OnClick="btnExecute_Click" CausesValidation="true" ValidationGroup="1" CssClass="btnSubmitForm" />
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[id$='txtPeriodicidade']").setMask({ mask: '9999', type: '', defaultValue: '' });
    });
</script>