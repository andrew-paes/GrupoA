
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
	public partial class ArquivoFH : IFilterHelper
	{
		private string _arquivoId;
		public string ArquivoId {
			get { return _arquivoId==null?String.Empty:_arquivoId; }
			set { _arquivoId=value; }
		}

		private string _tamanhoArquivo;
		public string TamanhoArquivo {
			get { return _tamanhoArquivo==null?String.Empty:_tamanhoArquivo; }
			set { _tamanhoArquivo=value; }
		}

		private string _dataHoraUploadPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraUploadPeriodoInicial {
			get { return _dataHoraUploadPeriodoInicial==null?String.Empty:_dataHoraUploadPeriodoInicial; }
			set { _dataHoraUploadPeriodoInicial=value; }
		}
		private string _dataHoraUploadPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraUploadPeriodoFinal {
			get { return _dataHoraUploadPeriodoFinal==null?String.Empty:_dataHoraUploadPeriodoFinal; }
			set { _dataHoraUploadPeriodoFinal=value; }
		}

		private string _nomeArquivo;
		public string NomeArquivo {
			get { return _nomeArquivo==null?String.Empty:_nomeArquivo; }
			set { _nomeArquivo=value; }
		}

		private string _nomeArquivoOriginal;
		public string NomeArquivoOriginal {
			get { return _nomeArquivoOriginal==null?String.Empty:_nomeArquivoOriginal; }
			set { _nomeArquivoOriginal=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ArquivoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoId="+ArquivoId+")");
			}

			if (!TamanhoArquivo.Equals(String.Empty)) {
				sbWhere.Append(" AND (tamanhoArquivo="+TamanhoArquivo+")");
			}

			if( !DataHoraUploadPeriodoInicial.Equals(String.Empty) && !DataHoraUploadPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraUpload >='"+DataHoraUploadPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraUpload <='"+DataHoraUploadPeriodoFinal+"')");
			} else if (!DataHoraUploadPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraUpload >='"+DataHoraUploadPeriodoInicial+"')");
			} else if (!DataHoraUploadPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraUpload <='"+DataHoraUploadPeriodoFinal+"')");
			}

			if (!NomeArquivo.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeArquivo LIKE '%"+NomeArquivo+"%')");
			}

			if (!NomeArquivoOriginal.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeArquivoOriginal LIKE '%"+NomeArquivoOriginal+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
