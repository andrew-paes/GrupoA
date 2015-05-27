using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    public class MeioPagamentoFaixaBLL : BaseBLL
	{
		#region Declarações DAL

		private IMeioPagamentoFaixaDAL _meioPagamentoFaixaDAL;
		private IMeioPagamentoFaixaDAL MeioPagamentoFaixaDAL
		{
			get
			{
				if (_meioPagamentoFaixaDAL == null)
					_meioPagamentoFaixaDAL = new MeioPagamentoFaixaADO();
				return _meioPagamentoFaixaDAL;
			}
		}

		#endregion

		#region Métodos: MeioPagamentoFaixa

		public MeioPagamentoFaixa Carregar(MeioPagamentoFaixa entidade)
		{
			entidade = MeioPagamentoFaixaDAL.Carregar(entidade);
			return entidade;
		}

		public MeioPagamentoFaixa Inserir(MeioPagamentoFaixa entidade)
		{
			MeioPagamentoFaixaDAL.Inserir(entidade);
			return entidade;
		}

		public void Atualizar(MeioPagamentoFaixa entidade)
		{
			MeioPagamentoFaixaDAL.Atualizar(entidade);
		}

		public void ExcluirRelacionado(MeioPagamento entidade)
		{
			MeioPagamentoFaixaDAL.ExcluirRelacionado(entidade);
		}

		public IEnumerable<MeioPagamentoFaixa> CarregarTodos(MeioPagamentoFaixa entidade)
		{
            MeioPagamentoFaixaFH meioPagamentoFaixaFH = new MeioPagamentoFaixaFH() { MeioPagamentoId = entidade.MeioPagamento.MeioPagamentoId.ToString() };
			IEnumerable<MeioPagamentoFaixa> retorno = MeioPagamentoFaixaDAL.CarregarTodos(0, 0, null, null, meioPagamentoFaixaFH);
            return retorno;
		}

        public MeioPagamentoFaixa CarregarPorPagamento(Pagamento entidade)
        {
            return MeioPagamentoFaixaDAL.CarregarPorPagamento(entidade);
        }

		#endregion
	}
}