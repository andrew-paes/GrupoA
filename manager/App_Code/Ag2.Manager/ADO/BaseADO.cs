using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;


namespace Ag2.Manager.ADO
{
    public class BaseADO
    {
        public Database db = DatabaseFactory.CreateDatabase();

        public BaseADO()
        {
        }
    }
}
