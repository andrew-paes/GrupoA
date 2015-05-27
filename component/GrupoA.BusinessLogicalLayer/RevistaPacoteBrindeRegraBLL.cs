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
    public class RevistaPacoteBrindeRegraBLL : BaseBLL
	{
        private IRevistaPacoteBrindeRegraDAL _revistaPacoteBrindeRegraDAL;

        private IRevistaPacoteBrindeRegraDAL RevistaPacoteBrindeRegraDAL
		{
			get
			{
				if (_revistaPacoteBrindeRegraDAL == null)
                    _revistaPacoteBrindeRegraDAL = new RevistaPacoteBrindeRegraADO();
				return _revistaPacoteBrindeRegraDAL;
			}
		}

        public RevistaPacoteBrindeRegra Carregar(RevistaPacoteBrindeRegra entidade)
		{
			return RevistaPacoteBrindeRegraDAL.Carregar(entidade);
		}

        public void Inserir(RevistaPacoteBrindeRegra entidade)
        {
            RevistaPacoteBrindeRegraDAL.Inserir(entidade);
        }

        public void Atualizar(RevistaPacoteBrindeRegra entidade)
        {
            RevistaPacoteBrindeRegraDAL.Atualizar(entidade);
        }

        public void Excluir(RevistaPacoteBrindeRegra entidade)
        {
            RevistaPacoteBrindeRegraDAL.Excluir(entidade);
        }

        /// <summary>
        /// Buscar "RevistaPacoteBrindeRegra" através de uma lista de produtos ( revistas BMJ )
        /// </summary>
        /// <param name="produtoBOList"></param>
        /// <returns></returns>
        public RevistaPacoteBrindeRegra CarregarBMJ(List<Produto> produtoBOList)
        {
            return RevistaPacoteBrindeRegraDAL.CarregarBMJ(produtoBOList);
        }

        /// <summary>
        /// Buscar "RevistaPacoteBrindeRegra" através de uma lista de produtos ( revistas Pátio )
        /// </summary>
        /// <param name="produtoBOList"></param>
        /// <returns></returns>
        public RevistaPacoteBrindeRegra CarregarPatio(List<Produto> produtoBOList)
        {
            return RevistaPacoteBrindeRegraDAL.CarregarPatio(produtoBOList);
        }

        /// <summary>
        /// Buscar "RevistaPacoteBrindeRegra" através do "Produto.CodigoProduto"
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        public RevistaPacoteBrindeRegra Carregar(Produto produtoBO)
        {
            return RevistaPacoteBrindeRegraDAL.Carregar(produtoBO);
        }
	}
}