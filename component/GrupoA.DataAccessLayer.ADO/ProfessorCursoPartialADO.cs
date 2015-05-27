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
    public partial class ProfessorCursoADO : ADOSuper, IProfessorCursoDAL
    {
        /// <summary>
        /// Método que remove um ProfessorCurso da base de dados.
        /// </summary>
        /// <param name="entidade">ProfessorCurso a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirPorProfessorInstituicao(ProfessorInstituicao professorInstituicao)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProfessorCurso WHERE ProfessorCurso.ProfessorCursoId IN ( ");
            sbSQL.Append("SELECT ProfessorCursoDel.ProfessorCursoId FROM ProfessorCurso ProfessorCursoDel ");
            sbSQL.Append("JOIN Curso ON Curso.CursoId = ProfessorCursoDel.CursoId ");
            sbSQL.Append("JOIN ProfessorInstituicao ON ProfessorInstituicao.ProfessorInstituicaoId = ProfessorCurso.ProfessorInstituicaoId ");
            sbSQL.Append("AND ProfessorInstituicao.ProfessorInstituicaoId = @ProfessorInstituicaoId ) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ProfessorInstituicaoId", DbType.Int32, professorInstituicao.ProfessorInstituicaoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professorInstituicaoBO"></param>
        /// <param name="cursoBO"></param>
        /// <returns></returns>
        public ProfessorCurso CarregarPorProfessorInstituicaoCurso(ProfessorInstituicao professorInstituicaoBO, Curso cursoBO)
        {
            ProfessorCurso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfessorCurso WHERE professorInstituicaoId=@professorInstituicaoId AND cursoId = @cursoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, professorInstituicaoBO.ProfessorInstituicaoId);
            _db.AddInParameter(command, "@cursoId", DbType.Int32, cursoBO.CursoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}