
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
	public partial class TituloInformacaoComentarioEspecialistaCategoriaFH : IFilterHelper
	{
		private string _tituloInformacaoComentarioEspecialistaComentarioId;
		public string TituloInformacaoComentarioEspecialistaComentarioId {
			get { return _tituloInformacaoComentarioEspecialistaComentarioId==null?String.Empty:_tituloInformacaoComentarioEspecialistaComentarioId; }
			set { _tituloInformacaoComentarioEspecialistaComentarioId=value; }
		}

		private string _tituloInformacaoComentarioEspecialistaId;
		public string TituloInformacaoComentarioEspecialistaId {
			get { return _tituloInformacaoComentarioEspecialistaId==null?String.Empty:_tituloInformacaoComentarioEspecialistaId; }
			set { _tituloInformacaoComentarioEspecialistaId=value; }
		}

		private string _categoriaId;
		public string CategoriaId {
			get { return _categoriaId==null?String.Empty:_categoriaId; }
			set { _categoriaId=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoComentarioEspecialistaComentarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoComentarioEspecialistaComentarioId="+TituloInformacaoComentarioEspecialistaComentarioId+")");
			}

			if (!TituloInformacaoComentarioEspecialistaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoComentarioEspecialistaId="+TituloInformacaoComentarioEspecialistaId+")");
			}

			if (!CategoriaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (categoriaId="+CategoriaId+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
