using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
    public partial interface IPromocaoCupomPedidoDAL
    {
        List<PromocaoCupomPedido> CarregarPromocaoCupomPedidoPorPromocao(Promocao promocao);
        Int32 TotalRegistrosPorCodigoCupom(String codigoCupom);
	}
}
