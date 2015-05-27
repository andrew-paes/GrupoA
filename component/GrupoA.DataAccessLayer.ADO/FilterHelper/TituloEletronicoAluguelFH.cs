
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
	public partial class TituloEletronicoAluguelFH : IFilterHelper
	{
		private string _tituloEletronicoAluguelId;
		public string TituloEletronicoAluguelId {
			get { return _tituloEletronicoAluguelId==null?String.Empty:_tituloEletronicoAluguelId; }
			set { _tituloEletronicoAluguelId=value; }
		}

		private string _tituloEletronicoId;
		public string TituloEletronicoId {
			get { return _tituloEletronicoId==null?String.Empty:_tituloEletronicoId; }
			set { _tituloEletronicoId=value; }
		}

		private string _tempoAluguel;
		public string TempoAluguel {
			get { return _tempoAluguel==null?String.Empty:_tempoAluguel; }
			set { _tempoAluguel=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloEletronicoAluguelId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloEletronicoAluguelId="+TituloEletronicoAluguelId+")");
			}

			if (!TituloEletronicoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloEletronicoId="+TituloEletronicoId+")");
			}

			if (!TempoAluguel.Equals(String.Empty)) {
				sbWhere.Append(" AND (tempoAluguel="+TempoAluguel+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
