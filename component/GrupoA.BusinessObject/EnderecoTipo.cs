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
	public partial class EnderecoTipo 
	{
		// Construtor
		public EnderecoTipo() {}

		// Construtor com identificador
		public EnderecoTipo(int enderecoTipoId) {
			_enderecoTipoId = enderecoTipoId;
		}

		private int _enderecoTipoId;
		private string _tipo;
		private List<Endereco> _enderecos;
		private List<PedidoEndereco> _pedidoEnderecos;

		public int EnderecoTipoId {
			get { return _enderecoTipoId; }
			set { _enderecoTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Tipo {
			get { return _tipo; }
			set { _tipo = value; }
		}

		public List<Endereco> Enderecos {
			get { return _enderecos; }
			set { _enderecos = value; }
		}

		public List<PedidoEndereco> PedidoEnderecos {
			get { return _pedidoEnderecos; }
			set { _pedidoEnderecos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<EnderecoTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<EnderecoTipo>(this);
        }
	}
	
	public struct EnderecoTipoColunas
	{	
		public static string EnderecoTipoId = @"enderecoTipoId";
		public static string Tipo = @"tipo";
	}
}
		