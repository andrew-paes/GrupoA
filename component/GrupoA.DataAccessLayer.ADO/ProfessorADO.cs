
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
    public partial class ProfessorADO : ADOSuper, IProfessorDAL
    {

        /// <summary>
        /// Método que persiste um Professor.
        /// </summary>
        /// <param name="entidade">Professor contendo os dados a serem persistidos.</param>	
        public void Inserir(Professor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO Professor ");
            sbSQL.Append(" (professorId, graduacaoProfessorId, autorGrupoa, colaboradorGrupoa, possuiPublicacao) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@professorId, @graduacaoProfessorId, @autorGrupoa, @colaboradorGrupoa, @possuiPublicacao) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            _db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessor.GraduacaoProfessorId);

            _db.AddInParameter(command, "@autorGrupoa", DbType.Int32, entidade.AutorGrupoa);

            _db.AddInParameter(command, "@colaboradorGrupoa", DbType.Int32, entidade.ColaboradorGrupoa);

            _db.AddInParameter(command, "@possuiPublicacao", DbType.Int32, entidade.PossuiPublicacao);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que atualiza os dados de um Professor.
        /// </summary>
        /// <param name="entidade">Professor contendo os dados a serem atualizados.</param>
        public void Atualizar(Professor entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Professor SET ");
            sbSQL.Append(" graduacaoProfessorId=@graduacaoProfessorId, autorGrupoa=@autorGrupoa, colaboradorGrupoa=@colaboradorGrupoa, possuiPublicacao=@possuiPublicacao ");
            sbSQL.Append(" WHERE professorId=@professorId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);
            _db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessor.GraduacaoProfessorId);
            _db.AddInParameter(command, "@autorGrupoa", DbType.Int32, entidade.AutorGrupoa);
            _db.AddInParameter(command, "@colaboradorGrupoa", DbType.Int32, entidade.ColaboradorGrupoa);
            _db.AddInParameter(command, "@possuiPublicacao", DbType.Int32, entidade.PossuiPublicacao);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove um Professor da base de dados.
        /// </summary>
        /// <param name="entidade">Professor a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Professor entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM Professor ");
            sbSQL.Append("WHERE professorId=@professorId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);


            _db.ExecuteNonQuery(command);
        }


        /// <summary>
        /// Método que carrega um Professor.
        /// </summary>
        /// <param name="entidade">Professor a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Professor</returns>
        public Professor Carregar(Professor entidade)
        {

            Professor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM Professor WHERE professorId=@professorId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que carrega um Professor com suas dependências.
        /// </summary>
        /// <param name="entidade">Professor a ser carregado (somente o identificador é necessário).</param>
        /// <returns>Professor</returns>
        public Professor CarregarComDependencias(Professor entidade)
        {

            Professor entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Professor.professorId, Professor.graduacaoProfessorId, Professor.autorGrupoa, Professor.colaboradorGrupoa, Professor.possuiPublicacao");
            sbSQL.Append(", usuarioId, tipoPessoa, sexo, ativo, nomeUsuario, cadastroPessoa, emailUsuario, login, dataNascimento, dataHoraCadastro, optinSMS, optinNewsletter, codigoUsuario, profissionalOcupacaoId, senha");
            sbSQL.Append(" FROM Professor");
            sbSQL.Append(" INNER JOIN Usuario ON Professor.professorId=Usuario.usuarioId");
            sbSQL.Append(" WHERE Professor.professorId=@professorId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@professorId", DbType.Int32, entidade.ProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
                entidadeRetorno.Usuario = new Usuario();
                UsuarioADO.PopulaUsuario(reader, entidadeRetorno.Usuario);
            }
            reader.Close();

            return entidadeRetorno;
        }


        /// <summary>
        /// Método que retorna uma coleção de Professor.
        /// </summary>
        /// <param name="entidade">ProfessorComprovanteDocencia relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Professor.</returns>
        public IEnumerable<Professor> Carregar(ProfessorComprovanteDocencia entidade)
        {
            List<Professor> entidadesRetorno = new List<Professor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Professor.* FROM Professor INNER JOIN ProfessorComprovanteDocencia ON Professor.professorId=ProfessorComprovanteDocencia.professorId WHERE ProfessorComprovanteDocencia.professorComprovanteDocenciaId=@professorComprovanteDocenciaId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorComprovanteDocenciaId", DbType.Int32, entidade.ProfessorComprovanteDocenciaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Professor entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Professor.
        /// </summary>
        /// <param name="entidade">ProfessorInstituicao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Professor.</returns>
        public IEnumerable<Professor> Carregar(ProfessorInstituicao entidade)
        {
            List<Professor> entidadesRetorno = new List<Professor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Professor.* FROM Professor INNER JOIN ProfessorInstituicao ON Professor.professorId=ProfessorInstituicao.professorId WHERE ProfessorInstituicao.professorInstituicaoId=@professorInstituicaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@professorInstituicaoId", DbType.Int32, entidade.ProfessorInstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Professor entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Professor.
        /// </summary>
        /// <param name="entidade">TituloSolicitacao relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Professor.</returns>
        public IEnumerable<Professor> Carregar(TituloSolicitacao entidade)
        {
            List<Professor> entidadesRetorno = new List<Professor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Professor.* FROM Professor INNER JOIN TituloSolicitacao ON Professor.professorId=TituloSolicitacao.professorId WHERE TituloSolicitacao.tituloSolicitacaoId=@tituloSolicitacaoId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloSolicitacaoId", DbType.Int32, entidade.TituloSolicitacaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Professor entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna uma coleção de Professor.
        /// </summary>
        /// <param name="entidade">GraduacaoProfessor relacionado(a) (somente o identificador é necessário).</param>		
        /// <returns>Retorna uma coleção de Professor.</returns>
        public IEnumerable<Professor> Carregar(GraduacaoProfessor entidade)
        {
            List<Professor> entidadesRetorno = new List<Professor>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT Professor.* FROM Professor WHERE Professor.graduacaoProfessorId=@graduacaoProfessorId");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@graduacaoProfessorId", DbType.Int32, entidade.GraduacaoProfessorId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Professor entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }


        /// <summary>
        /// Método que retorna uma coleção de Professor.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos Professor.</returns>
        public IEnumerable<Professor> CarregarTodos(int registrosPagina, int numeroPagina, string[] ordemColunas, string[] ordemSentidos, IFilterHelper filtro)
        {

            List<Professor> entidadesRetorno = new List<Professor>();

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
                sbOrder.Append(" ORDER BY professorId");
            }


            if (registrosPagina > 0)
            {

                //sbSQL.Append("SELECT TOP "+registrosPagina+" * FROM Professor");
                //if ( filtro!=null && !filtro.GetWhereString().Equals(String.Empty) ) {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Professor WHERE " + filtro.GetWhereString() + " ORDER BY " + orderBy + ") ");					
                //} else {
                //	sbWhere.Append(" NOT IN (SELECT TOP "+((numeroPagina-1)*registrosPagina)+"  FROM Professor ORDER BY " + orderBy + ")");				
                //}	
                sbSQL.Append("SELECT * FROM ( ");
                sbSQL.Append("SELECT Professor.*, ROW_NUMBER() OVER (" + sbOrder.ToString() + ") R FROM Professor ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                sbSQL.Append(") #Q WHERE R BETWEEN " + (((numeroPagina - 1) * registrosPagina) + 1).ToString() + " AND " + ((numeroPagina) * registrosPagina).ToString());

            }
            else
            {
                sbSQL.Append("SELECT Professor.* FROM Professor ");
                if (filtro != null && !filtro.GetWhereString().Equals(String.Empty)) { sbSQL.Append("WHERE" + filtro.GetWhereString() + " "); }
                if (sbOrder.Length > 0) { sbSQL.Append(sbOrder.ToString()); }
            }

            command = _db.GetSqlStringCommand(sbSQL.ToString());
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Professor entidadeRetorno = new Professor();
                PopulaProfessor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna todas os Professor existentes na base de dados.
        /// </summary>
        public IEnumerable<Professor> CarregarTodos()
        {
            return CarregarTodos(0, 0, null, null, null);
        }

        /// <summary>
        /// Método que retorna o total de Professor na base de dados.
        /// </summary>
        /// <returns></returns>
        public int TotalRegistros()
        {
            return TotalRegistros(null);
        }

        /// <summary>
        /// Método que retorna o total de Professor na base de dados, aceita filtro.
        /// </summary>
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>
        /// <returns></returns>
        public int TotalRegistros(IFilterHelper filtro)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT COUNT(*) AS Total FROM Professor");

            if (filtro != null && !filtro.GetWhereString().Equals(String.Empty))
                sbSQL.Append(" WHERE (" + filtro.GetWhereString() + ")");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Executa a query.

            int resultado = (int)_db.ExecuteScalar(command);


            return resultado;
        }

        /// <summary>
        /// Método que retorna popula um Professor baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">Professor a ser populado(.</param>
        public static void PopulaProfessor(IDataReader reader, Professor entidade)
        {
            if (reader["autorGrupoa"] != DBNull.Value)
                entidade.AutorGrupoa = Convert.ToBoolean(reader["autorGrupoa"].ToString());

            if (reader["colaboradorGrupoa"] != DBNull.Value)
                entidade.ColaboradorGrupoa = Convert.ToBoolean(reader["colaboradorGrupoa"].ToString());

            if (reader["possuiPublicacao"] != DBNull.Value)
                entidade.PossuiPublicacao = Convert.ToBoolean(reader["possuiPublicacao"].ToString());

            if (reader["professorId"] != DBNull.Value)
            {
                entidade.ProfessorId = Convert.ToInt32(reader["professorId"].ToString());
            }

            if (reader["graduacaoProfessorId"] != DBNull.Value)
            {
                entidade.GraduacaoProfessor = new GraduacaoProfessor();
                entidade.GraduacaoProfessor.GraduacaoProfessorId = Convert.ToInt32(reader["graduacaoProfessorId"].ToString());
            }


        }

    }
}
