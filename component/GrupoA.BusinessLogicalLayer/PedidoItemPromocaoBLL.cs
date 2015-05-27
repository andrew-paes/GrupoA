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
    public class PedidoItemPromocaoBLL : BaseBLL
    {
        private IPedidoItemPromocaoDAL _PedidoItemPromocaoDAL;

        private IPedidoItemPromocaoDAL PedidoItemPromocaoDAL
        {
            get
            {
                if (_PedidoItemPromocaoDAL == null)
                    _PedidoItemPromocaoDAL = new PedidoItemPromocaoADO();
                return _PedidoItemPromocaoDAL;
            }
        }

        public PedidoItemPromocao Carregar(PedidoItemPromocao entidade)
        {
            return PedidoItemPromocaoDAL.Carregar(entidade);
        }
    }
}