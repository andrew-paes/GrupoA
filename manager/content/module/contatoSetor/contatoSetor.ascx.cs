using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using System.Configuration;
using System.IO;
using GrupoA.GlobalResources;
using System.Data;
using Ag2.Manager;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class content_module_contatoSetor : SmartUserControl
{
	#region Eventos

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (IdRegistro > 0)
		{
			if (!IsPostBack)
			{
				this.hdnContatoSetorId.Value = IdRegistro.ToString();
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
			var contatoSetor = new ContatoSetorBLL().CarregarContatoSetor(new GrupoA.BusinessObject.ContatoSetor() { ContatoSetorId = (int)IdRegistro });
			this.hdnContatoSetorId.Value = contatoSetor.ContatoSetorId.ToString();
			this.txtNomeSetor.Text = contatoSetor.NomeSetor;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveOrUpdate()
	{
		if (Page.IsValid)
		{
			ContatoSetor contatoSetor = new ContatoSetor();
			ContatoSetorBLL contatoSetorBll = new ContatoSetorBLL();

			contatoSetor.ContatoSetorId = Convert.ToInt32(this.hdnContatoSetorId.Value);
			contatoSetor.NomeSetor = this.txtNomeSetor.Text;

			if (!contatoSetorBll.VerificaNomeSetorDuplicado(contatoSetor.ContatoSetorId, (this.txtNomeSetor.Text).Trim().ToUpper()))
			{
				Util.ShowMessage("Já existe um setor com este nome cadastrado!", Ag2.Manager.Enumerator.typeMessage.Erro);
			}
			else
			{
				if (contatoSetor.ContatoSetorId > 0)
				{
					new ContatoSetorBLL().AtualizarContatoSetor(contatoSetor);
					Util.ShowUpdateMessage();
				}
				else
				{
					new ContatoSetorBLL().InserirContatoSetor(contatoSetor);
					this.hdnContatoSetorId.Value = contatoSetor.ContatoSetorId.ToString();
					Util.ShowInsertMessage();
				}
			}
		}
	}

	#endregion
}