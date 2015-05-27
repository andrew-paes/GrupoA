
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
	public partial class ProgramaAtualizacaoChamadaFH : IFilterHelper
	{
		private string _programaAtualizacaoChamadaId;
		public string ProgramaAtualizacaoChamadaId {
			get { return _programaAtualizacaoChamadaId==null?String.Empty:_programaAtualizacaoChamadaId; }
			set { _programaAtualizacaoChamadaId=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _primeiraChamadaTitulo;
		public string PrimeiraChamadaTitulo {
			get { return _primeiraChamadaTitulo==null?String.Empty:_primeiraChamadaTitulo; }
			set { _primeiraChamadaTitulo=value; }
		}

		private string _primeiraChamadaTexto;
		public string PrimeiraChamadaTexto {
			get { return _primeiraChamadaTexto==null?String.Empty:_primeiraChamadaTexto; }
			set { _primeiraChamadaTexto=value; }
		}

		private string _primeiraChamadaUrl;
		public string PrimeiraChamadaUrl {
			get { return _primeiraChamadaUrl==null?String.Empty:_primeiraChamadaUrl; }
			set { _primeiraChamadaUrl=value; }
		}

		private string _primeiraChamadaTargetBlank;
		public string PrimeiraChamadaTargetBlank {
			get { return _primeiraChamadaTargetBlank==null?String.Empty:_primeiraChamadaTargetBlank; }
			set { _primeiraChamadaTargetBlank=value; }
		}

		private string _segundaChamadaTitulo;
		public string SegundaChamadaTitulo {
			get { return _segundaChamadaTitulo==null?String.Empty:_segundaChamadaTitulo; }
			set { _segundaChamadaTitulo=value; }
		}

		private string _segundaChamadaTexto;
		public string SegundaChamadaTexto {
			get { return _segundaChamadaTexto==null?String.Empty:_segundaChamadaTexto; }
			set { _segundaChamadaTexto=value; }
		}

		private string _segundaChamadaUrl;
		public string SegundaChamadaUrl {
			get { return _segundaChamadaUrl==null?String.Empty:_segundaChamadaUrl; }
			set { _segundaChamadaUrl=value; }
		}

		private string _segundaChamadaTargetBlank;
		public string SegundaChamadaTargetBlank {
			get { return _segundaChamadaTargetBlank==null?String.Empty:_segundaChamadaTargetBlank; }
			set { _segundaChamadaTargetBlank=value; }
		}

		private string _arquivoIdImagem;
		public string ArquivoIdImagem {
			get { return _arquivoIdImagem==null?String.Empty:_arquivoIdImagem; }
			set { _arquivoIdImagem=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!ProgramaAtualizacaoChamadaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (programaAtualizacaoChamadaId="+ProgramaAtualizacaoChamadaId+")");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if (!PrimeiraChamadaTitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (primeiraChamadaTitulo LIKE '%"+PrimeiraChamadaTitulo+"%')");
			}

			if (!PrimeiraChamadaTexto.Equals(String.Empty)) {
				sbWhere.Append(" AND (primeiraChamadaTexto LIKE '%"+PrimeiraChamadaTexto+"%')");
			}

			if (!PrimeiraChamadaUrl.Equals(String.Empty)) {
				sbWhere.Append(" AND (primeiraChamadaUrl LIKE '%"+PrimeiraChamadaUrl+"%')");
			}

			if (!PrimeiraChamadaTargetBlank.Equals(String.Empty)) {
				sbWhere.Append(" AND (primeiraChamadaTargetBlank LIKE '%"+PrimeiraChamadaTargetBlank+"%')");
			}

			if (!SegundaChamadaTitulo.Equals(String.Empty)) {
				sbWhere.Append(" AND (segundaChamadaTitulo LIKE '%"+SegundaChamadaTitulo+"%')");
			}

			if (!SegundaChamadaTexto.Equals(String.Empty)) {
				sbWhere.Append(" AND (segundaChamadaTexto LIKE '%"+SegundaChamadaTexto+"%')");
			}

			if (!SegundaChamadaUrl.Equals(String.Empty)) {
				sbWhere.Append(" AND (segundaChamadaUrl LIKE '%"+SegundaChamadaUrl+"%')");
			}

			if (!SegundaChamadaTargetBlank.Equals(String.Empty)) {
				sbWhere.Append(" AND (segundaChamadaTargetBlank LIKE '%"+SegundaChamadaTargetBlank+"%')");
			}

			if (!ArquivoIdImagem.Equals(String.Empty)) {
				sbWhere.Append(" AND (arquivoIdImagem="+ArquivoIdImagem+")");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
