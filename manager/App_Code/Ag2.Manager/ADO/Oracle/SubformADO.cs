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

namespace Ag2.Manager.ADO.Oracle
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

            sbSQL.Append(" SELECT * FROM noticia ");

            DbCommand dataProc = db.GetSqlStringCommand(sbSQL.ToString());
            ds = db.ExecuteDataSet(dataProc);

            if (ds.Tables[0].Rows.Count > 0)
            {
            
            }
            
            dataProc.Connection.Close();

            return ds;
        }

    }
}
