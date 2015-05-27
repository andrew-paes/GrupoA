
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
	public partial class EnderecoFH : IFilterHelper
	{
		private string _enderecoId;
		public string EnderecoId {
			get { return _enderecoId==null?String.Empty:_enderecoId; }
			set { _enderecoId=value; }
		}

		private string _municipioId;
		public string MunicipioId {
			get { return _municipioId==null?String.Empty:_municipioId; }
			set { _municipioId=value; }
		}

		private string _enderecoTipoId;
		public string EnderecoTipoId {
			get { return _enderecoTipoId==null?String.Empty:_enderecoTipoId; }
			set { _enderecoTipoId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _preferencialParaEntrega;
		public string PreferencialParaEntrega {
			get { return _preferencialParaEntrega==null?String.Empty:_preferencialParaEntrega; }
			set { _preferencialParaEntrega=value; }
		}

		private string _logradouro;
		public string Logradouro {
			get { return _logradouro==null?String.Empty:_logradouro; }
			set { _logradouro=value; }
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

		private string _numero;
		public string Numero {
			get { return _numero==null?String.Empty:_numero; }
			set { _numero=value; }
		}

		private string _nomeEndereco;
		public string NomeEndereco {
			get { return _nomeEndereco==null?String.Empty:_nomeEndereco; }
			set { _nomeEndereco=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!EnderecoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enderecoId="+EnderecoId+")");
			}

			if (!MunicipioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (municipioId="+MunicipioId+")");
			}

			if (!EnderecoTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (enderecoTipoId="+EnderecoTipoId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if (!PreferencialParaEntrega.Equals(String.Empty)) {
				sbWhere.Append(" AND (preferencialParaEntrega LIKE '%"+PreferencialParaEntrega+"%')");
			}

			if (!Logradouro.Equals(String.Empty)) {
				sbWhere.Append(" AND (logradouro LIKE '%"+Logradouro+"%')");
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

			if (!Numero.Equals(String.Empty)) {
				sbWhere.Append(" AND (numero LIKE '%"+Numero+"%')");
			}

			if (!NomeEndereco.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeEndereco LIKE '%"+NomeEndereco+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
