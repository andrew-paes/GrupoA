using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System;
using System.Xml;

namespace GrupoA.BusinessLogicalLayer
{
    public class CursoPanamericanoBLL : BaseBLL
    {
        #region Propriedades

        private ICursoPanamericanoDAL _noticiaDal;
        private ICategoriaDAL _categoriaDAL;
        private IArquivoDAL _arquivoDal;

        private ICursoPanamericanoDAL CursoPanamericanoDAL
        {
            get { return _noticiaDal ?? (_noticiaDal = new CursoPanamericanoADO()); }
        }

        private ICategoriaDAL CategoriaDAL
        {
            get { return _categoriaDAL ?? (_categoriaDAL = new CategoriaADO()); }
        }

        private IArquivoDAL ArquivoDal
        {
            get { return _arquivoDal ?? (_arquivoDal = new ArquivoADO()); }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public CursoPanamericano Carregar(CursoPanamericano entidade)
        {
            entidade = CursoPanamericanoDAL.Carregar(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public CursoPanamericano Inserir(CursoPanamericano entidade)
        {
            if (entidade.ArquivoImagem != null)
            {
                var arquivo = new Arquivo();
                arquivo.NomeArquivo = entidade.ArquivoImagem.NomeArquivo;
                arquivo.NomeArquivoOriginal = entidade.ArquivoImagem.NomeArquivoOriginal;
                arquivo.TamanhoArquivo = entidade.ArquivoImagem.TamanhoArquivo;
                arquivo.DataHoraUpload = entidade.ArquivoImagem.DataHoraUpload;
                ArquivoDal.Inserir(arquivo);
                entidade.ArquivoImagem.ArquivoId = arquivo.ArquivoId;

            }
            CursoPanamericanoDAL.Inserir(entidade);
            return entidade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoPanamericanoId"></param>
        /// <returns></returns>
        public List<Categoria> CarregarCategoriasDoCursoPanamericano(int cursoPanamericanoId)
        {
            return CursoPanamericanoDAL.CarregarCategoriasDoCursoPanamericano(cursoPanamericanoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Atualizar(CursoPanamericano entidade)
        {

            if (entidade.ArquivoImagem != null)
            {
                if (entidade.ArquivoImagem.ArquivoId == 0)
                {
                    var arquivo = new Arquivo();
                    arquivo.NomeArquivo = entidade.ArquivoImagem.NomeArquivo;
                    arquivo.NomeArquivoOriginal = entidade.ArquivoImagem.NomeArquivoOriginal;
                    arquivo.TamanhoArquivo = entidade.ArquivoImagem.TamanhoArquivo;
                    arquivo.DataHoraUpload = entidade.ArquivoImagem.DataHoraUpload;
                    ArquivoDal.Inserir(arquivo);
                    entidade.ArquivoImagem.ArquivoId = arquivo.ArquivoId;
                }
            }

            if (entidade.Categorias != null)
            {
                CursoPanamericanoDAL.ExcluiCursoPanamericanoCategoria(entidade.CursoPanamericanoId);

                for (int i = 0; i < entidade.Categorias.Count; i++)
                {
                    CursoPanamericanoDAL.InserirLocalizacaoCursoPanamericano(entidade.CursoPanamericanoId, entidade.Categorias[i].CategoriaId);
                }
            }


            CursoPanamericanoDAL.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Categoria> CarregarTodasCategoriasFilhas()
        {
            return CategoriaDAL.CarregarCategoriasDoSegundoNivel();
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
        /// <param name="entidade"></param>
        /// <returns></returns>
        public IEnumerable<CursoPanamericano> CarregarCursosPanamericano(Usuario entidade)
        {
            IEnumerable<CursoPanamericano> cursoPanamericano = CursoPanamericanoDAL.CarregarCursosPanamericano(entidade);
            return cursoPanamericano;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(CursoPanamericano entidade)
        {
            CursoPanamericanoDAL.Excluir(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoPanamericanoId"></param>
        public void ExcluiCursoPanamericanoCategoria(int cursoPanamericanoId)
        {
            CursoPanamericanoDAL.ExcluiCursoPanamericanoCategoria(cursoPanamericanoId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public CursoPanamericano CarregarCursoPanamericanoPorInteresseUsuario(Int32? usuarioId)
        {
            return CursoPanamericanoDAL.CarregarCursoPanamericanoPorInteresseUsuario(usuarioId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public List<CursoPanamericano> CarregarCursosPanamericanoParaRevistas(Int32? usuarioId)
        {
            return CursoPanamericanoDAL.CarregarCursosPanamericanoParaRevistas(usuarioId);
        }

        #endregion
    }
}
