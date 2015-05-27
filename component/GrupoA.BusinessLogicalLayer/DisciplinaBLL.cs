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
	public class DisciplinaBLL : BaseBLL
	{
		private IDisciplinaDAL _DisciplinaDAL;

		private IDisciplinaDAL DisciplinaDAL
		{
			get
			{
				if (_DisciplinaDAL == null)
					_DisciplinaDAL = new DisciplinaADO();
				return _DisciplinaDAL;
			}
		}

		public Disciplina Carregar(Disciplina entidade)
		{
			return DisciplinaDAL.Carregar(entidade);
		}

        public void Inserir(Disciplina entidade)
        {
            DisciplinaDAL.Inserir(entidade);
        }

        public void Atualizar(Disciplina entidade)
        {
            DisciplinaDAL.Atualizar(entidade);
        }

        public void Excluir(Disciplina entidade)
        {
            DisciplinaDAL.Excluir(entidade);
        }

        public Disciplina Carregar(String nomeDisciplina)
        {
            return DisciplinaDAL.Carregar(nomeDisciplina);
        }
	}
}