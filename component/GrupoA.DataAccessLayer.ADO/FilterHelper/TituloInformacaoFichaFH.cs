
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
	public partial class TituloInformacaoFichaFH : IFilterHelper
	{
		private string _tituloInformacaoFichaId;
		public string TituloInformacaoFichaId {
			get { return _tituloInformacaoFichaId==null?String.Empty:_tituloInformacaoFichaId; }
			set { _tituloInformacaoFichaId=value; }
		}

		private string _textoFichaTecnica;
		public string TextoFichaTecnica {
			get { return _textoFichaTecnica==null?String.Empty:_textoFichaTecnica; }
			set { _textoFichaTecnica=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoFichaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoFichaId="+TituloInformacaoFichaId+")");
			}

			if (!TextoFichaTecnica.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoFichaTecnica LIKE '%"+TextoFichaTecnica+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
