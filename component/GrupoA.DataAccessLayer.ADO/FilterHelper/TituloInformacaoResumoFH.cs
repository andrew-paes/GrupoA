
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
	public partial class TituloInformacaoResumoFH : IFilterHelper
	{
		private string _tituloInformacaoResumoId;
		public string TituloInformacaoResumoId {
			get { return _tituloInformacaoResumoId==null?String.Empty:_tituloInformacaoResumoId; }
			set { _tituloInformacaoResumoId=value; }
		}

		private string _textoResumo;
		public string TextoResumo {
			get { return _textoResumo==null?String.Empty:_textoResumo; }
			set { _textoResumo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoResumoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoResumoId="+TituloInformacaoResumoId+")");
			}

			if (!TextoResumo.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoResumo LIKE '%"+TextoResumo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
