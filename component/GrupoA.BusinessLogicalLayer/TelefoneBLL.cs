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
    public class TelefoneBLL : BaseBLL
    {
        private ITelefoneDAL _telefoneDAL;

        private ITelefoneDAL TelefoneDAL
        {
            get
            {
                if (_telefoneDAL == null)
                    _telefoneDAL = new TelefoneADO();
                return _telefoneDAL;

            }
        }

        public Telefone Carregar(Telefone entidade)
        {
            return TelefoneDAL.Carregar(entidade);
        }
        public IEnumerable<Telefone> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            return TelefoneDAL.CarregarTodos(registrosPagina, numeroPagina, ordemColunas, ordemSentidos, filtro);
        }

        public void Inserir(Telefone entidade)
        {
            TelefoneDAL.Inserir(entidade);
        }

        public void Atualizar(Telefone entidade)
        {
            TelefoneDAL.Atualizar(entidade);
        }

        public void Excluir(Telefone entidade)
        {
            TelefoneDAL.Excluir(entidade);
        }
    }
}