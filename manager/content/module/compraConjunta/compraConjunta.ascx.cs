using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Ag2.Manager.Helper;
using System.IO;
using System.Configuration;
using System.Data;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;
using GrupoA.FilterHelper;
using GrupoA.BusinessObject.Enumerator;

public partial class content_module_compraConjunta_compraconjunta : System.Web.UI.UserControl
{
	#region [ Propriedades ]

	/// <summary>
	/// 
	/// </summary>
	public DataTable DtGridView
	{
		get
		{
			return (DataTable)ViewState["dt"];
		}
		set
		{
			ViewState["dt"] = value;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public int RowsCountGridView
	{
		get
		{
			if (ViewState["RowsCount"] == null)
			{
				ViewState["RowsCount"] = 0;
			}
			return (int)ViewState["RowsCount"];
		}
		set
		{
			ViewState["RowsCount"] = value;
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
		Page.MaintainScrollPositionOnPostBack = true;

		if (!IsPostBack)
		{
			this.BindCompraConjuntaStatus();

			if (Util.GetRequestId() > 0)
			{
				int _id = Util.GetRequestId();

				this.LoadForm(_id);
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
		SaveOrUpdate();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnPesquisar_Click(object sender, EventArgs e)
	{
        //PesquisarISBN13(this.txtISBN13.Text, Convert.ToInt32(ddlTipoTitulo.SelectedValue));
        PesquisarISBN13(this.txtISBN13.Text, 1);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="source"></param>
	/// <param name="args"></param>
	protected void cvValidarDatasPublicacao_ServerValidate(object source, ServerValidateEventArgs args)
	{
		PeriodoValido(args);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnIncluir_Click(object sender, ImageClickEventArgs e)
	{
		AddEditRow();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void gdvQuantidadeDesconto_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		DeleteRow(e);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void gdvQuantidadeDesconto_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.SelectedRow();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
	{
		this.CancelarCompraConjunta();
	}

	#endregion

	#region [ Métodos ]

	/// <summary>
	/// 
	/// </summary>
	protected void BindCompraConjuntaStatus()
	{
		CompraConjuntaStatus compraConjuntaStatus = new CompraConjuntaStatus();

		this.ddlCompraConjuntaStatus.DataSource = new CompraConjuntaStatusBLL().CarregarTodos(compraConjuntaStatus);
		this.ddlCompraConjuntaStatus.DataValueField = "compraConjuntaStatusId";
		this.ddlCompraConjuntaStatus.DataTextField = "statusCompra";
		this.ddlCompraConjuntaStatus.DataBind();
		//this.ddlCompraConjuntaStatus.Items.Insert(0, new ListItem("Selecione...", ""));
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	protected void LoadForm(int id)
	{
		this.InitGridView();

		this.pnlDescontos.Visible = true;

		CompraConjunta compraConjunta = new CompraConjunta();
		compraConjunta.CompraConjuntaId = id;
		compraConjunta = new CompraConjuntaBLL().Carregar(compraConjunta);

		if (!String.IsNullOrEmpty(compraConjunta.DataHoraFinalizacao.ToString()))
		{
			this.txtDataHoraFinalizacao.Text = compraConjunta.DataHoraFinalizacao.ToString();

			this.NaoEditaCompleto(); // Se tem hora de finalização não poderá fazer edição de dados
		}

		this.hddCompraConjuntaId.Value = compraConjunta.CompraConjuntaId.ToString();
        this.txtDataInicialCompra.Text = compraConjunta.DataInicialCompra.ToString("dd/MM/yyyy");
        this.txtHoraInicialCompra.Text = compraConjunta.DataInicialCompra.ToString("HH:mm");
        this.txtDataFinalCompra.Text = compraConjunta.DataFinalCompra.ToString("dd/MM/yyyy");
        this.txtHoraFinalCompra.Text = compraConjunta.DataFinalCompra.ToString("HH:mm");
		this.hddDataFinalCompra.Value = compraConjunta.DataFinalCompra.ToString("dd/MM/yyyy");
		this.txtEstoqueSeguranca.Text = compraConjunta.EstoqueSeguranca.ToString();
        this.txtQuantidadeLimite.Text = compraConjunta.QuantidadeLimite.ToString();
		this.hddEstoqueSegurancaAnterior.Value = compraConjunta.EstoqueSeguranca.ToString();
		this.chkAtivo.Checked = compraConjunta.Ativa;
		this.lblAtivo.Visible = true;
		this.chkAtivo.Visible = true;
		this.ddlCompraConjuntaStatus.SelectedValue = compraConjunta.CompraConjuntaStatus.CompraConjuntaStatusId.ToString();
		this.ddlCompraConjuntaStatus.Enabled = false;

		TituloImpresso tituloImpresso = new TituloImpresso();
		tituloImpresso.TituloImpressoId = compraConjunta.Produto.ProdutoId;
		tituloImpresso = new TituloImpressoBLL().Carregar(tituloImpresso); // Busca por titulo impresso

		if (tituloImpresso != null && tituloImpresso.TituloImpressoId > 0) // Se não for encontrado titulo impresso
		{
			PesquisarISBN13(tituloImpresso.Isbn13, 1);
		}
		else
		{
			TituloEletronico tituloEletronico = new TituloEletronico();
			tituloEletronico.TituloEletronicoId = compraConjunta.Produto.ProdutoId;
			tituloEletronico = new TituloEletronicoBLL().Carregar(tituloEletronico); // Busca por titulo eletronico

			if (tituloEletronico != null && tituloEletronico.TituloEletronicoId > 0)
			{
				PesquisarISBN13(tituloEletronico.Isbn13, 2);
			}
		}

		this.LoadGridView(compraConjunta);

		this.VerificaAndamento(compraConjunta);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="compraConjunta"></param>
	private void LoadGridView(CompraConjunta compraConjunta)
	{
		CompraConjuntaDesconto compraConjuntaDesconto = new CompraConjuntaDesconto();
		compraConjuntaDesconto.CompraConjunta = new CompraConjunta();
		compraConjuntaDesconto.CompraConjunta.CompraConjuntaId = compraConjunta.CompraConjuntaId;

		IEnumerable<CompraConjuntaDesconto> iEnumCompraConjuntaDesconto = new CompraConjuntaDescontoBLL().CarregarTodos(compraConjuntaDesconto);

		if (iEnumCompraConjuntaDesconto != null && iEnumCompraConjuntaDesconto.Any())
		{
			foreach (CompraConjuntaDesconto compraConjuntaDescontoTemp in iEnumCompraConjuntaDesconto)
			{
				this.AddRow(compraConjuntaDescontoTemp); // Add a new row to GridView
			}

			this.BindGridView();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void NaoEditaParcial()
	{
        this.txtDataInicialCompra.ReadOnly = true;
        this.txtHoraInicialCompra.ReadOnly = true;
        this.txtHoraInicialCompra.Enabled = false;
		this.pnlFormDesconto.Visible = false;
		this.pnlCancelar.Visible = true;
		this.gdvQuantidadeDesconto.Enabled = false;

		this.btnPesquisar.Enabled = false;
		this.btnPesquisar.Visible = false;

		this.txtISBN13.ReadOnly = true;
	}

	/// <summary>
	/// Se pedido estiver finalizado, define atributos para nao deixar editar dados
	/// </summary>
	private void NaoEditaCompleto()
	{
		this.NaoEditaParcial();

		this.btnExecute.Enabled = false;
		this.btnExecute.Visible = false;

		this.txtISBN13.Enabled = false;
	}

	/// <summary>
	/// Verifica se a compra já está em andamento
	/// </summary>
	/// <param name="compraConjunta"></param>
	private void VerificaAndamento(CompraConjunta compraConjunta)
	{
		if (DateTime.Compare(compraConjunta.DataInicialCompra, DateTime.Now) <= 0)
		{
			this.NaoEditaParcial();
		}

        if (compraConjunta.CompraConjuntaStatus.CompraConjuntaStatusId != 1)
        {
            pnlCancelar.Visible = false;
            btnExecute.Visible = false;
        }
	}

	/// <summary>
	/// Pesquisa por ISBN13 e carrega os dados dos títulos
	/// </summary>
	private void PesquisarISBN13(string strISBN13, int produtoTipoId)
	{
		switch (produtoTipoId)
		{
			case 1: // Titulo Impresso

				TituloImpresso tituloImpresso = new TituloImpresso();
				tituloImpresso.Isbn13 = strISBN13;
				IEnumerable<TituloImpresso> iEnumTituloImpresso = new TituloImpressoBLL().CarregarTodos(tituloImpresso);

				if (iEnumTituloImpresso != null && iEnumTituloImpresso.Any())
				{
					foreach (TituloImpresso tituloImpressoTemp in iEnumTituloImpresso)
					{
						Produto produto = new Produto();
						produto.ProdutoId = tituloImpressoTemp.TituloImpressoId;
						produto = new ProdutoBLL().Carregar(produto);

						//this.ddlTipoTitulo.SelectedValue = "1";
						this.txtISBN13.Text = tituloImpressoTemp.Isbn13;
						this.hddProdutoId.Value = produto.ProdutoId.ToString();
						this.txtValorUnitario.Text = produto.ValorUnitario.ToString();

						if (!String.IsNullOrEmpty(produto.ValorOferta.ToString()))
						{
							this.txtValorOferta.Text = produto.ValorOferta.ToString();
						}
						else
						{
							this.lblValorOferta.Visible = false;
							this.txtValorOferta.Visible = false;
						}

						Titulo titulo = new Titulo();
						titulo.TituloId = tituloImpressoTemp.Titulo.TituloId;
						titulo = new TituloBLL().Carregar(titulo); // Busca por titulo impresso

						if (titulo != null)
						{
							this.txtTitulo.Text = titulo.NomeTitulo;
							this.txtDataPublicacao.Text = titulo.DataPublicacao.ToString();
						}
					}
				}

				break;
			case 2: // Titulo Eletrônico

				TituloEletronico tituloEletronico = new TituloEletronico();
				tituloEletronico.Isbn13 = strISBN13;
				IEnumerable<TituloEletronico> iEnumTituloEletronico = new TituloEletronicoBLL().CarregarTodos(tituloEletronico);

				if (iEnumTituloEletronico != null && iEnumTituloEletronico.Any())
				{
					foreach (TituloEletronico tituloEletronicoTemp in iEnumTituloEletronico)
					{
						Produto produto = new Produto();
						produto.ProdutoId = tituloEletronicoTemp.TituloEletronicoId;
						produto = new ProdutoBLL().Carregar(produto);

						//this.ddlTipoTitulo.SelectedValue = "2";
						this.txtISBN13.Text = tituloEletronicoTemp.Isbn13;
						this.hddProdutoId.Value = produto.ProdutoId.ToString();
						this.txtValorUnitario.Text = produto.ValorUnitario.ToString();

						if (!String.IsNullOrEmpty(produto.ValorOferta.ToString()))
						{
							this.txtValorOferta.Text = produto.ValorOferta.ToString();
						}
						else
						{
							this.lblValorOferta.Visible = false;
							this.txtValorOferta.Visible = false;
						}

                        this.txtTitulo.Text = produto.NomeProduto;
						//this.txtDataPublicacao.Text = tituloEletronicoTemp.DataPublicacao.ToString();
					}
				}

				break;
			default:
				break;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="args"></param>
	private void PeriodoValido(ServerValidateEventArgs args)
	{
		if (string.IsNullOrEmpty(this.txtDataInicialCompra.Text) && string.IsNullOrEmpty(this.txtDataFinalCompra.Text))
		{
			args.IsValid = true;
			return;
		}

		try
		{
			DateTime dtIni = DateTime.Parse(this.txtDataInicialCompra.Text);
			DateTime dtFim = DateTime.Parse(this.txtDataFinalCompra.Text);

            string[] arrayHoraIni = this.txtHoraInicialCompra.Text.Split(':');

            if (arrayHoraIni.Any())
            {
                dtIni = dtIni.AddHours(Convert.ToInt32(arrayHoraIni[0].ToString()));

                if (arrayHoraIni.Count() > 1)
                {
                    dtIni = dtIni.AddMinutes(Convert.ToInt32(arrayHoraIni[1].ToString()));
                }
            }

            string[] arrayHoraEnd = this.txtHoraFinalCompra.Text.Split(':');

            if (arrayHoraEnd.Any())
            {
                dtFim = dtFim.AddHours(Convert.ToInt32(arrayHoraEnd[0].ToString()));

                if (arrayHoraEnd.Count() > 1)
                {
                    dtFim = dtFim.AddMinutes(Convert.ToInt32(arrayHoraEnd[1].ToString()));
                }
                else
                {
                    dtFim = dtFim.AddMinutes(59);
                }

                dtFim = dtFim.AddSeconds(59);
            }
            else
            {
                dtFim = dtFim.AddHours(23).AddMinutes(59).AddSeconds(59);
            }

			if (DateTime.Compare(dtFim, dtIni) >= 0)
			{
				if (DateTime.Compare(dtIni, DateTime.Now) >= 0 || !this.gdvQuantidadeDesconto.Enabled) // Permite a data ser menor que a data atual se o produto já está em andamento
				{

                    if (!String.IsNullOrEmpty(this.hddDataFinalCompra.Value) && (!String.IsNullOrEmpty(this.hddCompraConjuntaId.Value) && Convert.ToInt32(this.hddCompraConjuntaId.Value) > 0))
					{
						DateTime dtHddFim = DateTime.Parse(this.hddDataFinalCompra.Value);

						if (DateTime.Compare(dtFim, dtHddFim) >= 0) // Verifica se a nova data é igual ou maior que a data anterior de finalização
						{
							args.IsValid = true;
						}
						else
						{
							cvValidarDatasPublicacao.ErrorMessage = "A nova data final não pode ser menor a antiga data final '" + this.hddDataFinalCompra.Value + "'.";
							args.IsValid = false;
						}
					}
					else
					{
						args.IsValid = true;
					}
				}
				else
				{
					cvValidarDatasPublicacao.ErrorMessage = "Data inicial deve ser maior que data atual.";
					args.IsValid = false;
				}
			}
			else
			{
				cvValidarDatasPublicacao.ErrorMessage = "Data final deve ser maior ou igual que data inicial.";
				args.IsValid = false;
			}
		}
		catch
		{
			cvValidarDatasPublicacao.ErrorMessage = "Data incorreta!";
			args.IsValid = false;
		}
	}

	#region [ GridView Desconto ]

	/// <summary>
	/// 
	/// </summary>
	private void InitGridView()
	{
		this.DtGridView = new DataTable();
		this.DtGridView.Columns.Add(new DataColumn("QuantidadeMinima", typeof(string)));
		this.DtGridView.Columns.Add(new DataColumn("PercentualDesconto", typeof(string)));
		this.DtGridView.Columns.Add(new DataColumn("idGridView", typeof(int)));

		this.BindGridView();
	}

	/// <summary>
	/// 
	/// </summary>
	private void BindGridView()
	{
		this.txtQuantidadeMinima.Text = string.Empty;
		this.txtPercentualDesconto.Text = string.Empty;

		DataView dvGriView = DtGridView.DefaultView;
		dvGriView.Sort = "QuantidadeMinima ASC";
		DtGridView = dvGriView.ToTable();

		gdvQuantidadeDesconto.DataSource = this.DtGridView;
		gdvQuantidadeDesconto.DataBind();
	}

	/// <summary>
	/// 
	/// </summary>
	private void AddEditRow()
	{
		if (this.DtGridView != null)
		{
			// Verify if have a row selected
			if (!String.IsNullOrEmpty(hddIdGridView.Value) && Convert.ToInt32(hddIdGridView.Value) > 0)
			{
				this.EditRow();
			}
			else
			{
				this.AddRow();
			}

			this.BindGridView();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="e"></param>
	private void DeleteRow(GridViewDeleteEventArgs e)
	{
		this.DtGridView.Rows.RemoveAt(e.RowIndex);
		gdvQuantidadeDesconto.DataSource = this.DtGridView;
		gdvQuantidadeDesconto.DataBind();
	}

	/// <summary>
	/// Add new row to Grid View
	/// </summary>
	private void AddRow()
	{
		if (RowIsValid())
		{
			DataRow drow = this.DtGridView.NewRow();
			drow["QuantidadeMinima"] = this.txtQuantidadeMinima.Text;
			drow["PercentualDesconto"] = this.txtPercentualDesconto.Text;
			drow["idGridView"] = ++this.RowsCountGridView;
			this.DtGridView.Rows.Add(drow);
		}
	}

	/// <summary>
	/// Add new row to Grid View
	/// </summary>
	private void AddRow(CompraConjuntaDesconto compraConjuntaDesconto)
	{
		DataRow drow = this.DtGridView.NewRow();
		drow["QuantidadeMinima"] = compraConjuntaDesconto.QuantidadeMinima;
		drow["PercentualDesconto"] = compraConjuntaDesconto.PercentualDesconto;
		drow["idGridView"] = ++this.RowsCountGridView;
		this.DtGridView.Rows.Add(drow);
	}

	/// <summary>
	/// 
	/// </summary>
	private void SelectedRow()
	{
		hddIdGridView.Value = gdvQuantidadeDesconto.SelectedValue.ToString();
		this.txtQuantidadeMinima.Text = gdvQuantidadeDesconto.SelectedDataKey["QuantidadeMinima"].ToString();
		this.txtPercentualDesconto.Text = gdvQuantidadeDesconto.SelectedDataKey["PercentualDesconto"].ToString();

		this.btnIncluir.ImageUrl = "~/img/btn_atualizar.gif";
		this.btnIncluir.AlternateText = "AtualizarAutor";
	}

	/// <summary>
	/// 
	/// </summary>
	private void EditRow()
	{
		if (RowIsValid())
		{
			for (int i = 0; i < this.DtGridView.Rows.Count; i++)
			{
				if (Convert.ToString(this.DtGridView.Rows[i]["idGridView"]).Equals(this.hddIdGridView.Value))
				{
					DataRow drow = this.DtGridView.NewRow();
					this.DtGridView.Rows[i]["QuantidadeMinima"] = this.txtQuantidadeMinima.Text;
					this.DtGridView.Rows[i]["PercentualDesconto"] = this.txtPercentualDesconto.Text;

					hddIdGridView.Value = "0";
					gdvQuantidadeDesconto.SelectedIndex = -1;

					this.btnIncluir.ImageUrl = "~/img/btn_adicionar.png";
					this.btnIncluir.AlternateText = "Adicionar";
				}
			}
		}
	}

	/// <summary>
	/// Valida dados da linha ( quantidade mínima maior que 0, percentual entre 0 e 100, duplicação de dados)
	/// </summary>
	/// <returns></returns>
	private bool RowIsValid()
	{
		bool flag = false;

		if (
			(!String.IsNullOrEmpty(this.txtQuantidadeMinima.Text) && !String.IsNullOrEmpty(this.txtEstoqueSeguranca.Text))
			&& Convert.ToInt32(this.txtQuantidadeMinima.Text) > 0
            && Convert.ToInt32(this.txtQuantidadeMinima.Text) <= Convert.ToInt32(this.txtEstoqueSeguranca.Text.Replace(",", "").Replace(".", ""))
			)
		{
			if (!String.IsNullOrEmpty(this.txtPercentualDesconto.Text)
				&& (Convert.ToDecimal(this.txtPercentualDesconto.Text) > 0 && Convert.ToDecimal(this.txtPercentualDesconto.Text) < 100))
			{
				flag = true;

				for (int i = 0; i < this.DtGridView.Rows.Count; i++)
				{
					if (flag)
					{
						if (
							(
							!Convert.ToString(this.DtGridView.Rows[i]["QuantidadeMinima"]).Equals(this.txtQuantidadeMinima.Text)
							&&
							!Convert.ToString(this.DtGridView.Rows[i]["PercentualDesconto"]).Equals(this.txtPercentualDesconto.Text)
							)
							)
						{
							flag = true;
						}
						else
						{
							if (Convert.ToString(this.DtGridView.Rows[i]["idGridView"]).Equals(this.hddIdGridView.Value))
							{
								flag = true;
							}
							else
							{
								flag = false;
							}
						}
					}
					else
					{
						flag = false;
					}
				}

				if (!flag)
				{
					Util.ShowMessage("Cadastro conflitante em quantidade ou percentual com cadastro já inserido!");
				}
			}
			else
			{
				Util.ShowMessage("O desconto deve ser entre 0 e 100!");
			}
		}
		else
		{
			Util.ShowMessage("A quantidade deve ser maior que zero e não pode ser maior que o estoque de sergurança!");
		}

		if (!flag)
		{
			gdvQuantidadeDesconto.SelectedIndex = -1;
			this.hddIdGridView.Value = "0";
		}

		return flag;
	}

	#endregion

	#region [ CRUD ]

	/// <summary>
	/// 
	/// </summary>
	private bool PageIsValid()
	{
		bool flag = false;

		if (Page.IsValid)
		{
			//if (!String.IsNullOrEmpty(this.ddlTipoTitulo.SelectedValue) && Convert.ToInt32(this.ddlTipoTitulo.SelectedValue) > 0)
			{
				if (!String.IsNullOrEmpty(this.hddProdutoId.Value) && Convert.ToInt32(this.hddProdutoId.Value) > 0)
				{
                    if (!String.IsNullOrEmpty(this.txtEstoqueSeguranca.Text) && Convert.ToInt32(this.txtEstoqueSeguranca.Text.Replace(",", "").Replace(".", "")) > 0)
					{
                        if (String.IsNullOrEmpty(this.txtQuantidadeLimite.Text) || (Convert.ToInt32(this.txtQuantidadeLimite.Text.Replace(",", "").Replace(".", "")) > 0 && Convert.ToInt32(this.txtQuantidadeLimite.Text.Replace(",", "").Replace(".", "")) <= Convert.ToInt32(this.txtEstoqueSeguranca.Text.Replace(",", "").Replace(".", ""))))
                        {
                            if (!String.IsNullOrEmpty(this.ddlCompraConjuntaStatus.SelectedValue) && Convert.ToInt32(this.ddlCompraConjuntaStatus.SelectedValue) > 0)
                            {
                                if (this.chkAtivo.Checked)
                                {
                                    if (this.gdvQuantidadeDesconto.Rows.Count > 0)
                                    {
                                        flag = true;

                                        flag = ValidaQuantidadeMinima(flag);
                                    }
                                    else
                                    {
                                        Util.ShowMessage("Deve ser informado pelo menos um desconto se a compra for sinalizada como ativa.");
                                    }
                                }
                                else
                                {
                                    flag = true;

                                    flag = ValidaQuantidadeMinima(flag);
                                }
                            }
                            else
                            {
                                Util.ShowMessage("Deve ser informado o status da compra.");
                            }
                        }
                        else
                        {
                            Util.ShowMessage("A quantidade limite não pode ser igual a zero ou maior que o estoque de segurança.");
                        }
					}
					else
					{
						Util.ShowMessage("O estoque de segurança não pode ser vazio ou igual a zero!");
					}
				}
				else
				{
					Util.ShowMessage("Deve ser informado um título.");
				}
			}
            //else
            //{
            //    Util.ShowMessage("Deve ser informado um tipo de título.");
            //}
		}

		return flag;
	}

	/// <summary>
	/// Valida se a quantidade minima é superior ao estoque de segurança
	/// </summary>
	/// <param name="flag"></param>
	/// <returns></returns>
	private bool ValidaQuantidadeMinima(bool flag)
	{
		if (this.DtGridView != null)
		{
			for (int i = 0; i < this.DtGridView.Rows.Count; i++)
			{
				if (flag)
				{
                    if (Convert.ToInt32(this.DtGridView.Rows[i]["QuantidadeMinima"]) > Convert.ToInt32(this.txtEstoqueSeguranca.Text.Replace(",", "").Replace(".", "")))
					{
						flag = false;
					}
				}

				if (!flag)
				{
					Util.ShowMessage("A quantidade mínima deve ser maior que zero e não pode ser maior que o estoque de sergurança!");
				}
			}
		}

		return flag;
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveOrUpdate()
	{
		if (PageIsValid())
		{
			CompraConjunta compraConjuntaBO = new CompraConjunta();
			compraConjuntaBO.CompraConjuntaId = Convert.ToInt32(hddCompraConjuntaId.Value);
			compraConjuntaBO.Produto = new Produto();
			compraConjuntaBO.Produto.ProdutoId = Convert.ToInt32(this.hddProdutoId.Value);
			compraConjuntaBO.Ativa = this.chkAtivo.Checked;
            compraConjuntaBO.EstoqueSeguranca = Convert.ToInt32(this.txtEstoqueSeguranca.Text.Replace(",", "").Replace(".", ""));
			compraConjuntaBO.CompraConjuntaStatus = new CompraConjuntaStatus();
			compraConjuntaBO.CompraConjuntaStatus.CompraConjuntaStatusId = Convert.ToInt32(this.ddlCompraConjuntaStatus.SelectedValue);

            if (!string.IsNullOrEmpty(this.txtQuantidadeLimite.Text))
            {
                compraConjuntaBO.QuantidadeLimite = Convert.ToInt32(this.txtQuantidadeLimite.Text.Replace(",", "").Replace(".", ""));
            }

			if (!string.IsNullOrEmpty(this.txtDataInicialCompra.Text))
			{
				compraConjuntaBO.DataInicialCompra = Convert.ToDateTime(this.txtDataInicialCompra.Text);

                string[] arrayHoraIni = this.txtHoraInicialCompra.Text.Split(':');

                if (arrayHoraIni.Any())
                {
                    compraConjuntaBO.DataInicialCompra = compraConjuntaBO.DataInicialCompra.AddHours(Convert.ToInt32(arrayHoraIni[0].ToString()));

                    if (arrayHoraIni.Count() > 1)
                    {
                        compraConjuntaBO.DataInicialCompra = compraConjuntaBO.DataInicialCompra.AddMinutes(Convert.ToInt32(arrayHoraIni[1].ToString()));
                    }
                }
			}

			if (!string.IsNullOrEmpty(this.txtDataFinalCompra.Text))
			{
				compraConjuntaBO.DataFinalCompra = Convert.ToDateTime(this.txtDataFinalCompra.Text);

                string[] arrayHoraEnd = this.txtHoraFinalCompra.Text.Split(':');

                if (arrayHoraEnd.Any())
                {
                    compraConjuntaBO.DataFinalCompra = compraConjuntaBO.DataFinalCompra.AddHours(Convert.ToInt32(arrayHoraEnd[0].ToString()));

                    if (arrayHoraEnd.Count() > 1)
                    {
                        compraConjuntaBO.DataFinalCompra = compraConjuntaBO.DataFinalCompra.AddMinutes(Convert.ToInt32(arrayHoraEnd[1].ToString()));
                    }
                    else
                    {
                        compraConjuntaBO.DataFinalCompra = compraConjuntaBO.DataFinalCompra.AddMinutes(59);
                    }

                    compraConjuntaBO.DataFinalCompra = compraConjuntaBO.DataFinalCompra.AddSeconds(59);
                }
                else
                {
                    compraConjuntaBO.DataFinalCompra = compraConjuntaBO.DataFinalCompra.AddHours(23).AddMinutes(59).AddSeconds(59);
                }
			}

			CompraConjuntaBLL compraConjuntaBLL = new CompraConjuntaBLL();

			if (!compraConjuntaBLL.PeriodoConflitante(compraConjuntaBO))
			{
                if (!this.gdvQuantidadeDesconto.Enabled && (Convert.ToInt32(this.txtEstoqueSeguranca.Text.Replace(",", "").Replace(".", "")) < Convert.ToInt32(this.hddEstoqueSegurancaAnterior.Value))) // Se estiver false, a compra está em andamento
				{
					this.txtEstoqueSeguranca.Text = this.hddEstoqueSegurancaAnterior.Value;
					Util.ShowMessage("Não pode diminuir o estoque de segurança, a compra já está em andamento!");
				}
				else
				{
					if (compraConjuntaBO.CompraConjuntaId > 0)
					{
						this.Atualizar(compraConjuntaBO);
					}
					else
					{
                        compraConjuntaBO = this.Inserir(compraConjuntaBO);
					}
				}
			}
			else
			{
				Util.ShowMessage("Registro não pode ser salvo! Período está em conflito com outra Compra Conjunta.");
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="compraConjunta"></param>
	/// <returns></returns>
	private CompraConjunta Inserir(CompraConjunta compraConjunta)
	{
		compraConjunta = new CompraConjuntaBLL().Inserir(compraConjunta);
		hddCompraConjuntaId.Value = compraConjunta.CompraConjuntaId.ToString();
		Util.ShowMessage("Registro inserido com sucesso!");

		this.LoadForm(compraConjunta.CompraConjuntaId);

		return compraConjunta;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="compraConjuntaBO"></param>
	private void Atualizar(CompraConjunta compraConjuntaBO)
	{
		new CompraConjuntaBLL().Atualizar(compraConjuntaBO);

		//List<CompraConjuntaDesconto> compraConjuntaDescontoList = new List<CompraConjuntaDesconto>();

		if (this.gdvQuantidadeDesconto.Enabled)
		{
			new CompraConjuntaDescontoBLL().ExcluirRelacionado(compraConjuntaBO);

			for (int i = 0; i < this.DtGridView.Rows.Count; i++)
			{
				CompraConjuntaDesconto compraConjuntaDescontoBO = new CompraConjuntaDesconto();

				DataRow drow = this.DtGridView.NewRow();

				compraConjuntaDescontoBO.CompraConjunta = new CompraConjunta();
				compraConjuntaDescontoBO.CompraConjunta.CompraConjuntaId = compraConjuntaBO.CompraConjuntaId;
				compraConjuntaDescontoBO.QuantidadeMinima = Convert.ToInt32(this.DtGridView.Rows[i]["QuantidadeMinima"].ToString());
				compraConjuntaDescontoBO.PercentualDesconto = Convert.ToDecimal(this.DtGridView.Rows[i]["PercentualDesconto"].ToString());

				//compraConjuntaDescontoList.Add(compraConjuntaDesconto);

				new CompraConjuntaDescontoBLL().Inserir(compraConjuntaDescontoBO);
			}
		}

		Util.ShowMessage("Registro alterado com sucesso!");

		this.LoadForm(compraConjuntaBO.CompraConjuntaId);
	}

	#endregion

	#region [ Cancelar Compra Conjunta ]

	/// <summary>
	/// Cancela Compra Conjunta incluindo Gateway de Pagamento
	/// </summary>
	protected void CancelarCompraConjunta()
	{
		if (this.chkCancelar.Checked)
		{
            RetornoCancelarCompraConjunta retorno = new CompraConjuntaBLL().CancelarCompraConjunta(Convert.ToInt32(hddCompraConjuntaId.Value));

            if (RetornoCancelarCompraConjunta.Cancelado == retorno)
            {
                Util.ShowMessage("A compra conjunta cancelada.");

                this.LoadForm(Convert.ToInt32(hddCompraConjuntaId.Value));
            }
            else if (RetornoCancelarCompraConjunta.DesativadoNaoCancelado == retorno)
            {
                Util.ShowMessage("A compra conjunta não pode ser cancelada, somente desativada, existem pedidos desta compra que não puderam ser cancelados.");
            }
		}
		else
		{
			Util.ShowMessage("Deve ser selecionado o campo de Cancelar Compra Conjunta para efetuar a operãção!");
		}
	}

	#endregion

	#endregion
}