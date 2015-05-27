using System;
using System.Threading;
using GrupoA.Sincronizacao;
using GrupoaA.Log;

namespace GrupoA.IISServices
{
    public class SincronizadorPedido : IServico
    {
        private Object thisLock = new Object();
        private LogHelper objLogHelper = new LogHelper("SincronizadorPedido");

        public void Executar()
        {
            objLogHelper.WriteOnFile(Environment.NewLine + "PED1 - SincronizarPedidos", "Inicio");

            lock (thisLock)
            {
                try
                {
                    objLogHelper.WriteOnFile(Environment.NewLine + "PED2 - SincronizarPedidos", "Nova Thread Aberta");

                    new PedidoSincronizador().SincronizarPedidos();

                    objLogHelper.WriteOnFile(Environment.NewLine + "PED3 - SincronizarPedidos", "Sucesso");
                }
                catch (Exception ex)
                {
                    objLogHelper.WriteOnFile(Environment.NewLine + "PED2 - SincronizarPedidos", "ERRO", ex.ToString());
                }
            }

            //roda a cada 10 min
            Thread.Sleep(600000); //1s = 1000ms //1h = 3600000ms
        }
    }
}