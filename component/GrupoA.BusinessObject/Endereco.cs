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
	public partial class Endereco 
	{
		// Construtor
		public Endereco() {}

		// Construtor com identificador
		public Endereco(int enderecoId) {
			_enderecoId = enderecoId;
		}

		private int _enderecoId;
		private bool _preferencialParaEntrega;
		private string _logradouro;
		private string _bairro;
		private string _cep;
		private string _complemento;
		private string _numero;
		private string _nomeEndereco;
		private EnderecoTipo _enderecoTipo;
		private Municipio _municipio;
		private Usuario _usuario;

		public int EnderecoId {
			get { return _enderecoId; }
			set { _enderecoId = value; }
		}

		public bool PreferencialParaEntrega {
			get { return _preferencialParaEntrega; }
			set { _preferencialParaEntrega = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string Logradouro {
			get { return _logradouro; }
			set { _logradouro = value; }
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
		[StringLengthValidator(0, 50)]
		public string Numero {
			get { return _numero; }
			set { _numero = value; }
		}

		public string NomeEndereco {
			get { return _nomeEndereco; }
			set { _nomeEndereco = value; }
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
		public Usuario Usuario {
			get { return _usuario; }
			set { _usuario = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Endereco>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Endereco>(this);
        }
	}
	
	public struct EnderecoColunas
	{	
		public static string EnderecoId = @"enderecoId";
		public static string MunicipioId = @"municipioId";
		public static string EnderecoTipoId = @"enderecoTipoId";
		public static string UsuarioId = @"usuarioId";
		public static string PreferencialParaEntrega = @"preferencialParaEntrega";
		public static string Logradouro = @"logradouro";
		public static string Bairro = @"bairro";
		public static string Cep = @"cep";
		public static string Complemento = @"complemento";
		public static string Numero = @"numero";
		public static string NomeEndereco = @"nomeEndereco";
	}
}
		