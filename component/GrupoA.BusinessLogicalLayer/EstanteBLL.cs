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

namespace GrupoA.BusinessLogicalLayer
{
    public class EstanteBLL : BaseBLL
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
        private ITituloInformacaoMaterialDidaticoDAL _tituloInformacaoMaterialDidaticoDAL;
        private ITituloConteudoExtraArquivoDAL _tituloConteudoExtraArquivoDal;
        private ITituloConteudoExtraMidiaDAL _tituloConteudoExtraMidiaDal;
        private ITituloConteudoExtraUrlDAL _tituloConteudoExtraUrlDal;
        private IArquivoDAL _arquivoDAL;
        private IAutorDAL _autorDal;
   

        private IAutorDAL AutorDal
        {
            get { return _autorDal ?? (_autorDal = new AutorADO()); }
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

        public List<Titulo> CarregarTitulosLancamentosHome()
        {
            return new List<Titulo>();
        }

        public List<Titulo> CarregarTitulosMaisVendidosHome()
        {
            return new List<Titulo>();
        }

        public List<Titulo> CarregarTitulosOfertasHome()
        {
            return new List<Titulo>();
        }

        public List<Titulo> CarregarTitulosCompraConjuntaHome()
        {
            return new List<Titulo>();
        }

        public List<Titulo> CarregarTitulosEbooksHome()
        {
            return new List<Titulo>();
        }

        #region Métodos: Titulo

        /// <summary>
        /// Método que carrega um título por ISBN13
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public TituloImpresso CarregarTituloImpressoPorIsbn13(String Isbn13)
        {
            TituloImpresso entidade = TituloImpressoDal.CarregarPorIsbn13(Isbn13);
            if (entidade != null)
            {
                entidade.Titulo = TituloDal.CarregaTituloComComentarioDoEspecialista(entidade.Titulo);
            }
            return entidade;
        }

        public IEnumerable<TituloEletronico> CarregarTodosPorIsbn13(TituloEletronico entidade)
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
            titulo.TituloAutores = (List<TituloAutor>) TituloAutorDal.Carregar(titulo);
            return titulo;
        }

        /// <summary>
        /// Método que carrega um Título Impressocom a dependências de Produto e Conteudo já configuradas.
        /// </summary>
        /// <param name="titulo">Título com identificador a ser carregado.</param>
        /// <returns>Título com os dados configurados.</returns>
        public TituloImpresso CarregarTituloImpressoComDependencias(TituloImpresso tituloImpresso)
        {
            tituloImpresso = TituloImpressoDal.CarregarComDependencias(tituloImpresso);
            //
            //tituloImpresso.Titulo.TituloAutores = (List<TituloAutor>)TituloAutorDal.CarregarAutor(tituloImpresso);
            return tituloImpresso;
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
        /// <param name="titulo"></param>
        /// <returns></returns>
        public TituloImpresso CarregarTituloImpresso(TituloImpresso tituloImpresso)
        {
            return TituloImpressoDal.Carregar(tituloImpresso);
        }

        #endregion

        #region Métodos: Comentario Especialista
        
        /// <summary>
        ///Carrega informações complementares para o título
        /// </summary>        
        public TituloImpresso CarregarComentarioEspecialista(TituloImpresso entidade)
        {
            //Carrega titulo, titulo.produto e titulo.conteudo
            entidade = TituloImpressoDal.Carregar(entidade);
            entidade.Titulo = TituloDal.CarregaTituloComComentarioDoEspecialista(entidade.Titulo);
            return entidade;
        }
        public void AtualizarTituloComentarioEspecialista(Titulo titulo)
        {
            using (TransactionScope escopoDaTransacao = new TransactionScope())
            {
                //SOBRE COMENTARIO ESPECIALISTA
                if (titulo.TituloInformacaoComentarioEspecialista != null)
                {
                    //if (titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio != null)
                    //{
                    //    if (titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio.ArquivoId > 0)
                    //    {
                    //        ArquivoDAL.InserirNovoAutor(titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio);
                    //    }
                    //    else
                    //    {
                    //        ArquivoDAL.AtualizarAutor(titulo.TituloInformacaoComentarioEspecialista.ArquivoAudio);
                    //    }
                    //}
                    //if (titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem != null)
                    //{
                    //    if (titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem.ArquivoId > 0)
                    //    {
                    //        ArquivoDAL.InserirNovoAutor(titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem);
                    //    }
                    //    else
                    //    {
                    //        ArquivoDAL.AtualizarAutor(titulo.TituloInformacaoComentarioEspecialista.ArquivoImagem);
                    //    }
                    //}
                    if (titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId > 0)
                    {
                        TituloInformacaoComentarioEspecialistaDAL.Atualizar(titulo.TituloInformacaoComentarioEspecialista);
                    }
                    else
                    {
                        titulo.TituloInformacaoComentarioEspecialista.TituloInformacaoComentarioEspecialistaId = titulo.TituloId;
                        TituloInformacaoComentarioEspecialistaDAL.Inserir(titulo.TituloInformacaoComentarioEspecialista);
                    }
                }

                TituloDal.Atualizar(titulo);
                escopoDaTransacao.Complete();
            }
        }
        #endregion

        #region Métodos: Informacoes Complementares
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

        public void AtualizarTituloInformacoesComplementares(Titulo titulo)
        {
            using (TransactionScope escopoDaTransacao = new TransactionScope())
            {
                //SOBRE AUTOR
                if (titulo.TituloInformacaoSobreAutor != null)
                {
                    if (titulo.TituloInformacaoSobreAutor.ArquivoImagem != null)
                    {
                        if (titulo.TituloInformacaoSobreAutor.ArquivoImagem.ArquivoId > 0)
                        {
                            ArquivoDAL.Inserir(titulo.TituloInformacaoSobreAutor.ArquivoImagem);
                        }
                        else
                        {
                            ArquivoDAL.Atualizar(titulo.TituloInformacaoSobreAutor.ArquivoImagem);
                        }
                    }
                    if(titulo.TituloInformacaoSobreAutor.TituloInformacaoSobreAutorId > 0)
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
                    if (titulo.TituloInformacaoSumario.ArquivoSumario != null)
                    {
                        if (titulo.TituloInformacaoSumario.ArquivoSumario.ArquivoId > 0)
                        {
                            ArquivoDAL.Inserir(titulo.TituloInformacaoSumario.ArquivoSumario);
                        }
                        else
                        {
                            ArquivoDAL.Atualizar(titulo.TituloInformacaoSumario.ArquivoSumario);
                        }
                    }
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


        #endregion

        #region Métodos: Titulo Autor

        /// <summary>
        /// Método que retorna os autores de um título.
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public IEnumerable<Autor> CarregarAutores(Titulo titulo)
        {
            return AutorDal.CarregarAutores(titulo);
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
        /// Método que remove o relacionamento entre autor e título.
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirTituloAutor(TituloAutor entidade) 
        {
            TituloAutorDal.ExcluirTituloAutor(entidade);
        }

        #endregion
        
        #region Métodos: Conteudo Extra

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

        //public void AtualizaConteudoExtraArquivos(List<TituloConteudoExtraArquivo> tituloConteudoExtraArquivos)
        //{ 

        //}

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
        public IEnumerable<TituloConteudoExtraArquivo> CarregarConteudoExtraArquivos(Titulo titulo)
        {
            return this.TituloConteudoExtraArquivoDal.CarregarTodos(0, 0, null, null, new TituloFH() { TituloId = titulo.TituloId.ToString() });

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
            titulo.TituloConteudoExtraArquivos = (List<TituloConteudoExtraArquivo>)this.CarregarConteudoExtraArquivos(new Titulo() { TituloId = titulo.TituloId });
            return titulo;
        }


        #endregion
    }
}
