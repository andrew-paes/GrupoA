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
	public partial interface ICompraConjuntaDAL
	{
        IEnumerable<CompraConjunta> CarregarCompraConjuntaComPaginaRelacionada(int compraConjuntaPaginaId);
        IEnumerable<CompraConjunta> CarregarCompraConjuntaEmAberta(CompraConjunta entidade, int compraConjuntaPaginaId);
        bool PeriodoConflitante(CompraConjunta compraConjunta);
		bool CompraConjuntaRelacionada(CompraConjunta compraConjunta);
        void ExcluirCompraConjuntaLocalizacao(int compraConjuntaPaginaId);
        void ExcluirCompraConjuntaLocalizacao(CompraConjunta compraconjunta);
        bool InserirRelacionamentoPagina(int compraConjuntaId, int compraConjuntaPaginaId);
        CompraConjunta CarregarCompraConjuntaEmAbertoPorProduto(Produto produto);
        int TotalComprado(CompraConjunta compraconjunta);
        CompraConjuntaDesconto CarregarCompraConjuntaDesconto(Int32 compraConjuntaId);
        List<CompraConjunta> CarregarTodasCompraConjuntaExpiradaNaoFinalizada();
        CompraConjunta CarregarCompraConjuntaValida(CompraConjunta entidade);
        List<CompraConjunta> CarregarCompraConjuntaParaFechamento();
        bool CompraConjuntaComPedidoAberto(CompraConjunta compraConjuntaBO);
	}
}