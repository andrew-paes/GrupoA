
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
	public partial class PromocaoFaixaFH : IFilterHelper
	{
		private string _promocaoFaixaId;
		public string PromocaoFaixaId {
			get { return _promocaoFaixaId==null?String.Empty:_promocaoFaixaId; }
			set { _promocaoFaixaId=value; }
		}

		private string _promocaoId;
		public string PromocaoId {
			get { return _promocaoId==null?String.Empty:_promocaoId; }
			set { _promocaoId=value; }
		}

		private string _valorMinimo;
		public string ValorMinimo {
			get { return _valorMinimo==null?String.Empty:_valorMinimo; }
			set { _valorMinimo=value; }
		}

		private string _percentualDesconto;
		public string PercentualDesconto {
			get { return _percentualDesconto==null?String.Empty:_percentualDesconto; }
			set { _percentualDesconto=value; }
		}

		private string _valorDesconto;
		public string ValorDesconto {
			get { return _valorDesconto==null?String.Empty:_valorDesconto; }
			set { _valorDesconto=value; }
		}

		private string _freteGratis;
		public string FreteGratis {
			get { return _freteGratis==null?String.Empty:_freteGratis; }
			set { _freteGratis=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PromocaoFaixaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoFaixaId="+PromocaoFaixaId+")");
			}

			if (!PromocaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (promocaoId="+PromocaoId+")");
			}

			if (!ValorMinimo.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorMinimo="+ValorMinimo+")");
			}

			if (!PercentualDesconto.Equals(String.Empty)) {
				sbWhere.Append(" AND (percentualDesconto="+PercentualDesconto+")");
			}

			if (!ValorDesconto.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorDesconto="+ValorDesconto+")");
			}

			if (!FreteGratis.Equals(String.Empty)) {
				sbWhere.Append(" AND (freteGratis LIKE '%"+FreteGratis+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
