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
    public class ContatoResponsavelBLL : BaseBLL
    {
         
        #region Propriedades

        private IContatoResponsavelDAL _contatoResponsavelDAL;
        public IContatoResponsavelDAL ContatoResponsavelDAL
        {
            get 
            {
                if (_contatoResponsavelDAL == null)
                    _contatoResponsavelDAL = new ContatoResponsavelADO();
                return _contatoResponsavelDAL; 
            }            
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega TODOS registros da tabela ContatoResponsavel.
        /// </summary>
        public IEnumerable<ContatoResponsavel> CarregarTodos()
        {
            return ContatoResponsavelDAL.CarregarTodos();                 
        }

        /// <summary>
        /// Método que carrega um registro da tabela ContatoResponsavel.
        /// </summary>
        public ContatoResponsavel Carregar(ContatoResponsavel responsavel)
        {
            return ContatoResponsavelDAL.Carregar(responsavel);
        }

        /// <summary>
        /// Método de persistência - Update em ContatoResponsavel
        /// </summary>
        public void AtualizarContatoResponsavel(ContatoResponsavel revista)
        {
            ContatoResponsavelDAL.Atualizar(revista);
        }

        /// <summary>
        /// Método de persistência - Insert em ContatoResponsavel
        /// </summary>
        public void InserirContatoResponsavel(ContatoResponsavel revista)
        {
            ContatoResponsavelDAL.Inserir(revista);       
        }

        #endregion

    }
}
