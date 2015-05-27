using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using GrupoA.BusinessObject;

namespace GrupoA.DataAccess.ADO
{
    public partial class RevistaSecaoADO : ADOSuper, IRevistaSecaoDAL
    {
        #region Métodos

        /// <summary>
        /// Método que Carrega Todas Secoes Por RevistaId
        /// </summary>
        public IEnumerable<RevistaSecao> CarregarTodasSecoesPorRevistaId(int revistaId)
        {
            List<RevistaSecao> entidadeRetorno = new List<RevistaSecao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" SELECT * FROM RevistaSecao ");
            sbSQL.Append(" WHERE RevistaSecao.revistaId = @revistaId ");
            sbSQL.Append(" ORDER BY RevistaSecao.nomeSecao ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaSecao revistaSecao = new RevistaSecao();
                PopulaRevistaSecao(reader, revistaSecao);
                entidadeRetorno.Add(revistaSecao);
            }
            reader.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// Método que Exclui todos Registros de RevistaArtigoGaleriaImagem, de acordo com o revistaArtigoId recebido
        /// </summary>
        public void ExcluirRevistaArtigoGaleriaImagem(int revistaArtigoId)
        {

            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            sbSQL.Append("DELETE FROM RevistaArtigoGaleriaImagem ");
            sbSQL.Append("WHERE arquivoId=@arquivoId ");
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@arquivoId", DbType.Int32, 2);
            _db.ExecuteNonQuery(command);

            //new ArquivoADO().Excluir(entidade.Arquivo);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="revistaId"></param>
        /// <param name="revistaEdicaoId"></param>
        /// <returns></returns>
        public List<RevistaSecao> CarregarTodasSecoesPorRevistaIdEdicaoId(Int32 revistaId, Int32 revistaEdicaoId)
        {
            List<RevistaSecao> entidadeRetorno = new List<RevistaSecao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT *  ");
            sbSQL.Append("FROM RevistaSecao ");
            sbSQL.Append("WHERE revistaId = @revistaId ");
	        sbSQL.Append("    AND EXISTS (SELECT revistaSecaoId ");
			sbSQL.Append("                FROM RevistaArtigo ");
            sbSQL.Append("                WHERE revistaEdicaoId = @revistaEdicaoId AND RevistaArtigo.ativo = 1 ");
			sbSQL.Append("		            AND RevistaSecao.revistaSecaoId = RevistaArtigo.revistaSecaoId) ");
            sbSQL.Append("ORDER BY nomeSecao ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@revistaId", DbType.Int32, revistaId);
            _db.AddInParameter(command, "@revistaEdicaoId", DbType.Int32, revistaEdicaoId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                RevistaSecao revistaSecao = new RevistaSecao();
                PopulaRevistaSecao(reader, revistaSecao);
                entidadeRetorno.Add(revistaSecao);
            }
            reader.Close();

            return entidadeRetorno;
        }

        #endregion
    }
}

