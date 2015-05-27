
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
    public partial interface ICursoPanamericanoDAL
    {	
        void Inserir(CursoPanamericano entidade);
        void Atualizar(CursoPanamericano entidade);
        void Excluir(CursoPanamericano entidade);
        CursoPanamericano Carregar(CursoPanamericano entidade);
		
		IEnumerable<CursoPanamericano> Carregar(Categoria entidade);
		
		IEnumerable<CursoPanamericano> Carregar(Arquivo entidade);
				
        IEnumerable<CursoPanamericano> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<CursoPanamericano> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
