using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
    public partial class CarrinhoItemVH
    {
        private int _carrinhoId = 0;
        private int _produtoId;
        private string _nomeProduto;
        private string _tipo;
        private decimal _valorUnitario;
        private decimal _valorOferta;
        private DateTime _dataLancamento;
        private string _nomeArquivo;
        private decimal _valor;
        private int _parcelas;
        private string _autores;
        private bool exibirSite;
        private bool disponivel;
        private decimal quantidade;
        private int _carrinhoItemId;
        private decimal _peso;
        private bool utilizaFrete;
        private int _carrinhoItemCompraConjuntaId;
        private int _compraConjuntaId;
        private int _areaId;
        private int _categoriaId;
        private int _tituloId;
        private int _produtoIdPai;
        private int _produtoTipoId;
        private decimal _percentualDesconto = 0;
        private decimal _valorDesconto = 0;
        private Promocao _promocao;

        public int CarrinhoId
        {
            get { return _carrinhoId; }
            set { _carrinhoId = value; }
        }

        public int ProdutoId
        {
            get { return _produtoId; }
            set { _produtoId = value; }
        }

        public string NomeProduto
        {
            get { return _nomeProduto; }
            set { _nomeProduto = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public decimal ValorUnitario
        {
            get { return _valorUnitario; }
            set { _valorUnitario = value; }
        }

        public decimal ValorOferta
        {
            get { return _valorOferta; }
            set { _valorOferta = value; }
        }

        public DateTime DataLancamento
        {
            get { return _dataLancamento; }
            set { _dataLancamento = value; }
        }

        public string NomeArquivo
        {
            get { return _nomeArquivo; }
            set { _nomeArquivo = value; }
        }

        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public int Parcelas
        {
            get { return _parcelas; }
            set { _parcelas = value; }
        }

        public string Autores
        {
            get { return _autores; }
            set { _autores = value; }
        }

        public bool ExibirSite
        {
            get { return exibirSite; }
            set { exibirSite = value; }
        }

        public bool Disponivel
        {
            get { return disponivel; }
            set { disponivel = value; }
        }

        public decimal Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public decimal ValorFinal
        {
            get
            {
                decimal valorBase;
                if (ValorOferta > 0)
                {
                    valorBase = ValorOferta;

                }
                else
                {
                    valorBase = ValorUnitario;
                }

                if (Promocao != null && Promocao.PromocaoFaixas != null && Promocao.PromocaoFaixas.Count > 0)
                {
                    foreach (PromocaoFaixa promocaoFaixa in Promocao.PromocaoFaixas)
                    {
                        if (promocaoFaixa.PercentualDesconto.HasValue && promocaoFaixa.PercentualDesconto.Value > 0)
                        {
                            valorBase = valorBase * ((100 - promocaoFaixa.PercentualDesconto.Value) / 100);
                        }
                        else if (promocaoFaixa.ValorDesconto.HasValue && promocaoFaixa.ValorDesconto.Value > 0)
                        {
                            valorBase = valorBase - (promocaoFaixa.ValorDesconto.Value / Quantidade);
                        }
                    }
                }


                return valorBase * Quantidade;
            }
        }

        public int CarrinhoItemId
        {
            get { return _carrinhoItemId; }
            set { _carrinhoItemId = value; }
        }

        public decimal Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        public bool UtilizaFrete
        {
            get { return utilizaFrete; }
            set { utilizaFrete = value; }
        }

        public int CarrinhoItemCompraConjuntaId
        {
            get { return _carrinhoItemCompraConjuntaId; }
            set { _carrinhoItemCompraConjuntaId = value; }
        }

        public int CompraConjuntaId
        {
            get { return _compraConjuntaId; }
            set { _compraConjuntaId = value; }
        }

        public int AreaId
        {
            get { return _areaId; }
            set { _areaId = value; }
        }

        public int CategoriaId
        {
            get { return _categoriaId; }
            set { _categoriaId = value; }
        }

        public int TituloId
        {
            get { return _tituloId; }
            set { _tituloId = value; }
        }

        public int ProdutoIdPai
        {
            get { return _produtoIdPai; }
            set { _produtoIdPai = value; }
        }

        public int ProdutoTipoId
        {
            get { return _produtoTipoId; }
            set { _produtoTipoId = value; }
        }

        public decimal PercentualDesconto
        {
            get { return _percentualDesconto; }
            set { _percentualDesconto = value; }
        }

        public decimal ValorDesconto
        {
            get { return _valorDesconto; }
            set { _valorDesconto = value; }
        }

        public Promocao Promocao
        {
            get { return _promocao; }
            set { _promocao = value; }
        }
    }
}
