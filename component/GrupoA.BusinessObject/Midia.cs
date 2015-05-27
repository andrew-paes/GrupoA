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
    public partial class Midia
    {
        private int _midiaId;
        private string _tituloMidia;
        private string _urlMidia;
        private string _autor;
        private string _descricaoMidia;
        private bool _ativo;
        private List<MidiaCategoria> _midiaCategorias;
        private List<MidiaRevista> _midiaRevistas;
        private Arquivo _arquivo;
        private Arquivo _arquivoThumb;
        private Conteudo _conteudo;
        private MidiaTipo _midiaTipo;

        public int MidiaId
        {
            get { return _midiaId; }
            set { _midiaId = value; }
        }

        [NotNullValidator]
        [StringLengthValidator(0, 100)]
        public string TituloMidia
        {
            get { return _tituloMidia; }
            set { _tituloMidia = value; }
        }

        public string UrlMidia
        {
            get { return _urlMidia; }
            set { _urlMidia = value; }
        }

        public string Autor
        {
            get { return _autor; }
            set { _autor = value; }
        }

        public string DescricaoMidia
        {
            get { return _descricaoMidia; }
            set { _descricaoMidia = value; }
        }

        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public List<MidiaCategoria> MidiaCategorias
        {
            get { return _midiaCategorias; }
            set { _midiaCategorias = value; }
        }

        public List<MidiaRevista> MidiaRevistas
        {
            get { return _midiaRevistas; }
            set { _midiaRevistas = value; }
        }

        public Arquivo Arquivo
        {
            get { return _arquivo; }
            set { _arquivo = value; }
        }

        public Arquivo ArquivoThumb
        {
            get { return _arquivoThumb; }
            set { _arquivoThumb = value; }
        }

        [NotNullValidator]
        public Conteudo Conteudo
        {
            get { return _conteudo; }
            set { _conteudo = value; }
        }

        [NotNullValidator]
        public MidiaTipo MidiaTipo
        {
            get { return _midiaTipo; }
            set { _midiaTipo = value; }
        }

        /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Midia>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Midia>(this);
        }
    }

    public struct MidiaColunas
    {
        public static string MidiaId = @"midiaId";
        public static string MidiaTipoId = @"midiaTipoId";
        public static string ArquivoId = @"arquivoId";
        public static string ArquivoIdThumb = @"arquivoIdThumb";
        public static string TituloMidia = @"tituloMidia";
        public static string UrlMidia = @"urlMidia";
        public static string Autor = @"autor";
        public static string DescricaoMidia = @"descricaoMidia";
        public static string Ativo = @"ativo";
    }
}
