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
    public partial class ContatoAssuntoADO : ADOSuper, IContatoAssuntoDAL {
        /// <summary>
        /// Método que retorna uma coleção de ContatoAssunto.
        /// </summary>
        /// <param name="registrosPagina">Número máximo de registros na página.</param>
        /// <param name="numeroPagina">Número da página atual (inicia em 0).</param>
        /// <param name="ordemColunas">Nome das colunas na ordem em que se deseja ordernar os resultados.</param>
        /// <param name="ordemSentidos">Sentidos das respectivas colunas de ordenção informadas no parâmetro ordemColunas (OrderBy.Ascendente ou OrderBy.Descendente).</param>		
        /// <param name="filtro">Objeto do tipo IFilter que contém os dados de filtragem.</param>		
        ///  <returns>Retorna um List contendos ContatoAssunto.</returns>
        public IEnumerable<ContatoAssunto> CarregarTodosDoSetor(ContatoSetor setor)
        {

            List<ContatoAssunto> entidadesRetorno = new List<ContatoAssunto>();

            StringBuilder sbSQL = new StringBuilder();
                  
            IDataReader reader;

            sbSQL.Append("SELECT ContatoAssunto.* FROM ContatoAssunto where  ContatoAssunto.contatoSetorId = @contatoSetorId");
            
            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());
            _db.AddInParameter(command, "@contatoSetorId", DbType.Int32, setor.ContatoSetorId);

            
            reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                ContatoAssunto entidadeRetorno = new ContatoAssunto();
                PopulaContatoAssuntoSetor(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;

        }

        /// <summary>
        /// Método que retorna popula um ContatoAssunto baseado nos dados de um DataReader.
        /// </summary>
        /// <param name="reader">IDataReader contendo os dados da consulta.</param>
        /// <param name="entidade">ContatoAssunto a ser populado(.</param>
        public static void PopulaContatoAssuntoSetor(IDataReader reader, ContatoAssunto entidade)
        {
            if (reader["contatoAssuntoId"] != DBNull.Value)
                entidade.ContatoAssuntoId = Convert.ToInt32(reader["contatoAssuntoId"].ToString());

            if (reader["nomeAssunto"] != DBNull.Value)
                entidade.NomeAssunto = reader["nomeAssunto"].ToString();

            if (reader["contatoSetorId"] != DBNull.Value)
            {
                entidade.ContatoSetor = new ContatoSetor();
                entidade.ContatoSetor.ContatoSetorId = Convert.ToInt32(reader["contatoSetorId"].ToString());
            }


        }	
    }
}
