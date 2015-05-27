
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
    public partial interface IProdutoImagemTipoDAL
    {	
        void Inserir(ProdutoImagemTipo entidade);
        void Atualizar(ProdutoImagemTipo entidade);
        void Excluir(ProdutoImagemTipo entidade);
        ProdutoImagemTipo Carregar(ProdutoImagemTipo entidade);
		
		IEnumerable<ProdutoImagemTipo> Carregar(ProdutoImagem entidade);
				
        IEnumerable<ProdutoImagemTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProdutoImagemTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
