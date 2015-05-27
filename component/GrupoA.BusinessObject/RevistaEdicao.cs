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
	public partial class RevistaEdicao 
	{
		private int _revistaEdicaoId;
		private int? _numeroEdicao;
		private int _anoPublicacao;
		private int _mesPublicacao;
		private string _periodoPublicacao;
		private string _anoEdicao;
		private string _tituloEdicao;
		private string _descricaoEdicao;
		private bool _ativo;
		private int? _numeroPaginas;
		private List<RevistaArtigo> _revistaArtigos;
		private Produto _produto;
		private Revista _revista;

		public int RevistaEdicaoId {
			get { return _revistaEdicaoId; }
			set { _revistaEdicaoId = value; }
		}

		public int? NumeroEdicao {
			get { return _numeroEdicao; }
			set { _numeroEdicao = value; }
		}

		public int AnoPublicacao {
			get { return _anoPublicacao; }
			set { _anoPublicacao = value; }
		}

		public int MesPublicacao {
			get { return _mesPublicacao; }
			set { _mesPublicacao = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string PeriodoPublicacao {
			get { return _periodoPublicacao; }
			set { _periodoPublicacao = value; }
		}

		public string AnoEdicao {
			get { return _anoEdicao; }
			set { _anoEdicao = value; }
		}

		public string TituloEdicao {
			get { return _tituloEdicao; }
			set { _tituloEdicao = value; }
		}

		public string DescricaoEdicao {
			get { return _descricaoEdicao; }
			set { _descricaoEdicao = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		public int? NumeroPaginas {
			get { return _numeroPaginas; }
			set { _numeroPaginas = value; }
		}

		public List<RevistaArtigo> RevistaArtigos {
			get { return _revistaArtigos; }
			set { _revistaArtigos = value; }
		}

		[NotNullValidator]
		public Produto Produto {
			get { return _produto; }
			set { _produto = value; }
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
            get { return Validation.Validate<RevistaEdicao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<RevistaEdicao>(this);
        }
	}
	
	public struct RevistaEdicaoColunas
	{	
		public static string RevistaEdicaoId = @"revistaEdicaoId";
		public static string RevistaId = @"revistaId";
		public static string NumeroEdicao = @"numeroEdicao";
		public static string AnoPublicacao = @"anoPublicacao";
		public static string MesPublicacao = @"mesPublicacao";
		public static string PeriodoPublicacao = @"periodoPublicacao";
		public static string AnoEdicao = @"anoEdicao";
		public static string TituloEdicao = @"tituloEdicao";
		public static string DescricaoEdicao = @"descricaoEdicao";
		public static string Ativo = @"ativo";
		public static string NumeroPaginas = @"numeroPaginas";
	}
}
		