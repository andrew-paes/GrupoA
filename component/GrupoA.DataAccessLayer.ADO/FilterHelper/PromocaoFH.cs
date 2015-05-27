
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
	public partial class PromocaoFH : IFilterHelper
	{
		private string _promocaoId;
		public string PromocaoId {
			get { return _promocaoId==null?String.Empty:_promocaoId; }
			set { _promocaoId=value; }
		}

		private string _nomePromocao;
		public string NomePromocao {
			get { return _nomePromocao==null?String.Empty:_nomePromocao; }
			set { _nomePromocao=value; }
		}

		private string _codigoPromocao;
		public string CodigoPromocao {
			get { return _codigoPromocao==null?String.Empty:_codigoPromocao; }
			set { _codigoPromocao=value; }
		}

		private string _dataHoraInicioPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraInicioPeriodoInicial {
			get { return _dataHoraInicioPeriodoInicial==null?String.Empty:_dataHoraInicioPeriodoInicial; }
			set { _dataHoraInicioPeriodoInicial=value; }
		}
		private string _dataHoraInicioPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraInicioPeriodoFinal {
			get { return _dataHoraInicioPeriodoFinal==null?String.Empty:_dataHoraInicioPeriodoFinal; }
			set { _dataHoraInicioPeriodoFinal=value; }
		}

		private string _dataHoraFimPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraFimPeriodoInicial {
			get { return _dataHoraFimPeriodoInicial==null?String.Empty:_dataHoraFimPeriodoInicial; }
			set { _dataHoraFimPeriodoInicial=value; }
		}
		private string _dataHoraFimPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraFimPeriodoFinal {
			get { return _dataHoraFimPeriodoFinal==null?String.Empty:_dataHoraFimPeriodoFinal; }
			set { _dataHoraFimPeriodoFinal=value; }
		}

		private string _aplicaAutomaticamente;
		public string AplicaAutomaticamente {
			get { return _aplicaAutomaticamente==null?String.Empty:_aplicaAutomaticamente; }
			set { _aplicaAutomaticamente=value; }
		}

		private string _promocaoTipoId;
		public string PromocaoTipoId {
			get { return _promocaoTipoId==null?String.Empty:_promocaoTipoId; }
			set { _promocaoTipoId=value; }
		}

		private string _ativa;
		public string Ativa {
			get { return _ativa==null?String.Empty:_ativa; }
			set { _ativa=value; }
		}

		private string _descricaoPromocao;
		public string DescricaoPromocao {
			get { return _descricaoPromocao==null?String.Empty:_descricaoPromocao; }
			set { _descricaoPromocao=value; }
		}

		private string _numeroMaximoCupom;
		public string NumeroMaximoCupom {
			get { return _numeroMaximoCupom==null?String.Empty:_numeroMaximoCupom; }
			set { _numeroMaximoCupom=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PromocaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoId="+PromocaoId+")");
			}

			if (!NomePromocao.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomePromocao LIKE '%"+NomePromocao+"%')");
			}

			if (!CodigoPromocao.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoPromocao LIKE '%"+CodigoPromocao+"%')");
			}

			if( !DataHoraInicioPeriodoInicial.Equals(String.Empty) && !DataHoraInicioPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraInicio >='"+DataHoraInicioPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraInicio <='"+DataHoraInicioPeriodoFinal+"')");
			} else if (!DataHoraInicioPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraInicio >='"+DataHoraInicioPeriodoInicial+"')");
			} else if (!DataHoraInicioPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraInicio <='"+DataHoraInicioPeriodoFinal+"')");
			}

			if( !DataHoraFimPeriodoInicial.Equals(String.Empty) && !DataHoraFimPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraFim >='"+DataHoraFimPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraFim <='"+DataHoraFimPeriodoFinal+"')");
			} else if (!DataHoraFimPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraFim >='"+DataHoraFimPeriodoInicial+"')");
			} else if (!DataHoraFimPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraFim <='"+DataHoraFimPeriodoFinal+"')");
			}

			if (!AplicaAutomaticamente.Equals(String.Empty)) {
				sbWhere.Append(" AND (aplicaAutomaticamente LIKE '%"+AplicaAutomaticamente+"%')");
			}

			if (!PromocaoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoTipoId="+PromocaoTipoId+")");
			}

			if (!Ativa.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativa LIKE '%"+Ativa+"%')");
			}

			if (!DescricaoPromocao.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoPromocao LIKE '%"+DescricaoPromocao+"%')");
			}

			if (!NumeroMaximoCupom.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroMaximoCupom="+NumeroMaximoCupom+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
