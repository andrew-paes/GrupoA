using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class ProdutoTipoBLL : BaseBLL
    {
        private IProdutoTipoDAL _produtoTipoDAL;

        private IProdutoTipoDAL ProdutoTipoDAL
        {
            get
            {
                if (_produtoTipoDAL == null)
                    _produtoTipoDAL = new ProdutoTipoADO();
                return _produtoTipoDAL;
            }
        }

        public ProdutoTipo Carregar(ProdutoTipo entidade)
        {
            return ProdutoTipoDAL.Carregar(entidade);
        }
        public IEnumerable<ProdutoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return ProdutoTipoDAL.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }
    }
}