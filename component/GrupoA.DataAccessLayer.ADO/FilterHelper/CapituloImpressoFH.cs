
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
	public partial class CapituloImpressoFH : IFilterHelper
	{
		private string _capituloImpressoId;
		public string CapituloImpressoId {
			get { return _capituloImpressoId==null?String.Empty:_capituloImpressoId; }
			set { _capituloImpressoId=value; }
		}

		private string _capituloId;
		public string CapituloId {
			get { return _capituloId==null?String.Empty:_capituloId; }
			set { _capituloId=value; }
		}

		private string _tituloImpressoId;
		public string TituloImpressoId {
			get { return _tituloImpressoId==null?String.Empty:_tituloImpressoId; }
			set { _tituloImpressoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CapituloImpressoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (capituloImpressoId="+CapituloImpressoId+")");
			}

			if (!CapituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (capituloId="+CapituloId+")");
			}

			if (!TituloImpressoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloImpressoId="+TituloImpressoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
