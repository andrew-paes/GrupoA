using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
	/// <summary>
	/// Classe que abstrai as regras de negócio referentes a usuários.
	/// </summary>
	public class CursoNivelBLL : BaseBLL
	{
		private ICursoNivelDAL _CursoNivelDAL;

		private ICursoNivelDAL CursoNivelDAL
		{
			get
			{
				if (_CursoNivelDAL == null)
					_CursoNivelDAL = new CursoNivelADO();
				return _CursoNivelDAL;
			}
		}

		public CursoNivel Carregar(CursoNivel entidade)
		{
			return CursoNivelDAL.Carregar(entidade);
		}
	}
}