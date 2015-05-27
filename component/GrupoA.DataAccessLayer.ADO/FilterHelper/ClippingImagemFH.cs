
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
	public partial class ClippingImagemFH : IFilterHelper
	{
		private string _clippingImagemId;
		public string ClippingImagemId {
			get { return _clippingImagemId==null?String.Empty:_clippingImagemId; }
			set { _clippingImagemId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _clippingId;
		public string ClippingId {
			get { return _clippingId==null?String.Empty:_clippingId; }
			set { _clippingId=value; }
		}

		private string _ordemApresentacao;
		public string OrdemApresentacao {
			get { return _ordemApresentacao==null?String.Empty:_ordemApresentacao; }
			set { _ordemApresentacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ClippingImagemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (clippingImagemId="+ClippingImagemId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!ClippingId.Equals(String.Empty)) {
				sbWhere.Append(" AND (clippingId="+ClippingId+")");
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
