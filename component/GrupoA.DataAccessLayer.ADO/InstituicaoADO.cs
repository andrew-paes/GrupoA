
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
    public partial class InstituicaoADO : ADOSuper, IInstituicaoDAL
    {

        /// <summary>
        /// Método que persiste um Instituicao.
        /// </summary>
        /// <param name="entidade">Instituicao contendo os dados a serem persistidos.</param>	
        public void Inserir(Instituicao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Instituicao ");
            sbSQL.Append(" (nomeInstituicao, cnpj, telefoneNumero, emailInstituicao, urlSiteInstituicao, codigoInstituicao) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@nomeInstituicao, @cnpj, @telefoneNumero, @emailInstituicao, @urlSiteInstituicao, @codigoInstituicao) ");

            sbSQL.Append(" ; SET @instituicaoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@instituicaoId", DbType.Int32, 8);

            if (entidade.NomeInstituicao != null)
                _db.AddInParameter(command, "@nomeInstituicao", DbType.String, entidade.NomeInstituicao);
            else
                _db.AddInParameter(command, "@nomeInstituicao", DbType.String, null);

            if (entidade.Cnpj != null)
                _db.AddInParameter(command, "@cnpj", DbType.String, entidade.Cnpj);
            else
                _db.AddInParameter(command, "@cnpj", DbType.String, null);

            if (entidade.TelefoneNumero != null)
                _db.AddInParameter(command, "@telefoneNumero", DbType.String, entidade.TelefoneNumero);
            else
                _db.AddInParameter(command, "@telefoneNumero", DbType.String, null);

            if (entidade.EmailInstituicao != null)
                _db.AddInParameter(command, "@emailInstituicao", DbType.String, entidade.EmailInstituicao);
            else
                _db.AddInParameter(command, "@emailInstituicao", DbType.String, null);

            if (entidade.UrlSiteInstituicao != null)
                _db.AddInParameter(command, "@urlSiteInstituicao", DbType.String, entidade.UrlSiteInstituicao);
            else
                _db.AddInParameter(command, "@urlSiteInstituicao", DbType.String, null);

            _db.AddInParameter(command, "@codigoInstituicao", DbType.String, entidade.CodigoInstituicao);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.InstituicaoId = Convert.ToInt32(_db.GetParameterValue(command, "@instituicaoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Instituicao.
        /// </summary>
        /// <param name="entidade">Instituicao contendo os dados a serem atualizados.</param>
        public void Atualizar(Instituicao entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Instituicao SET ");
            sbSQL.Append(" nomeInstituicao=@nomeInstituicao, cnpj=@cnpj, telefoneNumero=@telefoneNumero, emailInstituicao=@emailInstituicao, urlSiteInstituicao=@urlSiteInstituicao, codigoInstituicao=@codigoInstituicao ");
            sbSQL.Append(" WHERE instituicaoId=@instituicaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.InstituicaoId);
            if (entidade.NomeInstituicao != null)
                _db.AddInParameter(command, "@nomeInstituicao", DbType.String, entidade.NomeInstituicao);
            else
                _db.AddInParameter(command, "@nomeInstituicao", DbType.String, null);
            if (entidade.Cnpj != null)
                _db.AddInParameter(command, "@cnpj", DbType.String, entidade.Cnpj);
            else
                _db.AddInParameter(command, "@cnpj", DbType.String, null);
            if (entidade.TelefoneNumero != null)
                _db.AddInParameter(command, "@telefoneNumero", DbType.String, entidade.TelefoneNumero);
            else
                _db.AddInParameter(command, "@telefoneNumero", DbType.String, null);
            if (entidade.EmailInstituicao != null)
                _db.AddInParameter(command, "@emailInstituicao", DbType.String, entidade.EmailInstituicao);
            else
                _db.AddInParameter(command, "@emailInstituicao", DbType.String, null);
            if (entidade.UrlSiteInstituicao != null)
                _db.AddInParameter(command, "@urlSiteInstituicao", DbType.String, entidade.UrlSiteInstituicao);
            else
                _db.AddInParameter(command, "@urlSiteInstituicao", DbType.String, null);
            _db.AddInParameter(command, "@codigoInstituicao", DbType.String, entidade.CodigoInstituicao);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Instituicao da base de dados.
        /// </summary>
        /// <param name="entidade">Instituicao a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Instituicao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Instituicao ");
            sbSQL.Append("WHERE instituicaoId=@instituicaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.InstituicaoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Instituicao.
        /// </summary>
        /// <param name="entidade">Instituicao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Instituicao</returns>
        public Instituicao Carregar(int instituicaoId)
        {
            Instituicao entidade = new Instituicao();
            entidade.InstituicaoId = instituicaoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Instituicao.
        /// </summary>
        /// <param name="entidade">Instituicao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Instituicao</returns>
        public Instituicao Carregar(Instituicao entidade)
        {

            Instituicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Instituicao WHERE instituicaoId=@instituicaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@instituicaoId", DbType.Int32, entidade.InstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de Instituicao.
        /// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Instituicao.</returns>
        public IEnumerable<Instituicao> Carregar(ProfessorComprovanteDocencia entidade)
        {
            List<Instituicao> entidadesRetorno = new List<Instituicao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Instituicao.* FROM Instituicao INNER JOIN ProfessorComprovanteDocencia ON Instituicao.instituicaoId=ProfessorComprovanteDocencia.instituicaoId WHERE ProfessorComprovanteDocencia.professorComprovanteDocenciaId=@professorComprovanteDocenciaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, entidade.ProfessorComprovanteDocenciaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Instituicao entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Instituicao.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Instituicao.</returns>
        public IEnumerable<Instituicao> Carregar(ProfessorInstituicao entidade)
        {
            List<Instituicao> entidadesRetorno = new List<Instituicao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Instituicao.* FROM Instituicao INNER JOIN ProfessorInstituicao ON Instituicao.instituicaoId=ProfessorInstituicao.instituicaoId WHERE ProfessorInstituicao.professorInstituicaoId=@professorInstituicaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Instituicao entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Instituicao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Instituicao.</returns>
        public IEnumerable<Instituicao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Instituicao> entidadesRetorno = new List<Instituicao>();

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
                sbOrder.Append(" ORDER BY instituicaoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Instituicao");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Instituicao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Instituicao ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Instituicao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Instituicao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Instituicao.* FROM Instituicao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Instituicao entidadeRetorno = new Instituicao();
                PopulaInstituicao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Instituicao existentes na base de dados.
        /// </summary>
        public IEnumerable<Instituicao> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Instituicao na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Instituicao na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Instituicao");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Instituicao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Instituicao a ser populado(.</param>
        public static void PopulaInstituicao(IDataReader reader, Instituicao entidade)
        {
            if (reader["instituicaoId"] != DBNull.Value)
                entidade.InstituicaoId = Convert.ToInt32(reader["instituicaoId"].ToString());

            if (reader["nomeInstituicao"] != DBNull.Value)
                entidade.NomeInstituicao = reader["nomeInstituicao"].ToString();

            if (reader["cnpj"] != DBNull.Value)
                entidade.Cnpj = reader["cnpj"].ToString();

            if (reader["telefoneNumero"] != DBNull.Value)
                entidade.TelefoneNumero = reader["telefoneNumero"].ToString();

            if (reader["emailInstituicao"] != DBNull.Value)
                entidade.EmailInstituicao = reader["emailInstituicao"].ToString();

            if (reader["urlSiteInstituicao"] != DBNull.Value)
                entidade.UrlSiteInstituicao = reader["urlSiteInstituicao"].ToString();

            if (reader["codigoInstituicao"] != DBNull.Value)
                entidade.CodigoInstituicao = reader["codigoInstituicao"].ToString();


        }

    }
}
