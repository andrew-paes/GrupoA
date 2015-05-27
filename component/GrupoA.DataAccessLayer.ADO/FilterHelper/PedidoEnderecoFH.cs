
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
	public partial class PedidoEnderecoFH : IFilterHelper
	{
		private string _pedidoEnderecoId;
		public string PedidoEnderecoId {
			get { return _pedidoEnderecoId==null?String.Empty:_pedidoEnderecoId; }
			set { _pedidoEnderecoId=value; }
		}

		private string _pedidoId;
		public string PedidoId {
			get { return _pedidoId==null?String.Empty:_pedidoId; }
			set { _pedidoId=value; }
		}

		private string _enderecoTipoId;
		public string EnderecoTipoId {
			get { return _enderecoTipoId==null?String.Empty:_enderecoTipoId; }
			set { _enderecoTipoId=value; }
		}

		private string _municipioId;
		public string MunicipioId {
			get { return _municipioId==null?String.Empty:_municipioId; }
			set { _municipioId=value; }
		}

		private string _bairro;
		public string Bairro {
			get { return _bairro==null?String.Empty:_bairro; }
			set { _bairro=value; }
		}

		private string _cep;
		public string Cep {
			get { return _cep==null?String.Empty:_cep; }
			set { _cep=value; }
		}

		private string _complemento;
		public string Complemento {
			get { return _complemento==null?String.Empty:_complemento; }
			set { _complemento=value; }
		}

		private string _logradouro;
		public string Logradouro {
			get { return _logradouro==null?String.Empty:_logradouro; }
			set { _logradouro=value; }
		}

		private string _numero;
		public string Numero {
			get { return _numero==null?String.Empty:_numero; }
			set { _numero=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoEnderecoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoEnderecoId="+PedidoEnderecoId+")");
			}

			if (!PedidoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoId="+PedidoId+")");
			}

			if (!EnderecoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enderecoTipoId="+EnderecoTipoId+")");
			}

			if (!MunicipioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (municipioId="+MunicipioId+")");
			}

			if (!Bairro.Equals(String.Empty)) {
				sbWhere.Append(" AND (bairro LIKE '%"+Bairro+"%')");
			}

			if (!Cep.Equals(String.Empty)) {
				sbWhere.Append(" AND (cep LIKE '%"+Cep+"%')");
			}

			if (!Complemento.Equals(String.Empty)) {
				sbWhere.Append(" AND (complemento LIKE '%"+Complemento+"%')");
			}

			if (!Logradouro.Equals(String.Empty)) {
				sbWhere.Append(" AND (logradouro LIKE '%"+Logradouro+"%')");
			}

			if (!Numero.Equals(String.Empty)) {
				sbWhere.Append(" AND (numero LIKE '%"+Numero+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
