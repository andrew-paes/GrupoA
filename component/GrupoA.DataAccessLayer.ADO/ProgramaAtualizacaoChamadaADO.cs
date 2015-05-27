
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
    public partial class ProgramaAtualizacaoChamadaADO : ADOSuper, IProgramaAtualizacaoChamadaDAL
    {

        /// <summary>
        /// Método que persiste um ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada contendo os dados a serem persistidos.</param>	
        public void Inserir(ProgramaAtualizacaoChamada entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProgramaAtualizacaoChamada ");
            sbSQL.Append(" (ativo, primeiraChamadaTitulo, primeiraChamadaTexto, primeiraChamadaUrl, primeiraChamadaTargetBlank, segundaChamadaTitulo, segundaChamadaTexto, segundaChamadaUrl, segundaChamadaTargetBlank, arquivoIdImagem) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@ativo, @primeiraChamadaTitulo, @primeiraChamadaTexto, @primeiraChamadaUrl, @primeiraChamadaTargetBlank, @segundaChamadaTitulo, @segundaChamadaTexto, @segundaChamadaUrl, @segundaChamadaTargetBlank, @arquivoIdImagem) ");

            sbSQL.Append(" ; SET @programaAtualizacaoChamadaId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddOutParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, 8);

            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

            _db.AddInParameter(command, "@primeiraChamadaTitulo", DbType.String, entidade.PrimeiraChamadaTitulo);

            _db.AddInParameter(command, "@primeiraChamadaTexto", DbType.String, entidade.PrimeiraChamadaTexto);

            if (entidade.PrimeiraChamadaUrl != null)
                _db.AddInParameter(command, "@primeiraChamadaUrl", DbType.String, entidade.PrimeiraChamadaUrl);
            else
                _db.AddInParameter(command, "@primeiraChamadaUrl", DbType.String, null);

            _db.AddInParameter(command, "@primeiraChamadaTargetBlank", DbType.Int32, entidade.PrimeiraChamadaTargetBlank);

            if (entidade.SegundaChamadaTitulo != null)
                _db.AddInParameter(command, "@segundaChamadaTitulo", DbType.String, entidade.SegundaChamadaTitulo);
            else
                _db.AddInParameter(command, "@segundaChamadaTitulo", DbType.String, null);

            if (entidade.SegundaChamadaTexto != null)
                _db.AddInParameter(command, "@segundaChamadaTexto", DbType.String, entidade.SegundaChamadaTexto);
            else
                _db.AddInParameter(command, "@segundaChamadaTexto", DbType.String, null);

            if (entidade.SegundaChamadaUrl != null)
                _db.AddInParameter(command, "@segundaChamadaUrl", DbType.String, entidade.SegundaChamadaUrl);
            else
                _db.AddInParameter(command, "@segundaChamadaUrl", DbType.String, null);

            if (entidade.SegundaChamadaTargetBlank != null)
                _db.AddInParameter(command, "@segundaChamadaTargetBlank", DbType.Int32, entidade.SegundaChamadaTargetBlank);
            else
                _db.AddInParameter(command, "@segundaChamadaTargetBlank", DbType.Int32, null);


            if (entidade.ArquivoImagem != null)
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);


            // Executa a query.
            _db.ExecuteNonQuery(command);

            entidade.ProgramaAtualizacaoChamadaId = Convert.ToInt32(_db.GetParameterValue(command, "@programaAtualizacaoChamadaId"));

        }

        /// <summary>
        /// Método que atualiza os dados de um ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada contendo os dados a serem atualizados.</param>
        public void Atualizar(ProgramaAtualizacaoChamada entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE ProgramaAtualizacaoChamada SET ");
            sbSQL.Append(" ativo=@ativo, primeiraChamadaTitulo=@primeiraChamadaTitulo, primeiraChamadaTexto=@primeiraChamadaTexto, primeiraChamadaUrl=@primeiraChamadaUrl, primeiraChamadaTargetBlank=@primeiraChamadaTargetBlank, segundaChamadaTitulo=@segundaChamadaTitulo, segundaChamadaTexto=@segundaChamadaTexto, segundaChamadaUrl=@segundaChamadaUrl, segundaChamadaTargetBlank=@segundaChamadaTargetBlank, arquivoIdImagem=@arquivoIdImagem ");
            sbSQL.Append(" WHERE programaAtualizacaoChamadaId=@programaAtualizacaoChamadaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, entidade.ProgramaAtualizacaoChamadaId);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);
            _db.AddInParameter(command, "@primeiraChamadaTitulo", DbType.String, entidade.PrimeiraChamadaTitulo);
            _db.AddInParameter(command, "@primeiraChamadaTexto", DbType.String, entidade.PrimeiraChamadaTexto);
            if (entidade.PrimeiraChamadaUrl != null)
                _db.AddInParameter(command, "@primeiraChamadaUrl", DbType.String, entidade.PrimeiraChamadaUrl);
            else
                _db.AddInParameter(command, "@primeiraChamadaUrl", DbType.String, null);
            _db.AddInParameter(command, "@primeiraChamadaTargetBlank", DbType.Int32, entidade.PrimeiraChamadaTargetBlank);
            if (entidade.SegundaChamadaTitulo != null)
                _db.AddInParameter(command, "@segundaChamadaTitulo", DbType.String, entidade.SegundaChamadaTitulo);
            else
                _db.AddInParameter(command, "@segundaChamadaTitulo", DbType.String, null);
            if (entidade.SegundaChamadaTexto != null)
                _db.AddInParameter(command, "@segundaChamadaTexto", DbType.String, entidade.SegundaChamadaTexto);
            else
                _db.AddInParameter(command, "@segundaChamadaTexto", DbType.String, null);
            if (entidade.SegundaChamadaUrl != null)
                _db.AddInParameter(command, "@segundaChamadaUrl", DbType.String, entidade.SegundaChamadaUrl);
            else
                _db.AddInParameter(command, "@segundaChamadaUrl", DbType.String, null);
            if (entidade.SegundaChamadaTargetBlank != null)
                _db.AddInParameter(command, "@segundaChamadaTargetBlank", DbType.Int32, entidade.SegundaChamadaTargetBlank);
            else
                _db.AddInParameter(command, "@segundaChamadaTargetBlank", DbType.Int32, null);
            if (entidade.ArquivoImagem != null)
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, entidade.ArquivoImagem.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoIdImagem", DbType.Int32, null);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um ProgramaAtualizacaoChamada da base de dados.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(ProgramaAtualizacaoChamada entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProgramaAtualizacaoChamada ");
            sbSQL.Append("WHERE programaAtualizacaoChamadaId=@programaAtualizacaoChamadaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, entidade.ProgramaAtualizacaoChamadaId);


            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Método que carrega um ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProgramaAtualizacaoChamada</returns>
        public ProgramaAtualizacaoChamada Carregar(int programaAtualizacaoChamadaId)
        {
            ProgramaAtualizacaoChamada entidade = new ProgramaAtualizacaoChamada();
            entidade.ProgramaAtualizacaoChamadaId = programaAtualizacaoChamadaId;
            return Carregar(entidade);

        }


        /// <summary>
        /// Método que carrega um ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoChamada a ser carregado (somente o identificador é necessário).</param>
        /// <returns>ProgramaAtualizacaoChamada</returns>
        public ProgramaAtualizacaoChamada Carregar(ProgramaAtualizacaoChamada entidade)
        {

            ProgramaAtualizacaoChamada entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM ProgramaAtualizacaoChamada WHERE programaAtualizacaoChamadaId=@programaAtualizacaoChamadaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, entidade.ProgramaAtualizacaoChamadaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProgramaAtualizacaoChamada();
                PopulaProgramaAtualizacaoChamada(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }



        /// <summary>
        /// Método que retorna uma coleção de ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="entidade">ProgramaAtualizacaoPagina relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProgramaAtualizacaoChamada.</returns>
        public IEnumerable<ProgramaAtualizacaoChamada> Carregar(ProgramaAtualizacaoPagina entidade)
        {
            List<ProgramaAtualizacaoChamada> entidadesRetorno = new List<ProgramaAtualizacaoChamada>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProgramaAtualizacaoChamada.* FROM ProgramaAtualizacaoChamada INNER JOIN ProgramaAtualizacaoChamadaLocalizacao ON ProgramaAtualizacaoChamada.programaAtualizacaoChamadaId=ProgramaAtualizacaoChamadaLocalizacao.programaAtualizacaoChamadaId WHERE ProgramaAtualizacaoChamadaLocalizacao.programaAtualizacaoPaginaId=@programaAtualizacaoPaginaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, entidade.ProgramaAtualizacaoPaginaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProgramaAtualizacaoChamada entidadeRetorno = new ProgramaAtualizacaoChamada();
                PopulaProgramaAtualizacaoChamada(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de ProgramaAtualizacaoChamada.</returns>
        public IEnumerable<ProgramaAtualizacaoChamada> Carregar(Arquivo entidade)
        {
            List<ProgramaAtualizacaoChamada> entidadesRetorno = new List<ProgramaAtualizacaoChamada>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT ProgramaAtualizacaoChamada.* FROM ProgramaAtualizacaoChamada WHERE ProgramaAtualizacaoChamada.arquivoId=@arquivoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProgramaAtualizacaoChamada entidadeRetorno = new ProgramaAtualizacaoChamada();
                PopulaProgramaAtualizacaoChamada(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de ProgramaAtualizacaoChamada.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ProgramaAtualizacaoChamada.</returns>
        public IEnumerable<ProgramaAtualizacaoChamada> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<ProgramaAtualizacaoChamada> entidadesRetorno = new List<ProgramaAtualizacaoChamada>();

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
                sbOrder.Append(" ORDER BY programaAtualizacaoChamadaId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM ProgramaAtualizacaoChamada");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProgramaAtualizacaoChamada WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM ProgramaAtualizacaoChamada ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT ProgramaAtualizacaoChamada.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM ProgramaAtualizacaoChamada ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT ProgramaAtualizacaoChamada.* FROM ProgramaAtualizacaoChamada ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProgramaAtualizacaoChamada entidadeRetorno = new ProgramaAtualizacaoChamada();
                PopulaProgramaAtualizacaoChamada(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os ProgramaAtualizacaoChamada existentes na base de dados.
        /// </summary>
        public IEnumerable<ProgramaAtualizacaoChamada> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de ProgramaAtualizacaoChamada na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de ProgramaAtualizacaoChamada na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM ProgramaAtualizacaoChamada");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um ProgramaAtualizacaoChamada baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ProgramaAtualizacaoChamada a ser populado(.</param>
        public static void PopulaProgramaAtualizacaoChamada(IDataReader reader, ProgramaAtualizacaoChamada entidade)
        {
            if (reader["programaAtualizacaoChamadaId"] != DBNull.Value)
                entidade.ProgramaAtualizacaoChamadaId = Convert.ToInt32(reader["programaAtualizacaoChamadaId"].ToString());

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["primeiraChamadaTitulo"] != DBNull.Value)
                entidade.PrimeiraChamadaTitulo = reader["primeiraChamadaTitulo"].ToString();

            if (reader["primeiraChamadaTexto"] != DBNull.Value)
                entidade.PrimeiraChamadaTexto = reader["primeiraChamadaTexto"].ToString();

            if (reader["primeiraChamadaUrl"] != DBNull.Value)
                entidade.PrimeiraChamadaUrl = reader["primeiraChamadaUrl"].ToString();

            if (reader["primeiraChamadaTargetBlank"] != DBNull.Value)
                entidade.PrimeiraChamadaTargetBlank = Convert.ToBoolean(reader["primeiraChamadaTargetBlank"].ToString());

            if (reader["segundaChamadaTitulo"] != DBNull.Value)
                entidade.SegundaChamadaTitulo = reader["segundaChamadaTitulo"].ToString();

            if (reader["segundaChamadaTexto"] != DBNull.Value)
                entidade.SegundaChamadaTexto = reader["segundaChamadaTexto"].ToString();

            if (reader["segundaChamadaUrl"] != DBNull.Value)
                entidade.SegundaChamadaUrl = reader["segundaChamadaUrl"].ToString();

            if (reader["segundaChamadaTargetBlank"] != DBNull.Value)
                entidade.SegundaChamadaTargetBlank = Convert.ToBoolean(reader["segundaChamadaTargetBlank"].ToString());

            if (reader["arquivoIdImagem"] != DBNull.Value)
            {
                entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
            }


        }

    }
}
