
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
    public partial interface IUsuarioControleDAL
    {	
        void Inserir(UsuarioControle entidade);
        void Atualizar(UsuarioControle entidade);
        void Excluir(UsuarioControle entidade);
        UsuarioControle Carregar(UsuarioControle entidade);
				
		UsuarioControle CarregarComDependencias(UsuarioControle entidade);	
				
        IEnumerable<UsuarioControle> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<UsuarioControle> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
