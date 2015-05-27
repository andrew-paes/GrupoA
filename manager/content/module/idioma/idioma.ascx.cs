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

public partial class content_module_idioma_idioma : System.Web.UI.UserControl
{
    IIdiomaDAL idiomaADO = (IIdiomaDAL)Util.GetADO("IdiomaADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

    protected void Page_Load(object sender, EventArgs e)
    {
        rptIdiomas.ItemDataBound += new RepeaterItemEventHandler(rptIdiomas_ItemDataBound);

        List<Ag2.Manager.Entity.Idioma> lista = idiomaADO.GetIdiomas();

        if (!IsPostBack)
        {
            rptIdiomas.DataSource = lista;
            rptIdiomas.DataBind();
        }
    }

    void rptIdiomas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Ag2.Manager.Entity.Idioma idioma = (Ag2.Manager.Entity.Idioma)e.Item.DataItem;
        CheckBox chkIdioma = (CheckBox)e.Item.FindControl("chkIdioma");
        Image imgFlag = (Image)e.Item.FindControl("imgFlag");
        Literal ltrIdioma = (Literal)e.Item.FindControl("ltrIdioma");

        chkIdioma.InputAttributes.Add("value", idioma.idiomaId.ToString());

        if (idioma.active)
            chkIdioma.Checked = true;

        imgFlag.ImageUrl = idioma.flag;

        ltrIdioma.Text = idioma.name;
    }

    protected void chkIdioma_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;

        Ag2.Manager.Entity.Idioma idioma = new Ag2.Manager.Entity.Idioma();
        idioma.idiomaId = Convert.ToInt32(chk.InputAttributes["value"]);
        idioma.active = chk.Checked;

        idiomaADO.IdiomaUpdate(idioma);
    }
}
