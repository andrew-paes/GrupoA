
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
    public partial interface IClippingImagemDAL
    {	
        void Inserir(ClippingImagem entidade);
        void Atualizar(ClippingImagem entidade);
        void Excluir(ClippingImagem entidade);
        ClippingImagem Carregar(ClippingImagem entidade);
		
		IEnumerable<ClippingImagem> Carregar(Arquivo entidade);
		
		IEnumerable<ClippingImagem> Carregar(Clipping entidade);
				
        IEnumerable<ClippingImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ClippingImagem> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
