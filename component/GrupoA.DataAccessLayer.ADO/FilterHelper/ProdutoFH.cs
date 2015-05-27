
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
	public partial class ProdutoFH : IFilterHelper
	{
		private string _produtoId;
		public string ProdutoId {
			get { return _produtoId==null?String.Empty:_produtoId; }
			set { _produtoId=value; }
		}

		private string _produtoTipoId;
		public string ProdutoTipoId {
			get { return _produtoTipoId==null?String.Empty:_produtoTipoId; }
			set { _produtoTipoId=value; }
		}

		private string _disponivel;
		public string Disponivel {
			get { return _disponivel==null?String.Empty:_disponivel; }
			set { _disponivel=value; }
		}

		private string _fabricanteId;
		public string FabricanteId {
			get { return _fabricanteId==null?String.Empty:_fabricanteId; }
			set { _fabricanteId=value; }
		}

		private string _valorUnitario;
		public string ValorUnitario {
			get { return _valorUnitario==null?String.Empty:_valorUnitario; }
			set { _valorUnitario=value; }
		}

		private string _valorOferta;
		public string ValorOferta {
			get { return _valorOferta==null?String.Empty:_valorOferta; }
			set { _valorOferta=value; }
		}

		private string _codigoEAN13;
		public string CodigoEAN13 {
			get { return _codigoEAN13==null?String.Empty:_codigoEAN13; }
			set { _codigoEAN13=value; }
		}

		private string _codigoProduto;
		public string CodigoProduto {
			get { return _codigoProduto==null?String.Empty:_codigoProduto; }
			set { _codigoProduto=value; }
		}

		private string _exibirSite;
		public string ExibirSite {
			get { return _exibirSite==null?String.Empty:_exibirSite; }
			set { _exibirSite=value; }
		}

		private string _nomeProduto;
		public string NomeProduto {
			get { return _nomeProduto==null?String.Empty:_nomeProduto; }
			set { _nomeProduto=value; }
		}

		private string _utilizaFrete;
		public string UtilizaFrete {
			get { return _utilizaFrete==null?String.Empty:_utilizaFrete; }
			set { _utilizaFrete=value; }
		}

		private string _peso;
		public string Peso {
			get { return _peso==null?String.Empty:_peso; }
			set { _peso=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProdutoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoId="+ProdutoId+")");
			}

			if (!ProdutoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (produtoTipoId="+ProdutoTipoId+")");
			}

			if (!Disponivel.Equals(String.Empty)) {
				sbWhere.Append(" AND (disponivel LIKE '%"+Disponivel+"%')");
			}

			if (!FabricanteId.Equals(String.Empty)) {
				sbWhere.Append(" AND (fabricanteId="+FabricanteId+")");
			}

			if (!ValorUnitario.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorUnitario="+ValorUnitario+")");
			}

			if (!ValorOferta.Equals(String.Empty)) {
				sbWhere.Append(" AND (valorOferta="+ValorOferta+")");
			}

			if (!CodigoEAN13.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoEAN13 LIKE '%"+CodigoEAN13+"%')");
			}

			if (!CodigoProduto.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoProduto LIKE '%"+CodigoProduto+"%')");
			}

			if (!ExibirSite.Equals(String.Empty)) {
				sbWhere.Append(" AND (exibirSite LIKE '%"+ExibirSite+"%')");
			}

			if (!NomeProduto.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeProduto LIKE '%"+NomeProduto+"%')");
			}

			if (!UtilizaFrete.Equals(String.Empty)) {
				sbWhere.Append(" AND (utilizaFrete LIKE '%"+UtilizaFrete+"%')");
			}

			if (!Peso.Equals(String.Empty)) {
				sbWhere.Append(" AND (peso="+Peso+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
