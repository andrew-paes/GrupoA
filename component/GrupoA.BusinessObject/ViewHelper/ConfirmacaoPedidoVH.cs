using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
    public partial class ConfirmacaoPedidoVH : Pedido
    {
        List<PedidoItemVH> pedidoItensVH;
        public List<PedidoItemVH> PedidoItensVH
        {
            get
            {
                return pedidoItensVH;
            }
            set
            {
                pedidoItensVH = value;
            }
        }
    }
}
