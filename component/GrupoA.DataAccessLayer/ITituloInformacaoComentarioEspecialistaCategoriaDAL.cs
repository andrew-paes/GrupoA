
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
    public partial interface ITituloInformacaoComentarioEspecialistaCategoriaDAL
    {	
        void Inserir(TituloInformacaoComentarioEspecialistaCategoria entidade);
        void Atualizar(TituloInformacaoComentarioEspecialistaCategoria entidade);
        void Excluir(TituloInformacaoComentarioEspecialistaCategoria entidade);
        TituloInformacaoComentarioEspecialistaCategoria Carregar(TituloInformacaoComentarioEspecialistaCategoria entidade);
		
		IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> Carregar(Categoria entidade);
		
		IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> Carregar(TituloInformacaoComentarioEspecialista entidade);
				
        IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoComentarioEspecialistaCategoria> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
