using System;
using System.IO;
using System.Threading;

namespace GrupoA.IISServices
{
    public class ServicoHandler : System.Web.Hosting.IProcessHostPreloadClient
    {
        private static Thread threadGerenciador = null;

        public void Preload(string[] parameters)
        {           
            try
            {
                if (threadGerenciador == null)
                {
                    ServicoGerenciador manager = new ServicoGerenciador();
                    threadGerenciador = new Thread(manager.Iniciar);
                    threadGerenciador.Name = "ThreadServicoGerenciador";
                    threadGerenciador.Start();
                }
            }
            catch (Exception ex)
            {
                using (var file = File.CreateText(@"c:\temp\log_ServicoHandler_" + DateTime.Now.ToString() + ".txt"))
                {
                    file.WriteLine(ex.Message);
                    file.Close();
                }
            }
        }

    }
}
