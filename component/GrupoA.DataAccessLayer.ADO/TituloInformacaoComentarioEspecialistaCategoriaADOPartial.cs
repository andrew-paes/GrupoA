
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
	public partial class TituloInformacaoComentarioEspecialistaCategoriaADO : ADOSuper, ITituloInformacaoComentarioEspecialistaCategoriaDAL 
    {
        /// <summary>
        /// Método que remove um TituloInformacaoComentarioEspecialistaCategoria da base de dados.
        /// </summary>
        /// <param name="tituloInformacaoComentarioEspecialistaId">tituloInformacaoComentarioEspecialistaId a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirTodosPorComentarioEspecialista(Int32 tituloInformacaoComentarioEspecialistaId) 
		{		
			StringBuilder sbSQL = new StringBuilder();			
			DbCommand command;

			sbSQL.Append("DELETE FROM TituloInformacaoComentarioEspecialistaCategoria ");
            sbSQL.Append("WHERE tituloInformacaoComentarioEspecialistaId = @tituloInformacaoComentarioEspecialistaId ");
			
			command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, tituloInformacaoComentarioEspecialistaId);

								
			_db.ExecuteNonQuery(command);
		}

        public IEnumerable<Categoria> CarregarTodasAreasConhecimentoCategoria(Int32 tituloInformacaoComentarioEspecialistaId)
        {
            List<Categoria> entidadesRetorno = new List<Categoria>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM TituloInformacaoComentarioEspecialistaCategoria ");
            sbSQL.Append("WHERE tituloInformacaoComentarioEspecialistaId = @tituloInformacaoComentarioEspecialistaId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@tituloInformacaoComentarioEspecialistaId", DbType.Int32, tituloInformacaoComentarioEspecialistaId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                Categoria entidadeRetorno = new Categoria();
                if (reader["categoriaId"] != DBNull.Value)
                    entidadeRetorno.CategoriaId = Convert.ToInt32(reader["categoriaId"].ToString());

                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }
	}
}
		