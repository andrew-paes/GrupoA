using GrupoA.BusinessLogicalLayer;

namespace GrupoA.Sincronizacao
{
    public class ProdutoOferta
    {
        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void SincronizarOfertas()
        {
            try
            {
                new OfertaBLL().AplicarOfertas();
            }
            catch { }
        }

        #endregion
    }
}