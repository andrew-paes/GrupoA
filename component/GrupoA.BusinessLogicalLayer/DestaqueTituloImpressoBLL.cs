using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class DestaqueTituloImpressoBLL : BaseBLL
    {
        private IDestaqueTituloImpressoDAL _destaqueTituloImpressoDAL;

        private IDestaqueTituloImpressoDAL DestaqueTituloImpressoDAL
        {
            get
            {
                if (_destaqueTituloImpressoDAL == null)
                    _destaqueTituloImpressoDAL = new DestaqueTituloImpressoADO();
                return _destaqueTituloImpressoDAL;

            }
        }

        public DestaqueTituloImpresso Carregar(DestaqueTituloImpresso entidade)
        {
            return DestaqueTituloImpressoDAL.Carregar(entidade);
        }
        public IEnumerable<DestaqueTituloImpresso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return DestaqueTituloImpressoDAL.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }
        public DestaqueTituloImpresso CarregarTitulosRelacionados(DestaqueTituloImpresso entidade)
        {
            return DestaqueTituloImpressoDAL.CarregarComTitulosRelacionados(entidade);
        }

        public void ExcluirTitulosRelacionados(DestaqueTituloImpresso entidade)
        {
            if (entidade.Titulos != null && entidade.Titulos.Count > 0)
            {
                DestaqueTituloImpressoDAL.ExcluirTitulosRelacionados(entidade);
            }
        }
        
        public void InserirTitulosRelacionados(DestaqueTituloImpresso entidade)
        {
            if (entidade.Titulos != null && entidade.Titulos.Count > 0)
            {
                foreach (Titulo item in entidade.Titulos)
                {
                    DestaqueTituloImpressoDAL.InserirTituloRelacionado(entidade.DestaqueTituloImpressoId, item.TituloId);
                }
            }
        }
    }
}
