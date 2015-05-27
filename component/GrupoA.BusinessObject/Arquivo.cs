using System;
using System.Collections.Generic;
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

namespace GrupoA.BusinessObject
{	
	
	[Serializable]
	public partial class Arquivo 
	{
		// Construtor
		public Arquivo() {}

		// Construtor com identificador
		public Arquivo(int arquivoId) {
			_arquivoId = arquivoId;
		}

		private int _arquivoId;
		private int _tamanhoArquivo;
		private DateTime? _dataHoraUpload;
		private string _nomeArquivo;
		private string _nomeArquivoOriginal;
		private List<Autor> _autoresImagem;
		private List<Banner> _banneres;
		private List<ClippingImagem> _clippingImagens;
		private List<CursoPanamericano> _cursoPanamericanosImagem;
		private List<Evento> _eventosThumb;
		private List<EventoImagem> _eventoImagens;
		private List<Midia> _midias;
		private List<Midia> _midiasThumb;
		private List<Noticia> _noticiasThumb;
		private List<NoticiaImagem> _noticiaImagens;
		private List<PaginaPromocional> _paginaPromocionais;
		private List<ProdutoImagem> _produtoImagens;
		private List<ProfessorComprovanteDocencia> _professorComprovanteDocencias;
		private List<ProgramaAtualizacaoChamada> _programaAtualizacaoChamadasImagem;
		private List<RevistaArtigo> _revistaArtigosCapa;
		private List<RevistaArtigo> _revistaArtigosThumbP;
		private List<RevistaArtigo> _revistaArtigosThumbM;
		private List<RevistaArtigo> _revistaArtigos;
		private List<RevistaGrupoAEdicao> _revistaGrupoAEdicoesGrande;
		private List<RevistaGrupoAEdicao> _revistaGrupoAEdicoesPequena;
		private List<TituloConteudoExtraArquivo> _tituloConteudoExtraArquivos;
		private List<TituloImagemResumo> _tituloImagemResumos;
		private List<TituloInformacaoComentarioEspecialista> _tituloInformacaoComentarioEspecialistasAudio;
		private List<TituloInformacaoComentarioEspecialista> _tituloInformacaoComentarioEspecialistasImagem;
		private List<TituloInformacaoSobreAutor> _tituloInformacaoSobreAutoresImagem;
		private List<TituloInformacaoSumario> _tituloInformacaoSumariosSumario;

		public int ArquivoId {
			get { return _arquivoId; }
			set { _arquivoId = value; }
		}

		public int TamanhoArquivo {
			get { return _tamanhoArquivo; }
			set { _tamanhoArquivo = value; }
		}

		public DateTime? DataHoraUpload {
			get { return _dataHoraUpload; }
			set { _dataHoraUpload = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 300)]
		public string NomeArquivo {
			get { return _nomeArquivo; }
			set { _nomeArquivo = value; }
		}

		public string NomeArquivoOriginal {
			get { return _nomeArquivoOriginal; }
			set { _nomeArquivoOriginal = value; }
		}

		public List<Autor> AutoresImagem {
			get { return _autoresImagem; }
			set { _autoresImagem = value; }
		}

		public List<Banner> Banneres {
			get { return _banneres; }
			set { _banneres = value; }
		}

		public List<ClippingImagem> ClippingImagens {
			get { return _clippingImagens; }
			set { _clippingImagens = value; }
		}

		public List<CursoPanamericano> CursoPanamericanosImagem {
			get { return _cursoPanamericanosImagem; }
			set { _cursoPanamericanosImagem = value; }
		}

		public List<Evento> EventosThumb {
			get { return _eventosThumb; }
			set { _eventosThumb = value; }
		}

		public List<EventoImagem> EventoImagens {
			get { return _eventoImagens; }
			set { _eventoImagens = value; }
		}

		public List<Midia> Midias {
			get { return _midias; }
			set { _midias = value; }
		}

		public List<Midia> MidiasThumb {
			get { return _midiasThumb; }
			set { _midiasThumb = value; }
		}

		public List<Noticia> NoticiasThumb {
			get { return _noticiasThumb; }
			set { _noticiasThumb = value; }
		}

		public List<NoticiaImagem> NoticiaImagens {
			get { return _noticiaImagens; }
			set { _noticiaImagens = value; }
		}

		public List<PaginaPromocional> PaginaPromocionais {
			get { return _paginaPromocionais; }
			set { _paginaPromocionais = value; }
		}

		public List<ProdutoImagem> ProdutoImagens {
			get { return _produtoImagens; }
			set { _produtoImagens = value; }
		}

		public List<ProfessorComprovanteDocencia> ProfessorComprovanteDocencias {
			get { return _professorComprovanteDocencias; }
			set { _professorComprovanteDocencias = value; }
		}

		public List<ProgramaAtualizacaoChamada> ProgramaAtualizacaoChamadasImagem {
			get { return _programaAtualizacaoChamadasImagem; }
			set { _programaAtualizacaoChamadasImagem = value; }
		}

		public List<RevistaArtigo> RevistaArtigosCapa {
			get { return _revistaArtigosCapa; }
			set { _revistaArtigosCapa = value; }
		}

		public List<RevistaArtigo> RevistaArtigosThumbP {
			get { return _revistaArtigosThumbP; }
			set { _revistaArtigosThumbP = value; }
		}

		public List<RevistaArtigo> RevistaArtigosThumbM {
			get { return _revistaArtigosThumbM; }
			set { _revistaArtigosThumbM = value; }
		}

		public List<RevistaArtigo> RevistaArtigos {
			get { return _revistaArtigos; }
			set { _revistaArtigos = value; }
		}

		public List<RevistaGrupoAEdicao> RevistaGrupoAEdicoesGrande {
			get { return _revistaGrupoAEdicoesGrande; }
			set { _revistaGrupoAEdicoesGrande = value; }
		}

		public List<RevistaGrupoAEdicao> RevistaGrupoAEdicoesPequena {
			get { return _revistaGrupoAEdicoesPequena; }
			set { _revistaGrupoAEdicoesPequena = value; }
		}

		public List<TituloConteudoExtraArquivo> TituloConteudoExtraArquivos {
			get { return _tituloConteudoExtraArquivos; }
			set { _tituloConteudoExtraArquivos = value; }
		}

		public List<TituloImagemResumo> TituloImagemResumos {
			get { return _tituloImagemResumos; }
			set { _tituloImagemResumos = value; }
		}

		public List<TituloInformacaoComentarioEspecialista> TituloInformacaoComentarioEspecialistasAudio {
			get { return _tituloInformacaoComentarioEspecialistasAudio; }
			set { _tituloInformacaoComentarioEspecialistasAudio = value; }
		}

		public List<TituloInformacaoComentarioEspecialista> TituloInformacaoComentarioEspecialistasImagem {
			get { return _tituloInformacaoComentarioEspecialistasImagem; }
			set { _tituloInformacaoComentarioEspecialistasImagem = value; }
		}

		public List<TituloInformacaoSobreAutor> TituloInformacaoSobreAutoresImagem {
			get { return _tituloInformacaoSobreAutoresImagem; }
			set { _tituloInformacaoSobreAutoresImagem = value; }
		}

		public List<TituloInformacaoSumario> TituloInformacaoSumariosSumario {
			get { return _tituloInformacaoSumariosSumario; }
			set { _tituloInformacaoSumariosSumario = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<Arquivo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<Arquivo>(this);
        }
	}
	
	public struct ArquivoColunas
	{	
		public static string ArquivoId = @"arquivoId";
		public static string TamanhoArquivo = @"tamanhoArquivo";
		public static string DataHoraUpload = @"dataHoraUpload";
		public static string NomeArquivo = @"nomeArquivo";
		public static string NomeArquivoOriginal = @"nomeArquivoOriginal";
	}
}
		