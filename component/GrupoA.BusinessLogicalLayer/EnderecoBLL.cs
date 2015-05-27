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
    public class EnderecoBLL : BaseBLL
    {
        private IEnderecoDAL _enderecoDAL;

        private IEnderecoDAL EnderecoDAL
        {
            get
            {
                if (_enderecoDAL == null)
                    _enderecoDAL = new EnderecoADO();
                return _enderecoDAL;

            }
        }

        public Endereco Carregar(Endereco entidade)
        {
            return EnderecoDAL.Carregar(entidade);
        }

        public void Inserir(Endereco entidade)
        {
            EnderecoDAL.Inserir(entidade);
        }

        public void Atualizar(Endereco entidade)
        {
            EnderecoDAL.Atualizar(entidade);
        }

        public void Excluir(Endereco entidade)
        {
            EnderecoDAL.Excluir(entidade);
        }

        public Endereco CarregarEnderecoComDependencias(Endereco entidade)
        {
            return EnderecoDAL.CarregarEnderecoComDependencias(entidade);
        }
    }
}