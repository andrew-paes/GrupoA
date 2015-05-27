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
	public class CursoBLL : BaseBLL
	{
		private ICursoDAL _CursoDAL;

		private ICursoDAL CursoDAL
		{
			get
			{
				if (_CursoDAL == null)
					_CursoDAL = new CursoADO();
				return _CursoDAL;
			}
		}

		public Curso Carregar(Curso entidade)
		{
			return CursoDAL.Carregar(entidade);
		}

        public void Inserir(Curso entidade)
        {
            CursoDAL.Inserir(entidade);
        }

        public void Atualizar(Curso entidade)
        {
            CursoDAL.Atualizar(entidade);
        }

        public void Excluir(Curso entidade)
        {
            CursoDAL.Excluir(entidade);
        }

        public Curso Carregar(String nomeCurso)
        {
            return CursoDAL.Carregar(nomeCurso);
        }
	}
}