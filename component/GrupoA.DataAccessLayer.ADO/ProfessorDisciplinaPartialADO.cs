
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
	public partial class ProfessorDisciplinaADO : ADOSuper, IProfessorDisciplinaDAL {
	
	    /// <summary>
        /// Método que remove um ProfessorDisciplina conforme o professor recebido.
        /// </summary>
        /// <param name="professorInstituicao">ProfessorDisciplina a ser excluído (somente o identificador é necessário).</param>		
		public void ExcluirPorProfessorInstituicao(ProfessorInstituicao professorInstituicao)
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

            sbSQL.Append("DELETE FROM ProfessorDisciplina  ");
            sbSQL.Append("WHERE ProfessorDisciplina.professorDisciplinaId IN ( ");
            sbSQL.Append(" SELECT ProfessorDisciplinaDel.ProfessorDisciplinaId FROM ProfessorDisciplina ProfessorDisciplinaDel");
            sbSQL.Append(" JOIN ProfessorCurso ON ProfessorCurso.ProfessorCursoId = ProfessorDisciplinaDel.ProfessorCursoId ");
            sbSQL.Append(" JOIN ProfessorInstituicao ON ProfessorInstituicao.ProfessorInstituicaoId = ProfessorCurso.ProfessorInstituicaoId ");
            sbSQL.Append(" AND ProfessorInstituicao.ProfessorInstituicaoId = @ProfessorInstituicaoId ");
            sbSQL.Append(" AND ProfessorInstituicao.ProfessorId = @professorId) ");
			
            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ProfessorInstituicaoId", DbType.Int32, professorInstituicao.ProfessorInstituicaoId);
            _db.AddInParameter(command, "@professorId", DbType.Int32, professorInstituicao.Professor.ProfessorId);

								
			_db.ExecuteNonQuery(command);
		}

        public ProfessorDisciplina CarregarPorProfessorCursoDisciplina(ProfessorCurso professorCursoBO, Disciplina disciplinaBO)
        {
            ProfessorDisciplina entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfessorDisciplina WHERE professorCursoId=@professorCursoId AND disciplinaId = @disciplinaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, professorCursoBO.ProfessorCursoId);
            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, disciplinaBO.DisciplinaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorDisciplina();
                PopulaProfessorDisciplina(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
	}
}
		