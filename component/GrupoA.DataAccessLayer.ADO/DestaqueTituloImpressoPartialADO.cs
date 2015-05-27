
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
	public partial class DestaqueTituloImpressoADO : ADOSuper, IDestaqueTituloImpressoDAL {

        /// <summary>
        /// Método que persiste um DestaqueTituloImpressoRelacionado.
        /// </summary>
        /// <param name="destaqueTituloImpressoId"></param>	
        /// <param name="tituloId"></param>	
        public void InserirTituloRelacionado(int destaqueTituloImpressoId, int tituloId)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            // Monta a string de insert.
            sbSQL.Append(" INSERT INTO DestaqueTituloImpressoRelacionado ");
            sbSQL.Append(" (destaqueTituloImpressoId, tituloId) ");
            sbSQL.Append(" VALUES ");
            sbSQL.Append(" (@destaqueTituloImpressoId, @tituloId) ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, destaqueTituloImpressoId);

            _db.AddInParameter(command, "@tituloId", DbType.Int32, tituloId);


            // Executa a query.
            _db.ExecuteNonQuery(command);

        }

        /// <summary>
        /// Método que remove as associoções do DestaqueTituloImpresso
        /// </summary>
        /// <param name="entidade">TituloAutor a ser excluído (somente o identificador é necessário).</param>		
        public void ExcluirTitulosRelacionados(DestaqueTituloImpresso entidade)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;
            sbSQL.Append("DELETE FROM DestaqueTituloImpressoRelacionado ");
            sbSQL.Append("WHERE destaqueTituloImpressoId=@destaqueTituloImpressoId");
            command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);
            _db.ExecuteNonQuery(command);
        }

		/// <summary>
		/// Método que carrega um DestaqueTituloImpresso com a lista de Títulos relacionados
		/// </summary>
        /// <param name="entidade">DestaqueTituloImpresso a ser carregado (somente o identificador é necessário).</param>
		/// <returns>DestaqueTituloImpresso</returns>
        public DestaqueTituloImpresso CarregarComTitulosRelacionados(DestaqueTituloImpresso entidade)
        {		
		
			DestaqueTituloImpresso entidadeRetorno = null;
			
			StringBuilder sbSQL = new StringBuilder();
			
			sbSQL.Append("SELECT * FROM DestaqueTituloImpresso WHERE destaqueTituloImpressoId=@destaqueTituloImpressoId");
			
			DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
			
			_db.AddInParameter(command, "@destaqueTituloImpressoId", DbType.Int32, entidade.DestaqueTituloImpressoId);
			
			IDataReader reader = _db.ExecuteReader(command);
			
			if (reader.Read())
			{
				entidadeRetorno = new DestaqueTituloImpresso();
                PopulaComTitulosRelacionados(reader, entidadeRetorno);
			}
			reader.Close();
			
			return entidadeRetorno;
		}
			
		/// <summary>
        /// Método que retorna popula um DestaqueTituloImpresso baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">DestaqueTituloImpresso a ser populado(.</param>
		public static void PopulaComTitulosRelacionados(IDataReader reader, DestaqueTituloImpresso entidade) 
		{						
			if (reader["destaqueTituloImpressoId"] != DBNull.Value)
				entidade.DestaqueTituloImpressoId = Convert.ToInt32(reader["destaqueTituloImpressoId"].ToString());
			
			if (reader["nomeArea"] != DBNull.Value)
				entidade.NomeArea = reader["nomeArea"].ToString();

            
            //Carrega Titulos Relacionados
            TituloADO tituloADO = new TituloADO();
            entidade.Titulos = (List<Titulo>)tituloADO.CarregarTodosPorAreaDestaque(entidade.DestaqueTituloImpressoId);
            if (entidade.Titulos != null && entidade.Titulos.Count > 0)
            {
                for (int i = 0; i < entidade.Titulos.Count; i++)
                {
                    entidade.Titulos[i] = tituloADO.CarregarComDependencias(entidade.Titulos[i]);
                }
            }
		}		
		
	}
}
		