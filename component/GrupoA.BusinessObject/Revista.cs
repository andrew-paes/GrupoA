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
	public partial class Revista 
	{
		// Construtor
		public Revista() {}

		// Construtor com identificador
		public Revista(int revistaId) {
			_revistaId = revistaId;
		}

		private int _revistaId;
		private string _nomeRevista;
		private int _periodicidade;
		private string _descricaoRevista;
		private string _publicoAlvo;
		private string _iSSN;
		private List<MidiaRevista> _midiaRevistas;
		private List<Promocao> _promocoes;
		private List<Categoria> _categorias;
		private List<RevistaAssinatura> _revistaAssinaturas;
		private List<RevistaEdicao> _revistaEdicoes;
		private List<RevistaPagina> _revistaPaginas;
		private List<RevistaSecao> _revistaSecoes;
		private List<UsuarioRevista> _usuarioRevistas;

		public int RevistaId {
			get { return _revistaId; }
			set { _revistaId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeRevista {
			get { return _nomeRevista; }
			set { _nomeRevista = value; }
		}

		public int Periodicidade {
			get { return _periodicidade; }
			set { _periodicidade = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string DescricaoRevista {
			get { return _descricaoRevista; }
			set { _descricaoRevista = value; }
		}

		public string PublicoAlvo {
			get { return _publicoAlvo; }
			set { _publicoAlvo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string ISSN {
			get { return _iSSN; }
			set { _iSSN = value; }
		}

		public List<MidiaRevista> MidiaRevistas {
			get { return _midiaRevistas; }
			set { _midiaRevistas = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

		public List<Categoria> Categorias {
			get { return _categorias; }
			set { _categorias = value; }
		}

		public List<RevistaAssinatura> RevistaAssinaturas {
			get { return _revistaAssinaturas; }
			set { _revistaAssinaturas = value; }
		}

		public List<RevistaEdicao> RevistaEdicoes {
			get { return _revistaEdicoes; }
			set { _revistaEdicoes = value; }
		}

		public List<RevistaPagina> RevistaPaginas {
			get { return _revistaPaginas; }
			set { _revistaPaginas = value; }
		}

		public List<RevistaSecao> RevistaSecoes {
			get { return _revistaSecoes; }
			set { _revistaSecoes = value; }
		}
        
		public List<UsuarioRevista> UsuarioRevistas {
			get { return _usuarioRevistas; }
			set { _usuarioRevistas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Revista>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Revista>(this);
        }
	}
	
	public struct RevistaColunas
	{	
		public static string RevistaId = @"revistaId";
		public static string NomeRevista = @"nomeRevista";
		public static string Periodicidade = @"periodicidade";
		public static string DescricaoRevista = @"descricaoRevista";
		public static string PublicoAlvo = @"publicoAlvo";
		public static string ISSN = @"ISSN";
	}
}
		