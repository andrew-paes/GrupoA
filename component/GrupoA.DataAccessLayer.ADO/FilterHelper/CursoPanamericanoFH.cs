
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
	public partial class CursoPanamericanoFH : IFilterHelper
	{
		private string _cursoPanamericanoId;
		public string CursoPanamericanoId {
			get { return _cursoPanamericanoId==null?String.Empty:_cursoPanamericanoId; }
			set { _cursoPanamericanoId=value; }
		}

		private string _titulo;
		public string Titulo {
			get { return _titulo==null?String.Empty:_titulo; }
			set { _titulo=value; }
		}

		private string _subtitulo;
		public string Subtitulo {
			get { return _subtitulo==null?String.Empty:_subtitulo; }
			set { _subtitulo=value; }
		}

		private string _descricao;
		public string Descricao {
			get { return _descricao==null?String.Empty:_descricao; }
			set { _descricao=value; }
		}

		private string _arquivoIdImagem;
		public string ArquivoIdImagem {
			get { return _arquivoIdImagem==null?String.Empty:_arquivoIdImagem; }
			set { _arquivoIdImagem=value; }
		}

		private string _urlLink;
		public string UrlLink {
			get { return _urlLink==null?String.Empty:_urlLink; }
			set { _urlLink=value; }
		}

		private string _targetBlank;
		public string TargetBlank {
			get { return _targetBlank==null?String.Empty:_targetBlank; }
			set { _targetBlank=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CursoPanamericanoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (cursoPanamericanoId="+CursoPanamericanoId+")");
			}

			if (!Titulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (titulo LIKE '%"+Titulo+"%')");
			}

			if (!Subtitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (subtitulo LIKE '%"+Subtitulo+"%')");
			}

			if (!Descricao.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricao LIKE '%"+Descricao+"%')");
			}

			if (!ArquivoIdImagem.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdImagem="+ArquivoIdImagem+")");
			}

			if (!UrlLink.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlLink LIKE '%"+UrlLink+"%')");
			}

			if (!TargetBlank.Equals(String.Empty)) {
				sbWhere.Append(" AND (targetBlank LIKE '%"+TargetBlank+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
