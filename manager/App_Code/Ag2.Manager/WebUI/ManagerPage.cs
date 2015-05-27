using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using Ag2.Manager.Module;

namespace Ag2.Manager.WebUI
{
    public class ManagerPage : Page
    {
        CurrentSessions cs = new CurrentSessions();
        ManagerModule manager = new ManagerModule();

        public ManagerPage()
        {
            //
        }
                
        protected override void OnLoad(EventArgs e)
        {
            int idiomaId = 0;

            HiddenField hdnIdioma = (HiddenField)this.Master.FindControl("hdnIdioma");

            hdnIdioma.Value = cs.CurrentIdioma.IdiomaId.ToString();


            if (!IsPostBack)
            {
                cs.CurrentFilters = null;
            }
            base.OnLoad(e);
        }
    }
}
