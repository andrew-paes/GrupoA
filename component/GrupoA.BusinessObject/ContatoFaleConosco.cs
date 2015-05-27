using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject
{
    public class ContatoFaleConosco
    {
        #region Private Fields

        private string _nome;
        private string _email;
        private Municipio _municipio;
        private Telefone _telefone;
        private ContatoSetor _setor;
        private ContatoAssunto _assunto;
        private string _mensagem;
        private bool _optin;
        private string _templatePath;        

        #endregion

        #region Constructor

        public ContatoFaleConosco()
        {

        }

        #endregion

        #region Properties

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Municipio Municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        public Telefone Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

        public ContatoSetor Setor
        {
            get { return _setor; }
            set { _setor = value; }
        }

        public ContatoAssunto Assunto
        {
            get { return _assunto; }
            set { _assunto = value; }
        }

        public string Mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value; }
        }

        public bool Optin
        {
            get { return _optin; }
            set { _optin = value; }
        }

        public string TemplatePath
        {
            get { return _templatePath; }
            set { _templatePath = value; }
        }

        #endregion

    }
}
