
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
    public partial class BannerADO : ADOSuper, IBannerDAL
    {
        #region Métodos

        /// <summary>
        /// Método que carrega o Banners mais recentes de uma area.
        /// </summary>
        /// <param name="bannerAreaId">Area do Banner a ser carregado.</param>
        /// <returns>Banner</returns>
        public List<Banner> CarregarBannersPorArea(int qtdRegistros, int bannerAreaId, bool permiteFlash)
        {
            List<Banner> entidadesRetorno = new List<Banner>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(string.Concat(" SELECT TOP ", qtdRegistros.ToString(), " Banner.*, Arquivo.*, BannerArea.* FROM Banner "));
            sbSQL.Append(" INNER JOIN BannerLocalizacao ON Banner.bannerId = BannerLocalizacao.bannerId ");
            sbSQL.Append(" INNER JOIN BannerArea ON BannerLocalizacao.bannerAreaId = BannerArea.bannerAreaId ");
            sbSQL.Append(" INNER JOIN Arquivo ON Banner.arquivoId = Arquivo.arquivoId ");
            sbSQL.Append(" WHERE BannerArea.bannerAreaId=@bannerAreaId AND (dbo.Banner.ativo = 1) ");
            sbSQL.Append(" AND (dbo.Banner.arquivoId IS NOT NULL) ");
            sbSQL.Append(" AND (GETDATE() >= ISNULL(dbo.Banner.dataExibicaoInicio, GETDATE()) ");
            sbSQL.Append(" AND GETDATE() <= ISNULL(dbo.Banner.dataExibicaoFim, GETDATE())) ");

            if (!permiteFlash)
            {
                sbSQL.Append(" AND Arquivo.nomeArquivo NOT LIKE '%.swf' ");
            }

            sbSQL.Append(" ORDER BY NEWID(), dataExibicaoInicio DESC  ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@bannerAreaId", DbType.Int32, bannerAreaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Banner entidadeRetorno = new Banner();
                PopulaBanner(reader, entidadeRetorno);
                entidadeRetorno.Arquivo = new Arquivo();
                ArquivoADO.PopulaArquivo(reader, entidadeRetorno.Arquivo);

                entidadeRetorno.BannerAreas = new List<BannerArea>();
                BannerArea bannerArea = new BannerArea();
                BannerAreaADO.PopulaBannerArea(reader, bannerArea);
                entidadeRetorno.BannerAreas.Add(bannerArea);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        public List<BannerArea> CarregarAreasDoBanner(int bannerId)
        {
            List<BannerArea> entidadesRetorno = new List<BannerArea>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT  BannerArea.* ");
            sbSQL.Append("FROM BannerArea ");
            sbSQL.Append("INNER JOIN BannerLocalizacao ON BannerArea.bannerAreaId = BannerLocalizacao.BannerAreaId ");
            sbSQL.Append("WHERE BannerLocalizacao.bannerId = @bannerId ");


            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@bannerId", DbType.Int32, @bannerId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                BannerArea entidadeRetorno = new BannerArea();
                BannerAreaADO.PopulaBannerArea(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }

            reader.Close();

            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bannerId"></param>
        public void ExcluirAreasPorBanner(int bannerId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM BannerLocalizacao ");
            sbSQL.Append("WHERE bannerId=@bannerId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@bannerId", DbType.Int32, bannerId);

            _db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bannerId"></param>
        /// <param name="bannerAreaId"></param>
        public void InserirLocalizacaoBanner(int bannerId, int bannerAreaId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO BannerLocalizacao ");
            sbSQL.Append(" (bannerId, bannerAreaId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@bannerId, @bannerAreaId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@bannerId", DbType.Int32, bannerId);
            _db.AddInParameter(command, "@bannerAreaId", DbType.Int32, bannerAreaId);

            // Executa a query.
            _db.ExecuteNonQuery(command);
        }

        #endregion
    }
}