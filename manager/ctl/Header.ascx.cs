using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class src_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        phLogoLink.Visible = false;
        phLogo.Visible = true;

        if (IsInternal)
        {
            phLogoLink.Visible = true;
            phLogo.Visible = false;
        }
    }

    public bool IsInternal { get; set; }
}
