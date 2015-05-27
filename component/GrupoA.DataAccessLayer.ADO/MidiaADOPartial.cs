using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class MidiaADO : ADOSuper, IMidiaDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="revista"></param>
        /// <returns></returns>
        public List<Midia> CarregarMidiasPorCategoria(Categoria categoria, Revista revista)
        {
            List<Midia> entidadesRetorno = new List<Midia>();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.AppendFormat("SELECT TOP 2  ");
            sbSQL.AppendFormat("    Midia.midiaId, ");
            sbSQL.AppendFormat("    Midia.tituloMidia, ");
            sbSQL.AppendFormat("    Convert(varchar(20), Conteudo.dataHoraCadastro, 103) + ' ' + Convert(varchar(20), Conteudo.dataHoraCadastro, 108) as dataHoraCadastro, ");
            sbSQL.AppendFormat("    MidiaTipo.tipoMidia, ");
            sbSQL.AppendFormat("    MidiaTipo.midiaTipoId, ");
            sbSQL.AppendFormat("    Arquivo.nomeArquivo ");
            sbSQL.AppendFormat("FROM Midia ");
            sbSQL.AppendFormat("INNER JOIN MidiaTipo ");
            sbSQL.AppendFormat("    ON Midia.midiaTipoId = MidiaTipo.midiaTipoId ");
            sbSQL.AppendFormat("INNER JOIN Conteudo ");
            sbSQL.AppendFormat("    ON Midia.midiaId = Conteudo.conteudoId ");
            if (categoria != null)
            {
                sbSQL.AppendFormat("INNER JOIN MidiaCategoria ");
                sbSQL.AppendFormat("    ON Midia.midiaId = MidiaCategoria.midiaId ");
            }
            if (revista != null)
            {
                sbSQL.AppendFormat("INNER JOIN MidiaRevista ");
                sbSQL.AppendFormat("    ON Midia.midiaId = MidiaRevista.midiaId ");
            }
            sbSQL.AppendFormat("LEFT JOIN Arquivo ");
            sbSQL.AppendFormat("    ON Midia.arquivoId = Arquivo.arquivoId ");
            sbSQL.AppendFormat(" WHERE Midia.ativo = 1");
            if (categoria != null)
            {
                sbSQL.AppendFormat(" AND MidiaCategoria.categoriaId = @categoriaId ");
            }
            if (revista != null)
            {
                sbSQL.AppendFormat(" AND MidiaRevista.revistaId = @revistaId ");
            }
            sbSQL.AppendFormat("ORDER BY Conteudo.dataHoraCadastro DESC ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (categoria != null)
            {
                _db.AddInParameter(command, "@categoriaId", DbType.String, categoria.CategoriaId);
            }
            if (revista != null)
            {
                _db.AddInParameter(command, "@revistaId", DbType.String, revista.RevistaId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaUltimasMidias(reader, entidadeRetorno);

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaUltimasMidias(IDataReader reader, Midia entidade)
        {
            if (reader["midiaId"] != DBNull.Value)
            {
                entidade.MidiaId = Convert.ToInt32(reader["midiaId"].ToString());
            }

            if (reader["tituloMidia"] != DBNull.Value)
            {
                entidade.TituloMidia = reader["tituloMidia"].ToString();
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.Arquivo == null) entidade.Arquivo = new Arquivo();
                entidade.Arquivo.NomeArquivo = reader["nomeArquivo"].ToString();
            }

            if (reader["midiaTipoId"] != DBNull.Value)
            {
                if (entidade.MidiaTipo == null) entidade.MidiaTipo = new MidiaTipo();
                entidade.MidiaTipo.MidiaTipoId = Convert.ToInt32(reader["midiaTipoId"].ToString());
            }

            if (reader["tipoMidia"] != DBNull.Value)
            {
                if (entidade.MidiaTipo == null) entidade.MidiaTipo = new MidiaTipo();
                entidade.MidiaTipo.TipoMidia = reader["tipoMidia"].ToString();
            }

            if (reader["dataHoraCadastro"] != DBNull.Value)
            {
                if (entidade.Conteudo == null) entidade.Conteudo = new Conteudo();
                entidade.Conteudo.DataHoraCadastro = Convert.ToDateTime(reader["dataHoraCadastro"].ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrosPagina"></param>
        /// <param name="numeroPagina"></param>
        /// <param name="ordemColunas"></param>
        /// <param name="ordemSentidos"></param>
        /// <param name="midiaTipoId"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public IEnumerable<Midia> CarregarTodosPorRevista(Int32 registrosPagina, Int32 numeroPagina, String[] ordemColunas, String[] ordemSentidos, Int32? midiaTipoId, Int32 revistaId)
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

            sbSQL.Append("SELECT * FROM ( ");
            sbSQL.Append("SELECT Midia.*, Conteudo.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Midia ");
            sbSQL.Append("INNER JOIN Conteudo ");
            sbSQL.Append("    ON Midia.midiaId = Conteudo.conteudoId ");
            sbSQL.Append("INNER JOIN MidiaRevista ");
            sbSQL.Append("    ON Midia.midiaId = MidiaRevista.midiaId ");
            sbSQL.Append("WHERE MidiaRevista.revistaId = @revistaId AND Midia.ativo = 1");

            if (midiaTipoId != null)
            {
                sbSQL.Append("AND Midia.midiaTipoId = @midiaTipoId ");
            }

            sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.String, revistaId);

            if (midiaTipoId != null)
            {
                _db.AddInParameter(command, "@midiaTipoId", DbType.String, midiaTipoId);
            }

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);

                entidadeRetorno.Conteudo = new Conteudo();
                ConteudoADO.PopulaConteudo(reader, entidadeRetorno.Conteudo);

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midiaTipoId"></param>
        /// <param name="revistaId"></param>
        /// <returns></returns>
        public Int32 ContarTodosPorRevista(Int32? midiaTipoId, Int32 revistaId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Midia ");
            sbSQL.Append("INNER JOIN MidiaRevista ");
            sbSQL.Append("    ON Midia.midiaId = MidiaRevista.midiaId ");
            sbSQL.Append("WHERE MidiaRevista.revistaId = @revistaId ");

            if (midiaTipoId != null)
            {
                sbSQL.Append("AND Midia.midiaTipoId = @midiaTipoId ");
            }

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.String, revistaId);

            if (midiaTipoId != null)
            {
                _db.AddInParameter(command, "@midiaTipoId", DbType.String, midiaTipoId);
            }

            Int32 resultado = (Int32)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public int ContarMidiaBusca(String palavra)
        {
            int total = 0;

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(@"SELECT
	                            COUNT(Midia.midiaId) AS Total
                            FROM
	                            Midia
	                            INNER JOIN CONTAINSTABLE(Midia, *, @palavra) AS R1 ON R1.[KEY] = Midia.midiaId
                            WHERE
	                            R1.[RANK] IS NOT NULL
							");

            DbCommand command = _db.GetSqlStringCommand(sbSql.ToString());

            if (!String.IsNullOrEmpty(palavra))
            {
                _db.AddInParameter(command, "@palavra", DbType.String, palavra);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if ((reader.Read()) && (reader["Total"] != null))
            {
                total = Int32.Parse(reader["Total"].ToString());
            }

            reader.Close();

            return total;
        }

        public List<Midia> CarregarMidiaBusca(int registrosPagina, int numeroPagina, String[] ordenacao, String[] ordenacaoSentido, String palavra)
        {
            List<Midia> entidadesRetorno = new List<Midia>();

            string iniRegister = (((numeroPagina - 1) * registrosPagina) + 1).ToString();
            string endRegister = ((numeroPagina) * registrosPagina).ToString();

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            IDataReader reader;

            sbSQL.Append(@"SELECT
	                            *
                            FROM
	                            (
	                            SELECT ");

            if (ordenacao != null && !String.IsNullOrEmpty(ordenacao[0]))
                sbSQL.AppendFormat(@"   ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNumber ", ordenacao[0].ToString());
            else
                sbSQL.Append(@"       ROW_NUMBER() OVER ( ORDER BY RANKT DESC ) AS RowNumber  ");

            sbSQL.Append(@"             , *
	                            FROM
		                            (
		                            SELECT
                                        ISNULL(R1.RANK, 0) AS RANKT
			                            , Midia.*
			                            , MidiaRevista.midiaRevistaId
	                                    , MidiaRevista.revistaId
		                            FROM
			                            Midia
                                        INNER JOIN MidiaTipo ON MidiaTipo.midiaTipoId = Midia.midiaTipoId
	                                    INNER JOIN MidiaRevista ON MidiaRevista.midiaId = Midia.midiaId
			                            INNER JOIN CONTAINSTABLE(Midia, *, @palavra) AS R1 ON R1.[KEY] = Midia.midiaId
		                            WHERE
			                            R1.[RANK] IS NOT NULL
		                            ) AS tabelaTemp1
	                            ) AS tabelaTemp2 ");

            sbSQL.AppendFormat(@"WHERE 
	                                RowNumber BETWEEN {0} AND {1}"
                                    , iniRegister
                                    , endRegister);

            if (ordenacao != null && !String.IsNullOrEmpty(ordenacao[0]))
                sbSQL.AppendFormat(@"ORDER BY {0}", ordenacao[0].ToString());
            else
                sbSQL.Append(@"ORDER BY RANKT DESC");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@palavra", DbType.String, palavra);

            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Midia entidadeRetorno = new Midia();
                PopulaMidia(reader, entidadeRetorno);

                if (entidadeRetorno.MidiaTipo != null && entidadeRetorno.MidiaTipo.MidiaTipoId > 0)
                {
                    entidadeRetorno.MidiaTipo = new MidiaTipoADO().Carregar(entidadeRetorno.MidiaTipo);
                }

                entidadeRetorno.Conteudo = new ConteudoADO().Carregar(new Conteudo() { ConteudoId = entidadeRetorno.MidiaId });

                MidiaRevista midiaRevistaBO = new MidiaRevista();
                MidiaRevistaADO.PopulaMidiaRevista(reader, midiaRevistaBO);

                if (midiaRevistaBO != null && midiaRevistaBO.MidiaRevistaId > 0)
                {
                    if (midiaRevistaBO.Revista != null && midiaRevistaBO.Revista.RevistaId > 0)
                    {
                        midiaRevistaBO.Revista = new RevistaADO().Carregar(midiaRevistaBO.Revista);
                    }

                    entidadeRetorno.MidiaRevistas = new List<MidiaRevista>();
                    entidadeRetorno.MidiaRevistas.Add(midiaRevistaBO);
                }

                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }
    }
}
