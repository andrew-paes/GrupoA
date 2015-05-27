using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using Ag2.Manager.Entity;
using Ag2.Manager.ADO.MSSql;
using Ag2.Manager.DAL;
using Ag2.Manager.Helper;
using System.Web.Security;


public partial class content_manager : System.Web.UI.MasterPage
{
    Ag2.Manager.Helper.CurrentSessions cs = new CurrentSessions();
    private Ag2.Manager.Entity.Ag2mngUserLog _ag2mngUserLog = null;
    IAg2mngUserLogDAL _ag2mngUserLogADO = (IAg2mngUserLogDAL)Util.GetADO("Ag2mngUserLogADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

    protected void Page_Init(object sender, EventArgs e)
    {
        if (cs.User == null)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ltrUsuarioNome.Text = cs.User.name;

            _ag2mngUserLog = _ag2mngUserLogADO.CarregaUltimoAcesso(cs.User);
            ltrUltimoAcesso.Visible = true;
            if (_ag2mngUserLog != null)
                ltrUltimoAcesso.Text = string.Format("Seu último acesso foi em: {0} | Ip: {1}", _ag2mngUserLog.dataAcesso.ToString("dd/MM/yyyy HH:mm:ss"), _ag2mngUserLog.ipAcesso);
            else
                ltrUltimoAcesso.Visible = false;
        }
    }
        
    protected void btnSair_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
        Response.Redirect("~/default.aspx");
    }
}
