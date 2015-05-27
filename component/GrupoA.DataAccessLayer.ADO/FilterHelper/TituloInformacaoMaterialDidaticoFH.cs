
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
	public partial class TituloInformacaoMaterialDidaticoFH : IFilterHelper
	{
		private string _tituloInformacaoMaterialDidaticoId;
		public string TituloInformacaoMaterialDidaticoId {
			get { return _tituloInformacaoMaterialDidaticoId==null?String.Empty:_tituloInformacaoMaterialDidaticoId; }
			set { _tituloInformacaoMaterialDidaticoId=value; }
		}

		private string _textoMaterial;
		public string TextoMaterial {
			get { return _textoMaterial==null?String.Empty:_textoMaterial; }
			set { _textoMaterial=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!TituloInformacaoMaterialDidaticoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (tituloInformacaoMaterialDidaticoId="+TituloInformacaoMaterialDidaticoId+")");
			}

			if (!TextoMaterial.Equals(String.Empty)) {
				sbWhere.Append(" AND (textoMaterial LIKE '%"+TextoMaterial+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
