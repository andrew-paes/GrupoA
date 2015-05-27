
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
    public partial interface IDestaqueTituloImpressoDAL
    {	
        void Inserir(DestaqueTituloImpresso entidade);
        void Atualizar(DestaqueTituloImpresso entidade);
        void Excluir(DestaqueTituloImpresso entidade);
        DestaqueTituloImpresso Carregar(DestaqueTituloImpresso entidade);
		
		IEnumerable<DestaqueTituloImpresso> Carregar(Titulo entidade);
				
        IEnumerable<DestaqueTituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<DestaqueTituloImpresso> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
