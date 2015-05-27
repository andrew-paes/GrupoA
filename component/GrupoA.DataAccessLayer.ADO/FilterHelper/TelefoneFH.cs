
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
	public partial class TelefoneFH : IFilterHelper
	{
		private string _telefoneId;
		public string TelefoneId {
			get { return _telefoneId==null?String.Empty:_telefoneId; }
			set { _telefoneId=value; }
		}

		private string _numeroTelefone;
		public string NumeroTelefone {
			get { return _numeroTelefone==null?String.Empty:_numeroTelefone; }
			set { _numeroTelefone=value; }
		}

		private string _dddTelefone;
		public string DddTelefone {
			get { return _dddTelefone==null?String.Empty:_dddTelefone; }
			set { _dddTelefone=value; }
		}

		private string _telefoneTipoId;
		public string TelefoneTipoId {
			get { return _telefoneTipoId==null?String.Empty:_telefoneTipoId; }
			set { _telefoneTipoId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _ramal;
		public string Ramal {
			get { return _ramal==null?String.Empty:_ramal; }
			set { _ramal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TelefoneId.Equals(String.Empty)) {
				sbWhere.Append(" AND (telefoneId="+TelefoneId+")");
			}

			if (!NumeroTelefone.Equals(String.Empty)) {
				sbWhere.Append(" AND (numeroTelefone LIKE '%"+NumeroTelefone+"%')");
			}

			if (!DddTelefone.Equals(String.Empty)) {
				sbWhere.Append(" AND (dddTelefone LIKE '%"+DddTelefone+"%')");
			}

			if (!TelefoneTipoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (telefoneTipoId="+TelefoneTipoId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if (!Ramal.Equals(String.Empty)) {
				sbWhere.Append(" AND (ramal LIKE '%"+Ramal+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
