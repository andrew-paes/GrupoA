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
	public partial class Regiao 
	{
		// Construtor
		public Regiao() {}

		// Construtor com identificador
		public Regiao(int regiaoId) {
			_regiaoId = regiaoId;
		}

		private int _regiaoId;
		private string _nomeRegiao;
		private string _uf;
		private List<Municipio> _municipios;

		public int RegiaoId {
			get { return _regiaoId; }
			set { _regiaoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 100)]
		public string NomeRegiao {
			get { return _nomeRegiao; }
			set { _nomeRegiao = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 2)]
		public string Uf {
			get { return _uf; }
			set { _uf = value; }
		}

		public List<Municipio> Municipios {
			get { return _municipios; }
			set { _municipios = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Regiao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Regiao>(this);
        }
	}
	
	public struct RegiaoColunas
	{	
		public static string RegiaoId = @"regiaoId";
		public static string NomeRegiao = @"nomeRegiao";
		public static string Uf = @"uf";
	}
}
		