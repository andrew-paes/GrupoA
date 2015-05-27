
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
	public partial class ProfissionalOcupacaoFH : IFilterHelper
	{
		private string _profissionalOcupacaoId;
		public string ProfissionalOcupacaoId {
			get { return _profissionalOcupacaoId==null?String.Empty:_profissionalOcupacaoId; }
			set { _profissionalOcupacaoId=value; }
		}

		private string _ocupacao;
		public string Ocupacao {
			get { return _ocupacao==null?String.Empty:_ocupacao; }
			set { _ocupacao=value; }
		}

		private string _codigoOcupacao;
		public string CodigoOcupacao {
			get { return _codigoOcupacao==null?String.Empty:_codigoOcupacao; }
			set { _codigoOcupacao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProfissionalOcupacaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (profissionalOcupacaoId="+ProfissionalOcupacaoId+")");
			}

			if (!Ocupacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (ocupacao LIKE '%"+Ocupacao+"%')");
			}

			if (!CodigoOcupacao.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoOcupacao LIKE '%"+CodigoOcupacao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
