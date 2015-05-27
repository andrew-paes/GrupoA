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
	public partial class TelefoneTipo 
	{
		// Construtor
		public TelefoneTipo() {}

		// Construtor com identificador
		public TelefoneTipo(int telefoneTipoId) {
			_telefoneTipoId = telefoneTipoId;
		}

		private int _telefoneTipoId;
		private string _tipoTelefone;
		private List<Telefone> _telefones;

		public int TelefoneTipoId {
			get { return _telefoneTipoId; }
			set { _telefoneTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string TipoTelefone {
			get { return _tipoTelefone; }
			set { _tipoTelefone = value; }
		}

		public List<Telefone> Telefones {
			get { return _telefones; }
			set { _telefones = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TelefoneTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TelefoneTipo>(this);
        }
	}
	
	public struct TelefoneTipoColunas
	{	
		public static string TelefoneTipoId = @"telefoneTipoId";
		public static string TipoTelefone = @"tipoTelefone";
	}
}
		