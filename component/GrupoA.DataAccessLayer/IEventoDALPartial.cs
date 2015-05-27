using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
	public partial interface IEventoDAL
	{
		IEnumerable<Evento> CarregarPorAreaInteresse(Usuario entidade, Int32 quantidadeRegistros);
		int ContarEventoBusca(string palavra);
        List<Evento> CarregarEventoBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra);
        Int32 ContarTodosEventosPorInteresseUsuarioId(Int32 usuarioId);
        IEnumerable<Evento> CarregarTodosEventosPorInteresseUsuarioId(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32 usuarioId);
        List<Evento> CarregarEventosParaEnviarAlerta();
	}
}