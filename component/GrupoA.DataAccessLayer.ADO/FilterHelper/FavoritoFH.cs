
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
	public partial class FavoritoFH : IFilterHelper
	{
		private string _favoritoId;
		public string FavoritoId {
			get { return _favoritoId==null?String.Empty:_favoritoId; }
			set { _favoritoId=value; }
		}

		private string _conteudoId;
		public string ConteudoId {
			get { return _conteudoId==null?String.Empty:_conteudoId; }
			set { _conteudoId=value; }
		}

		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!FavoritoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (favoritoId="+FavoritoId+")");
			}

			if (!ConteudoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (conteudoId="+ConteudoId+")");
			}

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
