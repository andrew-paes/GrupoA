using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;

namespace GrupoA.IISServices
{
    public class ServicoGerenciador
    {
        private Object thisLock = new Object();

        public void Iniciar()
        {
            Dictionary<string, Thread> threadList = new Dictionary<string, Thread>();

            while (true)
            {
                try
                {
                    ValidaThreadsEmExecucao(threadList);

                    Dictionary<string, string> servicosConfigurados = RecuperaServicosConfigurados();

                    foreach (KeyValuePair<string, string> item in servicosConfigurados)
                    {
                        if (!threadList.ContainsKey(item.Key)) // Verifica se a thread já não existe na listagem, se existe está em execução ainda
                        {
                            string[] assemblyInfoData = item.Value.Split(new char[] { ',' });

                            Thread novaThread = new Thread(() => ExecutarServico(assemblyInfoData[0], assemblyInfoData[1]));
                            novaThread.Name = item.Key;
                            threadList.Add(item.Key, novaThread);
                            novaThread.Start();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //using (var file = File.CreateText(@"c:\temp\log_ServicoGerenciador_" + DateTime.Now.ToString() + ".txt"))
                    //{
                    //    file.WriteLine(ex.Message);
                    //    file.Close();
                    //}
                }
                finally
                {
                    Thread.Sleep(5000);
                }
            }
        }

        /// <summary>
        /// Executa o serviço.
        /// </summary>
        /// <param name="nomeAssembly">String com o nome do Assembly que contém o serviço.</param>
        /// <param name="nomeClasse">String com o nome da Classe de serviço.</param>
        private static void ExecutarServico(string nomeAssembly, string nomeClasse)
        {
            Assembly asm = Assembly.Load(nomeAssembly); // Carrega o assembly informado

            Object obj = asm.CreateInstance(nomeClasse, true); // Cria uma instância da classe informada

            Type tipo = typeof(IServico);

            tipo.InvokeMember("Executar", BindingFlags.InvokeMethod, null, obj, null);
        }

        /// <summary>
        /// Verifica quais Threads de serviços ainda estão em execução.
        /// </summary>
        /// <param name="threadList"></param>
        private void ValidaThreadsEmExecucao(Dictionary<string, Thread> threadList)
        {
            Dictionary<string, Thread> threadListShadow = new Dictionary<string, Thread>(); // Cria uma cópia da listagem de Thread

            foreach (KeyValuePair<string, Thread> item in threadList)
            {
                threadListShadow.Add(item.Key, item.Value);
            }

            threadList.Clear();

            foreach (KeyValuePair<string, Thread> item in threadListShadow) // Deixa somente as threads (serviços) em execução.
            {
                Thread threadCorrente = item.Value;
                if (threadCorrente != null && threadCorrente.IsAlive)
                {
                    threadList.Add(item.Key, item.Value);
                }
            }

            threadListShadow.Clear();
        }

        /// <summary>
        /// Recupera os serviços configurados.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> RecuperaServicosConfigurados()
        {
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("ServicosDictionary");

            Dictionary<string, string> servicos = new Dictionary<string, string>();

            if (section != null)
            {
                foreach (string key in section.AllKeys)
                {
                    servicos.Add(key, section[key]);
                }
            }

            return servicos;
        }
    }
}