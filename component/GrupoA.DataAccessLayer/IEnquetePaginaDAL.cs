
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
    public partial interface IEnquetePaginaDAL
    {	
        void Inserir(EnquetePagina entidade);
        void Atualizar(EnquetePagina entidade);
        void Excluir(EnquetePagina entidade);
        EnquetePagina Carregar(EnquetePagina entidade);
		
		IEnumerable<EnquetePagina> Carregar(Enquete entidade);
				
        IEnumerable<EnquetePagina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<EnquetePagina> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
