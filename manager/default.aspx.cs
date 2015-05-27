using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.BusinessObject;

public partial class _default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        ltrBuild.Text = string.Concat(" - Versão ", Ag2.Manager.Helper.ConfigurationManager.Build);
    }

    protected void sendLogin_Click(object sender, ImageClickEventArgs e)
    {
        string userName = managerLogin.UserName.ToString();
        string userPassword = managerLogin.Password.ToString();

        User user = new User();
        UserStatus userStatus = user.Validate(userName, userPassword);

        if (userStatus != UserStatus.Valid)
        {
            Label FailureText = (Label)managerLogin.FindControl("FailureText");
            FailureText.Text = "Usuário ou senha incorretos";
        }
    }
}
