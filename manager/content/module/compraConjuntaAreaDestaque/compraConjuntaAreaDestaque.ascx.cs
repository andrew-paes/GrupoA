using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using System.Transactions;

public partial class content_module_compraConjuntaAreaDestaque : SmartUserControl
{

	#region Eventos

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			LoadForm();
		}
	}

	protected void btnExecute_Click(object sender, EventArgs e)
	{
		Page.MaintainScrollPositionOnPostBack = false;
		SaveEntidadeTemplate();

	}

	#endregion

	#region Métodos

	protected void LoadForm()
	{
		listRel.TituloOrigem = "Compras não relacionadas";
		listRel.TituloDestino = "Compras já relacionadas";
		if (IdRegistro > 0)
		{
			CompraConjuntaPagina compraConjuntaPagina = new CompraConjuntaPaginaBLL().CarregarPorIdCompraConjuntaPagina(Convert.ToInt32(IdRegistro));
			this.txtNomeArea.Text = compraConjuntaPagina.Pagina;

			//Compras Conjuntas Paginas

			var compraConjuntaEmAberta = new CompraConjuntaBLL().CarregarCompraConjuntaEmAberta(new CompraConjunta(), Convert.ToInt32(IdRegistro));
			foreach (var item in compraConjuntaEmAberta)
			{
				listRel.listaOrigem.Items.Add(new ListItem(string.Concat(item.Produto.NomeProduto, " - ", item.DataInicialCompra.ToShortDateString(), " - ", item.DataFinalCompra.ToShortDateString()), item.CompraConjuntaId.ToString()));
			}

			var compraConjuntaComPaginaRelacionada = new CompraConjuntaBLL().CarregarCompraConjuntaComPaginaRelacionada(Convert.ToInt32(IdRegistro));
			foreach (var item in compraConjuntaComPaginaRelacionada)
			{
				listRel.listaDestino.Items.Add(new ListItem(string.Concat(item.Produto.NomeProduto, " - ", item.DataInicialCompra.ToShortDateString(), " - ", item.DataFinalCompra.ToShortDateString()), item.CompraConjuntaId.ToString()));
			}


		}
	}

	protected void SaveEntidadeTemplate()
	{

		bool sucesso = true;
		using (TransactionScope scope = new TransactionScope())
		{
			new CompraConjuntaBLL().ExcluirCompraConjuntaLocalizacao(Convert.ToInt32(IdRegistro));
			foreach (ListItem item in listRel.listaDestino.Items)
			{
				// To do: rever a não utilização do TRY Catch dentro da DAL, o métoso está retornando true sempre...
				sucesso = new CompraConjuntaBLL().InserirRelacionamentoPagina(Int32.Parse(item.Value), Convert.ToInt32(IdRegistro));
			}
			scope.Complete();
		}
		Util.ShowUpdateMessage();
	}

	#endregion

}