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
    public partial class Titulo
    {
        private int _tituloId;
        private string _subtituloLivro;
        private int? _numeroPaginas;
        private int? _edicao;
        private DateTime? _dataLancamento;
        private DateTime? _dataPublicacao;
        private bool _maisVendido;
        private string _nomeTitulo;
        private string _formato;
        private List<Capitulo> _capitulos;
        private List<DestaqueTituloImpresso> _destaqueTituloImpressos;
        private List<TituloAutor> _tituloAutores;
        private List<TituloConteudoExtraArquivo> _tituloConteudoExtraArquivos;
        private TituloConteudoExtraMidia _tituloConteudoExtraMidia;
        private TituloConteudoExtraUrl _tituloConteudoExtraUrl;
        private TituloEletronico _tituloEletronico;
        private List<TituloImagemResumo> _tituloImagemResumos;
        private TituloImpresso _tituloImpresso;
        private TituloInformacaoComentarioEspecialista _tituloInformacaoComentarioEspecialista;
        private TituloInformacaoFicha _tituloInformacaoFicha;
        private TituloInformacaoMaterialDidatico _tituloInformacaoMaterialDidatico;
        private TituloInformacaoResumo _tituloInformacaoResumo;
        private TituloInformacaoSobreAutor _tituloInformacaoSobreAutor;
        private TituloInformacaoSumario _tituloInformacaoSumario;
        private List<TituloSolicitacao> _tituloSolicitacoes;
        private Conteudo _conteudo;
        private int? _maisVendidoOrdem;

        public int TituloId
        {
            get { return _tituloId; }
            set { _tituloId = value; }
        }

        public string SubtituloLivro
        {
            get { return _subtituloLivro; }
            set { _subtituloLivro = value; }
        }

        public int? NumeroPaginas
        {
            get { return _numeroPaginas; }
            set { _numeroPaginas = value; }
        }

        public int? Edicao
        {
            get { return _edicao; }
            set { _edicao = value; }
        }

        public DateTime? DataLancamento
        {
            get { return _dataLancamento; }
            set { _dataLancamento = value; }
        }

        public DateTime? DataPublicacao
        {
            get { return _dataPublicacao; }
            set { _dataPublicacao = value; }
        }

        public bool MaisVendido
        {
            get { return _maisVendido; }
            set { _maisVendido = value; }
        }

        public string NomeTitulo
        {
            get { return _nomeTitulo; }
            set { _nomeTitulo = value; }
        }

        public string Formato
        {
            get { return _formato; }
            set { _formato = value; }
        }

        public List<Capitulo> Capitulos
        {
            get { return _capitulos; }
            set { _capitulos = value; }
        }

        public List<DestaqueTituloImpresso> DestaqueTituloImpressos
        {
            get { return _destaqueTituloImpressos; }
            set { _destaqueTituloImpressos = value; }
        }

        public List<TituloAutor> TituloAutores
        {
            get { return _tituloAutores; }
            set { _tituloAutores = value; }
        }

        public List<TituloConteudoExtraArquivo> TituloConteudoExtraArquivos
        {
            get { return _tituloConteudoExtraArquivos; }
            set { _tituloConteudoExtraArquivos = value; }
        }

        [NotNullValidator]
        public TituloConteudoExtraMidia TituloConteudoExtraMidia
        {
            get { return _tituloConteudoExtraMidia; }
            set { _tituloConteudoExtraMidia = value; }
        }

        [NotNullValidator]
        public TituloConteudoExtraUrl TituloConteudoExtraUrl
        {
            get { return _tituloConteudoExtraUrl; }
            set { _tituloConteudoExtraUrl = value; }
        }

        [NotNullValidator]
        public TituloEletronico TituloEletronico
        {
            get { return _tituloEletronico; }
            set { _tituloEletronico = value; }
        }

        public List<TituloImagemResumo> TituloImagemResumos
        {
            get { return _tituloImagemResumos; }
            set { _tituloImagemResumos = value; }
        }

        [NotNullValidator]
        public TituloImpresso TituloImpresso
        {
            get { return _tituloImpresso; }
            set { _tituloImpresso = value; }
        }

        [NotNullValidator]
        public TituloInformacaoComentarioEspecialista TituloInformacaoComentarioEspecialista
        {
            get { return _tituloInformacaoComentarioEspecialista; }
            set { _tituloInformacaoComentarioEspecialista = value; }
        }

        [NotNullValidator]
        public TituloInformacaoFicha TituloInformacaoFicha
        {
            get { return _tituloInformacaoFicha; }
            set { _tituloInformacaoFicha = value; }
        }

        [NotNullValidator]
        public TituloInformacaoMaterialDidatico TituloInformacaoMaterialDidatico
        {
            get { return _tituloInformacaoMaterialDidatico; }
            set { _tituloInformacaoMaterialDidatico = value; }
        }

        [NotNullValidator]
        public TituloInformacaoResumo TituloInformacaoResumo
        {
            get { return _tituloInformacaoResumo; }
            set { _tituloInformacaoResumo = value; }
        }

        [NotNullValidator]
        public TituloInformacaoSobreAutor TituloInformacaoSobreAutor
        {
            get { return _tituloInformacaoSobreAutor; }
            set { _tituloInformacaoSobreAutor = value; }
        }

        [NotNullValidator]
        public TituloInformacaoSumario TituloInformacaoSumario
        {
            get { return _tituloInformacaoSumario; }
            set { _tituloInformacaoSumario = value; }
        }

        public List<TituloSolicitacao> TituloSolicitacoes
        {
            get { return _tituloSolicitacoes; }
            set { _tituloSolicitacoes = value; }
        }

        [NotNullValidator]
        public Conteudo Conteudo
        {
            get { return _conteudo; }
            set { _conteudo = value; }
        }

        /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Titulo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Titulo>(this);
        }

        public int? MaisVendidoOrdem
        {
            get { return _maisVendidoOrdem; }
            set { _maisVendidoOrdem = value; }
        }
    }

    public struct TituloColunas
    {
        public static string TituloId = @"tituloId";
        public static string SubtituloLivro = @"subtituloLivro";
        public static string NumeroPaginas = @"numeroPaginas";
        public static string Edicao = @"edicao";
        public static string DataLancamento = @"dataLancamento";
        public static string DataPublicacao = @"dataPublicacao";
        public static string MaisVendido = @"maisVendido";
        public static string NomeTitulo = @"nomeTitulo";
        public static string Formato = @"formato";
        public static string MaisVendidoOrdem = @"maisVendidoOrdem";
    }
}
