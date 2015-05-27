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
	public partial class AvisoDisponibilidadeStatus 
	{
		// Construtor
		public AvisoDisponibilidadeStatus() {}

		// Construtor com identificador
		public AvisoDisponibilidadeStatus(int avisoDisponibilidadeStatusId) {
			_avisoDisponibilidadeStatusId = avisoDisponibilidadeStatusId;
		}

		private int _avisoDisponibilidadeStatusId;
		private string _statusAviso;
		private List<AvisoDisponibilidade> _avisoDisponibilidades;

		public int AvisoDisponibilidadeStatusId {
			get { return _avisoDisponibilidadeStatusId; }
			set { _avisoDisponibilidadeStatusId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string StatusAviso {
			get { return _statusAviso; }
			set { _statusAviso = value; }
		}

		public List<AvisoDisponibilidade> AvisoDisponibilidades {
			get { return _avisoDisponibilidades; }
			set { _avisoDisponibilidades = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<AvisoDisponibilidadeStatus>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<AvisoDisponibilidadeStatus>(this);
        }
	}
	
	public struct AvisoDisponibilidadeStatusColunas
	{	
		public static string AvisoDisponibilidadeStatusId = @"avisoDisponibilidadeStatusId";
		public static string StatusAviso = @"statusAviso";
	}
}
		