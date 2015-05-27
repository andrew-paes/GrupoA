using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;

public partial class content_AlteraSenha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEnviar_Click(object sender, ImageClickEventArgs e)
    {
        Ag2.Manager.DAL.IUserDAL _userADO = (Ag2.Manager.DAL.IUserDAL)Util.GetADO("UserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);
        CurrentSessions cs = new CurrentSessions();

        string senhaDB = cs.User.password;

        if (!senhaDB.Equals(txtSenhaAtual.Text))
        {
            Util.ShowMessage("Senha atual incorreta.");
            return;
        }

        cs.User.password = txtNovaSenha.Text;

        _userADO.ChangePassword(cs.User);

        new Ag2.Manager.Helper.WriteScriptOnPage().AddAlert("Senha alterada com sucesso.").Bind();
    }

    protected void LimpaForm()
    {
        txtConfirmacaoSenha.Text = "";
        txtNovaSenha.Text = "";
        txtSenhaAtual.Text = "";
    }
}
