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
    public partial class ProfissionalOcupacaoADO : ADOSuper, IProfissionalOcupacaoDAL
    {

        /// <summary>
        /// Método que persiste um ProfissionalOcupacao.
        /// </summary>
        /// <param name="entidade">ProfissionalOcupacao contendo os dados a serem persistidos.</param>	
        public void Inserir(ProfissionalOcupacao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProfissionalOcupacao ");
            sbSQL.Append(" (ocupacao, codigoOcupacao) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@ocupacao, @codigoOcupacao) ");

            sbSQL.Append(" ; SET @profissionalOcupacaoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@profissionalOcupacaoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@ocupacao", DbType.String, entidade.Ocupacao);

            if (entidade.CodigoOcupacao != null)
                _db.AddInParameter(command, "@codigoOcupacao", DbType.String, entidade.CodigoOcupacao);
            else
                _db.AddInParameter(command, "@codigoOcupacao", DbType.String, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.ProfissionalOcupacaoId = Convert.ToInt32(_db.GetParameterValue(command, "@profissionalOcupacaoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um ProfissionalOcupacao.
        /// </summary>
        /// <param name="entidade">ProfissionalOcupacao contendo os dados a serem atualizados.</param>
        public void Atualizar(ProfissionalOcupacao entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE ProfissionalOcupacao SET ");
            sbSQL.Append(" ocupacao=@ocupacao, codigoOcupacao=@codigoOcupacao ");
            sbSQL.Append(" WHERE profissionalOcupacaoId=@profissionalOcupacaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, entidade.ProfissionalOcupacaoId);
            _db.AddInParameter(command, "@ocupacao", DbType.String, entidade.Ocupacao);
            if (entidade.CodigoOcupacao != null)
                _db.AddInParameter(command, "@codigoOcupacao", DbType.String, entidade.CodigoOcupacao);
            else
                _db.AddInParameter(command, "@codigoOcupacao", DbType.String, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um ProfissionalOcupacao da base de dados.
        /// </summary>
        /// <param name="entidade">ProfissionalOcupacao a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(ProfissionalOcupacao entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProfissionalOcupacao ");
            sbSQL.Append("WHERE profissionalOcupacaoId=@profissionalOcupacaoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, entidade.ProfissionalOcupacaoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um ProfissionalOcupacao.
        /// </summary>
        /// <param name="entidade">ProfissionalOcupacao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfissionalOcupacao</returns>
        public ProfissionalOcupacao Carregar(int profissionalOcupacaoId)
        {
            ProfissionalOcupacao entidade = new ProfissionalOcupacao();
            entidade.ProfissionalOcupacaoId = profissionalOcupacaoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um ProfissionalOcupacao.
        /// </summary>
        /// <param name="entidade">ProfissionalOcupacao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProfissionalOcupacao</returns>
        public ProfissionalOcupacao Carregar(ProfissionalOcupacao entidade)
        {

            ProfissionalOcupacao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProfissionalOcupacao WHERE profissionalOcupacaoId=@profissionalOcupacaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@profissionalOcupacaoId", DbType.Int32, entidade.ProfissionalOcupacaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProfissionalOcupacao();
                PopulaProfissionalOcupacao(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de ProfissionalOcupacao.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProfissionalOcupacao.</returns>
        public IEnumerable<ProfissionalOcupacao> Carregar(Usuario entidade)
        {
            List<ProfissionalOcupacao> entidadesRetorno = new List<ProfissionalOcupacao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProfissionalOcupacao.* FROM ProfissionalOcupacao INNER JOIN Usuario ON ProfissionalOcupacao.profissionalOcupacaoId=Usuario.profissionalOcupacaoId WHERE Usuario.usuarioId=@usuarioId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfissionalOcupacao entidadeRetorno = new ProfissionalOcupacao();
                PopulaProfissionalOcupacao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de ProfissionalOcupacao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ProfissionalOcupacao.</returns>
        public IEnumerable<ProfissionalOcupacao> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ProfissionalOcupacao> entidadesRetorno = new List<ProfissionalOcupacao>();

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
                sbOrder.Append(" ORDER BY profissionalOcupacaoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProfissionalOcupacao");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfissionalOcupacao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProfissionalOcupacao ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ProfissionalOcupacao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProfissionalOcupacao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ProfissionalOcupacao.* FROM ProfissionalOcupacao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProfissionalOcupacao entidadeRetorno = new ProfissionalOcupacao();
                PopulaProfissionalOcupacao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os ProfissionalOcupacao existentes na base de dados.
        /// </summary>
        public IEnumerable<ProfissionalOcupacao> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de ProfissionalOcupacao na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de ProfissionalOcupacao na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM ProfissionalOcupacao");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um ProfissionalOcupacao baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProfissionalOcupacao a ser populado(.</param>
        public static void PopulaProfissionalOcupacao(IDataReader reader, ProfissionalOcupacao entidade)
        {
            if (reader["profissionalOcupacaoId"] != DBNull.Value)
                entidade.ProfissionalOcupacaoId = Convert.ToInt32(reader["profissionalOcupacaoId"].ToString());

            if (reader["ocupacao"] != DBNull.Value)
                entidade.Ocupacao = reader["ocupacao"].ToString();

            if (reader["codigoOcupacao"] != DBNull.Value)
                entidade.CodigoOcupacao = reader["codigoOcupacao"].ToString();


        }

    }
}
