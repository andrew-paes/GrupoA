using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ItemSessao
    /// </summary>
    public class Modelo
    {
        private int _modeloId = 0;
        private string _nome = string.Empty;
        private string _arquivo = string.Empty;

        public Modelo()
        {
            //
        }

        public int ModeloId
        {
            get { return _modeloId; }
            set { _modeloId = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Arquivo
        {
            get { return _arquivo; }
            set { _arquivo = value; }
        }

    }
}
