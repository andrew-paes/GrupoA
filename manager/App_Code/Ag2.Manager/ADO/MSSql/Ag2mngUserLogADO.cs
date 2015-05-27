using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Web.Security;
using System.Net;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Summary description for IdiomaADO
    /// </summary>
    public class Ag2mngUserLogADO : BaseADO, Ag2.Manager.DAL.IAg2mngUserLogDAL
    {
        public Ag2mngUserLogADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Atualiza o idioma
        /// </summary>
        /// <param name="idioma"></param>
        public Ag2.Manager.Entity.Ag2mngUserLog CarregaUltimoAcesso(Ag2.Manager.Entity.ag2mngUser user)
        {
            Ag2.Manager.Entity.Ag2mngUserLog log = null;

            DbCommand cmd = db.GetStoredProcCommand("[CarregaUltimoAcesso]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            cmd.Connection.Close();

            if (dt.Rows.Count > 0)
            {
                log = new Ag2.Manager.Entity.Ag2mngUserLog();
                PopulaLog(log, dt.Rows[0]);
            }

            return log;
        }

        public void InserirLogAcesso(Ag2.Manager.Entity.ag2mngUser user)
        {
            DbCommand cmd = db.GetStoredProcCommand("[ag2mngUserLogInsert]");
            db.AddInParameter(cmd, "@userId", DbType.Int32, user.userId);
            db.AddInParameter(cmd, "@dataAcesso", DbType.DateTime, DateTime.Now);

            string host = System.Net.Dns.GetHostName();
            string ip = System.Net.Dns.GetHostByName(host).AddressList[0].ToString();

            db.AddInParameter(cmd, "@ipAcesso", DbType.String, ip);

            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
        }

        private void PopulaLog(Ag2.Manager.Entity.Ag2mngUserLog log, DataRow dr)
        {
            log.UserId = Convert.ToInt32(dr["UserId"].ToString());
            log.dataAcesso = Convert.ToDateTime(dr["dataAcesso"].ToString());
            log.ipAcesso = dr["ipAcesso"].ToString();
        }
    }
}
