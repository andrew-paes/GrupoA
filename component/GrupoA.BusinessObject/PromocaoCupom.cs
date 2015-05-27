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
	public partial class PromocaoCupom 
	{
		// Construtor
		public PromocaoCupom() {}

		// Construtor com identificador
		public PromocaoCupom(int promocaoCupomId) {
			_promocaoCupomId = promocaoCupomId;
		}

		private int _promocaoCupomId;
		private Guid _codigoCupom;
		private bool _reutilizavel;
		private string _codigoAmigavel;
		private List<PromocaoCupomPedido> _promocaoCupomPedidos;
		private Promocao _promocao;

		public int PromocaoCupomId {
			get { return _promocaoCupomId; }
			set { _promocaoCupomId = value; }
		}

		public Guid CodigoCupom {
			get { return _codigoCupom; }
			set { _codigoCupom = value; }
		}

		public bool Reutilizavel {
			get { return _reutilizavel; }
			set { _reutilizavel = value; }
		}

		public string CodigoAmigavel {
			get { return _codigoAmigavel; }
			set { _codigoAmigavel = value; }
		}

		public List<PromocaoCupomPedido> PromocaoCupomPedidos {
			get { return _promocaoCupomPedidos; }
			set { _promocaoCupomPedidos = value; }
		}

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
            get { return Validation.Validate<PromocaoCupom>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PromocaoCupom>(this);
        }
	}
	
	public struct PromocaoCupomColunas
	{	
		public static string PromocaoCupomId = @"promocaoCupomId";
		public static string PromocaoId = @"promocaoId";
		public static string CodigoCupom = @"codigoCupom";
		public static string Reutilizavel = @"reutilizavel";
		public static string CodigoAmigavel = @"codigoAmigavel";
	}
}
		