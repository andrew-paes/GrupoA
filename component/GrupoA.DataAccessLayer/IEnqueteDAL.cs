
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
    public partial interface IEnqueteDAL
    {	
        void Inserir(Enquete entidade);
        void Atualizar(Enquete entidade);
        void Excluir(Enquete entidade);
        Enquete Carregar(Enquete entidade);
		
		IEnumerable<Enquete> Carregar(EnquetePagina entidade);
		
		IEnumerable<Enquete> Carregar(EnqueteOpcao entidade);
		
		IEnumerable<Enquete> Carregar(Usuario entidade);
				
        IEnumerable<Enquete> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Enquete> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
