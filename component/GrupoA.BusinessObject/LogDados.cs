using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{
    public class LogDados
    {
        private readonly Dictionary<string, Valores> _entradas;
        private Dictionary<string, Valores> Entradas
        {
            get { return _entradas; }
        }

        public LogDados()
        {
            this._entradas = new Dictionary<string, Valores>();
        }

        /// <summary>
        /// Método para adicionar dados do log
        /// </summary>
        /// <param name="entrada">nome da ocorrencia</param>
        /// <param name="label">nome da chave</param>
        /// <param name="valor">valor da chave</param>
        public void Adicionar(string entrada, string label, string valor)
        {
            this._entradas.Add(entrada, new Valores()
            {
                Label = label,
                Valor = valor
            });
        }

        /// <summary>
        /// Cria um XML com os dados do log
        /// </summary>
        /// <returns>XML com os dados do log</returns>
        public XDocument ToXml()
        {
            var retorno = new XDocument();
            XElement raiz = new XElement("ocorrencia");
            retorno.AddFirst(raiz);

            if (Entradas != null && Entradas.Count > 0)
                foreach (var entradas in Entradas)
                {
                    raiz.Add(new XElement(entradas.Key,
                        new XElement("label", new XCData(entradas.Value.Label)),
                        new XElement("valor", new XCData(entradas.Value.Valor))
                        ));
                }

            return retorno;
        }
    }

    internal class Valores
    {
        public string Label { get; set; }
        public string Valor { get; set; }
    }
}