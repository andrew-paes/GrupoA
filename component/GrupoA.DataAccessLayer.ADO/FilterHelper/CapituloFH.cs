
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
	public partial class CapituloFH : IFilterHelper
	{
		private string _capituloId;
		public string CapituloId {
			get { return _capituloId==null?String.Empty:_capituloId; }
			set { _capituloId=value; }
		}

		private string _nomeCapitulo;
		public string NomeCapitulo {
			get { return _nomeCapitulo==null?String.Empty:_nomeCapitulo; }
			set { _nomeCapitulo=value; }
		}

		private string _numeroPaginaCapitulo;
		public string NumeroPaginaCapitulo {
			get { return _numeroPaginaCapitulo==null?String.Empty:_numeroPaginaCapitulo; }
			set { _numeroPaginaCapitulo=value; }
		}

		private string _resumoCapitulo;
		public string ResumoCapitulo {
			get { return _resumoCapitulo==null?String.Empty:_resumoCapitulo; }
			set { _resumoCapitulo=value; }
		}

		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

		private string _codigoLegado;
		public string CodigoLegado {
			get { return _codigoLegado==null?String.Empty:_codigoLegado; }
			set { _codigoLegado=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CapituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (capituloId="+CapituloId+")");
			}

			if (!NomeCapitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeCapitulo LIKE '%"+NomeCapitulo+"%')");
			}

			if (!NumeroPaginaCapitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroPaginaCapitulo="+NumeroPaginaCapitulo+")");
			}

			if (!ResumoCapitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (resumoCapitulo LIKE '%"+ResumoCapitulo+"%')");
			}

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

			if (!CodigoLegado.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoLegado LIKE '%"+CodigoLegado+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
