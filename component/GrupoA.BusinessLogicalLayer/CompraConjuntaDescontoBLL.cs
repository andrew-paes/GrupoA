using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;
using System.Transactions;
using System;

namespace GrupoA.BusinessLogicalLayer
{
    public class CompraConjuntaDescontoBLL : BaseBLL
    {
        #region Declarações DAL

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

        #region Métodos: CompraConjuntaDesconto

        public CompraConjuntaDesconto Carregar(CompraConjuntaDesconto entidade)
        {
            entidade = CompraConjuntaDescontoDAL.Carregar(entidade);
            return entidade;
        }

        public CompraConjuntaDesconto Inserir(CompraConjuntaDesconto entidade)
        {
            CompraConjuntaDescontoDAL.Inserir(entidade);
            return entidade;
        }

        public void Atualizar(CompraConjuntaDesconto entidade)
        {
            CompraConjuntaDescontoDAL.Atualizar(entidade);
        }

        public void ExcluirRelacionado(CompraConjunta entidade)
        {
            CompraConjuntaDescontoDAL.ExcluirRelacionado(entidade);
        }

        public IEnumerable<CompraConjuntaDesconto> CarregarTodos(CompraConjuntaDesconto entidade)
        {
            var compraConjuntaDescontoFH = new CompraConjuntaDescontoFH() { CompraConjuntaId = entidade.CompraConjunta.CompraConjuntaId.ToString() };
            IEnumerable<CompraConjuntaDesconto> autores = CompraConjuntaDescontoDAL.CarregarTodos(0, 0, new String[] { "quantidadeMinima" }, new String[] { "" }, compraConjuntaDescontoFH);
            return autores;
        }

        public CompraConjuntaDesconto CarregarPorCompraConjuntaValor(Int32 compraConjuntaId, Int32 quantidade)
        {
            return CompraConjuntaDescontoDAL.CarregarPorCompraConjuntaValor(compraConjuntaId, quantidade);
        }

        #endregion
    }
}