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
	public class InstituicaoBLL : BaseBLL
	{
		private IInstituicaoDAL _instituicaoDAL;

		private IInstituicaoDAL InstituicaoDAL
		{
			get
			{
				if (_instituicaoDAL == null)
					_instituicaoDAL = new InstituicaoADO();
				return _instituicaoDAL;
			}
		}

		public Instituicao Carregar(Instituicao entidade)
		{
			return InstituicaoDAL.Carregar(entidade);
		}

        public void Inserir(Instituicao entidade)
        {
            InstituicaoDAL.Inserir(entidade);
        }

        public void Atualizar(Instituicao entidade)
        {
            InstituicaoDAL.Atualizar(entidade);
        }

        public void Excluir(Instituicao entidade)
        {
            InstituicaoDAL.Excluir(entidade);
        }

        public Instituicao Carregar(String nomeInstituicao)
        {
            return InstituicaoDAL.Carregar(nomeInstituicao);
        }
	}
}