
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
    public partial interface ILogOcorrenciaDAL
    {	
        void Inserir(LogOcorrencia entidade);
        void Atualizar(LogOcorrencia entidade);
        void Excluir(LogOcorrencia entidade);
        LogOcorrencia Carregar(LogOcorrencia entidade);
		
		IEnumerable<LogOcorrencia> Carregar(LogEvento entidade);
		
		IEnumerable<LogOcorrencia> Carregar(Usuario entidade);
				
        IEnumerable<LogOcorrencia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<LogOcorrencia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
