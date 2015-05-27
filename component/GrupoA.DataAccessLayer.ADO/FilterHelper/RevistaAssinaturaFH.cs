
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
	public partial class RevistaAssinaturaFH : IFilterHelper
	{
		private string _revistaAssinaturaId;
		public string RevistaAssinaturaId {
			get { return _revistaAssinaturaId==null?String.Empty:_revistaAssinaturaId; }
			set { _revistaAssinaturaId=value; }
		}

		private string _revistaId;
		public string RevistaId {
			get { return _revistaId==null?String.Empty:_revistaId; }
			set { _revistaId=value; }
		}

		private string _numeroExemplares;
		public string NumeroExemplares {
			get { return _numeroExemplares==null?String.Empty:_numeroExemplares; }
			set { _numeroExemplares=value; }
		}

		private string _descricaoAssinatura;
		public string DescricaoAssinatura {
			get { return _descricaoAssinatura==null?String.Empty:_descricaoAssinatura; }
			set { _descricaoAssinatura=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaAssinaturaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaAssinaturaId="+RevistaAssinaturaId+")");
			}

			if (!RevistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaId="+RevistaId+")");
			}

			if (!NumeroExemplares.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroExemplares="+NumeroExemplares+")");
			}

			if (!DescricaoAssinatura.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoAssinatura LIKE '%"+DescricaoAssinatura+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
