using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System;
using System.Xml;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
	public class BlogBLL : BaseBLL
	{
		#region Declarações DAL

		private IBlogDAL _blogDal;

        private IBlogDAL BlogDal
		{
            get { return _blogDal ?? (_blogDal = new BlogADO()); }
		}

		#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<Blog> CarregarBlogsPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            return BlogDal.CarregarBlogsPorCategoria(categoria, qtdRegistros);
        }
	}
}