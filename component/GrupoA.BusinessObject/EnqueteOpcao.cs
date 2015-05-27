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
	public partial class EnqueteOpcao 
	{
		// Construtor
		public EnqueteOpcao() {}

		// Construtor com identificador
		public EnqueteOpcao(int enqueteOpcaoId) {
			_enqueteOpcaoId = enqueteOpcaoId;
		}

		private int _enqueteOpcaoId;
		private string _descricao;
		private int _contador;
		private Enquete _enquete;

		public int EnqueteOpcaoId {
			get { return _enqueteOpcaoId; }
			set { _enqueteOpcaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 1073741823)]
		public string Descricao {
			get { return _descricao; }
			set { _descricao = value; }
		}

		public int Contador {
			get { return _contador; }
			set { _contador = value; }
		}

		[NotNullValidator]
		public Enquete Enquete {
			get { return _enquete; }
			set { _enquete = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<EnqueteOpcao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<EnqueteOpcao>(this);
        }
	}
	
	public struct EnqueteOpcaoColunas
	{	
		public static string EnqueteOpcaoId = @"enqueteOpcaoId";
		public static string EnqueteId = @"enqueteId";
		public static string Descricao = @"descricao";
		public static string Contador = @"contador";
	}
}
		