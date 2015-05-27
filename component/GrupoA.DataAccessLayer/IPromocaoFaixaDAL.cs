
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
    public partial interface IPromocaoFaixaDAL
    {	
        void Inserir(PromocaoFaixa entidade);
        void Atualizar(PromocaoFaixa entidade);
        void Excluir(PromocaoFaixa entidade);
        PromocaoFaixa Carregar(PromocaoFaixa entidade);
		
		IEnumerable<PromocaoFaixa> Carregar(Promocao entidade);
				
        IEnumerable<PromocaoFaixa> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<PromocaoFaixa> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
