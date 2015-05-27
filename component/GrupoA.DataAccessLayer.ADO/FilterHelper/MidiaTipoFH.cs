
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
	public partial class MidiaTipoFH : IFilterHelper
	{
		private string _midiaTipoId;
		public string MidiaTipoId {
			get { return _midiaTipoId==null?String.Empty:_midiaTipoId; }
			set { _midiaTipoId=value; }
		}

		private string _tipoMidia;
		public string TipoMidia {
			get { return _tipoMidia==null?String.Empty:_tipoMidia; }
			set { _tipoMidia=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MidiaTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaTipoId="+MidiaTipoId+")");
			}

			if (!TipoMidia.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipoMidia LIKE '%"+TipoMidia+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
