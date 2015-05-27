
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
	public partial class NewsletterDestinatarioFH : IFilterHelper
	{
		private string _newsletterDestinatarioId;
		public string NewsletterDestinatarioId {
			get { return _newsletterDestinatarioId==null?String.Empty:_newsletterDestinatarioId; }
			set { _newsletterDestinatarioId=value; }
		}

		private string _emailDestinatario;
		public string EmailDestinatario {
			get { return _emailDestinatario==null?String.Empty:_emailDestinatario; }
			set { _emailDestinatario=value; }
		}

		private string _nomeDestinatario;
		public string NomeDestinatario {
			get { return _nomeDestinatario==null?String.Empty:_nomeDestinatario; }
			set { _nomeDestinatario=value; }
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

			if (!NewsletterDestinatarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (newsletterDestinatarioId="+NewsletterDestinatarioId+")");
			}

			if (!EmailDestinatario.Equals(String.Empty)) {
				sbWhere.Append(" AND (emailDestinatario LIKE '%"+EmailDestinatario+"%')");
			}

			if (!NomeDestinatario.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeDestinatario LIKE '%"+NomeDestinatario+"%')");
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
