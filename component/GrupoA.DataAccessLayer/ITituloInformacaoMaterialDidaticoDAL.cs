
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
    public partial interface ITituloInformacaoMaterialDidaticoDAL
    {	
        void Inserir(TituloInformacaoMaterialDidatico entidade);
        void Atualizar(TituloInformacaoMaterialDidatico entidade);
        void Excluir(TituloInformacaoMaterialDidatico entidade);
        TituloInformacaoMaterialDidatico Carregar(TituloInformacaoMaterialDidatico entidade);
				
		TituloInformacaoMaterialDidatico CarregarComDependencias(TituloInformacaoMaterialDidatico entidade);	
				
        IEnumerable<TituloInformacaoMaterialDidatico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloInformacaoMaterialDidatico> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
