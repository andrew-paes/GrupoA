using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using GrupoA.BusinessObject;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using System.IO;
using System.Configuration;
using System.Data;
using GrupoA.GlobalResources;

public partial class content_module_newsletterDestinatario : SmartUserControl
{
    #region [ Properties ]

    private NewsletterDestinatario _newsletterDestinatarioBO;
    protected NewsletterDestinatario NewsletterDestinatarioBO
    {
        get
        {
            if (_newsletterDestinatarioBO == null)
            {
                _newsletterDestinatarioBO = new NewsletterDestinatarioBLL().Carregar(new NewsletterDestinatario() { NewsletterDestinatarioId = Util.GetRequestId() });
            }

            return _newsletterDestinatarioBO;
        }
        set
        {
            _newsletterDestinatarioBO = value;
        }
    }

    #endregion

    #region [ Events ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.NewsletterDestinatarioBO != null && this.NewsletterDestinatarioBO.NewsletterDestinatarioId > 0)
            {
                this.LoadForm();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = false;
        this.SaveOrUpdate();
    }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    protected void LoadForm()
    {
        if (this.NewsletterDestinatarioBO != null && this.NewsletterDestinatarioBO.NewsletterDestinatarioId > 0)
        {
            this.txtNome.Text = this.NewsletterDestinatarioBO.NomeDestinatario;
            this.txtEmail.Text = this.NewsletterDestinatarioBO.EmailDestinatario;

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void SaveOrUpdate()
    {
        if (Page.IsValid)
        {
            if (this.NewsletterDestinatarioBO == null || this.NewsletterDestinatarioBO.NewsletterDestinatarioId < 1)
            {
                this.NewsletterDestinatarioBO = new NewsletterDestinatario();
            }

            this.NewsletterDestinatarioBO.NomeDestinatario = this.txtNome.Text;
            this.NewsletterDestinatarioBO.EmailDestinatario = this.txtEmail.Text;

            if (this.NewsletterDestinatarioBO != null && this.NewsletterDestinatarioBO.NewsletterDestinatarioId > 0)
            {
                new NewsletterDestinatarioBLL().Atualizar(this.NewsletterDestinatarioBO);
                Util.ShowUpdateMessage();
            }
            else
            {
                new NewsletterDestinatarioBLL().Inserir(this.NewsletterDestinatarioBO);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = this.NewsletterDestinatarioBO.NewsletterDestinatarioId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
        }
    }

    #endregion
}