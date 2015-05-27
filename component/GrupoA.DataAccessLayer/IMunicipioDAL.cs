
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
    public partial interface IMunicipioDAL
    {	
        void Inserir(Municipio entidade);
        void Atualizar(Municipio entidade);
        void Excluir(Municipio entidade);
        Municipio Carregar(Municipio entidade);
		
		IEnumerable<Municipio> Carregar(Endereco entidade);
		
		IEnumerable<Municipio> Carregar(PedidoEndereco entidade);
		
		IEnumerable<Municipio> Carregar(Regiao entidade);
				
        IEnumerable<Municipio> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Municipio> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
