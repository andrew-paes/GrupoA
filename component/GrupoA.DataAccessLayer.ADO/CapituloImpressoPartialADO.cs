
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
    public partial class CapituloImpressoADO : ADOSuper, ICapituloImpressoDAL
    {
        /// <summary>
        /// Método que carrega os Autores do Capitulo Impresso
        /// </summary>
        /// <param name="entidade">CapituloEletronico a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloAutor</returns>
        public List<Autor> CarregarCapituloImpressoAutor(CapituloImpresso entidade)
        {
            List<Autor> entidadesRetorno = new List<Autor>();
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Autor.* FROM CapituloImpressoAutor ");
            sbSQL.Append("JOIN Autor ON Autor.autorId = CapituloImpressoAutor.autorId ");
            sbSQL.Append("WHERE capituloImpressoId=@capituloImpressoId ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloImpressoId", DbType.Int32, entidade.CapituloImpressoId);
            IDataReader reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                Autor entidadeRetorno = new Autor();
                AutorADO.PopulaAutor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();
            return entidadesRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capituloBO"></param>
        /// <param name="tituloImpressoBO"></param>
        /// <returns></returns>
        public bool CapituloImpressoRelacionado(Capitulo capituloBO, TituloImpresso tituloImpressoBO)
        {
            bool entidadeRetorno = false;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(@"SELECT
								COUNT(*) AS Total
							FROM 
								CapituloImpresso
							WHERE
                                CapituloImpresso.capituloId = @capituloId
								AND CapituloImpresso.tituloId = @tituloId
                            ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloId", DbType.Int32, capituloBO.CapituloId);
            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloImpressoBO.TituloImpressoId);
            IDataReader entidades = _db.ExecuteReader(command);

            if (entidades.Read())
            {
                if (entidades["Total"] != DBNull.Value)
                {
                    if (Convert.ToInt32(entidades["Total"].ToString()) > 0)
                    {
                        entidadeRetorno = true;
                    }
                }
            }

            entidades.Close();

            return entidadeRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capituloBO"></param>
        /// <param name="tituloImpressoBO"></param>
        /// <returns></returns>
        public CapituloImpresso CarregarCapituloImpressoRelacionado(Capitulo capituloBO, TituloImpresso tituloImpressoBO)
        {
            CapituloImpresso entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM CapituloImpresso WHERE CapituloImpresso.capituloId = @capituloId AND CapituloImpresso.tituloImpressoId = @tituloImpressoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloId", DbType.Int32, capituloBO.CapituloId);
            _db.AddInParameter(command, "@tituloImpressoId", DbType.Int32, tituloImpressoBO.Titulo.TituloId);
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new CapituloImpresso();
                PopulaCapituloImpresso(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}
