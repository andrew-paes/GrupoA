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
    public partial class TituloConteudoExtraArquivoADO : ADOSuper, ITituloConteudoExtraArquivoDAL
    {
        /// <summary>
        /// Método que persiste um TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo contendo os dados a serem persistidos.</param>	
        public void Inserir(TituloConteudoExtraArquivo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO TituloConteudoExtraArquivo ");
            sbSQL.Append(" (tituloId, somenteLogado, restritoProfessor, arquivoId, nomeConteudo, ativo, dataCadastro) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@tituloId, @somenteLogado, @restritoProfessor, @arquivoId, @nomeConteudo, @ativo, GETDATE()) ");

            sbSQL.Append(" ; SET @tituloConteudoExtraArquivoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

            _db.AddInParameter(command, "@somenteLogado", DbType.Int32, entidade.SomenteLogado);

            _db.AddInParameter(command, "@restritoProfessor", DbType.Int32, entidade.RestritoProfessor);

            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);

            //_db.AddInParameter(command, "@dataCadastro", DbType.DateTime, entidade.DataCadastro);

            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, null);

            _db.AddInParameter(command, "@nomeConteudo", DbType.String, entidade.NomeConteudo);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.TituloConteudoExtraArquivoId = Convert.ToInt32(_db.GetParameterValue(command, "@tituloConteudoExtraArquivoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo contendo os dados a serem atualizados.</param>
        public void Atualizar(TituloConteudoExtraArquivo entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloConteudoExtraArquivo SET ");
            sbSQL.Append(" tituloId=@tituloId, somenteLogado=@somenteLogado, restritoProfessor=@restritoProfessor, arquivoId=@arquivoId, nomeConteudo=@nomeConteudo, ativo=@ativo ");
            sbSQL.Append(" WHERE tituloConteudoExtraArquivoId=@tituloConteudoExtraArquivoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, entidade.TituloConteudoExtraArquivoId);
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);
            _db.AddInParameter(command, "@somenteLogado", DbType.Int32, entidade.SomenteLogado);
            _db.AddInParameter(command, "@restritoProfessor", DbType.Int32, entidade.RestritoProfessor);
            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);

            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, null);
            _db.AddInParameter(command, "@nomeConteudo", DbType.String, entidade.NomeConteudo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um TituloConteudoExtraArquivo da base de dados.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(TituloConteudoExtraArquivo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM TituloConteudoExtraArquivo ");
            sbSQL.Append("WHERE tituloConteudoExtraArquivoId=@tituloConteudoExtraArquivoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, entidade.TituloConteudoExtraArquivoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloConteudoExtraArquivo</returns>
        public TituloConteudoExtraArquivo Carregar(int tituloConteudoExtraArquivoId)
        {
            TituloConteudoExtraArquivo entidade = new TituloConteudoExtraArquivo();
            entidade.TituloConteudoExtraArquivoId = tituloConteudoExtraArquivoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="entidade">TituloConteudoExtraArquivo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloConteudoExtraArquivo</returns>
        public TituloConteudoExtraArquivo Carregar(TituloConteudoExtraArquivo entidade)
        {

            TituloConteudoExtraArquivo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloConteudoExtraArquivo WHERE tituloConteudoExtraArquivoId=@tituloConteudoExtraArquivoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloConteudoExtraArquivoId", DbType.Int32, entidade.TituloConteudoExtraArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloConteudoExtraArquivo();
                PopulaTituloConteudoExtraArquivo(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de TituloConteudoExtraArquivo.</returns>
        public IEnumerable<TituloConteudoExtraArquivo> Carregar(Arquivo entidade)
        {
            List<TituloConteudoExtraArquivo> entidadesRetorno = new List<TituloConteudoExtraArquivo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloConteudoExtraArquivo.* FROM TituloConteudoExtraArquivo WHERE TituloConteudoExtraArquivo.arquivoId=@arquivoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloConteudoExtraArquivo entidadeRetorno = new TituloConteudoExtraArquivo();
                PopulaTituloConteudoExtraArquivo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de TituloConteudoExtraArquivo.</returns>
        public IEnumerable<TituloConteudoExtraArquivo> Carregar(Titulo entidade)
        {
            List<TituloConteudoExtraArquivo> entidadesRetorno = new List<TituloConteudoExtraArquivo>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloConteudoExtraArquivo.* FROM TituloConteudoExtraArquivo WHERE TituloConteudoExtraArquivo.tituloId=@tituloId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloConteudoExtraArquivo entidadeRetorno = new TituloConteudoExtraArquivo();
                PopulaTituloConteudoExtraArquivo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de TituloConteudoExtraArquivo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos TituloConteudoExtraArquivo.</returns>
        public IEnumerable<TituloConteudoExtraArquivo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<TituloConteudoExtraArquivo> entidadesRetorno = new List<TituloConteudoExtraArquivo>();

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
                sbOrder.Append(" ORDER BY tituloConteudoExtraArquivoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloConteudoExtraArquivo");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloConteudoExtraArquivo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloConteudoExtraArquivo ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT TituloConteudoExtraArquivo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloConteudoExtraArquivo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT TituloConteudoExtraArquivo.* FROM TituloConteudoExtraArquivo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloConteudoExtraArquivo entidadeRetorno = new TituloConteudoExtraArquivo();
                PopulaTituloConteudoExtraArquivo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os TituloConteudoExtraArquivo existentes na base de dados.
        /// </summary>
        public IEnumerable<TituloConteudoExtraArquivo> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de TituloConteudoExtraArquivo na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de TituloConteudoExtraArquivo na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloConteudoExtraArquivo");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um TituloConteudoExtraArquivo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloConteudoExtraArquivo a ser populado(.</param>
        public static void PopulaTituloConteudoExtraArquivo(IDataReader reader, TituloConteudoExtraArquivo entidade)
        {
            if (reader["tituloConteudoExtraArquivoId"] != DBNull.Value)
                entidade.TituloConteudoExtraArquivoId = Convert.ToInt32(reader["tituloConteudoExtraArquivoId"].ToString());

            if (reader["somenteLogado"] != DBNull.Value)
                entidade.SomenteLogado = Convert.ToBoolean(reader["somenteLogado"].ToString());

            if (reader["restritoProfessor"] != DBNull.Value)
                entidade.RestritoProfessor = Convert.ToBoolean(reader["restritoProfessor"].ToString());

            if (reader["nomeConteudo"] != DBNull.Value)
                entidade.NomeConteudo = reader["nomeConteudo"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"]);

            if (reader["dataCadastro"] != DBNull.Value)
                entidade.DataCadastro = Convert.ToDateTime(reader["dataCadastro"]);

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }

        }
    }
}