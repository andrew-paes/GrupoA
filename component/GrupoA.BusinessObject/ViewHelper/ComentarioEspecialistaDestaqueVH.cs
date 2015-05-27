using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
    [Serializable]
    public partial class ComentarioEspecialistaDestaqueVH
    {
        private string nomeTitulo;
        private string comentarioTitulo;
        private string nomeEspecialista;
        private string nomeArquivo;

        public string NomeTitulo
        {
            get { return nomeTitulo; }
            set { nomeTitulo = value; }
        }

        public string ComentarioTitulo
        {
            get { return comentarioTitulo; }
            set { comentarioTitulo = value; }
        }

        public string NomeEspecialista
        {
            get { return nomeEspecialista; }
            set { nomeEspecialista = value; }
        }

        public string NomeArquivo
        {
            get { return nomeArquivo; }
            set { nomeArquivo = value; }
        }
    }
}
