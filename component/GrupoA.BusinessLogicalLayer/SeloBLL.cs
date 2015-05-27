using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
	/// <summary>
	/// Classe que abstrai as regras de negócio referentes a usuários.
	/// </summary>
	public class SeloBLL : BaseBLL
	{
		private ISeloDAL _seloDAL;

		private ISeloDAL SeloDAL
		{
			get
			{
				if (_seloDAL == null)
					_seloDAL = new SeloADO();
				return _seloDAL;

			}
		}

		public Selo Carregar(Selo entidade)
		{
			return SeloDAL.Carregar(entidade);
		}

		public List<Selo> Carregar(Produto entidade)
		{
			return SeloDAL.Carregar(entidade).ToList();
		}

        /// <summary>
        /// Verifica se o selo já está relacionado com o produto
        /// </summary>
        /// <param name="seloBO"></param>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        public bool SeloProdutoRelacionado(Selo seloBO, Produto produtoBO)
        {
            return SeloDAL.SeloProdutoRelacionado(seloBO, produtoBO);
        }
	}
}