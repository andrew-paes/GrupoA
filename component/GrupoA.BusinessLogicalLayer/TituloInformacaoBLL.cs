using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.BusinessObject.ViewHelper;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.BusinessLogicalLayer
{
    public class TituloInformacaoBLL : BaseBLL
    {
        private ITituloInformacaoSumarioDAL _tituloInformacaoSumarioDAL;
        private ITituloInformacaoFichaDAL _tituloInformacaoFichaDAL;
        private ITituloInformacaoSobreAutorDAL _tituloInformacaoSobreAutorDAL;
        private ITituloInformacaoComentarioEspecialistaDAL _tituloInformacaoComentarioEspecialistaDAL;
        private ITituloInformacaoMaterialDidaticoDAL _tituloInformacaoMaterialDidaticoDAL;
        private IArquivoDAL _arquivoDAL;
        private IProdutoImagemDAL _produtoImagemDAL;
        private ITituloImpressoDAL _tituloImpressoDAL;
        private ITituloInformacaoResumoDAL _tituloInformacaoResumoDAL;

        private ITituloImpressoDAL TituloImpressoDAL
        {
            get
            {
                if (_tituloImpressoDAL == null)
                    _tituloImpressoDAL = new TituloImpressoADO();
                return _tituloImpressoDAL;
            }
        }

        private IProdutoImagemDAL ProdutoImagemDAL
        {
            get
            {
                if (_produtoImagemDAL == null)
                    _produtoImagemDAL = new ProdutoImagemADO();
                return _produtoImagemDAL;
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

        private ITituloInformacaoSobreAutorDAL TituloInformacaoSobreAutorDAL
        {
            get
            {
                if (_tituloInformacaoSobreAutorDAL == null)
                    _tituloInformacaoSobreAutorDAL = new TituloInformacaoSobreAutorADO();
                return _tituloInformacaoSobreAutorDAL;
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

        private ITituloInformacaoResumoDAL TituloInformacaoResumoDAL
        {
            get
            {
                if (_tituloInformacaoResumoDAL == null)
                    _tituloInformacaoResumoDAL = new TituloInformacaoResumoADO();
                return _tituloInformacaoResumoDAL;
            }
        }

        public IEnumerable<ComentarioEspecialistaDestaqueVH> CarregarAvaliacoesEspecialistasDestaque(Usuario usuario, int quantidadeRegistros)
        {
            return TituloInformacaoComentarioEspecialistaDAL.CarregarAvaliacoesEspecialistasDestaque(usuario, quantidadeRegistros);
        }

        public IEnumerable<TituloInformacaoComentarioEspecialista> CarregarAvaliacoesEspecialistas(Usuario usuario, int quantidadeRegistros)
        {
            return TituloInformacaoComentarioEspecialistaDAL.CarregarAvaliacoesEspecialistas(usuario, quantidadeRegistros);
        }

        public TituloInformacaoSumario CarregarComDependencias(TituloInformacaoSumario entidade)
        {
            TituloInformacaoSumario tituloInformacaoSumario = new TituloInformacaoSumario();
            tituloInformacaoSumario = TituloInformacaoSumarioDAL.CarregarComDependencias(entidade);
            if (tituloInformacaoSumario != null
                && tituloInformacaoSumario.ArquivoSumario != null)
            {
                Arquivo arquivo = new Arquivo();
                arquivo.ArquivoId = tituloInformacaoSumario.ArquivoSumario.ArquivoId;
                arquivo = ArquivoDAL.Carregar(arquivo);
                tituloInformacaoSumario.ArquivoSumario = arquivo;
            }
            return tituloInformacaoSumario;
        }

        public TituloInformacaoFicha CarregarComDependencias(TituloInformacaoFicha entidade)
        {
            return TituloInformacaoFichaDAL.CarregarComDependencias(entidade);
        }

        public TituloInformacaoSobreAutor CarregarComDependencias(TituloInformacaoSobreAutor entidade)
        {
            TituloInformacaoSobreAutor tituloInformacoSobreAutor = new TituloInformacaoSobreAutor();
            tituloInformacoSobreAutor = TituloInformacaoSobreAutorDAL.CarregarComDependencias(entidade);

            if (tituloInformacoSobreAutor != null
                && tituloInformacoSobreAutor.ArquivoImagem != null)
            {
                Arquivo arquivo = new Arquivo();
                arquivo.ArquivoId = tituloInformacoSobreAutor.ArquivoImagem.ArquivoId;
                arquivo = ArquivoDAL.Carregar(arquivo);
            }

            return tituloInformacoSobreAutor;
        }

        public TituloInformacaoComentarioEspecialista CarregarComDependencias(TituloInformacaoComentarioEspecialista entidade)
        {
            return TituloInformacaoComentarioEspecialistaDAL.CarregarComDependencias(entidade);
        }

        public TituloInformacaoMaterialDidatico CarregarComDependencias(TituloInformacaoMaterialDidatico entidade)
        {
            return TituloInformacaoMaterialDidaticoDAL.CarregarComDependencias(entidade);
        }

        public TituloInformacaoResumo CarregarComDependencias(TituloInformacaoResumo entidade)
        {
            return TituloInformacaoResumoDAL.CarregarComDependencias(entidade);
        }

        public TituloInformacaoComentarioEspecialista CarregarComentarioEspecialistaPorCategoria(Categoria categoria, Usuario usuario)
        {
            return TituloInformacaoComentarioEspecialistaDAL.CarregarComentarioEspecialistaPorCategoria(categoria, usuario);
        }

        public TituloInformacaoComentarioEspecialista CarregarComentarioEspecialistaPorCategoriaParaRevista(Categoria categoria)
        {
            return TituloInformacaoComentarioEspecialistaDAL.CarregarComentarioEspecialistaPorCategoriaParaRevista(categoria);
        }

        public void Atualizar(TituloInformacaoResumo entidade)
        {
            TituloInformacaoResumoDAL.Atualizar(entidade);
        }

        public void Atualizar(TituloInformacaoSobreAutor entidade)
        {
            TituloInformacaoSobreAutorDAL.Atualizar(entidade);
        }

        public void Atualizar(TituloInformacaoSumario entidade)
        {
            TituloInformacaoSumarioDAL.Atualizar(entidade);
        }

        public void Atualizar(TituloInformacaoMaterialDidatico entidade)
        {
            TituloInformacaoMaterialDidaticoDAL.Atualizar(entidade);
        }

        public void Inserir(TituloInformacaoResumo entidade)
        {
            TituloInformacaoResumoDAL.Inserir(entidade);
        }

        public void Inserir(TituloInformacaoSobreAutor entidade)
        {
            TituloInformacaoSobreAutorDAL.Inserir(entidade);
        }

        public void Inserir(TituloInformacaoSumario entidade)
        {
            TituloInformacaoSumarioDAL.Inserir(entidade);
        }

        public void Inserir(TituloInformacaoMaterialDidatico entidade)
        {
            TituloInformacaoMaterialDidaticoDAL.Inserir(entidade);
        }
    }
}