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
    /// Summary description for SubformADO
    /// </summary>
    public class SubformADO : BaseADO, ISubformADO
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SubformADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet CarregaDados(Ag2.Manager.Entity.QueryCommand entity)
        {
            DataSet ds = null;
            StringBuilder sbSQL = new StringBuilder();

            if (entity.WhereIds.Count.Equals(0))
                return null;

            sbSQL.Append(entity.Sql);

            sbSQL.Append(Util.AddWhereOrAnd(sbSQL.ToString()));

            sbSQL.Append(" ( ");

            for (int i = 0; i < entity.WhereIds.Count; i++)
            {
                if (!i.Equals(0))
                    sbSQL.Append(" OR ");
                sbSQL.Append(entity.Fields[0].DataFieldName).Append(" = ").Append(entity.WhereIds[i]);
            }

            sbSQL.Append(" ) ");

            DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());

            if (entity.IdiomaId > 0)
                db.AddInParameter(cmd, "@idiomaId", DbType.Int32, entity.IdiomaId);


            ds = db.ExecuteDataSet(cmd);

            cmd.Connection.Close();

            return ds;
        }

    }
}
