
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
	public partial class ConfiguracaoValorFH : IFilterHelper
	{
		private string _configuracaoId;
		public string ConfiguracaoId {
			get { return _configuracaoId==null?String.Empty:_configuracaoId; }
			set { _configuracaoId=value; }
		}

		private string _valor;
		public string Valor {
			get { return _valor==null?String.Empty:_valor; }
			set { _valor=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ConfiguracaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (configuracaoId="+ConfiguracaoId+")");
			}

			if (!Valor.Equals(String.Empty)) {
				sbWhere.Append(" AND (valor LIKE '%"+Valor+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
