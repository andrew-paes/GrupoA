
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
    public partial interface IClippingDAL
    {	
        void Inserir(Clipping entidade);
        void Atualizar(Clipping entidade);
        void Excluir(Clipping entidade);
        Clipping Carregar(Clipping entidade);
				
		Clipping CarregarComDependencias(Clipping entidade);	
		
		IEnumerable<Clipping> Carregar(ClippingImagem entidade);
				
        IEnumerable<Clipping> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Clipping> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
