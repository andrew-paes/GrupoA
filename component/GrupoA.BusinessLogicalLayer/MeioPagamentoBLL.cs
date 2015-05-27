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
    public class MeioPagamentoBLL : BaseBLL
    {
        private IMeioPagamentoDAL _meioPagamentoDAL;

        private IMeioPagamentoDAL MeioPagamentoDAL
        {
            get
            {
                if (_meioPagamentoDAL == null)
                    _meioPagamentoDAL = new MeioPagamentoADO();
                return _meioPagamentoDAL;

            }
        }

        public MeioPagamento Carregar(MeioPagamento entidade)
        {
            return MeioPagamentoDAL.Carregar(entidade);
        }
        public MeioPagamento CarregarComDependencias(MeioPagamento entidade)
        {
            return MeioPagamentoDAL.CarregarComDependencias(entidade);

        }
        public IEnumerable<MeioPagamento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return MeioPagamentoDAL.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        public void Atualizar(MeioPagamento entidade)
        {
            MeioPagamentoDAL.Atualizar(entidade);
        }

        public void Inserir(MeioPagamento entidade)
        {
            MeioPagamentoDAL.Inserir(entidade);
        }

    }
}
