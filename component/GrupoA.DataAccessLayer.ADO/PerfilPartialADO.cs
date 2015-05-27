
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

using System.Collections.Generic;
using System.Text;


using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class PerfilADO : ADOSuper, IPerfilDAL
    {

        #region public List<Perfil> CarregarPerfisDoUsuario( Usuario usuario )
        /// <summary>
        /// Carrega todos os Perfis filtrados pelo código identificador do Usuário
        /// </summary>
        /// <param name="usuario">Objeto Usuário que contém o identificador usuarioId</param>
        /// <returns>Listagem de Perfis do Usuário</returns>
        public List<Perfil> CarregarPerfisDoUsuario(Usuario usuario)
        {
            List<Perfil> entidadeRetorno = new List<Perfil>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT p.* FROM usuario u ");
            sbSQL.Append(" INNER JOIN usuarioperfil up ON u.usuarioId = up.usuarioId ");
            sbSQL.Append(" INNER JOIN perfil p ON p.perfilId = up.perfilId ");
            sbSQL.Append(" WHERE u.usuarioId = @usuarioId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Perfil perfil = new Perfil();
                PopulaPerfil(reader, perfil);
                entidadeRetorno.Add(perfil);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

        #region public List<Perfil> CarregarTodosPorPromocao(Promocao promocao)
        /// <summary>
        /// Carrega todos os Perfis filtrados pelo código identificador da Promoção
        /// </summary>
        /// <param name="promocao">Objeto Promoção que contém o identificador promocaoId</param>
        /// <returns>Listagem de Perfis da Promoção</returns>
        public List<Perfil> CarregarTodosPorPromocao(Promocao promocao)
        {
            List<Perfil> entidadeRetorno = new List<Perfil>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT perf.* FROM PromocaoPerfil pp ");
            sbSQL.Append(" INNER JOIN Promocao p ON pp.promocaoId = p.promocaoId ");
            sbSQL.Append(" INNER JOIN Perfil perf ON perf.perfilId = pp.perfilId ");
            sbSQL.Append(" WHERE pp.promocaoId = @PromocaoId ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@PromocaoId", DbType.Int32, promocao.PromocaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Perfil perfil = new Perfil();
                PopulaPerfil(reader, perfil);
                entidadeRetorno.Add(perfil);
            }
            reader.Close();

            return entidadeRetorno;
        }
        #endregion

        #region public List<Perfil> CarregarTodosExcetoPerfis(List<Perfil> perfis)
        /// <summary>
        /// Método que carrega todos os Perfis exceto os Perfis recebidos por parâmetro.
        /// </summary>
        /// <param name="perfis">Coleção de Perfis que NÃO deve ser carregado.</param>
        /// <returns>Coleção de Perfis que deve ser carregado.</returns>
        public List<Perfil> CarregarTodosExcetoPerfis(List<Perfil> perfis)
        {

            List<Perfil> entidadeRetorno = new List<Perfil>();
            String ids = "";
            foreach (Perfil perfil in perfis)
            {
                ids += string.Concat(",", perfil.PerfilId);
            }

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(string.Concat("SELECT * FROM Perfil WHERE perfilId NOT IN (0", ids, ")"));

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            //_db.AddInParameter(command, "@Ids", DbType.Int64, ids);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Perfil perfil = new Perfil();
                PopulaPerfil(reader, perfil);
                entidadeRetorno.Add(perfil);
            }
            reader.Close();

            return entidadeRetorno;
        }

        #endregion

    }
}
		