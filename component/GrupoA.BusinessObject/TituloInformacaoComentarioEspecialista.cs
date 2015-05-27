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
	public partial class TituloInformacaoComentarioEspecialista 
	{
		private int _tituloInformacaoComentarioEspecialistaId;
		private string _textoComentario;
		private string _tituloComentario;
		private string _urlMidia;
		private bool _destaqueAreaConhecimento;
		private string _nomeEspecialista;
		private string _especialidade;
		private string _resumoComentario;
		private List<TituloInformacaoComentarioEspecialistaCategoria> _tituloInformacaoComentarioEspecialistaCategorias;
		private Arquivo _arquivoAudio;
		private Arquivo _arquivoImagem;
		private ComentarioFormato _comentarioFormato;
		private Titulo _titulo;

		public int TituloInformacaoComentarioEspecialistaId {
			get { return _tituloInformacaoComentarioEspecialistaId; }
			set { _tituloInformacaoComentarioEspecialistaId = value; }
		}

		public string TextoComentario {
			get { return _textoComentario; }
			set { _textoComentario = value; }
		}

		public string TituloComentario {
			get { return _tituloComentario; }
			set { _tituloComentario = value; }
		}

		public string UrlMidia {
			get { return _urlMidia; }
			set { _urlMidia = value; }
		}

		public bool DestaqueAreaConhecimento {
			get { return _destaqueAreaConhecimento; }
			set { _destaqueAreaConhecimento = value; }
		}

		public string NomeEspecialista {
			get { return _nomeEspecialista; }
			set { _nomeEspecialista = value; }
		}

		public string Especialidade {
			get { return _especialidade; }
			set { _especialidade = value; }
		}

		public string ResumoComentario {
			get { return _resumoComentario; }
			set { _resumoComentario = value; }
		}

		public List<TituloInformacaoComentarioEspecialistaCategoria> TituloInformacaoComentarioEspecialistaCategorias {
			get { return _tituloInformacaoComentarioEspecialistaCategorias; }
			set { _tituloInformacaoComentarioEspecialistaCategorias = value; }
		}

		public Arquivo ArquivoAudio {
			get { return _arquivoAudio; }
			set { _arquivoAudio = value; }
		}

		public Arquivo ArquivoImagem {
			get { return _arquivoImagem; }
			set { _arquivoImagem = value; }
		}

		public ComentarioFormato ComentarioFormato {
			get { return _comentarioFormato; }
			set { _comentarioFormato = value; }
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
            get { return Validation.Validate<TituloInformacaoComentarioEspecialista>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloInformacaoComentarioEspecialista>(this);
        }
	}
	
	public struct TituloInformacaoComentarioEspecialistaColunas
	{	
		public static string TituloInformacaoComentarioEspecialistaId = @"tituloInformacaoComentarioEspecialistaId";
		public static string TextoComentario = @"textoComentario";
		public static string TituloComentario = @"tituloComentario";
		public static string UrlMidia = @"urlMidia";
		public static string ArquivoIdAudio = @"arquivoIdAudio";
		public static string ArquivoIdImagem = @"arquivoIdImagem";
		public static string DestaqueAreaConhecimento = @"destaqueAreaConhecimento";
		public static string NomeEspecialista = @"nomeEspecialista";
		public static string Especialidade = @"especialidade";
		public static string ComentarioFormatoId = @"comentarioFormatoId";
		public static string ResumoComentario = @"resumoComentario";
	}
}
		