
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
	public partial class MeioPagamentoFH : IFilterHelper
	{
		private string _meioPagamentoId;
		public string MeioPagamentoId {
			get { return _meioPagamentoId==null?String.Empty:_meioPagamentoId; }
			set { _meioPagamentoId=value; }
		}

		private string _nome;
		public string Nome {
			get { return _nome==null?String.Empty:_nome; }
			set { _nome=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _codigoLegado;
		public string CodigoLegado {
			get { return _codigoLegado==null?String.Empty:_codigoLegado; }
			set { _codigoLegado=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MeioPagamentoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (meioPagamentoId="+MeioPagamentoId+")");
			}

			if (!Nome.Equals(String.Empty)) {
				sbWhere.Append(" AND (nome LIKE '%"+Nome+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if (!CodigoLegado.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoLegado LIKE '%"+CodigoLegado+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
