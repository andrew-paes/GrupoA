
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
	public partial class ConteudoFH : IFilterHelper
	{
		private string _conteudoId;
		public string ConteudoId {
			get { return _conteudoId==null?String.Empty:_conteudoId; }
			set { _conteudoId=value; }
		}

		private string _conteudoTipoId;
		public string ConteudoTipoId {
			get { return _conteudoTipoId==null?String.Empty:_conteudoTipoId; }
			set { _conteudoTipoId=value; }
		}

		private string _dataHoraCadastroPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCadastroPeriodoInicial {
			get { return _dataHoraCadastroPeriodoInicial==null?String.Empty:_dataHoraCadastroPeriodoInicial; }
			set { _dataHoraCadastroPeriodoInicial=value; }
		}
		private string _dataHoraCadastroPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCadastroPeriodoFinal {
			get { return _dataHoraCadastroPeriodoFinal==null?String.Empty:_dataHoraCadastroPeriodoFinal; }
			set { _dataHoraCadastroPeriodoFinal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ConteudoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoId="+ConteudoId+")");
			}

			if (!ConteudoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoTipoId="+ConteudoTipoId+")");
			}

			if( !DataHoraCadastroPeriodoInicial.Equals(String.Empty) && !DataHoraCadastroPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraCadastro >='"+DataHoraCadastroPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraCadastro <='"+DataHoraCadastroPeriodoFinal+"')");
			} else if (!DataHoraCadastroPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCadastro >='"+DataHoraCadastroPeriodoInicial+"')");
			} else if (!DataHoraCadastroPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCadastro <='"+DataHoraCadastroPeriodoFinal+"')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
