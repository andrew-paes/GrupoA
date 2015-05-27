
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
	public partial class PaginaPromocionalFH : IFilterHelper
	{
		private string _paginaPromocionalId;
		public string PaginaPromocionalId {
			get { return _paginaPromocionalId==null?String.Empty:_paginaPromocionalId; }
			set { _paginaPromocionalId=value; }
		}

		private string _tituloPagina;
		public string TituloPagina {
			get { return _tituloPagina==null?String.Empty:_tituloPagina; }
			set { _tituloPagina=value; }
		}

		private string _subtituloPagina;
		public string SubtituloPagina {
			get { return _subtituloPagina==null?String.Empty:_subtituloPagina; }
			set { _subtituloPagina=value; }
		}

		private string _resumo;
		public string Resumo {
			get { return _resumo==null?String.Empty:_resumo; }
			set { _resumo=value; }
		}

		private string _linkMidia;
		public string LinkMidia {
			get { return _linkMidia==null?String.Empty:_linkMidia; }
			set { _linkMidia=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PaginaPromocionalId.Equals(String.Empty)) {
				sbWhere.Append(" AND (paginaPromocionalId="+PaginaPromocionalId+")");
			}

			if (!TituloPagina.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloPagina LIKE '%"+TituloPagina+"%')");
			}

			if (!SubtituloPagina.Equals(String.Empty)) {
				sbWhere.Append(" AND (subtituloPagina LIKE '%"+SubtituloPagina+"%')");
			}

			if (!Resumo.Equals(String.Empty)) {
				sbWhere.Append(" AND (resumo LIKE '%"+Resumo+"%')");
			}

			if (!LinkMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (linkMidia LIKE '%"+LinkMidia+"%')");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
