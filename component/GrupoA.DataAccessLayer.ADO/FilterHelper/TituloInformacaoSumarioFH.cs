
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
	public partial class TituloInformacaoSumarioFH : IFilterHelper
	{
		private string _tituloInformacaoSumarioId;
		public string TituloInformacaoSumarioId {
			get { return _tituloInformacaoSumarioId==null?String.Empty:_tituloInformacaoSumarioId; }
			set { _tituloInformacaoSumarioId=value; }
		}

		private string _arquivoIdSumario;
		public string ArquivoIdSumario {
			get { return _arquivoIdSumario==null?String.Empty:_arquivoIdSumario; }
			set { _arquivoIdSumario=value; }
		}

		private string _textoSumario;
		public string TextoSumario {
			get { return _textoSumario==null?String.Empty:_textoSumario; }
			set { _textoSumario=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoSumarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoSumarioId="+TituloInformacaoSumarioId+")");
			}

			if (!ArquivoIdSumario.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdSumario="+ArquivoIdSumario+")");
			}

			if (!TextoSumario.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoSumario LIKE '%"+TextoSumario+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
