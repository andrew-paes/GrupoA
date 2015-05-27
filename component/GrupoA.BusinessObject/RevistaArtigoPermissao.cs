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
	public partial class RevistaArtigoPermissao 
	{
		// Construtor
		public RevistaArtigoPermissao() {}

		// Construtor com identificador
		public RevistaArtigoPermissao(int revistaArtigoPermissaoId) {
			_revistaArtigoPermissaoId = revistaArtigoPermissaoId;
		}

		private int _revistaArtigoPermissaoId;
		private string _permissao;
		private List<RevistaArtigo> _revistaArtigos;

		public int RevistaArtigoPermissaoId {
			get { return _revistaArtigoPermissaoId; }
			set { _revistaArtigoPermissaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string Permissao {
			get { return _permissao; }
			set { _permissao = value; }
		}

		public List<RevistaArtigo> RevistaArtigos {
			get { return _revistaArtigos; }
			set { _revistaArtigos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaArtigoPermissao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaArtigoPermissao>(this);
        }
	}
	
	public struct RevistaArtigoPermissaoColunas
	{	
		public static string RevistaArtigoPermissaoId = @"revistaArtigoPermissaoId";
		public static string Permissao = @"permissao";
	}
}
		