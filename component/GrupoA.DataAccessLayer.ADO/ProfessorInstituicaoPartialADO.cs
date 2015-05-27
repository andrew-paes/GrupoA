using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class ProfessorInstituicaoADO : ADOSuper, IProfessorInstituicaoDAL
    {

        /// <summary>
        /// Método que retorna uma coleção de ProfessorInstituicao populando instituições, curos e disciplinas.
        /// </summary>
        /// <param name="entidade">Professor relacionado(a) (somente o identificador é necessário).</param>
        /// <param name="professorInstituicaoIdsRemovidos"></param>
        /// <returns>Retorna uma coleção de ProfessorInstituicao.</returns>
        public IEnumerable<ProfessorInstituicao> CarregarComDependenciasPorProfessor(Professor entidade, String professorInstituicaoIdsRemovidos)
        {
            List<ProfessorInstituicao> entidadesRetorno = new List<ProfessorInstituicao>();
            String[] instituicoesRemovidas = null;
            StringBuilder sbSQL = new StringBuilder();

            if (!String.IsNullOrEmpty(professorInstituicaoIdsRemovidos))
            {
                instituicoesRemovidas = professorInstituicaoIdsRemovidos.Split('|');
            }

            sbSQL.Append(" SELECT *");
            sbSQL.Append(" FROM ProfessorInstituicao");
            sbSQL.Append(" JOIN Instituicao ON ProfessorInstituicao.InstituicaoId = Instituicao.InstituicaoId");
            sbSQL.Append(" LEFT JOIN professorcomprovantedocencia ON professorcomprovantedocencia.InstituicaoId = Instituicao.InstituicaoId AND professorcomprovantedocencia.professorId = @professorId");
            sbSQL.Append(" JOIN ProfessorCurso ON ProfessorCurso.ProfessorInstituicaoId = ProfessorInstituicao.ProfessorInstituicaoId");
            sbSQL.Append(" JOIN CursoNivel ON CursoNivel.CursoNivelId = ProfessorCurso.CursoNivelId");
            sbSQL.Append(" JOIN Curso ON Curso.CursoId = ProfessorCurso.CursoId");
            sbSQL.Append(" JOIN ProfessorDisciplina ON ProfessorDisciplina.ProfessorCursoId = ProfessorCurso.ProfessorCursoId");
            sbSQL.Append(" JOIN Disciplina ON Disciplina.DisciplinaId = ProfessorDisciplina.DisciplinaId");
            sbSQL.Append(" WHERE ProfessorInstituicao.professorId=@professorId");

            if (instituicoesRemovidas != null && instituicoesRemovidas.Length > 0)
            {
                sbSQL.Append("	AND ProfessorInstituicao.professorInstituicaoId NOT IN (");

                String parametros = String.Empty;

                for (int i = 0; i < instituicoesRemovidas.Length; i++)
                {
                    parametros += String.Concat("@professorInstituicaoId", i, ",");
                }

                sbSQL.Append(parametros.Substring(0, parametros.Length - 1));
                sbSQL.Append(")");
            }

            sbSQL.Append(" ORDER BY Instituicao.nomeInstituicao, Curso.Nome");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            if (instituicoesRemovidas != null && instituicoesRemovidas.Length > 0)
            {
                for (int i = 0; i < instituicoesRemovidas.Length; i++)
                {
                    _db.AddInParameter(command, String.Concat("@professorInstituicaoId", i), DbType.Int32, Convert.ToInt32(instituicoesRemovidas[i].ToString()));
                }
            }

            IDataReader reader = _db.ExecuteReader(command);

            ProfessorInstituicao professorInstituicaoAnterior = new ProfessorInstituicao();
            ProfessorInstituicao professorInstituicaoAtual = new ProfessorInstituicao();
            professorInstituicaoAtual.ProfessorCursos = new List<ProfessorCurso>();

            ProfessorCurso professorCursoAnterior = new ProfessorCurso();
            ProfessorCurso professorCursoAtual = new ProfessorCurso();
            professorCursoAtual.ProfessorDisciplinas = new List<ProfessorDisciplina>();
            while (reader.Read())
            {
                // Professor Instituição e Instituição
                // se for uma nova instituicao
                if (Convert.ToInt32(reader["professorInstituicaoId"]) != professorInstituicaoAnterior.ProfessorInstituicaoId)
                {
                    // Se não for a 1a vez
                    professorInstituicaoAtual = new ProfessorInstituicao();
                    PopulaProfessorInstituicao(reader, professorInstituicaoAtual);
                    InstituicaoADO.PopulaInstituicao(reader, professorInstituicaoAtual.Instituicao);
                    if (reader["professorcomprovantedocenciaid"] != null)
                    {
                        professorInstituicaoAtual.Instituicao.ProfessorComprovanteDocencias = new List<ProfessorComprovanteDocencia>();
                        ProfessorComprovanteDocencia comprovante = new ProfessorComprovanteDocencia();
                        ProfessorComprovanteDocenciaADO.PopulaProfessorComprovanteDocencia(reader, comprovante);
                        professorInstituicaoAtual.Instituicao.ProfessorComprovanteDocencias.Add(comprovante);
                    }
                    professorInstituicaoAtual.ProfessorCursos = new List<ProfessorCurso>();
                    entidadesRetorno.Add(professorInstituicaoAtual);
                    professorInstituicaoAnterior = new ProfessorInstituicao() { ProfessorInstituicaoId = professorInstituicaoAtual.ProfessorInstituicaoId };
                }

                // ProfessorCurso e Curso
                if ((professorInstituicaoAtual.ProfessorCursos.Count == 0) ||
                    (Convert.ToInt32(reader["professorCursoId"]) != professorCursoAnterior.ProfessorCursoId))
                {
                    professorCursoAtual = new ProfessorCurso();
                    ProfessorCursoADO.PopulaProfessorCurso(reader, professorCursoAtual);
                    CursoADO.PopulaCurso(reader, professorCursoAtual.Curso);
                    CursoNivelADO.PopulaCursoNivel(reader, professorCursoAtual.CursoNivel);
                    professorCursoAtual.ProfessorDisciplinas = new List<ProfessorDisciplina>();
                    professorInstituicaoAtual.ProfessorCursos.Add(professorCursoAtual);
                    professorCursoAnterior = new ProfessorCurso() { ProfessorCursoId = professorCursoAtual.ProfessorCursoId };
                }

                // Professor Disciplina e Disciplina
                ProfessorDisciplina professorDisciplina = new ProfessorDisciplina();
                ProfessorDisciplinaADO.PopulaProfessorDisciplina(reader, professorDisciplina);
                DisciplinaADO.PopulaDisciplina(reader, professorDisciplina.Disciplina);

                professorCursoAtual.ProfessorDisciplinas.Add(professorDisciplina);

            }

            reader.Close();
            // o último não foi inserido do laço, deve ser inserido aqui
            //if (professorCursoAtual != new ProfessorCurso())
            //{
            //    professorInstituicaoAtual.ProfessorCursos.Add(professorCursoAtual);
            //}
            //if (professorInstituicaoAtual != new ProfessorInstituicao())
            //{
            //    entidadesRetorno.Add(professorInstituicaoAtual);
            //}

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professorBO"></param>
        /// <param name="instituicaoBO"></param>
        /// <returns></returns>
        public bool ValidarProfessorInstituicaoUnico(Professor professorBO, Instituicao instituicaoBO)
        {
            int resultado = 0;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) ");
            sbSQL.Append("FROM ProfessorInstituicao ");
            sbSQL.Append("WHERE professorId = @professorId AND instituicaoId = @instituicaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.String, professorBO.ProfessorId);
            _db.AddInParameter(command, "@instituicaoId", DbType.String, instituicaoBO.InstituicaoId);

            resultado = (int)_db.ExecuteScalar(command);

            return (resultado > 0 ? false : true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="professorBO"></param>
        /// <param name="instituicaoBO"></param>
        /// <returns></returns>
        public ProfessorInstituicao CarregarPorProfessorInstituicao(Professor professorBO, Instituicao instituicaoBO)
        {
            ProfessorInstituicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfessorInstituicao WHERE professorId = @professorId AND instituicaoId=@instituicaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, professorBO.ProfessorId);
            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, instituicaoBO.InstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        public IEnumerable<ProfessorInstituicao> CarregarComDependenciasPorProfessorInstituicoesIds(Professor entidade, String professorInstituicaoIdsRemovidos)
        {
            List<ProfessorInstituicao> entidadesRetorno = new List<ProfessorInstituicao>();
            String[] instituicoesRemovidas = null;
            StringBuilder sbSQL = new StringBuilder();

            if (!String.IsNullOrEmpty(professorInstituicaoIdsRemovidos))
            {
                instituicoesRemovidas = professorInstituicaoIdsRemovidos.Split('|');
            }

            sbSQL.Append(" SELECT *");
            sbSQL.Append(" FROM ProfessorInstituicao");
            sbSQL.Append(" JOIN Instituicao ON ProfessorInstituicao.InstituicaoId = Instituicao.InstituicaoId");
            sbSQL.Append(" LEFT JOIN professorcomprovantedocencia ON professorcomprovantedocencia.InstituicaoId = Instituicao.InstituicaoId AND professorcomprovantedocencia.professorId = @professorId");
            sbSQL.Append(" JOIN ProfessorCurso ON ProfessorCurso.ProfessorInstituicaoId = ProfessorInstituicao.ProfessorInstituicaoId");
            sbSQL.Append(" JOIN CursoNivel ON CursoNivel.CursoNivelId = ProfessorCurso.CursoNivelId");
            sbSQL.Append(" JOIN Curso ON Curso.CursoId = ProfessorCurso.CursoId");
            sbSQL.Append(" JOIN ProfessorDisciplina ON ProfessorDisciplina.ProfessorCursoId = ProfessorCurso.ProfessorCursoId");
            sbSQL.Append(" JOIN Disciplina ON Disciplina.DisciplinaId = ProfessorDisciplina.DisciplinaId");
            sbSQL.Append(" WHERE ProfessorInstituicao.professorId=@professorId");

            if (instituicoesRemovidas != null && instituicoesRemovidas.Length > 0)
            {
                sbSQL.Append("	AND ProfessorInstituicao.professorInstituicaoId IN (");

                String parametros = String.Empty;

                for (int i = 0; i < instituicoesRemovidas.Length; i++)
                {
                    parametros += String.Concat("@professorInstituicaoId", i, ",");
                }

                sbSQL.Append(parametros.Substring(0, parametros.Length - 1));
                sbSQL.Append(")");
            }

            sbSQL.Append(" ORDER BY Instituicao.nomeInstituicao, Curso.Nome");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            if (instituicoesRemovidas != null && instituicoesRemovidas.Length > 0)
            {
                for (int i = 0; i < instituicoesRemovidas.Length; i++)
                {
                    _db.AddInParameter(command, String.Concat("@professorInstituicaoId", i), DbType.Int32, Convert.ToInt32(instituicoesRemovidas[i].ToString()));
                }
            }

            IDataReader reader = _db.ExecuteReader(command);

            ProfessorInstituicao professorInstituicaoAnterior = new ProfessorInstituicao();
            ProfessorInstituicao professorInstituicaoAtual = new ProfessorInstituicao();
            professorInstituicaoAtual.ProfessorCursos = new List<ProfessorCurso>();

            ProfessorCurso professorCursoAnterior = new ProfessorCurso();
            ProfessorCurso professorCursoAtual = new ProfessorCurso();
            professorCursoAtual.ProfessorDisciplinas = new List<ProfessorDisciplina>();
            while (reader.Read())
            {
                // Professor Instituição e Instituição
                // se for uma nova instituicao
                if (Convert.ToInt32(reader["professorInstituicaoId"]) != professorInstituicaoAnterior.ProfessorInstituicaoId)
                {
                    // Se não for a 1a vez
                    professorInstituicaoAtual = new ProfessorInstituicao();
                    PopulaProfessorInstituicao(reader, professorInstituicaoAtual);
                    InstituicaoADO.PopulaInstituicao(reader, professorInstituicaoAtual.Instituicao);
                    if (reader["professorcomprovantedocenciaid"] != null)
                    {
                        professorInstituicaoAtual.Instituicao.ProfessorComprovanteDocencias = new List<ProfessorComprovanteDocencia>();
                        ProfessorComprovanteDocencia comprovante = new ProfessorComprovanteDocencia();
                        ProfessorComprovanteDocenciaADO.PopulaProfessorComprovanteDocencia(reader, comprovante);
                        professorInstituicaoAtual.Instituicao.ProfessorComprovanteDocencias.Add(comprovante);
                    }
                    professorInstituicaoAtual.ProfessorCursos = new List<ProfessorCurso>();
                    entidadesRetorno.Add(professorInstituicaoAtual);
                    professorInstituicaoAnterior = new ProfessorInstituicao() { ProfessorInstituicaoId = professorInstituicaoAtual.ProfessorInstituicaoId };
                }

                // ProfessorCurso e Curso
                if ((professorInstituicaoAtual.ProfessorCursos.Count == 0) ||
                    (Convert.ToInt32(reader["professorCursoId"]) != professorCursoAnterior.ProfessorCursoId))
                {
                    professorCursoAtual = new ProfessorCurso();
                    ProfessorCursoADO.PopulaProfessorCurso(reader, professorCursoAtual);
                    CursoADO.PopulaCurso(reader, professorCursoAtual.Curso);
                    CursoNivelADO.PopulaCursoNivel(reader, professorCursoAtual.CursoNivel);
                    professorCursoAtual.ProfessorDisciplinas = new List<ProfessorDisciplina>();
                    professorInstituicaoAtual.ProfessorCursos.Add(professorCursoAtual);
                    professorCursoAnterior = new ProfessorCurso() { ProfessorCursoId = professorCursoAtual.ProfessorCursoId };
                }

                // Professor Disciplina e Disciplina
                ProfessorDisciplina professorDisciplina = new ProfessorDisciplina();
                ProfessorDisciplinaADO.PopulaProfessorDisciplina(reader, professorDisciplina);
                DisciplinaADO.PopulaDisciplina(reader, professorDisciplina.Disciplina);

                professorCursoAtual.ProfessorDisciplinas.Add(professorDisciplina);

            }

            reader.Close();

            return entidadesRetorno;
        }
    }
}