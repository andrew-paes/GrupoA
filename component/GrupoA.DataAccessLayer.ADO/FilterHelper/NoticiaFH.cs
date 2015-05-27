
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
	public partial class NoticiaFH : IFilterHelper
	{
		private string _noticiaId;
		public string NoticiaId {
			get { return _noticiaId==null?String.Empty:_noticiaId; }
			set { _noticiaId=value; }
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

		private string _categoriaNoticiaId;
		public string CategoriaNoticiaId {
			get { return _categoriaNoticiaId==null?String.Empty:_categoriaNoticiaId; }
			set { _categoriaNoticiaId=value; }
		}

		private string _arquivoIdThumb;
		public string ArquivoIdThumb {
			get { return _arquivoIdThumb==null?String.Empty:_arquivoIdThumb; }
			set { _arquivoIdThumb=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!NoticiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (noticiaId="+NoticiaId+")");
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

			if (!CategoriaNoticiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaNoticiaId="+CategoriaNoticiaId+")");
			}

			if (!ArquivoIdThumb.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdThumb="+ArquivoIdThumb+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
