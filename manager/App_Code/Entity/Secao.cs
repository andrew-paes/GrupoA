using System;
using System.Collections.Generic;
using System.Web;

namespace Ag2.Manager.Entity
{
    /// <summary>
    /// Summary description for ItemSessao
    /// </summary>
    public class Secao
    {
        private Int32 _secaoId;
        private Int32 _secaoIdPai;
        private Int32 _modeloId;
        private Int32 _idiomaId;
        private Int32 _ordem;
        private Int64 _avaliacaoSomaNotas;
        private Int64 _avaliacoes;
        private Boolean _acessoRapido;
        private string _estadoPublicacao = string.Empty;
        private Int32 _redirecionaId;
        private Boolean _secaoAutenticada;
        private Boolean _habilitaBoxRSS;
        private Boolean _comentar;
        private Boolean _avaliar;
        private Boolean _compartilhar;
        private Boolean _exibeNoMenu;
        private Boolean _excluido;
        private string _titulo = string.Empty;
        private string _tituloMenu = string.Empty;
        private Boolean _ativo;
        private string _palavraChave = string.Empty;
        private string _texto = string.Empty;
        private string _tituloArquivos = string.Empty;
        private string _textoArquivos = string.Empty;

        public Secao()
        {

        }

        public Int32 SecaoId
        {
            get { return _secaoId; }
            set { _secaoId = value; }
        }

        public Int32 SecaoIdPai
        {
            get { return _secaoIdPai; }
            set { _secaoIdPai = value; }
        }

        public Int32 ModeloId
        {
            get { return _modeloId; }
            set { _modeloId = value; }
        }

        public Int32 IdiomaId
        {
            get { return _idiomaId; }
            set { _idiomaId = value; }
        }

        public Int32 Ordem
        {
            get { return _ordem; }
            set { _ordem = value; }
        }

        public Int64 AvaliacaoSomaNotas
        {
            get { return _avaliacaoSomaNotas; }
            set { _avaliacaoSomaNotas = value; }
        }

        public Int64 Avaliacoes
        {
            get { return _avaliacoes; }
            set { _avaliacoes = value; }
        }

        public Boolean AcessoRapido
        {
            get { return _acessoRapido; }
            set { _acessoRapido = value; }
        }

        public Boolean Excluido
        {
            get { return _excluido; }
            set { _excluido = value; }
        }

        public string EstadoPublicacao
        {
            get { return _estadoPublicacao; }
            set { _estadoPublicacao = value; }
        }

        public Int32 RedirecionaId
        {
            get { return _redirecionaId; }
            set { _redirecionaId = value; }
        }

        public Boolean SecaoAutenticada
        {
            get { return _secaoAutenticada; }
            set { _secaoAutenticada = value; }
        }

        public Boolean HabilitaBoxRSS
        {
            get { return _habilitaBoxRSS; }
            set { _habilitaBoxRSS = value; }
        }

        public Boolean Comentar
        {
            get { return _comentar; }
            set { _comentar = value; }
        }

        public Boolean Avaliar
        {
            get { return _avaliar; }
            set { _avaliar = value; }
        }

        public Boolean Compartilhar
        {
            get { return _compartilhar; }
            set { _compartilhar = value; }
        }

        public Boolean ExibeNoMenu
        {
            get { return _exibeNoMenu; }
            set { _exibeNoMenu = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string TituloMenu
        {
            get { return _tituloMenu; }
            set { _tituloMenu = value; }
        }

        public Boolean Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public string PalavraChave
        {
            get { return _palavraChave; }
            set { _palavraChave = value; }
        }

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        public string TituloArquivos
        {
            get { return _tituloArquivos; }
            set { _tituloArquivos = value; }
        }

        public string TextoArquivos
        {
            get { return _textoArquivos; }
            set { _textoArquivos = value; }
        }

    }
}
