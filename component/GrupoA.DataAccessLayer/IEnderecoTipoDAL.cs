
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
    public partial interface IEnderecoTipoDAL
    {	
        void Inserir(EnderecoTipo entidade);
        void Atualizar(EnderecoTipo entidade);
        void Excluir(EnderecoTipo entidade);
        EnderecoTipo Carregar(EnderecoTipo entidade);
		
		IEnumerable<EnderecoTipo> Carregar(Endereco entidade);
		
		IEnumerable<EnderecoTipo> Carregar(PedidoEndereco entidade);
				
        IEnumerable<EnderecoTipo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<EnderecoTipo> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
