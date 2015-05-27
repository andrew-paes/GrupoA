using System;
using System.Threading;
using GrupoA.Sincronizacao;

namespace GrupoA.IISServices
{
    public class ServicoLembretes : IServico
	{
        private Object thisLock = new Object();

		public void Executar()
		{
            lock (thisLock)
            {
                //if (DateTime.Now.Hour >= 1 && DateTime.Now.Hour < 6)
                //if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                //{
                    new UsuarioLembretes().SincronizarLembretes();
                //}
            }

            //roda a cada 12h
            Thread.Sleep(43200000); //1s = 1000ms //1h = 3600000ms //900000 = (43200000 / 60 / 60 / 1000) = 12h
		}
	}
}