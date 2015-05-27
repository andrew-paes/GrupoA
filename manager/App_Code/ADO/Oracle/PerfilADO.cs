using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.DAL;
using Ag2.Manager.Helper;

namespace Ag2.Manager.ADO.Oracle
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
            sbSQL.Append(" INSERT INTO ag2mnguserperfil ");
            sbSQL.Append(" VALUES (:userId, :perfilId) ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(dataProc, ":userId", DbType.String, user.ProviderUserKey.ToString());
            db.AddInParameter(dataProc, ":perfilId", DbType.Int32, perfil.PerfilId);

            db.ExecuteNonQuery(dataProc);
        }

        /// <summary>
        /// Deleta perfil relacionado ao usuário
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUserPerfil(MembershipUser user)
        {
            DbCommand dataProc = db.GetSqlStringCommand(" DELETE FROM ag2mngUserPerfil WHERE userId = :userId ");
            db.AddInParameter(dataProc, ":userId", DbType.String, user.ProviderUserKey.ToString());
            db.ExecuteNonQuery(dataProc);
        }

        /// <summary>
        /// Retirna uma lista de Perfis
        /// </summary>
        /// <returns></returns>
        public List<Perfil> GetAllPerfil()
        {
            List<Perfil> perfis = new List<Perfil>();
            Perfil perfil = null;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT perfilId, name FROM ag2mngPerfil ORDER BY name ");

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

            sbSQL.Append(" SELECT P.perfilId, P.name FROM ag2mngPerfil P ");
            sbSQL.Append(" INNER JOIN ag2mngUserPerfil UP ON UP.perfilId = P.perfilId ");
            sbSQL.Append(" WHERE userId = :userId ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(dataProc, ":userId", DbType.String, user.ProviderUserKey.ToString());

            IDataReader dr = db.ExecuteReader(dataProc);

            while (dr.Read())
            {
                perfil.PerfilId = Convert.ToInt32(dr["perfilId"]);
                perfil.Name = dr["name"].ToString();
            }

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
            sbSQL.Append(" AND perfilId = ");
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
            dataProc.CommandType = CommandType.StoredProcedure;
            dataProc.Connection = db.CreateConnection();

            OracleParameter parametro1 = new OracleParameter("OUTCURSOR", OracleType.Cursor);
            parametro1.Direction = ParameterDirection.Output;

            dataProc.Parameters.Add(parametro1);


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
            dataProc.CommandType = CommandType.StoredProcedure;
            dataProc.Connection = db.CreateConnection();

            OracleParameter parametro1 = new OracleParameter("p_PERFILID", OracleType.Int32);
            parametro1.Value = perfil.perfilId;
            parametro1.Direction = ParameterDirection.Input;

            dataProc.Parameters.Add(parametro1);

            OracleParameter parametro2 = new OracleParameter("OUTCURSOR", OracleType.Cursor);
            parametro2.Direction = ParameterDirection.Output;

            dataProc.Parameters.Add(parametro2);

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

            StringBuilder sbSQL = new StringBuilder();
            if (perfil.perfilId == 0)
                sbSQL.AppendFormat(" SELECT count(perfilId) FROM AG2MNGPERFIL WHERE NAME = :NAME ");
            else
                sbSQL.AppendFormat(" SELECT count(perfilId) FROM AG2MNGPERFIL WHERE NAME = :NAME AND perfilId <> :PERFILID ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());

            if (perfil.perfilId > 0)
            {
                db.AddInParameter(dataProc, ":PERFILID", DbType.Int32, perfil.perfilId);
            }

            db.AddInParameter(dataProc, ":NAME", DbType.String, perfil.name);

            bool exist = Convert.ToInt32(db.ExecuteScalar(dataProc)) > 0 ? true : false;

            if (exist)
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

            db.ExecuteNonQuery(dataProc);

            if (insereNovo)
                perfil.perfilId = Convert.ToInt32(db.GetParameterValue(dataProc, "PerfilId"));

            // Deleta as permissoes atuais
            dataProc = db.GetSqlStringCommand(" DELETE FROM AG2MNGPERMISSION WHERE PERFILID = :PERFILID ");
            db.AddInParameter(dataProc, ":PERFILID", DbType.Int32, perfil.perfilId);
            db.ExecuteNonQuery(dataProc);

            foreach (Control c in repeater.Controls)
            {
                RepeaterItem rpt = (RepeaterItem)c;
                grid = ((GridView)rpt.FindControl("gvSubMenu"));

                liberaMenuPai = false;
                //Response.Write(c.ClientID.ToString()+"<br>");

                foreach (GridViewRow row in grid.Rows)
                {
                    fullControl = false;
                    canInsert = false;
                    canDelete = false;
                    canRead = false;
                    canUpdate = false;

                    if (((CheckBox)row.FindControl("chkFull")).Checked)
                    {
                        //Response.Write(((CheckBox)row.FindControl("chkFull")).Text + "<br>");
                        //Response.Write(row.Cells[0].Text + "<br>");
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

                    sbSQL.Length = 0;

                    sbSQL.Append(" INSERT INTO ag2mngpermission (perfilid, menuid, fullcontrol, caninsert, candelete, canread, canupdate) ");
                    sbSQL.Append("VALUES");
                    sbSQL.Append("(");
                    sbSQL.Append(":PERFILID,");
                    sbSQL.Append(":MENUPAI,");
                    sbSQL.Append(":FULLCONTROL,");
                    sbSQL.Append(":CANINSERT,");
                    sbSQL.Append(":CANDELETE,");
                    sbSQL.Append(":CANREAD,");
                    sbSQL.Append(":CANUPDATE");
                    sbSQL.Append(")");

                    dataProc = db.GetSqlStringCommand(sbSQL.ToString());
                    db.AddInParameter(dataProc, ":PERFILID", DbType.Int32, perfil.perfilId);
                    db.AddInParameter(dataProc, ":MENUPAI", DbType.Int32, menuId);
                    db.AddInParameter(dataProc, ":FULLCONTROL", DbType.Int32, Convert.ToInt32(fullControl));
                    db.AddInParameter(dataProc, ":CANINSERT", DbType.Int32, Convert.ToInt32(canInsert));
                    db.AddInParameter(dataProc, ":CANDELETE", DbType.Int32, Convert.ToInt32(canDelete));
                    db.AddInParameter(dataProc, ":CANREAD", DbType.Int32, Convert.ToInt32(canRead));
                    db.AddInParameter(dataProc, ":CANUPDATE", DbType.Int32, Convert.ToInt32(canUpdate));
                    db.ExecuteNonQuery(dataProc);
                }

            }

            if (insereNovo)
            {
                Util.ShowMessage("Registro inserido com sucesso.");
            }
            else
            {
                Util.ShowMessage("Registro alterado com sucesso.");
            }
        }
    }
}
