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
    public class RevistaPacoteBLL : BaseBLL
	{
        private IRevistaPacoteDAL _revistaPacoteDAL;

        private IRevistaPacoteDAL RevistaPacoteDAL
		{
			get
			{
				if (_revistaPacoteDAL == null)
                    _revistaPacoteDAL = new RevistaPacoteADO();
				return _revistaPacoteDAL;
			}
		}

        public RevistaPacote Carregar(RevistaPacote entidade)
		{
			return RevistaPacoteDAL.Carregar(entidade);
		}

        public void Inserir(RevistaPacote entidade)
        {
            RevistaPacoteDAL.Inserir(entidade);
        }

        public void Atualizar(RevistaPacote entidade)
        {
            RevistaPacoteDAL.Atualizar(entidade);
        }

        public void Excluir(RevistaPacote entidade)
        {
            RevistaPacoteDAL.Excluir(entidade);
        }

        /// <summary>
        /// Método que persiste um RevistaPacoteBrinde.
        /// </summary>
        /// <param name="revistaPacoteBO"></param>
        /// <param name="produtoBO"></param>
        public void Inserir(RevistaPacote revistaPacoteBO, Produto produtoBO)
        {
            RevistaPacoteDAL.Inserir(revistaPacoteBO, produtoBO);
        }

        public void Excluir(RevistaPacote revistaPacoteBO, Produto produtoBO)
        {
            RevistaPacoteDAL.Excluir(revistaPacoteBO, produtoBO);
        }

        /// <summary>
        /// Verificar se produto consta na lista de brindes de assinaturas de revistas
        /// </summary>
        /// <param name="produtoBO"></param>
        /// <returns></returns>
        public bool ProdutoBrinde(Produto produtoBO)
        {
            return RevistaPacoteDAL.ProdutoBrinde(produtoBO);
        }

        public bool BrindeRevistaBMJ(Produto produtoBO)
        {
            return RevistaPacoteDAL.BrindeRevistaBMJ(produtoBO);
        }
	}
}