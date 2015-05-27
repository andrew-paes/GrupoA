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
	public class ConteudoImprensaBLL : BaseBLL
	{
		private IConteudoImprensaDAL _conteudoImprensaDAL;

		private IConteudoImprensaDAL ConteudoImprensaDAL
		{
			get
			{
				if (_conteudoImprensaDAL == null)
					_conteudoImprensaDAL = new ConteudoImprensaADO();
				return _conteudoImprensaDAL;

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entidade"></param>
		public void Excluir(ConteudoImprensa entidade)
		{
			TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

			using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
			{
				ConteudoImprensaDAL.Excluir(entidade);

				tScope.Complete();
			}
		}
	}
}