
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
	public partial class RevistaArtigoFH : IFilterHelper
	{
		private string _revistaArtigoId;
		public string RevistaArtigoId {
			get { return _revistaArtigoId==null?String.Empty:_revistaArtigoId; }
			set { _revistaArtigoId=value; }
		}

		private string _revistaEdicaoId;
		public string RevistaEdicaoId {
			get { return _revistaEdicaoId==null?String.Empty:_revistaEdicaoId; }
			set { _revistaEdicaoId=value; }
		}

		private string _tituloArtigo;
		public string TituloArtigo {
			get { return _tituloArtigo==null?String.Empty:_tituloArtigo; }
			set { _tituloArtigo=value; }
		}

		private string _subTituloArtigo;
		public string SubTituloArtigo {
			get { return _subTituloArtigo==null?String.Empty:_subTituloArtigo; }
			set { _subTituloArtigo=value; }
		}

		private string _resumo;
		public string Resumo {
			get { return _resumo==null?String.Empty:_resumo; }
			set { _resumo=value; }
		}

		private string _textoArtigo;
		public string TextoArtigo {
			get { return _textoArtigo==null?String.Empty:_textoArtigo; }
			set { _textoArtigo=value; }
		}

		private string _autores;
		public string Autores {
			get { return _autores==null?String.Empty:_autores; }
			set { _autores=value; }
		}

		private string _revistaSecaoId;
		public string RevistaSecaoId {
			get { return _revistaSecaoId==null?String.Empty:_revistaSecaoId; }
			set { _revistaSecaoId=value; }
		}

		private string _revistaArtigoPermissaoId;
		public string RevistaArtigoPermissaoId {
			get { return _revistaArtigoPermissaoId==null?String.Empty:_revistaArtigoPermissaoId; }
			set { _revistaArtigoPermissaoId=value; }
		}

		private string _arquivoIdThumbP;
		public string ArquivoIdThumbP {
			get { return _arquivoIdThumbP==null?String.Empty:_arquivoIdThumbP; }
			set { _arquivoIdThumbP=value; }
		}

		private string _arquivoIdThumbM;
		public string ArquivoIdThumbM {
			get { return _arquivoIdThumbM==null?String.Empty:_arquivoIdThumbM; }
			set { _arquivoIdThumbM=value; }
		}

		private string _arquivoIdCapa;
		public string ArquivoIdCapa {
			get { return _arquivoIdCapa==null?String.Empty:_arquivoIdCapa; }
			set { _arquivoIdCapa=value; }
		}

		private string _arquivoIdLateral;
		public string ArquivoIdLateral {
			get { return _arquivoIdLateral==null?String.Empty:_arquivoIdLateral; }
			set { _arquivoIdLateral=value; }
		}

		private string _bibliografia;
		public string Bibliografia {
			get { return _bibliografia==null?String.Empty:_bibliografia; }
			set { _bibliografia=value; }
		}

		private string _destaquePrincipal;
		public string DestaquePrincipal {
			get { return _destaquePrincipal==null?String.Empty:_destaquePrincipal; }
			set { _destaquePrincipal=value; }
		}

		private string _destaqueHome;
		public string DestaqueHome {
			get { return _destaqueHome==null?String.Empty:_destaqueHome; }
			set { _destaqueHome=value; }
		}

		private string _revistaArtigoIdAssociado;
		public string RevistaArtigoIdAssociado {
			get { return _revistaArtigoIdAssociado==null?String.Empty:_revistaArtigoIdAssociado; }
			set { _revistaArtigoIdAssociado=value; }
		}

		private string _conteudoOnline;
		public string ConteudoOnline {
			get { return _conteudoOnline==null?String.Empty:_conteudoOnline; }
			set { _conteudoOnline=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
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

			if (!RevistaArtigoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaArtigoId="+RevistaArtigoId+")");
			}

			if (!RevistaEdicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaEdicaoId="+RevistaEdicaoId+")");
			}

			if (!TituloArtigo.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloArtigo LIKE '%"+TituloArtigo+"%')");
			}

			if (!SubTituloArtigo.Equals(String.Empty)) {
				sbWhere.Append(" AND (subTituloArtigo LIKE '%"+SubTituloArtigo+"%')");
			}

			if (!Resumo.Equals(String.Empty)) {
				sbWhere.Append(" AND (resumo LIKE '%"+Resumo+"%')");
			}

			if (!TextoArtigo.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoArtigo LIKE '%"+TextoArtigo+"%')");
			}

			if (!Autores.Equals(String.Empty)) {
				sbWhere.Append(" AND (autores LIKE '%"+Autores+"%')");
			}

			if (!RevistaSecaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaSecaoId="+RevistaSecaoId+")");
			}

			if (!RevistaArtigoPermissaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaArtigoPermissaoId="+RevistaArtigoPermissaoId+")");
			}

			if (!ArquivoIdThumbP.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdThumbP="+ArquivoIdThumbP+")");
			}

			if (!ArquivoIdThumbM.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdThumbM="+ArquivoIdThumbM+")");
			}

			if (!ArquivoIdCapa.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdCapa="+ArquivoIdCapa+")");
			}

			if (!ArquivoIdLateral.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdLateral="+ArquivoIdLateral+")");
			}

			if (!Bibliografia.Equals(String.Empty)) {
				sbWhere.Append(" AND (bibliografia LIKE '%"+Bibliografia+"%')");
			}

			if (!DestaquePrincipal.Equals(String.Empty)) {
				sbWhere.Append(" AND (destaquePrincipal LIKE '%"+DestaquePrincipal+"%')");
			}

			if (!DestaqueHome.Equals(String.Empty)) {
				sbWhere.Append(" AND (destaqueHome LIKE '%"+DestaqueHome+"%')");
			}

			if (!RevistaArtigoIdAssociado.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaArtigoIdAssociado="+RevistaArtigoIdAssociado+")");
			}

			if (!ConteudoOnline.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoOnline LIKE '%"+ConteudoOnline+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
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
