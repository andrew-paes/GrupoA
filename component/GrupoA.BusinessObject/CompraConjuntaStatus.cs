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
	public partial class CompraConjuntaStatus 
	{
		// Construtor
		public CompraConjuntaStatus() {}

		// Construtor com identificador
		public CompraConjuntaStatus(int compraConjuntaStatusId) {
			_compraConjuntaStatusId = compraConjuntaStatusId;
		}

		private int _compraConjuntaStatusId;
		private string _statusCompra;
		private List<CompraConjunta> _compraConjuntas;

		public int CompraConjuntaStatusId {
			get { return _compraConjuntaStatusId; }
			set { _compraConjuntaStatusId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string StatusCompra {
			get { return _statusCompra; }
			set { _statusCompra = value; }
		}

		public List<CompraConjunta> CompraConjuntas {
			get { return _compraConjuntas; }
			set { _compraConjuntas = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<CompraConjuntaStatus>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<CompraConjuntaStatus>(this);
        }
	}
	
	public struct CompraConjuntaStatusColunas
	{	
		public static string CompraConjuntaStatusId = @"compraConjuntaStatusId";
		public static string StatusCompra = @"statusCompra";
	}
}
		