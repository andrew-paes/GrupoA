
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
using System;
using System.Text;

namespace GrupoA.FilterHelper
{
	public partial class LogPaymentGatewayFH : IFilterHelper
	{
		private string _logPaymentGatewayId;
		public string LogPaymentGatewayId {
			get { return _logPaymentGatewayId==null?String.Empty:_logPaymentGatewayId; }
			set { _logPaymentGatewayId=value; }
		}

		private string _codigoPedido;
		public string CodigoPedido {
			get { return _codigoPedido==null?String.Empty:_codigoPedido; }
			set { _codigoPedido=value; }
		}

		private string _conteudoParametros;
		public string ConteudoParametros {
			get { return _conteudoParametros==null?String.Empty:_conteudoParametros; }
			set { _conteudoParametros=value; }
		}

		private string _conteudoXML;
		public string ConteudoXML {
			get { return _conteudoXML==null?String.Empty:_conteudoXML; }
			set { _conteudoXML=value; }
		}

		private string _dataHoraPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraPeriodoInicial {
			get { return _dataHoraPeriodoInicial==null?String.Empty:_dataHoraPeriodoInicial; }
			set { _dataHoraPeriodoInicial=value; }
		}
		private string _dataHoraPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraPeriodoFinal {
			get { return _dataHoraPeriodoFinal==null?String.Empty:_dataHoraPeriodoFinal; }
			set { _dataHoraPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!LogPaymentGatewayId.Equals(String.Empty)) {
				sbWhere.Append(" AND (logPaymentGatewayId="+LogPaymentGatewayId+")");
			}

			if (!CodigoPedido.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoPedido="+CodigoPedido+")");
			}

			if (!ConteudoParametros.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoParametros LIKE '%"+ConteudoParametros+"%')");
			}

			if (!ConteudoXML.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoXML LIKE '%"+ConteudoXML+"%')");
			}

			if( !DataHoraPeriodoInicial.Equals(String.Empty) && !DataHoraPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHora >='"+DataHoraPeriodoInicial+"'");
				sbWhere.Append(" AND dataHora <='"+DataHoraPeriodoFinal+"')");
			} else if (!DataHoraPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHora >='"+DataHoraPeriodoInicial+"')");
			} else if (!DataHoraPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHora <='"+DataHoraPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
