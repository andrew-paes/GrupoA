using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System.Collections;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;

namespace GrupoA.BusinessLogicalLayer
{
    public class NoticiaBLL : BaseBLL
    {
        #region Declarações DAL

        private INoticiaDAL _noticiaDal;
        private ICategoriaNoticiaDAL _categoriaNoticiaDal;
        private INoticiaImagemDAL _noticiaImagemDal;
        private IConteudoDAL _conteudoDal;
        private IArquivoDAL _arquivoDal;

        private INoticiaDAL NoticiaDal
        {
            get { return _noticiaDal ?? (_noticiaDal = new NoticiaADO()); }
        }

        private ICategoriaNoticiaDAL CategoriaNoticiaDal
        {
            get
            {
                if (_categoriaNoticiaDal == null)
                    _categoriaNoticiaDal = new CategoriaNoticiaADO();
                return _categoriaNoticiaDal;
            }
        }

        private IConteudoDAL ConteudoDal
        {
            get
            {
                if (_conteudoDal == null)
                    _conteudoDal = new ConteudoADO();
                return _conteudoDal;
            }
        }

        private IArquivoDAL ArquivoDal
        {
            get
            {
                if (_arquivoDal == null)
                    _arquivoDal = new ArquivoADO();
                return _arquivoDal;
            }
        }

        private INoticiaImagemDAL NoticiaImagemDal
        {
            get
            {
                if (_noticiaImagemDal == null)
                    _noticiaImagemDal = new NoticiaImagemADO();
                return _noticiaImagemDal;
            }
        }

        private IConteudoImprensaDAL _conteudoImprensaDal;

        private IConteudoImprensaDAL ConteudoImprensaDal
        {
            get
            {
                if (_conteudoImprensaDal == null)
                    _conteudoImprensaDal = new ConteudoImprensaADO();
                return _conteudoImprensaDal;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Noticia Carregar(Noticia entidade)
        {
            entidade = NoticiaDal.CarregarComDependencias(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Noticia CarregarCompleto(Noticia entidade)
        {
            entidade = NoticiaDal.CarregarComDependencias(entidade);
            if (entidade.ArquivoThumb != null && entidade.ArquivoThumb.ArquivoId > 0)
            {
                entidade.ArquivoThumb = CarregarArquivo(entidade.ArquivoThumb);
            }
            entidade.CategoriaNoticia = CarregarCategoriaNoticia(entidade.CategoriaNoticia);
            entidade.NoticiaImagens = (List<NoticiaImagem>)this.CarregarTodosNoticiaImagem(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noticia"></param>
        /// <param name="categorias"></param>
        /// <returns></returns>
        public Noticia Inserir(Noticia noticia, List<Categoria> categorias)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Inserção de Conteúdo
                Conteudo conteudo = new Conteudo();
                conteudo.DataHoraCadastro = DateTime.Now;
                conteudo.ConteudoTipo = new ConteudoTipo((int)TipoDeConteudo.Noticia);
                ConteudoDal.Inserir(conteudo);
                // Inserção de Conteúdo Imprensa
                ConteudoImprensa conteudoImprensa = noticia.ConteudoImprensa;
                conteudoImprensa.ConteudoImprensaId = conteudo.ConteudoId;
                //conteudoImprensa.Conteudo = conteudo;
                ConteudoImprensaDal.Inserir(conteudoImprensa);
                // Inserção de Categorias
                foreach (Categoria categoria in categorias)
                    if (ConteudoDal != null) ConteudoDal.InserirRelacionamentoAreaConhecimento(conteudo, categoria);
                // Inserção de Notícia com mesmo código identificador (Id)
                noticia.NoticiaId = conteudo.ConteudoId;
                noticia.ConteudoImprensa = conteudoImprensa;
                NoticiaDal.Inserir(noticia);
                scope.Complete();
            }
            return noticia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noticia"></param>
        /// <param name="categorias"></param>
        public void Atualizar(Noticia noticia, List<Categoria> categorias)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Atualização dos Ids
                noticia.ConteudoImprensa.ConteudoImprensaId = noticia.NoticiaId;
                noticia.ConteudoImprensa.Conteudo = new Conteudo() { ConteudoId = noticia.NoticiaId };

                // Atualização de Conteúdo Imprensa
                ConteudoImprensa conteudoImprensa = noticia.ConteudoImprensa;
                conteudoImprensa.ConteudoImprensaId = noticia.NoticiaId;
                ConteudoImprensaDal.Atualizar(conteudoImprensa);

                // Atualização de Categorias
                // a. Exclui todos os relacionamentos com áreas de conhecimento
                ConteudoDal.ExcluirTodasAreasConhecimento(noticia.ConteudoImprensa.Conteudo);
                // b. Inclui os novos relacionamentos
                foreach (Categoria categoria in categorias)
                    ConteudoDal.InserirRelacionamentoAreaConhecimento(noticia.ConteudoImprensa.Conteudo, categoria);

                // Atualização de Evento com mesmo código identificador (Id)
                NoticiaDal.Atualizar(noticia);

                scope.Complete();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IEnumerable<Noticia> CarregarNoticiasValidasComDependencias(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return (List<Noticia>)NoticiaDal.CarregarTodosValidosComDependencias(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Arquivo CarregarArquivo(Arquivo entidade)
        {
            return ArquivoDal.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoriaNoticia> CarregarTodosCategoriaNoticia()
        {
            return CategoriaNoticiaDal.CarregarTodos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IEnumerable<CategoriaNoticia> CarregarTodosCategoriaNoticia(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return CategoriaNoticiaDal.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public CategoriaNoticia CarregarCategoriaNoticia(CategoriaNoticia entidade)
        {
            return CategoriaNoticiaDal.Carregar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<NoticiaImagem> CarregarTodosNoticiaImagem(Noticia entidade)
        {
            var noticiaImagemFH = new NoticiaImagemFH() { NoticiaId = entidade.NoticiaId.ToString() };
            IEnumerable<NoticiaImagem> imagens = NoticiaImagemDal.CarregarTodosArquivos(0, 0, null, null, noticiaImagemFH);

            return imagens;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public NoticiaImagem CarregarNoticiaImagem(NoticiaImagem entidade)
        {
            return NoticiaImagemDal.CarregarNoticiaImagem(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public NoticiaImagem InserirNoticiaImagem(NoticiaImagem entidade)
        {
            if (entidade.Arquivo.ArquivoId == 0)
            {
                var arquivo = new Arquivo
                                  {
                                      NomeArquivo = entidade.Arquivo.NomeArquivo,
                                      NomeArquivoOriginal = entidade.Arquivo.NomeArquivoOriginal,
                                      TamanhoArquivo = entidade.Arquivo.TamanhoArquivo,
                                      DataHoraUpload = entidade.Arquivo.DataHoraUpload
                                  };
                ArquivoDal.Inserir(arquivo);
                entidade.Arquivo.ArquivoId = arquivo.ArquivoId;
                NoticiaImagemDal.Inserir(entidade);
            }
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public NoticiaImagem ExcluirNoticiaImagem(NoticiaImagem entidade)
        {
            NoticiaImagemDal.ExcluirImagem(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarNoticiaImagem(NoticiaImagem entidade)
        {
            NoticiaImagemDal.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void InserirCategoriaDeNoticia(CategoriaNoticia entidade)
        {
            CategoriaNoticiaDal.Inserir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarNoticiaCategoria(CategoriaNoticia entidade)
        {
            CategoriaNoticiaDal.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ContarTodasNoticiasValidosComDependencias()
        {
            return NoticiaDal.ContarTodosValidosComDependencias();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(Noticia entidade)
        {
            TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
            {
                NoticiaDal.Excluir(entidade);

                tScope.Complete();
            }
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
                String[] arrPalavra = BuscaHelper.RemoveStopWords(palavra);

                if (arrPalavra.Count() > 0)
                {
                    var res = arrPalavra.Aggregate((current, next) => current + " AND " + next);
                    palavra = res.ToString();
                    palavra = palavra.Replace("'", "");
                    palavra = palavra.Replace(" AND ", "*\" AND \"");
                    palavra = "\"" + palavra + "*\"";
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
        /// Método que retorna quantidade total de noticias para busca por palavra
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public int ContarNoticiaBusca(String palavra)
        {
            palavra = FormataPalavraFiltro(palavra);

            return NoticiaDal.ContarNoticiaBusca(palavra);
        }

        /// <summary>
        /// Método para fazer a busca de release através do filtro
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public List<Noticia> CarregarNoticiaBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra)
        {
            palavra = FormataPalavraFiltro(palavra);

            return NoticiaDal.CarregarNoticiaBusca(registrosPagina, numeroPagina, ordenacao, ordenacaoSentido, palavra);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Noticia> CarregarNoticiasPorCategoria(Categoria categoria)
        {
            return this.CarregarNoticiasPorCategoria(categoria,3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="qtdRegistros"></param>
        /// <returns></returns>
        public List<Noticia> CarregarNoticiasPorCategoria(Categoria categoria, Int32 qtdRegistros)
        {
            return NoticiaDal.CarregarNoticiasPorCategoria(categoria, qtdRegistros);
        }
    }
}