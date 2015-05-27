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
	public partial class PedidoFreteTipo 
	{
		// Construtor
		public PedidoFreteTipo() {}

		// Construtor com identificador
		public PedidoFreteTipo(string pedidoFreteTipoId) {
			_pedidoFreteTipoId = pedidoFreteTipoId;
		}

		private string _pedidoFreteTipoId;
		private string _nomeTipo;
		private List<PedidoFreteGrupo> _pedidoFreteGrupos;

		public string PedidoFreteTipoId {
			get { return _pedidoFreteTipoId; }
			set { _pedidoFreteTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string NomeTipo {
			get { return _nomeTipo; }
			set { _nomeTipo = value; }
		}

		public List<PedidoFreteGrupo> PedidoFreteGrupos {
			get { return _pedidoFreteGrupos; }
			set { _pedidoFreteGrupos = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoFreteTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoFreteTipo>(this);
        }
	}
	
	public struct PedidoFreteTipoColunas
	{	
		public static string PedidoFreteTipoId = @"PedidoFreteTipoId";
		public static string NomeTipo = @"nomeTipo";
	}
}
		