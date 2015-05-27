using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System.Transactions;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.BusinessLogicalLayer.Helper;
using System.Text.RegularExpressions;


namespace GrupoA.BusinessLogicalLayer
{
    public class TituloBLL : BaseBLL
    {
        #region Declarações DAL

        private ITituloDAL _tituloDal;
        private ITituloImpressoDAL _tituloImpressoDal;
        private ITituloEletronicoDAL _tituloEletronicoDal;
        private ITituloAutorDAL _tituloAutorDal;
        private ITituloInformacaoSobreAutorDAL _tituloInformacaoSobreAutorDAL;
        private ITituloInformacaoResumoDAL _tituloInformacaoResumoDAL;
        private ITituloInformacaoSumarioDAL _tituloInformacaoSumarioDAL;
        private ITituloInformacaoFichaDAL _tituloInformacaoFichaDAL;
        private ITituloInformacaoComentarioEspecialistaDAL _tituloInformacaoComentarioEspecialistaDAL;
        private ITituloInformacaoComentarioEspecialistaCategoriaDAL _tituloInformacaoComentarioEspecialistaCategoriaDAL;
        private ITituloInformacaoMaterialDidaticoDAL _tituloInformacaoMaterialDidaticoDAL;
        private ITituloConteudoExtraArquivoDAL _tituloConteudoExtraArquivoDal;
        private ITituloConteudoExtraMidiaDAL _tituloConteudoExtraMidiaDal;
        private ITituloConteudoExtraUrlDAL _tituloConteudoExtraUrlDal;
        private IArquivoDAL _arquivoDAL;
        private IAutorDAL _autorDal;
        private IPromocaoDAL _promocaoDal;


        private IAutorDAL AutorDal
        {
            get { return _autorDal ?? (_autorDal = new AutorADO()); }
        }

        private IPromocaoDAL PromocaoDal
        {
            get { return _promocaoDal ?? (_promocaoDal = new PromocaoADO()); }
        }

        private ITituloDAL TituloDal
        {
            get { return _tituloDal ?? (_tituloDal = new TituloADO()); }
        }

        private ITituloImpressoDAL TituloImpressoDal
        {
            get { return _tituloImpressoDal ?? (_tituloImpressoDal = new TituloImpressoADO()); }
        }

        private ITituloEletronicoDAL TituloEletronicoDal
        {
            get { return _tituloEletronicoDal ?? (_tituloEletronicoDal = new TituloEletronicoADO()); }
        }


        private ITituloAutorDAL TituloAutorDal
        {
            get { return _tituloAutorDal ?? (_tituloAutorDal = new TituloAutorADO()); }
        }

        private ITituloInformacaoSobreAutorDAL TituloInformacaoSobreAutorDAL
        {
            get
            {
                if (_tituloInformacaoSobreAutorDAL == null)
                    _tituloInformacaoSobreAutorDAL = new TituloInformacaoSobreAutorADO();
                return _tituloInformacaoSobreAutorDAL;
            }
        }
        private ITituloInformacaoResumoDAL TituloInformacaoResumoDAL
        {
            get
            {
                if (_tituloInformacaoResumoDAL == null)
                    _tituloInformacaoResumoDAL = new TituloInformacaoResumoADO();
                return _tituloInformacaoResumoDAL;
            }
        }
        private ITituloInformacaoSumarioDAL TituloInformacaoSumarioDAL
        {
            get
            {
                if (_tituloInformacaoSumarioDAL == null)
                    _tituloInformacaoSumarioDAL = new TituloInformacaoSumarioADO();
                return _tituloInformacaoSumarioDAL;
            }
        }
        private ITituloInformacaoFichaDAL TituloInformacaoFichaDAL
        {
            get
            {
                if (_tituloInformacaoFichaDAL == null)
                    _tituloInformacaoFichaDAL = new TituloInformacaoFichaADO();
                return _tituloInformacaoFichaDAL;
            }
        }
        private ITituloInformacaoComentarioEspecialistaDAL TituloInformacaoComentarioEspecialistaDAL
        {
            get
            {
                if (_tituloInformacaoComentarioEspecialistaDAL == null)
                    _tituloInformacaoComentarioEspecialistaDAL = new TituloInformacaoComentarioEspecialistaADO();
                return _tituloInformacaoComentarioEspecialistaDAL;
            }
        }
        private ITituloInformacaoComentarioEspecialistaCategoriaDAL TituloInformacaoComentarioEspecialistaCategoriaDAL
        {
            get
            {
                if (_tituloInformacaoComentarioEspecialistaCategoriaDAL == null)
                    _tituloInformacaoComentarioEspecialistaCategoriaDAL = new TituloInformacaoComentarioEspecialistaCategoriaADO();
                return _tituloInformacaoComentarioEspecialistaCategoriaDAL;
            }
        }
        private ITituloInformacaoMaterialDidaticoDAL TituloInformacaoMaterialDidaticoDAL
        {
            get
            {
                if (_tituloInformacaoMaterialDidaticoDAL == null)
                    _tituloInformacaoMaterialDidaticoDAL = new TituloInformacaoMaterialDidaticoADO();
                return _tituloInformacaoMaterialDidaticoDAL;
            }
        }
        private IArquivoDAL ArquivoDAL
        {
            get
            {
                if (_arquivoDAL == null)
                    _arquivoDAL = new ArquivoADO();
                return _arquivoDAL;
            }
        }


        private ITituloConteudoExtraArquivoDAL TituloConteudoExtraArquivoDal
        {
            get
            {
                if (this._tituloConteudoExtraArquivoDal == null)
                    this._tituloConteudoExtraArquivoDal = new TituloConteudoExtraArquivoADO();
                return this._tituloConteudoExtraArquivoDal;
            }
        }

        private ITituloConteudoExtraMidiaDAL TituloConteudoExtraMidiaDal
        {
            get
            {
                if (this._tituloConteudoExtraMidiaDal == null)
                    this._tituloConteudoExtraMidiaDal = new TituloConteudoExtraMidiaADO();
                return _tituloConteudoExtraMidiaDal;
            }
        }

        private ITituloConteudoExtraUrlDAL TituloConteudoExtraUrlDal
        {
            get
            {
                if (this._tituloConteudoExtraUrlDal == null)
                    this._tituloConteudoExtraUrlDal = new TituloConteudoExtraUrlADO();
                return this._tituloConteudoExtraUrlDal;
            }
        }


        #endregion

        public IEnumerable<Titulo> CarregarItensSalaDeAula(Usuario usuario, Int32 quantidadeRegistros)
        {
            return TituloDal.CarregarItensSalaDeAula(usuario, quantidadeRegistros);
        }

        /// <summary>
        /// Carrega os títulos para a estante.
        /// </summary>
        /// <param name="categoriaPai">Objecto do tipo Categoria com o identificador setado.</param>
        /// <returns>Coleção de objetos do tipo EstanteTituloVH.</returns>
        public List<EstanteTituloVH> CarregaTitulosParaEstantePorCategoria(Categoria categoriaPai)
        {
            List<EstanteTituloVH> estanteTituloVhs;

            if (categoriaPai != null)
            {
                string storageKey = string.Format("Carrega_Titulos_Para_Estante_Por_Categoria_CategoriaPai_{0}", categoriaPai.CategoriaId.ToString());
                estanteTituloVhs = Cache.Retrieve<List<EstanteTituloVH>>(storageKey);
                if (estanteTituloVhs == null)
                {
                    estanteTituloVhs =
                        TituloDal.CarregaTitulosParaEstantePorCategoria(new List<Categoria>() { categoriaPai }, false);
                    Cache.Store(storageKey, estanteTituloVhs, DateTime.Now.AddMinutes(10));
                }
            }
            else
            {
                string storageKey = string.Format("Carrega_Titulos_Para_Estante_Por_Categoria__CategoriaPai_Todas");
                estanteTituloVhs = Cache.Retrieve<List<EstanteTituloVH>>(storageKey);
                if (estanteTituloVhs == null)
                {
                    estanteTituloVhs = TituloDal.CarregaTitulosParaEstantePorCategoria(
                        null, false);
                    Cache.Store(storageKey, estanteTituloVhs, DateTime.Now.AddMinutes(10));
                }
            }
            return estanteTituloVhs;
        }

        /// <summary>
        /// Carrega a estante de titulos com os itens e interesse randomico de um determinado usuario.
        /// </summary>
        /// <param name="categorias"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTituloPorCategorias(List<Categoria> categorias, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregaTitulosParaEstantePorCategoria(categorias, numeroItensSeremExibidos, false);
        }

        /// <summary>
        /// Busca em TituloImpresso ou TituloEletronico para encontrar o Titulo
        /// </summary>
        /// <param name="Isbn13"></param>
        /// <returns></returns>
        public Titulo CarregarTituloPorISBN13(String Isbn13)
        {
            Titulo tituloBO = new Titulo();

            TituloImpresso tituloImpressoBO = new TituloImpressoBLL().CarregarPorIsbn13(Isbn13);

            if (tituloImpressoBO != null && tituloImpressoBO.Titulo != null && tituloImpressoBO.Titulo.TituloId > 0)
            {
                tituloBO = tituloImpressoBO.Titulo;
            }
            else
            {
                TituloEletronico tituloEletronicoBO = new TituloEletronicoBLL().CarregarPorIsbn13(Isbn13);

                if (tituloEletronicoBO != null && tituloEletronicoBO.Titulo != null && tituloEletronicoBO.Titulo.TituloId > 0)
                {
                    tituloBO = tituloEletronicoBO.Titulo;
                }
            }

            return tituloBO;
        }

        /// <summary>
        /// Método que carrega um título por ISBN13
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public TituloImpresso CarregarPorISBN13(String Isbn13)
        {
            TituloImpresso entidade = TituloImpressoDal.CarregarPorIsbn13(Isbn13);

            if (entidade != null)
            {
                entidade.Titulo = TituloDal.CarregaTituloComComentarioDoEspecialista(entidade.Titulo);
            }

            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<TituloEletronico> CarregarTodosPorISBN13(TituloEletronico entidade)
        {
            var tituloFH = new TituloEletronicoFH() { Isbn13 = entidade.Isbn13 };
            IEnumerable<TituloEletronico> titulos = this.TituloEletronicoDal.CarregarTodos(0, 0, null, null, tituloFH);
            return titulos;
        }

        /// <summary>
        /// Método que carrega um Título com a dependências de Produto e Conteudo já configuradas.
        /// </summary>
        /// <param name="titulo">Título com identificador a ser carregado.</param>
        /// <returns>Título com os dados configurados.</returns>
        public Titulo CarregarComDependencias(Titulo titulo)
        {
            titulo = TituloDal.CarregarComDependencias(titulo);
            if (titulo != null)
                titulo.TituloAutores = (List<TituloAutor>)TituloAutorDal.Carregar(titulo);
            return titulo;
        }

        /// <summary>
        /// Método que carrega um título.
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public Titulo Carregar(Titulo titulo)
        {
            return TituloDal.Carregar(titulo);
        }

        /// <summary>
        /// Método que carrega um título impresso.
        /// </summary>
        /// <param name="tituloImpresso"></param>
        /// <returns></returns>
        public TituloImpresso CarregarTituloImpresso(TituloImpresso tituloImpresso)
        {
            return TituloImpressoDal.Carregar(tituloImpresso);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRecomendados()
        {
            List<EstanteTituloVH> list = this.CarregarTitulosRecomendadosPorCategoria(null);

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRecomendadosPorCategoria(Categoria categoria)
        {
            return this.CarregarTitulosRecomendadosPorCategoria(categoria, 6);
        }

        public List<EstanteTituloVH> CarregarTitulosRecomendadosPorCategoria(Usuario usuario, Int32 qtdRegistros)
        {
            List<EstanteTituloVH> list = TituloDal.CarregarRecomendadosPorCategoria(usuario, qtdRegistros);

            if (list.Count < qtdRegistros)
            {
                list = TituloDal.CarregarLivrosQueDeixaramDeSerLancamentoPorCategoria(list, null, (qtdRegistros - list.Count));
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRecomendadosPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            List<EstanteTituloVH> list = TituloDal.CarregarRecomendadosPorCategoria(categoria, qtdRegistros);

            if (list.Count < qtdRegistros)
            {
                list = TituloDal.CarregarLivrosQueDeixaramDeSerLancamentoPorCategoria(list, categoria, (qtdRegistros - list.Count));
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaArtigo"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRelacionadosRevistaArtigo(RevistaArtigo revistaArtigo)
        {
            return TituloDal.CarregarTitulosRelacionadosRevistaArtigo(revistaArtigo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRecomendadosPorArea(int areaId)
        {
            return TituloDal.CarregarRecomendadosPorArea(areaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosMaisVistos()
        {
            return this.CarregarTitulosMaisVistosPorCategoria(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosMaisVistosPorCategoria(Categoria categoria)
        {
            return this.CarregarTitulosMaisVistosPorCategoria(categoria, 7);
        }

        public List<EstanteTituloVH> CarregarTitulosMaisVistosPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            return TituloDal.CarregarMaisVistos(categoria, qtdRegistros);
        }

        public List<EstanteTituloVH> CarregarTitulosMaisVistosPorCategoria(Usuario usuario, Int32 qtdRegistros)
        {
            return TituloDal.CarregarMaisVistos(usuario, qtdRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosMaisVistosPorArea(int areaId)
        {
            return TituloDal.CarregarMaisVistosPorArea(areaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosImpressosLancamentosPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregarLancamentosPorCategoria(categoria, numeroItensSeremExibidos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosImpressosOfertaPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregarOfertasPorCategoria(categoria, numeroItensSeremExibidos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosImpressosCompraConjuntaPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregarCompraConjuntaPorCategoria(categoria, numeroItensSeremExibidos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosMaisVendidosPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregarMaisVendidosPorCategoria(categoria, numeroItensSeremExibidos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosEBooksPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregarEBooksPorCategoria(categoria, numeroItensSeremExibidos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="numeroItensSeremExibidos"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosOfertasPorCategoria(Categoria categoria, int numeroItensSeremExibidos)
        {
            return TituloDal.CarregarOfertasPorCategoria(categoria, numeroItensSeremExibidos);
        }

        public List<TituloVH> CarregarTituloComMaterialExtraArquivo(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, Usuario usuario, Categoria categoria)
        {
            return new TituloADO().CarregarTituloComMaterialExtraArquivo(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, usuario, categoria);
        }

        public int ContarTituloComMaterialExtraArquivo(Usuario usuario, Categoria categoria)
        {
            return new TituloADO().ContarTituloComMaterialExtraArquivo(usuario, categoria);
        }

        /// <summary>
        ///Carrega informações complementares para o título
        /// </summary>        
        public TituloImpresso CarregarTituloImpressoComComentarioDoEspecialista(TituloImpresso entidade)
        {
            //Carrega titulo, titulo.produto e titulo.conteudo
            entidade = TituloImpressoDal.Carregar(entidade);
            entidade.Titulo = TituloDal.CarregaTituloComComentarioDoEspecialista(entidade.Titulo);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        public void AtualizarTituloComentarioEspecialista(Titulo titulo)
        {
            this.AtualizarTituloComentarioEspecialista(titulo, new List<Categoria>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="categorias"></param>
        public void AtualizarTituloComentarioEspecialista(Titulo titulo, List<Categoria> categorias)
        {
            using (TransactionScope escopoDaTransacao = new TransactionScope())
            {
                //SOBRE COMENTARIO ESPECIALISTA
                if (titulo.TituloInformacaoComentarioEspecialista != null)
                {
                    int formatoId = 1;

                    if (!string.IsNullOrEmpty(titulo.TituloInformacaoComentarioEspecialista.UrlMidia))
                    {
                        formatoId = 3;
                    }
                    else if (titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio != null)
                    {
                        formatoId = 2;
                    }

                    titulo.TituloInformacaoComentarioEspecialista.ComentarioFormato =
                       new ComentarioFormato() { ComentarioFormatoId = formatoId };

                    if (titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
                    {
                        TituloInformacaoComentarioEspecialistaCategoriaDAL.ExcluirTodosPorComentarioEspecialista(titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId);
                        foreach (Categoria categoria in categorias)
                        {
                            TituloInformacaoComentarioEspecialistaCategoria tituloInformacaoComentarioEspecialistaCategoria = new TituloInformacaoComentarioEspecialistaCategoria();
                            tituloInformacaoComentarioEspecialistaCategoria.Categoria = new Categoria();
                            tituloInformacaoComentarioEspecialistaCategoria.Categoria = categoria;
                            tituloInformacaoComentarioEspecialistaCategoria.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
                            tituloInformacaoComentarioEspecialistaCategoria.TituloInformacaoComentarioEspecialista = titulo.TituloInformacaoComentarioEspecialista;

                            new TituloInformacaoComentarioEspecialistaCategoriaADO().Inserir(tituloInformacaoComentarioEspecialistaCategoria);
                        }

                        TituloInformacaoComentarioEspecialistaDAL.Atualizar(titulo.TituloInformacaoComentarioEspecialista);
                    }
                    else
                    {
                        titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId = titulo.TituloId;
                        TituloInformacaoComentarioEspecialistaDAL.Inserir(titulo.TituloInformacaoComentarioEspecialista);

                        foreach (Categoria categoria in categorias)
                        {
                            TituloInformacaoComentarioEspecialistaCategoria tituloInformacaoComentarioEspecialistaCategoria = new TituloInformacaoComentarioEspecialistaCategoria();
                            tituloInformacaoComentarioEspecialistaCategoria.Categoria = new Categoria();
                            tituloInformacaoComentarioEspecialistaCategoria.Categoria = categoria;
                            tituloInformacaoComentarioEspecialistaCategoria.TituloInformacaoComentarioEspecialista = new TituloInformacaoComentarioEspecialista();
                            tituloInformacaoComentarioEspecialistaCategoria.TituloInformacaoComentarioEspecialista = titulo.TituloInformacaoComentarioEspecialista;

                            new TituloInformacaoComentarioEspecialistaCategoriaADO().Inserir(tituloInformacaoComentarioEspecialistaCategoria);
                        }
                    }
                }

                TituloDal.Atualizar(titulo);
                escopoDaTransacao.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloInformacaoComentarioEspecialistaId"></param>
        /// <returns></returns>
        public IEnumerable<Categoria> CarregarCategoriasComentarioEspecialista(Int32 tituloInformacaoComentarioEspecialistaId)
        {
            return new TituloInformacaoComentarioEspecialistaCategoriaADO().CarregarTodasAreasConhecimentoCategoria(tituloInformacaoComentarioEspecialistaId);
        }

        /// <summary>
        ///Carrega informações complementares para o título
        /// </summary>        
        public TituloImpresso CarregarInformacoesComplementares(TituloImpresso entidade)
        {
            //Carrega titulo, titulo.produto e titulo.conteudo
            entidade = TituloImpressoDal.CarregarComDependencias(entidade);
            entidade.Titulo = TituloDal.CarregarComInformacoesComplementares(entidade.Titulo);
            return entidade;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        public void AtualizarTituloInformacoesComplementares(Titulo titulo)
        {
            using (TransactionScope escopoDaTransacao = new TransactionScope())
            {
                //SOBRE AUTOR
                if (titulo.TituloInformacaoSobreAutor != null)
                {
                    if (titulo.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId > 0)
                    {
                        TituloInformacaoSobreAutorDAL.Atualizar(titulo.TituloInformacaoSobreAutor);
                    }
                    else
                    {
                        titulo.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId = titulo.TituloId;
                        TituloInformacaoSobreAutorDAL.Inserir(titulo.TituloInformacaoSobreAutor);
                    }
                }

                //SOBRE RESUMO
                if (titulo.TituloInformacaoResumo != null)
                {
                    if (titulo.TituloInformacaoResumo.TituloInformacaoResumoId > 0)
                    {
                        TituloInformacaoResumoDAL.Atualizar(titulo.TituloInformacaoResumo);
                    }
                    else
                    {
                        titulo.TituloInformacaoResumo.TituloInformacaoResumoId = titulo.TituloId;
                        TituloInformacaoResumoDAL.Inserir(titulo.TituloInformacaoResumo);
                    }
                }

                //SOBRE SUMÁRIO
                if (titulo.TituloInformacaoSumario != null)
                {
                    if (titulo.TituloInformacaoSumario.TituloInformacaoSumarioId > 0)
                    {
                        TituloInformacaoSumarioDAL.Atualizar(titulo.TituloInformacaoSumario);
                    }
                    else
                    {
                        titulo.TituloInformacaoSumario.TituloInformacaoSumarioId = titulo.TituloId;
                        TituloInformacaoSumarioDAL.Inserir(titulo.TituloInformacaoSumario);
                    }
                }
                //SOBRE FICHA TÉCNICA
                if (titulo.TituloInformacaoFicha != null)
                {
                    if (titulo.TituloInformacaoFicha.TituloInformacaoFichaId > 0)
                    {
                        TituloInformacaoFichaDAL.Atualizar(titulo.TituloInformacaoFicha);
                    }
                    else
                    {
                        titulo.TituloInformacaoFicha.TituloInformacaoFichaId = titulo.TituloId;
                        TituloInformacaoFichaDAL.Inserir(titulo.TituloInformacaoFicha);
                    }
                }
                //SOBRE MATERIAL DIDÁTICO
                if (titulo.TituloInformacaoMaterialDidatico != null)
                {
                    if (titulo.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId > 0)
                    {
                        TituloInformacaoMaterialDidaticoDAL.Atualizar(titulo.TituloInformacaoMaterialDidatico);
                    }
                    else
                    {
                        titulo.TituloInformacaoMaterialDidatico.TituloInformacaoMaterialDidaticoId = titulo.TituloId;
                        TituloInformacaoMaterialDidaticoDAL.Inserir(titulo.TituloInformacaoMaterialDidatico);
                    }
                }

                TituloDal.Atualizar(titulo);

                escopoDaTransacao.Complete();
            }
        }

        /// <summary>
        /// Método que retorna os autores de um título.
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public List<Autor> CarregarAutores(Titulo titulo)
        {
            return AutorDal.CarregarAutores(titulo);
        }

        public List<Autor> CarregarAutores(Titulo titulo, Autor autor)
        {
            return AutorDal.CarregarAutores(titulo, autor);
        }

        /// <summary>
        /// Método que persiste os autores de um título.
        /// </summary>
        /// <param name="tituloAutores"></param>
        public void InserirTituloAutor(List<TituloAutor> tituloAutores)
        {
            foreach (var item in tituloAutores)
            {
                TituloAutorDal.Inserir(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        public void ExcluirTodosTituloAutor(Titulo titulo)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TituloAutorDal.ExcluirTodosPorTitulo(titulo);
                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloAutores"></param>
        /// <param name="titulo"></param>
        public void AtualizarTituloAutor(List<TituloAutor> tituloAutores, Titulo titulo)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                this.ExcluirTodosTituloAutor(titulo);

                foreach (var item in tituloAutores)
                {
                    TituloAutorDal.Inserir(item);
                }
                scope.Complete();
            }
        }

        /// <summary>
        /// Método que remove o relacionamento entre autor e título.
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirTituloAutor(TituloAutor entidade)
        {
            TituloAutorDal.ExcluirTituloAutor(entidade);
        }

        /// <summary>
        /// Atualiza o conteudo extra do titulo
        /// </summary>
        /// <param name="titulo"></param>
        public void AtualizarConteudoExtra(Titulo titulo)
        {
            if (titulo.TituloConteudoExtraMidia != null)
            {
                this.AtualizaConteudoExtraMidia(titulo.TituloConteudoExtraMidia);
            }

            if (titulo.TituloConteudoExtraUrl != null)
            {
                this.AtualizaConteudoExtraUrl(titulo.TituloConteudoExtraUrl);
            }

        }

        /// <summary>
        /// Atualiza as informacoes de conteudo extra de midia do titulo
        /// </summary>
        /// <param name="tituloConteudoExtraMidia"></param>
        public void AtualizaConteudoExtraMidia(TituloConteudoExtraMidia tituloConteudoExtraMidia)
        {
            if (tituloConteudoExtraMidia.TituloConteudoExtraMidiaId > 0)
            {
                this.TituloConteudoExtraMidiaDal.Atualizar(tituloConteudoExtraMidia);
            }
            else
            {
                tituloConteudoExtraMidia.TituloConteudoExtraMidiaId = tituloConteudoExtraMidia.Titulo.TituloId;
                this.TituloConteudoExtraMidiaDal.Inserir(tituloConteudoExtraMidia);
            }
        }

        /// <summary>
        /// Atualiza as informacoes de conteudo extra url do titulo
        /// </summary>
        /// <param name="tituloConteudoExtraUrl"></param>
        public void AtualizaConteudoExtraUrl(TituloConteudoExtraUrl tituloConteudoExtraUrl)
        {
            if (tituloConteudoExtraUrl.TituloConteudoExtraUrlId > 0)
            {
                this.TituloConteudoExtraUrlDal.Atualizar(tituloConteudoExtraUrl);
            }
            else
            {
                tituloConteudoExtraUrl.TituloConteudoExtraUrlId = tituloConteudoExtraUrl.Titulo.TituloId;
                this.TituloConteudoExtraUrlDal.Inserir(tituloConteudoExtraUrl);
            }
        }

        /// <summary>
        /// /// Insere um conteudo extra arquivo de um titulo
        /// </summary>
        /// <param name="tituloConteudoExtraArquivos"></param>
        public void InserirConteudoExtraArquivo(TituloConteudoExtraArquivo entidade)
        {
            this.TituloConteudoExtraArquivoDal.Inserir(entidade);
        }

        /// <summary>
        /// Atualiza o conteudo extra arquivo de um titulo
        /// </summary>
        /// <param name="tituloConteudoExtraArquivos"></param>
        public void AtualizaConteudoExtraArquivo(List<TituloConteudoExtraArquivo> tituloConteudoExtraArquivos)
        {
            foreach (var item in tituloConteudoExtraArquivos)
            {
                this.TituloConteudoExtraArquivoDal.Atualizar(item);
            }

        }

        /// <summary>
        /// Exclui o conteudo extra arquivo de um titulo
        /// </summary>
        /// <param name="tituloConteudoExtraArquivoExcluir"></param>
        public void ExcluirConteudoExtraArquivo(List<TituloConteudoExtraArquivo> entidade)
        {
            foreach (var item in entidade)
            {
                this.TituloConteudoExtraArquivoDal.Excluir(item);
            }
        }

        /// <summary>
        /// Carrega o conteudo extra midia de um titulo
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloConteudoExtraMidia CarregaConteudoExtraMidia(TituloConteudoExtraMidia entidade)
        {
            entidade = this.TituloConteudoExtraMidiaDal.Carregar(entidade);
            return entidade;
        }

        /// <summary>
        /// Carrega do conteudo extra de arquivos do titulos
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public IEnumerable<TituloConteudoExtraArquivo> CarregarConteudoExtraArquivos(TituloConteudoExtraArquivoFH tituloConteudoExtraArquivoFH)
        {
            return this.TituloConteudoExtraArquivoDal.CarregarTodos(0, 0, null, null, tituloConteudoExtraArquivoFH);

        }

        /// <summary>
        /// Carrega o conteudo extra de url de um titulo 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public TituloConteudoExtraUrl CarregarConteudoExtraUrl(TituloConteudoExtraUrl entidade)
        {
            entidade = this.TituloConteudoExtraUrlDal.Carregar(entidade);
            return entidade;
        }

        /// <summary>
        /// Carrega as informacoes extra de um titulo (midia, arquivos e url)
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public Titulo CarregarConteudoExtra(Titulo titulo)
        {
            titulo = TituloDal.CarregaConteudoExtraMidiaURL(titulo);
            titulo.TituloConteudoExtraArquivos = (List<TituloConteudoExtraArquivo>)this.CarregarConteudoExtraArquivos(new TituloConteudoExtraArquivoFH() { TituloId = titulo.TituloId.ToString(), Ativo = "1" });
            return titulo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaDeConhecimentoId"></param>
        /// <param name="numeroMaximoRegistros"></param>
        /// <returns></returns>
        public List<TituloInformacaoComentarioEspecialista> CarregarTitulosPorAreaDeConhecimento(int areaDeConhecimentoId, int numeroMaximoRegistros)
        {

            return TituloDal.CarregarTitulosPorAreaDeConhecimento(areaDeConhecimentoId, numeroMaximoRegistros);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="areaDeConhecimentoId"></param>
        /// <param name="numeroMaximoDeRegistros"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregarTitulosRelacionadosPorArea(int produtoId, int areaDeConhecimentoId, int numeroMaximoDeRegistros)
        {
            return TituloDal.CarregarTitulosRelacionadosPorArea(produtoId, areaDeConhecimentoId, numeroMaximoDeRegistros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        private String FormataPalavraFiltro(String palavra)
        {
            if (!String.IsNullOrEmpty(palavra))
            {
                palavra = palavra.RemoveAccents();
                //palavra = Regex.Replace(palavra, @"[^a-zA-Z0-9\s-#?!&$@.]", "");
                palavra = Regex.Replace(palavra, @"[^a-zA-Z0-9\s#+?!&$@]", " "); // invalid chars
                palavra = Regex.Replace(palavra, @"\s+", " ").Trim(); // convert multiple spaces into one space

                String[] arrPalavra = BuscaHelper.RemoveStopWords(palavra);

                if (arrPalavra.Count() > 0)
                {
                    var res = arrPalavra.Aggregate((current, next) => current + " AND " + next);
                    palavra = res.ToString();
                    palavra = palavra.Replace("'", "");
                    palavra = palavra.Replace(" AND ", @"*"" AND """);
                    palavra = @"""" + palavra + @"*""";
                    return palavra;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return palavra;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        public AbasEstanteVH CarregaAbasEstante(Int32 categoriaId)
        {
            return new TituloADO().CarregaAbasEstante(categoriaId);
        }

        /// <summary>
        /// Método que retorna StringBuilder do auto complete da busca
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public StringBuilder CarregarAutoCompleteBusca(String palavra)
        {
            return new TituloADO().CarregarAutoCompleteBusca(palavra);
        }

        /// <summary>
        /// Carrega 7 títulos de sugestão a partir do histórico de compras
        /// </summary>
        /// <param name="usuarioBO"></param>
        /// <param name="categoriaBO"></param>
        /// <returns></returns>
        public List<EstanteTituloVH> CarregaTitulosHistoricoComprasParaEstante(Usuario usuarioBO, Categoria categoriaBO)
        {
            return TituloDal.CarregaTitulosHistoricoComprasParaEstante(usuarioBO, categoriaBO);
        }

        public void Inserir(Titulo entidade)
        {
            this.TituloDal.Inserir(entidade);
        }

        public void Atualizar(Titulo entidade)
        {
            TituloDal.Atualizar(entidade);
        }

        public void AtualizarNomeSubTitulo(Titulo titulo, Produto produto)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TituloDal.AtualizarNomeSubTitulo(titulo);
                new ProdutoADO().AtualizarNome(produto);

                scope.Complete();
            }

        }

        /// <summary>
        /// Verifica se o titulo e o autor estão relacionados
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <param name="autorBO"></param>
        /// <returns></returns>
        public bool TituloAutorRelacionado(Titulo tituloBO, Autor autorBO)
        {
            return TituloAutorDal.TituloAutorRelacionado(tituloBO, autorBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn13SB"></param>
        public void AtualizarTitulosMaisVendidos(List<Titulo> titulosMaisVendidos)
        {
            using (TransactionScope escopoDaTransacao = new TransactionScope())
            {
                TituloDal.DesmarcarMaisVendidos();

                TituloDal.AtualizarMaisVendidos(titulosMaisVendidos);

                escopoDaTransacao.Complete();
            }
        }

        public void AtualizarMenosNomeSubtitulo(Titulo entidade)
        {
            TituloDal.AtualizarMenosNomeSubtitulo(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public Int32 ContarTituloPorAutor(Autor autor)
        {
            return TituloDal.ContarTituloPorAutor(autor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public List<Titulo> CarregarTitulosPorAutor(Autor autor)
        {
            return TituloDal.CarregarTitulosPorAutor(autor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloBO"></param>
        /// <param name="arquivoBO"></param>
        /// <returns></returns>
        public bool TituloArquivoRelacionado(Titulo tituloBO, Arquivo arquivoBO)
        {
            return TituloAutorDal.TituloArquivoRelacionado(tituloBO, arquivoBO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        public void ExcluirComentarioEspecialista(TituloInformacaoComentarioEspecialista tituloInformacaoComentarioEspecialista)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TituloInformacaoComentarioEspecialistaCategoriaDAL.ExcluirTodosPorComentarioEspecialista(tituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId);

                TituloInformacaoComentarioEspecialistaDAL.Excluir(tituloInformacaoComentarioEspecialista);

                if (tituloInformacaoComentarioEspecialista.ArquivoImagem != null && tituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId > 0)
                {
                    ArquivoDAL.Excluir(tituloInformacaoComentarioEspecialista.ArquivoImagem);
                }

                scope.Complete();
            }
        }

        #region [ Busca ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="palavra"></param>
        /// <param name="categoriaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="seloId"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarBuscaTituloOrdenadaPaginada(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, String palavra, Int32 categoriaId, String tipoId, Int32 seloId)
        {
            string palavraExata = palavra;

            palavra = FormataPalavraFiltro(palavra);

            return TituloDal.CarregarBuscaTituloOrdenadaPaginada(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, palavra, palavraExata, categoriaId, tipoId, seloId);
        }

        /// <summary>
        /// Método que retorna o menu da tela de busca e o total de itens para Titulo
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="categoriaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="seloId"></param>
        /// <returns></returns>
        public MenuBuscaVH CarregarMenuBusca(String palavra, Int32 categoriaId, String tipoId, Int32 seloId, out int totalItem)
        {
            totalItem = 0;

            string palavraExata = palavra;

            palavra = FormataPalavraFiltro(palavra);

            MenuBuscaVH menuBuscaVH = TituloDal.CarregarMenuBusca(palavra, palavraExata, categoriaId, tipoId, seloId, out totalItem);

            return menuBuscaVH;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="revista"></param>
        /// <returns></returns>
        public List<TituloVH> CarregarTitulosPorPromocaoRevista(Int32 registrosPagina, Int32 numeroPagina, String ordem, Revista revista)
        {
            if (PromocaoDal.ContarPromocaoRevistaSemProdutoPorRevista(revista) > 0)
            {
                return TituloDal.CarregarTodosTitulosPorPromocaoRevista(registrosPagina, numeroPagina, ordem);
            }
            else
            {
                return TituloDal.CarregarTitulosPorPromocaoRevista(registrosPagina, numeroPagina, ordem, revista);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <returns></returns>
        public Int32 ContarTitulosPorPromocaoRevista(Revista revista)
        {
            if (PromocaoDal.ContarPromocaoRevistaSemProdutoPorRevista(revista) > 0)
            {
                return TituloDal.ContarTodosTitulosPorPromocaoRevista();
            }
            else
            {
                return TituloDal.ContarTitulosPorPromocaoRevista(revista);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloId"></param>
        /// <returns></returns>
        public Int32 CarregarCategoriaPorTituloId(Int32 tituloId)
        {
            return TituloDal.CarregarCategoriaPorTituloId(tituloId);
        }
    }
}