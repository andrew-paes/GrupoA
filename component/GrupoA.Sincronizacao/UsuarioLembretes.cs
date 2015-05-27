using System;
using GrupoA.BusinessLogicalLayer;
using GrupoaA.Log;


namespace GrupoA.Sincronizacao
{
    public class UsuarioLembretes
    {
        private LogHelper objLogHelper = new LogHelper("SincronizadorLembretes");

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void SincronizarLembretes()
        {
            try
            {
                objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarLembretes - Início Sincronização", "");

                new EventoBLL().DispararAlertas();

                objLogHelper.WriteOnFile(Environment.NewLine + "P2", "SincronizarLembretes - Fim Sincronização", "");
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("P3", "Erro no processamento", ex.ToString());
            }
        }

        #endregion
    }
}