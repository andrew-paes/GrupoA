using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ItemSessao
    /// </summary>
    public class Arquivo
    {
        public Arquivo()
        {

        }

        private long _arquivoId;
        public long arquivoId
        {
            get { return _arquivoId; }
            set { _arquivoId = value; }
        }

        private string _nomeArquivo;
        public string nomeArquivo
        {
            get { return _nomeArquivo; }
            set { _nomeArquivo = value; }
        }

        private long _tamanho;
        public long tamanho
        {
            get { return _tamanho; }
            set { _tamanho = value; }
        }

        private string _extensao;
        public string extensao
        {
            get { return _extensao; }
            set { _extensao = value; }
        }

        private string _nomeOriginal;
        public string nomeOriginal
        {
            get { return _nomeOriginal; }
            set { _nomeOriginal = value; }
        }

        private DateTime _dataCriacao;
        public DateTime dataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = value; }
        }

        private string _titulo;
        public string titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }


    }
}
