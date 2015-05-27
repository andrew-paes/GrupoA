using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.FilterHelper;

namespace GrupoA.DataAccess.ADO
{
    public partial class MidiaADO : ADOSuper, IMidiaDAL
    {

        /// <summary>
        /// Método que persiste um Midia.
        /// </summary>
        /// <param name="entidade">Midia contendo os dados a serem persistidos.</param>	
        public void Inserir(Midia entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Midia ");
            sbSQL.Append(" (midiaId, midiaTipoId, arquivoId, arquivoIdThumb, tituloMidia, urlMidia, autor, descricaoMidia, ativo) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@midiaId, @midiaTipoId, @arquivoId, @arquivoIdThumb, @tituloMidia, @urlMidia, @autor, @descricaoMidia, @ativo) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);

            _db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipo.MidiaTipoId);

            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, null);

            if (entidade.ArquivoThumb != null)
                _db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, entidade.ArquivoThumb.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, null);

            _db.AddInParameter(command, "@tituloMidia", DbType.String, entidade.TituloMidia);

            if (entidade.UrlMidia != null)
                _db.AddInParameter(command, "@urlMidia", DbType.String, entidade.UrlMidia);
            else
                _db.AddInParameter(command, "@urlMidia", DbType.String, null);

            if (entidade.Autor != null)
                _db.AddInParameter(command, "@autor", DbType.String, entidade.Autor);
            else
                _db.AddInParameter(command, "@autor", DbType.String, null);

            if (entidade.DescricaoMidia != null)
                _db.AddInParameter(command, "@descricaoMidia", DbType.String, entidade.DescricaoMidia);
            else
                _db.AddInParameter(command, "@descricaoMidia", DbType.String, null);

            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um Midia.
        /// </summary>
        /// <param name="entidade">Midia contendo os dados a serem atualizados.</param>
        public void Atualizar(Midia entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Midia SET ");
            sbSQL.Append(" midiaTipoId=@midiaTipoId, arquivoId=@arquivoId, arquivoIdThumb=@arquivoIdThumb, tituloMidia=@tituloMidia, urlMidia=@urlMidia, autor=@autor, descricaoMidia=@descricaoMidia, ativo=@ativo ");
            sbSQL.Append(" WHERE midiaId=@midiaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);
            _db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipo.MidiaTipoId);
            if (entidade.Arquivo != null)
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.Arquivo.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoId", DbType.Int32, null);
            if (entidade.ArquivoThumb != null)
                _db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, entidade.ArquivoThumb.ArquivoId);
            else
                _db.AddInParameter(command, "@arquivoIdThumb", DbType.Int32, null);
            _db.AddInParameter(command, "@tituloMidia", DbType.String, entidade.TituloMidia);
            if (entidade.UrlMidia != null)
                _db.AddInParameter(command, "@urlMidia", DbType.String, entidade.UrlMidia);
            else
                _db.AddInParameter(command, "@urlMidia", DbType.String, null);
            if (entidade.Autor != null)
                _db.AddInParameter(command, "@autor", DbType.String, entidade.Autor);
            else
                _db.AddInParameter(command, "@autor", DbType.String, null);
            if (entidade.DescricaoMidia != null)
                _db.AddInParameter(command, "@descricaoMidia", DbType.String, entidade.DescricaoMidia);
            else
                _db.AddInParameter(command, "@descricaoMidia", DbType.String, null);

            _db.AddInParameter(command, "@ativo", DbType.Boolean, entidade.Ativo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Midia da base de dados.
        /// </summary>
        /// <param name="entidade">Midia a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Midia entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Midia ");
            sbSQL.Append("WHERE midiaId=@midiaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);


            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Método que carrega um Midia.
        /// </summary>
        /// <param name="entidade">Midia a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Midia</returns>
        public Midia Carregar(Midia entidade)
        {

            Midia entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Midia WHERE midiaId=@midiaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que carrega um Midia com suas dependências.
        /// </summary>
        /// <param name="entidade">Midia a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Midia</returns>
        public Midia CarregarComDependencias(Midia entidade)
        {

            Midia entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Midia.midiaId, Midia.midiaTipoId, Midia.ativo, Midia.arquivoId, Midia.arquivoIdThumb, Midia.tituloMidia, Midia.urlMidia, Midia.autor, Midia.descricaoMidia");
            sbSQL.Append(", conteudoId, conteudoTipoId, dataHoraCadastro");
            sbSQL.Append(" FROM Midia");
            sbSQL.Append(" INNER JOIN Conteudo ON Midia.midiaId=Conteudo.conteudoId");
            sbSQL.Append(" WHERE Midia.midiaId=@midiaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@midiaId", DbType.Int32, entidade.MidiaId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
                entidadeRetorno.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que retorna uma coleção de Midia.
        /// </summary>
        /// <param name="entidade">MidiaCategoria relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Midia.</returns>
        public IEnumerable<Midia> Carregar(MidiaCategoria entidade)
        {
            List<Midia> entidadesRetorno = new List<Midia>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Midia.* FROM Midia INNER JOIN MidiaCategoria ON Midia.midiaId=MidiaCategoria.midiaId WHERE MidiaCategoria.midiaCategoriaId=@midiaCategoriaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@midiaCategoriaId", DbType.Int32, entidade.MidiaCategoriaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Midia.
        /// </summary>
        /// <param name="entidade">MidiaRevista relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Midia.</returns>
        public IEnumerable<Midia> Carregar(MidiaRevista entidade)
        {
            List<Midia> entidadesRetorno = new List<Midia>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Midia.* FROM Midia INNER JOIN MidiaRevista ON Midia.midiaId=MidiaRevista.midiaId WHERE MidiaRevista.midiaRevistaId=@midiaRevistaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@midiaRevistaId", DbType.Int32, entidade.MidiaRevistaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Midia.
        /// </summary>
        /// <param name="entidade">Arquivo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Midia.</returns>
        public IEnumerable<Midia> Carregar(Arquivo entidade)
        {
            List<Midia> entidadesRetorno = new List<Midia>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Midia.* FROM Midia WHERE Midia.arquivoId=@arquivoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, entidade.ArquivoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Midia.
        /// </summary>
        /// <param name="entidade">MidiaTipo relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Midia.</returns>
        public IEnumerable<Midia> Carregar(MidiaTipo entidade)
        {
            List<Midia> entidadesRetorno = new List<Midia>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Midia.* FROM Midia WHERE Midia.midiaTipoId=@midiaTipoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@midiaTipoId", DbType.Int32, entidade.MidiaTipoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Midia.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Midia.</returns>
        public IEnumerable<Midia> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Midia> entidadesRetorno = new List<Midia>();

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
                sbOrder.Append(" ORDER BY midiaId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Midia");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Midia WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Midia ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Midia.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Midia ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Midia.* FROM Midia ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Midia existentes na base de dados.
        /// </summary>
        public IEnumerable<Midia> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Midia na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Midia na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Midia");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Midia baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Midia a ser populado(.</param>
        public static void PopulaMidia(IDataReader reader, Midia entidade)
        {
            if (reader["tituloMidia"] != DBNull.Value)
                entidade.TituloMidia = reader["tituloMidia"].ToString();

            if (reader["urlMidia"] != DBNull.Value)
                entidade.UrlMidia = reader["urlMidia"].ToString();

            if (reader["autor"] != DBNull.Value)
                entidade.Autor = reader["autor"].ToString();

            if (reader["descricaoMidia"] != DBNull.Value)
                entidade.DescricaoMidia = reader["descricaoMidia"].ToString();

            if (reader["ativo"] != DBNull.Value)
                entidade.Ativo = Convert.ToBoolean(reader["ativo"].ToString());

            if (reader["midiaId"] != DBNull.Value)
            {
                entidade.MidiaId = Convert.ToInt32(reader["midiaId"].ToString());
            }

            if (reader["midiaTipoId"] != DBNull.Value)
            {
                entidade.MidiaTipo = new MidiaTipo();
                entidade.MidiaTipo.MidiaTipoId = Convert.ToInt32(reader["midiaTipoId"].ToString());
            }

            if (reader["arquivoId"] != DBNull.Value)
            {
                entidade.Arquivo = new Arquivo();
                entidade.Arquivo.ArquivoId = Convert.ToInt32(reader["arquivoId"].ToString());
            }

            if (reader["arquivoIdThumb"] != DBNull.Value)
            {
                entidade.ArquivoThumb = new Arquivo();
                entidade.ArquivoThumb.ArquivoId = Convert.ToInt32(reader["arquivoIdThumb"].ToString());
            }


        }

    }
}
