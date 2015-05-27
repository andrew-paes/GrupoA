using System;
using System.Threading;
using GrupoA.Sincronizacao;

namespace GrupoA.IISServices
{
    public class ServicoCompraConjunta : IServico
	{
        private Object thisLock = new Object();

		public void Executar()
		{
            lock (thisLock)
            {
                new CompraConjunta().SincronizarCompraConjunta();
            }

            //roda a cada 1h
            Thread.Sleep(3600000); //1s = 1000ms //1h = 3600000ms // 86400000 / 60 / 60 / 1000 = 24h
		}
	}
}