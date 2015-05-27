
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
	public partial class FabricanteFH : IFilterHelper
	{
		private string _fabricanteId;
		public string FabricanteId {
			get { return _fabricanteId==null?String.Empty:_fabricanteId; }
			set { _fabricanteId=value; }
		}

		private string _nomeFabricante;
		public string NomeFabricante {
			get { return _nomeFabricante==null?String.Empty:_nomeFabricante; }
			set { _nomeFabricante=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!FabricanteId.Equals(String.Empty)) {
				sbWhere.Append(" AND (fabricanteId="+FabricanteId+")");
			}

			if (!NomeFabricante.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeFabricante LIKE '%"+NomeFabricante+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
