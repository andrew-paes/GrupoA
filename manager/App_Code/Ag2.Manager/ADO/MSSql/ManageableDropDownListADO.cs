using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Responsável pelo acesso a dados do controle ManageableDropDownList.
    /// </summary>
    public class ManageableDropDownListADO : BaseADO, Ag2.Manager.DAL.IManageableDropDownListADO
    {
        Ag2.Manager.Helper.CurrentSessions cs = new Ag2.Manager.Helper.CurrentSessions();

        public ManageableDropDownListADO()
        {
            //
            // TODO: Add constructor logic here
            //            
        }

        public ListItem Save(string tableName, string primeryKey, string fieldDescriptionName, object primaryKeyValue, string fieldDescriptionValue)
        {
            StringBuilder sbQuery = new StringBuilder();
            DbCommand cmd = null;
            int currentId = 0;

            if (primaryKeyValue == null)
            {
                if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
                {
                    sbQuery.AppendFormat(" INSERT INTO {0} DEFAULT VALUES; SET @currentId = SCOPE_IDENTITY(); ", tableName);
                    cmd = db.GetSqlStringCommand(sbQuery.ToString());
                    db.AddOutParameter(cmd, "@currentId", DbType.Int32, 8);
                    db.ExecuteNonQuery(cmd);

                    currentId = Convert.ToInt32(db.GetParameterValue(cmd, "@currentId").ToString());
                }

                sbQuery.Length = 0;
                sbQuery.AppendFormat(" INSERT INTO {0}{1} ", tableName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "_idioma" : string.Empty);
                sbQuery.AppendFormat(" ({0}{1}) ", fieldDescriptionName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? string.Concat(", idiomaId", ",", primeryKey) : string.Empty);
                sbQuery.Append(" VALUES ");
                sbQuery.AppendFormat(" (@{0}{1}) ", fieldDescriptionName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? string.Concat(", @idiomaId", ", @", primeryKey) : string.Empty);
                sbQuery.AppendFormat(" ; SET @currentId = SCOPE_IDENTITY(); ", primeryKey);
            }
            else
            {
                sbQuery.AppendFormat(" UPDATE {0}{1} SET ", tableName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "_idioma" : string.Empty);
                sbQuery.AppendFormat(" {0} = @{0} ", fieldDescriptionName);
                sbQuery.Append(" WHERE ");
                sbQuery.AppendFormat(" {0} = @{0} {1}", primeryKey, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "AND idiomaId = @idiomaId" : string.Empty);
            }

            cmd = db.GetSqlStringCommand(sbQuery.ToString());

            if (primaryKeyValue != null)
            {
                if (primaryKeyValue is int)
                    db.AddInParameter(cmd, primeryKey, DbType.Int32, Convert.ToInt32(primaryKeyValue));
                if (primaryKeyValue is string)
                    db.AddInParameter(cmd, primeryKey, DbType.String, primaryKeyValue.ToString());
            }
            else
            {
                db.AddOutParameter(cmd, "@currentId", DbType.Int32, 8);
            }

            db.AddInParameter(cmd, fieldDescriptionName, DbType.String, fieldDescriptionValue);

            if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
            {
                if (primaryKeyValue == null)
                {
                    db.AddInParameter(cmd, primeryKey, DbType.Int32, currentId);
                }
                db.AddInParameter(cmd, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId);
            }

            db.ExecuteNonQuery(cmd);

            ListItem li = new ListItem();

            if (primaryKeyValue == null)
                if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
                    li.Value = currentId.ToString();
                else
                    li.Value = db.GetParameterValue(cmd, "@currentId").ToString();
            else
                li.Value = primaryKeyValue.ToString();

            li.Text = fieldDescriptionValue;

            return li;
        }

        public ListItemCollection SelectAll(string tableName, string primeryKey, string fieldDescriptionName)
        {
            ListItemCollection lista = new ListItemCollection();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.AppendFormat(" SELECT {0}, {1} FROM {2}{3} ",
                primeryKey,
                fieldDescriptionName,
                tableName,
                cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "_idioma" : string.Empty);

            if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
            {
                sbQuery.Append(" WHERE idiomaId = @idiomaId ");
            }

            sbQuery.AppendFormat(" ORDER BY {0} ", fieldDescriptionName);

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());
            if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
            {
                db.AddInParameter(cmd, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId.ToString());
            }

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count > 0)
            {
                ListItem li = null;
                foreach (DataRow dr in dt.Rows)
                {
                    li = new ListItem(dr[1].ToString(), dr[0].ToString());
                    lista.Add(li);
                }
            }

            return lista;
        }

        public bool Delete(string tableName, string primeryKey, object primaryKeyValue)
        {
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.AppendFormat(" DELETE FROM {0}{1} ", tableName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "_idioma" : string.Empty);
            sbQuery.AppendFormat(" WHERE {0} = @{0} {1} ", primeryKey, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "AND idiomaId = @idiomaId" : string.Empty);

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());

            if (primaryKeyValue != null)
            {
                if (primaryKeyValue is int)
                    db.AddInParameter(cmd, primeryKey, DbType.Int32, Convert.ToInt32(primaryKeyValue));
                if (primaryKeyValue is string)
                    db.AddInParameter(cmd, primeryKey, DbType.String, primaryKeyValue.ToString());
            }

            if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
            {
                db.AddInParameter(cmd, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId.ToString());
            }

            try
            {
                db.ExecuteNonQuery(cmd);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RegisterExist(string tableName, string primeryKey, string primaryKeyValue, string fieldDescriptionName, string fieldDescriptionValue)
        {
            ListItemCollection lista = new ListItemCollection();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.AppendFormat(" SELECT count(*) FROM {0}{1} ", tableName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "_idioma" : string.Empty);
            sbQuery.AppendFormat(" WHERE {0} = @{0} {1}", fieldDescriptionName, cs.ActiveManagerModule.ModuleStructure.Multilanguage == true ? "AND idiomaId = @idiomaId" : string.Empty);

            if (primeryKey != null)
            {
                sbQuery.AppendFormat(" AND {0} <> @{0} ", primeryKey);
            }

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());
            if (primeryKey != null)
            {
                db.AddInParameter(cmd, primeryKey.ToString(), DbType.Int32, Convert.ToInt32(primaryKeyValue));
            }

            db.AddInParameter(cmd, fieldDescriptionName.ToString(), DbType.String, fieldDescriptionValue);

            if (cs.ActiveManagerModule.ModuleStructure.Multilanguage)
            {
                db.AddInParameter(cmd, "@idiomaId", DbType.Int32, cs.CurrentIdioma.IdiomaId);
            }

            int count = Convert.ToInt32(db.ExecuteScalar(cmd));

            bool retorno = false;

            if (count > 0)
                retorno = true;

            return retorno;
        }


    }
}
