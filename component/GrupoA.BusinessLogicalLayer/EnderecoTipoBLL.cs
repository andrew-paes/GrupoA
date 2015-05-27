using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que abstrai as regras de negócio referentes a usuários.
    /// </summary>
    public class EnderecoTipoBLL : BaseBLL
    {
        private IEnderecoTipoDAL _enderecoTipoDAL;

        private IEnderecoTipoDAL EnderecoTipoDAL
        {
            get
            {
                if (_enderecoTipoDAL == null)
                    _enderecoTipoDAL = new EnderecoTipoADO();
                return _enderecoTipoDAL;

            }
        }

        public EnderecoTipo Carregar(EnderecoTipo entidade)
        {
            return EnderecoTipoDAL.Carregar(entidade);
        }

        public List<EnderecoTipo> CarregarTodos()
        {
            return EnderecoTipoDAL.CarregarTodos().ToList();
        }

    }
}
