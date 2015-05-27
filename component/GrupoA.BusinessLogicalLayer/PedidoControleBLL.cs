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
    public class PedidoControleBLL : BaseBLL
    {
        private IPedidoControleDAL _pedidoControleDAL;

        private IPedidoControleDAL PedidoControleDAL
        {
            get
            {
                if (_pedidoControleDAL == null)
                    _pedidoControleDAL = new PedidoControleADO();
                return _pedidoControleDAL;

            }
        }

        public PedidoControle Carregar(PedidoControle entidade)
        {
            return PedidoControleDAL.Carregar(entidade);
        }
    }
}