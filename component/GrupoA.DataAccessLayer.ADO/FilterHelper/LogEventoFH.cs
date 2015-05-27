
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
	public partial class LogEventoFH : IFilterHelper
	{
		private string _logEventoId;
		public string LogEventoId {
			get { return _logEventoId==null?String.Empty:_logEventoId; }
			set { _logEventoId=value; }
		}

		private string _logCategoriaId;
		public string LogCategoriaId {
			get { return _logCategoriaId==null?String.Empty:_logCategoriaId; }
			set { _logCategoriaId=value; }
		}

		private string _evento;
		public string Evento {
			get { return _evento==null?String.Empty:_evento; }
			set { _evento=value; }
		}

		private string _descricaoEvento;
		public string DescricaoEvento {
			get { return _descricaoEvento==null?String.Empty:_descricaoEvento; }
			set { _descricaoEvento=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!LogEventoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (logEventoId="+LogEventoId+")");
			}

			if (!LogCategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (logCategoriaId="+LogCategoriaId+")");
			}

			if (!Evento.Equals(String.Empty)) {
				sbWhere.Append(" AND (evento LIKE '%"+Evento+"%')");
			}

			if (!DescricaoEvento.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoEvento LIKE '%"+DescricaoEvento+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
