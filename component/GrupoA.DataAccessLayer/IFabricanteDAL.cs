
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
    public partial interface IFabricanteDAL
    {	
        void Inserir(Fabricante entidade);
        void Atualizar(Fabricante entidade);
        void Excluir(Fabricante entidade);
        Fabricante Carregar(Fabricante entidade);
		
		IEnumerable<Fabricante> Carregar(Produto entidade);
				
        IEnumerable<Fabricante> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Fabricante> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
