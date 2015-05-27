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
    public partial class TituloInformacaoResumoADO : ADOSuper, ITituloInformacaoResumoDAL
    {
        /// <summary>
        /// Método que persiste um TituloInformacaoResumo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoResumo contendo os dados a serem persistidos.</param>	
        public void Inserir(TituloInformacaoResumo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO TituloInformacaoResumo ");
            sbSQL.Append(" (tituloInformacaoResumoId, textoResumo) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@tituloInformacaoResumoId, @textoResumo) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoResumoId", DbType.Int32, entidade.TituloInformacaoResumoId);

            _db.AddInParameter(command, "@textoResumo", DbType.String, entidade.TextoResumo);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que atualiza os dados de um TituloInformacaoResumo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoResumo contendo os dados a serem atualizados.</param>
        public void Atualizar(TituloInformacaoResumo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloInformacaoResumo SET ");
            sbSQL.Append(" textoResumo=@textoResumo ");
            sbSQL.Append(" WHERE tituloInformacaoResumoId=@tituloInformacaoResumoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloInformacaoResumoId", DbType.Int32, entidade.TituloInformacaoResumoId);
            _db.AddInParameter(command, "@textoResumo", DbType.String, entidade.TextoResumo);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que remove um TituloInformacaoResumo da base de dados.
        /// </summary>
        /// <param name="entidade">TituloInformacaoResumo a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(TituloInformacaoResumo entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM TituloInformacaoResumo ");
            sbSQL.Append("WHERE tituloInformacaoResumoId=@tituloInformacaoResumoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoResumoId", DbType.Int32, entidade.TituloInformacaoResumoId);

            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Método que carrega um TituloInformacaoResumo.
        /// </summary>
        /// <param name="entidade">TituloInformacaoResumo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloInformacaoResumo</returns>
        public TituloInformacaoResumo Carregar(TituloInformacaoResumo entidade)
        {
            TituloInformacaoResumo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloInformacaoResumo WHERE tituloInformacaoResumoId=@tituloInformacaoResumoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoResumoId", DbType.Int32, entidade.TituloInformacaoResumoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoResumo();
                PopulaTituloInformacaoResumo(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um TituloInformacaoResumo com suas dependências.
        /// </summary>
        /// <param name="entidade">TituloInformacaoResumo a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloInformacaoResumo</returns>
        public TituloInformacaoResumo CarregarComDependencias(TituloInformacaoResumo entidade)
        {
            TituloInformacaoResumo entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT TituloInformacaoResumo.tituloInformacaoResumoId, TituloInformacaoResumo.textoResumo");
            sbSQL.Append(", tituloId, subtituloLivro, numeroPaginas, edicao, dataLancamento, dataPublicacao, maisVendido, nomeTitulo, formato");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(" FROM TituloInformacaoResumo");
            sbSQL.Append(" INNER JOIN Titulo ON TituloInformacaoResumo.tituloInformacaoResumoId=Titulo.tituloId");
            sbSQL.Append(" INNER JOIN Conteudo ON Titulo.tituloId=Conteudo.conteudoId");
            sbSQL.Append(" WHERE TituloInformacaoResumo.tituloInformacaoResumoId=@tituloInformacaoResumoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoResumoId", DbType.Int32, entidade.TituloInformacaoResumoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloInformacaoResumo();
                PopulaTituloInformacaoResumo(reader, entidadeRetorno);
                entidadeRetorno.Titulo = new Titulo();
                TituloADO.PopulaTitulo(reader, entidadeRetorno.Titulo);
                entidadeRetorno.Titulo.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Titulo.Conteudo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloInformacaoResumo.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos TituloInformacaoResumo.</returns>
        public IEnumerable<TituloInformacaoResumo> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<TituloInformacaoResumo> entidadesRetorno = new List<TituloInformacaoResumo>();

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
                sbOrder.Append(" ORDER BY tituloInformacaoResumoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloInformacaoResumo");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoResumo WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloInformacaoResumo ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT TituloInformacaoResumo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloInformacaoResumo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT TituloInformacaoResumo.* FROM TituloInformacaoResumo ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloInformacaoResumo entidadeRetorno = new TituloInformacaoResumo();
                PopulaTituloInformacaoResumo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os TituloInformacaoResumo existentes na base de dados.
        /// </summary>
        public IEnumerable<TituloInformacaoResumo> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de TituloInformacaoResumo na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de TituloInformacaoResumo na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloInformacaoResumo");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um TituloInformacaoResumo baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloInformacaoResumo a ser populado(.</param>
        public static void PopulaTituloInformacaoResumo(IDataReader reader, TituloInformacaoResumo entidade)
        {
            if (reader["textoResumo"] != DBNull.Value)
                entidade.TextoResumo = reader["textoResumo"].ToString();

            if (reader["tituloInformacaoResumoId"] != DBNull.Value)
            {
                entidade.TituloInformacaoResumoId = Convert.ToInt32(reader["tituloInformacaoResumoId"].ToString());
            }


        }
    }
}