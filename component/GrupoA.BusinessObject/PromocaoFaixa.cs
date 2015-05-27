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
	public partial class PromocaoFaixa 
	{
		// Construtor
		public PromocaoFaixa() {}

		// Construtor com identificador
		public PromocaoFaixa(int promocaoFaixaId) {
			_promocaoFaixaId = promocaoFaixaId;
		}

		private int _promocaoFaixaId;
		private decimal _valorMinimo;
		private decimal? _percentualDesconto;
		private decimal? _valorDesconto;
		private bool _freteGratis;
		private Promocao _promocao;

		public int PromocaoFaixaId {
			get { return _promocaoFaixaId; }
			set { _promocaoFaixaId = value; }
		}

		public decimal ValorMinimo {
			get { return _valorMinimo; }
			set { _valorMinimo = value; }
		}

		public decimal? PercentualDesconto {
			get { return _percentualDesconto; }
			set { _percentualDesconto = value; }
		}

		public decimal? ValorDesconto {
			get { return _valorDesconto; }
			set { _valorDesconto = value; }
		}

		public bool FreteGratis {
			get { return _freteGratis; }
			set { _freteGratis = value; }
		}

		[NotNullValidator]
		public Promocao Promocao {
			get { return _promocao; }
			set { _promocao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PromocaoFaixa>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PromocaoFaixa>(this);
        }
	}
	
	public struct PromocaoFaixaColunas
	{	
		public static string PromocaoFaixaId = @"promocaoFaixaId";
		public static string PromocaoId = @"promocaoId";
		public static string ValorMinimo = @"valorMinimo";
		public static string PercentualDesconto = @"percentualDesconto";
		public static string ValorDesconto = @"valorDesconto";
		public static string FreteGratis = @"freteGratis";
	}
}
		