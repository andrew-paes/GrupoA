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
	public partial class Selo 
	{
		// Construtor
		public Selo() {}

		// Construtor com identificador
		public Selo(int seloId) {
			_seloId = seloId;
		}

		private int _seloId;
		private string _nomeSelo;
		private List<Produto> _produtos;

		public int SeloId {
			get { return _seloId; }
			set { _seloId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeSelo {
			get { return _nomeSelo; }
			set { _nomeSelo = value; }
		}

		public List<Produto> Produtos {
			get { return _produtos; }
			set { _produtos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Selo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Selo>(this);
        }
	}
	
	public struct SeloColunas
	{	
		public static string SeloId = @"seloId";
		public static string NomeSelo = @"nomeSelo";
	}
}
		