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
	public partial class PedidoFretePreco 
	{
		// Construtor
		public PedidoFretePreco() {}

		// Construtor com identificador
		public PedidoFretePreco(int pedidoFretePrecoId) {
			_pedidoFretePrecoId = pedidoFretePrecoId;
		}

		private int _pedidoFretePrecoId;
		private decimal _peso;
		private float _preco;
		private PedidoFreteGrupo _pedidoFreteGrupo;

		public int PedidoFretePrecoId {
			get { return _pedidoFretePrecoId; }
			set { _pedidoFretePrecoId = value; }
		}

		public decimal Peso {
			get { return _peso; }
			set { _peso = value; }
		}

		public float Preco {
			get { return _preco; }
			set { _preco = value; }
		}

		[NotNullValidator]
		public PedidoFreteGrupo PedidoFreteGrupo {
			get { return _pedidoFreteGrupo; }
			set { _pedidoFreteGrupo = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoFretePreco>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoFretePreco>(this);
        }
	}
	
	public struct PedidoFretePrecoColunas
	{	
		public static string PedidoFretePrecoId = @"pedidoFretePrecoId";
		public static string PedidoFreteGrupoId = @"pedidoFreteGrupoId";
		public static string Peso = @"peso";
		public static string Preco = @"preco";
	}
}
		