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
	public partial class ProfissionalOcupacao 
	{
		// Construtor
		public ProfissionalOcupacao() {}

		// Construtor com identificador
		public ProfissionalOcupacao(int profissionalOcupacaoId) {
			_profissionalOcupacaoId = profissionalOcupacaoId;
		}

		private int _profissionalOcupacaoId;
		private string _ocupacao;
		private string _codigoOcupacao;
		private List<Usuario> _usuarios;

		public int ProfissionalOcupacaoId {
			get { return _profissionalOcupacaoId; }
			set { _profissionalOcupacaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 200)]
		public string Ocupacao {
			get { return _ocupacao; }
			set { _ocupacao = value; }
		}

		public string CodigoOcupacao {
			get { return _codigoOcupacao; }
			set { _codigoOcupacao = value; }
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
            get { return Validation.Validate<ProfissionalOcupacao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<ProfissionalOcupacao>(this);
        }
	}
	
	public struct ProfissionalOcupacaoColunas
	{	
		public static string ProfissionalOcupacaoId = @"profissionalOcupacaoId";
		public static string Ocupacao = @"ocupacao";
		public static string CodigoOcupacao = @"codigoOcupacao";
	}
}
		