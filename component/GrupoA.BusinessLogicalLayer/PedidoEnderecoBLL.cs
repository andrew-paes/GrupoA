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
    /// Classe que abstrai as regras de negócio referentes a pedidoEnderecos.
    /// </summary>
    public class PedidoEnderecoBLL : BaseBLL
    {

        private IPedidoEnderecoDAL _pedidoEnderecoDAL;

        private IPedidoEnderecoDAL PedidoEnderecoDAL
        {
            get
            {
                if (_pedidoEnderecoDAL == null)
                    _pedidoEnderecoDAL = new PedidoEnderecoADO();
                return _pedidoEnderecoDAL;

            }
        }

        public PedidoEndereco Carregar(PedidoEndereco entidade)
        {
            return PedidoEnderecoDAL.Carregar(entidade);
        }

        public IEnumerable<PedidoEndereco> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return PedidoEnderecoDAL.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        public PedidoEndereco Carregar(Pedido entidade)
        {
            return PedidoEnderecoDAL.Carregar(entidade);
        }
    }
}