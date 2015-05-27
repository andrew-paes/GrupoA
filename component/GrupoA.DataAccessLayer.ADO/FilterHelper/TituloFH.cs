
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
	public partial class TituloFH : IFilterHelper
	{
		private string _tituloId;
		public string TituloId {
			get { return _tituloId==null?String.Empty:_tituloId; }
			set { _tituloId=value; }
		}

		private string _subtituloLivro;
		public string SubtituloLivro {
			get { return _subtituloLivro==null?String.Empty:_subtituloLivro; }
			set { _subtituloLivro=value; }
		}

		private string _numeroPaginas;
		public string NumeroPaginas {
			get { return _numeroPaginas==null?String.Empty:_numeroPaginas; }
			set { _numeroPaginas=value; }
		}

		private string _edicao;
		public string Edicao {
			get { return _edicao==null?String.Empty:_edicao; }
			set { _edicao=value; }
		}

		private string _dataLancamentoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataLancamentoPeriodoInicial {
			get { return _dataLancamentoPeriodoInicial==null?String.Empty:_dataLancamentoPeriodoInicial; }
			set { _dataLancamentoPeriodoInicial=value; }
		}
		private string _dataLancamentoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataLancamentoPeriodoFinal {
			get { return _dataLancamentoPeriodoFinal==null?String.Empty:_dataLancamentoPeriodoFinal; }
			set { _dataLancamentoPeriodoFinal=value; }
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

		private string _maisVendido;
		public string MaisVendido {
			get { return _maisVendido==null?String.Empty:_maisVendido; }
			set { _maisVendido=value; }
		}

		private string _nomeTitulo;
		public string NomeTitulo {
			get { return _nomeTitulo==null?String.Empty:_nomeTitulo; }
			set { _nomeTitulo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloId="+TituloId+")");
			}

			if (!SubtituloLivro.Equals(String.Empty)) {
				sbWhere.Append(" AND (subtituloLivro LIKE '%"+SubtituloLivro+"%')");
			}

			if (!NumeroPaginas.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroPaginas="+NumeroPaginas+")");
			}

			if (!Edicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (edicao="+Edicao+")");
			}

			if( !DataLancamentoPeriodoInicial.Equals(String.Empty) && !DataLancamentoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataLancamento >='"+DataLancamentoPeriodoInicial+"'");
				sbWhere.Append(" AND dataLancamento <='"+DataLancamentoPeriodoFinal+"')");
			} else if (!DataLancamentoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataLancamento >='"+DataLancamentoPeriodoInicial+"')");
			} else if (!DataLancamentoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataLancamento <='"+DataLancamentoPeriodoFinal+"')");
			}

			if( !DataPublicacaoPeriodoInicial.Equals(String.Empty) && !DataPublicacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataPublicacao >='"+DataPublicacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataPublicacao <='"+DataPublicacaoPeriodoFinal+"')");
			} else if (!DataPublicacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataPublicacao >='"+DataPublicacaoPeriodoInicial+"')");
			} else if (!DataPublicacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataPublicacao <='"+DataPublicacaoPeriodoFinal+"')");
			}

			if (!MaisVendido.Equals(String.Empty)) {
				sbWhere.Append(" AND (maisVendido LIKE '%"+MaisVendido+"%')");
			}

			if (!NomeTitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeTitulo LIKE '%"+NomeTitulo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
