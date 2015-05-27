
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
    public partial interface IAutorDAL
    {	
        void Inserir(Autor entidade);
        void Atualizar(Autor entidade);
        void Excluir(Autor entidade);
        Autor Carregar(Autor entidade);
		
		IEnumerable<Autor> Carregar(Capitulo entidade);
		
		IEnumerable<Autor> Carregar(Arquivo entidade);
				
        IEnumerable<Autor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Autor> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
