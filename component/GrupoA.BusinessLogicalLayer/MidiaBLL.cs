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
    public class MidiaBLL : BaseBLL
    {
        #region Declarações DAL

        private IMidiaDAL _midiaDAL;
        private IMidiaDAL MidiaDAL
        {
            get
            {
                if (_midiaDAL == null)
                    _midiaDAL = new MidiaADO();
                return _midiaDAL;
            }
        }

        private IMidiaTipoDAL _midiaTipoDAL;
        private IMidiaTipoDAL MidiaTipoDAL
        {
            get
            {
                if (_midiaTipoDAL == null)
                    _midiaTipoDAL = new MidiaTipoADO();
                return _midiaTipoDAL;
            }
        }

        private IMidiaCategoriaDAL _midiaCategoriaDAL;
        private IMidiaCategoriaDAL MidiaCategoriaDAL
        {
            get
            {
                if (_midiaCategoriaDAL == null)
                    _midiaCategoriaDAL = new MidiaCategoriaADO();
                return _midiaCategoriaDAL;
            }
        }

        private IMidiaRevistaDAL _midiaRevistaDAL;
        private IMidiaRevistaDAL MidiaRevistaDAL
        {
            get
            {
                if (_midiaRevistaDAL == null)
                    _midiaRevistaDAL = new MidiaRevistaADO();
                return _midiaRevistaDAL;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Midia> CarregarMidiasPorCategoria(Categoria categoria)
        {
            return this.CarregarMidiasPorCategoria(categoria, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revista"></param>
        /// <returns></returns>
        public List<Midia> CarregarMidiasPorRevista(Revista revista)
        {
            return this.CarregarMidiasPorCategoria(null, revista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<Midia> CarregarMidiasPorCategoria(Categoria categoria, Revista revista)
        {
            return MidiaDAL.CarregarMidiasPorCategoria(categoria, revista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <returns></returns>
        public Midia CarregarComDependencia(Midia entidade)
        {
            return MidiaDAL.CarregarComDependencias(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <param name="categorias"></param>
        /// <param name="revistas"></param>
        /// <returns></returns>
        public Midia Inserir(Midia midia, List<Categoria> categorias, List<Revista> revistas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Conteudo conteudo = new Conteudo();
                conteudo.ConteudoTipo = new ConteudoTipo();
                conteudo.ConteudoTipo.ConteudoTipoId = 17;
                conteudo.DataHoraCadastro = DateTime.Now;

                new ConteudoBLL().InserirConteudo(conteudo);

                midia.MidiaId = conteudo.ConteudoId;
                MidiaDAL.Inserir(midia);

                foreach (Categoria categoria in categorias)
                {
                    MidiaCategoria midiaCategoria = new MidiaCategoria();
                    midiaCategoria.Midia = new Midia();
                    midiaCategoria.Midia.MidiaId = midia.MidiaId;
                    midiaCategoria.Categoria = new Categoria();
                    midiaCategoria.Categoria.CategoriaId = categoria.CategoriaId;

                    MidiaCategoriaDAL.Inserir(midiaCategoria);
                }

                if (revistas != null && revistas.Count() > 0)
                {
                    foreach (Revista revista in revistas)
                    {
                        MidiaRevista midiaRevista = new MidiaRevista();
                        midiaRevista.Midia = new Midia();
                        midiaRevista.Midia.MidiaId = midia.MidiaId;
                        midiaRevista.Revista = new Revista();
                        midiaRevista.Revista.RevistaId = revista.RevistaId;

                        MidiaRevistaDAL.Inserir(midiaRevista);
                    }
                }

                scope.Complete();
            }
            return midia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <returns></returns>
        public void Excluir(Midia midia)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Conteudo conteudo = new Conteudo();
                conteudo.ConteudoId = midia.MidiaId;

                MidiaCategoriaDAL.ExcluirTodosPorMidia(midia);

                MidiaRevistaDAL.ExcluirTodosPorMidia(midia);

                MidiaDAL.Excluir(midia);

                new ConteudoBLL().ExcluirConteudo(conteudo);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <param name="categorias"></param>
        /// <param name="revistas"></param>
        /// <returns></returns>
        public Midia Atualizar(Midia midia, List<Categoria> categorias, List<Revista> revistas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MidiaDAL.Atualizar(midia);

                MidiaCategoriaDAL.ExcluirTodosPorMidia(midia);

                MidiaRevistaDAL.ExcluirTodosPorMidia(midia);

                foreach (Categoria categoria in categorias)
                {
                    MidiaCategoria midiaCategoria = new MidiaCategoria();
                    midiaCategoria.Midia = new Midia();
                    midiaCategoria.Midia.MidiaId = midia.MidiaId;
                    midiaCategoria.Categoria = new Categoria();
                    midiaCategoria.Categoria.CategoriaId = categoria.CategoriaId;

                    MidiaCategoriaDAL.Inserir(midiaCategoria);
                }

                if (revistas != null && revistas.Count() > 0)
                {
                    foreach (Revista revista in revistas)
                    {
                        MidiaRevista midiaRevista = new MidiaRevista();
                        midiaRevista.Midia = new Midia();
                        midiaRevista.Midia.MidiaId = midia.MidiaId;
                        midiaRevista.Revista = new Revista();
                        midiaRevista.Revista.RevistaId = revista.RevistaId;

                        MidiaRevistaDAL.Inserir(midiaRevista);
                    }
                }

                scope.Complete();
            }
            return midia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <returns></returns>
        public Midia Atualizar(Midia midia)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MidiaDAL.Atualizar(midia);
                scope.Complete();
            }
            return midia;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MidiaTipo> CarregarTodosTiposMidia()
        {
            return MidiaTipoDAL.CarregarTodos().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <returns></returns>
        public List<MidiaCategoria> CarregarMidiaCategoriaPorMidia(Midia midia)
        {
            return MidiaCategoriaDAL.Carregar(midia).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midia"></param>
        /// <returns></returns>
        public List<MidiaRevista> CarregarMidiaRevistaPorMidia(Midia midia)
        {
            return MidiaRevistaDAL.Carregar(midia).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="midiaTipoId"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public List<Midia> CarregarTodosPorRevista(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32? midiaTipoId, Int32 revistaId)
        {
            return MidiaDAL.CarregarTodosPorRevista(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, midiaTipoId, revistaId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midiaTipoId"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public Int32 ContarTodosPorRevista(Int32? midiaTipoId, Int32 revistaId)
        {
            return MidiaDAL.ContarTodosPorRevista(midiaTipoId, revistaId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public int ContarMidiaBusca(String palavra)
        {
            palavra = BuscaHelper.FormataPalavraFiltro(palavra);

            return MidiaDAL.ContarMidiaBusca(palavra);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordenacao"></param>
        /// <param name="ordenacaoSentido"></param>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public List<Midia> CarregarMidiaBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra)
        {
            palavra = BuscaHelper.FormataPalavraFiltro(palavra);

            return MidiaDAL.CarregarMidiaBusca(registrosPagina, numeroPagina, ordenacao, ordenacaoSentido, palavra);
        }
    }
}