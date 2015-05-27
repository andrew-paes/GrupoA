using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject
{
    public partial class CarrinhoItem
    {
        private PromocaoFaixa _promocaoFaixaAplicada;
        private CompraConjuntaDesconto _compraConjuntaDesconto;

        public CompraConjuntaDesconto CompraConjuntaDesconto
        {
            get { return _compraConjuntaDesconto; }
            set { _compraConjuntaDesconto = value; }
        }

        public PromocaoFaixa PromocaoFaixaAplicada
        {
            get { return _promocaoFaixaAplicada; }
            set { _promocaoFaixaAplicada = value; }
        }

        public bool FreteGratis
        {
            get
            {
                return _promocaoFaixaAplicada != null && _promocaoFaixaAplicada.FreteGratis;
            }
        }

        /// <summary>
        /// Retorna o valor final com percentual de desconto aplicado por promoção ou compra conjunta.
        /// </summary>
        public decimal ValorUnitarioDoItem
        {
            get
            {
                decimal valorUnitario = (Produto.ValorOferta.HasValue && Produto.ValorOferta > 0) ? Produto.ValorOferta.Value : Produto.ValorUnitario;

                if (PromocaoFaixaAplicada != null)
                {
                    if (PromocaoFaixaAplicada.PercentualDesconto.HasValue)
                    {
                        valorUnitario -= valorUnitario * (PromocaoFaixaAplicada.PercentualDesconto.Value / 100);
                    }
                    else if (PromocaoFaixaAplicada.ValorDesconto.HasValue)
                    {
                        valorUnitario -= (PromocaoFaixaAplicada.ValorDesconto.Value / Quantidade);
                    }
                }

                if (CompraConjuntaDesconto != null)
                {
                    valorUnitario -= valorUnitario * (CompraConjuntaDesconto.PercentualDesconto / 100);
                }

                return valorUnitario;
            }
        }

        public object Clone()
        {
            CarrinhoItem clone = new CarrinhoItem
            {
                Carrinho = this.Carrinho,
                Produto = this.Produto,
                CarrinhoItemCompraConjunta = this.CarrinhoItemCompraConjunta,
                CarrinhoItemId = this.CarrinhoItemId,
                CompraConjuntaDesconto = this.CompraConjuntaDesconto,
                PromocaoFaixaAplicada = this.PromocaoFaixaAplicada,
                Quantidade = this.Quantidade
            };

            return clone;
        }
    }
}
