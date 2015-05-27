using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    public class PedidoCompraConjuntaBLL : BaseBLL
	{
		#region Declarações DAL

		private IPedidoCompraConjuntaDAL _pedidoCompraConjuntaDal;
		private IPedidoCompraConjuntaDAL PedidoCompraConjuntaDal
		{
			get { return _pedidoCompraConjuntaDal ?? (_pedidoCompraConjuntaDal = new PedidoCompraConjuntaADO()); }
		}

		#endregion

		#region Métodos: CompraConjunta

		public PedidoCompraConjunta Carregar(PedidoCompraConjunta entidade)
		{
			entidade = PedidoCompraConjuntaDal.Carregar(entidade);
			return entidade;
		}

		public PedidoCompraConjunta Inserir(PedidoCompraConjunta entidade)
		{
			PedidoCompraConjuntaDal.Inserir(entidade);
			return entidade;
		}

		public void Atualizar(PedidoCompraConjunta entidade)
		{
			PedidoCompraConjuntaDal.Atualizar(entidade);
		}

		public IEnumerable<PedidoCompraConjunta> CarregarTodos(PedidoCompraConjunta entidade)
		{
			var pedidoCompraConjuntaFH = new PedidoCompraConjuntaFH() { PedidoCompraConjuntaId = entidade.PedidoCompraConjuntaId.ToString() };
			IEnumerable<PedidoCompraConjunta> iEnumPedidoCompraConjunta = PedidoCompraConjuntaDal.CarregarTodos(0, 0, null, null, pedidoCompraConjuntaFH);
			return iEnumPedidoCompraConjunta;
		}

		public List<PedidoCompraConjunta> CarregarTodosPorCompraConjunta(CompraConjunta compraConjunta)
		{
            List<PedidoCompraConjunta> iEnumPedidoCompraConjunta = (List<PedidoCompraConjunta>)PedidoCompraConjuntaDal.CarregarTodosPorCompraConjunta(compraConjunta);

			return iEnumPedidoCompraConjunta;
		}

		#endregion
	}
}