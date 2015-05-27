
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
	public partial class MunicipioFH : IFilterHelper
	{
		private string _municipioId;
		public string MunicipioId {
			get { return _municipioId==null?String.Empty:_municipioId; }
			set { _municipioId=value; }
		}

		private string _nomeMunicipio;
		public string NomeMunicipio {
			get { return _nomeMunicipio==null?String.Empty:_nomeMunicipio; }
			set { _nomeMunicipio=value; }
		}

		private string _regiaoId;
		public string RegiaoId {
			get { return _regiaoId==null?String.Empty:_regiaoId; }
			set { _regiaoId=value; }
		}

		private string _codigoIbge;
		public string CodigoIbge {
			get { return _codigoIbge==null?String.Empty:_codigoIbge; }
			set { _codigoIbge=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!MunicipioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (municipioId="+MunicipioId+")");
			}

			if (!NomeMunicipio.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeMunicipio LIKE '%"+NomeMunicipio+"%')");
			}

			if (!RegiaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (regiaoId="+RegiaoId+")");
			}

			if (!CodigoIbge.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoIbge="+CodigoIbge+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
