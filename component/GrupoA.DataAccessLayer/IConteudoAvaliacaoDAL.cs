
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
    public partial interface IConteudoAvaliacaoDAL
    {	
        void Inserir(ConteudoAvaliacao entidade);
        void Atualizar(ConteudoAvaliacao entidade);
        void Excluir(ConteudoAvaliacao entidade);
        ConteudoAvaliacao Carregar(ConteudoAvaliacao entidade);
				
		ConteudoAvaliacao CarregarComDependencias(ConteudoAvaliacao entidade);	
				
        IEnumerable<ConteudoAvaliacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ConteudoAvaliacao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
