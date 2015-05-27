using System;
using System.Collections.Generic;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess
{
    public partial interface IMidiaDAL
    {
        List<Midia> CarregarMidiasPorCategoria(Categoria categoria, Revista revista);
        IEnumerable<Midia> CarregarTodosPorRevista(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Int32? midiaTipoId, Int32 revistaId);
        Int32 ContarTodosPorRevista(Int32? midiaTipoId, Int32 revistaId);
        int ContarMidiaBusca(String palavra);
        List<Midia> CarregarMidiaBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra);
	}
}
