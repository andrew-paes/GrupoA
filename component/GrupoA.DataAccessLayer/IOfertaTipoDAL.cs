
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
    public partial interface IOfertaTipoDAL
    {	
        void Inserir(OfertaTipo entidade);
        void Atualizar(OfertaTipo entidade);
        void Excluir(OfertaTipo entidade);
        OfertaTipo Carregar(OfertaTipo entidade);
		
		IEnumerable<OfertaTipo> Carregar(Oferta entidade);
				
        IEnumerable<OfertaTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<OfertaTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
