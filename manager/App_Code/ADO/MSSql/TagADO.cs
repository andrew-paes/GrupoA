using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Ag2.Manager.DAL;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Helper;
using System.Web.Security;
using System.Data.Common;
using System.Data;

namespace Ag2.Manager.ADO.MSSql
{
    /// <summary>
    /// Summary description for UploadBrowser
    /// </summary>
    public class TagADO : BaseADO, ITagDAL
    {
        public TagADO()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        public Ag2.Manager.Entity.Tag Save(Ag2.Manager.Entity.Tag tag)
        {
            DbCommand cmdArquivo = db.GetStoredProcCommand("[tagInsert]");
            db.AddOutParameter(cmdArquivo, "@tagId", DbType.Int32, 0);
            db.AddInParameter(cmdArquivo, "@nome", DbType.String, tag.nome);

            db.ExecuteNonQuery(cmdArquivo);

            tag.tagId = Convert.ToInt32(db.GetParameterValue(cmdArquivo, "@tagId"));

            cmdArquivo.Connection.Close();

            return tag;
        }

        private void PopulaTag(Ag2.Manager.Entity.Tag tag, DataRow dr)
        {
            tag.tagId = Convert.ToInt32(dr["tagId"]);
            tag.nome = dr["nome"].ToString();
        }
    }
}
