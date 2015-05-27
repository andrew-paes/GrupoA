
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
	public partial class RevistaGrupoAEdicaoFH : IFilterHelper
	{
		private string _revistaGrupoAEdicaoId;
		public string RevistaGrupoAEdicaoId {
			get { return _revistaGrupoAEdicaoId==null?String.Empty:_revistaGrupoAEdicaoId; }
			set { _revistaGrupoAEdicaoId=value; }
		}

		private string _mesPublicacao;
		public string MesPublicacao {
			get { return _mesPublicacao==null?String.Empty:_mesPublicacao; }
			set { _mesPublicacao=value; }
		}

		private string _anoPublicacao;
		public string AnoPublicacao {
			get { return _anoPublicacao==null?String.Empty:_anoPublicacao; }
			set { _anoPublicacao=value; }
		}

		private string _numeroEdicao;
		public string NumeroEdicao {
			get { return _numeroEdicao==null?String.Empty:_numeroEdicao; }
			set { _numeroEdicao=value; }
		}

		private string _arquivoIdPequena;
		public string ArquivoIdPequena {
			get { return _arquivoIdPequena==null?String.Empty:_arquivoIdPequena; }
			set { _arquivoIdPequena=value; }
		}

		private string _textoChamada;
		public string TextoChamada {
			get { return _textoChamada==null?String.Empty:_textoChamada; }
			set { _textoChamada=value; }
		}

		private string _urlRevista;
		public string UrlRevista {
			get { return _urlRevista==null?String.Empty:_urlRevista; }
			set { _urlRevista=value; }
		}

		private string _arquivoIdGrande;
		public string ArquivoIdGrande {
			get { return _arquivoIdGrande==null?String.Empty:_arquivoIdGrande; }
			set { _arquivoIdGrande=value; }
		}

		private string _tituloRevista;
		public string TituloRevista {
			get { return _tituloRevista==null?String.Empty:_tituloRevista; }
			set { _tituloRevista=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaGrupoAEdicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaGrupoAEdicaoId="+RevistaGrupoAEdicaoId+")");
			}

			if (!MesPublicacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (mesPublicacao LIKE '%"+MesPublicacao+"%')");
			}

			if (!AnoPublicacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (anoPublicacao LIKE '%"+AnoPublicacao+"%')");
			}

			if (!NumeroEdicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroEdicao="+NumeroEdicao+")");
			}

			if (!ArquivoIdPequena.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdPequena="+ArquivoIdPequena+")");
			}

			if (!TextoChamada.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoChamada LIKE '%"+TextoChamada+"%')");
			}

			if (!UrlRevista.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlRevista LIKE '%"+UrlRevista+"%')");
			}

			if (!ArquivoIdGrande.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdGrande="+ArquivoIdGrande+")");
			}

			if (!TituloRevista.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloRevista LIKE '%"+TituloRevista+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
