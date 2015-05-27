
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
    public partial interface IRevistaPaginaDAL
    {	
        void Inserir(RevistaPagina entidade);
        void Atualizar(RevistaPagina entidade);
        void Excluir(RevistaPagina entidade);
        RevistaPagina Carregar(RevistaPagina entidade);
		
		IEnumerable<RevistaPagina> Carregar(Revista entidade);
				
        IEnumerable<RevistaPagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaPagina> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
