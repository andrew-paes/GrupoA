
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
	public partial class EventoImagemFH : IFilterHelper
	{
		private string _eventoImagemId;
		public string EventoImagemId {
			get { return _eventoImagemId==null?String.Empty:_eventoImagemId; }
			set { _eventoImagemId=value; }
		}

		private string _eventoId;
		public string EventoId {
			get { return _eventoId==null?String.Empty:_eventoId; }
			set { _eventoId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _ordemApresentacao;
		public string OrdemApresentacao {
			get { return _ordemApresentacao==null?String.Empty:_ordemApresentacao; }
			set { _ordemApresentacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EventoImagemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (eventoImagemId="+EventoImagemId+")");
			}

			if (!EventoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (eventoId="+EventoId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!OrdemApresentacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (ordemApresentacao="+OrdemApresentacao+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
