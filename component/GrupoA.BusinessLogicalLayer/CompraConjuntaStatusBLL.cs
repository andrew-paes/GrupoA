using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    public class CompraConjuntaStatusBLL : BaseBLL
	{
		#region Declarações DAL

		private ICompraConjuntaStatusDAL _compraConjuntaStatusDAL;
		private ICompraConjuntaStatusDAL CompraConjuntaStatusDAL
		{
			get
			{
				if (_compraConjuntaStatusDAL == null)
					_compraConjuntaStatusDAL = new CompraConjuntaStatusADO();
				return _compraConjuntaStatusDAL;
			}
		}

		#endregion

		#region Métodos: Autor

		public CompraConjuntaStatus Carregar(CompraConjuntaStatus entidade)
		{
			entidade = CompraConjuntaStatusDAL.Carregar(entidade);
			return entidade;
		}

		public CompraConjuntaStatus Inserir(CompraConjuntaStatus entidade)
		{
			CompraConjuntaStatusDAL.Inserir(entidade);
			return entidade;
		}

		public void Atualizar(CompraConjuntaStatus entidade)
		{
			CompraConjuntaStatusDAL.Atualizar(entidade);
		}

		public IEnumerable<CompraConjuntaStatus> CarregarTodos(CompraConjuntaStatus entidade)
		{
			var compraConjuntaStatusFH = new CompraConjuntaStatusFH() { StatusCompra = entidade.StatusCompra };
			IEnumerable<CompraConjuntaStatus> autores = CompraConjuntaStatusDAL.CarregarTodos(0, 0, null, null, compraConjuntaStatusFH);
			return autores;
		}

		#endregion
	}
}
