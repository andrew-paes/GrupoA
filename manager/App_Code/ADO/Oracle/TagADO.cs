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

namespace Ag2.Manager.ADO.Oracle
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
            throw new NotImplementedException();
        }

        private void PopulaTag(Ag2.Manager.Entity.Tag tag, DataRow dr)
        {
            tag.tagId = Convert.ToInt32(dr["tagId"]);
            tag.nome = dr["nome"].ToString();
        }
    }
}
