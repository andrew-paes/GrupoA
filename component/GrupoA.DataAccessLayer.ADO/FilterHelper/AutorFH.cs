
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
	public partial class AutorFH : IFilterHelper
	{
		private string _autorId;
		public string AutorId {
			get { return _autorId==null?String.Empty:_autorId; }
			set { _autorId=value; }
		}

		private string _url;
		public string Url {
			get { return _url==null?String.Empty:_url; }
			set { _url=value; }
		}

		private string _email;
		public string Email {
			get { return _email==null?String.Empty:_email; }
			set { _email=value; }
		}

		private string _blog;
		public string Blog {
			get { return _blog==null?String.Empty:_blog; }
			set { _blog=value; }
		}

		private string _nomeAutor;
		public string NomeAutor {
			get { return _nomeAutor==null?String.Empty:_nomeAutor; }
			set { _nomeAutor=value; }
		}

		private string _codigoLegado;
		public string CodigoLegado {
			get { return _codigoLegado==null?String.Empty:_codigoLegado; }
			set { _codigoLegado=value; }
		}

		private string _biografia;
		public string Biografia {
			get { return _biografia==null?String.Empty:_biografia; }
			set { _biografia=value; }
		}

		private string _arquivoIdImagem;
		public string ArquivoIdImagem {
			get { return _arquivoIdImagem==null?String.Empty:_arquivoIdImagem; }
			set { _arquivoIdImagem=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!AutorId.Equals(String.Empty)) {
				sbWhere.Append(" AND (autorId="+AutorId+")");
			}

			if (!Url.Equals(String.Empty)) {
				sbWhere.Append(" AND (url LIKE '%"+Url+"%')");
			}

			if (!Email.Equals(String.Empty)) {
				sbWhere.Append(" AND (email LIKE '%"+Email+"%')");
			}

			if (!Blog.Equals(String.Empty)) {
				sbWhere.Append(" AND (blog LIKE '%"+Blog+"%')");
			}

			if (!NomeAutor.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeAutor LIKE '%"+NomeAutor+"%')");
			}

			if (!CodigoLegado.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoLegado LIKE '%"+CodigoLegado+"%')");
			}

			if (!Biografia.Equals(String.Empty)) {
				sbWhere.Append(" AND (biografia LIKE '%"+Biografia+"%')");
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
