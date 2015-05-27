
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
    public partial class NoticiaImagemADO
    {


        /// <summary>
        /// Método que retorna uma coleção de NoticiaImagem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos NoticiaImagem.</returns>
        public IEnumerable<NoticiaImagem> CarregarTodosArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<NoticiaImagem> entidadesRetorno = new List<NoticiaImagem>();

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
                sbOrder.Append(" ORDER BY noticiaImagemId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM NoticiaImagem");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NoticiaImagem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM NoticiaImagem ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT NoticiaImagem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM NoticiaImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT NoticiaImagem.* FROM NoticiaImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                NoticiaImagem entidadeRetorno = new NoticiaImagem();
                PopulaNoticiaImagemArquivo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }	
		       

        /// <summary>
        /// Método que retorna popula um NoticiaImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">NoticiaImagem a ser populado(.</param>
        public static void PopulaNoticiaImagemArquivo(IDataReader reader, NoticiaImagem entidade)
        {
            if (reader["noticiaImagemId"] != DBNull.Value)
                entidade.NoticiaImagemId = Convert.ToInt32(reader["noticiaImagemId"].ToString());

            if (reader["ordemApresentacao"] != DBNull.Value)
                entidade.OrdemApresentacao = Convert.ToInt32(reader["ordemApresentacao"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
                entidade.Arquivo = new ArquivoADO().Carregar(entidade.Arquivo);
            }

            if (reader["noticiaId"] != DBNull.Value)
            {
                entidade.Noticia = new Noticia();
                entidade.Noticia.NoticiaId = Convert.ToInt32(reader["noticiaId"].ToString());
                //entidade.Noticia = new NoticiaADO().Carregar(entidade.Noticia);
            }
        }	

        /// <summary>
        /// Método que remove um NoticiaImagem da base de dados.
        /// </summary>
        /// <param name="entidade">NoticiaImagem a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirImagem(NoticiaImagem entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            sbSQL.Append("DELETE FROM NoticiaImagem ");
            sbSQL.Append("WHERE arquivoId=@arquivoId ");
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            _db.ExecuteNonQuery(command);

            new ArquivoADO().Excluir(entidade.Arquivo);

        }

        /// <summary>
        /// Método que carrega um NoticiaImagem.
        /// </summary>
        /// <param name="entidade">NoticiaImagem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>NoticiaImagem</returns>
        public NoticiaImagem CarregarNoticiaImagem(NoticiaImagem entidade)
        {

            NoticiaImagem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM NoticiaImagem WHERE noticiaId=@noticiaId and arquivoId=@arquivoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@noticiaId", DbType.Int32, entidade.Noticia.NoticiaId);
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new NoticiaImagem();
                PopulaNoticiaImagem(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
    }
}
