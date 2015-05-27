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
    public partial class ProdutoADO : ADOSuper, IProdutoDAL
    {
        /// <summary>
        /// Método que persiste um Produto.
        /// </summary>
        /// <param name="entidade">Produto contendo os dados a serem persistidos.</param>	
        public void Inserir(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Produto ");
            sbSQL.Append(" (produtoId, produtoTipoId, disponivel, fabricanteId, valorUnitario, valorOferta, codigoEAN13, codigoProduto, exibirSite, nomeProduto, utilizaFrete, peso) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@produtoId, @produtoTipoId, @disponivel, @fabricanteId, @valorUnitario, @valorOferta, @codigoEAN13, @codigoProduto, @exibirSite, @nomeProduto, @utilizaFrete, @peso) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            _db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipo.ProdutoTipoId);

            _db.AddInParameter(command, "@disponivel", DbType.Int32, entidade.Disponivel);

            _db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.Fabricante.FabricanteId);

            _db.AddInParameter(command, "@valorUnitario", DbType.Decimal, entidade.ValorUnitario);

            if (entidade.ValorOferta != null)
                _db.AddInParameter(command, "@valorOferta", DbType.Decimal, entidade.ValorOferta);
            else
                _db.AddInParameter(command, "@valorOferta", DbType.Decimal, null);

            if (entidade.CodigoEAN13 != null)
                _db.AddInParameter(command, "@codigoEAN13", DbType.String, entidade.CodigoEAN13);
            else
                _db.AddInParameter(command, "@codigoEAN13", DbType.String, null);

            if (entidade.CodigoProduto != null)
                _db.AddInParameter(command, "@codigoProduto", DbType.String, entidade.CodigoProduto);
            else
                _db.AddInParameter(command, "@codigoProduto", DbType.String, null);

            _db.AddInParameter(command, "@exibirSite", DbType.Int32, entidade.ExibirSite);

            _db.AddInParameter(command, "@nomeProduto", DbType.String, entidade.NomeProduto);

            _db.AddInParameter(command, "@utilizaFrete", DbType.Int32, entidade.UtilizaFrete);

            _db.AddInParameter(command, "@peso", DbType.Decimal, entidade.Peso);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um Produto.
        /// </summary>
        /// <param name="entidade">Produto contendo os dados a serem atualizados.</param>
        public void Atualizar(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Produto SET ");
            sbSQL.Append(" produtoTipoId=@produtoTipoId, disponivel=@disponivel, fabricanteId=@fabricanteId, valorUnitario=@valorUnitario, valorOferta=@valorOferta, codigoEAN13=@codigoEAN13, codigoProduto=@codigoProduto, exibirSite=@exibirSite, homologado=@homologado, nomeProduto=@nomeProduto, utilizaFrete=@utilizaFrete, peso=@peso ");
            sbSQL.Append(" WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);
            _db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipo.ProdutoTipoId);
            _db.AddInParameter(command, "@disponivel", DbType.Int32, entidade.Disponivel);
            _db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.Fabricante.FabricanteId);
            _db.AddInParameter(command, "@valorUnitario", DbType.Decimal, entidade.ValorUnitario);
            if (entidade.ValorOferta != null)
                _db.AddInParameter(command, "@valorOferta", DbType.Decimal, entidade.ValorOferta);
            else
                _db.AddInParameter(command, "@valorOferta", DbType.Decimal, null);
            if (entidade.CodigoEAN13 != null)
                _db.AddInParameter(command, "@codigoEAN13", DbType.String, entidade.CodigoEAN13);
            else
                _db.AddInParameter(command, "@codigoEAN13", DbType.String, null);
            if (entidade.CodigoProduto != null)
                _db.AddInParameter(command, "@codigoProduto", DbType.String, entidade.CodigoProduto);
            else
                _db.AddInParameter(command, "@codigoProduto", DbType.String, null);

            _db.AddInParameter(command, "@exibirSite", DbType.Int32, entidade.ExibirSite);
            _db.AddInParameter(command, "@homologado", DbType.Int32, entidade.Homologado);
            _db.AddInParameter(command, "@nomeProduto", DbType.String, entidade.NomeProduto);
            _db.AddInParameter(command, "@utilizaFrete", DbType.Int32, entidade.UtilizaFrete);
            _db.AddInParameter(command, "@peso", DbType.Decimal, entidade.Peso);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que remove um Produto da base de dados.
        /// </summary>
        /// <param name="entidade">Produto a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Produto entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Produto ");
            sbSQL.Append("WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);


            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Método que carrega um Produto.
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public Produto Carregar(Produto entidade)
        {

            Produto entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Produto WHERE produtoId=@produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que carrega um Produto com suas dependências.
        /// </summary>
        /// <param name="entidade">Produto a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Produto</returns>
        public Produto CarregarComDependencias(Produto entidade)
        {

            Produto entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Produto.produtoId, Produto.produtoTipoId, Produto.disponivel, Produto.homologado, Produto.fabricanteId, Produto.valorUnitario, Produto.valorOferta, Produto.codigoEAN13, Produto.codigoProduto, Produto.exibirSite, Produto.homologado, Produto.nomeProduto, Produto.utilizaFrete, Produto.peso");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(" FROM Produto");
            sbSQL.Append(" INNER JOIN Conteudo ON Produto.produtoId=Conteudo.conteudoId");
            sbSQL.Append(" WHERE Produto.produtoId=@produtoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.ProdutoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadeRetorno.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">AvisoDisponibilidade relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(AvisoDisponibilidade entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN AvisoDisponibilidade ON Produto.produtoId=AvisoDisponibilidade.produtoId WHERE AvisoDisponibilidade.avisoDisponibilidadeId=@avisoDisponibilidadeId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@avisoDisponibilidadeId", DbType.Int32, entidade.AvisoDisponibilidadeId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">CarrinhoItem relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(CarrinhoItem entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN CarrinhoItem ON Produto.produtoId=CarrinhoItem.produtoId WHERE CarrinhoItem.carrinhoItemId=@carrinhoItemId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@carrinhoItemId", DbType.Int32, entidade.CarrinhoItemId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">CompraConjunta relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(CompraConjunta entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN CompraConjunta ON Produto.produtoId=CompraConjunta.produtoId WHERE CompraConjunta.compraConjuntaId=@compraConjuntaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">NotificacaoDisponibilidade relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(NotificacaoDisponibilidade entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN NotificacaoDisponibilidade ON Produto.produtoId=NotificacaoDisponibilidade.produtoId WHERE NotificacaoDisponibilidade.notificacaoDisponibilidadeId=@notificacaoDisponibilidadeId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@notificacaoDisponibilidadeId", DbType.Int32, entidade.NotificacaoDisponibilidadeId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">PedidoItem relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(PedidoItem entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN PedidoItem ON Produto.produtoId=PedidoItem.produtoId WHERE PedidoItem.pedidoItemId=@pedidoItemId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@pedidoItemId", DbType.Int32, entidade.PedidoItemId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">Categoria relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(Categoria entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN ProdutoCategoria ON Produto.produtoId=ProdutoCategoria.produtoId WHERE ProdutoCategoria.categoriaId=@categoriaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@categoriaId", DbType.Int32, entidade.CategoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">ProdutoImagem relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(ProdutoImagem entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN ProdutoImagem ON Produto.produtoId=ProdutoImagem.produtoId WHERE ProdutoImagem.produtoImagemId=@produtoImagemId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoImagemId", DbType.Int32, entidade.ProdutoImagemId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">Selo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(Selo entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN ProdutoSelo ON Produto.produtoId=ProdutoSelo.produtoId WHERE ProdutoSelo.seloId=@seloId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@seloId", DbType.Int32, entidade.SeloId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">Promocao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(Promocao entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto INNER JOIN PromocaoProduto ON Produto.produtoId=PromocaoProduto.produtoId WHERE PromocaoProduto.promocaoId=@promocaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@promocaoId", DbType.Int32, entidade.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">Fabricante relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(Fabricante entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto WHERE Produto.fabricanteId=@fabricanteId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@fabricanteId", DbType.Int32, entidade.FabricanteId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="entidade">ProdutoTipo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Produto.</returns>
        public IEnumerable<Produto> Carregar(ProdutoTipo entidade)
        {
            List<Produto> entidadesRetorno = new List<Produto>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Produto.* FROM Produto WHERE Produto.produtoTipoId=@produtoTipoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@produtoTipoId", DbType.Int32, entidade.ProdutoTipoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Produto.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Produto.</returns>
        public IEnumerable<Produto> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Produto> entidadesRetorno = new List<Produto>();

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
                sbOrder.Append(" ORDER BY produtoId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Produto");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Produto WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Produto ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Produto.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Produto ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Produto.* FROM Produto ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Produto entidadeRetorno = new Produto();
                PopulaProduto(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Produto existentes na base de dados.
        /// </summary>
        public IEnumerable<Produto> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Produto na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Produto na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Produto");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Produto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Produto a ser populado(.</param>
        public static void PopulaProduto(IDataReader reader, Produto entidade)
        {
            if (reader["disponivel"] != DBNull.Value)
                entidade.Disponivel = Convert.ToBoolean(reader["disponivel"].ToString());

            if (reader["valorUnitario"] != DBNull.Value)
                entidade.ValorUnitario = Convert.ToDecimal(reader["valorUnitario"].ToString());

            if (reader["valorOferta"] != DBNull.Value)
                entidade.ValorOferta = Convert.ToDecimal(reader["valorOferta"].ToString());

            if (reader["codigoEAN13"] != DBNull.Value)
                entidade.CodigoEAN13 = reader["codigoEAN13"].ToString();

            if (reader["codigoProduto"] != DBNull.Value)
                entidade.CodigoProduto = reader["codigoProduto"].ToString();

            if (reader["exibirSite"] != DBNull.Value)
                entidade.ExibirSite = Convert.ToBoolean(reader["exibirSite"].ToString());

            if (reader["homologado"] != DBNull.Value)
                entidade.Homologado = Convert.ToBoolean(reader["homologado"].ToString());

            if (reader["nomeProduto"] != DBNull.Value)
                entidade.NomeProduto = reader["nomeProduto"].ToString();

            if (reader["utilizaFrete"] != DBNull.Value)
                entidade.UtilizaFrete = Convert.ToBoolean(reader["utilizaFrete"].ToString());

            if (reader["peso"] != DBNull.Value)
                entidade.Peso = Convert.ToDecimal(reader["peso"].ToString());

            if (reader["produtoId"] != DBNull.Value)
            {
                entidade.ProdutoId = Convert.ToInt32(reader["produtoId"].ToString());
            }

            if (reader["produtoTipoId"] != DBNull.Value)
            {
                entidade.ProdutoTipo = new ProdutoTipo();
                entidade.ProdutoTipo.ProdutoTipoId = Convert.ToInt32(reader["produtoTipoId"].ToString());
            }

            if (reader["fabricanteId"] != DBNull.Value)
            {
                entidade.Fabricante = new Fabricante();
                entidade.Fabricante.FabricanteId = Convert.ToInt32(reader["fabricanteId"].ToString());
            }


        }

    }
}
