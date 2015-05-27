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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.Helper;
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;


public partial class content_module_atendimento_atendimento : System.Web.UI.UserControl
{

    private int _id = 0;
    Database db = DatabaseFactory.CreateDatabase();
    IPerfilDAL perfilado = (IPerfilDAL)Util.GetADO("PerfilADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);
    IUserDAL userADO = (IUserDAL)Util.GetADO("UserADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

    Ag2.Manager.Entity.ag2mngMenu menusPermicao = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ContentPlaceHolder cphScript = (ContentPlaceHolder)Page.Master.FindControl("cphScript");
        HtmlGenericControl scriptAttach = new HtmlGenericControl("script");
        scriptAttach.Attributes.Add("type", "text/javascript");
        scriptAttach.Attributes.Add("src", ResolveUrl("~/js/perfil.js"));
        cphScript.Controls.Add(scriptAttach);

        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                LoadForm();
            }
        }

        if (!IsPostBack)
        {
            loadMenu();
        }
    }

    protected void LoadForm()
    {
        Ag2.Manager.Entity.ag2mngPerfil perfil = new Ag2.Manager.Entity.ag2mngPerfil();
        perfil.perfilId = _id;
        perfil = perfilado.LoadById(perfil);

        txtNome.Text = perfil.name;
        chkPermitirResetPassword.Checked = perfil.resetPassword;
        ViewState["nomeAtual"] = perfil.name;
    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        Ag2.Manager.Entity.ag2mngPerfil perfil = new Ag2.Manager.Entity.ag2mngPerfil();
        perfil.perfilId = _id;
        perfil.name = txtNome.Text;
        perfil.resetPassword = chkPermitirResetPassword.Checked;
        perfilado.Save(perfil, rptMenu);

        if (_id == 0)
        {
            txtNome.Text = "";
            ViewState["nomeAtual"] = null;
            loadMenu();
        }
    }

    protected void loadMenu()
    {
        rptMenu.DataSource = perfilado.LoadPermission();
        rptMenu.DataBind();
    }

    protected void getMarcado_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        string menuId = "";
        e.Row.Cells[0].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            menuId = e.Row.Cells[0].Text.ToString();
            //((CheckBox)e.Row.FindControl("chkFull")).Text = menuId;

            Perfil perfil = new Perfil();
            perfil.PerfilId = _id;
            using (IDataReader registro = perfilado.LoadPermissionByMenuIdAndPerfilId(menuId, perfil))
            {
                if (registro.Read())
                {
                    Ag2.Manager.Entity.ag2mngMenu menu2 = new Ag2.Manager.Entity.ag2mngMenu();
                    menu2.menuId = Convert.ToInt32(menuId);
                    menusPermicao = userADO.GetMenuPermissionByMenu(menu2);

                    if (Convert.ToBoolean(registro["fullControl"]).Equals(true))
                    {
                        ((CheckBox)e.Row.FindControl("chkFull")).Checked = true;
                    }

                    if (Convert.ToBoolean(registro["canRead"]).Equals(true))
                    {
                        ((CheckBox)e.Row.FindControl("chkRead")).Checked = true;
                    }

                    if (Convert.ToBoolean(registro["canInsert"]).Equals(true))
                    {
                        ((CheckBox)e.Row.FindControl("chkInsert")).Checked = true;
                    }

                    if (Convert.ToBoolean(registro["canUpdate"]).Equals(true))
                    {
                        ((CheckBox)e.Row.FindControl("chkUpdate")).Checked = true;
                    }

                    if (Convert.ToBoolean(registro["canDelete"]).Equals(true))
                    {
                        ((CheckBox)e.Row.FindControl("chkDelete")).Checked = true;
                    }
                }
            }

        }
    }

    protected void getInfoPonto_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int idMenuPai = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "menuId").ToString());
        
        ((GridView)e.Item.FindControl("gvSubMenu")).DataSource = perfilado.MenuLoadByParentId(idMenuPai);
        ((GridView)e.Item.FindControl("gvSubMenu")).DataBind();
    }

}
