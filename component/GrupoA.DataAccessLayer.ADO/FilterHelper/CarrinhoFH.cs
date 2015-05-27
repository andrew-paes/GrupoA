
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
	public partial class CarrinhoFH : IFilterHelper
	{
		private string _carrinhoId;
		public string CarrinhoId {
			get { return _carrinhoId==null?String.Empty:_carrinhoId; }
			set { _carrinhoId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _dataHoraCriacaoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCriacaoPeriodoInicial {
			get { return _dataHoraCriacaoPeriodoInicial==null?String.Empty:_dataHoraCriacaoPeriodoInicial; }
			set { _dataHoraCriacaoPeriodoInicial=value; }
		}
		private string _dataHoraCriacaoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCriacaoPeriodoFinal {
			get { return _dataHoraCriacaoPeriodoFinal==null?String.Empty:_dataHoraCriacaoPeriodoFinal; }
			set { _dataHoraCriacaoPeriodoFinal=value; }
		}

		private string _carrinhoStatusId;
		public string CarrinhoStatusId {
			get { return _carrinhoStatusId==null?String.Empty:_carrinhoStatusId; }
			set { _carrinhoStatusId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!CarrinhoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoId="+CarrinhoId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if( !DataHoraCriacaoPeriodoInicial.Equals(String.Empty) && !DataHoraCriacaoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraCriacao >='"+DataHoraCriacaoPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraCriacao <='"+DataHoraCriacaoPeriodoFinal+"')");
			} else if (!DataHoraCriacaoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCriacao >='"+DataHoraCriacaoPeriodoInicial+"')");
			} else if (!DataHoraCriacaoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCriacao <='"+DataHoraCriacaoPeriodoFinal+"')");
			}

			if (!CarrinhoStatusId.Equals(String.Empty)) {
				sbWhere.Append(" AND (carrinhoStatusId="+CarrinhoStatusId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
