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
	public partial class CursoPanamericano 
	{
		// Construtor
		public CursoPanamericano() {}

		// Construtor com identificador
		public CursoPanamericano(int cursoPanamericanoId) {
			_cursoPanamericanoId = cursoPanamericanoId;
		}

		private int _cursoPanamericanoId;
		private string _titulo;
		private string _subtitulo;
		private string _descricao;
		private string _urlLink;
		private bool _targetBlank;
		private List<Categoria> _categorias;
		private Arquivo _arquivoImagem;

		public int CursoPanamericanoId {
			get { return _cursoPanamericanoId; }
			set { _cursoPanamericanoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string Subtitulo {
			get { return _subtitulo; }
			set { _subtitulo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string Descricao {
			get { return _descricao; }
			set { _descricao = value; }
		}

		public string UrlLink {
			get { return _urlLink; }
			set { _urlLink = value; }
		}

		public bool TargetBlank {
			get { return _targetBlank; }
			set { _targetBlank = value; }
		}

		public List<Categoria> Categorias {
			get { return _categorias; }
			set { _categorias = value; }
		}

		public Arquivo ArquivoImagem {
			get { return _arquivoImagem; }
			set { _arquivoImagem = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CursoPanamericano>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CursoPanamericano>(this);
        }
	}
	
	public struct CursoPanamericanoColunas
	{	
		public static string CursoPanamericanoId = @"cursoPanamericanoId";
		public static string Titulo = @"titulo";
		public static string Subtitulo = @"subtitulo";
		public static string Descricao = @"descricao";
		public static string ArquivoIdImagem = @"arquivoIdImagem";
		public static string UrlLink = @"urlLink";
		public static string TargetBlank = @"targetBlank";
	}
}
		