using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.DataAccess.ADO;
using GrupoA.DataAccess;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;
using System.IO;
using System.Configuration;
using System.Data;
using GrupoA.BusinessObject.Enumerator;

public partial class content_module_evento_evento : System.Web.UI.UserControl
{
	#region [ Properyies ]

	/// <summary>
	/// 
	/// </summary>
	private EnqueteBLL _enqueteBLL;

	/// <summary>
	/// 
	/// </summary>
	EnqueteBLL enqueteBLL
	{
		get
		{
			if (_enqueteBLL == null)
				_enqueteBLL = new EnqueteBLL();
			return _enqueteBLL;
		}
	}

	#endregion

	#region [ Page Events ]

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Util.GetRequestId() > 0)
		{
			hddEnqueteId.Value = Util.GetRequestId().ToString();
			if (!IsPostBack)
			{
				MontaChecklistPagina();
				LoadForm();
			}
		}
		else
		{
			if (!IsPostBack)
			{
				MontaChecklistPagina();
			}
		}

		msg.Text = "";
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnExecute_Click(object sender, ImageClickEventArgs e)
	{
		this.SaveOrUpdate();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnInserirOpcaoEvento_Click(object sender, EventArgs e)
	{
		this.InserirOpcaoEnquete();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="e"></param>
	protected void dgGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
	{
		if (e.CommandName.ToUpper() == "DELETE")
		{
			EnqueteOpcao enqueteOpcao = new EnqueteOpcao();
			enqueteOpcao.EnqueteOpcaoId = Convert.ToInt32(e.CommandArgument);
			enqueteBLL.ExcluirOpcaoPorEnquete(enqueteOpcao);
			this.CarregarOpcoesEnquete();
			if (dgGrid.Items.Count < 2)
			{
				Util.ShowMessage("Dados atualizados com sucesso<br><br><strong>Atenção:A Enquete deve possuir pelo menos 2 opções.</strong>", Ag2.Manager.Enumerator.typeMessage.Sucesso);
				chkAtivo.Checked = false;
			}
			else
				Util.ShowUpdateMessage();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="Sender"></param>
	/// <param name="e"></param>
	protected void dgGrid_ItemCreated(Object Sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			ImageButton _myButton = (ImageButton)e.Item.FindControl("btnDelete");
			_myButton.Attributes.Add("onclick", "return confirm('Deseja realmente excluir o item selecionado?');");
		}
	}

	#region Validações

	/// <summary>
	/// Validação do intervalo de Datas do Evento
	/// </summary>
	/// <param name="source">Padrão</param>
	/// <param name="args">Padrão</param>
	protected void cvValidarAtivacao_ServerValidate(object source, ServerValidateEventArgs args)
	{
		this.ValidarAtivacao(args);
	}

	#endregion

	#endregion

	#region Métodos

	/// <summary>
	/// Insere as opções referentes à Enquete
	/// </summary>
	private void InserirOpcaoEnquete()
	{
		if (Page.IsValid && dgGrid.Items.Count < 4)
		{
			EnqueteOpcao enqueteOpcao = new EnqueteOpcao();
			Enquete enquete = new Enquete();
			enquete.EnqueteId = Convert.ToInt32(hddEnqueteId.Value);
			enqueteOpcao.Enquete = enquete;
			enqueteOpcao.Descricao = txtDescricaoOpcao.Text;
			enqueteBLL.InserirOpcao(enqueteOpcao);
			Util.ShowInsertMessage();
			txtDescricaoOpcao.Text = "";
			CarregarOpcoesEnquete();
		}
		else
		{
			Util.ShowMessage("Pode ser inserido no máximo 4 opções.", Ag2.Manager.Enumerator.typeMessage.Erro);
		}
	}

	/// <summary>
	/// Carrega a listagem de opções da Enquete
	/// </summary>
	private void CarregarOpcoesEnquete()
	{
		List<EnqueteOpcao> opcoesEnquete = new EnqueteBLL()
			.CarregarOpcoesEnquetePorEnquete(
				new Enquete()
				{
					EnqueteId = Convert.ToInt32(hddEnqueteId.Value)
				});

		if (opcoesEnquete.Count > 0)
		{
			dgGrid.DataSource = opcoesEnquete;
			dgGrid.DataKeyField = "enqueteOpcaoId";
			dgGrid.DataBind();
			areaOpcoes.Visible = true;
		}
	}

	/// <summary>
	/// Popula a lista de checklists conforme as páginas
	/// </summary>
	protected void MontaChecklistPagina()
	{
		cblPaginas.DataSource = enqueteBLL.CarregarPaginas();
		cblPaginas.DataValueField = "enquetePaginaId";
		cblPaginas.DataTextField = "nomePagina";
		cblPaginas.DataBind();
	}

	/// <summary>
	/// Salva ou atualiza as informações após validar a página atual.
	/// A informação entre salvar ou atualizar será feita com base no 
	/// campo oculto "hddEnqueteId". Além disso, são populados os 
	/// checklists conforme as páginas marcadas.
	/// </summary>
	public void SaveOrUpdate()
	{
		if (Page.IsValid)
		{
			Enquete enquete = new Enquete();
			// Carrega campos conforme preenchimento
			enquete.EnqueteId = Convert.ToInt32(hddEnqueteId.Value);
			enquete.NomeEnquete = txtNome.Text;
			enquete.Pergunta = txtPergunta.Text;
			enquete.Ativo = chkAtivo.Checked;

			// Popula Páginas referentes a Enquete
			List<EnquetePagina> enquetesPaginas = new List<EnquetePagina>();

			foreach (ListItem li in cblPaginas.Items)
			{
				if (li.Selected)
				{
					EnquetePagina ep = new EnquetePagina();
					ep.EnquetePaginaId = Convert.ToInt32(li.Value);
					enquetesPaginas.Add(ep);
				}
			}

			enquete.EnquetePaginas = enquetesPaginas;

			// Executa o Salvar ou AtualizarAutor
			if (hddEnqueteId.Value == "0")
			{
				enqueteBLL.InserirEnquete(enquete, enquetesPaginas);
				Util.ShowInsertMessage();
				hddEnqueteId.Value = enquete.EnqueteId.ToString();
				areaOpcoes.Visible = true;
			}
			else
			{
				enqueteBLL.AtualizarEnquete(enquete);
				Util.ShowUpdateMessage();
			}
		}
	}

	/// <summary>
	/// Carrega o formulário conforme o código contido no campo
	/// oculto "hddEnqueteId"
	/// </summary>
	public void LoadForm()
	{
		// Busca dados
		Enquete enquete = new Enquete();
		enquete.EnqueteId = int.Parse(hddEnqueteId.Value);
		enquete = enqueteBLL.CarregarEnquete(enquete);
		// Carregamento dos Campos
		txtNome.Text = enquete.NomeEnquete;
		txtPergunta.Text = enquete.Pergunta;
		chkAtivo.Checked = enquete.Ativo;

		// marca as páginas que tem relacionamento
		foreach (EnquetePagina pagina in enquete.EnquetePaginas)
		{
			for (int i = 0; i < cblPaginas.Items.Count; i++)
			{
				if (cblPaginas.Items[i].Value.Equals(pagina.EnquetePaginaId.ToString()))
					cblPaginas.Items[i].Selected = true;
			}
		}

		// Se o número de páginas for maior ou igual a 1 deve permitir ativar
		areaOpcoes.Visible = true;
		this.CarregarOpcoesEnquete();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="args"></param>
	private void ValidarAtivacao(ServerValidateEventArgs args)
	{
		try
		{
			if (chkAtivo.Checked)
			{
				// Condições:
				// 1. Ter ao menos uma página
				// 2. Ter ao menos 2 opções
				int totalOpcoes = dgGrid.Items.Count;
				int totalPaginas = 0;

				foreach (ListItem item in cblPaginas.Items)
					if (item.Selected)
						totalPaginas++;

				if ((totalOpcoes > 1) && (totalPaginas > 0))
					args.IsValid = true;

				if (totalOpcoes < 2)
				{
					cvValidarAtivacao.ErrorMessage = "É necessário inserir pelo menos 2 opções para ativar a Enquete";
					args.IsValid = false;
					return;
				}
				else
					cvValidarAtivacao.ErrorMessage = string.Empty;

				if (totalPaginas < 1)
				{
					cvPaginas.ErrorMessage = "É necessário selecionar pelo menos 1 página para ativar a Enquete";
					args.IsValid = false;
					return;
				}
				else
					cvPaginas.ErrorMessage = string.Empty;

				args.IsValid = true;
			}
			else
			{
				args.IsValid = true;
			}
		}
		catch
		{
			cvValidarAtivacao.ErrorMessage = "Erro ao ativar enquete!";
			args.IsValid = false;
		}
	}

	#endregion
}