using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ag2.Manager.Helper;
using Ag2.Manager.Entity;

public partial class ctl_ag2SubForm : System.Web.UI.UserControl
{
    private System.Text.StringBuilder sbScript = new System.Text.StringBuilder();
    private string _moduleName = string.Empty;
    private string _label = string.Empty;
    private string _selectType = string.Empty;
    private bool _required = false;
    private string _modulePath = string.Empty;
    private QueryCommand _queryCommand = new QueryCommand();
    private List<Int32> _itens = new List<int>();
    private Ag2.Security.SecureQueryString sq = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        _queryCommand = new QueryCommand();

        _modulePath = String.Format("{0}\\App_Data\\module\\{1}\\{1}.xml", HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath), _moduleName);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(_modulePath);

        XmlNode node = xmlDoc.SelectSingleNode(string.Format("/AG2Manager/module/fields/SubForm[@name='{0}']", this.ID));

        if (node != null)
        {
            XmlNode queryNode = node.SelectSingleNode("query");
            _queryCommand.Sql = queryNode.InnerText;

            XmlNodeList fields = node.SelectNodes("fields/field");
            FieldSubform fieldForm = null;

            if (fields != null)
            {
                foreach (XmlNode field in fields)
                {
                    fieldForm = new FieldSubform();
                    fieldForm.DataFieldName = field.Attributes["dataFieldName"] == null ? "" : field.Attributes["dataFieldName"].Value;
                    fieldForm.HeaderTitle = field.Attributes["headerTitle"] == null ? "" : field.Attributes["headerTitle"].Value;
                    _queryCommand.Fields.Add(fieldForm);
                }
            }
        }

        //NÓ TABLE DO XML
        node = xmlDoc.SelectSingleNode("/AG2Manager/module/table");

        if (node != null)
        {
            _queryCommand.PrimaryKey = node.Attributes["primaryKey"].Value;
        }

        Util util = new Util();
        if (util.CurrentIdioma.Length > 0)
            _queryCommand.IdiomaId = Convert.ToInt32(util.CurrentIdioma);

        btnPostBak.Click += new EventHandler(btnPostBak_Click);
        btnApagar.Click += new EventHandler(btnApagar_Click);
        gvSubForm.RowDataBound += new GridViewRowEventHandler(gvSubForm_RowDataBound);

        ctSubForm.Enabled = _required;

        Control ctrl = Page.Master.FindControl("cphScripts");

        sq = new Ag2.Security.SecureQueryString();
        sq["t"] = "modal";
        sq["md"] = _moduleName;
        sq["ts"] = _selectType;
        sq["control"] = pnlConteudoGrid.ClientID;

        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("type", "text/javascript");

        sbScript.AppendLine();
        sbScript.Append(" $('#").Append(btnSelecionar.ClientID).Append("').fancybox({ ").AppendLine();
        sbScript.Append(" 'type': 'iframe', ").AppendLine();
        sbScript.Append(" 'href': 'list.aspx?q=").Append(sq.ToString()).Append("', ").AppendLine();
        sbScript.Append(" 'height': 550, ").AppendLine();
        sbScript.Append(" 'width': 1024, ").AppendLine();
        sbScript.Append(" 'autoScale': false, ").AppendLine();
        sbScript.Append(" 'type': 'iframe', ").AppendLine();
        sbScript.Append(" 'hideOnOverlayClick': false ").AppendLine();
        sbScript.Append(" }); ").AppendLine();

        script.Controls.Add(new LiteralControl(sbScript.ToString()));

        ctrl.Controls.Add(script);

        lblTitulo.Text = _label;

        if (!IsPostBack)
        {
            if (SessionIds.Count > 0)
            {
                BindGrid(SessionIds);
            }
        }

        _itens = RetornaIdsGridCorrente();
    }

    void btnApagar_Click(object sender, EventArgs e)
    {
        List<Int32> removeIndexs = new List<int>();

        foreach (GridViewRow row in gvSubForm.Rows)
        {
            CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkItem");
            if (chk.Checked)
            {
                SessionIds.Remove(Convert.ToInt32(row.Cells[1].Text));
            }
        }

        Session[this.ClientID] = SessionIds;
        BindGrid(SessionIds);
    }

    void gvSubForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[1].Attributes.Add("style", "text-align: center;");

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.HorizontalAlign = HorizontalAlign.Left;

            for (int i = 0; i < _queryCommand.Fields.Count; i++)
            {
                e.Row.Cells[i + 1].Text = _queryCommand.Fields[i].HeaderTitle;
            }
        }
    }

    public void BindGrid(List<Int32> ids)
    {
        Ag2.Manager.DAL.ISubformADO subformADO = (Ag2.Manager.DAL.ISubformADO)Util.GetADO("SubformADO", (System.Reflection.Assembly)System.Web.Compilation.BuildManager.CodeAssemblies[0]);

        _queryCommand.WhereIds = ids;

        System.Data.DataSet ds = subformADO.CarregaDados(_queryCommand);

        gvSubForm.DataSource = ds;
        gvSubForm.DataBind();
    }

    void btnPostBak_Click(object sender, EventArgs e)
    {
        List<Int32> idsList = RetornaIds();
        List<Int32> idsGrid = RetornaIdsGridCorrente();

        //VERIFICA SE O ITEM JA EXISTE NO GRID
        idsGrid.ForEach(delegate(Int32 id)
        {
            if (!idsList.Contains(id))
                idsList.Add(id);
        });

        SessionIds = idsList;

        BindGrid(idsList);
    }

    protected List<Int32> RetornaIds()
    {
        string[] ids = hdnIds.Value.Split('|');
        List<Int32> idsList = new List<int>();

        foreach (string id in ids)
        {
            idsList.Add(Convert.ToInt32(id));
        }

        return idsList;
    }

    protected List<Int32> RetornaIdsGridCorrente()
    {
        List<Int32> idsList = new List<int>();

        foreach (GridViewRow row in gvSubForm.Rows)
        {
            idsList.Add(Convert.ToInt32(row.Cells[1].Text));
        }

        return idsList;
    }

    public List<Int32> Itens
    {
        get { return this._itens; }
        set { this._itens = value; }
    }

    public string moduleName
    {
        get
        {
            return this._moduleName;
        }
        set
        {
            this._moduleName = value;
        }
    }

    public string label
    {
        get
        {
            return this._label;
        }
        set
        {
            this._label = value;
        }
    }

    public string selectType
    {
        get
        {
            return this._selectType;
        }
        set
        {
            this._selectType = value;
        }
    }

    public bool Required
    {
        get
        {
            return this._required;
        }
        set
        {
            this._required = value;
        }
    }

    public QueryCommand QueryCommand
    {
        get
        {
            _modulePath = String.Format("{0}\\App_Data\\module\\{1}\\{1}.xml", HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath), _moduleName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_modulePath);

            XmlNode node = xmlDoc.SelectSingleNode(string.Format("/AG2Manager/module/fields/SubForm[@name='{0}']", this.ID));

            if (node != null)
            {
                XmlNode queryNode = node.SelectSingleNode("query");
                _queryCommand.Sql = queryNode.InnerText;

                XmlNodeList fields = node.SelectNodes("fields/field");
                FieldSubform fieldForm = null;

                if (fields != null)
                {
                    foreach (XmlNode field in fields)
                    {
                        fieldForm = new FieldSubform();
                        fieldForm.DataFieldName = field.Attributes["dataFieldName"] == null ? "" : field.Attributes["dataFieldName"].Value;
                        fieldForm.HeaderTitle = field.Attributes["headerTitle"] == null ? "" : field.Attributes["headerTitle"].Value;
                        _queryCommand.Fields.Add(fieldForm);
                    }
                }
            }

            //NÓ TABLE DO XML
            node = xmlDoc.SelectSingleNode("/AG2Manager/module/table");

            if (node != null)
            {
                _queryCommand.PrimaryKey = node.Attributes["primaryKey"].Value;
            }

            Util util = new Util();
            if (util.CurrentIdioma.Length > 0)
                _queryCommand.IdiomaId = Convert.ToInt32(util.CurrentIdioma);


            return this._queryCommand;
        }
        set
        {
            this._queryCommand = value;
        }
    }

    public void SetSessionIds(List<int> ids)
    {
        SessionIds = ids;
    }

    protected List<Int32> SessionIds
    {
        get
        {
            List<Int32> listTemp = null;
            if (Session[string.Format("subform_{0}", this.ClientID)] == null)
                listTemp = new List<int>();
            else
                listTemp = (List<Int32>)Session[string.Format("subform_{0}", this.ClientID)];

            return listTemp;

        }
        set
        {
            Session[string.Format("subform_{0}", this.ClientID)] = value;
        }
    }

    public void ClearSelection()
    {
        SessionIds = new List<int>();
        gvSubForm.DataSource = null;
        gvSubForm.DataBind();

    }

}
