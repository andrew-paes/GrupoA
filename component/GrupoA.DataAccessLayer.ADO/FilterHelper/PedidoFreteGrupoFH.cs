
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
	public partial class PedidoFreteGrupoFH : IFilterHelper
	{
		private string _pedidoFreteGrupoId;
		public string PedidoFreteGrupoId {
			get { return _pedidoFreteGrupoId==null?String.Empty:_pedidoFreteGrupoId; }
			set { _pedidoFreteGrupoId=value; }
		}

		private string _nomeGrupo;
		public string NomeGrupo {
			get { return _nomeGrupo==null?String.Empty:_nomeGrupo; }
			set { _nomeGrupo=value; }
		}

		private string _pedidoFreteTipoId;
		public string PedidoFreteTipoId {
			get { return _pedidoFreteTipoId==null?String.Empty:_pedidoFreteTipoId; }
			set { _pedidoFreteTipoId=value; }
		}

		private string _cepInicial1;
		public string CepInicial1 {
			get { return _cepInicial1==null?String.Empty:_cepInicial1; }
			set { _cepInicial1=value; }
		}

		private string _cepInicial2;
		public string CepInicial2 {
			get { return _cepInicial2==null?String.Empty:_cepInicial2; }
			set { _cepInicial2=value; }
		}

		private string _cepFinal1;
		public string CepFinal1 {
			get { return _cepFinal1==null?String.Empty:_cepFinal1; }
			set { _cepFinal1=value; }
		}

		private string _cepFinal2;
		public string CepFinal2 {
			get { return _cepFinal2==null?String.Empty:_cepFinal2; }
			set { _cepFinal2=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!PedidoFreteGrupoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (pedidoFreteGrupoId="+PedidoFreteGrupoId+")");
			}

			if (!NomeGrupo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeGrupo LIKE '%"+NomeGrupo+"%')");
			}

			if (!PedidoFreteTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (PedidoFreteTipoId LIKE '%"+PedidoFreteTipoId+"%')");
			}

			if (!CepInicial1.Equals(String.Empty)) {
				sbWhere.Append(" AND (cepInicial1="+CepInicial1+")");
			}

			if (!CepInicial2.Equals(String.Empty)) {
				sbWhere.Append(" AND (cepInicial2="+CepInicial2+")");
			}

			if (!CepFinal1.Equals(String.Empty)) {
				sbWhere.Append(" AND (cepFinal1="+CepFinal1+")");
			}

			if (!CepFinal2.Equals(String.Empty)) {
				sbWhere.Append(" AND (cepFinal2="+CepFinal2+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
