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
	public partial class TituloSolicitacao 
	{
		// Construtor
		public TituloSolicitacao() {}

		// Construtor com identificador
		public TituloSolicitacao(int tituloSolicitacaoId) {
			_tituloSolicitacaoId = tituloSolicitacaoId;
		}

		private int _tituloSolicitacaoId;
		private DateTime _dataSolicitacao;
		private string _justificativaProfessor;
		private bool _exportada;
		private List<TituloAvaliacao> _tituloAvaliacoes;
		private Professor _professor;
		private Titulo _titulo;
		private TituloSolicitacaoStatus _tituloSolicitacaoStatus;

		public int TituloSolicitacaoId {
			get { return _tituloSolicitacaoId; }
			set { _tituloSolicitacaoId = value; }
		}

		[NotNullValidator]
		public DateTime DataSolicitacao {
			get { return _dataSolicitacao; }
			set { _dataSolicitacao = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string JustificativaProfessor {
			get { return _justificativaProfessor; }
			set { _justificativaProfessor = value; }
		}

		public bool Exportada {
			get { return _exportada; }
			set { _exportada = value; }
		}

		public List<TituloAvaliacao> TituloAvaliacoes {
			get { return _tituloAvaliacoes; }
			set { _tituloAvaliacoes = value; }
		}

		[NotNullValidator]
		public Professor Professor {
			get { return _professor; }
			set { _professor = value; }
		}

		[NotNullValidator]
		public Titulo Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

		[NotNullValidator]
		public TituloSolicitacaoStatus TituloSolicitacaoStatus {
			get { return _tituloSolicitacaoStatus; }
			set { _tituloSolicitacaoStatus = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloSolicitacao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloSolicitacao>(this);
        }
	}
	
	public struct TituloSolicitacaoColunas
	{	
		public static string TituloSolicitacaoId = @"tituloSolicitacaoId";
		public static string ProfessorId = @"professorId";
		public static string TituloId = @"tituloId";
		public static string TituloSolicitacaoStatusId = @"tituloSolicitacaoStatusId";
		public static string DataSolicitacao = @"dataSolicitacao";
		public static string JustificativaProfessor = @"justificativaProfessor";
		public static string Exportada = @"exportada";
	}
}
		