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
	public partial class RevistaSecao 
	{
		// Construtor
		public RevistaSecao() {}

		// Construtor com identificador
		public RevistaSecao(int revistaSecaoId) {
			_revistaSecaoId = revistaSecaoId;
		}

		private int _revistaSecaoId;
		private string _nomeSecao;
		private List<RevistaArtigo> _revistaArtigos;
		private Revista _revista;

		public int RevistaSecaoId {
			get { return _revistaSecaoId; }
			set { _revistaSecaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeSecao {
			get { return _nomeSecao; }
			set { _nomeSecao = value; }
		}

		public List<RevistaArtigo> RevistaArtigos {
			get { return _revistaArtigos; }
			set { _revistaArtigos = value; }
		}

		[NotNullValidator]
		public Revista Revista {
			get { return _revista; }
			set { _revista = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaSecao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaSecao>(this);
        }
	}
	
	public struct RevistaSecaoColunas
	{	
		public static string RevistaSecaoId = @"revistaSecaoId";
		public static string NomeSecao = @"nomeSecao";
		public static string RevistaId = @"revistaId";
	}
}
		