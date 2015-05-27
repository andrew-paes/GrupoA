
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
    public partial interface ITituloAvaliacaoDAL
    {	
        void Inserir(TituloAvaliacao entidade);
        void Atualizar(TituloAvaliacao entidade);
        void Excluir(TituloAvaliacao entidade);
        TituloAvaliacao Carregar(TituloAvaliacao entidade);
		
		IEnumerable<TituloAvaliacao> Carregar(TituloSolicitacao entidade);
				
        IEnumerable<TituloAvaliacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloAvaliacao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
