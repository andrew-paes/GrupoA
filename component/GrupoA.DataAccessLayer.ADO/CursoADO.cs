
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
        /// Método que persiste um Curso.
        /// </summary>
        /// <param name="entidade">Curso contendo os dados a serem persistidos.</param>	
        public void Inserir(Curso entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Curso ");
            sbSQL.Append(" (nome, codigoCurso) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@nome, @codigoCurso) ");

            sbSQL.Append(" ; SET @cursoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@cursoId", DbType.Int32, 8);

            if (entidade.Nome != null)
                _db.AddInParameter(command, "@nome", DbType.String, entidade.Nome);
            else
                _db.AddInParameter(command, "@nome", DbType.String, null);

            _db.AddInParameter(command, "@codigoCurso", DbType.String, entidade.CodigoCurso);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.CursoId = Convert.ToInt32(_db.GetParameterValue(command, "@cursoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Curso.
        /// </summary>
        /// <param name="entidade">Curso contendo os dados a serem atualizados.</param>
        public void Atualizar(Curso entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Curso SET ");
            sbSQL.Append(" nome=@nome, codigoCurso=@codigoCurso ");
            sbSQL.Append(" WHERE cursoId=@cursoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@cursoId", DbType.Int32, entidade.CursoId);
            if (entidade.Nome != null)
                _db.AddInParameter(command, "@nome", DbType.String, entidade.Nome);
            else
                _db.AddInParameter(command, "@nome", DbType.String, null);
            _db.AddInParameter(command, "@codigoCurso", DbType.String, entidade.CodigoCurso);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Curso da base de dados.
        /// </summary>
        /// <param name="entidade">Curso a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Curso entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Curso ");
            sbSQL.Append("WHERE cursoId=@cursoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cursoId", DbType.Int32, entidade.CursoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Curso.
        /// </summary>
        /// <param name="entidade">Curso a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Curso</returns>
        public Curso Carregar(int cursoId)
        {
            Curso entidade = new Curso();
            entidade.CursoId = cursoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Curso.
        /// </summary>
        /// <param name="entidade">Curso a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Curso</returns>
        public Curso Carregar(Curso entidade)
        {

            Curso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Curso WHERE cursoId=@cursoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@cursoId", DbType.Int32, entidade.CursoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Curso();
                PopulaCurso(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de Curso.
        /// </summary>
        /// <param name="entidade">ProfessorCurso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Curso.</returns>
        public IEnumerable<Curso> Carregar(ProfessorCurso entidade)
        {
            List<Curso> entidadesRetorno = new List<Curso>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Curso.* FROM Curso INNER JOIN ProfessorCurso ON Curso.cursoId=ProfessorCurso.cursoId WHERE ProfessorCurso.professorCursoId=@professorCursoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Curso entidadeRetorno = new Curso();
                PopulaCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Curso.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Curso.</returns>
        public IEnumerable<Curso> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Curso> entidadesRetorno = new List<Curso>();

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
                sbOrder.Append(" ORDER BY cursoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Curso");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Curso WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Curso ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Curso.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Curso ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Curso.* FROM Curso ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Curso entidadeRetorno = new Curso();
                PopulaCurso(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Curso existentes na base de dados.
        /// </summary>
        public IEnumerable<Curso> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Curso na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Curso na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Curso");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Curso baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Curso a ser populado(.</param>
        public static void PopulaCurso(IDataReader reader, Curso entidade)
        {
            if (reader["cursoId"] != DBNull.Value)
                entidade.CursoId = Convert.ToInt32(reader["cursoId"].ToString());

            if (reader["nome"] != DBNull.Value)
                entidade.Nome = reader["nome"].ToString();

            if (reader["codigoCurso"] != DBNull.Value)
                entidade.CodigoCurso = reader["codigoCurso"].ToString();


        }

    }
}
