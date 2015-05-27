
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
	public partial class EnderecoTipoFH : IFilterHelper
	{
		private string _enderecoTipoId;
		public string EnderecoTipoId {
			get { return _enderecoTipoId==null?String.Empty:_enderecoTipoId; }
			set { _enderecoTipoId=value; }
		}

		private string _tipo;
		public string Tipo {
			get { return _tipo==null?String.Empty:_tipo; }
			set { _tipo=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EnderecoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enderecoTipoId="+EnderecoTipoId+")");
			}

			if (!Tipo.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipo LIKE '%"+Tipo+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
