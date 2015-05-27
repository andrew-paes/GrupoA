
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
	public partial class InstituicaoFH : IFilterHelper
	{
		private string _instituicaoId;
		public string InstituicaoId {
			get { return _instituicaoId==null?String.Empty:_instituicaoId; }
			set { _instituicaoId=value; }
		}

		private string _nomeInstituicao;
		public string NomeInstituicao {
			get { return _nomeInstituicao==null?String.Empty:_nomeInstituicao; }
			set { _nomeInstituicao=value; }
		}

		private string _cnpj;
		public string Cnpj {
			get { return _cnpj==null?String.Empty:_cnpj; }
			set { _cnpj=value; }
		}

		private string _telefoneNumero;
		public string TelefoneNumero {
			get { return _telefoneNumero==null?String.Empty:_telefoneNumero; }
			set { _telefoneNumero=value; }
		}

		private string _emailInstituicao;
		public string EmailInstituicao {
			get { return _emailInstituicao==null?String.Empty:_emailInstituicao; }
			set { _emailInstituicao=value; }
		}

		private string _urlSiteInstituicao;
		public string UrlSiteInstituicao {
			get { return _urlSiteInstituicao==null?String.Empty:_urlSiteInstituicao; }
			set { _urlSiteInstituicao=value; }
		}

		private string _codigoInstituicao;
		public string CodigoInstituicao {
			get { return _codigoInstituicao==null?String.Empty:_codigoInstituicao; }
			set { _codigoInstituicao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!InstituicaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (instituicaoId="+InstituicaoId+")");
			}

			if (!NomeInstituicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeInstituicao LIKE '%"+NomeInstituicao+"%')");
			}

			if (!Cnpj.Equals(String.Empty)) {
				sbWhere.Append(" AND (cnpj LIKE '%"+Cnpj+"%')");
			}

			if (!TelefoneNumero.Equals(String.Empty)) {
				sbWhere.Append(" AND (telefoneNumero LIKE '%"+TelefoneNumero+"%')");
			}

			if (!EmailInstituicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (emailInstituicao LIKE '%"+EmailInstituicao+"%')");
			}

			if (!UrlSiteInstituicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (urlSiteInstituicao LIKE '%"+UrlSiteInstituicao+"%')");
			}

			if (!CodigoInstituicao.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoInstituicao LIKE '%"+CodigoInstituicao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
