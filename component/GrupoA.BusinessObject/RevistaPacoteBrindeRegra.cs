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
	public partial class RevistaPacoteBrindeRegra 
	{
		// Construtor
		public RevistaPacoteBrindeRegra() {}

		// Construtor com identificador
		public RevistaPacoteBrindeRegra(int revistaPacoteBrindeRegraId) {
			_revistaPacoteBrindeRegraId = revistaPacoteBrindeRegraId;
		}

		private int _revistaPacoteBrindeRegraId;
		private string _codigosProdutos;
		private int _quantidade;
		private RevistaPacote _revistaPacote;

		public int RevistaPacoteBrindeRegraId {
			get { return _revistaPacoteBrindeRegraId; }
			set { _revistaPacoteBrindeRegraId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string CodigosProdutos {
			get { return _codigosProdutos; }
			set { _codigosProdutos = value; }
		}

		public int Quantidade {
			get { return _quantidade; }
			set { _quantidade = value; }
		}

		[NotNullValidator]
		public RevistaPacote RevistaPacote {
			get { return _revistaPacote; }
			set { _revistaPacote = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaPacoteBrindeRegra>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaPacoteBrindeRegra>(this);
        }
	}
	
	public struct RevistaPacoteBrindeRegraColunas
	{	
		public static string RevistaPacoteBrindeRegraId = @"revistaPacoteBrindeRegraId";
		public static string RevistaPacoteId = @"revistaPacoteId";
		public static string CodigosProdutos = @"codigosProdutos";
		public static string Quantidade = @"quantidade";
	}
}
		