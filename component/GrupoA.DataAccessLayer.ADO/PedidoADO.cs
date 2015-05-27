
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
    public partial class PedidoADO : ADOSuper, IPedidoDAL
    {

        /// <summary>
        /// Método que persiste um Pedido.
        /// </summary>
        /// <param name="entidade">Pedido contendo os dados a serem persistidos.</param>	
        public void Inserir(Pedido entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Pedido ");
            sbSQL.Append(" (usuarioId, dataHoraPedido, carrinhoId, pedidoStatusId, freteValor, valorPedido, pagamentoId, transportadoraServicoId, pedidoCodigo) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@usuarioId, @dataHoraPedido, @carrinhoId, @pedidoStatusId, @freteValor, @valorPedido, @pagamentoId, @transportadoraServicoId, @pedidoCodigo) ");

            sbSQL.Append(" ; SET @pedidoId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@pedidoId", DbType.Int32, 8);

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);

            _db.AddInParameter(command, "@dataHoraPedido", DbType.DateTime, entidade.DataHoraPedido);

            if (entidade.Carrinho != null)
                _db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.Carrinho.CarrinhoId);
            else
                _db.AddInParameter(command, "@carrinhoId", DbType.Int32, null);

            _db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatus.PedidoStatusId);

            _db.AddInParameter(command, "@freteValor", DbType.Decimal, entidade.FreteValor);

            _db.AddInParameter(command, "@valorPedido", DbType.Decimal, entidade.ValorPedido);

            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.Pagamento.PagamentoId);

            _db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServico.TransportadoraServicoId);

            if (entidade.PedidoCodigo != null)
                _db.AddInParameter(command, "@pedidoCodigo", DbType.Int32, entidade.PedidoCodigo);
            else
                _db.AddInParameter(command, "@pedidoCodigo", DbType.Int32, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.PedidoId = Convert.ToInt32(_db.GetParameterValue(command, "@pedidoId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um Pedido.
        /// </summary>
        /// <param name="entidade">Pedido contendo os dados a serem atualizados.</param>
        public void Atualizar(Pedido entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Pedido SET ");
            sbSQL.Append(" usuarioId=@usuarioId, dataHoraPedido=@dataHoraPedido, carrinhoId=@carrinhoId, pedidoStatusId=@pedidoStatusId, freteValor=@freteValor, valorPedido=@valorPedido, pagamentoId=@pagamentoId, transportadoraServicoId=@transportadoraServicoId, pedidoCodigo=@pedidoCodigo ");
            sbSQL.Append(" WHERE pedidoId=@pedidoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.Usuario.UsuarioId);
            _db.AddInParameter(command, "@dataHoraPedido", DbType.DateTime, entidade.DataHoraPedido);
            if (entidade.Carrinho != null)
                _db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.Carrinho.CarrinhoId);
            else
                _db.AddInParameter(command, "@carrinhoId", DbType.Int32, null);
            _db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatus.PedidoStatusId);
            _db.AddInParameter(command, "@freteValor", DbType.Decimal, entidade.FreteValor);
            _db.AddInParameter(command, "@valorPedido", DbType.Decimal, entidade.ValorPedido);
            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.Pagamento.PagamentoId);
            _db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServico.TransportadoraServicoId);
            if (entidade.PedidoCodigo != null)
                _db.AddInParameter(command, "@pedidoCodigo", DbType.Int32, entidade.PedidoCodigo);
            else
                _db.AddInParameter(command, "@pedidoCodigo", DbType.Int32, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Pedido da base de dados.
        /// </summary>
        /// <param name="entidade">Pedido a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Pedido entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Pedido ");
            sbSQL.Append("WHERE pedidoId=@pedidoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um Pedido.
        /// </summary>
        /// <param name="entidade">Pedido a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Pedido</returns>
        public Pedido Carregar(int pedidoId)
        {
            Pedido entidade = new Pedido();
            entidade.PedidoId = pedidoId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um Pedido.
        /// </summary>
        /// <param name="entidade">Pedido a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Pedido</returns>
        public Pedido Carregar(Pedido entidade)
        {

            Pedido entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Pedido WHERE pedidoId=@pedidoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que carrega um Pedido com suas dependências.
        /// </summary>
        /// <param name="entidade">Pedido a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Pedido</returns>
        public Pedido CarregarComDependencias(Pedido entidade)
        {

            Pedido entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Pedido.pedidoId, Pedido.usuarioId, Pedido.dataHoraPedido, Pedido.carrinhoId, Pedido.pedidoStatusId, Pedido.freteValor, Pedido.valorPedido, Pedido.pagamentoId, Pedido.transportadoraServicoId, Pedido.pedidoCodigo");
            sbSQL.Append(", pedidoEnderecoId, enderecoTipoId, municipioId, bairro, cep, complemento, logradouro, numero");
            sbSQL.Append(" FROM Pedido");
            sbSQL.Append(" LEFT JOIN PedidoEndereco ON Pedido.pedidoId=PedidoEndereco.pedidoEnderecoId");
            sbSQL.Append(" WHERE Pedido.pedidoId=@pedidoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@pedidoId", DbType.Int32, entidade.PedidoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadeRetorno.PedidoEndereco = new PedidoEndereco();
                PedidoEnderecoADO.PopulaPedidoEndereco(reader, entidadeRetorno.PedidoEndereco);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que retorna um Pedido.
        /// </summary>
        /// <param name="entidade">PedidoEndereco relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um Pedido.</returns>
        public Pedido Carregar(PedidoEndereco entidade)
        {
            Pedido entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido INNER JOIN PedidoEndereco ON Pedido.pedidoId=PedidoEndereco.pedidoId WHERE PedidoEndereco.pedidoEnderecoId=@pedidoEnderecoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoEnderecoId", DbType.Int32, entidade.PedidoEnderecoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">PedidoItem relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(PedidoItem entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido INNER JOIN PedidoItem ON Pedido.pedidoId=PedidoItem.pedidoId WHERE PedidoItem.pedidoItemId=@pedidoItemId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoItemId", DbType.Int32, entidade.PedidoItemId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(Promocao entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido INNER JOIN PedidoPromocaoCarrinho ON Pedido.pedidoId=PedidoPromocaoCarrinho.pedidoId WHERE PedidoPromocaoCarrinho.promocaoId=@promocaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">PedidoSituacao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(PedidoSituacao entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido INNER JOIN PedidoSituacao ON Pedido.pedidoId=PedidoSituacao.pedidoId WHERE PedidoSituacao.pedidoSituacaoId=@pedidoSituacaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoSituacaoId", DbType.Int32, entidade.PedidoSituacaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">Carrinho relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(Carrinho entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido WHERE Pedido.carrinhoId=@carrinhoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@carrinhoId", DbType.Int32, entidade.CarrinhoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna um Pedido.
        /// </summary>
        /// <param name="entidade">Pagamento relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna um Pedido.</returns>
        public Pedido Carregar(Pagamento entidade)
        {
            Pedido entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido WHERE Pedido.pagamentoId=@pagamentoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pagamentoId", DbType.Int32, entidade.PagamentoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">PedidoStatus relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(PedidoStatus entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido WHERE Pedido.pedidoStatusId=@pedidoStatusId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoStatusId", DbType.Int32, entidade.PedidoStatusId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">TransportadoraServico relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(TransportadoraServico entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido WHERE Pedido.transportadoraServicoId=@transportadoraServicoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@transportadoraServicoId", DbType.Int32, entidade.TransportadoraServicoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="entidade">Usuario relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Pedido.</returns>
        public IEnumerable<Pedido> Carregar(Usuario entidade)
        {
            List<Pedido> entidadesRetorno = new List<Pedido>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Pedido.* FROM Pedido WHERE Pedido.usuarioId=@usuarioId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, entidade.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Pedido.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Pedido.</returns>
        public IEnumerable<Pedido> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Pedido> entidadesRetorno = new List<Pedido>();

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
                sbOrder.Append(" ORDER BY pedidoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Pedido");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Pedido WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Pedido ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Pedido.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Pedido ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Pedido.* FROM Pedido ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Pedido entidadeRetorno = new Pedido();
                PopulaPedido(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Pedido existentes na base de dados.
        /// </summary>
        public IEnumerable<Pedido> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Pedido na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Pedido na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Pedido");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Pedido baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Pedido a ser populado(.</param>
        public static void PopulaPedido(IDataReader reader, Pedido entidade)
        {
            if (reader["pedidoId"] != DBNull.Value)
                entidade.PedidoId = Convert.ToInt32(reader["pedidoId"].ToString());

            if (reader["dataHoraPedido"] != DBNull.Value)
                entidade.DataHoraPedido = Convert.ToDateTime(reader["dataHoraPedido"].ToString());

            if (reader["freteValor"] != DBNull.Value)
                entidade.FreteValor = Convert.ToDecimal(reader["freteValor"].ToString());

            if (reader["valorPedido"] != DBNull.Value)
                entidade.ValorPedido = Convert.ToDecimal(reader["valorPedido"].ToString());

            if (reader["pedidoCodigo"] != DBNull.Value)
                entidade.PedidoCodigo = Convert.ToInt32(reader["pedidoCodigo"].ToString());

            if (reader["usuarioId"] != DBNull.Value)
            {
                entidade.Usuario = new Usuario();
                entidade.Usuario.UsuarioId = Convert.ToInt32(reader["usuarioId"].ToString());
            }

            if (reader["carrinhoId"] != DBNull.Value)
            {
                entidade.Carrinho = new Carrinho();
                entidade.Carrinho.CarrinhoId = Convert.ToInt32(reader["carrinhoId"].ToString());
            }

            if (reader["pedidoStatusId"] != DBNull.Value)
            {
                entidade.PedidoStatus = new PedidoStatus();
                entidade.PedidoStatus.PedidoStatusId = Convert.ToInt32(reader["pedidoStatusId"].ToString());
            }

            if (reader["pagamentoId"] != DBNull.Value)
            {
                entidade.Pagamento = new Pagamento();
                entidade.Pagamento.PagamentoId = Convert.ToInt32(reader["pagamentoId"].ToString());
            }

            if (reader["transportadoraServicoId"] != DBNull.Value)
            {
                entidade.TransportadoraServico = new TransportadoraServico();
                entidade.TransportadoraServico.TransportadoraServicoId = Convert.ToInt32(reader["transportadoraServicoId"].ToString());
            }
        }
    }
}