using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.OracleClient;
using Ag2.Manager.DAL;
using Ag2.Manager.Helper;
using System.Web.Security;

namespace Ag2.Manager.ADO.Oracle
{
    /// <summary>
    /// Summary description for UsuarioADO
    /// </summary>
    public class UsuarioADO : BaseADO, Ag2.Manager.DAL.IUsuarioDAL
    {
        public UsuarioADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void GetUser()
        { 
            //
        }

        /// <summary>
        /// Deleta o usuário da base de dados
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(MembershipUser user)
        {
            PerfilADO perfilADO = new PerfilADO();

            //REMOVE O RELACIONAMENTO COM O PERFIL DO USUARIO
            perfilADO.DeleteUserPerfil(user);

            //REMOVE USUARIO DO MEMBERSHIP
            Membership.DeleteUser(user.UserName);
        }
    }
}
