
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
	public partial class TituloConteudoExtraArquivoFH : IFilterHelper
	{
		private string _tituloConteudoExtraArquivoId;
		public string TituloConteudoExtraArquivoId {
			get { return _tituloConteudoExtraArquivoId==null?String.Empty:_tituloConteudoExtraArquivoId; }
			set { _tituloConteudoExtraArquivoId=value; }
		}

		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

        private string _ativo;
        public string Ativo
        {
            get { return _ativo == null ? String.Empty : _ativo; }
            set { _ativo = value; }
        }

		private string _somenteLogado;
		public string SomenteLogado {
			get { return _somenteLogado==null?String.Empty:_somenteLogado; }
			set { _somenteLogado=value; }
		}

		private string _restritoProfessor;
		public string RestritoProfessor {
			get { return _restritoProfessor==null?String.Empty:_restritoProfessor; }
			set { _restritoProfessor=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _nomeConteudo;
		public string NomeConteudo {
			get { return _nomeConteudo==null?String.Empty:_nomeConteudo; }
			set { _nomeConteudo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloConteudoExtraArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloConteudoExtraArquivoId="+TituloConteudoExtraArquivoId+")");
			}

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

            if (!Ativo.Equals(String.Empty))
            {
                sbWhere.Append(" AND (ativo=" + Ativo + ")");
            }

			if (!SomenteLogado.Equals(String.Empty)) {
				sbWhere.Append(" AND (somenteLogado LIKE '%"+SomenteLogado+"%')");
			}

			if (!RestritoProfessor.Equals(String.Empty)) {
				sbWhere.Append(" AND (restritoProfessor LIKE '%"+RestritoProfessor+"%')");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!NomeConteudo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeConteudo LIKE '%"+NomeConteudo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
