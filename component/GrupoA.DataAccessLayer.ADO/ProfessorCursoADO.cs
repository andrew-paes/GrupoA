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
        /// Método que persiste um ProfessorCurso.
        /// </summary>
        /// <param name="entidade">ProfessorCurso contendo os dados a serem persistidos.</param>	
        public void Inserir(ProfessorCurso entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProfessorCurso ");
            sbSQL.Append(" (professorInstituicaoId, cursoNivelId, coordenadorCurso, cursoId, cargo) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@professorInstituicaoId, @cursoNivelId, @coordenadorCurso, @cursoId, @cargo) ");

            sbSQL.Append(" ; SET @professorCursoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@professorCursoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicao.ProfessorInstituicaoId);

            _db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivel.CursoNivelId);

            _db.AddInParameter(command, "@coordenadorCurso", DbType.Int32, entidade.CoordenadorCurso);

            _db.AddInParameter(command, "@cursoId", DbType.Int32, entidade.Curso.CursoId);

            if (entidade.Cargo != null)
                _db.AddInParameter(command, "@cargo", DbType.String, entidade.Cargo);
            else
                _db.AddInParameter(command, "@cargo", DbType.String, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.ProfessorCursoId = Convert.ToInt32(_db.GetParameterValue(command, "@professorCursoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um ProfessorCurso.
        /// </summary>
        /// <param name="entidade">ProfessorCurso contendo os dados a serem atualizados.</param>
        public void Atualizar(ProfessorCurso entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE ProfessorCurso SET ");
            sbSQL.Append(" professorInstituicaoId=@professorInstituicaoId, cursoNivelId=@cursoNivelId, coordenadorCurso=@coordenadorCurso, cursoId=@cursoId, cargo=@cargo ");
            sbSQL.Append(" WHERE professorCursoId=@professorCursoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);
            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicao.ProfessorInstituicaoId);
            _db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivel.CursoNivelId);
            _db.AddInParameter(command, "@coordenadorCurso", DbType.Int32, entidade.CoordenadorCurso);
            _db.AddInParameter(command, "@cursoId", DbType.Int32, entidade.Curso.CursoId);
            if (entidade.Cargo != null)
                _db.AddInParameter(command, "@cargo", DbType.String, entidade.Cargo);
            else
                _db.AddInParameter(command, "@cargo", DbType.String, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um ProfessorCurso da base de dados.
        /// </summary>
        /// <param name="entidade">ProfessorCurso a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(ProfessorCurso entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProfessorCurso ");
            sbSQL.Append("WHERE professorCursoId=@professorCursoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um ProfessorCurso.
        /// </summary>
        /// <param name="entidade">ProfessorCurso a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfessorCurso</returns>
        public ProfessorCurso Carregar(int professorCursoId)
        {
            ProfessorCurso entidade = new ProfessorCurso();
            entidade.ProfessorCursoId = professorCursoId;
            return Carregar(entidade);
        }

        /// <summary>
        /// Método que carrega um ProfessorCurso.
        /// </summary>
        /// <param name="entidade">ProfessorCurso a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfessorCurso</returns>
        public ProfessorCurso Carregar(ProfessorCurso entidade)
        {
            ProfessorCurso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfessorCurso WHERE professorCursoId=@professorCursoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorCurso.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorCurso.</returns>
        public IEnumerable<ProfessorCurso> Carregar(ProfessorDisciplina entidade)
        {
            List<ProfessorCurso> entidadesRetorno = new List<ProfessorCurso>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorCurso.* FROM ProfessorCurso INNER JOIN ProfessorDisciplina ON ProfessorCurso.professorCursoId=ProfessorDisciplina.professorCursoId WHERE ProfessorDisciplina.professorDisciplinaId=@professorDisciplinaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorDisciplinaId", DbType.Int32, entidade.ProfessorDisciplinaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorCurso entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorCurso.
        /// </summary>
        /// <param name="entidade">Curso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorCurso.</returns>
        public IEnumerable<ProfessorCurso> Carregar(Curso entidade)
        {
            List<ProfessorCurso> entidadesRetorno = new List<ProfessorCurso>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorCurso.* FROM ProfessorCurso WHERE ProfessorCurso.cursoId=@cursoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@cursoId", DbType.Int32, entidade.CursoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorCurso entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorCurso.
        /// </summary>
        /// <param name="entidade">CursoNivel relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorCurso.</returns>
        public IEnumerable<ProfessorCurso> Carregar(CursoNivel entidade)
        {
            List<ProfessorCurso> entidadesRetorno = new List<ProfessorCurso>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorCurso.* FROM ProfessorCurso WHERE ProfessorCurso.cursoNivelId=@cursoNivelId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@cursoNivelId", DbType.Int32, entidade.CursoNivelId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorCurso entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorCurso.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorCurso.</returns>
        public IEnumerable<ProfessorCurso> Carregar(ProfessorInstituicao entidade)
        {
            List<ProfessorCurso> entidadesRetorno = new List<ProfessorCurso>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorCurso.* FROM ProfessorCurso WHERE ProfessorCurso.professorInstituicaoId=@professorInstituicaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorCurso entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de ProfessorCurso.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ProfessorCurso.</returns>
        public IEnumerable<ProfessorCurso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ProfessorCurso> entidadesRetorno = new List<ProfessorCurso>();

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
                sbOrder.Append(" ORDER BY professorCursoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProfessorCurso");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorCurso WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorCurso ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ProfessorCurso.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProfessorCurso ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ProfessorCurso.* FROM ProfessorCurso ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorCurso entidadeRetorno = new ProfessorCurso();
                PopulaProfessorCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os ProfessorCurso existentes na base de dados.
        /// </summary>
        public IEnumerable<ProfessorCurso> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de ProfessorCurso na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de ProfessorCurso na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM ProfessorCurso");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um ProfessorCurso baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProfessorCurso a ser populado(.</param>
        public static void PopulaProfessorCurso(IDataReader reader, ProfessorCurso entidade)
        {
            if (reader["professorCursoId"] != DBNull.Value)
                entidade.ProfessorCursoId = Convert.ToInt32(reader["professorCursoId"].ToString());

            if (reader["coordenadorCurso"] != DBNull.Value)
                entidade.CoordenadorCurso = Convert.ToBoolean(reader["coordenadorCurso"].ToString());

            if (reader["cargo"] != DBNull.Value)
                entidade.Cargo = reader["cargo"].ToString();

            if (reader["professorInstituicaoId"] != DBNull.Value)
            {
                entidade.ProfessorInstituicao = new ProfessorInstituicao();
                entidade.ProfessorInstituicao.ProfessorInstituicaoId = Convert.ToInt32(reader["professorInstituicaoId"].ToString());
            }

            if (reader["cursoNivelId"] != DBNull.Value)
            {
                entidade.CursoNivel = new CursoNivel();
                entidade.CursoNivel.CursoNivelId = Convert.ToInt32(reader["cursoNivelId"].ToString());
            }

            if (reader["cursoId"] != DBNull.Value)
            {
                entidade.Curso = new Curso();
                entidade.Curso.CursoId = Convert.ToInt32(reader["cursoId"].ToString());
            }
        }
    }
}