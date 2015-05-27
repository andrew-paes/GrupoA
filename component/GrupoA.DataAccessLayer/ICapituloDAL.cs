
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
    public partial interface ICapituloDAL
    {	
        void Inserir(Capitulo entidade);
        void Atualizar(Capitulo entidade);
        void Excluir(Capitulo entidade);
        Capitulo Carregar(Capitulo entidade);
				
		Capitulo CarregarComDependencias(Capitulo entidade);	
		
		IEnumerable<Capitulo> Carregar(Autor entidade);
		
		IEnumerable<Capitulo> Carregar(CapituloEletronico entidade);
		
		IEnumerable<Capitulo> Carregar(Titulo entidade);
		
		Capitulo Carregar(CapituloImpresso entidade);
				
        IEnumerable<Capitulo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Capitulo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
