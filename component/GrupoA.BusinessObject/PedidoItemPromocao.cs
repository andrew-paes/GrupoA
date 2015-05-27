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
	public partial class PedidoItemPromocao 
	{
		private int _pedidoItemPromocaoId;
		private bool _freteGratis;
		private decimal? _descontoPercentual;
		private decimal? _descontoValor;
		private PedidoItem _pedidoItem;
		private Promocao _promocao;

		public int PedidoItemPromocaoId {
			get { return _pedidoItemPromocaoId; }
			set { _pedidoItemPromocaoId = value; }
		}

		public bool FreteGratis {
			get { return _freteGratis; }
			set { _freteGratis = value; }
		}

		public decimal? DescontoPercentual {
			get { return _descontoPercentual; }
			set { _descontoPercentual = value; }
		}

		public decimal? DescontoValor {
			get { return _descontoValor; }
			set { _descontoValor = value; }
		}

		[NotNullValidator]
		public PedidoItem PedidoItem {
			get { return _pedidoItem; }
			set { _pedidoItem = value; }
		}

		[NotNullValidator]
		public Promocao Promocao {
			get { return _promocao; }
			set { _promocao = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<PedidoItemPromocao>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<PedidoItemPromocao>(this);
        }
	}
	
	public struct PedidoItemPromocaoColunas
	{	
		public static string PedidoItemPromocaoId = @"pedidoItemPromocaoId";
		public static string FreteGratis = @"freteGratis";
		public static string DescontoPercentual = @"descontoPercentual";
		public static string DescontoValor = @"descontoValor";
		public static string PromocaoId = @"promocaoId";
	}
}
		