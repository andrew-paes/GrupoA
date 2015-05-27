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
	public partial class RevistaArtigo 
	{
		private int _revistaArtigoId;
		private string _tituloArtigo;
		private string _subTituloArtigo;
		private string _resumo;
		private string _textoArtigo;
		private string _autores;
		private string _bibliografia;
		private bool _destaquePrincipal;
		private bool _destaqueHome;
		private bool _conteudoOnline;
		private bool _ativo;
		private DateTime _dataPublicacao;
		private RevistaArtigo _revistaArtigoAssociado;
		private List<RevistaArtigoControversia> _revistaArtigoControversias;
		private List<Arquivo> _arquivos;
		private List<Produto> _produtos;
		private Arquivo _arquivoThumbM;
		private Arquivo _arquivoCapa;
		private Arquivo _arquivoThumbP;
		private Arquivo _arquivoLateral;
		private Conteudo _conteudo;
		private RevistaArtigoPermissao _revistaArtigoPermissao;
		private RevistaEdicao _revistaEdicao;
		private RevistaSecao _revistaSecao;

		public int RevistaArtigoId {
			get { return _revistaArtigoId; }
			set { _revistaArtigoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string TituloArtigo {
			get { return _tituloArtigo; }
			set { _tituloArtigo = value; }
		}

		public string SubTituloArtigo {
			get { return _subTituloArtigo; }
			set { _subTituloArtigo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string Resumo {
			get { return _resumo; }
			set { _resumo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string TextoArtigo {
			get { return _textoArtigo; }
			set { _textoArtigo = value; }
		}

		public string Autores {
			get { return _autores; }
			set { _autores = value; }
		}

		public string Bibliografia {
			get { return _bibliografia; }
			set { _bibliografia = value; }
		}

		public bool DestaquePrincipal {
			get { return _destaquePrincipal; }
			set { _destaquePrincipal = value; }
		}

		public bool DestaqueHome {
			get { return _destaqueHome; }
			set { _destaqueHome = value; }
		}

		public bool ConteudoOnline {
			get { return _conteudoOnline; }
			set { _conteudoOnline = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		[NotNullValidator]
		public DateTime DataPublicacao {
			get { return _dataPublicacao; }
			set { _dataPublicacao = value; }
		}

		public RevistaArtigo RevistaArtigoAssociado {
			get { return _revistaArtigoAssociado; }
			set { _revistaArtigoAssociado = value; }
		}

		public List<RevistaArtigoControversia> RevistaArtigoControversias {
			get { return _revistaArtigoControversias; }
			set { _revistaArtigoControversias = value; }
		}

		public List<Arquivo> Arquivos {
			get { return _arquivos; }
			set { _arquivos = value; }
		}

		public List<Produto> Produtos {
			get { return _produtos; }
			set { _produtos = value; }
		}

		public Arquivo ArquivoThumbM {
			get { return _arquivoThumbM; }
			set { _arquivoThumbM = value; }
		}

		public Arquivo ArquivoCapa {
			get { return _arquivoCapa; }
			set { _arquivoCapa = value; }
		}

		public Arquivo ArquivoThumbP {
			get { return _arquivoThumbP; }
			set { _arquivoThumbP = value; }
		}

		public Arquivo ArquivoLateral {
			get { return _arquivoLateral; }
			set { _arquivoLateral = value; }
		}

		[NotNullValidator]
		public Conteudo Conteudo {
			get { return _conteudo; }
			set { _conteudo = value; }
		}

		[NotNullValidator]
		public RevistaArtigoPermissao RevistaArtigoPermissao {
			get { return _revistaArtigoPermissao; }
			set { _revistaArtigoPermissao = value; }
		}

		public RevistaEdicao RevistaEdicao {
			get { return _revistaEdicao; }
			set { _revistaEdicao = value; }
		}

		[NotNullValidator]
		public RevistaSecao RevistaSecao {
			get { return _revistaSecao; }
			set { _revistaSecao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<RevistaArtigo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaArtigo>(this);
        }
	}
	
	public struct RevistaArtigoColunas
	{	
		public static string RevistaArtigoId = @"revistaArtigoId";
		public static string RevistaEdicaoId = @"revistaEdicaoId";
		public static string TituloArtigo = @"tituloArtigo";
		public static string SubTituloArtigo = @"subTituloArtigo";
		public static string Resumo = @"resumo";
		public static string TextoArtigo = @"textoArtigo";
		public static string Autores = @"autores";
		public static string RevistaSecaoId = @"revistaSecaoId";
		public static string RevistaArtigoPermissaoId = @"revistaArtigoPermissaoId";
		public static string ArquivoIdThumbP = @"arquivoIdThumbP";
		public static string ArquivoIdThumbM = @"arquivoIdThumbM";
		public static string ArquivoIdCapa = @"arquivoIdCapa";
		public static string ArquivoIdLateral = @"arquivoIdLateral";
		public static string Bibliografia = @"bibliografia";
		public static string DestaquePrincipal = @"destaquePrincipal";
		public static string DestaqueHome = @"destaqueHome";
		public static string RevistaArtigoIdAssociado = @"revistaArtigoIdAssociado";
		public static string ConteudoOnline = @"conteudoOnline";
		public static string Ativo = @"ativo";
		public static string DataPublicacao = @"dataPublicacao";
	}
}
		