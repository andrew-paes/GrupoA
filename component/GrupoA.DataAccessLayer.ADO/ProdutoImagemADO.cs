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
    public partial class ProdutoImagemADO : ADOSuper, IProdutoImagemDAL
    {
        /// <summary>
        /// Método que persiste um ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagem contendo os dados a serem persistidos.</param>	
        public void Inserir(ProdutoImagem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProdutoImagem ");
            sbSQL.Append(" (arquivoId, produtoId, produtoImagemTipoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@arquivoId, @produtoId, @produtoImagemTipoId) ");

            sbSQL.Append(" ; SET @produtoImagemId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@produtoImagemId", DbType.Int32, 8);

            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

            _db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipo.ProdutoImagemTipoId);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.ProdutoImagemId = Convert.ToInt32(_db.GetParameterValue(command, "@produtoImagemId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagem contendo os dados a serem atualizados.</param>
        public void Atualizar(ProdutoImagem entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE ProdutoImagem SET ");
            sbSQL.Append(" arquivoId=@arquivoId, produtoId=@produtoId, produtoImagemTipoId=@produtoImagemTipoId ");
            sbSQL.Append(" WHERE produtoImagemId=@produtoImagemId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@produtoImagemId", DbType.Int32, entidade.ProdutoImagemId);
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
            _db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipo.ProdutoImagemTipoId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um ProdutoImagem da base de dados.
        /// </summary>
        /// <param name="entidade">ProdutoImagem a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(ProdutoImagem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProdutoImagem ");
            sbSQL.Append("WHERE produtoImagemId=@produtoImagemId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoImagemId", DbType.Int32, entidade.ProdutoImagemId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProdutoImagem</returns>
        public ProdutoImagem Carregar(int produtoImagemId)
        {
            ProdutoImagem entidade = new ProdutoImagem();
            entidade.ProdutoImagemId = produtoImagemId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProdutoImagem</returns>
        public ProdutoImagem Carregar(ProdutoImagem entidade)
        {

            ProdutoImagem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProdutoImagem WHERE produtoImagemId=@produtoImagemId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoImagemId", DbType.Int32, entidade.ProdutoImagemId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProdutoImagem();
                PopulaProdutoImagem(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de ProdutoImagem.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProdutoImagem.</returns>
        public IEnumerable<ProdutoImagem> Carregar(Arquivo entidade)
        {
            List<ProdutoImagem> entidadesRetorno = new List<ProdutoImagem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProdutoImagem.* FROM ProdutoImagem WHERE ProdutoImagem.arquivoId=@arquivoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoImagem entidadeRetorno = new ProdutoImagem();
                PopulaProdutoImagem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProdutoImagem.
        /// </summary>
        /// <param name="entidade">Produto relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProdutoImagem.</returns>
        public IEnumerable<ProdutoImagem> Carregar(Produto entidade)
        {
            List<ProdutoImagem> entidadesRetorno = new List<ProdutoImagem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProdutoImagem.* FROM ProdutoImagem WHERE ProdutoImagem.produtoId=@produtoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoImagem entidadeRetorno = new ProdutoImagem();
                PopulaProdutoImagem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagemTipo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProdutoImagem.</returns>
        public IEnumerable<ProdutoImagem> Carregar(ProdutoImagemTipo entidade)
        {
            List<ProdutoImagem> entidadesRetorno = new List<ProdutoImagem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProdutoImagem.* FROM ProdutoImagem WHERE ProdutoImagem.produtoImagemTipoId=@produtoImagemTipoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoImagem entidadeRetorno = new ProdutoImagem();
                PopulaProdutoImagem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de ProdutoImagem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ProdutoImagem.</returns>
        public IEnumerable<ProdutoImagem> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ProdutoImagem> entidadesRetorno = new List<ProdutoImagem>();

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
                sbOrder.Append(" ORDER BY produtoImagemId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProdutoImagem");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoImagem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProdutoImagem ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ProdutoImagem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProdutoImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ProdutoImagem.* FROM ProdutoImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoImagem entidadeRetorno = new ProdutoImagem();
                PopulaProdutoImagem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os ProdutoImagem existentes na base de dados.
        /// </summary>
        public IEnumerable<ProdutoImagem> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de ProdutoImagem na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de ProdutoImagem na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM ProdutoImagem");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um ProdutoImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProdutoImagem a ser populado(.</param>
        public static void PopulaProdutoImagem(IDataReader reader, ProdutoImagem entidade)
        {
            if (reader["produtoImagemId"] != DBNull.Value)
                entidade.ProdutoImagemId = Convert.ToInt32(reader["produtoImagemId"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.Produto = new Produto();
                entidade.Produto.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["produtoImagemTipoId"] != DBNull.Value)
            {
                entidade.ProdutoImagemTipo = new ProdutoImagemTipo();
                entidade.ProdutoImagemTipo.ProdutoImagemTipoId = Convert.ToInt32(reader["produtoImagemTipoId"].ToString());
            }


        }

    }
}
