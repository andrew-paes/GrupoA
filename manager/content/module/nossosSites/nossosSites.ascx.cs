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

public partial class content_module_nossosSites_nossosSites : SmartUserControl
{
    #region [ Properties ]

    protected Link LinkBO = new Link();
    protected int LinkId
    {
        get
        {
            return (int)Util.GetRequestId();
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
        if (LinkId > 0)
        {
            this.CarregarObjetos();

            if (!IsPostBack)
            {
                this.CarregarForm();
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
        this.Salvar();
    }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    private void CarregarObjetos()
    {
        this.LinkBO = new LinkBLL().Carregar(new Link { LinkId = this.LinkId });
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregarForm()
    {
        if (this.LinkBO != null && this.LinkBO.LinkId > 0)
        {
            this.txtNomeLink.Text = this.LinkBO.NomeLink;
            this.txtUrlLink.Text = this.LinkBO.UrlLink;
            this.chkAtivo.Checked = this.LinkBO.Ativo;
            this.chkTargetBlank.Checked = this.LinkBO.TargetBlank;

            if (Util.GetQueryString("origem") == "insert")
            {
                Util.ShowInsertMessage();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Salvar()
    {
        if (Page.IsValid)
        {
            try
            {
                if (this.LinkBO != null && this.LinkBO.LinkId > 0)
                {
                    this.LinkBO.NomeLink = this.txtNomeLink.Text;
                    this.LinkBO.UrlLink = this.txtUrlLink.Text;
                    this.LinkBO.Ativo = this.chkAtivo.Checked;
                    this.LinkBO.TargetBlank = this.chkTargetBlank.Checked;

                    new LinkBLL().Atualizar(this.LinkBO);

                    Util.ShowUpdateMessage();

                    this.CarregarObjetos();
                    this.CarregarForm();
                }
                else
                {
                    this.LinkBO = new Link();
                    this.LinkBO.NomeLink = this.txtNomeLink.Text;
                    this.LinkBO.UrlLink = this.txtUrlLink.Text;
                    this.LinkBO.Ativo = this.chkAtivo.Checked;
                    this.LinkBO.TargetBlank = this.chkTargetBlank.Checked;

                    new LinkBLL().Inserir(this.LinkBO);

                    Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                    sqGrid["md"] = Util.GetQueryString("md");
                    sqGrid["id"] = this.LinkBO.LinkId.ToString();
                    sqGrid["origem"] = "insert";

                    Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));

                    //Util.ShowInsertMessage();
                }
            }
            catch
            {
                Util.ShowMessage("Erro ao atualizar!");
            }
        }
    }

    #endregion
}