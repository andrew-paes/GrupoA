using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.Enumerator
{
	public enum StatusDoPedido
	{
		Finalizado = 1,
		Cancelado = 2,
		AguardandoProcessamento = 3,
		AguardandoPagamento = 4
	}
}