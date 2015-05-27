
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
    public partial interface IRevistaAssinaturaDAL
    {	
        void Inserir(RevistaAssinatura entidade);
        void Atualizar(RevistaAssinatura entidade);
        void Excluir(RevistaAssinatura entidade);
        RevistaAssinatura Carregar(RevistaAssinatura entidade);
				
		RevistaAssinatura CarregarComDependencias(RevistaAssinatura entidade);	
		
		IEnumerable<RevistaAssinatura> Carregar(Revista entidade);
				
        IEnumerable<RevistaAssinatura> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<RevistaAssinatura> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
