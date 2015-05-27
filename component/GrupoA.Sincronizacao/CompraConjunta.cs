using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.OleDb;
using GrupoaA.Log;

namespace GrupoA.Sincronizacao
{
    public class CompraConjunta
    {
        private LogHelper objLogHelper = new LogHelper("SincronizadorCompraColetiva");

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        public void SincronizarCompraConjunta()
        {
            try
            {
                objLogHelper.WriteOnFile(Environment.NewLine + "P1", "SincronizarCompraConjunta - Início Sincronização", "");
                
                new CompraConjuntaBLL().FechamentoCompraConjunta();

                objLogHelper.WriteOnFile(Environment.NewLine + "P2", "SincronizarCompraConjunta - Fim Sincronização", "");
            }
            catch (Exception ex)
            {
                objLogHelper.WriteOnFile("P3", "Erro no processamento", ex.ToString());
            }
        }

        #endregion
    }
}