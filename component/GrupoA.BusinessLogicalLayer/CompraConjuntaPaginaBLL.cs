using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;
using System;

namespace GrupoA.BusinessLogicalLayer
{
    public class CompraConjuntaPaginaBLL : BaseBLL
	{
		#region Declarações DAL

        private ICompraConjuntaPaginaDAL _compraConjuntaPaginaDAL;
        private ICompraConjuntaPaginaDAL CompraConjuntaPaginaDAL
		{
			get
			{
				if (_compraConjuntaPaginaDAL == null)
                    _compraConjuntaPaginaDAL = new CompraConjuntaPaginaADO();
                return _compraConjuntaPaginaDAL;
			}
		}

		private ICompraConjuntaDescontoDAL _compraConjuntaDescontoDAL;
		private ICompraConjuntaDescontoDAL CompraConjuntaDescontoDAL
		{
			get
			{
				if (_compraConjuntaDescontoDAL == null)
					_compraConjuntaDescontoDAL = new CompraConjuntaDescontoADO();
				return _compraConjuntaDescontoDAL;
			}
		}

		#endregion

		#region Métodos

        public CompraConjuntaPagina CarregarPorIdCompraConjuntaPagina(int CompraConjuntaPaginaId)
        {
            return CompraConjuntaPaginaDAL.CarregarPorIdCompraConjuntaPagina(CompraConjuntaPaginaId);
        }

        public CompraConjuntaPagina Carregar(CompraConjuntaPagina entidade)
		{
            entidade = CompraConjuntaPaginaDAL.Carregar(entidade);
			return entidade;
		}

        public CompraConjuntaPagina Inserir(CompraConjuntaPagina entidade)
		{
            CompraConjuntaPaginaDAL.Inserir(entidade);
			return entidade;
		}

        public void Atualizar(CompraConjuntaPagina entidade)
		{
            CompraConjuntaPaginaDAL.Atualizar(entidade);
		}

        //public IEnumerable<CompraConjuntaPagina> CarregarAutores(CompraConjuntaPagina entidade)
        //{
        //    var compraConjuntaFH = new CompraConjuntaFH() { Ativa = entidade..Ativa.ToString() };
        //    IEnumerable<CompraConjuntaPagina> autores = CompraConjuntaPaginaDAL.CarregarAutores(0, 0, null, null, compraConjuntaFH);
        //    return autores;
        //}

        //public bool PeriodoConflitante(CompraConjunta compraConjunta)
        //{
        //    bool flag = CompraConjuntaDal.PeriodoConflitante(compraConjunta);
        //    return flag;
        //}

        //public bool CompraConjuntaRelacionada(CompraConjunta compraConjunta)
        //{
        //    bool flag = CompraConjuntaDal.CompraConjuntaRelacionada(compraConjunta);
        //    return flag;
        //}

        public void ExcluirCompraConjunta(CompraConjuntaPagina compraConjuntaPagina)
		{
			bool flag = true;
            compraConjuntaPagina = this.Carregar(compraConjuntaPagina);

//			flag = this.CompraConjuntaRelacionada(compraConjunta);

			if (flag) // Relacionado
			{
				// Sim
				//compraConjunta.Ativa = false;

				using (TransactionScope scope = new TransactionScope())
				{
                    CompraConjuntaPaginaDAL.Atualizar(compraConjuntaPagina);
					scope.Complete();
				}
			}
			else
			{
				// Não
				using (TransactionScope scope = new TransactionScope())
				{
					//CompraConjuntaDescontoDAL.ExcluirRelacionado(compraConjunta);
					CompraConjuntaPaginaDAL.Excluir(compraConjuntaPagina);
					scope.Complete();
				}
			}
		}

		#endregion
	}
}
