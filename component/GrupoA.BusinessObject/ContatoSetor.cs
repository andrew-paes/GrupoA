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
	public partial class ContatoSetor 
	{
		// Construtor
		public ContatoSetor() {}

		// Construtor com identificador
		public ContatoSetor(int contatoSetorId) {
			_contatoSetorId = contatoSetorId;
		}

		private int _contatoSetorId;
		private string _nomeSetor;
		private List<ContatoAssunto> _contatoAssuntos;

		public int ContatoSetorId {
			get { return _contatoSetorId; }
			set { _contatoSetorId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeSetor {
			get { return _nomeSetor; }
			set { _nomeSetor = value; }
		}

		public List<ContatoAssunto> ContatoAssuntos {
			get { return _contatoAssuntos; }
			set { _contatoAssuntos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ContatoSetor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ContatoSetor>(this);
        }
	}
	
	public struct ContatoSetorColunas
	{	
		public static string ContatoSetorId = @"contatoSetorId";
		public static string NomeSetor = @"nomeSetor";
	}
}
		