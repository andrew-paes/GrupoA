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

public partial class content_module_instituicao : SmartUserControl
{
    #region [ Properties ]

    private Instituicao _instituicaoBO;
    protected Instituicao InstituicaoBO
    {
        get
        {
            if (_instituicaoBO == null)
            {
                _instituicaoBO = new InstituicaoBLL().Carregar(new Instituicao() { InstituicaoId = Util.GetRequestId() });
            }

            return _instituicaoBO;
        }
        set
        {
            _instituicaoBO = value;
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
			if (this.InstituicaoBO != null && this.InstituicaoBO.InstituicaoId > 0)
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
		if (this.InstituicaoBO != null && this.InstituicaoBO.InstituicaoId > 0)
		{
            this.txtNomeInstituicao.Text = this.InstituicaoBO.NomeInstituicao;
            this.txtCnpj.Text = this.InstituicaoBO.Cnpj;
            this.txtTelefone.Text = this.InstituicaoBO.TelefoneNumero;
            this.txtEmail.Text = this.InstituicaoBO.EmailInstituicao;
            this.txtUrlSite.Text = this.InstituicaoBO.UrlSiteInstituicao;
            this.txtCodigo.Text = this.InstituicaoBO.CodigoInstituicao;

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
            if (this.InstituicaoBO == null || this.InstituicaoBO.InstituicaoId < 1)
            {
                this.InstituicaoBO = new Instituicao();
            }

            this.InstituicaoBO.NomeInstituicao = this.txtNomeInstituicao.Text;
            this.InstituicaoBO.Cnpj = this.txtCnpj.Text;
            this.InstituicaoBO.TelefoneNumero = this.txtTelefone.Text;
            this.InstituicaoBO.EmailInstituicao = this.txtEmail.Text;
            this.InstituicaoBO.UrlSiteInstituicao = this.txtUrlSite.Text;
            this.InstituicaoBO.CodigoInstituicao = this.txtCodigo.Text;

            if (this.InstituicaoBO != null && this.InstituicaoBO.InstituicaoId > 0)
			{
				new InstituicaoBLL().Atualizar(this.InstituicaoBO);
				Util.ShowUpdateMessage();
			}
			else
			{
                new InstituicaoBLL().Inserir(this.InstituicaoBO);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = this.InstituicaoBO.InstituicaoId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
			}
		}
	}

	#endregion
}