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
	public partial class Conteudo 
	{
		// Construtor
		public Conteudo() {}

		// Construtor com identificador
		public Conteudo(int conteudoId) {
			_conteudoId = conteudoId;
		}

		private int _conteudoId;
		private DateTime _dataHoraCadastro;
		private Capitulo _capitulo;
		private List<Categoria> _categorias;
		private ConteudoAvaliacao _conteudoAvaliacao;
		private ConteudoHits _conteudoHits;
		private ConteudoImprensa _conteudoImprensa;
		private List<Conteudo> _conteudoRelacionadosPai;
		private List<Conteudo> _conteudoRelacionadosRelacionado;
		private List<Favorito> _favoritos;
		private Midia _midia;
		private Produto _produto;
		private RevistaArtigo _revistaArtigo;
		private Titulo _titulo;
		private ConteudoTipo _conteudoTipo;

		public int ConteudoId {
			get { return _conteudoId; }
			set { _conteudoId = value; }
		}

		public DateTime DataHoraCadastro {
			get { return _dataHoraCadastro; }
			set { _dataHoraCadastro = value; }
		}

		[NotNullValidator]
		public Capitulo Capitulo {
			get { return _capitulo; }
			set { _capitulo = value; }
		}

		public List<Categoria> Categorias {
			get { return _categorias; }
			set { _categorias = value; }
		}

		[NotNullValidator]
		public ConteudoAvaliacao ConteudoAvaliacao {
			get { return _conteudoAvaliacao; }
			set { _conteudoAvaliacao = value; }
		}

		[NotNullValidator]
		public ConteudoHits ConteudoHits {
			get { return _conteudoHits; }
			set { _conteudoHits = value; }
		}

		[NotNullValidator]
		public ConteudoImprensa ConteudoImprensa {
			get { return _conteudoImprensa; }
			set { _conteudoImprensa = value; }
		}

		public List<Conteudo> ConteudoRelacionadosPai {
			get { return _conteudoRelacionadosPai; }
			set { _conteudoRelacionadosPai = value; }
		}

		public List<Conteudo> ConteudoRelacionadosRelacionado {
			get { return _conteudoRelacionadosRelacionado; }
			set { _conteudoRelacionadosRelacionado = value; }
		}

		public List<Favorito> Favoritos {
			get { return _favoritos; }
			set { _favoritos = value; }
		}

		[NotNullValidator]
		public Midia Midia {
			get { return _midia; }
			set { _midia = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
		}

		[NotNullValidator]
		public RevistaArtigo RevistaArtigo {
			get { return _revistaArtigo; }
			set { _revistaArtigo = value; }
		}

		[NotNullValidator]
		public Titulo Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

		[NotNullValidator]
		public ConteudoTipo ConteudoTipo {
			get { return _conteudoTipo; }
			set { _conteudoTipo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Conteudo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Conteudo>(this);
        }
	}
	
	public struct ConteudoColunas
	{	
		public static string ConteudoId = @"conteudoId";
		public static string ConteudoTipoId = @"conteudoTipoId";
		public static string DataHoraCadastro = @"dataHoraCadastro";
	}
}
		