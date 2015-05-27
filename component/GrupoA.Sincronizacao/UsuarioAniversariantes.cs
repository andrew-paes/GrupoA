using System;
using GrupoA.BusinessLogicalLayer;
using GrupoaA.Log;

namespace GrupoA.Sincronizacao
{
    public class UsuarioAniversariantes
    {
        private LogHelper objLogHelper = new LogHelper("SincronizadorAniversariantes");

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void SincronizarAniversariantes()
        {
            try
            {
                objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarAniversariantes - Início Sincronização", "");
                
                new PromocaoBLL().DispararPromocaoAniversariantes();

                objLogHelper.WriteOnFile(Environment.NewLine + "P2", "SincronizarAniversariantes - Fim Sincronização", "");
            }
            catch(Exception ex)
            {
                objLogHelper.WriteOnFile("P3", "Erro no processamento", ex.ToString());
            }
        }

        #endregion
    }
}