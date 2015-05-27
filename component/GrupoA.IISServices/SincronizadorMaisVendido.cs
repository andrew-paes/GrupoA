using System;
using System.Threading;
using GrupoA.Sincronizacao;

namespace GrupoA.IISServices
{
    public class SincronizadorMaisVendido : IServico
    {
        private Object thisLock = new Object();

        public void Executar()
        {
            lock (thisLock)
            {
                new ProdutoMaisVendidoSincronizador().SincronizarProduto();
            }

            //roda a cada 3h
            Thread.Sleep(10800000); //1s = 1000ms //1h = 3600000ms
        }
    }
}
