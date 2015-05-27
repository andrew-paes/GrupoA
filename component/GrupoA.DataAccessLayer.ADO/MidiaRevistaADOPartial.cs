using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class MidiaRevistaADO : ADOSuper, IMidiaRevistaDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirTodosPorMidia(Midia entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM MidiaRevista ");
            sbSQL.Append("WHERE midiaId=@midiaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);


            _db.ExecuteNonQuery(command);
        }
    }
}
