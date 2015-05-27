
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
	public partial class RevistaFH : IFilterHelper
	{
		private string _revistaId;
		public string RevistaId {
			get { return _revistaId==null?String.Empty:_revistaId; }
			set { _revistaId=value; }
		}

		private string _nomeRevista;
		public string NomeRevista {
			get { return _nomeRevista==null?String.Empty:_nomeRevista; }
			set { _nomeRevista=value; }
		}

		private string _periodicidade;
		public string Periodicidade {
			get { return _periodicidade==null?String.Empty:_periodicidade; }
			set { _periodicidade=value; }
		}

		private string _descricaoRevista;
		public string DescricaoRevista {
			get { return _descricaoRevista==null?String.Empty:_descricaoRevista; }
			set { _descricaoRevista=value; }
		}

		private string _publicoAlvo;
		public string PublicoAlvo {
			get { return _publicoAlvo==null?String.Empty:_publicoAlvo; }
			set { _publicoAlvo=value; }
		}

		private string _iSSN;
		public string ISSN {
			get { return _iSSN==null?String.Empty:_iSSN; }
			set { _iSSN=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!RevistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (revistaId="+RevistaId+")");
			}

			if (!NomeRevista.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeRevista LIKE '%"+NomeRevista+"%')");
			}

			if (!Periodicidade.Equals(String.Empty)) {
				sbWhere.Append(" AND (periodicidade="+Periodicidade+")");
			}

			if (!DescricaoRevista.Equals(String.Empty)) {
				sbWhere.Append(" AND (descricaoRevista LIKE '%"+DescricaoRevista+"%')");
			}

			if (!PublicoAlvo.Equals(String.Empty)) {
				sbWhere.Append(" AND (publicoAlvo LIKE '%"+PublicoAlvo+"%')");
			}

			if (!ISSN.Equals(String.Empty)) {
				sbWhere.Append(" AND (ISSN LIKE '%"+ISSN+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
