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
	public partial class ConteudoTipo 
	{
		// Construtor
		public ConteudoTipo() {}

		// Construtor com identificador
		public ConteudoTipo(int conteudoTipoId) {
			_conteudoTipoId = conteudoTipoId;
		}

		private int _conteudoTipoId;
		private string _tipo;
		private List<Conteudo> _conteudos;

		public int ConteudoTipoId {
			get { return _conteudoTipoId; }
			set { _conteudoTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Tipo {
			get { return _tipo; }
			set { _tipo = value; }
		}

		public List<Conteudo> Conteudos {
			get { return _conteudos; }
			set { _conteudos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ConteudoTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ConteudoTipo>(this);
        }
	}
	
	public struct ConteudoTipoColunas
	{	
		public static string ConteudoTipoId = @"conteudoTipoId";
		public static string Tipo = @"tipo";
	}
}
		