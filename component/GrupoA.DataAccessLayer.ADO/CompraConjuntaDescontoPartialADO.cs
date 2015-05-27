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
	public partial class CompraConjuntaDescontoADO : ADOSuper, ICompraConjuntaDescontoDAL
	{
		#region Métodos de CompraConjuntaDesconto

		/// <summary>
		/// Método que remove da base de dados lista de CompraConjuntaDesconto relacionados com CompraConjunto.
		/// </summary>
		/// <param name="entidade">CompraConjuntaDesconto a ser excluído (somente o identificador é necessário).</param>		
		public void ExcluirRelacionado(CompraConjunta entidade)
		{
			StringBuilder sbSQL = new StringBuilder();
			DbCommand command;

			sbSQL.Append(@"DELETE FROM 
									CompraConjuntaDesconto 
							WHERE 
									CompraConjuntaDesconto.compraConjuntaId=@compraConjuntaId
						");

			command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, entidade.CompraConjuntaId);

			_db.ExecuteNonQuery(command);
		}

        public CompraConjuntaDesconto CarregarPorCompraConjuntaValor(Int32 compraConjuntaId, Int32 quantidade)
        {
            CompraConjuntaDesconto entidadeRetorno = null;

            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("SELECT TOP 1 * ");
            sbSQL.Append("FROM CompraConjuntaDesconto ");
            sbSQL.Append("WHERE compraConjuntaId = @compraConjuntaId ");
            sbSQL.Append("      AND quantidadeMinima <= @quantidade ");
            sbSQL.Append("ORDER BY quantidadeMinima DESC");

            DbCommand command = _db.GetSqlStringCommand(sbSQL.ToString());

            _db.AddInParameter(command, "@compraConjuntaId", DbType.Int32, compraConjuntaId);
            _db.AddInParameter(command, "@quantidade", DbType.Int32, quantidade);

            IDataReader reader = _db.ExecuteReader(command);

            if (reader.Read())
            {
                entidadeRetorno = new CompraConjuntaDesconto();
                PopulaCompraConjuntaDesconto(reader, entidadeRetorno);
            }
            reader.Close();

            return entidadeRetorno;
        }

		#endregion
	}
}