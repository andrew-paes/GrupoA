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
    public partial class ProfessorDisciplinaADO : ADOSuper, IProfessorDisciplinaDAL
    {
        /// <summary>
        /// Método que persiste um ProfessorDisciplina.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina contendo os dados a serem persistidos.</param>	
        public void Inserir(ProfessorDisciplina entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProfessorDisciplina ");
            sbSQL.Append(" (numeroAlunos, professorCursoId, disciplinaId, indicaTitulo) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@numeroAlunos, @professorCursoId, @disciplinaId, @indicaTitulo) ");

            sbSQL.Append(" ; SET @professorDisciplinaId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@professorDisciplinaId", DbType.Int32, 8);

            _db.AddInParameter(command, "@numeroAlunos", DbType.Int32, entidade.NumeroAlunos);

            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCurso.ProfessorCursoId);

            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, entidade.Disciplina.DisciplinaId);

            _db.AddInParameter(command, "@indicaTitulo", DbType.Int32, entidade.IndicaTitulo);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.ProfessorDisciplinaId = Convert.ToInt32(_db.GetParameterValue(command, "@professorDisciplinaId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um ProfessorDisciplina.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina contendo os dados a serem atualizados.</param>
        public void Atualizar(ProfessorDisciplina entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE ProfessorDisciplina SET ");
            sbSQL.Append(" numeroAlunos=@numeroAlunos, professorCursoId=@professorCursoId, disciplinaId=@disciplinaId, indicaTitulo=@indicaTitulo ");
            sbSQL.Append(" WHERE professorDisciplinaId=@professorDisciplinaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@professorDisciplinaId", DbType.Int32, entidade.ProfessorDisciplinaId);
            _db.AddInParameter(command, "@numeroAlunos", DbType.Int32, entidade.NumeroAlunos);
            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCurso.ProfessorCursoId);
            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, entidade.Disciplina.DisciplinaId);
            _db.AddInParameter(command, "@indicaTitulo", DbType.Int32, entidade.IndicaTitulo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um ProfessorDisciplina da base de dados.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(ProfessorDisciplina entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProfessorDisciplina ");
            sbSQL.Append("WHERE professorDisciplinaId=@professorDisciplinaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorDisciplinaId", DbType.Int32, entidade.ProfessorDisciplinaId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um ProfessorDisciplina.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfessorDisciplina</returns>
        public ProfessorDisciplina Carregar(int professorDisciplinaId)
        {
            ProfessorDisciplina entidade = new ProfessorDisciplina();
            entidade.ProfessorDisciplinaId = professorDisciplinaId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um ProfessorDisciplina.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfessorDisciplina</returns>
        public ProfessorDisciplina Carregar(ProfessorDisciplina entidade)
        {

            ProfessorDisciplina entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfessorDisciplina WHERE professorDisciplinaId=@professorDisciplinaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorDisciplinaId", DbType.Int32, entidade.ProfessorDisciplinaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorDisciplina();
                PopulaProfessorDisciplina(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de ProfessorDisciplina.
        /// </summary>
        /// <param name="entidade">Disciplina relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorDisciplina.</returns>
        public IEnumerable<ProfessorDisciplina> Carregar(Disciplina entidade)
        {
            List<ProfessorDisciplina> entidadesRetorno = new List<ProfessorDisciplina>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorDisciplina.* FROM ProfessorDisciplina WHERE ProfessorDisciplina.disciplinaId=@disciplinaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, entidade.DisciplinaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorDisciplina entidadeRetorno = new ProfessorDisciplina();
                PopulaProfessorDisciplina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorDisciplina.
        /// </summary>
        /// <param name="entidade">ProfessorCurso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorDisciplina.</returns>
        public IEnumerable<ProfessorDisciplina> Carregar(ProfessorCurso entidade)
        {
            List<ProfessorDisciplina> entidadesRetorno = new List<ProfessorDisciplina>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorDisciplina.* FROM ProfessorDisciplina WHERE ProfessorDisciplina.professorCursoId=@professorCursoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorDisciplina entidadeRetorno = new ProfessorDisciplina();
                PopulaProfessorDisciplina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de ProfessorDisciplina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ProfessorDisciplina.</returns>
        public IEnumerable<ProfessorDisciplina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ProfessorDisciplina> entidadesRetorno = new List<ProfessorDisciplina>();

            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StringBuilder sbOrder = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            // Monta o "OrderBy"
            if (ordemColunas != null)
            {
                for (int i = 0; i < ordemColunas.Length; i++)
                {
                    if (sbOrder.Length > 0) { sbOrder.Append(", "); }
                    sbOrder.Append(ordemColunas[i] + " " + ordemSentidos[i]);
                }
                if (sbOrder.Length > 0) { sbOrder.Insert(0, " ORDER BY "); }
            }
            else
            {
                sbOrder.Append(" ORDER BY professorDisciplinaId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProfessorDisciplina");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorDisciplina WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorDisciplina ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ProfessorDisciplina.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProfessorDisciplina ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ProfessorDisciplina.* FROM ProfessorDisciplina ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorDisciplina entidadeRetorno = new ProfessorDisciplina();
                PopulaProfessorDisciplina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os ProfessorDisciplina existentes na base de dados.
        /// </summary>
        public IEnumerable<ProfessorDisciplina> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de ProfessorDisciplina na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de ProfessorDisciplina na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM ProfessorDisciplina");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um ProfessorDisciplina baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProfessorDisciplina a ser populado(.</param>
        public static void PopulaProfessorDisciplina(IDataReader reader, ProfessorDisciplina entidade)
        {
            if (reader["professorDisciplinaId"] != DBNull.Value)
                entidade.ProfessorDisciplinaId = Convert.ToInt32(reader["professorDisciplinaId"].ToString());

            if (reader["numeroAlunos"] != DBNull.Value)
                entidade.NumeroAlunos = Convert.ToInt32(reader["numeroAlunos"].ToString());

            if (reader["indicaTitulo"] != DBNull.Value)
                entidade.IndicaTitulo = Convert.ToBoolean(reader["indicaTitulo"].ToString());

            if (reader["professorCursoId"] != DBNull.Value)
            {
                entidade.ProfessorCurso = new ProfessorCurso();
                entidade.ProfessorCurso.ProfessorCursoId = Convert.ToInt32(reader["professorCursoId"].ToString());
            }

            if (reader["disciplinaId"] != DBNull.Value)
            {
                entidade.Disciplina = new Disciplina();
                entidade.Disciplina.DisciplinaId = Convert.ToInt32(reader["disciplinaId"].ToString());
            }


        }

    }
}
