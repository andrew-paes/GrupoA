
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
    public partial class ClippingImagemADO
    {

        /// <summary>
        /// Método que retorna uma coleção de ClippingImagem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ClippingImagem.</returns>
        public IEnumerable<ClippingImagem> CarregarTodosArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ClippingImagem> entidadesRetorno = new List<ClippingImagem>();

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
                sbOrder.Append(" ORDER BY clippingImagemId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ClippingImagem");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ClippingImagem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ClippingImagem ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ClippingImagem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ClippingImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ClippingImagem.* FROM ClippingImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ClippingImagem entidadeRetorno = new ClippingImagem();
                PopulaClippingImagemArquivo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna popula um ClippingImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ClippingImagem a ser populado(.</param>
        public static void PopulaClippingImagemArquivo(IDataReader reader, ClippingImagem entidade)
        {
            if (reader["clippingImagemId"] != DBNull.Value)
                entidade.ClippingImagemId = Convert.ToInt32(reader["clippingImagemId"].ToString());

            if (reader["ordemApresentacao"] != DBNull.Value)
                entidade.OrdemApresentacao = Convert.ToInt32(reader["ordemApresentacao"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
                entidade.Arquivo = new ArquivoADO().Carregar(entidade.Arquivo);
            }

            if (reader["clippingId"] != DBNull.Value)
            {
                entidade.Clipping = new Clipping();
                entidade.Clipping.ClippingId = Convert.ToInt32(reader["clippingId"].ToString());
                //entidade.Clipping = new ClippingADO().Carregar(entidade.Clipping);
            }
        }

        /// <summary>
        /// Método que remove um ClippingImagem da base de dados.
        /// </summary>
        /// <param name="entidade">ClippingImagem a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirImagem(ClippingImagem entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            sbSQL.Append("DELETE FROM ClippingImagem ");
            sbSQL.Append("WHERE arquivoId=@arquivoId ");
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            _db.ExecuteNonQuery(command);

            new ArquivoADO().Excluir(entidade.Arquivo);

        }

        /// <summary>
        /// Método que carrega um ClippingImagem.
        /// </summary>
        /// <param name="entidade">ClippingImagem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ClippingImagem</returns>
        public ClippingImagem CarregarClippingImagem(ClippingImagem entidade)
        {

            ClippingImagem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ClippingImagem WHERE clippingId=@clippingId and arquivoId=@arquivoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.Clipping.ClippingId);
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ClippingImagem();
                PopulaClippingImagem(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        public ClippingImagem CarregarClippingImagem(Clipping entidade)
        {

            ClippingImagem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ClippingImagem WHERE clippingId=@clippingId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@clippingId", DbType.Int32, entidade.ClippingId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ClippingImagem();
                PopulaClippingImagem(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
