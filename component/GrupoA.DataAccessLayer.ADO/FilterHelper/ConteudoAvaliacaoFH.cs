
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
	public partial class ConteudoAvaliacaoFH : IFilterHelper
	{
		private string _conteudoId;
		public string ConteudoId {
			get { return _conteudoId==null?String.Empty:_conteudoId; }
			set { _conteudoId=value; }
		}

		private string _avaliacoes;
		public string Avaliacoes {
			get { return _avaliacoes==null?String.Empty:_avaliacoes; }
			set { _avaliacoes=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ConteudoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoId="+ConteudoId+")");
			}

			if (!Avaliacoes.Equals(String.Empty)) {
				sbWhere.Append(" AND (avaliacoes="+Avaliacoes+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
