
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
    public partial interface IConteudoTipoDAL
    {	
        void Inserir(ConteudoTipo entidade);
        void Atualizar(ConteudoTipo entidade);
        void Excluir(ConteudoTipo entidade);
        ConteudoTipo Carregar(ConteudoTipo entidade);
		
		IEnumerable<ConteudoTipo> Carregar(Conteudo entidade);
				
        IEnumerable<ConteudoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ConteudoTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
