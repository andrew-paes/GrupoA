
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
	public partial class ProgramaAtualizacaoPaginaFH : IFilterHelper
	{
		private string _programaAtualizacaoPaginaId;
		public string ProgramaAtualizacaoPaginaId {
			get { return _programaAtualizacaoPaginaId==null?String.Empty:_programaAtualizacaoPaginaId; }
			set { _programaAtualizacaoPaginaId=value; }
		}

		private string _pagina;
		public string Pagina {
			get { return _pagina==null?String.Empty:_pagina; }
			set { _pagina=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProgramaAtualizacaoPaginaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (programaAtualizacaoPaginaId="+ProgramaAtualizacaoPaginaId+")");
			}

			if (!Pagina.Equals(String.Empty)) {
				sbWhere.Append(" AND (pagina LIKE '%"+Pagina+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
