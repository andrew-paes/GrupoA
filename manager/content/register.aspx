<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="content_register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="divModalCad">
        <asp:Label ID="lblDescricao" runat="server"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDescricao"
            runat="server" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtDescricao" CssClass="frmborder" Width="383px" runat="server"
            MaxLength="50"></asp:TextBox>
        <div class="divScrolling">
            <asp:GridView ID="gvItem" AutoGenerateColumns="False" CssClass="tblCad" ShowHeader="False"
                runat="server" OnRowDeleting="gvItem_RowDeleting" OnSelectedIndexChanging="gvItem_SelectedIndexChanging">
                <Columns>
                    <asp:BoundField DataField="" />
                    <asp:BoundField DataField="" />
                    <asp:CommandField SelectText="Editar" ButtonType="Image" SelectImageUrl="~/img/reply.gif"
                        ShowSelectButton="True">
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:CommandField DeleteText="Apagar" ButtonType="Image" DeleteImageUrl="~/img/action_delete.gif"
                        ControlStyle-ForeColor="Red" ShowDeleteButton="True">
                        <ControlStyle ForeColor="Red"></ControlStyle>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="boxesc" style="vertical-align: bottom;">
            <asp:ImageButton ID="btnExecute" runat="server" ImageUrl="~/img/btn_executar.gif"
                Width="73" Height="20" BorderWidth="0" AlternateText="Executar" ImageAlign="AbsMiddle"
                CausesValidation="true" OnClick="btnExecute_Click" />
        </div>
    </div>
    </form>
</body>
</html>
