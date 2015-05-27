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
    public partial class NewsletterDestinatarioADO : ADOSuper, INewsletterDestinatarioDAL
    {
        /// <summary>
        /// Método que persiste um NewsletterDestinatario.
        /// </summary>
        /// <param name="entidade">NewsletterDestinatario contendo os dados a serem persistidos.</param>	
        public void Inserir(NewsletterDestinatario entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO NewsletterDestinatario ");
            sbSQL.Append(" (emailDestinatario, nomeDestinatario, dataHoraCadastro) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@emailDestinatario, @nomeDestinatario, @dataHoraCadastro) ");

            sbSQL.Append(" ; SET @newsletterDestinatarioId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@newsletterDestinatarioId", DbType.Int32, 8);

            _db.AddInParameter(command, "@emailDestinatario", DbType.String, entidade.EmailDestinatario);

            _db.AddInParameter(command, "@nomeDestinatario", DbType.String, entidade.NomeDestinatario);

            _db.AddInParameter(command, "@dataHoraCadastro", DbType.DateTime, DateTime.Now);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.NewsletterDestinatarioId = Convert.ToInt32(_db.GetParameterValue(command, "@newsletterDestinatarioId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um NewsletterDestinatario.
        /// </summary>
        /// <param name="entidade">NewsletterDestinatario contendo os dados a serem atualizados.</param>
        public void Atualizar(NewsletterDestinatario entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE NewsletterDestinatario SET ");
            sbSQL.Append(" emailDestinatario=@emailDestinatario, nomeDestinatario=@nomeDestinatario, dataHoraCadastro=@dataHoraCadastro ");
            sbSQL.Append(" WHERE newsletterDestinatarioId=@newsletterDestinatarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@newsletterDestinatarioId", DbType.Int32, entidade.NewsletterDestinatarioId);
            _db.AddInParameter(command, "@emailDestinatario", DbType.String, entidade.EmailDestinatario);
            _db.AddInParameter(command, "@nomeDestinatario", DbType.String, entidade.NomeDestinatario);
            _db.AddInParameter(command, "@dataHoraCadastro", DbType.DateTime, entidade.DataHoraCadastro);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um NewsletterDestinatario da base de dados.
        /// </summary>
        /// <param name="entidade">NewsletterDestinatario a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(NewsletterDestinatario entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM NewsletterDestinatario ");
            sbSQL.Append("WHERE newsletterDestinatarioId=@newsletterDestinatarioId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@newsletterDestinatarioId", DbType.Int32, entidade.NewsletterDestinatarioId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um NewsletterDestinatario.
        /// </summary>
        /// <param name="entidade">NewsletterDestinatario a ser carregado (somente o identificador é necessário).</param>
        /// <returns>NewsletterDestinatario</returns>
        public NewsletterDestinatario Carregar(int newsletterDestinatarioId)
        {
            NewsletterDestinatario entidade = new NewsletterDestinatario();
            entidade.NewsletterDestinatarioId = newsletterDestinatarioId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um NewsletterDestinatario.
        /// </summary>
        /// <param name="entidade">NewsletterDestinatario a ser carregado (somente o identificador é necessário).</param>
        /// <returns>NewsletterDestinatario</returns>
        public NewsletterDestinatario Carregar(NewsletterDestinatario entidade)
        {

            NewsletterDestinatario entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM NewsletterDestinatario WHERE newsletterDestinatarioId=@newsletterDestinatarioId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@newsletterDestinatarioId", DbType.Int32, entidade.NewsletterDestinatarioId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new NewsletterDestinatario();
                PopulaNewsletterDestinatario(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }




        /// <summary>
        /// Método que retorna uma coleção de NewsletterDestinatario.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos NewsletterDestinatario.</returns>
        public IEnumerable<NewsletterDestinatario> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<NewsletterDestinatario> entidadesRetorno = new List<NewsletterDestinatario>();

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
                sbOrder.Append(" ORDER BY newsletterDestinatarioId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM NewsletterDestinatario");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NewsletterDestinatario WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NewsletterDestinatario ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT NewsletterDestinatario.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM NewsletterDestinatario ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT NewsletterDestinatario.* FROM NewsletterDestinatario ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                NewsletterDestinatario entidadeRetorno = new NewsletterDestinatario();
                PopulaNewsletterDestinatario(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os NewsletterDestinatario existentes na base de dados.
        /// </summary>
        public IEnumerable<NewsletterDestinatario> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de NewsletterDestinatario na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de NewsletterDestinatario na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM NewsletterDestinatario");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um NewsletterDestinatario baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">NewsletterDestinatario a ser populado(.</param>
        public static void PopulaNewsletterDestinatario(IDataReader reader, NewsletterDestinatario entidade)
        {
            if (reader["newsletterDestinatarioId"] != DBNull.Value)
                entidade.NewsletterDestinatarioId = Convert.ToInt32(reader["newsletterDestinatarioId"].ToString());

            if (reader["emailDestinatario"] != DBNull.Value)
                entidade.EmailDestinatario = reader["emailDestinatario"].ToString();

            if (reader["nomeDestinatario"] != DBNull.Value)
                entidade.NomeDestinatario = reader["nomeDestinatario"].ToString();

            if (reader["dataHoraCadastro"] != DBNull.Value)
                entidade.DataHoraCadastro = Convert.ToDateTime(reader["dataHoraCadastro"].ToString());


        }

    }
}
