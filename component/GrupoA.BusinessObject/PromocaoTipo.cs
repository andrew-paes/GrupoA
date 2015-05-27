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
	public partial class PromocaoTipo 
	{
		// Construtor
		public PromocaoTipo() {}

		// Construtor com identificador
		public PromocaoTipo(int promocaoTipoId) {
			_promocaoTipoId = promocaoTipoId;
		}

		private int _promocaoTipoId;
		private string _tipoPromocao;
		private List<Promocao> _promocoes;

		public int PromocaoTipoId {
			get { return _promocaoTipoId; }
			set { _promocaoTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string TipoPromocao {
			get { return _tipoPromocao; }
			set { _tipoPromocao = value; }
		}

		public List<Promocao> Promocoes {
			get { return _promocoes; }
			set { _promocoes = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PromocaoTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PromocaoTipo>(this);
        }
	}
	
	public struct PromocaoTipoColunas
	{	
		public static string PromocaoTipoId = @"promocaoTipoId";
		public static string TipoPromocao = @"tipoPromocao";
	}
}
		