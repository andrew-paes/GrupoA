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

        #region Métodos

        public IEnumerable<ProgramaAtualizacaoPagina> CarregarLocalizacoesPorProgramaAtualizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada)
        {
            List<ProgramaAtualizacaoPagina> entidadesRetorno = new List<ProgramaAtualizacaoPagina>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT progAtPag.* FROM ProgramaAtualizacaoChamada progAtChamada ");
            sbSQL.Append(" INNER JOIN ProgramaAtualizacaoChamadaLocalizacao progAtPagLocal ON progAtPagLocal.programaAtualizacaoChamadaId = progAtChamada.programaAtualizacaoChamadaId  ");
            sbSQL.Append(" INNER JOIN ProgramaAtualizacaoPagina progAtPag ON progAtPag.programaAtualizacaoPaginaId = progAtPagLocal.programaAtualizacaoPaginaId ");
            sbSQL.Append(" WHERE progAtChamada.programaAtualizacaoChamadaId = @programaAtualizacaoChamadaId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, programaAtualizacaoChamada.ProgramaAtualizacaoChamadaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProgramaAtualizacaoPagina entidadeRetorno = new ProgramaAtualizacaoPagina();
                ProgramaAtualizacaoPaginaADO.PopulaProgramaAtualizacaoPagina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        public void ExcluirLocalizacoesPorProgramaAtualizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM ProgramaAtualizacaoChamadaLocalizacao ");
            sbSQL.Append("WHERE programaAtualizacaoChamadaId=@programaAtualizacaoChamadaId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, programaAtualizacaoChamada.ProgramaAtualizacaoChamadaId);


            _db.ExecuteNonQuery(command);
        }

        public void InserirLocalizacao(ProgramaAtualizacaoChamada programaAtualizacaoChamada, ProgramaAtualizacaoPagina programaAtualizacaoPagina)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO ProgramaAtualizacaoChamadaLocalizacao ");
            sbSQL.Append(" (programaAtualizacaoChamadaId, programaAtualizacaoPaginaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@programaAtualizacaoChamadaId, @programaAtualizacaoPaginaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, programaAtualizacaoChamada.ProgramaAtualizacaoChamadaId);
            _db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, programaAtualizacaoPagina.ProgramaAtualizacaoPaginaId);
            
            // Executa a query.
            _db.ExecuteNonQuery(command);


        }

        public List<ProgramaAtualizacaoChamada> CarregarChamadasRandomicas(int numeroMaximoRegistros)
        {

            List<ProgramaAtualizacaoChamada> entidadesRetorno = new List<ProgramaAtualizacaoChamada>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP " + numeroMaximoRegistros + " * FROM ProgramaAtualizacaoChamada ");
            sbSQL.Append("INNER JOIN Arquivo ON ProgramaAtualizacaoChamada.arquivoIdImagem=Arquivo.arquivoId ");
            sbSQL.Append("WHERE Ativo = 1 ");
            sbSQL.Append("ORDER BY NEWID() ");



            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            //_db.AddInParameter(command, "@programaAtualizacaoChamadaId", DbType.Int32, entidade.ProgramaAtualizacaoChamadaId);
            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ProgramaAtualizacaoChamada entidadeRetorno = new ProgramaAtualizacaoChamada();
                PopulaProgramaAtualizacaoChamada(reader, entidadeRetorno);

                if (reader["nomeArquivo"] != DBNull.Value)
                    entidadeRetorno.ArquivoImagem.NomeArquivo = reader["nomeArquivo"].ToString();
                
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="programaAtualizacaoPagina"></param>
        /// <returns></returns>
        public ProgramaAtualizacaoChamada CarregarProgramaAtualizacaoChamadaPorPagina(ProgramaAtualizacaoPagina programaAtualizacaoPagina)
        {
            ProgramaAtualizacaoChamada entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 ");
	        sbSQL.Append("    ProgramaAtualizacaoChamada.programaAtualizacaoChamadaId, ");
	        sbSQL.Append("    ProgramaAtualizacaoChamada.primeiraChamadaTitulo, ");
	        sbSQL.Append("    ProgramaAtualizacaoChamada.primeiraChamadaTexto, ");
            sbSQL.Append("    ProgramaAtualizacaoChamada.primeiraChamadaUrl, ");
            sbSQL.Append("    ProgramaAtualizacaoChamada.arquivoIdImagem, ");
            sbSQL.Append("    Arquivo.nomeArquivo ");
            sbSQL.Append("FROM ProgramaAtualizacaoChamada ");
            sbSQL.Append("INNER JOIN Arquivo on ProgramaAtualizacaoChamada.arquivoIdImagem = Arquivo.arquivoId ");
            if (programaAtualizacaoPagina != null)
            {
                sbSQL.Append("INNER JOIN ProgramaAtualizacaoChamadaLocalizacao ");
                sbSQL.Append("    ON ProgramaAtualizacaoChamada.programaAtualizacaoChamadaId = ProgramaAtualizacaoChamadaLocalizacao.programaAtualizacaoChamadaId ");
            }
            sbSQL.Append("WHERE ProgramaAtualizacaoChamada.ativo = 1 ");
            if (programaAtualizacaoPagina != null)
            {
                sbSQL.Append("    AND ProgramaAtualizacaoChamadaLocalizacao.programaAtualizacaoPaginaId = @programaAtualizacaoPaginaId ");
            }
            sbSQL.Append("ORDER BY programaAtualizacaoChamadaId DESC ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (programaAtualizacaoPagina != null)
            {
                _db.AddInParameter(command, "@programaAtualizacaoPaginaId", DbType.Int32, programaAtualizacaoPagina.ProgramaAtualizacaoPaginaId);
            }

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new ProgramaAtualizacaoChamada();
                PopulaUltimoProgramaAtualizacaoChamada(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entidade"></param>
        public static void PopulaUltimoProgramaAtualizacaoChamada(IDataReader reader, ProgramaAtualizacaoChamada entidade)
        {
            if (reader["programaAtualizacaoChamadaId"] != DBNull.Value)
            {
                entidade.ProgramaAtualizacaoChamadaId = Convert.ToInt32(reader["programaAtualizacaoChamadaId"].ToString());
            }

            if (reader["primeiraChamadaTitulo"] != DBNull.Value)
            {
                entidade.PrimeiraChamadaTitulo = reader["primeiraChamadaTitulo"].ToString();
            }

            if (reader["primeiraChamadaTexto"] != DBNull.Value)
            {
                entidade.PrimeiraChamadaTexto = reader["primeiraChamadaTexto"].ToString();
            }

            if (reader["primeiraChamadaUrl"] != DBNull.Value)
            {
                entidade.PrimeiraChamadaUrl = reader["primeiraChamadaUrl"].ToString();
            }

            if (reader["arquivoIdImagem"] != DBNull.Value)
            {
                if (entidade.ArquivoImagem == null) entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.ArquivoId = Convert.ToInt32(reader["arquivoIdImagem"].ToString());
            }

            if (reader["nomeArquivo"] != DBNull.Value)
            {
                if (entidade.ArquivoImagem == null) entidade.ArquivoImagem = new Arquivo();
                entidade.ArquivoImagem.NomeArquivo = reader["nomeArquivo"].ToString();
            }
        }

        #endregion

    }
}
		