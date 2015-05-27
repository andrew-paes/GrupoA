using System.Threading;
using GrupoA.Sincronizacao;
using System;

namespace GrupoA.IISServices
{
    public class ServicoOferta : IServico
    {
        private Object thisLock = new Object();

        public void Executar()
		{
            lock (thisLock)
            {
                new ProdutoOferta().SincronizarOfertas();
            }

            //roda a cada 30 minutos
            Thread.Sleep(1800000); //1s = 1000ms //1h = 3600000ms //900000 = (1800000 / 60 / 1000) = 30m
		}
	}
}