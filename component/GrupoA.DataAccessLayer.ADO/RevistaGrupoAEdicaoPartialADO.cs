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
    public partial class RevistaGrupoAEdicaoADO : ADOSuper, IRevistaGrupoAEdicaoDAL
    {
        /// <summary>
        /// Método que retorna o total de RevistaGrupoAEdicao na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int ValidarAtualizacao(RevistaGrupoAEdicao revista)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM RevistaGrupoAEdicao");
            sbSQL.Append(" WHERE mesPublicacao = @mesPublicacao ");
            sbSQL.Append(" AND anoPublicacao = @anoPublicacao ");
            sbSQL.Append(" AND numeroEdicao = @numeroEdicao ");
            sbSQL.Append(" AND revistaGrupoAEdicaoId <> @revistaGrupoAEdicaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@mesPublicacao", DbType.String, revista.MesPublicacao);
            _db.AddInParameter(command, "@anoPublicacao", DbType.String, revista.AnoPublicacao);
            _db.AddInParameter(command, "@numeroEdicao", DbType.Int32, revista.NumeroEdicao);
            _db.AddInParameter(command, "@revistaGrupoAEdicaoId", DbType.Int32, revista.RevistaGrupoAEdicaoId);

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que carrega um RevistaGrupoAEdicao.
        /// </summary>
        /// <param name="entidade">RevistaGrupoAEdicao a ser carregado (somente o identificador é necessário).</param>
        /// <returns>RevistaGrupoAEdicao</returns>
        public RevistaGrupoAEdicao CarregarComArquivos(RevistaGrupoAEdicao entidade)
        {

            RevistaGrupoAEdicao entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT RevistaGrupoAEdicao.* FROM RevistaGrupoAEdicao ");
            sbSQL.Append("WHERE revistaGrupoAEdicaoId=@revistaGrupoAEdicaoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaGrupoAEdicaoId", DbType.Int32, entidade.RevistaGrupoAEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new RevistaGrupoAEdicao();
                PopulaRevistaGrupoAEdicao(reader, entidadeRetorno);
                if (entidadeRetorno.ArquivoPequena != null && entidadeRetorno.ArquivoPequena.ArquivoId > 0)
                {
                    entidadeRetorno.ArquivoPequena = new ArquivoADO().Carregar(entidadeRetorno.ArquivoPequena);
                }
                if (entidadeRetorno.ArquivoGrande != null && entidadeRetorno.ArquivoGrande.ArquivoId > 0)
                {
                    entidadeRetorno.ArquivoGrande = new ArquivoADO().Carregar(entidadeRetorno.ArquivoGrande);
                }
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de RevistaGrupoAEdicao.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos RevistaGrupoAEdicao.</returns>
        public List<RevistaGrupoAEdicao> CarregarTodosComArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<RevistaGrupoAEdicao> entidadesRetorno = new List<RevistaGrupoAEdicao>();

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
                sbOrder.Append(" ORDER BY revistaGrupoAEdicaoId");
            }

            if (registrosPagina > 0)
            {
                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM RevistaGrupoAEdicao");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaGrupoAEdicao WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM RevistaGrupoAEdicao ORDER BY " + orderBy + ")");				
                //}

                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT RevistaGrupoAEdicao.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM RevistaGrupoAEdicao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());
            }
            else
            {
                sbSQL.Append("SELECT RevistaGrupoAEdicao.* FROM RevistaGrupoAEdicao ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaGrupoAEdicao entidadeRetorno = new RevistaGrupoAEdicao();
                PopulaRevistaGrupoAEdicaoComArquivos(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna todas os RevistaGrupoAEdicao existentes na base de dados.
        /// </summary>
        public List<RevistaGrupoAEdicao> CarregarTodosComArquivos()
        {
            return CarregarTodosComArquivos(0, 0, null, null, null);
        }

        public static void PopulaRevistaGrupoAEdicaoComArquivos(IDataReader reader, RevistaGrupoAEdicao entidade)
        {
            if (reader["revistaGrupoAEdicaoId"] != DBNull.Value)
                entidade.RevistaGrupoAEdicaoId = Convert.ToInt32(reader["revistaGrupoAEdicaoId"].ToString());

            if (reader["mesPublicacao"] != DBNull.Value)
                entidade.MesPublicacao = reader["mesPublicacao"].ToString();

            if (reader["anoPublicacao"] != DBNull.Value)
                entidade.AnoPublicacao = reader["anoPublicacao"].ToString();

            if (reader["numeroEdicao"] != DBNull.Value)
                entidade.NumeroEdicao = Convert.ToInt32(reader["numeroEdicao"].ToString());

            if (reader["textoChamada"] != DBNull.Value)
                entidade.TextoChamada = reader["textoChamada"].ToString();

            if (reader["urlRevista"] != DBNull.Value)
                entidade.UrlRevista = reader["urlRevista"].ToString();

            if (reader["tituloRevista"] != DBNull.Value)
                entidade.TituloRevista = reader["tituloRevista"].ToString();

            if (reader["arquivoIdPequena"] != DBNull.Value)
            {
                entidade.ArquivoPequena = new Arquivo();
                entidade.ArquivoPequena.ArquivoId = Convert.ToInt32(reader["arquivoIdPequena"].ToString());
                ArquivoADO arquivoAdo = new ArquivoADO();
                entidade.ArquivoPequena = arquivoAdo.Carregar(entidade.ArquivoPequena);
            }

            if (reader["arquivoIdGrande"] != DBNull.Value)
            {
                entidade.ArquivoGrande = new Arquivo();
                entidade.ArquivoGrande.ArquivoId = Convert.ToInt32(reader["arquivoIdGrande"].ToString());
                ArquivoADO arquivoAdo = new ArquivoADO();
                entidade.ArquivoGrande = arquivoAdo.Carregar(entidade.ArquivoGrande);
            }
        }
    }
}