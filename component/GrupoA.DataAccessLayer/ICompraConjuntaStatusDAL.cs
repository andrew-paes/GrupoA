
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
    public partial interface ICompraConjuntaStatusDAL
    {	
        void Inserir(CompraConjuntaStatus entidade);
        void Atualizar(CompraConjuntaStatus entidade);
        void Excluir(CompraConjuntaStatus entidade);
        CompraConjuntaStatus Carregar(CompraConjuntaStatus entidade);
		
		IEnumerable<CompraConjuntaStatus> Carregar(CompraConjunta entidade);
				
        IEnumerable<CompraConjuntaStatus> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CompraConjuntaStatus> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
