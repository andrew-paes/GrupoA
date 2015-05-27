
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
    public partial interface ITituloInformacaoComentarioEspecialistaDAL
    {	
        void Inserir(TituloInformacaoComentarioEspecialista entidade);
        void Atualizar(TituloInformacaoComentarioEspecialista entidade);
        void Excluir(TituloInformacaoComentarioEspecialista entidade);
        TituloInformacaoComentarioEspecialista Carregar(TituloInformacaoComentarioEspecialista entidade);
				
		TituloInformacaoComentarioEspecialista CarregarComDependencias(TituloInformacaoComentarioEspecialista entidade);	
		
		IEnumerable<TituloInformacaoComentarioEspecialista> Carregar(TituloInformacaoComentarioEspecialistaCategoria entidade);
		
		IEnumerable<TituloInformacaoComentarioEspecialista> Carregar(Arquivo entidade);
		
		IEnumerable<TituloInformacaoComentarioEspecialista> Carregar(ComentarioFormato entidade);
				
        IEnumerable<TituloInformacaoComentarioEspecialista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoComentarioEspecialista> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
