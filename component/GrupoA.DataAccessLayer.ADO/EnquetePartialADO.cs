using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
	public partial class EnqueteADO : ADOSuper, IEnqueteDAL {
	
        #region Métodos de EnqueteLocalização

        #region public void InserirLocalizacao(Enquete enquete, EnquetePagina enquetePagina)
        /// <summary>
        /// Insere uma nova Localização para Enquete na Página
        /// </summary>
        /// <param name="enquete">Enquete a ser relacionada com a Página</param>
        /// <param name="enquetePagina">Página para ser relacionada com a Enquete</param>
        public void InserirLocalizacao(Enquete enquete, EnquetePagina enquetePagina)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO EnqueteLocalizacao ");
            sbSQL.Append(" (enqueteId, enquetePaginaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@enqueteId, @enquetePaginaId) ");

            //sbSQL.Append(" ; SET @enqueteId = SCOPE_IDENTITY(); ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);
            _db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, enquetePagina.EnquetePaginaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);

            //entidade.EnqueteId = Convert.ToInt32(_db.GetParameterValue(command, "@enqueteId"));

        }
        #endregion

        #region public IEnumerable<EnquetePagina> CarregarLocalizacoesPorEnquete(Enquete enquete)
        /// <summary>
        /// Carrega todas as localizações (EnquetePagina) 
        /// conforme o código identificador da enquete "enqueteId" recebido.
        /// </summary>
        /// <param name="enquete">Objeto Enquete que filtrará as informações pelo código enqueteId.</param>
        /// <returns>Coleção de Páginas de Enquete (EnquetePagina).</returns>
        public IEnumerable<EnquetePagina> CarregarLocalizacoesPorEnquete(Enquete enquete)
        {
            List<EnquetePagina> entidadesRetorno = new List<EnquetePagina>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT ep.* FROM enquete e ");
            sbSQL.Append(" INNER JOIN EnqueteLocalizacao el ON el.enqueteId = e.enqueteId ");
            sbSQL.Append(" INNER JOIN EnquetePagina ep ON ep.enquetePaginaId = el.enquetePaginaId ");
            sbSQL.Append(" WHERE e.enqueteId = @enqueteId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EnquetePagina entidadeRetorno = new EnquetePagina();
                EnquetePaginaADO.PopulaEnquetePagina(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }
        #endregion

        #region public void ExcluirLocalizacoesPorEnquete(Enquete entidade)
        /// <summary>
        /// Método que remove uma Localização de Enquete da base de dados.
        /// </summary>
        /// <param name="entidade">Enquete a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirLocalizacoesPorEnquete(Enquete entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM EnqueteLocalizacao ");
            sbSQL.Append("WHERE enqueteId=@enqueteId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);


            _db.ExecuteNonQuery(command);
        }
        #endregion

        #region public void AtualizaStatus(Enquete entidade)
        /// <summary>
        /// Método que atualiza o Status de um Enquete.
        /// </summary>
        /// <param name="entidade">Enquete contendo os dados a serem atualizados.</param>
        public void AtualizaStatus(Enquete entidade)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de atualização.
            sbSQL.Append(" UPDATE Enquete SET ");
            sbSQL.Append(" ativo=@ativo ");
            sbSQL.Append(" WHERE enqueteId=@enqueteId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            // Parâmetros
            _db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);
            _db.AddInParameter(command, "@ativo", DbType.Int32, entidade.Ativo);

            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<Enquete> CarregarEnquetePorAreas(Int32 enquetePaginaId, Usuario usuario)
        {
            List<Enquete> entidadesRetorno = new List<Enquete>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT * FROM ");
            sbSQL.Append(" (");
            sbSQL.Append(" SELECT TOP 1 E.*, ");
            if (usuario != null)
            {
                sbSQL.Append(" EU.usuarioId ");
            }
            else
            {
                sbSQL.Append(" usuarioId = null ");
            }
            sbSQL.Append(" FROM dbo.Enquete E");
            sbSQL.Append(" INNER JOIN dbo.EnqueteOpcao EO ON EO.enqueteId = E.enqueteId");
            sbSQL.Append(" INNER JOIN dbo.EnqueteLocalizacao ON E.enqueteId = EnqueteLocalizacao.enqueteId");
            if (usuario != null)
            {
                sbSQL.Append(" LEFT JOIN dbo.EnqueteUsuario EU ON E.enqueteId = EU.enqueteId AND EU.usuarioId = @usuarioId");
            }
            sbSQL.Append(" WHERE E.ativo = 1 ");
            sbSQL.Append("      AND EnqueteLocalizacao.enquetePaginaId = @enquetePaginaId ");
            sbSQL.Append(" ORDER BY NEWID()");
            sbSQL.Append(" ) Enquetes INNER JOIN dbo.EnqueteOpcao EO ON Enquetes.enqueteId = EO.enqueteId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            if (usuario != null)
            {
                _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            }
            _db.AddInParameter(command, "@enquetePaginaId", DbType.Int32, enquetePaginaId);
            
            IDataReader reader = _db.ExecuteReader(command);

            Enquete enquete = null;

            while(reader.Read())
            {
                if (enquete == null)
                {
                    enquete = new Enquete();
                    PopulaEnquete(reader, enquete);
                    enquete.EnqueteOpcoes = new List<EnqueteOpcao>();

                    if (reader["usuarioId"] != DBNull.Value)
                    {
                        Usuario usuarioLogado = new Usuario();
                        usuarioLogado.UsuarioId = Convert.ToInt32(reader["usuarioId"]);

                        enquete.Usuarios = new List<Usuario>();
                        enquete.Usuarios.Add(usuarioLogado);
                    }
                }

                EnqueteOpcao enqueteOpcao = new EnqueteOpcao();
                EnqueteOpcaoADO.PopulaEnqueteOpcao(reader, enqueteOpcao);

                enquete.EnqueteOpcoes.Add(enqueteOpcao);
            }

            entidadesRetorno.Add(enquete);
            reader.Close();

            return entidadesRetorno;
        }

        public Enquete VotarNaEnquete(Enquete enquete)
        {
            int totalEnquete = TotalContadorOpcaoEnquete(enquete);

            totalEnquete++;
            AtualizarContado(enquete, totalEnquete);
            UsuarioVotaEnquete(enquete);

            return CarregaEnquete(enquete);
        }

        private Enquete CarregaEnquete(Enquete entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Verifica total do contador
            sbSQL.Append(" SELECT Enquete.*,EnqueteOpcao.* FROM Enquete");
            sbSQL.Append(" INNER JOIN EnqueteOpcao ON Enquete.enqueteId=EnqueteOpcao.enqueteId");
            sbSQL.Append(" WHERE Enquete.enqueteId=@enqueteId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteId", DbType.Int32, entidade.EnqueteId);

            IDataReader reader = _db.ExecuteReader(command);

            Enquete enquete = null;

            while (reader.Read())
            {
                if (enquete == null)
                {
                    enquete = new Enquete();
                    PopulaEnquete(reader, enquete);
                    enquete.EnqueteOpcoes = new List<EnqueteOpcao>();
                }

                EnqueteOpcao enqueteOpcao = new EnqueteOpcao();
                EnqueteOpcaoADO.PopulaEnqueteOpcao(reader, enqueteOpcao);

                enquete.EnqueteOpcoes.Add(enqueteOpcao);
            }

            reader.Close();

            return enquete;
        }

        public void UsuarioVotaEnquete(Enquete enquete)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Atualiza dados do contador
            sbSQL.Append(" Insert into EnqueteUsuario values ( ");
            sbSQL.Append(" @EnqueteId,");
            sbSQL.Append(" @usuarioId)");
            
            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);
            _db.AddInParameter(command, "@usuarioId", DbType.Int32, enquete.Usuarios[0].UsuarioId);

            _db.ExecuteNonQuery(command);
        }

        public void AtualizarContado(Enquete enquete, int totalEnquete)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

             // Atualiza dados do contador
            sbSQL.Append(" UPDATE EnqueteOpcao SET ");
            sbSQL.Append(" contador=@totalEnquete ");
            sbSQL.Append(" WHERE  enqueteId=@enqueteId  and enqueteOpcaoId=@enqueteOpcaoId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteOpcaoId", DbType.Int32, enquete.EnqueteOpcoes[0].EnqueteOpcaoId);
            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);
            _db.AddInParameter(command, "@totalEnquete", DbType.Int32, totalEnquete);

            _db.ExecuteNonQuery(command);
        }

        public int TotalContadorOpcaoEnquete(Enquete enquete)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Verifica total do contador
            sbSQL.Append(" SELECT contador FROM  EnqueteOpcao where ");
            sbSQL.Append(" enqueteId=@enqueteId  and enqueteOpcaoId=@enqueteOpcaoId");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteOpcaoId", DbType.Int32, enquete.EnqueteOpcoes[0].EnqueteOpcaoId);
            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);

            IDataReader reader = _db.ExecuteReader(command);

            reader.Read();

            Int32 totalEnquete = 0;

            if (reader["contador"] != DBNull.Value)
            {
                totalEnquete = Convert.ToInt32(reader["contador"].ToString());
            }

            reader.Close();

            return totalEnquete;
        }

        #endregion
    }
}
		