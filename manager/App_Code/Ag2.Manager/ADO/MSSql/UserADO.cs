using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Usuários de acesso ao gerenciador de conteúdo
    /// </summary>
    public class UserADO : BaseADO, Ag2.Manager.DAL.IUserDAL
    {
        //
        public UserADO()
        {
            //
            // TODO: Add constructor logic here
            //            
        }

        public void ResetPassword(Ag2.Manager.Entity.ag2mngUser user)
        {
            user.password = Guid.NewGuid().ToString();
            user.password = user.password.Substring(0, 8);

            DbCommand cmd = db.GetStoredProcCommand("[ag2mngUserResetPassword]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            db.AddInParameter(cmd, "@password", DbType.String, user.password);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        public void DeleteUser(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngUserDeleteLogic]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        public bool Exists(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngUserExists]");
            db.AddInParameter(cmd, "@login", DbType.String, user.login);
            bool retorno = Convert.ToBoolean(db.ExecuteScalar(cmd));
            cmd.Connection.Close();

            return retorno;
        }

        public List<Ag2.Manager.Entity.ag2mngPerfil> GetPerfilsByPerfis(List<Ag2.Manager.Entity.ag2mngPerfil> perfis)
        {
            System.Text.StringBuilder sbIds = new StringBuilder();
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngPerfilByIds]");

            foreach (Ag2.Manager.Entity.ag2mngPerfil p in perfis)
            {
                if (sbIds.Length > 0)
                    sbIds.Append(",");
                sbIds.Append(p.perfilId.ToString());
            }

            db.AddInParameter(cmd, "@ids", DbType.String, sbIds.ToString());
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            List<Ag2.Manager.Entity.ag2mngPerfil> lista = new List<Ag2.Manager.Entity.ag2mngPerfil>();
            Ag2.Manager.Entity.ag2mngPerfil perfil = null;

            foreach (DataRow dr in dt.Rows)
            {
                perfil = new Ag2.Manager.Entity.ag2mngPerfil();
                PopulaPerfil(perfil, dr);
                lista.Add(perfil);
            }

            return lista;
        }

        public Ag2.Manager.Entity.ag2mngUser Load(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngUserLoad]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            if (dt.Rows.Count > 0)
            {
                PopulaUser(user, dt.Rows[0]);
            }

            return user;
        }

        public List<Ag2.Manager.Entity.ag2mngPerfil> GetAllPerfil()
        {
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngPerfilSelect]");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            List<Ag2.Manager.Entity.ag2mngPerfil> lista = new List<Ag2.Manager.Entity.ag2mngPerfil>();
            Ag2.Manager.Entity.ag2mngPerfil perfil = null;

            foreach (DataRow dr in dt.Rows)
            {
                perfil = new Ag2.Manager.Entity.ag2mngPerfil();
                PopulaPerfil(perfil, dr);
                lista.Add(perfil);
            }

            return lista;
        }

        public void Save(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbConnection conn = db.CreateConnection();
            conn.Open();

            //INICIA UMA TRANSAÇÂO
            DbTransaction transaction = conn.BeginTransaction();
            DbCommand cmd = null;

            try
            {
                if (user.userId > 0)
                {
                    cmd = db.GetStoredProcCommand("[ag2mngUserUpdate]");
                    db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
                }
                else
                {
                    cmd = db.GetStoredProcCommand("[ag2mngUserInsert]");
                    db.AddOutParameter(cmd, "@userId", DbType.Int32, 0);
                }

                db.AddInParameter(cmd, "@name", DbType.String, user.name);
                db.AddInParameter(cmd, "@email", DbType.String, user.email);
                db.AddInParameter(cmd, "@login", DbType.String, user.login);
                db.AddInParameter(cmd, "@password", DbType.String, user.password);
                db.AddInParameter(cmd, "@active", DbType.Boolean, user.active);
                db.AddInParameter(cmd, "@deleted", DbType.Boolean, false);
                db.ExecuteNonQuery(cmd, transaction);

                if (user.userId.Equals(0))
                    user.userId = Convert.ToInt32(db.GetParameterValue(cmd, "@userId"));

                //PERFIL
                cmd = db.GetStoredProcCommand("[ag2mngUserPerfilDelete]");
                db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
                db.ExecuteNonQuery(cmd, transaction);

                cmd = db.GetStoredProcCommand("[ag2mngUserPerfilInsert]");
                db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
                db.AddInParameter(cmd, "@perfilId", DbType.Int32, 0);

                foreach (Ag2.Manager.Entity.ag2mngPerfil perfil in user.perfis)
                {
                    db.SetParameterValue(cmd, "perfilId", perfil.perfilId);
                    db.ExecuteNonQuery(cmd, transaction);
                }

                //FINALIZA A TRANSAÇAO
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                //CASO DE ERRO DESFAZ TUDO
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                //FECHA CONEXAO COM A BASE
                conn.Close();
            }
        }

        public void ChangePassword(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ChangePassword]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            db.AddInParameter(cmd, "@password", DbType.String, user.password);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        public bool ValidateUser(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ValidateUser]");
            db.AddInParameter(cmd, "@login", DbType.String, user.login);
            db.AddInParameter(cmd, "@password", DbType.String, user.password);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            if (dt.Rows.Count > 0)
            {
                PopulaUser(user, dt.Rows[0]);
            }

            return user.userId > 0;

        }

        public List<Ag2.Manager.Entity.ag2mngMenu> GetMenusPermission(List<Ag2.Manager.Entity.ag2mngPerfil> perfis)
        {
            System.Text.StringBuilder sbIds = new StringBuilder();

            DbCommand cmd = db.GetStoredProcCommand("[GetMenusPermissionByPerfis]");

            foreach (Ag2.Manager.Entity.ag2mngPerfil p in perfis)
            {
                if (sbIds.Length > 0)
                    sbIds.Append(",");
                sbIds.Append(p.perfilId.ToString());
            }

            db.AddInParameter(cmd, "@perfisId", DbType.String, sbIds.ToString());
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            List<Ag2.Manager.Entity.ag2mngMenu> menus = new List<Ag2.Manager.Entity.ag2mngMenu>();
            Ag2.Manager.Entity.ag2mngMenu menu = null;

            foreach (DataRow dr in dt.Rows)
            {
                menu = new Ag2.Manager.Entity.ag2mngMenu();
                PopulaMenus(menu, dr);
                menus.Add(menu);
            }

            return menus;
        }

        public Ag2.Manager.Entity.ag2mngMenu GetMenuPermissionByMenu(Ag2.Manager.Entity.ag2mngMenu menu)
        {
            DbCommand cmd = db.GetStoredProcCommand("[GetMenusPermissionByMenu]");

            db.AddInParameter(cmd, "@menuId", DbType.Int32, menu.menuId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            if (dt.Rows.Count > 0)
            {
                menu = new Ag2.Manager.Entity.ag2mngMenu();
                PopulaMenus(menu, dt.Rows[0]);
            }

            return menu;
        }

        /// <summary>Consulta usario pelo ID</summary>
        /// <param name="ID">Código do usuário a ser pesquisado</param>
        public DataTable GetById(int ID)
        {
            //dataset com informações do usuario
            DataTable userData;

            DbCommand userProc = db.GetStoredProcCommand("UserGetById");

            //configura parametros da stored procedure
            db.AddInParameter(userProc, "ID", DbType.Int32, ID);

            //executa consuta           
            userData = db.ExecuteDataSet(userProc).Tables[0];

            //retorno da função
            return userData;
        }

        /// <summary>Consulta usuário pelo login</summary>
        /// <param name="login"></param>
        public DataTable GetByLogin(string login)
        {
            //dataset com informações do usuario
            DataTable userData;

            DbCommand userProc = db.GetStoredProcCommand("UserGetByLogin");

            //configura parametros da stored procedure
            db.AddInParameter(userProc, "Login", DbType.String, login.ToString());


            //executa consuta           
            userData = db.ExecuteDataSet(userProc).Tables[0];

            //retorno da função
            return userData;
        }

        public List<Ag2.Manager.Entity.ag2mngPerfil> GetPerfilByUser(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("ag2mngPerfilByUserIdLoadById");
            db.AddInParameter(cmd, "@userId", DbType.String, user.userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            List<Ag2.Manager.Entity.ag2mngPerfil> perfis = new List<Ag2.Manager.Entity.ag2mngPerfil>();
            Ag2.Manager.Entity.ag2mngPerfil perfil = null;

            foreach (DataRow dr in dt.Rows)
            {
                perfil = new Ag2.Manager.Entity.ag2mngPerfil();
                PopulaPerfil(perfil, dr);
                perfis.Add(perfil);
            }

            return perfis;
        }

        public List<Ag2.Manager.Entity.ag2mngPerfil> GetPerfilsByUser(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngUserPerfilByUser]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            List<Ag2.Manager.Entity.ag2mngPerfil> lista = new List<Ag2.Manager.Entity.ag2mngPerfil>();
            Ag2.Manager.Entity.ag2mngPerfil perfil = null;

            foreach (DataRow dr in dt.Rows)
            {
                perfil = new Ag2.Manager.Entity.ag2mngPerfil();
                PopulaPerfil(perfil, dr);
                lista.Add(perfil);
            }

            return lista;
        }

        private void PopulaMenus(Ag2.Manager.Entity.ag2mngMenu menu, DataRow dr)
        {
            menu.active = Convert.ToBoolean(dr["active"]);
            menu.menuId = Convert.ToInt32(dr["menuId"]);
            menu.moduleName = dr["moduleName"].ToString();
            menu.name = dr["name"].ToString();
            menu.parentMenuId = Convert.ToInt32(dr["parentMenuId"]);
            menu.canDelete = Convert.ToBoolean(dr["canDelete"]);
            menu.canInsert = Convert.ToBoolean(dr["canInsert"]);
            menu.canRead = Convert.ToBoolean(dr["canRead"]);
            menu.canUpdate = Convert.ToBoolean(dr["canUpdate"]);
            menu.canPublish = Convert.ToBoolean(dr["canPublish"]);
            menu.fullControl = Convert.ToBoolean(dr["fullControl"]);
        }

        private void PopulaPerfil(Ag2.Manager.Entity.ag2mngPerfil perfil, DataRow dr)
        {
            perfil.perfilId = Convert.ToInt32(dr["perfilId"]);
            perfil.name = dr["name"].ToString();
            perfil.resetPassword = Convert.ToBoolean(dr["resetPassword"]);
        }

        private void PopulaUser(Ag2.Manager.Entity.ag2mngUser user, DataRow dr)
        {
            user.userId = Convert.ToInt32(dr["userId"]);
            user.active = Convert.ToBoolean(dr["active"]);
            user.deleted = Convert.ToBoolean(dr["deleted"]);
            user.email = dr["email"].ToString();
            user.login = dr["login"].ToString();
            user.password = dr["password"].ToString();
            user.name = dr["name"].ToString();
            user.perfis = this.GetPerfilsByUser(user);
            user.Menus = this.GetMenusPermission(user.perfis);
        }

    }
}
