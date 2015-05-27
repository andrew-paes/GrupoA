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
	public class PaginaPromocionalBLL : BaseBLL
	{
        /// <summary>
        /// 
        /// </summary>
		private IPaginaPromocionalDAL _paginaPromocionalDAL;

        /// <summary>
        /// 
        /// </summary>
		private IPaginaPromocionalDAL PaginaPromocionalDAL
		{
			get
			{
				if (_paginaPromocionalDAL == null)
					_paginaPromocionalDAL = new PaginaPromocionalADO();
				return _paginaPromocionalDAL;

			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
		public void Atualizar(PaginaPromocional entidade)
		{
			PaginaPromocionalDAL.Atualizar(entidade);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(PaginaPromocional entidade)
        {
            PaginaPromocionalDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
		public PaginaPromocional Carregar(PaginaPromocional entidade)
		{
			return PaginaPromocionalDAL.Carregar(entidade);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public IEnumerable<PaginaPromocional> CarregarTodos()
		{
			return PaginaPromocionalDAL.CarregarTodos();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public PaginaPromocional CarregarPorNomePagina(PaginaPromocional entidade)
        {
            return PaginaPromocionalDAL.CarregarPorNomePagina(entidade);
        }
	}
}