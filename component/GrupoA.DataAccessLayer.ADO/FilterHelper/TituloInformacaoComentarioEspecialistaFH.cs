
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
	public partial class TituloInformacaoComentarioEspecialistaFH : IFilterHelper
	{
		private string _tituloInformacaoComentarioEspecialistaId;
		public string TituloInformacaoComentarioEspecialistaId {
			get { return _tituloInformacaoComentarioEspecialistaId==null?String.Empty:_tituloInformacaoComentarioEspecialistaId; }
			set { _tituloInformacaoComentarioEspecialistaId=value; }
		}

		private string _textoComentario;
		public string TextoComentario {
			get { return _textoComentario==null?String.Empty:_textoComentario; }
			set { _textoComentario=value; }
		}

		private string _tituloComentario;
		public string TituloComentario {
			get { return _tituloComentario==null?String.Empty:_tituloComentario; }
			set { _tituloComentario=value; }
		}

		private string _urlMidia;
		public string UrlMidia {
			get { return _urlMidia==null?String.Empty:_urlMidia; }
			set { _urlMidia=value; }
		}

		private string _arquivoIdAudio;
		public string ArquivoIdAudio {
			get { return _arquivoIdAudio==null?String.Empty:_arquivoIdAudio; }
			set { _arquivoIdAudio=value; }
		}

		private string _arquivoIdImagem;
		public string ArquivoIdImagem {
			get { return _arquivoIdImagem==null?String.Empty:_arquivoIdImagem; }
			set { _arquivoIdImagem=value; }
		}

		private string _destaqueAreaConhecimento;
		public string DestaqueAreaConhecimento {
			get { return _destaqueAreaConhecimento==null?String.Empty:_destaqueAreaConhecimento; }
			set { _destaqueAreaConhecimento=value; }
		}

		private string _nomeEspecialista;
		public string NomeEspecialista {
			get { return _nomeEspecialista==null?String.Empty:_nomeEspecialista; }
			set { _nomeEspecialista=value; }
		}

		private string _especialidade;
		public string Especialidade {
			get { return _especialidade==null?String.Empty:_especialidade; }
			set { _especialidade=value; }
		}

		private string _comentarioFormatoId;
		public string ComentarioFormatoId {
			get { return _comentarioFormatoId==null?String.Empty:_comentarioFormatoId; }
			set { _comentarioFormatoId=value; }
		}

		private string _resumoComentario;
		public string ResumoComentario {
			get { return _resumoComentario==null?String.Empty:_resumoComentario; }
			set { _resumoComentario=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoComentarioEspecialistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoComentarioEspecialistaId="+TituloInformacaoComentarioEspecialistaId+")");
			}

			if (!TextoComentario.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoComentario LIKE '%"+TextoComentario+"%')");
			}

			if (!TituloComentario.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloComentario LIKE '%"+TituloComentario+"%')");
			}

			if (!UrlMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlMidia LIKE '%"+UrlMidia+"%')");
			}

			if (!ArquivoIdAudio.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdAudio="+ArquivoIdAudio+")");
			}

			if (!ArquivoIdImagem.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdImagem="+ArquivoIdImagem+")");
			}

			if (!DestaqueAreaConhecimento.Equals(String.Empty)) {
				sbWhere.Append(" AND (destaqueAreaConhecimento LIKE '%"+DestaqueAreaConhecimento+"%')");
			}

			if (!NomeEspecialista.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeEspecialista LIKE '%"+NomeEspecialista+"%')");
			}

			if (!Especialidade.Equals(String.Empty)) {
				sbWhere.Append(" AND (especialidade LIKE '%"+Especialidade+"%')");
			}

			if (!ComentarioFormatoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (comentarioFormatoId="+ComentarioFormatoId+")");
			}

			if (!ResumoComentario.Equals(String.Empty)) {
				sbWhere.Append(" AND (resumoComentario LIKE '%"+ResumoComentario+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
