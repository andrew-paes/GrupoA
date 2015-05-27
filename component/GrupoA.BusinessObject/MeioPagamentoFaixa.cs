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
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{	
	
	[Serializable]
	public partial class MeioPagamentoFaixa 
	{
		// Construtor
		public MeioPagamentoFaixa() {}

		// Construtor com identificador
		public MeioPagamentoFaixa(int meioPagamentoFaixaId) {
			_meioPagamentoFaixaId = meioPagamentoFaixaId;
		}

		private int _meioPagamentoFaixaId;
		private decimal _valorMinimo;
		private int _numeroParcelas;
		private string _codigoGatewayFaixa;
		private string _codigoLegado;
		private decimal _taxaJuros;
		private MeioPagamento _meioPagamento;

		public int MeioPagamentoFaixaId {
			get { return _meioPagamentoFaixaId; }
			set { _meioPagamentoFaixaId = value; }
		}

		public decimal ValorMinimo {
			get { return _valorMinimo; }
			set { _valorMinimo = value; }
		}

		public int NumeroParcelas {
			get { return _numeroParcelas; }
			set { _numeroParcelas = value; }
		}

		public string CodigoGatewayFaixa {
			get { return _codigoGatewayFaixa; }
			set { _codigoGatewayFaixa = value; }
		}

		public string CodigoLegado {
			get { return _codigoLegado; }
			set { _codigoLegado = value; }
		}

		public decimal TaxaJuros {
			get { return _taxaJuros; }
			set { _taxaJuros = value; }
		}

		[NotNullValidator]
		public MeioPagamento MeioPagamento {
			get { return _meioPagamento; }
			set { _meioPagamento = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<MeioPagamentoFaixa>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<MeioPagamentoFaixa>(this);
        }
	}
	
	public struct MeioPagamentoFaixaColunas
	{	
		public static string MeioPagamentoFaixaId = @"meioPagamentoFaixaId";
		public static string MeioPagamentoId = @"meioPagamentoId";
		public static string ValorMinimo = @"valorMinimo";
		public static string NumeroParcelas = @"numeroParcelas";
		public static string CodigoGatewayFaixa = @"codigoGatewayFaixa";
		public static string CodigoLegado = @"codigoLegado";
		public static string TaxaJuros = @"taxaJuros";
	}
}
		