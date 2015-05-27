
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
	public partial class TituloImagemResumoFH : IFilterHelper
	{
		private string _tituloImagemResumoId;
		public string TituloImagemResumoId {
			get { return _tituloImagemResumoId==null?String.Empty:_tituloImagemResumoId; }
			set { _tituloImagemResumoId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloImagemResumoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloImagemResumoId="+TituloImagemResumoId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
