
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
	public partial class MidiaFH : IFilterHelper
	{
		private string _midiaId;
		public string MidiaId {
			get { return _midiaId==null?String.Empty:_midiaId; }
			set { _midiaId=value; }
		}

		private string _midiaTipoId;
		public string MidiaTipoId {
			get { return _midiaTipoId==null?String.Empty:_midiaTipoId; }
			set { _midiaTipoId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _arquivoIdThumb;
		public string ArquivoIdThumb {
			get { return _arquivoIdThumb==null?String.Empty:_arquivoIdThumb; }
			set { _arquivoIdThumb=value; }
		}

		private string _tituloMidia;
		public string TituloMidia {
			get { return _tituloMidia==null?String.Empty:_tituloMidia; }
			set { _tituloMidia=value; }
		}

		private string _urlMidia;
		public string UrlMidia {
			get { return _urlMidia==null?String.Empty:_urlMidia; }
			set { _urlMidia=value; }
		}

		private string _autor;
		public string Autor {
			get { return _autor==null?String.Empty:_autor; }
			set { _autor=value; }
		}

		private string _descricaoMidia;
		public string DescricaoMidia {
			get { return _descricaoMidia==null?String.Empty:_descricaoMidia; }
			set { _descricaoMidia=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MidiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaId="+MidiaId+")");
			}

			if (!MidiaTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaTipoId="+MidiaTipoId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!ArquivoIdThumb.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdThumb="+ArquivoIdThumb+")");
			}

			if (!TituloMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloMidia LIKE '%"+TituloMidia+"%')");
			}

			if (!UrlMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlMidia LIKE '%"+UrlMidia+"%')");
			}

			if (!Autor.Equals(String.Empty)) {
				sbWhere.Append(" AND (autor LIKE '%"+Autor+"%')");
			}

			if (!DescricaoMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoMidia LIKE '%"+DescricaoMidia+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
