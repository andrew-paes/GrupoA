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

public partial class content_module_disciplina : SmartUserControl
{
    #region [ Properties ]

    private Disciplina _disciplinaBO;
    protected Disciplina DisciplinaBO
    {
        get
        {
            if (_disciplinaBO == null)
            {
                _disciplinaBO = new DisciplinaBLL().Carregar(new Disciplina() { DisciplinaId = Util.GetRequestId() });
            }

            return _disciplinaBO;
        }
        set
        {
            _disciplinaBO = value;
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
			if (this.DisciplinaBO != null && this.DisciplinaBO.DisciplinaId > 0)
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
		if (this.DisciplinaBO != null && this.DisciplinaBO.DisciplinaId > 0)
		{
            this.txtNome.Text = this.DisciplinaBO.Descricao;
            this.txtCodigo.Text = this.DisciplinaBO.CodigoDisciplina;

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
            if (this.DisciplinaBO == null || this.DisciplinaBO.DisciplinaId < 1)
            {
                this.DisciplinaBO = new Disciplina();
            }

            this.DisciplinaBO.Descricao = this.txtNome.Text;
            this.DisciplinaBO.CodigoDisciplina = this.txtCodigo.Text;

            if (this.DisciplinaBO != null && this.DisciplinaBO.DisciplinaId > 0)
			{
				new DisciplinaBLL().Atualizar(this.DisciplinaBO);
				Util.ShowUpdateMessage();
			}
			else
			{
                new DisciplinaBLL().Inserir(this.DisciplinaBO);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = this.DisciplinaBO.DisciplinaId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
			}
		}
	}

	#endregion
}