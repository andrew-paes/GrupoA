
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
    public partial interface IRevistaArtigoControversiaDAL
    {	
        void Inserir(RevistaArtigoControversia entidade);
        void Atualizar(RevistaArtigoControversia entidade);
        void Excluir(RevistaArtigoControversia entidade);
        RevistaArtigoControversia Carregar(RevistaArtigoControversia entidade);
				
        IEnumerable<RevistaArtigoControversia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaArtigoControversia> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
