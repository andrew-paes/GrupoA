/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{

    [Serializable]
    public partial class TituloConteudoExtraArquivo
    {
        // Construtor
        public TituloConteudoExtraArquivo() { }

        // Construtor com identificador
        public TituloConteudoExtraArquivo(int tituloConteudoExtraArquivoId)
        {
            _tituloConteudoExtraArquivoId = tituloConteudoExtraArquivoId;
        }

        private int _tituloConteudoExtraArquivoId;
        private bool _somenteLogado;
        private bool _restritoProfessor;
        private string _nomeConteudo;
        private Arquivo _arquivo;
        private Titulo _titulo;

        public int TituloConteudoExtraArquivoId
        {
            get { return _tituloConteudoExtraArquivoId; }
            set { _tituloConteudoExtraArquivoId = value; }
        }

        public bool SomenteLogado
        {
            get { return _somenteLogado; }
            set { _somenteLogado = value; }
        }

        public bool RestritoProfessor
        {
            get { return _restritoProfessor; }
            set { _restritoProfessor = value; }
        }

        [NotNullValidator]
        [StringLengthValidator(0, 250)]
        public string NomeConteudo
        {
            get { return _nomeConteudo; }
            set { _nomeConteudo = value; }
        }

        public Arquivo Arquivo
        {
            get { return _arquivo; }
            set { _arquivo = value; }
        }

        [NotNullValidator]
        public Titulo Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public Boolean Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloConteudoExtraArquivo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloConteudoExtraArquivo>(this);
        }
    }

    public struct TituloConteudoExtraArquivoColunas
    {
        public static string TituloConteudoExtraArquivoId = @"tituloConteudoExtraArquivoId";
        public static string TituloId = @"tituloId";
        public static string SomenteLogado = @"somenteLogado";
        public static string RestritoProfessor = @"restritoProfessor";
        public static string ArquivoId = @"arquivoId";
        public static string NomeConteudo = @"nomeConteudo";
    }
}
