using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.DataAccess;
using System.Data.Common;

namespace GrupoA.DataAccessLayer.ADO
{
    public class ImportacaoProdutos : ADOSuper
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
