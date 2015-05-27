
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
	public partial class PagamentoFH : IFilterHelper
	{
		private string _pagamentoId;
		public string PagamentoId {
			get { return _pagamentoId==null?String.Empty:_pagamentoId; }
			set { _pagamentoId=value; }
		}

		private string _numeroParcelas;
		public string NumeroParcelas {
			get { return _numeroParcelas==null?String.Empty:_numeroParcelas; }
			set { _numeroParcelas=value; }
		}

		private string _meioPagamentoId;
		public string MeioPagamentoId {
			get { return _meioPagamentoId==null?String.Empty:_meioPagamentoId; }
			set { _meioPagamentoId=value; }
		}

		private string _codigoTransacao;
		public string CodigoTransacao {
			get { return _codigoTransacao==null?String.Empty:_codigoTransacao; }
			set { _codigoTransacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PagamentoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pagamentoId="+PagamentoId+")");
			}

			if (!NumeroParcelas.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroParcelas="+NumeroParcelas+")");
			}

			if (!MeioPagamentoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (meioPagamentoId="+MeioPagamentoId+")");
			}

			if (!CodigoTransacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoTransacao LIKE '%"+CodigoTransacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
