
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
	public partial class ClippingFH : IFilterHelper
	{
		private string _clippingId;
		public string ClippingId {
			get { return _clippingId==null?String.Empty:_clippingId; }
			set { _clippingId=value; }
		}

		private string _autor;
		public string Autor {
			get { return _autor==null?String.Empty:_autor; }
			set { _autor=value; }
		}

		private string _dataPublicacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataPublicacaoPeriodoInicial {
			get { return _dataPublicacaoPeriodoInicial==null?String.Empty:_dataPublicacaoPeriodoInicial; }
			set { _dataPublicacaoPeriodoInicial=value; }
		}
		private string _dataPublicacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataPublicacaoPeriodoFinal {
			get { return _dataPublicacaoPeriodoFinal==null?String.Empty:_dataPublicacaoPeriodoFinal; }
			set { _dataPublicacaoPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ClippingId.Equals(String.Empty)) {
				sbWhere.Append(" AND (clippingId="+ClippingId+")");
			}

			if (!Autor.Equals(String.Empty)) {
				sbWhere.Append(" AND (autor LIKE '%"+Autor+"%')");
			}

			if( !DataPublicacaoPeriodoInicial.Equals(String.Empty) && !DataPublicacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataPublicacao >='"+DataPublicacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataPublicacao <='"+DataPublicacaoPeriodoFinal+"')");
			} else if (!DataPublicacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataPublicacao >='"+DataPublicacaoPeriodoInicial+"')");
			} else if (!DataPublicacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataPublicacao <='"+DataPublicacaoPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
