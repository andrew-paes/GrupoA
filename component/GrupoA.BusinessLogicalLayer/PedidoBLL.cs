using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a pedidos.
    /// </summary>
    public class PedidoBLL : BaseBLL
    {
        #region Declarações DAL

        private IPedidoDAL _pedidoDAL;
        private IPedidoSituacaoDAL _pedidoSituacaoDAL;
        private IPedidoItemDAL _pedidoItemDAL;

        private IPedidoDAL PedidoDAL
        {
            get
            {
                if (_pedidoDAL == null)
                    _pedidoDAL = new PedidoADO();
                return _pedidoDAL;
            }
        }
        private IPedidoSituacaoDAL PedidoSituacaoDAL
        {
            get
            {
                if (_pedidoSituacaoDAL == null)
                    _pedidoSituacaoDAL = new PedidoSituacaoADO();
                return _pedidoSituacaoDAL;

            }
        }
        private IPedidoItemDAL PedidoItemDAL
        {
            get
            {
                if (_pedidoItemDAL == null)
                    _pedidoItemDAL = new PedidoItemADO();
                return _pedidoItemDAL;

            }
        }

        #endregion

        public Pedido CarregarComDependencias(Pedido entidade)
        {
            entidade = PedidoDAL.CarregarPedidoComDependencias(entidade);
            return entidade;
        }

        /// <summary>
        /// Carrega Apenas os Pedidos que foram finalizados (STATUS =1 ) e que não foram sincronizados
        /// </summary>
        /// <returns>Retorna uma List de pedidos</returns>
        public IList<Pedido> CarregarFinalizadosNaoSincronizados()
        {
            IList<Pedido> list = new List<Pedido>();
            list = PedidoDAL.CarregarFinalizadosNaoSincronizados();
            return list;
        }

        public void Atualizar(Pedido entidade)
        {
            PedidoDAL.Atualizar(entidade);
        }

        public IEnumerable<Pedido> CarregarTodosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return PedidoDAL.CarregarTodosComDependencias(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        public int ContarTodosPedidos(IFilterHelper filtro)
        {
            return PedidoDAL.ContarTodosPedidos(filtro);
        }

        public void AtualizarStatusPedido(NameValueCollection nvc)
        {
            Pedido pedido = new Pedido();
            Int32 status = Convert.ToInt32(nvc["codigo_status"].ToString());

            pedido.PedidoId = Convert.ToInt32(nvc["codigo_pedido"].ToString());
            pedido.PedidoStatus = new PedidoStatus();

            switch (status)
            {
                case 2:
                    pedido.PedidoStatus.PedidoStatusId = 4;
                    break;
                case 3:
                    pedido.PedidoStatus.PedidoStatusId = 1;
                    break;
                case 4:
                    pedido.PedidoStatus.PedidoStatusId = 2;
                    break;
                default:
                    break;
            }

            PedidoDAL.AtualizarStatusPedido(pedido);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedido"></param>
        public void AtualizarStatusPedido(Pedido pedido)
        {
            PedidoDAL.AtualizarStatusPedido(pedido);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Pedido Carregar(Pedido entidade)
        {
            return PedidoDAL.Carregar(entidade);
        }
    }
}
