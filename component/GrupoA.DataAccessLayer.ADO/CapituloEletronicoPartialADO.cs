
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
    public partial class CapituloEletronicoADO : ADOSuper, ICapituloEletronicoDAL
    {

        /// <summary>
        /// Método que carrega os Autores do Capitulo
        /// </summary>
        /// <param name="entidade">CapituloEletronico a ser carregado (somente o identificador é necessário).</param>
        /// <returns>TituloAutor</returns>
        public List<Autor> CarregarCapituloEletronicoAutor(CapituloEletronico entidade)
        {
            List<Autor> entidadesRetorno = new List<Autor>();
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT Autor.* FROM CapituloEletronicoAutor ");
            sbSQL.Append("JOIN Autor ON Autor.autorId = CapituloEletronicoAutor.autorId ");
            sbSQL.Append("WHERE capituloEletronicoId=@capituloEletronicoId ");
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloEletronicoId", DbType.Int32, entidade.CapituloEletronicoId);
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
        /// <param name="tituloEletronicoBO"></param>
        /// <returns></returns>
        public CapituloEletronico CarregarCapituloEletronicoRelacionado(Capitulo capituloBO, TituloEletronico tituloEletronicoBO)
        {
            CapituloEletronico entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM CapituloEletronico WHERE CapituloEletronico.capituloId = @capituloId AND CapituloEletronico.tituloEletronicoId = @tituloEletronicoId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@capituloId", DbType.Int32, capituloBO.CapituloId);
            _db.AddInParameter(command, "@tituloEletronicoId", DbType.Int32, tituloEletronicoBO.TituloEletronicoId);
            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new CapituloEletronico();
                PopulaCapituloEletronico(reader, entidadeRetorno);
            }

            reader.Close();

            return entidadeRetorno;
        }
    }
}