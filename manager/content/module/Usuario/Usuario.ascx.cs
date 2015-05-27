using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Ag2.Manager.Helper;
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public partial class content_module_Usuario_Usuario : System.Web.UI.UserControl
{
    private int _id = 0;
    private IUserDAL _userADO = (IUserDAL)Util.GetADO("UserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);
    private Ag2.Manager.Entity.ag2mngUser _user = null;
    private Ag2.Manager.Helper.CurrentSessions cs = new CurrentSessions();

    protected void Page_Load(object sender, EventArgs e)
    {
        lnkResetSenha.Click += new EventHandler(lnkResetSenha_Click);

        if (Ag2.Manager.Helper.Util.GetRequestId() > 0)
        {
            _id = Ag2.Manager.Helper.Util.GetRequestId();

            if (_id > 0)
                phSenha.Visible = false;
            else
                phSenha.Visible = true;
        }

        if (!IsPostBack)
        {
            PopulaPerfil();
            FormLoad();
        }
    }

    void lnkResetSenha_Click(object sender, EventArgs e)
    {
        _user = new Ag2.Manager.Entity.ag2mngUser();
        _user.userId = _id;
        _user = _userADO.Load(_user);

        _userADO.ResetPassword(_user);

        EnviaEmail(_user);

        lblSenhaEnviada.Visible = true;
    }

    protected void FormLoad()
    {
        if (_id > 0)
        {
            //VERIFICA SE PODE RESETAR A SENHA
            if (cs.User.userId != _id)
            {
                foreach (Ag2.Manager.Entity.ag2mngPerfil p1 in cs.User.perfis)
                {
                    if (p1.resetPassword)
                    {
                        phResetSenha.Visible = true;
                        break;
                    }
                }
            }

            txtLogin.Enabled = false;

            _user = new Ag2.Manager.Entity.ag2mngUser();
            _user.userId = _id;
            _user = _userADO.Load(_user);

            List<Ag2.Manager.Entity.ag2mngPerfil> perfils = _userADO.GetPerfilByUser(_user);

            txtUsuario.Text = _user.name;
            txtLogin.Text = _user.login;
            txtEmail.Text = _user.email;

            foreach (ListItem item in chkListPerfis.Items)
            {
                if (perfils.Exists(delegate(Ag2.Manager.Entity.ag2mngPerfil v1) { return v1.perfilId == Convert.ToInt32(item.Value); }))
                {
                    item.Selected = true;
                }
            }

            //cmbPerfil.SelectedValue = perfil.perfilId.ToString();
            chkBloqueado.Checked = !_user.active;
        }
    }

    protected void PopulaPerfil()
    {
        chkListPerfis.DataSource = _userADO.GetAllPerfil();
        chkListPerfis.DataValueField = "perfilId";
        chkListPerfis.DataTextField = "name";
        chkListPerfis.DataBind();
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Perfil perfil = new Perfil();

        if (_id > 0)
        {
            _user = new Ag2.Manager.Entity.ag2mngUser();
            _user.userId = _id;
            _user = _userADO.Load(_user);

            _user.active = !chkBloqueado.Checked;
            _user.email = txtEmail.Text;
            _user.login = txtLogin.Text;
            _user.name = txtUsuario.Text;

            List<Ag2.Manager.Entity.ag2mngPerfil> perfis = GetPerfis();

            _user.perfis = _userADO.GetPerfilsByPerfis(perfis);

            _userADO.Save(_user);

            Util.ShowUpdateMessage();
        }
        else
        {
            _user = new Ag2.Manager.Entity.ag2mngUser();
            _user.active = !chkBloqueado.Checked;
            _user.email = txtEmail.Text;
            _user.login = txtLogin.Text;
            _user.password = txtSenha.Text;
            _user.name = txtUsuario.Text;

            List<Ag2.Manager.Entity.ag2mngPerfil> perfis = GetPerfis();

            _user.perfis = _userADO.GetPerfilsByPerfis(perfis);

            if (_userADO.Exists(_user))
            {
                Util.ShowMessage("Já existe um usuário com este login.", Ag2.Manager.Enumerator.typeMessage.Erro);
                return;
            }

            _userADO.Save(_user);

            LimpaForm();

            Util.ShowInsertMessage();
        }

    }

    private List<Ag2.Manager.Entity.ag2mngPerfil> GetPerfis()
    {
        List<Ag2.Manager.Entity.ag2mngPerfil> perfis = new List<Ag2.Manager.Entity.ag2mngPerfil>();
        Ag2.Manager.Entity.ag2mngPerfil perfil = null;

        foreach (ListItem item in chkListPerfis.Items)
        {
            if (item.Selected)
            {
                perfil = new Ag2.Manager.Entity.ag2mngPerfil();
                perfil.perfilId = Convert.ToInt32(item.Value);
                perfis.Add(perfil);
            }
        }

        return perfis;
    }

    protected void LimpaForm()
    {
        txtEmail.Text = "";
        txtUsuario.Text = "";
        chkListPerfis.SelectedIndex = -1;
        chkBloqueado.Checked = false;
    }

    private void EnviaEmail(Ag2.Manager.Entity.ag2mngUser user)
    {
        Ag2.Net.Mail.MailMessage msg = new Ag2.Net.Mail.MailMessage();
        msg.To.Add(new System.Net.Mail.MailAddress(txtEmail.Text, txtUsuario.Text));
        msg.IsBodyHtml = true;
        msg.Subject = "Manager - Nova Senha";
        msg.PathTemplate = Server.MapPath("~/templateMail/NovaSenha.htm");
        msg.Dictionary.Add("#usuario#", user.name);
        msg.Dictionary.Add("#senha#", user.password);

        new Ag2.Net.Mail.SendMail(msg, false);
    }

}
