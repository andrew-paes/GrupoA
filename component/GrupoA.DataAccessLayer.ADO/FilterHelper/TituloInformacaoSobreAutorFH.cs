
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
	public partial class TituloInformacaoSobreAutorFH : IFilterHelper
	{
		private string _tituloInformacaoSobreAutorId;
		public string TituloInformacaoSobreAutorId {
			get { return _tituloInformacaoSobreAutorId==null?String.Empty:_tituloInformacaoSobreAutorId; }
			set { _tituloInformacaoSobreAutorId=value; }
		}

		private string _textoAutor;
		public string TextoAutor {
			get { return _textoAutor==null?String.Empty:_textoAutor; }
			set { _textoAutor=value; }
		}

		private string _urlMidia;
		public string UrlMidia {
			get { return _urlMidia==null?String.Empty:_urlMidia; }
			set { _urlMidia=value; }
		}

		private string _arquivoIdImagem;
		public string ArquivoIdImagem {
			get { return _arquivoIdImagem==null?String.Empty:_arquivoIdImagem; }
			set { _arquivoIdImagem=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoSobreAutorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoSobreAutorId="+TituloInformacaoSobreAutorId+")");
			}

			if (!TextoAutor.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoAutor LIKE '%"+TextoAutor+"%')");
			}

			if (!UrlMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlMidia LIKE '%"+UrlMidia+"%')");
			}

			if (!ArquivoIdImagem.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdImagem="+ArquivoIdImagem+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
