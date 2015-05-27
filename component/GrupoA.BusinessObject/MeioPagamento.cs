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
	public partial class MeioPagamento 
	{
		// Construtor
		public MeioPagamento() {}

		// Construtor com identificador
		public MeioPagamento(int meioPagamentoId) {
			_meioPagamentoId = meioPagamentoId;
		}

		private int _meioPagamentoId;
		private string _nome;
		private bool _ativo;
		private string _codigoLegado;
        private string _codigoGateway;
		private List<MeioPagamentoFaixa> _meioPagamentoFaixas;
		private List<Pagamento> _pagamentos;

		public int MeioPagamentoId {
			get { return _meioPagamentoId; }
			set { _meioPagamentoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Nome {
			get { return _nome; }
			set { _nome = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public string CodigoLegado {
			get { return _codigoLegado; }
			set { _codigoLegado = value; }
		}

        public string CodigoGateway
        {
            get { return _codigoGateway; }
            set { _codigoGateway = value; }
        }

		public List<MeioPagamentoFaixa> MeioPagamentoFaixas {
			get { return _meioPagamentoFaixas; }
			set { _meioPagamentoFaixas = value; }
		}

		public List<Pagamento> Pagamentos {
			get { return _pagamentos; }
			set { _pagamentos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<MeioPagamento>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<MeioPagamento>(this);
        }
	}
	
	public struct MeioPagamentoColunas
	{	
		public static string MeioPagamentoId = @"meioPagamentoId";
		public static string Nome = @"nome";
		public static string Ativo = @"ativo";
		public static string CodigoLegado = @"codigoLegado";
        public static string CodigoGateway = @"codigoGateway";
	}
}
		