
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
	public partial class MidiaCategoriaFH : IFilterHelper
	{
		private string _midiaCategoriaId;
		public string MidiaCategoriaId {
			get { return _midiaCategoriaId==null?String.Empty:_midiaCategoriaId; }
			set { _midiaCategoriaId=value; }
		}

		private string _midiaId;
		public string MidiaId {
			get { return _midiaId==null?String.Empty:_midiaId; }
			set { _midiaId=value; }
		}

		private string _categoriaId;
		public string CategoriaId {
			get { return _categoriaId==null?String.Empty:_categoriaId; }
			set { _categoriaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MidiaCategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaCategoriaId="+MidiaCategoriaId+")");
			}

			if (!MidiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (midiaId="+MidiaId+")");
			}

			if (!CategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaId="+CategoriaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
