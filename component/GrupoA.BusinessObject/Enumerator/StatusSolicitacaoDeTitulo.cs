using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.Enumerator
{
    public enum StatusSolicitacaoDeTitulo
	{
		SolicitacaoOk = 1,
		TituloInexistente = 2,
		TituloJaSolicitado = 3,
        LimiteExcedido = 4
	}
}