
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
    public partial class CursoADO : ADOSuper, ICursoDAL
    {


        /// <summary>
        /// Método que retorna uma coleção de Curso.
        /// </summary>
        /// <param name="entidade">ProfessorCurso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Curso.</returns>
        public IEnumerable<Curso> CarregarPorProfessor(Professor professor)
        {
            List<Curso> cursos = new List<Curso>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Curso.*, Instituicao.*,CursoNivel.*, Arquivo.* FROM Professor ");
            sbSQL.Append(" JOIN ProfessorInstituicao ON ProfessorInstituicao.ProfessorId = Professor.ProfessorId AND Professor.ProfessorId = @professorId");
            sbSQL.Append(" JOIN ProfessorCurso ON ProfessorCurso.ProfessorInstituicaoId = ProfessorInstituicao.ProfessorInstituicaoId");
            sbSQL.Append(" JOIN Instituicao ON Instituicao.InstituicaoId = ProfessorInstituicao.InstituicaoId");
            sbSQL.Append(" JOIN Curso ON Curso.CursoId = ProfessorCurso.CursoId");
            sbSQL.Append(" LEFT JOIN ProfessorComprovanteDocencia ON ProfessorComprovanteDocencia.ProfessorId = Professor.ProfessorId AND ProfessorComprovanteDocencia.InstituicaoId = ProfessorInstituicao.InstituicaoId");
            sbSQL.Append(" LEFT JOIN Arquivo ON Arquivo.ArquivoId = ProfessorComprovanteDocencia.ArquivoId");
            sbSQL.Append(" JOIN CursoNivel ON CursoNivel.CursoNivelId = ProfessorCurso.CursoNivelId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorId", DbType.Int32, professor.ProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Curso curso = new Curso();
                PopulaCurso(reader, curso);
                cursos.Add(curso);
            }
            reader.Close();

            return cursos;

        }

        public Curso Carregar(String nomeCurso)
        {
            Curso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Curso WHERE nome=@nome");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@nome", DbType.String, nomeCurso);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Curso();
                PopulaCurso(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}