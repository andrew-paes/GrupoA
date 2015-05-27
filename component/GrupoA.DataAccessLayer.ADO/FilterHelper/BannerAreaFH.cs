
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
	public partial class BannerAreaFH : IFilterHelper
	{
		private string _bannerAreaId;
		public string BannerAreaId {
			get { return _bannerAreaId==null?String.Empty:_bannerAreaId; }
			set { _bannerAreaId=value; }
		}

		private string _area;
		public string Area {
			get { return _area==null?String.Empty:_area; }
			set { _area=value; }
		}

		private string _dimensao;
		public string Dimensao {
			get { return _dimensao==null?String.Empty:_dimensao; }
			set { _dimensao=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!BannerAreaId.Equals(String.Empty)) {
				sbWhere.Append(" AND (bannerAreaId="+BannerAreaId+")");
			}

			if (!Area.Equals(String.Empty)) {
				sbWhere.Append(" AND (area LIKE '%"+Area+"%')");
			}

			if (!Dimensao.Equals(String.Empty)) {
				sbWhere.Append(" AND (dimensao LIKE '%"+Dimensao+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
