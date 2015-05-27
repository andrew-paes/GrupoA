
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
	public partial class TelefoneADO : ADOSuper, ITelefoneDAL
    {

        #region Métodos
        
        public List<Telefone> CarregarTelefonesUsuario(Usuario usuario)
        {

            List<Telefone> entidadeRetorno = new List<Telefone>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT t.*, tt.tipoTelefone FROM telefone t ");
            sbSQL.Append(" INNER JOIN telefoneTipo tt ON t.telefoneTipoId = tt.telefoneTipoId");
            sbSQL.Append(" WHERE usuarioId = @usuarioId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Telefone tel = new Telefone();
                PopulaTelefone(reader, tel);
                entidadeRetorno.Add(tel);
                TelefoneTipoADO.PopulaTelefoneTipo(reader, tel.TelefoneTipo);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public int ExcluiTelefonePorUsuarioId(int usuarioId)
        {
            this.ExcluirTelefoneDependencies(usuarioId);

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("DELETE FROM Telefone ");
            sbSQL.Append("WHERE usuarioId=@usuarioId ");
            //sbSQL.Append(" and telefoneid not in (select telefoneid from professorinstituicao where professorid = @usuarioId) ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int64, usuarioId);

            _db.ExecuteNonQuery(command); // Executa a query.

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        public void ExcluirTelefoneDependencies(int usuarioId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"UPDATE
	                            ProfessorInstituicao
                            SET
	                            telefoneId = NULL
                            WHERE
	                            telefoneId IN ( SELECT telefoneId FROM Telefone WHERE usuarioId = @usuarioId )");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int64, usuarioId);

            _db.ExecuteNonQuery(command); // Executa a query.
        }

        public void ExcluirPorProfessorInstituicao(ProfessorInstituicao professorInstituicao)
        {
                StringBuilder sbSQL = new StringBuilder();

                sbSQL.Append("DELETE FROM Telefone ");
                sbSQL.Append("WHERE Telefone.TelefoneId IN ( ");
                sbSQL.Append(" SELECT TelefoneDel.TelefoneId FROM Telefone TelefoneDel");
                sbSQL.Append(" JOIN ProfessorInstituicao ON ProfessorInstituicao.TelefoneId = TelefoneDel.TelefoneId AND ProfessorInstituicao.ProfessorInstituicaoId = @ProfessorInstituicaoId )");

                DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

                _db.AddInParameter(command, "@ProfessorInstituicaoId", DbType.Int64, professorInstituicao.ProfessorInstituicaoId);

                // Executa a query.

                //int resultado = (int) _db.ExecuteScalar(command);
                _db.ExecuteNonQuery(command);

        }

        public Telefone CarregarTelefonePorProfessorInstituicao(ProfessorInstituicao professorInstituicao)
        {

            //List<Telefone> entidadeRetorno = new List<Telefone>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT t.*, tt.tipoTelefone FROM telefone t ");
            sbSQL.Append(" INNER JOIN telefoneTipo tt ON t.telefoneTipoId = tt.telefoneTipoId");
            sbSQL.Append(" INNER JOIN ProfessorInstituicao ON ProfessorInstituicao.TelefoneId = t.TelefoneId AND ProfessorInstituicaoid = @ProfessorInstituicaoId");
            //sbSQL.Append(" WHERE usuarioId = @usuarioId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@ProfessorInstituicaoId", DbType.Int32, professorInstituicao.ProfessorInstituicaoId);

            IDataReader reader = _db.ExecuteReader(command);
            Telefone tel = null;
            if (reader.Read())
            {
                tel = new Telefone();
                PopulaTelefone(reader, tel);
                //entidadeRetorno.Add(tel);
                TelefoneTipoADO.PopulaTelefoneTipo(reader, tel.TelefoneTipo);
            }
            reader.Close();

            return tel;
        }

        #endregion

	}
}
		