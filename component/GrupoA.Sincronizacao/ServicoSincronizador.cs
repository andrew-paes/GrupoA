using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace GrupoA.Sincronizacao
{
    class ServicoSincronizador : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            if (!EventLog.SourceExists("Sincronizacao"))
            {
                EventLog.CreateEventSource("Sincronizacao", "Sincronização Grupo A");
            }

            EventLog myLog = new EventLog();
            myLog.Source = "Sincronizacao";

            try
            {
                myLog.WriteEntry("Iniciando a Thread de Sincronização.");
                // TODO: inicia Thread que irá ser executada
                myLog.WriteEntry("Thread de Sincronização Iniciada.");
            }
            catch (Exception exception)
            {
                myLog.WriteEntry("Erro ao iniciar a Thread de Sincronização. " + exception.Message);
            }

        }

        /// <summary>
        /// Método que executa as classes sincronizadoras.
        /// </summary>
        private void ExecutarSincronizadores()
        {
            EventLog myLog = new EventLog();
            myLog.Source = "Sincronizacao";

            try
            {
                if (ConfigurationManager.AppSettings["sincronizadores"] != null)
                {
                    var sincronizadores =
                        new List<string>(ConfigurationManager.AppSettings["sincronizadores"].Split(new char[] { ';' })).Where(s => !String.IsNullOrEmpty(s));

                    foreach (string sincronizador in sincronizadores)
                    {
                        
                    }
                }
                else
                {
                    myLog.WriteEntry("Sincronização não Executada. Não existe a entrada sincronizadores no config da aplicação.");
                }
            }
            catch (Exception exception)
            {
                myLog.WriteEntry("Erro ao executar Sincronização. " + exception.Message);
            }

        }
    }
}
