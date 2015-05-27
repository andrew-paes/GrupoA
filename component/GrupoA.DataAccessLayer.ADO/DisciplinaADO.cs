
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
    public partial class DisciplinaADO : ADOSuper, IDisciplinaDAL
    {

        /// <summary>
        /// Método que persiste um Disciplina.
        /// </summary>
        /// <param name="entidade">Disciplina contendo os dados a serem persistidos.</param>	
        public void Inserir(Disciplina entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Disciplina ");
            sbSQL.Append(" (descricao, codigoDisciplina) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@descricao, @codigoDisciplina) ");

            sbSQL.Append(" ; SET @disciplinaId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@disciplinaId", DbType.Int32, 8);

            if (entidade.Descricao != null)
                _db.AddInParameter(command, "@descricao", DbType.String, entidade.Descricao);
            else
                _db.AddInParameter(command, "@descricao", DbType.String, null);

            _db.AddInParameter(command, "@codigoDisciplina", DbType.String, entidade.CodigoDisciplina);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.DisciplinaId = Convert.ToInt32(_db.GetParameterValue(command, "@disciplinaId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Disciplina.
        /// </summary>
        /// <param name="entidade">Disciplina contendo os dados a serem atualizados.</param>
        public void Atualizar(Disciplina entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Disciplina SET ");
            sbSQL.Append(" descricao=@descricao, codigoDisciplina=@codigoDisciplina ");
            sbSQL.Append(" WHERE disciplinaId=@disciplinaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, entidade.DisciplinaId);
            if (entidade.Descricao != null)
                _db.AddInParameter(command, "@descricao", DbType.String, entidade.Descricao);
            else
                _db.AddInParameter(command, "@descricao", DbType.String, null);
            _db.AddInParameter(command, "@codigoDisciplina", DbType.String, entidade.CodigoDisciplina);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Disciplina da base de dados.
        /// </summary>
        /// <param name="entidade">Disciplina a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Disciplina entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Disciplina ");
            sbSQL.Append("WHERE disciplinaId=@disciplinaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, entidade.DisciplinaId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Disciplina.
        /// </summary>
        /// <param name="entidade">Disciplina a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Disciplina</returns>
        public Disciplina Carregar(int disciplinaId)
        {
            Disciplina entidade = new Disciplina();
            entidade.DisciplinaId = disciplinaId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Disciplina.
        /// </summary>
        /// <param name="entidade">Disciplina a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Disciplina</returns>
        public Disciplina Carregar(Disciplina entidade)
        {

            Disciplina entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Disciplina WHERE disciplinaId=@disciplinaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@disciplinaId", DbType.Int32, entidade.DisciplinaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Disciplina();
                PopulaDisciplina(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de Disciplina.
        /// </summary>
        /// <param name="entidade">ProfessorDisciplina relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Disciplina.</returns>
        public IEnumerable<Disciplina> Carregar(ProfessorDisciplina entidade)
        {
            List<Disciplina> entidadesRetorno = new List<Disciplina>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Disciplina.* FROM Disciplina INNER JOIN ProfessorDisciplina ON Disciplina.disciplinaId=ProfessorDisciplina.disciplinaId WHERE ProfessorDisciplina.professorDisciplinaId=@professorDisciplinaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorDisciplinaId", DbType.Int32, entidade.ProfessorDisciplinaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Disciplina entidadeRetorno = new Disciplina();
                PopulaDisciplina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Disciplina.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Disciplina.</returns>
        public IEnumerable<Disciplina> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Disciplina> entidadesRetorno = new List<Disciplina>();

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
                sbOrder.Append(" ORDER BY descricao");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Disciplina");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Disciplina WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Disciplina ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Disciplina.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Disciplina ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Disciplina.* FROM Disciplina ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Disciplina entidadeRetorno = new Disciplina();
                PopulaDisciplina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Disciplina existentes na base de dados.
        /// </summary>
        public IEnumerable<Disciplina> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Disciplina na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Disciplina na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Disciplina");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Disciplina baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Disciplina a ser populado(.</param>
        public static void PopulaDisciplina(IDataReader reader, Disciplina entidade)
        {
            if (reader["disciplinaId"] != DBNull.Value)
                entidade.DisciplinaId = Convert.ToInt32(reader["disciplinaId"].ToString());

            if (reader["descricao"] != DBNull.Value)
                entidade.Descricao = reader["descricao"].ToString();

            if (reader["codigoDisciplina"] != DBNull.Value)
                entidade.CodigoDisciplina = reader["codigoDisciplina"].ToString();


        }

    }
}
