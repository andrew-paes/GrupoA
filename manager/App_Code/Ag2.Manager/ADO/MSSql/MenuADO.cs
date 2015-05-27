using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Ag2.Manager.ADO.MSSql
{
    public class MenuADO : BaseADO, Ag2.Manager.DAL.IMenuDAL
    {
        // Static members are lazily initialized.
        // .NET guarantees thread safety for static initialization 
        private static readonly MenuADO _instance = new MenuADO();

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
            db.AddInParameter(cmd, "@perfilId", DbType.Int32, perfilId);
            db.AddInParameter(cmd, "@menuId", DbType.Int32, menuId);
            db.AddInParameter(cmd, "@fullControl", DbType.Boolean, fullControl);
            db.AddInParameter(cmd, "@canInsert", DbType.Boolean, canInsert);
            db.AddInParameter(cmd, "@canDelete", DbType.Boolean, canDelete);
            db.AddInParameter(cmd, "@canRead", DbType.Boolean, canRead);
            db.AddInParameter(cmd, "@canUpdate", DbType.Boolean, canUpdate);
            db.AddInParameter(cmd, "@liberaMenuPai", DbType.Boolean, liberaMenuPai);
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
            sbSQL.Append(" DELETE ag2mngPermission where menuId = @menuId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, "@menuId", DbType.Int32, menuId);

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
            sbSQL.Append(" name = @name, active = @active, tooltip = @tooltip, menuOrder = @menuOrder, moduleName = @moduleName, parentMenuId = @parentMenuId ");
            sbSQL.Append(" WHERE menuId = @menuId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());

            db.AddInParameter(cmd, "@name", DbType.String, nome);
            db.AddInParameter(cmd, "@menuId", DbType.Int32, menuId);
            db.AddInParameter(cmd, "@active", DbType.Boolean, ativo);
            db.AddInParameter(cmd, "@tooltip", DbType.String, toolTip);
            db.AddInParameter(cmd, "@menuOrder", DbType.Int32, orderm);
            db.AddInParameter(cmd, "@moduleName", DbType.String, nomeModulo);
            db.AddInParameter(cmd, "@parentMenuId", DbType.Int32, parentMenuId);


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
            sbSQL.Append(" SELECT * FROM ag2mngMenu WHERE name = @name AND menuId <> @menuId ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, "@name", DbType.String, name);
            db.AddInParameter(cmd, "@menuId", DbType.Int32, menuId);

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
            sbSQL.Append(" (@name, @active, @tooltip, @menuOrder, @moduleName, @parentMenuId); ");
            sbSQL.Append(" SELECT scope_identity(); ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, "name", DbType.String, nome);
            db.AddInParameter(cmd, "active", DbType.Boolean, ativo);
            db.AddInParameter(cmd, "tooltip", DbType.String, toolTip);
            db.AddInParameter(cmd, "menuOrder", DbType.Int32, orderm);
            db.AddInParameter(cmd, "moduleName", DbType.String, nomeModulo);
            db.AddInParameter(cmd, "parentMenuId", DbType.Int32, parentMenuId);

            int idRetorno = Convert.ToInt32(db.ExecuteScalar(cmd)); //retorna novo ID

            cmd.Connection.Close();

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
            sbSQL.Append(" SELECT * FROM ag2mngMenu WHERE name = @name ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, "@name", DbType.String, menuName);

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
            sbSQL.Append(" SELECT P.name , P.perfilId, PE.fullControl, PE.canInsert, PE.canDelete, PE.canread, PE.canUpdate ");
            sbSQL.Append(" FROM ag2mngperfil P ");
            sbSQL.Append(" LEFT JOIN ag2mngpermission PE on ( PE.perfilId = P.perfilId and menuid = @menuid ) ");
            sbSQL.Append(" WHERE P.deleted = 0 ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
            db.AddInParameter(cmd, "@menuid", DbType.Int32, menuId);

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
            DbCommand menuProc = db.GetSqlStringCommand("SELECT * FROM ag2mngMenu WHERE parentMenuId = 0");
            DataTable menuData = db.ExecuteDataSet(menuProc).Tables[0];
            menuProc.Connection.Close();

            return menuData;
        }

        /// <summary>
        /// Retorna um DataReader com o registro correspondente ao id informado no parametro
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public IDataReader LoadMenuByMenuId(int menuId)
        {

            DbCommand dataProc = db.GetSqlStringCommand("SELECT * FROM ag2mngMenu WHERE menuid = @menuid");
            db.AddInParameter(dataProc, "@menuid", DbType.Int32, menuId);

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

            // Cria o objeto de acesso a base de dados
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand menuProc = db.GetStoredProcCommand("MenuGetByParentId");

            //configura parametros da stored procedure
            db.AddInParameter(menuProc, "parentId", DbType.Int32, parentId);
            db.AddInParameter(menuProc, "perfilId", DbType.Int32, perfilId);

            //executa consulta           
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

            //configura parametros da stored procedure
            db.AddInParameter(menuProc, "moduleName", DbType.String, moduleName);
            db.AddInParameter(menuProc, "perfilId", DbType.Int32, perfilId);
            //executa consulta           
            menuData = db.ExecuteDataSet(menuProc).Tables[0];

            //retorno da função
            return menuData;
        }
    }
}
