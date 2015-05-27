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
    class SicronizacaoBLL : BaseBLL
    {

        #region Propriedades
        private IUsuarioDAL usuarioDAL;
        public IUsuarioDAL UsuarioDAL
        {
            get 
            {
                if (usuarioDAL == null)
                {
                    usuarioDAL = new UsuarioADO();
                }
                return usuarioDAL;
            }
        }
        #endregion

        /// <summary>
        /// Carrega em uma listagem todos os usuários que tiveram seus dados atualizados e devem ser sincronizados com o ERP
        /// </summary>
        /// <returns>Usuários que deverão ser sincronizados</returns>
        public List<Usuario> CarregarTodosUsuariosComSincronizacaoPendente()
        {
            return usuarioDAL.CarregarUsuariosComSincronizacaoPendente();
        }

        /// <summary>
        /// Atualiza as informações de controle sobre os usuários
        /// </summary>
        /// <param name="usuarios">Listagem de usuários que contém as informações de controle sobre o usuário</param>
        public void AtualizarDadosControleUsuario( List<Usuario> usuarios )
        { 
            
        }
    }
}
