using System.Collections.Generic;
using System;

namespace GrupoA.BusinessObject
{

  	[Serializable]
	public partial class ProdutoListaVH 
	{
        private int _produtoId;
        private int _tituloId;
        private string _nomeProduto;
        private string _tipo;
        private decimal _valorUnitario;
        private decimal? _valorOferta;
        private decimal? _valor;
        private DateTime? _dataLancamento;
		private List<Autor> _autores;
        private string _nomeArquivo;
        private int  _parcelas;
        private decimal _taxaJuros = 0;
        private int _tituloImagemTipoId = 1;
        private Boolean _disponivel;
        
        public int ProdutoId
        {
            get { return _produtoId; }
            set { _produtoId = value; }
        }

        public int TituloId
        {
			get { return _tituloId; }
			set { _tituloId = value; }
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

        public decimal? ValorOferta
        {
            get { return _valorOferta; }
            set { _valorOferta = value; }
        }

        public decimal? Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public DateTime? DataLancamento
        {
            get { return _dataLancamento; }
            set { _dataLancamento = value; }
        }

        public List<Autor> Autores
        {
            get { return _autores; }
            set { _autores = value; }
        }

        public string NomeArquivo
        {
            get { return _nomeArquivo; }
            set { _nomeArquivo = value; }
		}

        public int Parcelas
        {
            get { return _parcelas; }
            set { _parcelas = value; }
        }

        public Decimal TaxaJuros
        {
            get { return _taxaJuros; }
            set { _taxaJuros = value; }
        }

         public int TituloImagemTipoId
        {
            get { return _parcelas; }
            set { _parcelas = value; }
        }

         public Boolean Disponivel
         {
             get { return _disponivel; }
             set { _disponivel = value; }
         }
	}
}
