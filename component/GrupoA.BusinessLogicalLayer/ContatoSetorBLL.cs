using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    public class ContatoSetorBLL : BaseBLL
    { 

        #region Propriedades

        private IContatoSetorDAL _contatoSetorDAL;

        public IContatoSetorDAL ContatoSetorDAL
        {
            get 
            {
                if (_contatoSetorDAL == null)
                    _contatoSetorDAL = new ContatoSetorADO();
                return _contatoSetorDAL;
            }           
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega TODOS registros da tabela ContatoSetor.
        /// </summary>
        public IEnumerable<ContatoSetor> CarregarTodos()
        {
            return ContatoSetorDAL.CarregarTodos();
        }

        /// <summary>
        /// Método que carrega um registro da tabela ContatoSetor.
        /// </summary>
        public ContatoSetor CarregarContatoSetor(ContatoSetor entidade)
        {
            return ContatoSetorDAL.Carregar(entidade);
        }

        /// <summary>
        /// Método de persistência - Insert em ContatoSetor
        /// </summary>
        public void InserirContatoSetor(ContatoSetor entidade)
        {
            ContatoSetorDAL.Inserir(entidade);
        }

        /// <summary>
        /// Método de persistência - Update em ContatoSetor
        /// </summary>
        public void AtualizarContatoSetor(ContatoSetor entidade)
        {
            ContatoSetorDAL.Atualizar(entidade);
        }

        /// <summary>
        /// Método que Verifica se o Nome do Setor é Duplicado na tabela ContatoSetor
        /// </summary>
        public bool VerificaNomeSetorDuplicado(int contatoSetorId, string nomeSetor )
        {
            return ContatoSetorDAL.VerificaNomeSetorDuplicado(contatoSetorId, nomeSetor);
        }

        /// <summary>
        /// Método de persistência - Delete em ContatoSetor
        /// </summary>
        public void ExcluirContatoSetor(ContatoSetor contatoSetor)
        {
            contatoSetor = this.CarregarContatoSetor(contatoSetor);

            using (TransactionScope scope = new TransactionScope())
            {
                ContatoSetorDAL.Excluir(contatoSetor);
                scope.Complete();
            }
        }


        #endregion

    }
}
