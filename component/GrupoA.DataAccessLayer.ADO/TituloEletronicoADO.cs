
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
    public partial class TituloEletronicoADO : ADOSuper, ITituloEletronicoDAL
    {
        /// <summary>
        /// Método que persiste um TituloEletronico.
        /// </summary>
        /// <param name="entidade">TituloEletronico contendo os dados a serem persistidos.</param>	
        public void Inserir(TituloEletronico entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO TituloEletronico ");
            sbSQL.Append(" (tituloEletronicoId, isbn13, tituloId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@tituloEletronicoId, @isbn13, @tituloId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);

            _db.AddInParameter(command, "@isbn13", DbType.String, entidade.Isbn13);

            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um TituloEletronico.
        /// </summary>
        /// <param name="entidade">TituloEletronico contendo os dados a serem atualizados.</param>
        public void Atualizar(TituloEletronico entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE TituloEletronico SET ");
            sbSQL.Append(" isbn13=@isbn13, tituloId=@tituloId ");
            sbSQL.Append(" WHERE tituloEletronicoId=@tituloEletronicoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);
            _db.AddInParameter(command, "@isbn13", DbType.String, entidade.Isbn13);
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.Titulo.TituloId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um TituloEletronico da base de dados.
        /// </summary>
        /// <param name="entidade">TituloEletronico a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(TituloEletronico entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM TituloEletronico ");
            sbSQL.Append("WHERE tituloEletronicoId=@tituloEletronicoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um TituloEletronico.
        /// </summary>
        /// <param name="entidade">TituloEletronico a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloEletronico</returns>
        public TituloEletronico Carregar(TituloEletronico entidade)
        {

            TituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloEletronico WHERE tituloEletronicoId=@tituloEletronicoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que carrega um TituloEletronico com suas dependências.
        /// </summary>
        /// <param name="entidade">TituloEletronico a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloEletronico</returns>
        public TituloEletronico CarregarComDependencias(TituloEletronico entidade)
        {
            TituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT TituloEletronico.tituloEletronicoId, TituloEletronico.isbn13, TituloEletronico.tituloId");
            sbSQL.Append(", produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, homologado, nomeProduto, utilizaFrete, peso");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(" FROM TituloEletronico");
            sbSQL.Append(" INNER JOIN Produto ON TituloEletronico.tituloEletronicoId=Produto.produtoId");
            sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
            sbSQL.Append(" WHERE TituloEletronico.tituloEletronicoId=@tituloEletronicoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, entidade.TituloEletronicoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
                entidadeRetorno.Produto = new Produto();
                ProdutoADO.PopulaProduto(reader, entidadeRetorno.Produto);
                entidadeRetorno.Produto.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Produto.Conteudo);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloEletronico.
        /// </summary>
        /// <param name="entidade">CapituloEletronico relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de TituloEletronico.</returns>
        public IEnumerable<TituloEletronico> Carregar(CapituloEletronico entidade)
        {
            List<TituloEletronico> entidadesRetorno = new List<TituloEletronico>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloEletronico.* FROM TituloEletronico INNER JOIN CapituloEletronico ON TituloEletronico.tituloEletronicoId=CapituloEletronico.tituloEletronicoId WHERE CapituloEletronico.capituloEletronicoId=@capituloEletronicoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloEletronico entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloEletronico.
        /// </summary>
        /// <param name="entidade">TituloEletronicoAluguel relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de TituloEletronico.</returns>
        public IEnumerable<TituloEletronico> Carregar(TituloEletronicoAluguel entidade)
        {
            List<TituloEletronico> entidadesRetorno = new List<TituloEletronico>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloEletronico.* FROM TituloEletronico INNER JOIN TituloEletronicoAluguel ON TituloEletronico.tituloEletronicoId=TituloEletronicoAluguel.tituloEletronicoId WHERE TituloEletronicoAluguel.tituloEletronicoAluguelId=@tituloEletronicoAluguelId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloEletronicoAluguelId", DbType.Int32, entidade.TituloEletronicoAluguelId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloEletronico entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna um TituloEletronico.
        /// </summary>
        /// <param name="entidade">Titulo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um TituloEletronico.</returns>
        public TituloEletronico Carregar(Titulo entidade)
        {
            TituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TituloEletronico.* FROM TituloEletronico WHERE TituloEletronico.tituloId=@tituloId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloId", DbType.Int32, entidade.TituloId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna uma coleção de TituloEletronico.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos TituloEletronico.</returns>
        public IEnumerable<TituloEletronico> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {
            List<TituloEletronico> entidadesRetorno = new List<TituloEletronico>();

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
                sbOrder.Append(" ORDER BY tituloEletronicoId");
            }

            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM TituloEletronico");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloEletronico WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM TituloEletronico ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT TituloEletronico.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM TituloEletronico ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT TituloEletronico.* FROM TituloEletronico ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                TituloEletronico entidadeRetorno = new TituloEletronico();
                PopulaTituloEletronico(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que retorna todas os TituloEletronico existentes na base de dados.
        /// </summary>
        public IEnumerable<TituloEletronico> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de TituloEletronico na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de TituloEletronico na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM TituloEletronico");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um TituloEletronico baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">TituloEletronico a ser populado(.</param>
        public static void PopulaTituloEletronico(IDataReader reader, TituloEletronico entidade)
        {
            if (reader["isbn13"] != DBNull.Value)
                entidade.Isbn13 = reader["isbn13"].ToString();

            if (reader["tituloEletronicoId"] != DBNull.Value)
            {
                entidade.TituloEletronicoId = Convert.ToInt32(reader["tituloEletronicoId"].ToString());
            }

            if (reader["tituloId"] != DBNull.Value)
            {
                entidade.Titulo = new Titulo();
                entidade.Titulo.TituloId = Convert.ToInt32(reader["tituloId"].ToString());
            }
        }
    }
}