using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class ProfessorComprovanteDocenciaADO : ADOSuper, IProfessorComprovanteDocenciaDAL
    {
        public void ExcluirPorProfessorEInstituicao(Int64 professorId, Int64 instituicaoId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProfessorComprovanteDocencia ");
            sbSQL.Append("WHERE professorId=@professorId AND instituicaoId=@instituicaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int64, professorId);
            _db.AddInParameter(command, "@instituicaoId", DbType.Int64, instituicaoId);

            _db.ExecuteNonQuery(command);
        }
    }
}
