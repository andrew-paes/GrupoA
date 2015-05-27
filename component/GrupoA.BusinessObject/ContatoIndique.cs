using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject
{
    [Serializable]
    public class ContatoIndique
    {
        private string _seuNome;
        private string _seuEmail;
        private string _nomeAmigo;
        private string _emailAmigo;
        private string _observacao;
        private string _url;
        private string _templatePath;        

        public ContatoIndique()
        {
 
        }

        public string SeuNome
        {
            get { return _seuNome; }
            set { _seuNome = value; }
        }

        public string SeuEmail
        {
            get { return _seuEmail; }
            set { _seuEmail = value; }
        }

        public string NomeAmigo
        {
            get { return _nomeAmigo; }
            set { _nomeAmigo = value; }
        }

        public string EmailAmigo
        {
            get { return _emailAmigo; }
            set { _emailAmigo = value; }
        }

        public string Observacao
        {
            get { return _observacao; }
            set { _observacao = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string TemplatePath
        {
            get { return _templatePath; }
            set { _templatePath = value; }
        }

        

    }

   
}
