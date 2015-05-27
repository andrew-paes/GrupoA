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
    public class PedidoItemBLL : BaseBLL
    {
        private IPedidoItemDAL _PedidoItemDAL;

        private IPedidoItemDAL PedidoItemDAL
        {
            get
            {
                if (_PedidoItemDAL == null)
                    _PedidoItemDAL = new PedidoItemADO();
                return _PedidoItemDAL;
            }
        }

        public PedidoItem Carregar(PedidoItem entidade)
        {
            return PedidoItemDAL.Carregar(entidade);
        }

        /// <summary>
        /// Carregar PedidoItem usando PedidoId e ProdutoId
        /// </summary>
        /// <param name="pedidoBO"></param>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        public PedidoItem Carregar(Pedido pedidoBO, Produto produtoBO)
        {
            return PedidoItemDAL.Carregar(pedidoBO, produtoBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<PedidoItem> Carregar(Pedido entidade)
        {
            return PedidoItemDAL.Carregar(entidade).ToList();
        }

        public void Atualizar(PedidoItem entidade)
        {
            PedidoItemDAL.Atualizar(entidade);
        }
    }
}