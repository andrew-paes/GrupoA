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
	public partial class Municipio 
	{
		// Construtor
		public Municipio() {}

		// Construtor com identificador
		public Municipio(int municipioId) {
			_municipioId = municipioId;
		}

		private int _municipioId;
		private string _nomeMunicipio;
		private int? _codigoIbge;
		private List<Endereco> _enderecos;
		private List<PedidoEndereco> _pedidoEnderecos;
		private Regiao _regiao;

		public int MunicipioId {
			get { return _municipioId; }
			set { _municipioId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string NomeMunicipio {
			get { return _nomeMunicipio; }
			set { _nomeMunicipio = value; }
		}

		public int? CodigoIbge {
			get { return _codigoIbge; }
			set { _codigoIbge = value; }
		}

		public List<Endereco> Enderecos {
			get { return _enderecos; }
			set { _enderecos = value; }
		}

		public List<PedidoEndereco> PedidoEnderecos {
			get { return _pedidoEnderecos; }
			set { _pedidoEnderecos = value; }
		}

		[NotNullValidator]
		public Regiao Regiao {
			get { return _regiao; }
			set { _regiao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Municipio>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Municipio>(this);
        }
	}
	
	public struct MunicipioColunas
	{	
		public static string MunicipioId = @"municipioId";
		public static string NomeMunicipio = @"nomeMunicipio";
		public static string RegiaoId = @"regiaoId";
		public static string CodigoIbge = @"codigoIbge";
	}
}
		