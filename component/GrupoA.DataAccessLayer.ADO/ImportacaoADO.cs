using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using GrupoA.DataAccess;

namespace GrupoA.DataAccess.ADO
{
    public class ImportacaoADO : ADOSuper, IImportacaoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void InserirLote(string sql)
        {
            DbCommand command;

            command = _db.GetSqlStringCommand(sql);

            _db.ExecuteNonQuery(command);

        }
    }
}
