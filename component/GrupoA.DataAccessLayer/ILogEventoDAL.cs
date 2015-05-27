
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
    public partial interface ILogEventoDAL
    {	
        void Inserir(LogEvento entidade);
        void Atualizar(LogEvento entidade);
        void Excluir(LogEvento entidade);
        LogEvento Carregar(LogEvento entidade);
		
		IEnumerable<LogEvento> Carregar(LogOcorrencia entidade);
		
		IEnumerable<LogEvento> Carregar(LogCategoria entidade);
				
        IEnumerable<LogEvento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<LogEvento> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
