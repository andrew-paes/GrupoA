using System;
using GrupoA.BusinessLogicalLayer;
using GrupoaA.Log;

namespace GrupoA.Sincronizacao
{
    public class ProdutoDisponibilidade
    {
        private LogHelper objLogHelper = new LogHelper("SincronizadorDisponibilidade");

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void SincronizarDisponibilidade()
        {
            try
            {
                objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarDisponibilidade - Início Sincronização", "");

                new AvisoDisponibilidadeBLL().DispararAvisoDisponibilidade();

                objLogHelper.WriteOnFile(Environment.NewLine + "P2", "SincronizarDisponibilidade - Fim Sincronização", "");
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("P3", "Erro no processamento", ex.ToString());
            }
        }

        #endregion
    }
}