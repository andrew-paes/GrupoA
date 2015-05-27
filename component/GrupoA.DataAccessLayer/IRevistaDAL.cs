
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
    public partial interface IRevistaDAL
    {	
        void Inserir(Revista entidade);
        void Atualizar(Revista entidade);
        void Excluir(Revista entidade);
        Revista Carregar(Revista entidade);
		
		IEnumerable<Revista> Carregar(MidiaRevista entidade);
		
		IEnumerable<Revista> Carregar(Promocao entidade);
		
		IEnumerable<Revista> Carregar(Categoria entidade);
		
		IEnumerable<Revista> Carregar(RevistaAssinatura entidade);
		
		IEnumerable<Revista> Carregar(RevistaEdicao entidade);
		
		IEnumerable<Revista> Carregar(RevistaPagina entidade);
		
		IEnumerable<Revista> Carregar(RevistaSecao entidade);
		
		IEnumerable<Revista> Carregar(UsuarioRevista entidade);
				
        IEnumerable<Revista> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Revista> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
