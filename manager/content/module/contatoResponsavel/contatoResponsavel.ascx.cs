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

public partial class content_module_contatoResponsavel : SmartUserControl
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
				this.hdnContatoResponsavelId.Value = IdRegistro.ToString();
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
			var contatoResponsavel = new ContatoResponsavelBLL().Carregar(new GrupoA.BusinessObject.ContatoResponsavel() { ContatoResponsavelId = (int)IdRegistro });
			this.hdnContatoResponsavelId.Value = contatoResponsavel.ContatoResponsavelId.ToString();
			this.txtContatoResponsavel.Text = contatoResponsavel.NomeResponsavel;
			this.txtContatoResponsavelEmail.Text = contatoResponsavel.EmailResonsavel;
			this.ddlAssunto.SelectedValue = contatoResponsavel.ContatoAssunto.ContatoAssuntoId.ToString();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveOrUpdate()
	{
		if (Page.IsValid)
		{
			ContatoResponsavelBLL contatoResponsavelBLL = new ContatoResponsavelBLL();
			var contatoResponsavel = new GrupoA.BusinessObject.ContatoResponsavel();

			contatoResponsavel.ContatoResponsavelId = Convert.ToInt32(this.hdnContatoResponsavelId.Value);
			contatoResponsavel.NomeResponsavel = this.txtContatoResponsavel.Text;
			contatoResponsavel.EmailResonsavel = this.txtContatoResponsavelEmail.Text;

			contatoResponsavel.ContatoAssunto = new ContatoAssunto() { ContatoAssuntoId = Convert.ToInt32(this.ddlAssunto.SelectedValue) };

			if (contatoResponsavel.ContatoResponsavelId > 0)
			{
				contatoResponsavelBLL.AtualizarContatoResponsavel(contatoResponsavel);
				Util.ShowUpdateMessage();
			}
			else
			{
				contatoResponsavelBLL.InserirContatoResponsavel(contatoResponsavel);
				this.hdnContatoResponsavelId.Value = contatoResponsavel.ContatoResponsavelId.ToString();
				Util.ShowInsertMessage();
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void CarregarSetor()
	{
		ddlAssunto.DataSource = new ContatoAssuntoBLL().CarregarTodos();
		ddlAssunto.DataTextField = "NomeAssunto";
		ddlAssunto.DataValueField = "ContatoAssuntoId";
		ddlAssunto.DataBind();
		ddlAssunto.Items.Insert(0, ":: Selecione ::");
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="args"></param>
	protected void cvValidarAssunto_ServerValidate(object source, ServerValidateEventArgs args)
	{
		this.ValidarAssunto(args);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="args"></param>
	private void ValidarAssunto(ServerValidateEventArgs args)
	{
		if (ddlAssunto.SelectedIndex == 0)
		{
			cvValidarAssunto.ErrorMessage = "Campo Obrigatório";
			args.IsValid = false;
			return;
		}
		else
		{
			cvValidarAssunto.ErrorMessage = "";
			args.IsValid = true;
			return;
		}
	}

	#endregion
}