using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class content_Download : System.Web.UI.Page
{
    private string filename = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["f"]))
        {
            filename = Request.QueryString["f"].ToString();
            string ext = System.IO.Path.GetExtension(filename);

            if (!ext.Equals(".exe", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".asp", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".aspx", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".config", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".sql", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".asax", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".php", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".rescx", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".js", StringComparison.OrdinalIgnoreCase)
                || !ext.Equals(".css", StringComparison.OrdinalIgnoreCase))
            {
                Ag2.Manager.Helper.Util.DownloadFile(Server.MapPath(filename), System.IO.Path.GetFileName(filename), true);
            }
        }
    }
}
