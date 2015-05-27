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
	public partial class Enquete 
	{
		// Construtor
		public Enquete() {}

		// Construtor com identificador
		public Enquete(int enqueteId) {
			_enqueteId = enqueteId;
		}

		private int _enqueteId;
		private string _nomeEnquete;
		private bool _ativo;
		private string _pergunta;
		private List<EnquetePagina> _enquetePaginas;
		private List<EnqueteOpcao> _enqueteOpcoes;
		private List<Usuario> _usuarios;

		public int EnqueteId {
			get { return _enqueteId; }
			set { _enqueteId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeEnquete {
			get { return _nomeEnquete; }
			set { _nomeEnquete = value; }
		}

		public bool Ativo {
			get { return _ativo; }
			set { _ativo = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string Pergunta {
			get { return _pergunta; }
			set { _pergunta = value; }
		}

		public List<EnquetePagina> EnquetePaginas {
			get { return _enquetePaginas; }
			set { _enquetePaginas = value; }
		}

		public List<EnqueteOpcao> EnqueteOpcoes {
			get { return _enqueteOpcoes; }
			set { _enqueteOpcoes = value; }
		}

		public List<Usuario> Usuarios {
			get { return _usuarios; }
			set { _usuarios = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Enquete>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Enquete>(this);
        }
	}
	
	public struct EnqueteColunas
	{	
		public static string EnqueteId = @"enqueteId";
		public static string NomeEnquete = @"nomeEnquete";
		public static string Ativo = @"ativo";
		public static string Pergunta = @"pergunta";
	}
}
		