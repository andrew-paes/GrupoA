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
	public partial class RevistaPacote 
	{
		// Construtor
		public RevistaPacote() {}

		// Construtor com identificador
		public RevistaPacote(int revistaPacoteId) {
			_revistaPacoteId = revistaPacoteId;
		}

		private int _revistaPacoteId;
		private string _nome;
		private List<Produto> _produtos;
		private List<RevistaPacoteBrindeRegra> _revistaPacoteBrindeRegras;

		public int RevistaPacoteId {
			get { return _revistaPacoteId; }
			set { _revistaPacoteId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Nome {
			get { return _nome; }
			set { _nome = value; }
		}

		public List<Produto> Produtos {
			get { return _produtos; }
			set { _produtos = value; }
		}

		public List<RevistaPacoteBrindeRegra> RevistaPacoteBrindeRegras {
			get { return _revistaPacoteBrindeRegras; }
			set { _revistaPacoteBrindeRegras = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaPacote>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaPacote>(this);
        }
	}
	
	public struct RevistaPacoteColunas
	{	
		public static string RevistaPacoteId = @"revistaPacoteId";
		public static string Nome = @"nome";
	}
}
		