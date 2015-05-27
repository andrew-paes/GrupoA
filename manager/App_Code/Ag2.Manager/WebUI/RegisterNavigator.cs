using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace Ag2.Manager.WebUI
{
    [Serializable]
    [DefaultProperty("Text"), ToolboxData("<{0}:RegisterNavigator runat=server></{0}:RegisterNavigator>")]
    public class RegisterNavigator : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        private Panel pnlContainer = null;
        private Panel pnlInfo = null;
        private Panel pagprimeiras = null;
        private ImageButton btnFirstPage = null;
        private ImageButton btnPreviousPage = null;

        private Panel pagultimas = null;
        private ImageButton btnNextPage = null;
        private ImageButton btnLastPage = null;
        private Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();
        private Int32 _currentId = 0;
        private string _moduleName = string.Empty;
        private Ag2.Security.SecureQueryString sq = null;


        public RegisterNavigator()
        {
            pnlContainer = new Panel();
            pnlInfo = new Panel();

            pagprimeiras = new Panel();
            btnFirstPage = new ImageButton();
            btnPreviousPage = new ImageButton();

            pagultimas = new Panel();
            btnNextPage = new ImageButton();
            btnLastPage = new ImageButton();

            btnFirstPage.Click += new ImageClickEventHandler(btnFirstPage_Click);
            btnPreviousPage.Click += new ImageClickEventHandler(btnPreviousPage_Click);

            btnNextPage.Click += new ImageClickEventHandler(btnNextPage_Click);
            btnLastPage.Click += new ImageClickEventHandler(btnLastPage_Click);

            if (Ag2.Manager.Helper.Util.GetRequestId() > 0)
            {
                _currentId = Ag2.Manager.Helper.Util.GetRequestId();
                return;

            }
        }

        protected Int32 GetCurrentIndexRegister()
        {
            int currentIndex = cs.RegisterNavigator.FindIndex(delegate(Int32 searchId)
            {
                return _currentId == searchId;
            });

            return currentIndex;
        }

        void btnLastPage_Click(object sender, ImageClickEventArgs e)
        {
            sq = new Security.SecureQueryString();
            sq["md"] = _moduleName;
            sq["id"] = cs.RegisterNavigator[cs.RegisterNavigator.Count - 1].ToString();
            HttpContext.Current.Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sq.ToString()), true);
        }

        void btnNextPage_Click(object sender, ImageClickEventArgs e)
        {
            int currentIndex = GetCurrentIndexRegister();

            if (currentIndex < (cs.RegisterNavigator.Count - 1))
            {
                sq = new Security.SecureQueryString();
                sq["md"] = _moduleName;
                sq["id"] = cs.RegisterNavigator[currentIndex + 1].ToString();

                HttpContext.Current.Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sq.ToString()), true);
            }
        }

        void btnPreviousPage_Click(object sender, ImageClickEventArgs e)
        {
            int currentIndex = GetCurrentIndexRegister();

            if (currentIndex > 0)
            {
                sq = new Security.SecureQueryString();
                sq["md"] = _moduleName;
                sq["id"] = cs.RegisterNavigator[currentIndex - 1].ToString();

                HttpContext.Current.Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sq.ToString()), true);
            }
        }

        void btnFirstPage_Click(object sender, ImageClickEventArgs e)
        {
            sq = new Security.SecureQueryString();
            sq["md"] = _moduleName;
            sq["id"] = cs.RegisterNavigator[0].ToString();

            HttpContext.Current.Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sq.ToString()), true);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            pnlContainer.ID = string.Format("pnlContainer_{0}", this.ID);
            pnlContainer.CssClass = "pnlRegisterNavigator";

            pnlInfo.ID = string.Format("pnlInfo_{0}", this.ID);
            pnlInfo.CssClass = "pnlInfoRegisterNavigator";

            int current = (GetCurrentIndexRegister()) + 1;
            int to = cs.RegisterNavigator.Count;

            pnlInfo.Controls.Add(new LiteralControl(string.Format("{0} de {1}", current.ToString(), to.ToString())));

            //Container primeiros botões
            pagprimeiras.ID = string.Format("pagprimeiras_{0}", this.ID);
            pagprimeiras.CssClass = "pagprimeiras fl";

            //Container últimos botões
            pagultimas.ID = string.Format("pagultimas_{0}", this.ID);
            pagultimas.CssClass = "pagultimas fr";

            //Botões de navegação (primeiros)
            btnFirstPage.ID = string.Format("btnFirstPage_{0}", this.ID);
            btnFirstPage.BorderWidth = Unit.Pixel(0);
            btnFirstPage.ImageAlign = ImageAlign.AbsMiddle;
            btnFirstPage.AlternateText = "Primeiras";
            btnFirstPage.ImageUrl = "~/img/btn_primeira.gif";
            btnFirstPage.CausesValidation = false;

            btnPreviousPage.ID = string.Format("btnPreviousPage_{0}", this.ID);
            btnPreviousPage.BorderWidth = Unit.Pixel(0);
            btnPreviousPage.ImageAlign = ImageAlign.AbsMiddle;
            btnPreviousPage.AlternateText = "Anteriores";
            btnPreviousPage.ImageUrl = "~/img/btn_anteriore.gif";
            btnPreviousPage.CausesValidation = false;
            //Fim botões de navegação

            //Adiciona primeiros botões container primeiros botões
            pagprimeiras.Controls.Add(btnFirstPage);
            pagprimeiras.Controls.Add(btnPreviousPage);

            //Adiciona container primeiros botões no controle
            pnlContainer.Controls.Add(pagprimeiras);

            //Botões de navegação (ultimos)
            btnNextPage.ID = string.Format("btnNextPage_{0}", this.ID);
            btnNextPage.BorderWidth = Unit.Pixel(0);
            btnNextPage.ImageAlign = ImageAlign.AbsMiddle;
            btnNextPage.AlternateText = "Proximas";
            btnNextPage.ImageUrl = "~/img/btn_proxima.gif";
            btnNextPage.CausesValidation = false;

            btnLastPage.ID = string.Format("btnLastPage_{0}", this.ID);
            btnLastPage.BorderWidth = Unit.Pixel(0);
            btnLastPage.ImageAlign = ImageAlign.AbsMiddle;
            btnLastPage.AlternateText = "Ultima";
            btnLastPage.ImageUrl = "~/img/btn_ultima.gif";
            btnLastPage.CausesValidation = false;
            //Fim botões de navegação

            //Adiciona ultimos botões no container ultimos botões
            pagultimas.Controls.Add(btnNextPage);
            pagultimas.Controls.Add(btnLastPage);

            //Adiciona container ultimos botões no controle
            pnlContainer.Controls.Add(pagultimas);

            pnlContainer.Controls.Add(pnlInfo);

            //Adiciona controle filho no controle pai
            this.Controls.Add(pnlContainer);
            this.ChildControlsCreated = true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (_currentId > 0)
            {
                if (cs.RegisterNavigator.Count > 1)
                    pnlContainer.Visible = true;
                else
                    pnlContainer.Visible = false;

                if (GetCurrentIndexRegister() == 0)
                {
                    btnFirstPage.Enabled = false;
                    btnPreviousPage.Enabled = false;
                }
                else if (GetCurrentIndexRegister() == cs.RegisterNavigator.Count - 1)
                {
                    btnNextPage.Enabled = false;
                    btnLastPage.Enabled = false;
                }
            }
            else
            {
                pnlContainer.Visible = false;
            }

            this.RenderChildren(writer);
        }

        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }
    }
}
