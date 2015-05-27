using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IPedidoCompraConjuntaDAL
    {
        IEnumerable<PedidoCompraConjunta> CarregarTodosPorCompraConjunta(CompraConjunta entidade);
    }
}
