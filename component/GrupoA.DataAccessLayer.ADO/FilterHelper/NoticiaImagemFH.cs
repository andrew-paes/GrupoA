
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
	public partial class NoticiaImagemFH : IFilterHelper
	{
		private string _noticiaImagemId;
		public string NoticiaImagemId {
			get { return _noticiaImagemId==null?String.Empty:_noticiaImagemId; }
			set { _noticiaImagemId=value; }
		}

		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _noticiaId;
		public string NoticiaId {
			get { return _noticiaId==null?String.Empty:_noticiaId; }
			set { _noticiaId=value; }
		}

		private string _ordemApresentacao;
		public string OrdemApresentacao {
			get { return _ordemApresentacao==null?String.Empty:_ordemApresentacao; }
			set { _ordemApresentacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!NoticiaImagemId.Equals(String.Empty)) {
				sbWhere.Append(" AND (noticiaImagemId="+NoticiaImagemId+")");
			}

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!NoticiaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (noticiaId="+NoticiaId+")");
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
