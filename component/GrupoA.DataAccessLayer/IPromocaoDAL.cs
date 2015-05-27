
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
    public partial interface IPromocaoDAL
    {	
        void Inserir(Promocao entidade);
        void Atualizar(Promocao entidade);
        void Excluir(Promocao entidade);
        Promocao Carregar(Promocao entidade);
		
		IEnumerable<Promocao> Carregar(PedidoItemPromocao entidade);
		
		IEnumerable<Promocao> Carregar(Pedido entidade);
		
		IEnumerable<Promocao> Carregar(Categoria entidade);
		
		IEnumerable<Promocao> Carregar(PromocaoCupom entidade);
		
		IEnumerable<Promocao> Carregar(PromocaoFaixa entidade);
		
		IEnumerable<Promocao> Carregar(Perfil entidade);
		
		IEnumerable<Promocao> Carregar(Produto entidade);
		
		IEnumerable<Promocao> Carregar(ProdutoTipo entidade);
		
		IEnumerable<Promocao> Carregar(Revista entidade);
		
		IEnumerable<Promocao> Carregar(Usuario entidade);
		
		IEnumerable<Promocao> Carregar(PromocaoTipo entidade);
				
        IEnumerable<Promocao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro);
        IEnumerable<Promocao> CarregarTodos();
        int TotalRegistros();	
		int TotalRegistros(IFilterHelper filtro);
	}
}
