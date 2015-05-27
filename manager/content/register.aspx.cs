using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public partial class content_register : System.Web.UI.Page
{
    private string _dataSource = string.Empty;
    private string _primaryKey = string.Empty;
    private string _descriptionField = string.Empty;
    private Ag2.Security.SecureQueryString sqs = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        sqs = new Ag2.Security.SecureQueryString(Request.QueryString["v"]);

        _dataSource = sqs["DataSource"].ToString();
        _primaryKey = sqs["PrimaryKey"].ToString();
        _descriptionField = sqs["DescriptionField"].ToString();

        lblDescricao.Text = sqs["Title"].ToString();       

        if (!IsPostBack)
            Bind();
    }

    private void Bind()
    {
        Database db = DatabaseFactory.CreateDatabase();
        string select = "SELECT * FROM {0} ORDER BY {1}";
        DbCommand dataProc = db.GetSqlStringCommand(string.Format(select, _dataSource, _descriptionField));

        DataTable dt = db.ExecuteDataSet(dataProc).Tables[0];

        gvItem.RowDataBound += new GridViewRowEventHandler(gvItem_RowDataBound);
        gvItem.DataSource = dt;
        gvItem.DataBind();
    }

    void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //ESCONDE CELULA QUE CONTEM O ID DO REGISTRO
        e.Row.Cells[0].Visible = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            e.Row.Cells[0].Text = drv[_primaryKey].ToString();
            e.Row.Cells[1].Text = drv[_descriptionField].ToString();
        }
    }

    protected void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        Database db = DatabaseFactory.CreateDatabase();
        string insertUpdate = string.Empty;

        if (EditRegisterId == 0)
        {
            //MONTA QUERY INSERT
            insertUpdate = string.Format("INSERT INTO {0} ({1}) VALUES (@{1})", _dataSource, _descriptionField);
        }
        else
        {
            //MONTA QUERY UPDATE
            insertUpdate = string.Format("UPDATE {0} SET {1} = @{1} WHERE {2} = @{2}", _dataSource, _descriptionField, _primaryKey);
        }

        DbCommand dataProc = db.GetSqlStringCommand(insertUpdate);

        if (EditRegisterId > 0)
            db.AddInParameter(dataProc, string.Concat("@", _primaryKey), DbType.Int32, EditRegisterId);

        db.AddInParameter(dataProc, string.Concat("@", _descriptionField), DbType.String, txtDescricao.Text);
        db.ExecuteNonQuery(dataProc);

        EditRegisterId = 0;
        txtDescricao.Text = string.Empty;

        Bind();
    }

    public int EditRegisterId
    {
        get
        {
            if (ViewState["EditRegisterId"] == null)
                ViewState["EditRegisterId"] = 0;
            return (int)ViewState["EditRegisterId"];
        }
        set
        {
            ViewState["EditRegisterId"] = value;
        }
    }

    protected void gvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Database db = DatabaseFactory.CreateDatabase();

        DbCommand dataProc = db.GetSqlStringCommand(string.Format("DELETE FROM {0} WHERE {1} = @{1}", _dataSource, _primaryKey));
        db.AddInParameter(dataProc, string.Concat("@", _primaryKey), DbType.Int32, Convert.ToInt32(gvItem.Rows[e.RowIndex].Cells[0].Text));

        db.ExecuteNonQuery(dataProc);

        Bind();
    }

    protected void gvItem_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        EditRegisterId = Convert.ToInt32(gvItem.Rows[e.NewSelectedIndex].Cells[0].Text);
        txtDescricao.Text = gvItem.Rows[e.NewSelectedIndex].Cells[1].Text;
    }
}
