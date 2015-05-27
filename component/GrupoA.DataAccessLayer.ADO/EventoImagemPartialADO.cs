
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
	public partial class EventoImagemADO : ADOSuper, IEventoImagemDAL {
	

        /// <summary>
        /// Método que retorna uma coleção de EventoImagem correspondentes para um determinado Evento.
        /// </summary>
        /// <param name="evento">Objeto Evento que possui o identificador a ser pesquisado.</param>
        ///  <returns>Retorna um List contendo EventoImagem.</returns>
        public IEnumerable<EventoImagem> CarregarTodosPorEvento(Evento evento)
        {

            List<EventoImagem> entidadesRetorno = new List<EventoImagem>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM EventoImagem WHERE eventoId=@eventoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@eventoId", DbType.Int32, evento.EventoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EventoImagem entidadeRetorno = new EventoImagem();
                PopulaEventoImagem(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }	

        /// <summary>
        /// Método que retorna uma coleção de EventoImagem.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos EventoImagem.</returns>
        public IEnumerable<EventoImagem> CarregarTodosArquivos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<EventoImagem> entidadesRetorno = new List<EventoImagem>();

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
                sbOrder.Append(" ORDER BY eventoImagemId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM EventoImagem");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EventoImagem WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM EventoImagem ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT EventoImagem.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM EventoImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT EventoImagem.* FROM EventoImagem ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EventoImagem entidadeRetorno = new EventoImagem();
                PopulaEventoImagemArquivo(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna popula um EventoImagem baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">EventoImagem a ser populado(.</param>
        public static void PopulaEventoImagemArquivo(IDataReader reader, EventoImagem entidade)
        {
            if (reader["eventoImagemId"] != DBNull.Value)
                entidade.EventoImagemId = Convert.ToInt32(reader["eventoImagemId"].ToString());

            if (reader["ordemApresentacao"] != DBNull.Value)
                entidade.OrdemApresentacao = Convert.ToInt32(reader["ordemApresentacao"].ToString());

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
                entidade.Arquivo = new ArquivoADO().Carregar(entidade.Arquivo);
            }

            if (reader["eventoId"] != DBNull.Value)
            {
                entidade.Evento = new Evento();
                entidade.Evento.EventoId = Convert.ToInt32(reader["eventoId"].ToString());
                //entidade.Evento = new EventoADO().Carregar(entidade.Evento);
            }
        }

        /// <summary>
        /// Método que remove um EventoImagem da base de dados.
        /// </summary>
        /// <param name="entidade">EventoImagem a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Arquivo arquivo)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM EventoImagem ");
            sbSQL.Append("WHERE arquivoId=@arquivoId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@arquivoId", DbType.Int32, arquivo.ArquivoId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um EventoImagem.
        /// </summary>
        /// <param name="entidade">EventoImagem a ser carregado (somente o identificador é necessário).</param>
        /// <returns>EventoImagem</returns>
        public EventoImagem CarregarEventoImagem(EventoImagem entidade)
        {

            EventoImagem entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM EventoImagem WHERE eventoId=@eventoId and arquivoId=@arquivoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@eventoId", DbType.Int32, entidade.Evento.EventoId);
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new EventoImagem();
                PopulaEventoImagem(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }
	}
}
		