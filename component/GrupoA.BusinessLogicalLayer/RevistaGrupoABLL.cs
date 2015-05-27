using System;
using System.Collections.Generic;
using System.Transactions;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary> 
    /// Método que contém os métodos de negócio para utilização de Evento e Categorias de Evento
    /// </summary>
    public class RevistaGrupoABLL : BaseBLL
    {

        #region Propriedades

        private IRevistaDAL _revistaDAL;
        private IRevistaDAL revistaDAL
        {
            get
            {
                if (_revistaDAL == null)
                    _revistaDAL = new RevistaADO();
                return _revistaDAL;
            }
        }

        private IArquivoDAL _arquivoDal;
        private IArquivoDAL ArquivoDal
        {
            get { return _arquivoDal ?? (_arquivoDal = new ArquivoADO()); }
        }


        private IRevistaGrupoAEdicaoDAL _revistaGrupoA;
        private IRevistaGrupoAEdicaoDAL revistaGrupoA
        {
            get
            {
                if (_revistaGrupoA == null)
                    _revistaGrupoA = new RevistaGrupoAEdicaoADO();
                return _revistaGrupoA;
            }
        }

        private IRevistaDAL _revista;
        private IRevistaDAL RevistaDal
        {
            get
            {
                if (_revista == null)
                    _revista = new RevistaADO();
                return _revista;
            }
        }

        private IRevistaEdicaoDAL _revistaEdicaoDAL;
        private IRevistaEdicaoDAL revistaEdicaoDAL
        {
            get
            {
                if (_revistaEdicaoDAL == null)
                    _revistaEdicaoDAL = new RevistaEdicaoADO();
                return _revistaEdicaoDAL;
            }
        }

        private IRevistaArtigoPermissaoDAL _revistaArtigoPermissaoDAL;
        private IRevistaArtigoPermissaoDAL revistaArtigoPermissaoDAL
        {
            get
            {
                if (_revistaArtigoPermissaoDAL == null)
                    _revistaArtigoPermissaoDAL = new RevistaArtigoPermissaoADO();
                return _revistaArtigoPermissaoDAL;
            }
        }


        private IRevistaSecaoDAL _revistaSecaoDAL;
        private IRevistaSecaoDAL revistaSecaoDAL
        {
            get
            {
                if (_revistaSecaoDAL == null)
                    _revistaSecaoDAL = new RevistaSecaoADO();
                return _revistaSecaoDAL;
            }
        }

        private IRevistaArtigoDAL _revistaArtigoDAL;
        private IRevistaArtigoDAL revistaArtigoDAL
        {
            get
            {
                if (_revistaArtigoDAL == null)
                    _revistaArtigoDAL = new RevistaArtigoADO();
                return _revistaArtigoDAL;
            }
        }

        private IRevistaAssinaturaDAL _revistaAssinaturaDAL;
        private IRevistaAssinaturaDAL revistaAssinaturaDAL
        {
            get
            {
                if (_revistaAssinaturaDAL == null)
                    _revistaAssinaturaDAL = new RevistaAssinaturaADO();
                return _revistaAssinaturaDAL;
            }
        }



        private IArquivoDAL _arquivoDAL;
        private IArquivoDAL arquivoADO
        {
            get
            {
                if (_arquivoDAL == null)
                    _arquivoDAL = new ArquivoADO();
                return _arquivoDAL;
            }
        }

        #endregion

        #region Métodos

        public void AtualizarRevista(Revista revista)
        {
            RevistaDal.Atualizar(revista);
        }

        /// <summary>
        /// Método que Persiste a inserção em RevistaGrupoAEdicao
        /// </summary>
        public void InserirRevistaGrupoAEdicao(RevistaGrupoAEdicao revista)
        {
            revistaGrupoA.Inserir(revista);
        }

        /// <summary>
        /// Método que Atualiza RevistaGrupoAEdicao
        /// </summary>
        public void AtualizarRevistaGrupoAEdicao(RevistaGrupoAEdicao revista)
        {
            revistaGrupoA.Atualizar(revista);
        }

        /// <summary>
        /// Método que Atualiza registros em RevistaEdicao
        /// </summary>
        public void AtualizarRevistaEdicao(RevistaEdicao revista)
        {
            revistaEdicaoDAL.Atualizar(revista);
        }

        /// <summary>
        /// Método que Carrega registros de RevistaGrupoAEdicao
        /// </summary>
        public RevistaGrupoAEdicao CarregarRevistaGrupoAEdicao(RevistaGrupoAEdicao revista)
        {
            return revistaGrupoA.Carregar(revista);
        }

        /// <summary>
        /// Método que Carrega registros de RevistaGrupoAEdicao com Dependências de Arquivos
        /// </summary>
        public RevistaGrupoAEdicao CarregarRevistaGrupoAEdicaoComArquivos(RevistaGrupoAEdicao revista)
        {
            return revistaGrupoA.CarregarComArquivos(revista);
        }

        /// <summary>
        /// Método que Persiste a Exclusão registros de RevistaGrupoAEdicao
        /// </summary>
        public void ExcluirRevistaGrupoAEdicao(RevistaGrupoAEdicao revista)
        {
            revistaGrupoA.Excluir(revista);
        }

        /// <summary>
        /// Método que verifica se existe registros em RevistaGrupoAEdicao - Insercao
        /// </summary>
        public bool ExisteRevistaGrupoAEdicaoInsercao(RevistaGrupoAEdicao revista)
        {
            RevistaGrupoAEdicaoFH fh = new RevistaGrupoAEdicaoFH();
            fh.AnoPublicacao = revista.AnoPublicacao;
            fh.MesPublicacao = revista.MesPublicacao;
            fh.NumeroEdicao = revista.NumeroEdicao.ToString();
            if (revistaGrupoA.TotalRegistros(fh) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Método que verifica se existe registros em RevistaGrupoAEdicao - Alteração
        /// </summary>
        public bool ExisteRevistaGrupoAEdicaoAleracao(RevistaGrupoAEdicao revista)
        {
            if (revistaGrupoA.ValidarAtualizacao(revista) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Método que Carrega Ultimas Edicoes Por Area de Interesse
        /// </summary>
        public IEnumerable<RevistaEdicao> CarregarUltimasEdicoesPorAreaInteresse(List<Categoria> categoriasDasAreasDeInteresse, Int32 quantidadeRegistros)
        {
            if (categoriasDasAreasDeInteresse == null || categoriasDasAreasDeInteresse.Count == 0)
            {
                throw new ArgumentException("Áreas de interesse não informadas!");
            }
            return revistaEdicaoDAL.CarregarUltimasEdicoesPorAreaInteresse(categoriasDasAreasDeInteresse, quantidadeRegistros);
        }

        /// <summary>
        /// Carrega todas as Imagens de um Evento conforme código identificador recebido
        /// </summary>
        /// <param name="entidade">Objeto Entidade que possui o identificador eventoId</param>
        /// <returns>Lista de Imagens do Evento</returns>
        public List<EventoImagem> carregarTodosEventoImagem(Evento entidade)
        {
            var eventoImagemFH = new EventoImagemFH() { EventoId = entidade.EventoId.ToString() };
            List<EventoImagem> imagens = new List<EventoImagem>();
            //foreach (EventoImagem eventoImagem in eventoImagemADO.CarregarTodosArquivos(0, 0, null, null, eventoImagemFH))
            //{
            //    //eventoImagem.Arquivo = arquivoADO.CarregarAutor(new Arquivo() { ArquivoId = eventoImagem.Arquivo.ArquivoId });
            //    imagens.Add(eventoImagem);
            //}

            return imagens;
        }

        /// <summary>
        /// Carrega um Arquivo conforme o código identificador recebido
        /// </summary>
        /// <param name="entidade">Objeto Arquivo que contém o identificador arquivoId</param>
        /// <returns>Objeto Arquivo populado</returns>
        public Arquivo carregarArquivo(Arquivo entidade)
        {
            return arquivoADO.Carregar(entidade);
        }

        /// <summary>
        /// Insere uma nova Imagem ligada ao Evento
        /// </summary>
        /// <param name="entidade">Objeto EventoImagem a ser inserido</param>
        /// <returns>Objeto EventoImagem contendo o código inserido e demais informações</returns>
        public EventoImagem inserirEventoImagem(EventoImagem entidade)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    if (entidade.Arquivo.ArquivoId == 0)
            //    {
            //        var arquivo = new Arquivo();
            //        arquivo.NomeArquivo = entidade.Arquivo.NomeArquivo;
            //        arquivo.NomeArquivoOriginal = entidade.Arquivo.NomeArquivoOriginal;
            //        arquivo.TamanhoArquivo = entidade.Arquivo.TamanhoArquivo;
            //        arquivo.DataHoraUpload = entidade.Arquivo.DataHoraUpload;
            //        arquivoADO.InserirNovoAutor(arquivo);
            //        entidade.Arquivo.ArquivoId = arquivo.ArquivoId;
            //        eventoImagemADO.InserirNovoAutor(entidade);
            //    }
            //    scope.Complete();

            //}           
            return entidade;
        }

        /// <summary>
        /// Método que Carrega registros de RevistaGrupoAEdicao com dependências de Arquivos
        /// </summary>
        public List<RevistaGrupoAEdicao> CarregarTodosComArquivos()
        {
            return revistaGrupoA.CarregarTodosComArquivos();
        }

        /// <summary>
        /// Método que Carrega registros de RevistaEdicao
        /// </summary>
        public RevistaEdicao CarregarRevistaEdicao(RevistaEdicao entidade)
        {
            RevistaEdicao revistaEdicao = new RevistaEdicao();
            revistaEdicao = revistaEdicaoDAL.Carregar(entidade);
            revistaEdicao.Revista = new RevistaGrupoABLL().CarregarRevista(new Revista() { RevistaId = revistaEdicao.Revista.RevistaId });
            return revistaEdicao;
        }

        /// <summary>
        /// Método que Carrega registros de RevistaAssinatura
        /// </summary>
        public RevistaAssinatura CarregarRevistaAssinatura(RevistaAssinatura entidade)
        {
            RevistaAssinatura revistaAssinatura = new RevistaAssinatura();
            revistaAssinatura = revistaAssinaturaDAL.Carregar(entidade);
            revistaAssinatura.Revista = new RevistaGrupoABLL().CarregarRevista(new Revista() { RevistaId = revistaAssinatura.Revista.RevistaId });
            return revistaAssinatura;
        }

        /// <summary>
        /// Método que Carrega registros de Revista
        /// </summary>
        public Revista CarregarRevista(Revista revista)
        {
            return revistaDAL.Carregar(revista);
        }

        /// <summary>
        /// Método que Carrega todos registros de Revista
        /// </summary>
        public IEnumerable<Revista> CarregarTodasRevistas()
        {
            return revistaDAL.CarregarTodos();
        }

        /// <summary>
        /// Método que Carrega RevistaSecao
        /// </summary>
        public RevistaSecao CarregarRevistaSecao(RevistaSecao entidade)
        {
            RevistaSecao revistaSecao = revistaSecaoDAL.Carregar(entidade);
            revistaSecao.Revista = new RevistaGrupoABLL().CarregarRevista(new Revista() { RevistaId = revistaSecao.Revista.RevistaId });
            return revistaSecao;
        }

        /// <summary>
        /// Método de persistência - Update em RevistaSecao
        /// </summary>
        public void AtualizarRevistaSecao(RevistaSecao revista)
        {
            revistaSecaoDAL.Atualizar(revista);
        }

        /// <summary>
        /// Método de persistência - Update em RevistaAssinatura
        /// </summary>
        public void AtualizarRevistaAssinatura(RevistaAssinatura revista)
        {
            revistaAssinaturaDAL.Atualizar(revista);

            revistaDAL.Atualizar(revista.Revista);
        }

        /// <summary>
        /// Método de persistência - Insert em RevistaSecao
        /// </summary>
        public void InserirRevistaSecao(RevistaSecao revista)
        {
            revistaSecaoDAL.Inserir(revista);
        }

        /// <summary>
        /// Método de persistência - Insert em RevistaAssinatura
        /// </summary>
        public void InserirRevistaAssinatura(RevistaAssinatura revista)
        {
            revistaAssinaturaDAL.Inserir(revista);
        }

        /// <summary>
        /// Método de persistência - Delete em RevistaSecao
        /// </summary>
        public void ExcluirRevistaSecao(RevistaSecao revistaSecao)
        {
            revistaSecao = this.CarregarRevistaSecao(revistaSecao);

            using (TransactionScope scope = new TransactionScope())
            {
                revistaSecaoDAL.Excluir(revistaSecao);
                scope.Complete();
            }
        }

        /// <summary>
        /// Método de persistência - Delete em RevistaArtigo
        /// </summary>
        public void ExcluirRevistaArtigo(RevistaArtigo revistaArtigo)
        {
            revistaArtigo = this.CarregarRevistaArtigo(revistaArtigo);

            using (TransactionScope scope = new TransactionScope())
            {
                revistaArtigoDAL.Excluir(revistaArtigo);
                scope.Complete();
            }
        }

        /// <summary>
        /// Método de persistência - Delete de Auto-Relacionamentos em RevistaArtigo
        /// </summary>
        public void ExcluirRelacionamentoRevistaArtigoIdAssociado(int revistaArtigoId)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                revistaArtigoDAL.ExcluirRelacionamentoRevistaArtigoIdAssociado(revistaArtigoId);
                scope.Complete();
            }
        }

        /// <summary>
        /// Método que Carrega Todas Edicoes Por RevistaId de RevistaEdicao
        /// </summary>
        public IEnumerable<RevistaEdicao> CarregarTodasEdicoesPorRevistaId(int revistaId)
        {
            return revistaEdicaoDAL.CarregarTodasEdicoesPorRevistaId(revistaId);
        }

        /// <summary>
        /// Método que Carrega Todas Secoes Por Revista Id de RevistaSecao
        /// </summary>
        public IEnumerable<RevistaSecao> CarregarTodasSecoesPorRevistaId(int revistaId)
        {
            return revistaSecaoDAL.CarregarTodasSecoesPorRevistaId(revistaId);
        }

        /// <summary>
        /// Método que Carrega Todas permissões de RevistaArtigoPermissao
        /// </summary>
        public IEnumerable<RevistaArtigoPermissao> CarregarTodasRevistaArtigoPermissao()
        {
            return revistaArtigoPermissaoDAL.CarregarTodos();
        }

        /// <summary>
        /// Método que Carrega Todos Artigos Por RevistaEdicaoId de Revista
        /// </summary>
        public IEnumerable<RevistaArtigo> CarregarTodosArtigosPorRevistaEdicaoId(int revistaEdicaoId)
        {
            return revistaArtigoDAL.CarregarTodosArtigosPorRevistaEdicaoId(revistaEdicaoId);
        }

        /// <summary>
        /// Método de persistência - Update em RevistaArtigo
        /// </summary>
        public void AtualizarRevistaArtigo(RevistaArtigo revista, List<Categoria> categorias)
        {
            this.AtualizarRevistaArtigo(revista, categorias, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <param name="categorias"></param>
        /// <param name="revistaArtigoControversiaSim"></param>
        /// <param name="revistaArtigoControversiaNao"></param>
        public void AtualizarRevistaArtigo(RevistaArtigo revista, List<Categoria> categorias, List<RevistaArtigoControversia> controversias)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                revista.Conteudo = new Conteudo() { ConteudoId = revista.RevistaArtigoId };

                // Atualização de Categorias
                // a. Exclui todos os relacionamentos com áreas de conhecimento
                new ConteudoADO().ExcluirTodasAreasConhecimento(revista.Conteudo);
                // b. Inclui os novos relacionamentos
                foreach (Categoria categoria in categorias)
                    new ConteudoADO().InserirRelacionamentoAreaConhecimento(revista.Conteudo, categoria);

                if (controversias != null && controversias.Count > 0)
                {
                    foreach (RevistaArtigoControversia controversia in controversias)
                    {
                        if (controversia.RevistaArtigoControversiaId > 0)
                        {
                            new RevistaArtigoControversiaBLL().Atualizar(controversia);
                        }
                        else
                        {
                            new RevistaArtigoControversiaBLL().Inserir(controversia);
                        }
                    }
                }

                revistaArtigoDAL.Atualizar(revista);
                scope.Complete();
            }
        }

        /// <summary>
        /// Método de persistência - Insert em RevistaArtigo
        /// </summary>
        public void InserirRevistaArtigo(RevistaArtigo revista, List<Categoria> categorias)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Inserção de Categorias
                foreach (Categoria categoria in categorias)
                    new ConteudoADO().InserirRelacionamentoAreaConhecimento(revista.Conteudo, categoria);

                revistaArtigoDAL.Inserir(revista);
                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        public Boolean ValidarSecaoCapaParaRevistaEdicao(Int32 revistaEdicao, Int32 revistaArtigoId)
        {
            return (revistaArtigoDAL.ValidarArtigoPrincipalCapa(revistaEdicao, revistaArtigoId) > 0 ? false : true);
        }

        /// <summary>
        /// Método que Carrega registros de RevistaArtigo
        /// </summary>
        public RevistaArtigo CarregarRevistaArtigo(RevistaArtigo entidade)
        {
            RevistaArtigo revistaArtigo = new RevistaArtigo();
            revistaArtigo = revistaArtigoDAL.CarregarComDependencias(entidade);
            if (revistaArtigo.RevistaEdicao != null)
            {
                revistaArtigo.RevistaEdicao = new RevistaGrupoABLL().CarregarRevistaEdicao(new RevistaEdicao() { RevistaEdicaoId = revistaArtigo.RevistaEdicao.RevistaEdicaoId });
            }

            revistaArtigo.RevistaSecao = new RevistaGrupoABLL().CarregarRevistaSecao(revistaArtigo.RevistaSecao);

            return revistaArtigo;
        }

        /// <summary>
        /// Método que Carrega Arquivos de Imagens da Tabela RevistaArtigoGaleriaImagem
        /// </summary>
        public Arquivo CarregarArquivo(Arquivo entidade)
        {
            return ArquivoDal.Carregar(entidade);
        }

        /// <summary>
        /// Método de persistência - Delete de Arquivos de Imagens da Tabela RevistaArtigoGaleriaImagem conforme revistaArtigoId e arquivoId rpassados
        /// </summary>
        public void ExcluirRevistaArtigoImagem(int revistaArtigoId, int arquivoId, bool todosArquivos)
        {
            revistaArtigoDAL.ExcluirRevistaArtigoImagem(revistaArtigoId, arquivoId, todosArquivos);
        }

        /// <summary>
        /// Método de persistência - Delete de Arquivos da Tabela RevistaArtigoLocalizacaoImagem
        /// </summary>
        public void ExcluirRevistaArtigoArquivo(int arquivoId)
        {
            revistaArtigoDAL.ExcluirRevistaArtigoArquivo(arquivoId);
        }

        /// <summary>
        /// Método de persistência - Insert em RevistaArtigoGaleriaImagem
        /// </summary>
        public RevistaGaleriaArtigoImagem InserirRevistaArtigoGaleria(RevistaGaleriaArtigoImagem entidade)
        {
            if (entidade.Arquivo.ArquivoId == 0)
            {
                var arquivo = new Arquivo();
                arquivo.NomeArquivo = entidade.Arquivo.NomeArquivo;
                arquivo.NomeArquivoOriginal = entidade.Arquivo.NomeArquivoOriginal;
                arquivo.TamanhoArquivo = entidade.Arquivo.TamanhoArquivo;
                arquivo.DataHoraUpload = entidade.Arquivo.DataHoraUpload;
                ArquivoDal.Inserir(arquivo);
                entidade.Arquivo.ArquivoId = arquivo.ArquivoId;
                revistaArtigoDAL.InserirRevistaArtigoGaleriaImagem(entidade);
            }
            return entidade;
        }

        /// <summary>
        /// Método que Carrega todos registros de em RevistaGaleriaArtigoImagem
        /// </summary>
        public IEnumerable<RevistaGaleriaArtigoImagem> CarregarTodasRevistaGaleriaArtigoImagem(int revistaArtigoId)
        {
            IEnumerable<RevistaGaleriaArtigoImagem> imagens = revistaArtigoDAL.CarregarTodasRevistaGaleriaArtigoImagens(revistaArtigoId);
            return imagens;
        }

        public List<RevistaGrupoAEdicao> CarregarTodosComArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return revistaGrupoA.CarregarTodosComArquivos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoId(Int32 revistaEdicaoId, Boolean destaque)
        {
            return this.CarregarArtigosPorRevistaEdicaoId(0, 0, revistaEdicaoId, destaque, (Nullable<Int32>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="qtdRegistros"></param>
        /// <param name="destaque"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoId(Int32 revistaEdicaoId, Int32 qtdRegistros, Boolean destaque)
        {
            return this.CarregarArtigosPorRevistaEdicaoId(0, 0, revistaEdicaoId, qtdRegistros, destaque, (Nullable<Int32>)null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="revistaSecaoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, Int32 revistaEdicaoId, Boolean destaque, Int32? revistaSecaoId)
        {
            return this.CarregarArtigosPorRevistaEdicaoId(registrosPagina, numeroPagina, revistaEdicaoId, 0, destaque, revistaSecaoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="qtdRegistros"></param>
        /// <param name="destaque"></param>
        /// <param name="revistaSecaoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, Int32 revistaEdicaoId, Int32 qtdRegistros, Boolean destaque, Int32? revistaSecaoId)
        {
            return revistaArtigoDAL.CarregarArtigosPorRevistaEdicaoId(registrosPagina, numeroPagina, revistaEdicaoId, qtdRegistros, destaque, revistaSecaoId);
        }

        public Int32 ContarArtigosPorRevistaEdicaoId(Int32 revistaEdicaoId, Boolean destaque, Int32? revistaSecaoId)
        {
            return revistaArtigoDAL.ContarArtigosPorRevistaEdicaoId(revistaEdicaoId, destaque, revistaSecaoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public RevistaArtigo CarregarComDependencia(RevistaArtigo entidade)
        {
            return revistaArtigoDAL.CarregarCompleto(entidade.RevistaArtigoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public RevistaEdicao CarregarMaiorEdicaoPorRevistaId(Int32 revistaId)
        {
            return revistaEdicaoDAL.CarregarMaiorEdicaoPorRevistaId(revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaSecao> CarregarTodasSecoesPorRevistaIdEdicaoId(Int32 revistaId, Int32 revistaEdicaoId)
        {
            return revistaSecaoDAL.CarregarTodasSecoesPorRevistaIdEdicaoId(revistaId, revistaEdicaoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarTodosArtigosSelecionados(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32 revistaId)
        {
            return revistaArtigoDAL.CarregarTodosArtigosSelecionados(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public Int32 ContarArtigosSelecionados(Int32 revistaId)
        {
            return revistaArtigoDAL.ContarArtigosSelecionados(revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public RevistaArtigo CarregarArtigoCapaPorRevistaEdicaoId(Int32 revistaEdicaoId)
        {
            return revistaArtigoDAL.CarregarArtigoCapaPorRevistaEdicaoId(revistaEdicaoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosSemDestaquePorRevistaEdicaoId(Int32 revistaEdicaoId)
        {
            return revistaArtigoDAL.CarregarArtigosSemDestaquePorRevistaEdicaoId(revistaEdicaoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosConteudoOnlinePorRevistaEdicaoId(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32 revistaEdicaoId, string revistaId)
        {
            return revistaArtigoDAL.CarregarArtigosConteudoOnlinePorRevistaEdicaoId(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, revistaEdicaoId, revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public Int32 ContarArtigosConteudoOnlinePorRevistaEdicaoId(Int32 revistaEdicaoId, string revistaId)
        {
            return revistaArtigoDAL.ContarArtigosConteudoOnlinePorRevistaEdicaoId(revistaEdicaoId, revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaEdicaoId"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<RevistaArtigo> CarregarArtigosPorRevistaEdicaoIdParaSite(Int32 revistaEdicaoId, Int32 qtdRegistros)
        {
            return revistaArtigoDAL.CarregarArtigosPorRevistaEdicaoIdParaSite(revistaEdicaoId, qtdRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Int32 CarregarRevistaIdUltimaEdicaoCadastrada()
        {
            return revistaEdicaoDAL.CarregarRevistaIdUltimaEdicaoCadastrada();
        }

        #endregion
    }
}
