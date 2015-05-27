
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
	public partial class TituloConteudoExtraMidiaFH : IFilterHelper
	{
		private string _tituloConteudoExtraMidiaId;
		public string TituloConteudoExtraMidiaId {
			get { return _tituloConteudoExtraMidiaId==null?String.Empty:_tituloConteudoExtraMidiaId; }
			set { _tituloConteudoExtraMidiaId=value; }
		}

		private string _informacao;
		public string Informacao {
			get { return _informacao==null?String.Empty:_informacao; }
			set { _informacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloConteudoExtraMidiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloConteudoExtraMidiaId="+TituloConteudoExtraMidiaId+")");
			}

			if (!Informacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (informacao LIKE '%"+Informacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
