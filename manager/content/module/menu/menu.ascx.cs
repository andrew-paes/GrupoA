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
using Ag2.Manager.DAL;
using Ag2.Manager.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public partial class content_module_menu_menu : System.Web.UI.UserControl
{
    private int _id = 0;
    Ag2.Manager.DAL.IMenuDAL menuADO = (Ag2.Manager.DAL.IMenuDAL)Util.GetADO("MenuADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

    protected void Page_Load(object sender, EventArgs e)
    {
        ContentPlaceHolder cphScript = (ContentPlaceHolder)Page.Master.FindControl("cphScript");
        HtmlGenericControl scriptAttach = new HtmlGenericControl("script");
        scriptAttach.Attributes.Add("type", "text/javascript");
        scriptAttach.Attributes.Add("src", ResolveUrl("~/js/menu.js"));
        cphScript.Controls.Add(scriptAttach);

        if (Util.GetRequestId() > 0)
        {
            _id = Util.GetRequestId();

            if (!IsPostBack)
            {
                montaComboMenuPai();
                LoadForm();
            }
        }
        else
        {
            if (!IsPostBack)
            {
                montaComboMenuPai();
            }
        }

        if (!IsPostBack)
        {
            loadMenu();
        }

        msg.Text = "";
        
    }

    protected void LoadForm()
    {
        IDataReader registro = menuADO.LoadMenuByMenuId(_id);

        if (registro.Read())
        {
            txtNome.Text = registro["name"].ToString();
            txtNomeModulo.Text = registro["moduleName"].ToString();
            txtOrdem.Text = registro["menuOrder"].ToString();
            txtTooltip.Text = registro["tooltip"].ToString();
            cbAtivo.Checked = Convert.ToBoolean(Convert.ToInt32(registro["active"]));
            ddlModulo.SelectedValue = registro["parentMenuId"].ToString();

            ViewState["nomeAtual"] = registro["name"].ToString();
        }
        registro.Close();
    }

    protected void montaComboMenuPai()
    {
        ddlModulo.DataSource = menuADO.GetAllParentMenus();
        ddlModulo.DataTextField = "name";
        ddlModulo.DataValueField = "menuId";
        ddlModulo.DataBind();
        ddlModulo.Items.Insert(0, new ListItem("::Raiz do menu::", "0"));
    }

    protected void loadMenu()
    {
        gvMenu.DataSource = menuADO.GetPermissionPerfilByMenuId(_id);
        gvMenu.DataBind();
    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        GridView grid = new GridView();

        int perfilId = 0;
        int parentMenuId = 0;
        bool fullControl = false;
        bool canInsert = false;
        bool canDelete = false;
        bool canRead = false;
        bool canUpdate = false;
        bool liberaMenuPai = false;
        bool insereNovo = false;
        bool existeRegitro = false;


        parentMenuId = Convert.ToInt32(ddlModulo.SelectedValue.ToString());
        Database db = DatabaseFactory.CreateDatabase();

        if (_id == 0)
        {
            insereNovo = true;

            IDataReader existe = menuADO.GetMenuByName(txtNome.Text);

            if (existe.Read())
            {
                existeRegitro = true;
            }

            existe.Close();
            existe.Dispose();

            if (existeRegitro == false)
            {
                _id = menuADO.InsertMenu(txtNome.Text, cbAtivo.Checked, txtTooltip.Text, Convert.ToInt32(txtOrdem.Text.ToString()), txtNomeModulo.Text, parentMenuId);
                if (_id > 0)
                Util.ShowMessage("Registro inserido com sucesso!",Ag2.Manager.Enumerator.typeMessage.Sucesso);
            }
        }
        else
        {
            IDataReader existe = menuADO.GetMenuByNameDiscartCurrentMenuId(txtNome.Text, _id);
            if (existe.Read())
            {
                existeRegitro = true;
            }

            existe.Close();
            existe.Dispose();

            if (existeRegitro == false)
            {
                menuADO.UpdateMenu(_id, txtNome.Text, cbAtivo.Checked, txtTooltip.Text, Convert.ToInt32(txtOrdem.Text.ToString()), txtNomeModulo.Text, parentMenuId);

                Util.ShowUpdateMessage();
            }
        }

        if (existeRegitro == false)
        {
            menuADO.DeleteMenuPermissionByMenuId(_id);

            foreach (GridViewRow row in gvMenu.Rows)
            {
                fullControl = false;
                canInsert = false;
                canDelete = false;
                canRead = false;
                canUpdate = false;

                perfilId = Convert.ToInt32(row.Cells[0].Text.ToString());

                fullControl = ((CheckBox)row.FindControl("chkFull")).Checked;
                canRead = ((CheckBox)row.FindControl("chkRead")).Checked;
                canInsert = ((CheckBox)row.FindControl("chkInsert")).Checked;
                canUpdate = ((CheckBox)row.FindControl("chkUpdate")).Checked;
                canDelete = ((CheckBox)row.FindControl("chkDelete")).Checked;
                liberaMenuPai = (fullControl || canRead || canInsert || canUpdate || canDelete) && parentMenuId > 0;

                menuADO.InsertMenuPermission(perfilId, _id, fullControl, canInsert, canDelete, canRead, canUpdate, liberaMenuPai);
            }
        }

        if (existeRegitro)
        {
            Util.ShowMessage("Já existe um menu com este nome.");
            return;
        }
    }

    protected void getMarcado_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!String.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["fullControl"].ToString()))
            {
                ((CheckBox)e.Row.FindControl("chkFull")).Checked = Convert.ToBoolean(Convert.ToInt32(((DataRowView)e.Row.DataItem)["fullControl"]));
            }
            if (!String.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["canRead"].ToString()))
            {
                ((CheckBox)e.Row.FindControl("chkRead")).Checked = Convert.ToBoolean(Convert.ToInt32(((DataRowView)e.Row.DataItem)["canRead"]));
            }
            if (!String.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["canInsert"].ToString()))
            {
                ((CheckBox)e.Row.FindControl("chkInsert")).Checked = Convert.ToBoolean(Convert.ToInt32(((DataRowView)e.Row.DataItem)["canInsert"]));
            }
            if (!String.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["canUpdate"].ToString()))
            {
                ((CheckBox)e.Row.FindControl("chkUpdate")).Checked = Convert.ToBoolean(Convert.ToInt32(((DataRowView)e.Row.DataItem)["canUpdate"]));
            }
            if (!String.IsNullOrEmpty(((DataRowView)e.Row.DataItem)["canDelete"].ToString()))
            {
                ((CheckBox)e.Row.FindControl("chkDelete")).Checked = Convert.ToBoolean(Convert.ToInt32(((DataRowView)e.Row.DataItem)["canDelete"]));
            }
        }
    }
}
