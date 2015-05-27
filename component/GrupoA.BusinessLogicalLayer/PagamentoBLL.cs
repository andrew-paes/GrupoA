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
	public class PagamentoBLL : BaseBLL
	{
		private IPagamentoDAL _pagamentoDAL;

		private IPagamentoDAL PagamentoDAL
		{
			get
			{
				if (_pagamentoDAL == null)
					_pagamentoDAL = new PagamentoADO();
				return _pagamentoDAL;

			}
		}

		public Pagamento Carregar(Pedido entidade)
		{
			return PagamentoDAL.Carregar(entidade);
		}
	}
}