using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;

namespace GrupoA.BusinessLogicalLayer
{
    public class TituloEletronicoBLL : BaseBLL
    {
        #region Declarações DAL

        private ITituloEletronicoDAL _tituloEletronicoDAL;

        private ITituloEletronicoDAL TituloEletronicoDAL
        {
            get { return _tituloEletronicoDAL ?? (_tituloEletronicoDAL = new TituloEletronicoADO()); }
        }


        #endregion

        #region Métodos: TituloEletronico

        public TituloEletronico CarregarComDependencias(TituloEletronico entidade)
        {
            entidade = TituloEletronicoDAL.CarregarComDependencias(entidade);
            return entidade;
        }

        public TituloEletronico Carregar(TituloEletronico entidade)
        {
            entidade = TituloEletronicoDAL.Carregar(entidade);
            return entidade;
        }

        public IEnumerable<TituloEletronico> CarregarTodos(TituloEletronico entidade)
        {
            var tituloEletronicoFH = new TituloEletronicoFH() { Isbn13 = entidade.Isbn13 };
            IEnumerable<TituloEletronico> tituloEletronicos = TituloEletronicoDAL.CarregarTodos(0, 0, null, null, tituloEletronicoFH);
            return tituloEletronicos;
        }

        public TituloEletronico Carregar(Titulo entidade)
        {
            return TituloEletronicoDAL.Carregar(entidade);
        }

        #endregion

        #region Métodos: Titulo Eletrônico Autor

        public List<Autor> CarregarTituloEletronicoAutorComDependencias(TituloEletronico entidade)
        {
            throw new Exception("Ajustar, Autor vem direto de título agora!");
            return null;
            //return TituloEletronicoAutorDAL.CarregarTituloEletronicoAutorComDependencias(entidade);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Isbn13"></param>
        /// <returns></returns>
        public TituloEletronico CarregarPorIsbn13(String Isbn13)
        {
            return TituloEletronicoDAL.CarregarPorIsbn13(Isbn13);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Inserir(TituloEletronico entidade)
        {
            TituloEletronicoDAL.Inserir(entidade);
        }

        public void AtualizarISBN(TituloEletronico entidade)
        {
            TituloEletronicoDAL.AtualizarISBN(entidade);
        }

        public TituloEletronico CarregarPorTitulo(Titulo tituloBO)
        {
            return TituloEletronicoDAL.CarregarPorTitulo(tituloBO);
        }
    }
}