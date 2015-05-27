using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class PedidoStatusBLL : BaseBLL
    {
        private IPedidoStatusDAL _pedidoStatusDAL;

        private IPedidoStatusDAL PedidoStatusDAL
        {
            get
            {
                if (_pedidoStatusDAL == null)
                    _pedidoStatusDAL = new PedidoStatusADO();
                return _pedidoStatusDAL;

            }
        }

        public PedidoStatus Carregar(PedidoStatus entidade)
        {
            return PedidoStatusDAL.Carregar(entidade);
        }
        public IEnumerable<PedidoStatus> CarregarTodos()
        {
            return PedidoStatusDAL.CarregarTodos();
        }

    }
}
