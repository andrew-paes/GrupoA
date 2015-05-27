
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
    public partial class PagamentoADO : ADOSuper, IPagamentoDAL
    {

        /// <summary>
        /// Método que persiste um Pagamento.
        /// </summary>
        /// <param name="entidade">Pagamento contendo os dados a serem persistidos.</param>	
        public void Inserir(Pagamento entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Pagamento ");
            sbSQL.Append(" (numeroParcelas, meioPagamentoId, codigoTransacao, codigoLegadoMeioPagamentoFaixa) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@numeroParcelas, @meioPagamentoId, @codigoTransacao, @codigoLegadoMeioPagamentoFaixa) ");

            sbSQL.Append(" ; SET @pagamentoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@pagamentoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@numeroParcelas", DbType.Int32, entidade.NumeroParcelas);

            if (entidade.MeioPagamento != null)
                _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamento.MeioPagamentoId);
            else
                _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, null);

            _db.AddInParameter(command, "@codigoTransacao", DbType.String, entidade.CodigoTransacao);

            if (entidade.CodigoLegadoMeioPagamentoFaixa != null)
                _db.AddInParameter(command, "@codigoLegadoMeioPagamentoFaixa", DbType.String, entidade.CodigoLegadoMeioPagamentoFaixa);
            else
                _db.AddInParameter(command, "@codigoLegadoMeioPagamentoFaixa", DbType.String, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.PagamentoId = Convert.ToInt32(_db.GetParameterValue(command, "@pagamentoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Pagamento.
        /// </summary>
        /// <param name="entidade">Pagamento contendo os dados a serem atualizados.</param>
        public void Atualizar(Pagamento entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Pagamento SET ");
            sbSQL.Append(" numeroParcelas=@numeroParcelas, meioPagamentoId=@meioPagamentoId, codigoTransacao=@codigoTransacao, codigoLegadoMeioPagamentoFaixa=@codigoLegadoMeioPagamentoFaixa ");
            sbSQL.Append(" WHERE pagamentoId=@pagamentoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.PagamentoId);
            _db.AddInParameter(command, "@numeroParcelas", DbType.Int32, entidade.NumeroParcelas);
            if (entidade.MeioPagamento != null)
                _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamento.MeioPagamentoId);
            else
                _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, null);
            _db.AddInParameter(command, "@codigoTransacao", DbType.String, entidade.CodigoTransacao);
            if (entidade.CodigoLegadoMeioPagamentoFaixa != null)
                _db.AddInParameter(command, "@codigoLegadoMeioPagamentoFaixa", DbType.String, entidade.CodigoLegadoMeioPagamentoFaixa);
            else
                _db.AddInParameter(command, "@codigoLegadoMeioPagamentoFaixa", DbType.String, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Pagamento da base de dados.
        /// </summary>
        /// <param name="entidade">Pagamento a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Pagamento entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Pagamento ");
            sbSQL.Append("WHERE pagamentoId=@pagamentoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.PagamentoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Pagamento.
        /// </summary>
        /// <param name="entidade">Pagamento a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Pagamento</returns>
        public Pagamento Carregar(int pagamentoId)
        {
            Pagamento entidade = new Pagamento();
            entidade.PagamentoId = pagamentoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Pagamento.
        /// </summary>
        /// <param name="entidade">Pagamento a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Pagamento</returns>
        public Pagamento Carregar(Pagamento entidade)
        {
            Pagamento entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Pagamento WHERE pagamentoId=@pagamentoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.PagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pagamento();
                PopulaPagamento(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que carrega um Pagamento com suas dependências.
        /// </summary>
        /// <param name="entidade">Pagamento a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Pagamento</returns>
        public Pagamento CarregarComDependencias(Pagamento entidade)
        {

            Pagamento entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Pagamento.pagamentoId, Pagamento.numeroParcelas, Pagamento.meioPagamentoId, Pagamento.codigoTransacao, Pagamento.codigoLegadoMeioPagamentoFaixa");
            sbSQL.Append(", pedidoId, usuarioId, dataHoraPedido, carrinhoId, pedidoStatusId, freteValor, valorPedido, transportadoraServicoId, pedidoCodigo");
            sbSQL.Append(" FROM Pagamento");
            sbSQL.Append(" LEFT JOIN Pedido ON Pagamento.pagamentoId=Pedido.pedidoId");
            sbSQL.Append(" WHERE Pagamento.pagamentoId=@pagamentoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.PagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pagamento();
                PopulaPagamento(reader, entidadeRetorno);
                entidadeRetorno.Pedido = new Pedido();
                PedidoADO.PopulaPedido(reader, entidadeRetorno.Pedido);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que retorna um Pagamento.
        /// </summary>
        /// <param name="entidade">Pedido relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um Pagamento.</returns>
        public Pagamento Carregar(Pedido entidade)
        {
            Pagamento entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pagamento.* FROM Pagamento INNER JOIN Pedido ON Pagamento.pagamentoId=Pedido.pagamentoId WHERE Pedido.pedidoId=@pedidoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pagamento();
                PopulaPagamento(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pagamento.
        /// </summary>
        /// <param name="entidade">MeioPagamento relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pagamento.</returns>
        public IEnumerable<Pagamento> Carregar(MeioPagamento entidade)
        {
            List<Pagamento> entidadesRetorno = new List<Pagamento>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pagamento.* FROM Pagamento WHERE Pagamento.meioPagamentoId=@meioPagamentoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@meioPagamentoId", DbType.Int32, entidade.MeioPagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pagamento entidadeRetorno = new Pagamento();
                PopulaPagamento(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Pagamento.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Pagamento.</returns>
        public IEnumerable<Pagamento> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Pagamento> entidadesRetorno = new List<Pagamento>();

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
                sbOrder.Append(" ORDER BY pagamentoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Pagamento");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Pagamento WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Pagamento ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Pagamento.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Pagamento ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Pagamento.* FROM Pagamento ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pagamento entidadeRetorno = new Pagamento();
                PopulaPagamento(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Pagamento existentes na base de dados.
        /// </summary>
        public IEnumerable<Pagamento> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Pagamento na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Pagamento na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Pagamento");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Pagamento baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Pagamento a ser populado(.</param>
        public static void PopulaPagamento(IDataReader reader, Pagamento entidade)
        {
            if (reader["pagamentoId"] != DBNull.Value)
                entidade.PagamentoId = Convert.ToInt32(reader["pagamentoId"].ToString());

            if (reader["numeroParcelas"] != DBNull.Value)
                entidade.NumeroParcelas = Convert.ToInt32(reader["numeroParcelas"].ToString());

            if (reader["codigoTransacao"] != DBNull.Value)
                entidade.CodigoTransacao = reader["codigoTransacao"].ToString();

            if (reader["codigoLegadoMeioPagamentoFaixa"] != DBNull.Value)
                entidade.CodigoLegadoMeioPagamentoFaixa = reader["codigoLegadoMeioPagamentoFaixa"].ToString();

            if (reader["meioPagamentoId"] != DBNull.Value)
            {
                entidade.MeioPagamento = new MeioPagamento();
                entidade.MeioPagamento.MeioPagamentoId = Convert.ToInt32(reader["meioPagamentoId"].ToString());
            }


        }

    }
}
