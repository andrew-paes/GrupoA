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
    public class ContatoAssuntoBLL : BaseBLL
    {
         
        #region Propriedades

        private IContatoAssuntoDAL _contatoAssuntoDAL;
        public IContatoAssuntoDAL ContatoAssuntoDAL
        {
            get { return _contatoAssuntoDAL ?? (_contatoAssuntoDAL = new ContatoAssuntoADO()); }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que carrega TODOS registros da tabela ContatoAssunto.
        /// </summary>
        public IEnumerable<ContatoAssunto> CarregarTodos()
        {
            return ContatoAssuntoDAL.CarregarTodos();                 
        }

        /// <summary>
        /// Método que carrega TODOS registros da tabela ContatoAssunto por SETOR.
        /// </summary>
        public IEnumerable<ContatoAssunto> CarregarPorSetor(ContatoSetor setor)
        {
            return ContatoAssuntoDAL.CarregarTodosDoSetor(setor); 
        }

        /// <summary>
        /// Método que carrega um registro da tabela ContatoAssunto.
        /// </summary>
        public ContatoAssunto Carregar(ContatoAssunto assunto)
        {
            return ContatoAssuntoDAL.Carregar(assunto);
        }

        /// <summary>
        /// Método que Carrega ContatoAssunto
        /// </summary>
        public ContatoAssunto CarregarContatoAssunto(ContatoAssunto entidade)
        {
            ContatoAssunto contatoAssunto = new ContatoAssunto();
            contatoAssunto = ContatoAssuntoDAL.Carregar(entidade);
            contatoAssunto.ContatoSetor = new ContatoSetorBLL().CarregarContatoSetor(new ContatoSetor() { ContatoSetorId = contatoAssunto.ContatoSetor.ContatoSetorId });
            return contatoAssunto;
        }

        /// <summary>
        /// Método de persistência - Update em RevistaSecao
        /// </summary>
        public void AtualizarContatoAssunto(ContatoAssunto revista)
        {
            ContatoAssuntoDAL.Atualizar(revista);
        }

        /// <summary>
        /// Método de persistência - Insert em ContatoAssunto
        /// </summary>
        public void InserirContatoAssunto(ContatoAssunto revista)
        {
            ContatoAssuntoDAL.Inserir(revista);       
        }
        
        /// <summary>
        /// Método de persistência - Delete em ContatoAssunto
        /// </summary>
        public void ExcluirContatoAssunto(ContatoAssunto contatoAssunto)
        {
            contatoAssunto = this.CarregarContatoAssunto(contatoAssunto);

            using (TransactionScope scope = new TransactionScope())
            {
                ContatoAssuntoDAL.Excluir(contatoAssunto);
                scope.Complete();
            }
        }
        
        #endregion

    }
}
