
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
    public partial interface ICompraConjuntaPaginaDAL
    {	
        void Inserir(CompraConjuntaPagina entidade);
        void Atualizar(CompraConjuntaPagina entidade);
        void Excluir(CompraConjuntaPagina entidade);
        CompraConjuntaPagina Carregar(CompraConjuntaPagina entidade);
		
		IEnumerable<CompraConjuntaPagina> Carregar(CompraConjunta entidade);
				
        IEnumerable<CompraConjuntaPagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CompraConjuntaPagina> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
