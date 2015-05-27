
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
    public partial interface IConfiguracaoValorDAL
    {	
        void Inserir(ConfiguracaoValor entidade);
        void Atualizar(ConfiguracaoValor entidade);
        void Excluir(ConfiguracaoValor entidade);
        ConfiguracaoValor Carregar(ConfiguracaoValor entidade);
				
		ConfiguracaoValor CarregarComDependencias(ConfiguracaoValor entidade);	
				
        IEnumerable<ConfiguracaoValor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<ConfiguracaoValor> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
