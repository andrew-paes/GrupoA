using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_meioPagamento_meioPagamento : SmartUserControl
{
	#region [ Properties ]

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
		if (!IsPostBack)
		{
			LoadForm();
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
		//valida se esta ativo mas nao possuia faixas de preços
		if (chkAtivo.Checked && this.DtGridView.Rows.Count < 1)
		{
			Util.ShowMessage("Meio de Pagamento está configurado como ativo, mas não possui faixa de preço associada. O registro não pode ser salvo.");
		}
		else
		{
			SaveMeioPagamento();
		}
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
	protected void dgFaixas_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		DeleteRow(e);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void dgFaixas_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.SelectedRow();
	}

	#endregion

	#region [ Methods ]

	/// <summary>
	/// 
	/// </summary>
	protected void LoadForm()
	{
		this.InitGridView();
		if (IdRegistro > 0)
		{
			hddMeioPagamentoId.Value = IdRegistro.ToString();

			this.pnlDescontos.Visible = true;

			MeioPagamento meioPagamento = new MeioPagamentoBLL().CarregarComDependencias(new MeioPagamento() { MeioPagamentoId = Convert.ToInt32(IdRegistro) });

			this.txtMeioPagamentoId.Text = meioPagamento.MeioPagamentoId.ToString();
			this.txtNome.Text = meioPagamento.Nome;
			this.txtCodigoLegado.Text = meioPagamento.CodigoLegado;
            this.txtCodigoGateway.Text = meioPagamento.CodigoGateway;
			this.chkAtivo.Checked = meioPagamento.Ativo;
			//bind de lista de faixas
			CarregarFaixas(meioPagamento);
			//não alterar PK
			this.txtMeioPagamentoId.Enabled = false;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	protected void SaveMeioPagamento()
	{
		MeioPagamentoBLL meioPagamentoBLL = new MeioPagamentoBLL();
		MeioPagamento meioPagamento = new MeioPagamento();
		meioPagamento.MeioPagamentoId = Convert.ToInt32(this.txtMeioPagamentoId.Text);
		meioPagamento.Nome = this.txtNome.Text;
		meioPagamento.CodigoLegado = this.txtCodigoLegado.Text;
        meioPagamento.CodigoGateway = this.txtCodigoGateway.Text;

		if (IdRegistro > 0 || (!String.IsNullOrEmpty(hddMeioPagamentoId.Value)))
		{
			meioPagamento.Ativo = this.chkAtivo.Checked;
			meioPagamentoBLL.Atualizar(meioPagamento);
			if (this.dgFaixas.Enabled)
			{
				new MeioPagamentoFaixaBLL().ExcluirRelacionado(meioPagamento);

				for (int i = 0; i < this.DtGridView.Rows.Count; i++)
				{
					MeioPagamentoFaixa meioPagamentoFaixa = new MeioPagamentoFaixa();

					DataRow drow = this.DtGridView.NewRow();

					meioPagamentoFaixa.MeioPagamento = new MeioPagamento();
					meioPagamentoFaixa.MeioPagamento.MeioPagamentoId = meioPagamento.MeioPagamentoId;
					meioPagamentoFaixa.ValorMinimo = Convert.ToDecimal(this.DtGridView.Rows[i]["valorMinimo"].ToString());
					meioPagamentoFaixa.NumeroParcelas = Convert.ToInt32(this.DtGridView.Rows[i]["numeroParcelas"].ToString());
                    meioPagamentoFaixa.CodigoGatewayFaixa = string.Concat("A", this.DtGridView.Rows[i]["numeroParcelas"].ToString().PadLeft(2, '0'));
					meioPagamentoFaixa.CodigoLegado = this.DtGridView.Rows[i]["codigoLegadoFaixa"].ToString();

					new MeioPagamentoFaixaBLL().Inserir(meioPagamentoFaixa);
				}
			}
			Util.ShowUpdateMessage();
		}
		else
		{
			meioPagamento.Ativo = false;
			meioPagamentoBLL.Inserir(meioPagamento);
			Util.ShowInsertMessage();

			hddMeioPagamentoId.Value = meioPagamento.MeioPagamentoId.ToString();

			pnlDescontos.Visible = true;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="entidade"></param>
	private void CarregarFaixas(MeioPagamento entidade)
	{
		if (entidade.MeioPagamentoFaixas != null && entidade.MeioPagamentoFaixas.Count > 0)
		{
			foreach (MeioPagamentoFaixa meioPagamentoFaixa in entidade.MeioPagamentoFaixas)
			{
				AddRow(meioPagamentoFaixa);
			}

			this.BindGridView();
		}
	}

	#region [ GridView Desconto ]

	/// <summary>
	/// 
	/// </summary>
	private void InitGridView()
	{
		this.DtGridView = new DataTable();
		this.DtGridView.Columns.Add(new DataColumn("valorMinimo", typeof(string)));
		this.DtGridView.Columns.Add(new DataColumn("numeroParcelas", typeof(string)));
		this.DtGridView.Columns.Add(new DataColumn("codigoLegadoFaixa", typeof(string)));
		this.DtGridView.Columns.Add(new DataColumn("idGridView", typeof(int)));

		this.BindGridView();
	}

	/// <summary>
	/// 
	/// </summary>
	private void BindGridView()
	{
		this.txtValorMinimo.Text = string.Empty;
		this.txtNumeroParcelas.Text = string.Empty;
		this.txtCodigoLegadoFaixa.Text = string.Empty;

		DataView dvGriView = DtGridView.DefaultView;
		dvGriView.Sort = "valorMinimo ASC";
		DtGridView = dvGriView.ToTable();

		dgFaixas.DataSource = this.DtGridView;
		dgFaixas.DataBind();
	}

	/// <summary>
	/// 
	/// </summary>
	private void AddEditRow()
	{
		if (this.DtGridView != null)
		{
			// Verify if a row is selected
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
		dgFaixas.DataSource = this.DtGridView;
		dgFaixas.DataBind();
	}

	/// <summary>
	/// Add new row to Grid View
	/// </summary>
	private void AddRow()
	{
		if (RowIsValid())
		{
			DataRow drow = this.DtGridView.NewRow();
			drow["valorMinimo"] = this.txtValorMinimo.Text;
			drow["numeroParcelas"] = this.txtNumeroParcelas.Text;
			drow["codigoLegadoFaixa"] = this.txtCodigoLegadoFaixa.Text;
			drow["idGridView"] = ++this.RowsCountGridView;
			this.DtGridView.Rows.Add(drow);
		}
	}

	/// <summary>
	/// Add new row to Grid View
	/// </summary>
	private void AddRow(MeioPagamentoFaixa meioPagamentoFaixa)
	{
		DataRow drow = this.DtGridView.NewRow();
		drow["valorMinimo"] = meioPagamentoFaixa.ValorMinimo;
		drow["numeroParcelas"] = meioPagamentoFaixa.NumeroParcelas;
		drow["codigoLegadoFaixa"] = meioPagamentoFaixa.CodigoLegado;
		drow["idGridView"] = ++this.RowsCountGridView;
		this.DtGridView.Rows.Add(drow);
	}

	/// <summary>
	/// 
	/// </summary>
	private void SelectedRow()
	{
		this.hddIdGridView.Value = dgFaixas.SelectedValue.ToString();
		this.txtValorMinimo.Text = dgFaixas.SelectedDataKey["valorMinimo"].ToString();
		this.txtNumeroParcelas.Text = dgFaixas.SelectedDataKey["numeroParcelas"].ToString();
		this.txtCodigoLegadoFaixa.Text = dgFaixas.SelectedDataKey["codigoLegadoFaixa"].ToString();

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
					this.DtGridView.Rows[i]["valorMinimo"] = this.txtValorMinimo.Text;
					this.DtGridView.Rows[i]["numeroParcelas"] = this.txtNumeroParcelas.Text;
					this.DtGridView.Rows[i]["codigoLegadoFaixa"] = this.txtCodigoLegadoFaixa.Text;

					hddIdGridView.Value = "0";
					dgFaixas.SelectedIndex = -1;

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

		if (!String.IsNullOrEmpty(this.txtValorMinimo.Text) && Convert.ToDecimal(this.txtValorMinimo.Text) > 0)
		{
			if (!String.IsNullOrEmpty(this.txtNumeroParcelas.Text) && Convert.ToInt32(this.txtNumeroParcelas.Text) > 0)
			{
				flag = true;

				for (int i = 0; i < this.DtGridView.Rows.Count; i++)
				{
					if (flag)
					{
						if (
							(
							!Convert.ToString(this.DtGridView.Rows[i]["valorMinimo"]).Equals(this.txtValorMinimo.Text)
							&&
							!Convert.ToString(this.DtGridView.Rows[i]["numeroParcelas"]).Equals(this.txtNumeroParcelas.Text)
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
					Util.ShowMessage("Cadastro conflitante em Valor Mínimo ou Número de Parcelas com cadastro já inserido!");
				}
			}
			else
			{
				Util.ShowMessage("Numero de parcelas deve ser maior que zero!");
			}
		}
		else
		{
			Util.ShowMessage("O Valor Mínimo deve ser maior que zero!");
		}

		if (!flag)
		{
			dgFaixas.SelectedIndex = -1;
			this.hddIdGridView.Value = "0";
		}

		return flag;
	}

	#endregion

	#endregion
}