/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/

using System;
using System.Text;
using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess
{
	public partial interface INoticiaDAL
	{
		IEnumerable<Noticia> CarregarTodosValidosComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
		int ContarTodosValidosComDependencias();
		int ContarNoticiaBusca(string palavra);
        List<Noticia> CarregarNoticiaBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra);
        List<Noticia> CarregarNoticiasPorCategoria(Categoria categoria, Int32 qtdRegistros);
	}
}