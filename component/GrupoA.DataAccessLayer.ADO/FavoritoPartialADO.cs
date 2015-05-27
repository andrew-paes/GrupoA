
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
    public partial class FavoritoADO : ADOSuper, IFavoritoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="conteudo"></param>
        /// <returns></returns>
        public bool FavoritoRelacionado(Usuario usuario, Conteudo conteudo)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT 
                                COUNT(*) AS Total
                            FROM
                                Favorito
                            WHERE 
                                usuarioId = @usuarioId
                                AND conteudoId = @conteudoId
                            ");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@usuarioId", DbType.Int32, usuario.UsuarioId);
            _db.AddInParameter(command, "@conteudoId", DbType.Int32, conteudo.ConteudoId);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(reader["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }
            else
            {
                entidadeRetorno = false;
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}