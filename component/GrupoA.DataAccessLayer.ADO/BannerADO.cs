
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
    public partial class BannerADO : ADOSuper, IBannerDAL
    {

        /// <summary>
        /// Método que persiste um Banner.
        /// </summary>
        /// <param name="entidade">Banner contendo os dados a serem persistidos.</param>	
        public void Inserir(Banner entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Banner ");
            sbSQL.Append(" (nomeBanner, ativo, dataExibicaoInicio, dataExibicaoFim, url, arquivoId, targetBlank, tempoExibicao) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@nomeBanner, @ativo, @dataExibicaoInicio, @dataExibicaoFim, @url, @arquivoId, @targetBlank, @tempoExibicao) ");

            sbSQL.Append(" ; SET @bannerId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@bannerId", DbType.Int32, 8);

            _db.AddInParameter(command, "@nomeBanner", DbType.String, entidade.NomeBanner);

            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

            if (entidade.DataExibicaoInicio != null && entidade.DataExibicaoInicio != DateTime.MinValue)
                _db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, entidade.DataExibicaoInicio);
            else
                _db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, null);

            if (entidade.DataExibicaoFim != null && entidade.DataExibicaoFim != DateTime.MinValue)
                _db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, entidade.DataExibicaoFim);
            else
                _db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, null);

            if (entidade.Url != null)
                _db.AddInParameter(command, "@url", DbType.String, entidade.Url);
            else
                _db.AddInParameter(command, "@url", DbType.String, null);

            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, null);

            _db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);

            _db.AddInParameter(command, "@tempoExibicao", DbType.Int32, entidade.TempoExibicao);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.BannerId = Convert.ToInt32(_db.GetParameterValue(command, "@bannerId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Banner.
        /// </summary>
        /// <param name="entidade">Banner contendo os dados a serem atualizados.</param>
        public void Atualizar(Banner entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Banner SET ");
            sbSQL.Append(" nomeBanner=@nomeBanner, ativo=@ativo, dataExibicaoInicio=@dataExibicaoInicio, dataExibicaoFim=@dataExibicaoFim, url=@url, arquivoId=@arquivoId, targetBlank=@targetBlank, tempoExibicao=@tempoExibicao ");
            sbSQL.Append(" WHERE bannerId=@bannerId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@bannerId", DbType.Int32, entidade.BannerId);
            _db.AddInParameter(command, "@nomeBanner", DbType.String, entidade.NomeBanner);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
            if (entidade.DataExibicaoInicio != null && entidade.DataExibicaoInicio != DateTime.MinValue)
                _db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, entidade.DataExibicaoInicio);
            else
                _db.AddInParameter(command, "@dataExibicaoInicio", DbType.DateTime, null);
            if (entidade.DataExibicaoFim != null && entidade.DataExibicaoFim != DateTime.MinValue)
                _db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, entidade.DataExibicaoFim);
            else
                _db.AddInParameter(command, "@dataExibicaoFim", DbType.DateTime, null);
            if (entidade.Url != null)
                _db.AddInParameter(command, "@url", DbType.String, entidade.Url);
            else
                _db.AddInParameter(command, "@url", DbType.String, null);
            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, null);
            _db.AddInParameter(command, "@targetBlank", DbType.Int32, entidade.TargetBlank);
            _db.AddInParameter(command, "@tempoExibicao", DbType.Int32, entidade.TempoExibicao);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Banner da base de dados.
        /// </summary>
        /// <param name="entidade">Banner a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Banner entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Banner ");
            sbSQL.Append("WHERE bannerId=@bannerId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@bannerId", DbType.Int32, entidade.BannerId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Banner.
        /// </summary>
        /// <param name="entidade">Banner a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Banner</returns>
        public Banner Carregar(int bannerId)
        {
            Banner entidade = new Banner();
            entidade.BannerId = bannerId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Banner.
        /// </summary>
        /// <param name="entidade">Banner a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Banner</returns>
        public Banner Carregar(Banner entidade)
        {

            Banner entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Banner WHERE bannerId=@bannerId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@bannerId", DbType.Int32, entidade.BannerId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Banner();
                PopulaBanner(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de Banner.
        /// </summary>
        /// <param name="entidade">BannerArea relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Banner.</returns>
        public IEnumerable<Banner> Carregar(BannerArea entidade)
        {
            List<Banner> entidadesRetorno = new List<Banner>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Banner.* FROM Banner INNER JOIN BannerLocalizacao ON Banner.bannerId=BannerLocalizacao.bannerId WHERE BannerLocalizacao.bannerAreaId=@bannerAreaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@bannerAreaId", DbType.Int32, entidade.BannerAreaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Banner entidadeRetorno = new Banner();
                PopulaBanner(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Banner.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Banner.</returns>
        public IEnumerable<Banner> Carregar(Arquivo entidade)
        {
            List<Banner> entidadesRetorno = new List<Banner>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Banner.* FROM Banner WHERE Banner.arquivoId=@arquivoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Banner entidadeRetorno = new Banner();
                PopulaBanner(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Banner.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Banner.</returns>
        public IEnumerable<Banner> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Banner> entidadesRetorno = new List<Banner>();

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
                sbOrder.Append(" ORDER BY bannerId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Banner");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Banner WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Banner ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Banner.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Banner ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Banner.* FROM Banner ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Banner entidadeRetorno = new Banner();
                PopulaBanner(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Banner existentes na base de dados.
        /// </summary>
        public IEnumerable<Banner> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Banner na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Banner na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Banner");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Banner baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Banner a ser populado(.</param>
        public static void PopulaBanner(IDataReader reader, Banner entidade)
        {
            if (reader["bannerId"] != DBNull.Value)
                entidade.BannerId = Convert.ToInt32(reader["bannerId"].ToString());

            if (reader["nomeBanner"] != DBNull.Value)
                entidade.NomeBanner = reader["nomeBanner"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["dataExibicaoInicio"] != DBNull.Value)
                entidade.DataExibicaoInicio = Convert.ToDateTime(reader["dataExibicaoInicio"].ToString());

            if (reader["dataExibicaoFim"] != DBNull.Value)
                entidade.DataExibicaoFim = Convert.ToDateTime(reader["dataExibicaoFim"].ToString());

            if (reader["url"] != DBNull.Value)
                entidade.Url = reader["url"].ToString();

            if (reader["targetBlank"] != DBNull.Value)
                entidade.TargetBlank = Convert.ToBoolean(reader["targetBlank"].ToString());

            if (reader["tempoExibicao"] != DBNull.Value)
                entidade.TempoExibicao = Convert.ToInt32(reader["tempoExibicao"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }


        }

    }
}
