using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.BusinessLogicalLayer
{
    public class CapituloBLL : BaseBLL
    {
        #region Declarações DAL

        private ICapituloDAL _capituloDal;
        private ICapituloDAL CapituloDal
        {
            get { return _capituloDal ?? (_capituloDal = new CapituloADO()); }
        }

        private ITituloDAL _tituloDal;
        private ITituloDAL TituloDal
        {
            get { return _tituloDal ?? (_tituloDal = new TituloADO()); }
        }

        #endregion

        #region Métodos: Capitulo

        /// <summary>
        /// Carrega um objeto Capitulo com suas dependências.
        /// </summary>
        /// <param name="capitulo">Objeto Capitulo com identificador configurado.</param>
        /// <returns>Objeto Capitulo com seud dados configurados.</returns>
        public Capitulo CarregarComDependencias(Capitulo capitulo)
        {
            capitulo = CapituloDal.CarregarComDependencias(capitulo);
            capitulo.Titulo = TituloDal.Carregar(new Titulo() { TituloId = capitulo.Titulo.TituloId });
            capitulo.Autores = CapituloDal.CarregarAutoresDoCapitulo(capitulo);
            return capitulo;
        }

        /// <summary>
        /// Exclui um Capitulo.
        /// </summary>
        /// <param name="capitulo">Objeto Capitulo com identificador configurado.</param>
        public void ExcluirCapituloAutor(Capitulo capitulo)
        {
            CapituloDal.ExcluirCapituloAutor(capitulo);
        }

        /// <summary>
        /// Insere os Autores de um Capitulo.
        /// </summary>
        /// <param name="capitulo"></param>
        public void InserirAutoresDeCapitulo(Capitulo capitulo, List<Autor> autores)
        {
            CapituloDal.InserirAutoresDeCapitulo(capitulo, autores);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Capitulo Carregar(Capitulo entidade)
        {
            return CapituloDal.Carregar(entidade);
        }

        public Capitulo CarregarPorCodigoLegado(Capitulo entidade)
        {
            return CapituloDal.CarregarPorCodigoLegado(entidade);
        }

        public void Inserir(Capitulo entidade)
        {
            CapituloDal.Inserir(entidade);
        }

        public void Atualizar(Capitulo entidade)
        {
            CapituloDal.Atualizar(entidade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void AtualizarMenosNomeResumo(Capitulo entidade)
        {
            CapituloDal.AtualizarMenosNomeResumo(entidade);
        }

        #endregion
    }
}