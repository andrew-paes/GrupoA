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
	public partial class ContatoAssunto 
	{
		// Construtor
		public ContatoAssunto() {}

		// Construtor com identificador
		public ContatoAssunto(int contatoAssuntoId) {
			_contatoAssuntoId = contatoAssuntoId;
		}

		private int _contatoAssuntoId;
		private string _nomeAssunto;
		private List<ContatoResponsavel> _contatoResponsaveis;
		private ContatoSetor _contatoSetor;

		public int ContatoAssuntoId {
			get { return _contatoAssuntoId; }
			set { _contatoAssuntoId = value; }
		}

		public string NomeAssunto {
			get { return _nomeAssunto; }
			set { _nomeAssunto = value; }
		}

		public List<ContatoResponsavel> ContatoResponsaveis {
			get { return _contatoResponsaveis; }
			set { _contatoResponsaveis = value; }
		}

		[NotNullValidator]
		public ContatoSetor ContatoSetor {
			get { return _contatoSetor; }
			set { _contatoSetor = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<ContatoAssunto>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ContatoAssunto>(this);
        }
	}
	
	public struct ContatoAssuntoColunas
	{	
		public static string ContatoAssuntoId = @"contatoAssuntoId";
		public static string ContatoSetorId = @"contatoSetorId";
		public static string NomeAssunto = @"nomeAssunto";
	}
}
		