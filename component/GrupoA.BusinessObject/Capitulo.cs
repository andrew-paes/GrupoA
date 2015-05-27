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
	public partial class Capitulo 
	{
		private int _capituloId;
		private string _nomeCapitulo;
		private int _numeroPaginaCapitulo;
		private string _resumoCapitulo;
		private string _codigoLegado;
		private List<Autor> _autores;
		private CapituloEletronico _capituloEletronico;
		private CapituloImpresso _capituloImpresso;
		private Conteudo _conteudo;
		private Titulo _titulo;

		public int CapituloId {
			get { return _capituloId; }
			set { _capituloId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string NomeCapitulo {
			get { return _nomeCapitulo; }
			set { _nomeCapitulo = value; }
		}

		public int NumeroPaginaCapitulo {
			get { return _numeroPaginaCapitulo; }
			set { _numeroPaginaCapitulo = value; }
		}

		public string ResumoCapitulo {
			get { return _resumoCapitulo; }
			set { _resumoCapitulo = value; }
		}

		public string CodigoLegado {
			get { return _codigoLegado; }
			set { _codigoLegado = value; }
		}

		public List<Autor> Autores {
			get { return _autores; }
			set { _autores = value; }
		}

		[NotNullValidator]
		public CapituloEletronico CapituloEletronico {
			get { return _capituloEletronico; }
			set { _capituloEletronico = value; }
		}

		[NotNullValidator]
		public CapituloImpresso CapituloImpresso {
			get { return _capituloImpresso; }
			set { _capituloImpresso = value; }
		}

		[NotNullValidator]
		public Conteudo Conteudo {
			get { return _conteudo; }
			set { _conteudo = value; }
		}

		[NotNullValidator]
		public Titulo Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Capitulo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Capitulo>(this);
        }
	}
	
	public struct CapituloColunas
	{	
		public static string CapituloId = @"capituloId";
		public static string NomeCapitulo = @"nomeCapitulo";
		public static string NumeroPaginaCapitulo = @"numeroPaginaCapitulo";
		public static string ResumoCapitulo = @"resumoCapitulo";
		public static string TituloId = @"tituloId";
		public static string CodigoLegado = @"codigoLegado";
	}
}
		