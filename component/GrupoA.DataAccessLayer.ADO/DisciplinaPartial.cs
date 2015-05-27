
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
using GrupoA.BusinessObject.ViewHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class DisciplinaADO : ADOSuper, IDisciplinaDAL
    {

        public List<DisciplinaCursoVH> CarregarDisciplinasComCursosPorInstituicoesProfessor(ProfessorInstituicao entidade)
        {

            List<DisciplinaCursoVH> disciplinasCurso = new List<DisciplinaCursoVH>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT d.descricao nomeDisciplina, c.nome nomeCurso, cn.nivel nivelCurso, pd.numeroAlunos ");
            sbSQL.Append(" FROM  Disciplina d ");
            sbSQL.Append(" JOIN ProfessorDisciplina pd ON pd.disciplinaId = d.disciplinaId ");
            sbSQL.Append(" JOIN ProfessorCurso pc ON pc.professorCursoId = pd.professorCursoId AND pc.professorInstituicaoId = @professorInstituicaoId ");
            sbSQL.Append(" JOIN Curso c ON c.cursoId = pc.cursoId ");
            sbSQL.Append(" JOIN CursoNivel cn ON cn.cursoNivelId = pc.cursoNivelId ");
            sbSQL.Append(" ORDER BY d.descricao ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                DisciplinaCursoVH disciplinaCurso = new DisciplinaCursoVH();
                disciplinaCurso.NomeDisciplina = reader["nomeDisciplina"].ToString();
                disciplinaCurso.NomeCurso = reader["nomeCurso"].ToString();
                disciplinaCurso.NivelCurso = reader["nivelCurso"].ToString();
                disciplinaCurso.NumeroAlunos = reader["numeroAlunos"].ToString();
                disciplinasCurso.Add(disciplinaCurso);

            }
            reader.Close();

            return disciplinasCurso;
        }

        public Disciplina Carregar(String nomeDisciplina)
        {
            Disciplina entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Disciplina WHERE descricao=@descricao");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@descricao", DbType.String, nomeDisciplina);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Disciplina();
                PopulaDisciplina(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
