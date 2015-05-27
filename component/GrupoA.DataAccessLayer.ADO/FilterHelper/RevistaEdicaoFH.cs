
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
	public partial class RevistaEdicaoFH : IFilterHelper
	{
		private string _revistaEdicaoId;
		public string RevistaEdicaoId {
			get { return _revistaEdicaoId==null?String.Empty:_revistaEdicaoId; }
			set { _revistaEdicaoId=value; }
		}

		private string _revistaId;
		public string RevistaId {
			get { return _revistaId==null?String.Empty:_revistaId; }
			set { _revistaId=value; }
		}

		private string _numeroEdicao;
		public string NumeroEdicao {
			get { return _numeroEdicao==null?String.Empty:_numeroEdicao; }
			set { _numeroEdicao=value; }
		}

		private string _anoPublicacao;
		public string AnoPublicacao {
			get { return _anoPublicacao==null?String.Empty:_anoPublicacao; }
			set { _anoPublicacao=value; }
		}

		private string _mesPublicacao;
		public string MesPublicacao {
			get { return _mesPublicacao==null?String.Empty:_mesPublicacao; }
			set { _mesPublicacao=value; }
		}

		private string _periodoPublicacao;
		public string PeriodoPublicacao {
			get { return _periodoPublicacao==null?String.Empty:_periodoPublicacao; }
			set { _periodoPublicacao=value; }
		}

		private string _anoEdicao;
		public string AnoEdicao {
			get { return _anoEdicao==null?String.Empty:_anoEdicao; }
			set { _anoEdicao=value; }
		}

		private string _tituloEdicao;
		public string TituloEdicao {
			get { return _tituloEdicao==null?String.Empty:_tituloEdicao; }
			set { _tituloEdicao=value; }
		}

		private string _descricaoEdicao;
		public string DescricaoEdicao {
			get { return _descricaoEdicao==null?String.Empty:_descricaoEdicao; }
			set { _descricaoEdicao=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _numeroPaginas;
		public string NumeroPaginas {
			get { return _numeroPaginas==null?String.Empty:_numeroPaginas; }
			set { _numeroPaginas=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaEdicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaEdicaoId="+RevistaEdicaoId+")");
			}

			if (!RevistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaId="+RevistaId+")");
			}

			if (!NumeroEdicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroEdicao="+NumeroEdicao+")");
			}

			if (!AnoPublicacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (anoPublicacao="+AnoPublicacao+")");
			}

			if (!MesPublicacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (mesPublicacao="+MesPublicacao+")");
			}

			if (!PeriodoPublicacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (periodoPublicacao LIKE '%"+PeriodoPublicacao+"%')");
			}

			if (!AnoEdicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (anoEdicao LIKE '%"+AnoEdicao+"%')");
			}

			if (!TituloEdicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloEdicao LIKE '%"+TituloEdicao+"%')");
			}

			if (!DescricaoEdicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoEdicao LIKE '%"+DescricaoEdicao+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if (!NumeroPaginas.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroPaginas="+NumeroPaginas+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
