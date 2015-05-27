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

public partial class content_module_revista_secao : SmartUserControl
{
	#region Eventos

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			this.CarregarRevista();

			if (IdRegistro > 0)
			{
				this.hdnRevistaSecaoId.Value = IdRegistro.ToString();
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

	#region Métodos

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	protected void LoadForm()
	{
		if (IdRegistro > 0)
		{
			var revistaSecao = new RevistaGrupoABLL().CarregarRevistaSecao(new GrupoA.BusinessObject.RevistaSecao() { RevistaSecaoId = (int)IdRegistro });
			this.hdnRevistaSecaoId.Value = revistaSecao.RevistaSecaoId.ToString();
			this.txtNomeSecao.Text = revistaSecao.NomeSecao;
			this.ddlRevista.SelectedValue = revistaSecao.Revista.RevistaId.ToString();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveOrUpdate()
	{
		if (Page.IsValid)
		{
			var revistaSecao = new GrupoA.BusinessObject.RevistaSecao();

			revistaSecao.RevistaSecaoId = Convert.ToInt32(hdnRevistaSecaoId.Value);
			revistaSecao.NomeSecao = this.txtNomeSecao.Text;

			revistaSecao.Revista = new Revista() { RevistaId = Convert.ToInt32(this.ddlRevista.SelectedValue) };

			if (revistaSecao.RevistaSecaoId > 0)
			{
				new RevistaGrupoABLL().AtualizarRevistaSecao(revistaSecao);
				Util.ShowUpdateMessage();
			}
			else
			{
				RevistaGrupoABLL revistaBLL = new RevistaGrupoABLL();
				revistaBLL.InserirRevistaSecao(revistaSecao);
				this.hdnRevistaSecaoId.Value = revistaSecao.RevistaSecaoId.ToString();
				Util.ShowInsertMessage();
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void CarregarRevista()
	{
		this.ddlRevista.DataSource = new RevistaGrupoABLL().CarregarTodasRevistas();
		this.ddlRevista.DataTextField = "NomeRevista";
		this.ddlRevista.DataValueField = "RevistaId";
		this.ddlRevista.DataBind();
		this.ddlRevista.Items.Insert(0, ":: Selecione ::");
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="args"></param>
	protected void cvValidarRevista_ServerValidate(object source, ServerValidateEventArgs args)
	{
		this.ValidarRevista(args);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="args"></param>
	private void ValidarRevista(ServerValidateEventArgs args)
	{
		if (ddlRevista.SelectedIndex == 0)
		{
			this.cvValidarRevista.ErrorMessage = "Campo Obrigatório";
			args.IsValid = false;
			return;
		}
		else
		{
			this.cvValidarRevista.ErrorMessage = "";
			args.IsValid = true;
			return;
		}
	}

	#endregion
}