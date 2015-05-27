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
    public class ProdutoImagemBLL : BaseBLL
    {
        private IProdutoImagemDAL _produtoImagemDAL;

        private IProdutoImagemDAL ProdutoImagemDAL
        {
            get
            {
                if (_produtoImagemDAL == null)
                    _produtoImagemDAL = new ProdutoImagemADO();
                return _produtoImagemDAL;

            }
        }

        public void Inserir(ProdutoImagem entidade)
        {
            ProdutoImagemDAL.Inserir(entidade);
        }

        public ProdutoImagem Carregar(ProdutoImagem entidade)
        {
            return ProdutoImagemDAL.Carregar(entidade);
        }

        public List<ProdutoImagem> Carregar(Produto entidade)
        {
            return ProdutoImagemDAL.Carregar(entidade).ToList();
        }
    }
}