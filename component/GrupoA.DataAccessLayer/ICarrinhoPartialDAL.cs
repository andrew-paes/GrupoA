
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
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess
{
    public partial interface ICarrinhoDAL
    {
        Carrinho CarregarAbertoPorUsuario(Usuario usuario, CarrinhoStatus carrinhoStatus);
        double CalculaFrete(int cepInicial, int cepFinal, decimal peso);
        void AtualizarStatus(Carrinho carrinho);
        List<CarrinhoItemVH> CarregarPorCarrinho(Carrinho carrinho);
        List<CarrinhoItemVH> CarregarPorProduto(Carrinho carrinho);
	}
}
