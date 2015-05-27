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
	public partial class PedidoEndereco 
	{
		// Construtor
		public PedidoEndereco() {}

		// Construtor com identificador
		public PedidoEndereco(int pedidoEnderecoId) {
			_pedidoEnderecoId = pedidoEnderecoId;
		}

		private int _pedidoEnderecoId;
		private string _bairro;
		private string _cep;
		private string _complemento;
		private string _logradouro;
		private string _numero;
		private EnderecoTipo _enderecoTipo;
		private Municipio _municipio;
		private Pedido _pedido;

		public int PedidoEnderecoId {
			get { return _pedidoEnderecoId; }
			set { _pedidoEnderecoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string Bairro {
			get { return _bairro; }
			set { _bairro = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 20)]
		public string Cep {
			get { return _cep; }
			set { _cep = value; }
		}

		public string Complemento {
			get { return _complemento; }
			set { _complemento = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string Logradouro {
			get { return _logradouro; }
			set { _logradouro = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Numero {
			get { return _numero; }
			set { _numero = value; }
		}

		[NotNullValidator]
		public EnderecoTipo EnderecoTipo {
			get { return _enderecoTipo; }
			set { _enderecoTipo = value; }
		}

		[NotNullValidator]
		public Municipio Municipio {
			get { return _municipio; }
			set { _municipio = value; }
		}

		[NotNullValidator]
		public Pedido Pedido {
			get { return _pedido; }
			set { _pedido = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoEndereco>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoEndereco>(this);
        }
	}
	
	public struct PedidoEnderecoColunas
	{	
		public static string PedidoEnderecoId = @"pedidoEnderecoId";
		public static string PedidoId = @"pedidoId";
		public static string EnderecoTipoId = @"enderecoTipoId";
		public static string MunicipioId = @"municipioId";
		public static string Bairro = @"bairro";
		public static string Cep = @"cep";
		public static string Complemento = @"complemento";
		public static string Logradouro = @"logradouro";
		public static string Numero = @"numero";
	}
}
		