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
	public partial class Transportadora 
	{
		// Construtor
		public Transportadora() {}

		// Construtor com identificador
		public Transportadora(int transportadoraId) {
			_transportadoraId = transportadoraId;
		}

		private int _transportadoraId;
		private string _nomeTransportadora;
		private List<TransportadoraServico> _transportadoraServicos;

		public int TransportadoraId {
			get { return _transportadoraId; }
			set { _transportadoraId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeTransportadora {
			get { return _nomeTransportadora; }
			set { _nomeTransportadora = value; }
		}

		public List<TransportadoraServico> TransportadoraServicos {
			get { return _transportadoraServicos; }
			set { _transportadoraServicos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Transportadora>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Transportadora>(this);
        }
	}
	
	public struct TransportadoraColunas
	{	
		public static string TransportadoraId = @"transportadoraId";
		public static string NomeTransportadora = @"nomeTransportadora";
	}
}
		