using System;
using System.Configuration;
using System.Threading;
using GrupoA.Sincronizacao;
using GrupoaA.Log;

namespace GrupoA.IISServices
{
    public class SincronizadorCadastro : IServico
    {
        private Object thisLock = new Object();
        private LogHelper objLogHelper = new LogHelper("SincronizadorCadastro");

        public void Executar()
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "USU1 - SincronizadorCadastro", "Inicio");

            lock (thisLock)
            {
                try
                {
                    objLogHelper.WriteOnFile(Environment.NewLine + "USU2 - SincronizadorCadastro", "Nova Thread Aberta");

                    new UsuarioSincronizador().SincronizarClientesComIntegracaoPendente();

                    objLogHelper.WriteOnFile(Environment.NewLine + "USU3 - SincronizadorCadastro", "Sucesso");

                    if (Convert.ToString(ConfigurationManager.AppSettings["ExecutarSincPedidoNaSincDeCadastro"]).ToLower().Equals("true"))
                    {
                        objLogHelper.WriteOnFile(Environment.NewLine + "USU4 - SincronizadorPedido", "Chamada");

                        new SincronizadorPedido().Executar(); // Chama a sincronização de pedidos, dependente do término da sincronização de usuários
                    }
                }
                catch (Exception ex)
                {
                    objLogHelper.WriteOnFile(Environment.NewLine + "USU2 - SincronizadorCadastro", "ERRO", ex.ToString());
                }
            }

            //roda a cada 10 min
            Thread.Sleep(600000); //1s = 1000ms //1h = 3600000ms
        }
    }
}