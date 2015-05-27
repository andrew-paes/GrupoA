
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
    public partial interface IUsuarioRevistaDAL
    {	
        void Inserir(UsuarioRevista entidade);
        void Atualizar(UsuarioRevista entidade);
        void Excluir(UsuarioRevista entidade);
        UsuarioRevista Carregar(UsuarioRevista entidade);
		
		IEnumerable<UsuarioRevista> Carregar(Revista entidade);
		
		IEnumerable<UsuarioRevista> Carregar(Usuario entidade);
				
        IEnumerable<UsuarioRevista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<UsuarioRevista> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
