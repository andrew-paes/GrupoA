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
	public partial class RevistaGrupoAEdicao 
	{
		// Construtor
		public RevistaGrupoAEdicao() {}

		// Construtor com identificador
		public RevistaGrupoAEdicao(int revistaGrupoAEdicaoId) {
			_revistaGrupoAEdicaoId = revistaGrupoAEdicaoId;
		}

		private int _revistaGrupoAEdicaoId;
		private string _mesPublicacao;
		private string _anoPublicacao;
		private int? _numeroEdicao;
		private string _textoChamada;
		private string _urlRevista;
		private string _tituloRevista;
		private Arquivo _arquivoGrande;
		private Arquivo _arquivoPequena;

		public int RevistaGrupoAEdicaoId {
			get { return _revistaGrupoAEdicaoId; }
			set { _revistaGrupoAEdicaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string MesPublicacao {
			get { return _mesPublicacao; }
			set { _mesPublicacao = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 10)]
		public string AnoPublicacao {
			get { return _anoPublicacao; }
			set { _anoPublicacao = value; }
		}

		public int? NumeroEdicao {
			get { return _numeroEdicao; }
			set { _numeroEdicao = value; }
		}

		public string TextoChamada {
			get { return _textoChamada; }
			set { _textoChamada = value; }
		}

		public string UrlRevista {
			get { return _urlRevista; }
			set { _urlRevista = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string TituloRevista {
			get { return _tituloRevista; }
			set { _tituloRevista = value; }
		}

		public Arquivo ArquivoGrande {
			get { return _arquivoGrande; }
			set { _arquivoGrande = value; }
		}

		public Arquivo ArquivoPequena {
			get { return _arquivoPequena; }
			set { _arquivoPequena = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaGrupoAEdicao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaGrupoAEdicao>(this);
        }
	}
	
	public struct RevistaGrupoAEdicaoColunas
	{	
		public static string RevistaGrupoAEdicaoId = @"revistaGrupoAEdicaoId";
		public static string MesPublicacao = @"mesPublicacao";
		public static string AnoPublicacao = @"anoPublicacao";
		public static string NumeroEdicao = @"numeroEdicao";
		public static string ArquivoIdPequena = @"arquivoIdPequena";
		public static string TextoChamada = @"textoChamada";
		public static string UrlRevista = @"urlRevista";
		public static string ArquivoIdGrande = @"arquivoIdGrande";
		public static string TituloRevista = @"tituloRevista";
	}
}
		