using System.Collections.Generic;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class RevistaPaginaBLL : BaseBLL
    {
        private IRevistaPaginaDAL _revistaPaginaDAL;

        private IRevistaPaginaDAL RevistaPaginaDAL
        {
            get
            {
                if (_revistaPaginaDAL == null)
                    _revistaPaginaDAL = new RevistaPaginaADO();
                return _revistaPaginaDAL;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaPagina Carregar(RevistaPagina entidade)
        {
            return RevistaPaginaDAL.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(RevistaPagina entidade)
        {
            RevistaPaginaDAL.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(RevistaPagina entidade)
        {
            RevistaPaginaDAL.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(RevistaPagina entidade)
        {
            RevistaPaginaDAL.Excluir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <returns></returns>
        public List<RevistaPagina> CarregarRevistaPaginasMenu(Revista revista)
        {
            return RevistaPaginaDAL.CarregarRevistaPaginasMenu(revista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaPagina"></param>
        /// <returns></returns>
        public RevistaPagina CarregarRevistaPaginaPorNome(RevistaPagina revistaPagina)
        {
            return RevistaPaginaDAL.CarregarRevistaPaginaPorNome(revistaPagina);
        }
    }
}