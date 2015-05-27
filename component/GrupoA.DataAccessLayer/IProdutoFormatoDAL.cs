
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
    public partial interface IProdutoFormatoDAL
    {	
        void Inserir(ProdutoFormato entidade);
        void Atualizar(ProdutoFormato entidade);
        void Excluir(ProdutoFormato entidade);
        ProdutoFormato Carregar(ProdutoFormato entidade);
				
		ProdutoFormato CarregarComDependencias(ProdutoFormato entidade);	
				
        IEnumerable<ProdutoFormato> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ProdutoFormato> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
