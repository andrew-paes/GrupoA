
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
    public partial interface ILogCategoriaDAL
    {	
        void Inserir(LogCategoria entidade);
        void Atualizar(LogCategoria entidade);
        void Excluir(LogCategoria entidade);
        LogCategoria Carregar(LogCategoria entidade);
		
		IEnumerable<LogCategoria> Carregar(LogEvento entidade);
				
        IEnumerable<LogCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<LogCategoria> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
