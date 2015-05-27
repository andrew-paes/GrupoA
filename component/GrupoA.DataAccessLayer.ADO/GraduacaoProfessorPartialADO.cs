
/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
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
	public partial class GraduacaoProfessorADO : ADOSuper, IGraduacaoProfessorDAL
    {

        #region public GraduacaoProfessor CarregarGraduacaoCompletaPorProfessor(Professor entidade)
        /// <summary>
        /// Carrega a graduação do Professor conforme o código identificador recebido
        /// </summary>
        /// <param name="entidade">Objeto Professor que contém o identificador professorId</param>
        /// <returns>Objeto GraduacaoProfessor do Professor</returns>
        public GraduacaoProfessor CarregarGraduacaoCompletaPorProfessor(Professor entidade)
        {
            GraduacaoProfessor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT gp.* FROM Professor p ");
            sbSQL.Append(" INNER JOIN GraduacaoProfessor gp ON gp.graduacaoProfessorId = p.graduacaoProfessorId ");
            sbSQL.Append(" WHERE p.professorId=@professorId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new GraduacaoProfessor();
                PopulaGraduacaoProfessor(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

    }
}
		