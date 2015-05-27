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
	public partial class NoticiaImagem 
	{
		// Construtor
		public NoticiaImagem() {}

		// Construtor com identificador
		public NoticiaImagem(int noticiaImagemId) {
			_noticiaImagemId = noticiaImagemId;
		}

		private int _noticiaImagemId;
		private int _ordemApresentacao;
		private Arquivo _arquivo;
		private Noticia _noticia;

		public int NoticiaImagemId {
			get { return _noticiaImagemId; }
			set { _noticiaImagemId = value; }
		}

		public int OrdemApresentacao {
			get { return _ordemApresentacao; }
			set { _ordemApresentacao = value; }
		}

		[NotNullValidator]
		public Arquivo Arquivo {
			get { return _arquivo; }
			set { _arquivo = value; }
		}

		[NotNullValidator]
		public Noticia Noticia {
			get { return _noticia; }
			set { _noticia = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<NoticiaImagem>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<NoticiaImagem>(this);
        }
	}
	
	public struct NoticiaImagemColunas
	{	
		public static string NoticiaImagemId = @"noticiaImagemId";
		public static string ArquivoId = @"arquivoId";
		public static string NoticiaId = @"noticiaId";
		public static string OrdemApresentacao = @"ordemApresentacao";
	}
}
		