
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
    public partial interface ITituloImagemResumoDAL
    {	
        void Inserir(TituloImagemResumo entidade);
        void Atualizar(TituloImagemResumo entidade);
        void Excluir(TituloImagemResumo entidade);
        TituloImagemResumo Carregar(TituloImagemResumo entidade);
		
		IEnumerable<TituloImagemResumo> Carregar(Arquivo entidade);
		
		IEnumerable<TituloImagemResumo> Carregar(Titulo entidade);
				
        IEnumerable<TituloImagemResumo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<TituloImagemResumo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
