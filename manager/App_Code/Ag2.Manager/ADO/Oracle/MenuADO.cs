using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;

namespace Ag2.Manager.ADO.Oracle
{
    public class MenuADO : BaseADO, Ag2.Manager.DAL.IMenuDAL
    {

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        public MenuADO()
        {
            //
        }

        /// <summary>
        /// Insere permissão para os Menus
        /// </summary>
        /// <param name="perfilId"></param>
        /// <param name="menuId"></param>
        /// <param name="fullControl"></param>
        /// <param name="canInsert"></param>
        /// <param name="canDelete"></param>
        /// <param name="canRead"></param>
        /// <param name="canUpdate"></param>
        /// <param name="liberaMenuPai"></param>
        public void InsertMenuPermission(int perfilId, int menuId, bool fullControl, bool canInsert, bool canDelete, bool canRead, bool canUpdate, bool liberaMenuPai)
        {
            DbCommand cmd = db.GetStoredProcCommand("ag2mngPermissionInsert");
            db.AddInParameter(cmd, "p_perfilId", DbType.Int32, perfilId);
            db.AddInParameter(cmd, "p_menuId", DbType.Int32, menuId);
            db.AddInParameter(cmd, "fullControl", DbType.Int32, fullControl);
            db.AddInParameter(cmd, "canInsert", DbType.Int32, canInsert);
            db.AddInParameter(cmd, "canDelete", DbType.Int32, canDelete);
            db.AddInParameter(cmd, "canRead", DbType.Int32, canRead);
            db.AddInParameter(cmd, "canUpdate", DbType.Int32, canUpdate);
            db.AddInParameter(cmd, "liberaMenuPai", DbType.Int32, liberaMenuPai);
            db.ExecuteNonQuery(cmd);

            cmd.Connection.Close();
        }

        /// <summary>
        /// Deleta permissões do referido Menu
        /// </summary>
        /// <param name="menuId"></param>
        public void DeleteMenuPermissionByMenuId(int menuId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" delete ag2mngPermission where menuId = :menuId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, ":menuId", DbType.Int32, menuId);

            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        /// <summary>
        /// Atualiza menu
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="nome"></param>
        /// <param name="ativo"></param>
        /// <param name="toolTip"></param>
        /// <param name="orderm"></param>
        /// <param name="nomeModulo"></param>
        /// <param name="parentMenuId"></param>
        public void UpdateMenu(int menuId, string nome, bool ativo, string toolTip, int orderm, string nomeModulo, int parentMenuId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" UPDATE ag2mngMenu SET ");
            sbSQL.Append(" name = :name, active = :active, tooltip = :tooltip, menuOrder = :menuOrder, moduleName = :moduleName, parentMenuId = :parentMenuId ");
            sbSQL.Append(" WHERE menuId = :menuId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, ":menuId", DbType.Boolean, menuId);
            db.AddInParameter(cmd, ":name", DbType.String, nome);
            db.AddInParameter(cmd, ":active", DbType.Boolean, ativo);
            db.AddInParameter(cmd, ":tooltip", DbType.String, toolTip);
            db.AddInParameter(cmd, ":menuOrder", DbType.Int32, orderm);
            db.AddInParameter(cmd, ":moduleName", DbType.String, nomeModulo);
            db.AddInParameter(cmd, ":parentMenuId", DbType.Int32, parentMenuId);

            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        /// <summary>
        /// Retorna o registro do menu referente ao nome enviado, descartando o id do menu enviado
        /// </summary>
        /// <param name="name"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public IDataReader GetMenuByNameDiscartCurrentMenuId(string name, int menuId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT * FROM ag2mngMenu WHERE name = :name AND menuId <> :menuId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, ":name", DbType.String, name);
            db.AddInParameter(cmd, ":menuId", DbType.Int32, menuId);

            IDataReader dr = db.ExecuteReader(cmd);

            return dr;
        }

        /// <summary>
        /// Insere o novo menu na base de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="ativo"></param>
        /// <param name="toolTip"></param>
        /// <param name="orderm"></param>
        /// <param name="nomeModulo"></param>
        /// <param name="parentMenuId"></param>
        /// <returns></returns>
        public int InsertMenu(string nome, bool ativo, string toolTip, int orderm, string nomeModulo, int parentMenuId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" INSERT INTO ag2mngMenu ");
            sbSQL.Append(" (name, active, tooltip, menuOrder, moduleName, parentMenuId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (:name, :active, :tooltip, :menuOrder, :moduleName, :parentMenuId) ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, "name", DbType.String, nome);
            db.AddInParameter(cmd, "active", DbType.Boolean, ativo);
            db.AddInParameter(cmd, "tooltip", DbType.String, toolTip);
            db.AddInParameter(cmd, "menuOrder", DbType.Int32, orderm);
            db.AddInParameter(cmd, "moduleName", DbType.String, nomeModulo);
            db.AddInParameter(cmd, "parentMenuId", DbType.Int32, parentMenuId);

            db.ExecuteNonQuery(cmd);

            sbSQL.Length = 0;
            sbSQL.Append(" SELECT menuid from AG2MNGMENU where rownum <=1 ORDER BY menuid desc ");
            cmd = db.GetSqlStringCommand(sbSQL.ToString());
            int idRetorno = Convert.ToInt32(db.ExecuteScalar(cmd)); //retorna novo ID

            return idRetorno;
        }

        /// <summary>
        /// Retorna o registro do menu pelo Nome
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public IDataReader GetMenuByName(string menuName)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT * FROM ag2mngMenu WHERE name = :name ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, ":name", DbType.String, menuName);

            IDataReader dr = db.ExecuteReader(cmd);

            return dr;
        }

        /// <summary>
        /// Retorna as permissões de cada perfil para o menu informado
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable GetPermissionPerfilByMenuId(int menuId)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT Y.name , Y.perfilId, X.fullControl, X.canInsert, X.canDelete, X.canread, X.canUpdate ");
            sbSQL.Append(" FROM ag2mngperfil Y ");
            sbSQL.Append(" LEFT JOIN ag2mngpermission X on ( X.perfilId = Y.perfilId and menuid = :menuid ) ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, ":menuid", DbType.Int32, menuId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            return dt;
        }

        /// <summary>
        /// Retorna todos do menus pais
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllParentMenus()
        {
            //dataset com informações do usuario
            DataTable menuData;

            // Cria o objeto de acesso a base de dados
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand menuProc = db.GetSqlStringCommand("SELECT * FROM ag2mngMenu WHERE parentMenuId = 0");
            //executa consulta           
            menuData = db.ExecuteDataSet(menuProc).Tables[0];

            //retorno da função
            return menuData;
        }

        /// <summary>
        /// Retorna um DataReader com o registro correspondente ao id informado no parametro
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public IDataReader LoadMenuByMenuId(int menuId)
        {

            DbCommand dataProc = db.GetSqlStringCommand("SELECT * FROM ag2mngMenu WHERE menuid = :menuid");
            db.AddInParameter(dataProc, ":menuid", DbType.Int32, menuId);

            IDataReader registro = db.ExecuteReader(dataProc);

            return registro;
        }

        /// <summary>
        /// Obtem estrutura de menus cadastrada
        /// </summary>
        /// <returns></returns>
        public DataTable GetStructure(int perfilId)
        {
            //dataset com informações do usuario
            DataTable menuData;

            // Cria o objeto de acesso a base de dados
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand menuProc = db.GetStoredProcCommand("MenuGetStructure");

            //configura parametros da stored procedure
            db.AddInParameter(menuProc, "perfilId", DbType.Int32, perfilId);

            //executa consulta           
            menuData = db.ExecuteDataSet(menuProc).Tables[0];

            //retorno da função
            return menuData;
        }

        public DataTable GetBySiteId(int parentId, int perfilId, int siteId)
        {
            //dataset com informações do usuario
            DataTable menuData;

            // Cria o objeto de acesso a base de dados
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand menuProc = db.GetStoredProcCommand("MenuGetBySiteId");

            //configura parametros da stored procedure
            db.AddInParameter(menuProc, "parentId", DbType.Int32, parentId);
            db.AddInParameter(menuProc, "perfilId", DbType.Int32, perfilId);
            db.AddInParameter(menuProc, "siteId", DbType.Int32, siteId);

            //executa consulta           
            menuData = db.ExecuteDataSet(menuProc).Tables[0];

            //retorno da função
            return menuData;
        }

        /// <summary>
        /// Consulta menus pelo código do PAI (parent) 
        /// </summary>
        /// <param name="parentId">Código do PAI</param>
        /// <returns></returns>
        public DataTable GetByParentId(int parentId, int perfilId)
        {
            //dataset com informações do usuario
            DataTable menuData;

            DbCommand menuProc = db.GetStoredProcCommand("MenuGetByParentId");
            menuProc.CommandType = CommandType.StoredProcedure;
            menuProc.Connection = db.CreateConnection();
            //configura parametros da stored procedure

            OracleParameter parametro1 = new OracleParameter("P_PARENTID", OracleType.Int32);
            parametro1.Value = parentId;
            parametro1.Direction = ParameterDirection.Input;

            menuProc.Parameters.Add(parametro1);

            OracleParameter parametro2 = new OracleParameter("P_PERFILID", OracleType.Int32);
            parametro2.Value = perfilId;
            parametro2.Direction = ParameterDirection.Input;

            menuProc.Parameters.Add(parametro2);

            OracleParameter parametro3 = new OracleParameter("OUTCURSOR", OracleType.Cursor);
            parametro3.Direction = ParameterDirection.Output;

            menuProc.Parameters.Add(parametro3);

            menuData = db.ExecuteDataSet(menuProc).Tables[0];

            //retorno da função
            return menuData;
        }

        public DataTable GetByModuleName(string moduleName, int perfilId)
        {
            //dataset com informações do usuario
            DataTable menuData;

            // Cria o objeto de acesso a base de dados
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand menuProc = db.GetStoredProcCommand("menuGetByModuleName");
            menuProc.CommandType = CommandType.StoredProcedure;
            menuProc.Connection = db.CreateConnection();

            OracleParameter parametro1 = new OracleParameter("p_MODULENAME", OracleType.VarChar);
            parametro1.Value = moduleName;
            parametro1.Direction = ParameterDirection.Input;

            menuProc.Parameters.Add(parametro1);

            OracleParameter parametro2 = new OracleParameter("p_PERFILID", OracleType.Int32);
            parametro2.Value = perfilId;
            parametro2.Direction = ParameterDirection.Input;

            menuProc.Parameters.Add(parametro2);

            OracleParameter parametro3 = new OracleParameter("OUTCURSOR", OracleType.Cursor);
            parametro3.Direction = ParameterDirection.Output;

            menuProc.Parameters.Add(parametro3);

            menuData = db.ExecuteDataSet(menuProc).Tables[0];

            //retorno da função
            return menuData;
        }

        //public void SaveItemPermission(ma

    }
}
