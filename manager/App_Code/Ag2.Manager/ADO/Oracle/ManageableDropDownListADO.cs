using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Ag2.Manager.ADO.Oracle
{
    /// <summary>
    /// Responsável pelo acesso a dados do controle ManageableDropDownList.
    /// </summary>
    public class ManageableDropDownListADO : BaseADO, Ag2.Manager.DAL.IManageableDropDownListADO
    {
        public ManageableDropDownListADO()
        {
            //
            // TODO: Add constructor logic here
            //            
        }

        public ListItem Save(string tableName, string primeryKey, string fieldDescriptionName, object primaryKeyValue, string fieldDescriptionValue)
        {
            StringBuilder sbQuery = new StringBuilder();

            if (primaryKeyValue == null)
            {
                sbQuery.AppendFormat(" INSERT INTO {0} ", tableName);
                sbQuery.AppendFormat(" ({0}) ", fieldDescriptionName);
                sbQuery.Append(" VALUES ");
                sbQuery.AppendFormat(" (:{0}) ", fieldDescriptionName);
            }
            else
            {
                sbQuery.AppendFormat(" UPDATE {0} SET ", tableName);
                sbQuery.AppendFormat(" {0} = :{0} ", fieldDescriptionName);
                sbQuery.Append(" WHERE ");
                sbQuery.AppendFormat(" {0} = :{0} ", primeryKey);
            }

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());

            if (primaryKeyValue != null)
            {
                if (primaryKeyValue is int)
                    db.AddInParameter(cmd, string.Concat(":", primeryKey), DbType.Int32, Convert.ToInt32(primaryKeyValue));
                if (primaryKeyValue is string)
                    db.AddInParameter(cmd, string.Concat(":", primeryKey), DbType.String, primaryKeyValue.ToString());
            }

            db.AddInParameter(cmd, fieldDescriptionName, DbType.String, fieldDescriptionValue);

            db.ExecuteNonQuery(cmd);

            ListItem li = new ListItem();

            if (primaryKeyValue == null)
            {
                sbQuery.Length = 0;
                sbQuery.AppendFormat(" SELECT {0} FROM {1} WHERE ROWNUM <= 1 ORDER BY {0} DESC ", primeryKey, tableName);
                cmd = db.GetSqlStringCommand(sbQuery.ToString());

                li.Value = db.ExecuteScalar(cmd).ToString();
            }
            else
                li.Value = primaryKeyValue.ToString();

            li.Text = fieldDescriptionValue;

            return li;
        }

        public ListItemCollection SelectAll(string tableName, string primeryKey, string fieldDescriptionName)
        {
            ListItemCollection lista = new ListItemCollection();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.AppendFormat(" SELECT {0}, {1} FROM {2} ", primeryKey, fieldDescriptionName, tableName);
            sbQuery.AppendFormat(" ORDER BY {0} ", fieldDescriptionName);

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());
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

            sbQuery.AppendFormat(" DELETE FROM {0} ", tableName);
            sbQuery.AppendFormat(" WHERE {0} = :{0} ", primeryKey);

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());

            if (primaryKeyValue != null)
            {
                if (primaryKeyValue is int)
                    db.AddInParameter(cmd, string.Concat(":", primeryKey), DbType.Int32, Convert.ToInt32(primaryKeyValue));
                if (primaryKeyValue is string)
                    db.AddInParameter(cmd, string.Concat(":", primeryKey), DbType.String, primaryKeyValue.ToString());
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

            sbQuery.AppendFormat(" SELECT count(*) FROM {0} ", tableName);
            sbQuery.AppendFormat(" WHERE {0} = :{0} ", fieldDescriptionName);

            if (primeryKey != null)
            {
                sbQuery.AppendFormat(" AND {0} <> :{0} ", primeryKey);
            }

            DbCommand cmd = db.GetSqlStringCommand(sbQuery.ToString());
            if (primeryKey != null)
            {
                db.AddInParameter(cmd, string.Concat(":", primeryKey.ToString()), DbType.Int32, Convert.ToInt32(primaryKeyValue));
            }

            db.AddInParameter(cmd, string.Concat(":", fieldDescriptionName.ToString()), DbType.String, fieldDescriptionValue);

            int count = Convert.ToInt32(db.ExecuteScalar(cmd));

            bool retorno = false;

            if (count > 0)
                retorno = true;

            return retorno;
        }


    }
}
