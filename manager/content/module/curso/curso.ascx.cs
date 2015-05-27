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

public partial class content_module_curso : SmartUserControl
{
    #region [ Properties ]

    private Curso _cursoBO;
    protected Curso CursoBO
    {
        get
        {
            if (_cursoBO == null)
            {
                _cursoBO = new CursoBLL().Carregar(new Curso() { CursoId = Util.GetRequestId() });
            }

            return _cursoBO;
        }
        set
        {
            _cursoBO = value;
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
			if (this.CursoBO != null && this.CursoBO.CursoId > 0)
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
		if (this.CursoBO != null && this.CursoBO.CursoId > 0)
		{
            this.txtNome.Text = this.CursoBO.Nome;
            this.txtCodigoCurso.Text = this.CursoBO.CodigoCurso;

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
            if (this.CursoBO == null || this.CursoBO.CursoId < 1)
            {
                this.CursoBO = new Curso();
            }

            this.CursoBO.Nome = this.txtNome.Text;
            this.CursoBO.CodigoCurso = this.txtCodigoCurso.Text;

            if (this.CursoBO != null && this.CursoBO.CursoId > 0)
			{
				new CursoBLL().Atualizar(this.CursoBO);
				Util.ShowUpdateMessage();
			}
			else
			{
                new CursoBLL().Inserir(this.CursoBO);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = this.CursoBO.CursoId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
			}
		}
	}

	#endregion
}