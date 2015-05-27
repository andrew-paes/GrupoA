using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.PaymentGateway
{
    public interface IServicoPagamentoCofre
    {
        RetornoPedidoDTO EfetuarCobranca(PedidoCartaoCreditoDTO pedidoCartaoCreditoDto);
        RetornoCofreDTO CriarToken(PedidoCartaoCreditoDTO pedidoCartaoCreditoDTO);
    }
}