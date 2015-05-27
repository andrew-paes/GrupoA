using System;
using System.Threading;
using GrupoA.Sincronizacao;

namespace GrupoA.IISServices
{
    public class SincronizarProduto : IServico
	{
        private Object thisLock = new Object();

		public void Executar()
		{
            lock (thisLock)
            {
                new ProdutoSincronizador().SincronizarProduto();
            }

            //roda a cada 1h
            Thread.Sleep(1200000); //1s = 1000ms //1h = 3600000ms
		}
	}
}