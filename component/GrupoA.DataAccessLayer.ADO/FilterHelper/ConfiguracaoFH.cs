
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
	public partial class ConfiguracaoFH : IFilterHelper
	{
		private string _configuracaoId;
		public string ConfiguracaoId {
			get { return _configuracaoId==null?String.Empty:_configuracaoId; }
			set { _configuracaoId=value; }
		}

		private string _chave;
		public string Chave {
			get { return _chave==null?String.Empty:_chave; }
			set { _chave=value; }
		}

		private string _descricaoConfiguracao;
		public string DescricaoConfiguracao {
			get { return _descricaoConfiguracao==null?String.Empty:_descricaoConfiguracao; }
			set { _descricaoConfiguracao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ConfiguracaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (configuracaoId="+ConfiguracaoId+")");
			}

			if (!Chave.Equals(String.Empty)) {
				sbWhere.Append(" AND (chave LIKE '%"+Chave+"%')");
			}

			if (!DescricaoConfiguracao.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoConfiguracao LIKE '%"+DescricaoConfiguracao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
