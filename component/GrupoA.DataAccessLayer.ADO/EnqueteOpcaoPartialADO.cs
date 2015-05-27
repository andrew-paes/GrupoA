
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
	public partial class EnqueteOpcaoADO : ADOSuper, IEnqueteOpcaoDAL
    {

        #region public List<EnqueteOpcao> CarregarTodosPorEnquete(Enquete enquete)
        /// <summary>
        /// Carrega em uma lista de Opções de Enquete (EnqueteOpcao) através de um código identificador recebido 
        /// Enquete.enqueteId.
        /// </summary>
        /// <param name="enquete">Objeto Enquete que deve conter o código identificador para filtragem das Opções.</param>
        /// <returns>Lista de Opções de Enquete.</returns>
        public List<EnqueteOpcao> CarregarTodosPorEnquete(Enquete enquete)
        {
            List<EnqueteOpcao> entidadesRetorno = new List<EnqueteOpcao>();

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT * FROM EnqueteOpcao WHERE enqueteId=@enqueteId");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);

            IDataReader reader = _db.ExecuteReader(command);

            while (reader.Read())
            {
                EnqueteOpcao entidadeRetorno = new EnqueteOpcao();
                PopulaEnqueteOpcao(reader, entidadeRetorno);
                entidadesRetorno.Add(entidadeRetorno);
            }
            reader.Close();

            return entidadesRetorno;
        }
        #endregion

        #region public void Excluir(Enquete enquete)
        /// <summary>
        /// Método que remove um EnqueteOpcao da base de dados.
        /// </summary>
        /// <param name="entidade">EnqueteOpcao a ser excluído (somente o identificador é necessário).</param>		
        public void Excluir(Enquete enquete)
        {
            StringBuilder sbSQL = new StringBuilder();
            DbCommand command;

            sbSQL.Append("DELETE FROM EnqueteOpcao ");
            sbSQL.Append("WHERE enqueteId=@enqueteId ");

            command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@enqueteId", DbType.Int32, enquete.EnqueteId);


            _db.ExecuteNonQuery(command);
        }
        #endregion

       



    }
}
		