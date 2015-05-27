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
    public partial class ProfessorInstituicaoADO : ADOSuper, IProfessorInstituicaoDAL
    {
        /// <summary>
        /// Método que persiste um ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao contendo os dados a serem persistidos.</param>	
        public void Inserir(ProfessorInstituicao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProfessorInstituicao ");
            sbSQL.Append(" (instituicaoId, campus, departamento, telefoneId, professorId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@instituicaoId, @campus, @departamento, @telefoneId, @professorId) ");

            sbSQL.Append(" ; SET @professorInstituicaoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@professorInstituicaoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.Instituicao.InstituicaoId);

            if (entidade.Campus != null)
                _db.AddInParameter(command, "@campus", DbType.String, entidade.Campus);
            else
                _db.AddInParameter(command, "@campus", DbType.String, null);

            if (entidade.Departamento != null)
                _db.AddInParameter(command, "@departamento", DbType.String, entidade.Departamento);
            else
                _db.AddInParameter(command, "@departamento", DbType.String, null);

            if (entidade.Telefone != null)
                _db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.Telefone.TelefoneId);
            else
                _db.AddInParameter(command, "@telefoneId", DbType.Int32, null);

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.Professor.ProfessorId);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.ProfessorInstituicaoId = Convert.ToInt32(_db.GetParameterValue(command, "@professorInstituicaoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao contendo os dados a serem atualizados.</param>
        public void Atualizar(ProfessorInstituicao entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE ProfessorInstituicao SET ");
            sbSQL.Append(" instituicaoId=@instituicaoId, campus=@campus, departamento=@departamento, telefoneId=@telefoneId, professorId=@professorId ");
            sbSQL.Append(" WHERE professorInstituicaoId=@professorInstituicaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);
            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.Instituicao.InstituicaoId);
            if (entidade.Campus != null)
                _db.AddInParameter(command, "@campus", DbType.String, entidade.Campus);
            else
                _db.AddInParameter(command, "@campus", DbType.String, null);
            if (entidade.Departamento != null)
                _db.AddInParameter(command, "@departamento", DbType.String, entidade.Departamento);
            else
                _db.AddInParameter(command, "@departamento", DbType.String, null);
            if (entidade.Telefone != null)
                _db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.Telefone.TelefoneId);
            else
                _db.AddInParameter(command, "@telefoneId", DbType.Int32, null);
            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.Professor.ProfessorId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um ProfessorInstituicao da base de dados.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(ProfessorInstituicao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProfessorInstituicao ");
            sbSQL.Append("WHERE professorInstituicaoId=@professorInstituicaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfessorInstituicao</returns>
        public ProfessorInstituicao Carregar(int professorInstituicaoId)
        {
            ProfessorInstituicao entidade = new ProfessorInstituicao();
            entidade.ProfessorInstituicaoId = professorInstituicaoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfessorInstituicao</returns>
        public ProfessorInstituicao Carregar(ProfessorInstituicao entidade)
        {

            ProfessorInstituicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfessorInstituicao WHERE professorInstituicaoId=@professorInstituicaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">ProfessorCurso relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorInstituicao.</returns>
        public IEnumerable<ProfessorInstituicao> Carregar(ProfessorCurso entidade)
        {
            List<ProfessorInstituicao> entidadesRetorno = new List<ProfessorInstituicao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorInstituicao.* FROM ProfessorInstituicao INNER JOIN ProfessorCurso ON ProfessorInstituicao.professorInstituicaoId=ProfessorCurso.professorInstituicaoId WHERE ProfessorCurso.professorCursoId=@professorCursoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorCursoId", DbType.Int32, entidade.ProfessorCursoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorInstituicao entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">Instituicao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorInstituicao.</returns>
        public IEnumerable<ProfessorInstituicao> Carregar(Instituicao entidade)
        {
            List<ProfessorInstituicao> entidadesRetorno = new List<ProfessorInstituicao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorInstituicao.* FROM ProfessorInstituicao WHERE ProfessorInstituicao.instituicaoId=@instituicaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.InstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorInstituicao entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">Professor relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfessorInstituicao.</returns>
        public IEnumerable<ProfessorInstituicao> Carregar(Professor entidade)
        {
            List<ProfessorInstituicao> entidadesRetorno = new List<ProfessorInstituicao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorInstituicao.* FROM ProfessorInstituicao WHERE ProfessorInstituicao.professorId=@professorId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorInstituicao entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna um ProfessorInstituicao.
        /// </summary>
        /// <param name="entidade">Telefone relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um ProfessorInstituicao.</returns>
        public ProfessorInstituicao Carregar(Telefone entidade)
        {
            ProfessorInstituicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfessorInstituicao.* FROM ProfessorInstituicao WHERE ProfessorInstituicao.telefoneId=@telefoneId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@telefoneId", DbType.Int32, entidade.TelefoneId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de ProfessorInstituicao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ProfessorInstituicao.</returns>
        public IEnumerable<ProfessorInstituicao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ProfessorInstituicao> entidadesRetorno = new List<ProfessorInstituicao>();

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
                sbOrder.Append(" ORDER BY professorInstituicaoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProfessorInstituicao");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorInstituicao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfessorInstituicao ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ProfessorInstituicao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProfessorInstituicao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ProfessorInstituicao.* FROM ProfessorInstituicao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfessorInstituicao entidadeRetorno = new ProfessorInstituicao();
                PopulaProfessorInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os ProfessorInstituicao existentes na base de dados.
        /// </summary>
        public IEnumerable<ProfessorInstituicao> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de ProfessorInstituicao na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de ProfessorInstituicao na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM ProfessorInstituicao");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um ProfessorInstituicao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProfessorInstituicao a ser populado(.</param>
        public static void PopulaProfessorInstituicao(IDataReader reader, ProfessorInstituicao entidade)
        {
            if (reader["professorInstituicaoId"] != DBNull.Value)
                entidade.ProfessorInstituicaoId = Convert.ToInt32(reader["professorInstituicaoId"].ToString());

            if (reader["campus"] != DBNull.Value)
                entidade.Campus = reader["campus"].ToString();

            if (reader["departamento"] != DBNull.Value)
                entidade.Departamento = reader["departamento"].ToString();

            if (reader["instituicaoId"] != DBNull.Value)
            {
                entidade.Instituicao = new Instituicao();
                entidade.Instituicao.InstituicaoId = Convert.ToInt32(reader["instituicaoId"].ToString());
            }

            if (reader["telefoneId"] != DBNull.Value)
            {
                entidade.Telefone = new Telefone();
                entidade.Telefone.TelefoneId = Convert.ToInt32(reader["telefoneId"].ToString());
            }

            if (reader["professorId"] != DBNull.Value)
            {
                entidade.Professor = new Professor();
                entidade.Professor.ProfessorId = Convert.ToInt32(reader["professorId"].ToString());
            }


        }

    }
}
