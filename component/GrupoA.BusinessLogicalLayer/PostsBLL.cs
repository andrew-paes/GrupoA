using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
	public class PostsBLL : BaseBLL
	{
		#region Declarações DAL

		private IPostsDAL _postsDal;
		private IPostsDAL PostsDal
		{
			get { return _postsDal ?? (_postsDal = new PostsADO()); }
		}

		#endregion

		/// <summary>
		/// Retorna todos os posts em ordem descrescente de data de criação
		/// </summary>
		/// <returns></returns>
		public List<Posts> CarregarTodos()
		{
			return this.CarregarTodos(0);
		}

		/// <summary>
		/// Retorna todos os posts em ordem descrescente de data de criação
		/// </summary>
		/// <param name="quantidadeRegistros"></param>
		/// <returns></returns>
		public List<Posts> CarregarTodos(int quantidadeRegistros)
		{
			return PostsDal.CarregarTodos(quantidadeRegistros);
		}
	}
}