using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
	public partial class PedidoItemVH
	{
		private int _produtoId;

		public int ProdutoId
		{
			get { return _produtoId; }
			set { _produtoId = value; }
		}
		private string _nomeProduto;

		public string NomeProduto
		{
			get { return _nomeProduto; }
			set { _nomeProduto = value; }
		}
		private string _tipo;

		public string Tipo
		{
			get { return _tipo; }
			set { _tipo = value; }
		}
		private decimal _valorUnitario;
		public decimal ValorUnitario
		{
			get { return _valorUnitario; }
			set { _valorUnitario = value; }
		}

		private decimal _valorFinal;

        public decimal ValorFinal
		{
            get { return _valorFinal; }
            set { _valorFinal = value; }
		}
		private string _nomeArquivo;

		public string NomeArquivo
		{
			get { return _nomeArquivo; }
			set { _nomeArquivo = value; }
		}
	
		private string _autores;
		public string Autores
		{
			get { return _autores; }
			set { _autores = value; }
		}

		
		private decimal quantidade;

		public decimal Quantidade
		{
			get { return quantidade; }
			set { quantidade = value; }
		}
		

        private int _carrinhoItemCompraConjuntaId;

        public int CarrinhoItemCompraConjuntaId
        {
            get { return _carrinhoItemCompraConjuntaId; }
            set { _carrinhoItemCompraConjuntaId = value; }
        }

        private int _compraConjuntaId;

        public int CompraConjuntaId
        {
            get { return _compraConjuntaId; }
            set { _compraConjuntaId = value; }
        }
		
	}
}
