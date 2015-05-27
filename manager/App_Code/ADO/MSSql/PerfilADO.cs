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
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Helper;
using System.Web.Security;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Summary description for PerfilADO
    /// </summary>
    public class PerfilADO : BaseADO, IPerfilDAL
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PerfilADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Delete(Ag2.Manager.Entity.ag2mngPerfil perfil)
        {
            DbCommand dataProc = db.GetStoredProcCommand("[ag2mngPerfilDelete]");
            db.AddInParameter(dataProc, "@perfilId", DbType.Int32, perfil.perfilId);
            db.ExecuteNonQuery(dataProc);
        }

        /// <summary>
        /// Faz a associação de um perfil para um usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="perfil"></param>
        public void InsertUserPerfil(MembershipUser user, Perfil perfil)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" INSERT INTO [ag2mngUserPerfil] ");
            sbSQL.Append(" ( ");
            sbSQL.Append("      [userId], ");
            sbSQL.Append("      [perfilId] ");
            sbSQL.Append(" ) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" ( ");
            sbSQL.Append("      @userId, ");
            sbSQL.Append("      @perfilId ");
            sbSQL.Append(" ); ");
            //sbSQL.Append(" SELECT @userPerfilId = SCOPE_IDENTITY(); ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());
            //db.AddOutParameter(dataProc, "@userPerfilId", DbType.Int32, 0);
            db.AddInParameter(dataProc, "@userId", DbType.String, user.ProviderUserKey.ToString());
            db.AddInParameter(dataProc, "@perfilId", DbType.Int32, perfil.PerfilId);

            db.ExecuteNonQuery(dataProc);
        }

        /// <summary>
        /// Deleta perfil relacionado ao usuário
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUserPerfil(MembershipUser user)
        {
            DbCommand dataProc = db.GetSqlStringCommand(" DELETE FROM [ag2mngUserPerfil] WHERE [userId] = @userId ");
            db.AddInParameter(dataProc, "@userId", DbType.String, user.ProviderUserKey.ToString());
            db.ExecuteNonQuery(dataProc);
        }

        /// <summary>
        /// Retorna uma lista de Perfis
        /// </summary>
        /// <returns></returns>
        public List<Perfil> GetAllPerfil()
        {
            List<Perfil> perfis = new List<Perfil>();
            Perfil perfil = null;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT perfilId, name FROM dbo.ag2mngPerfil ORDER BY name ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());
            IDataReader dr = db.ExecuteReader(dataProc);

            while (dr.Read())
            {
                perfil = new Perfil();
                perfil.PerfilId = Convert.ToInt32(dr["perfilId"]);
                perfil.Name = dr["name"].ToString();

                perfis.Add(perfil);
            }
            dr.Close();
            dataProc.Connection.Close();

            return perfis;
        }

        /// <summary>
        /// Retorna uma entidade do tipo Perfil 
        /// </summary>
        /// <param name="user">Usuário que se deseja saber o perfil</param>
        /// <returns></returns>
        public Perfil GetPerfilByUser(MembershipUser user)
        {
            Perfil perfil = new Perfil();
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT P.perfilId, P.name FROM dbo.ag2mngPerfil P ");
            sbSQL.Append(" INNER JOIN ag2mngUserPerfil UP ON UP.perfilId = P.perfilId ");
            sbSQL.Append(" WHERE userId = @userId ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(dataProc, "@userId", DbType.String, user.ProviderUserKey.ToString());

            IDataReader dr = db.ExecuteReader(dataProc);

            while (dr.Read())
            {
                perfil.PerfilId = Convert.ToInt32(dr["perfilId"]);
                perfil.Name = dr["name"].ToString();
            }
            dr.Close();
            dataProc.Connection.Close();

            return perfil;
        }

        /// <summary>
        /// Carrega todos do menus filho de um menu pai
        /// </summary>
        /// <param name="MenuPaiId"></param>
        /// <returns></returns>
        public DataTable MenuLoadByParentId(int MenuPaiId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("SELECT * FROM ag2mngMenu WHERE parentMenuId = {0} ORDER BY menuOrder ", MenuPaiId.ToString());
            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());

            return db.ExecuteDataSet(dataProc).Tables[0];
        }

        /// <summary>
        /// Carrega menus de acordo mcom a permissão
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="perfil"></param>
        /// <returns></returns>
        public IDataReader LoadPermissionByMenuIdAndPerfilId(string menuId, Perfil perfil)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT * FROM ag2mngPermission WHERE menuId=");
            sbSQL.Append(menuId.ToString());
            sbSQL.Append(" AND perfilId=");
            sbSQL.Append(perfil.PerfilId.ToString());
            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());

            return db.ExecuteReader(dataProc);
        }

        /// <summary>
        /// Carrega Permissões
        /// </summary>
        /// <returns></returns>
        public DataTable LoadPermission()
        {
            DbCommand dataProc = db.GetStoredProcCommand("ag2mngMenuLoad");
            return db.ExecuteDataSet(dataProc).Tables[0];
        }

        /// <summary>
        /// Carrega perfil 
        /// </summary>
        /// <param name="perfil">Id do perfil a ser carregado</param>
        /// <returns></returns>
        public Ag2.Manager.Entity.ag2mngPerfil LoadById(Ag2.Manager.Entity.ag2mngPerfil perfil)
        {
            DbCommand dataProc = db.GetStoredProcCommand("ag2mngPerfilLoadById");
            db.AddInParameter(dataProc, "perfilId", DbType.Int32, perfil.perfilId);
            IDataReader dr = db.ExecuteReader(dataProc);

            if (dr.Read())
            {
                perfil.perfilId = Convert.ToInt32(dr["perfilId"].ToString());
                perfil.name = dr["name"].ToString();
                perfil.resetPassword = Convert.ToBoolean(dr["resetPassword"]);
            }

            return perfil;
        }

        /// <summary>
        /// Salva o registro de perfil e suas pemissões
        /// </summary>
        /// <param name="perfil">perfil a ser salvo</param>
        /// <param name="repeater">Repeater que contem as permissões seleciondas para o perfil</param>
        public void Save(Ag2.Manager.Entity.ag2mngPerfil perfil, Repeater repeater)
        {
            GridView grid = new GridView();
            int menuId = 0;
            bool fullControl = false;
            bool canInsert = false;
            bool canDelete = false;
            bool canRead = false;
            bool canUpdate = false;
            bool liberaMenuPai = false;
            bool insereNovo = false;

            DbCommand dataProc = db.GetStoredProcCommand("ag2mngPerfilExist");
            db.AddInParameter(dataProc, "perfilId", DbType.String, perfil.perfilId);
            db.AddInParameter(dataProc, "name", DbType.String, perfil.name);
            if (Convert.ToBoolean(db.ExecuteScalar(dataProc)))
            {
                Util.ShowMessage("Já existe um perfil com este nome.");
                return;
            }

            if (perfil.perfilId > 0)
            {
                dataProc = db.GetStoredProcCommand("ag2mngPerfilUpdate");
                db.AddInParameter(dataProc, "PerfilId", DbType.String, perfil.perfilId);
            }
            else
            {
                dataProc = db.GetStoredProcCommand("ag2mngPerfilInsert");
                db.AddOutParameter(dataProc, "PerfilId", DbType.Int32, 0);
                insereNovo = true;
            }

            db.AddInParameter(dataProc, "name", DbType.String, perfil.name);
            db.AddInParameter(dataProc, "resetPassword", DbType.Boolean, perfil.resetPassword);

            db.ExecuteNonQuery(dataProc);

            if (insereNovo)
                perfil.perfilId = Convert.ToInt32(db.GetParameterValue(dataProc, "PerfilId"));

            // Deleta as permissoes atuais
            dataProc = db.GetStoredProcCommand("ag2mngPermissionDeletebyPerfilId");
            db.AddInParameter(dataProc, "perfilId", DbType.Int32, perfil.perfilId);
            db.ExecuteNonQuery(dataProc);

            foreach (Control c in repeater.Controls)
            {
                RepeaterItem rpt = (RepeaterItem)c;
                grid = ((GridView)rpt.FindControl("gvSubMenu"));

                liberaMenuPai = false;

                foreach (GridViewRow row in grid.Rows)
                {
                    fullControl = false;
                    canInsert = false;
                    canDelete = false;
                    canRead = false;
                    canUpdate = false;

                    if (((CheckBox)row.FindControl("chkFull")).Checked)
                    {
                        fullControl = true;
                        liberaMenuPai = true;

                    }

                    if (((CheckBox)row.FindControl("chkRead")).Checked)
                    {
                        canRead = true;
                        liberaMenuPai = true;
                    }

                    if (((CheckBox)row.FindControl("chkInsert")).Checked)
                    {
                        canInsert = true;
                        liberaMenuPai = true;
                    }

                    if (((CheckBox)row.FindControl("chkUpdate")).Checked)
                    {
                        canUpdate = true;
                        liberaMenuPai = true;
                    }

                    if (((CheckBox)row.FindControl("chkDelete")).Checked)
                    {
                        canDelete = true;
                        liberaMenuPai = true;
                    }

                    menuId = Convert.ToInt32(row.Cells[0].Text.ToString());

                    dataProc = db.GetStoredProcCommand("ag2mngPermissionInsert");
                    db.AddInParameter(dataProc, "@perfilId", DbType.Int32, perfil.perfilId);
                    db.AddInParameter(dataProc, "@menuId", DbType.Int32, menuId);
                    db.AddInParameter(dataProc, "@fullControl", DbType.Boolean, fullControl);
                    db.AddInParameter(dataProc, "@canInsert", DbType.Boolean, canInsert);
                    db.AddInParameter(dataProc, "@canDelete", DbType.Boolean, canDelete);
                    db.AddInParameter(dataProc, "@canRead", DbType.Boolean, canRead);
                    db.AddInParameter(dataProc, "@canUpdate", DbType.Boolean, canUpdate);
                    db.AddInParameter(dataProc, "@liberaMenuPai", DbType.Boolean, liberaMenuPai);
                    db.ExecuteNonQuery(dataProc);
                }


            }

            if (insereNovo)
            {
                Util.ShowInsertMessage();
            }
            else
            {
                Util.ShowUpdateMessage();
            }
        }
    }
}
