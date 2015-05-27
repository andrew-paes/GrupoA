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

public partial class content_module_contatoAssunto : SmartUserControl
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
			this.CarregarSetor();

			if (IdRegistro > 0)
			{
				this.hdnContatoAssuntoId.Value = IdRegistro.ToString();
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
		SaveOrUpdate();
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
			var contatoAssunto = new ContatoAssuntoBLL().Carregar(new GrupoA.BusinessObject.ContatoAssunto() { ContatoAssuntoId = (int)IdRegistro });
			this.hdnContatoAssuntoId.Value = contatoAssunto.ContatoAssuntoId.ToString();
			this.txtContatoAssunto.Text = contatoAssunto.NomeAssunto;
			this.ddlSetor.SelectedValue = contatoAssunto.ContatoSetor.ContatoSetorId.ToString();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveOrUpdate()
	{

		if (Page.IsValid)
		{

			ContatoAssuntoBLL contatoAssuntoBLL = new ContatoAssuntoBLL();
			var contatoAssunto = new GrupoA.BusinessObject.ContatoAssunto();

			contatoAssunto.ContatoAssuntoId = Convert.ToInt32(this.hdnContatoAssuntoId.Value);
			contatoAssunto.NomeAssunto = this.txtContatoAssunto.Text;

			contatoAssunto.ContatoSetor = new ContatoSetor() { ContatoSetorId = Convert.ToInt32(this.ddlSetor.SelectedValue) };

			if (contatoAssunto.ContatoAssuntoId > 0)
			{
				contatoAssuntoBLL.AtualizarContatoAssunto(contatoAssunto);
				Util.ShowUpdateMessage();
			}
			else
			{
				contatoAssuntoBLL.InserirContatoAssunto(contatoAssunto);
				this.hdnContatoAssuntoId.Value = contatoAssunto.ContatoAssuntoId.ToString();
				Util.ShowInsertMessage();
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void CarregarSetor()
	{
		ddlSetor.DataSource = new ContatoSetorBLL().CarregarTodos();
		ddlSetor.DataTextField = "NomeSetor";
		ddlSetor.DataValueField = "ContatoSetorId";
		ddlSetor.DataBind();
		ddlSetor.Items.Insert(0, ":: Selecione ::");
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="args"></param>
	protected void cvValidarSetor_ServerValidate(object source, ServerValidateEventArgs args)
	{
		this.ValidarSetor(args);

	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="args"></param>
	private void ValidarSetor(ServerValidateEventArgs args)
	{
		if (ddlSetor.SelectedIndex == 0)
		{
			cvValidarSetor.ErrorMessage = "Campo Obrigatório";
			args.IsValid = false;
			return;
		}
		else
		{
			cvValidarSetor.ErrorMessage = "";
			args.IsValid = true;
			return;
		}
	}

	#endregion
}