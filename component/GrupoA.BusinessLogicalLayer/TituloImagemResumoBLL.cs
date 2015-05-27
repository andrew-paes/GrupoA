using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
	/// <summary>
	/// Classe que abstrai as regras de negócio referentes a usuários.
	/// </summary>
	public class TituloImagemResumoBLL : BaseBLL
	{
		private ITituloImagemResumoDAL _tituloImagemResumoDAL;

		private ITituloImagemResumoDAL TituloImagemResumoDAL
		{
			get
			{
				if (_tituloImagemResumoDAL == null)
					_tituloImagemResumoDAL = new TituloImagemResumoADO();
				return _tituloImagemResumoDAL;

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entidade"></param>
		/// <returns></returns>
		public TituloImagemResumo Inserir(TituloImagemResumo entidade)
		{
			TituloImagemResumoDAL.Inserir(entidade);
			return entidade;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entidade"></param>
		/// <returns></returns>
		public TituloImagemResumo Carregar(TituloImagemResumo entidade)
		{
			return TituloImagemResumoDAL.Carregar(entidade);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entidade"></param>
		/// <returns></returns>
		public List<TituloImagemResumo> Carregar(Titulo entidade)
		{
			return TituloImagemResumoDAL.Carregar(entidade).ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entidade"></param>
		public void Excluir(TituloImagemResumo entidade)
		{
			TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

			using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
			{
				TituloImagemResumoDAL.Excluir(entidade);

				tScope.Complete();
			}
		}
	}
}