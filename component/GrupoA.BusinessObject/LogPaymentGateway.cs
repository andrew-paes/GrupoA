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
	public partial class LogPaymentGateway 
	{
		private int _logPaymentGatewayId;
		private int? _codigoPedido;
		private string _conteudoParametros;
		private string _conteudoXML;
		private DateTime _dataHora;

		public int LogPaymentGatewayId {
			get { return _logPaymentGatewayId; }
			set { _logPaymentGatewayId = value; }
		}

		public int? CodigoPedido {
			get { return _codigoPedido; }
			set { _codigoPedido = value; }
		}

		public string ConteudoParametros {
			get { return _conteudoParametros; }
			set { _conteudoParametros = value; }
		}

		public string ConteudoXML {
			get { return _conteudoXML; }
			set { _conteudoXML = value; }
		}

		[NotNullValidator]
		public DateTime DataHora {
			get { return _dataHora; }
			set { _dataHora = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<LogPaymentGateway>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<LogPaymentGateway>(this);
        }
	}
	
	public struct LogPaymentGatewayColunas
	{	
		public static string LogPaymentGatewayId = @"logPaymentGatewayId";
		public static string CodigoPedido = @"codigoPedido";
		public static string ConteudoParametros = @"conteudoParametros";
		public static string ConteudoXML = @"conteudoXML";
		public static string DataHora = @"dataHora";
	}
}
		