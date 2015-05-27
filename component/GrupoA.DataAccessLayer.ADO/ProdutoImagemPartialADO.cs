
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
        /// Método que carrega um ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProdutoImagem</returns>
        public IList<ProdutoImagem> CarregarProdutoImagens(ProdutoImagem entidade)
        {

            List<ProdutoImagem> entidadesRetorno = new List<ProdutoImagem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProdutoImagem WHERE produtoId=@produtoId ");
            if (entidade.ProdutoImagemTipo != null)
                sbSQL.Append(" and produtoImagemTipoId = @produtoImagemTipoId ");
            if (entidade.Arquivo != null)
                sbSQL.Append(" and arquivoId = @arquivoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);
            if (entidade.ProdutoImagemTipo != null)
                _db.AddInParameter(command, "@produtoImagemTipoId", DbType.Int32, entidade.ProdutoImagemTipo.ProdutoImagemTipoId);
            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProdutoImagem entidadeRetorno = new ProdutoImagem();
                PopulaProdutoImagens(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// Método que persiste um ProdutoImagem.
        /// </summary>
        /// <param name="entidade">ProdutoImagem contendo os dados a serem persistidos.</param>	
        public void InserirProdutoImagem(ProdutoImagem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProdutoImagem ");
            sbSQL.Append(" (arquivoId, produtoId, produtoImagemTipoId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@arquivoId, @produtoId, @produtoImagemTipoId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

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
        public void ExcluirProdutoImagem(ProdutoImagem entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProdutoImagem ");
            sbSQL.Append("WHERE produtoId=@produtoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@produtoId", DbType.Int32, entidade.Produto.ProdutoId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que retorna popula um ProdutoImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProdutoImagem a ser populado(.</param>
        public static void PopulaProdutoImagens(IDataReader reader, ProdutoImagem entidade)
        {
            if (reader["produtoImagemId"] != DBNull.Value)
                entidade.ProdutoImagemId = Convert.ToInt32(reader["produtoImagemId"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
                entidade.Arquivo = new ArquivoADO().Carregar(entidade.Arquivo);
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
